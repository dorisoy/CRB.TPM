<!-- 该组件用于模拟测试 -->
<template>
  <el-select ref="sRef" v-model="value_" v-loading="loading" class="m-select"
    element-loading-background="rgba(255,255,255,.6)" :placeholder="placeholder || $t('tpm.please_select')">
    <slot :options="options">
      <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value"
        :disabled="item.disabled" />
    </slot>
  </el-select>
</template>
<script>
import { computed, inject, ref, watch } from 'vue'
export default {
  props: {
    modelValue: {
      type: [String, Number, Array],
      required: true,
    },
    action: {
      type: Function,
      required: true,
    },
    /** 是否选中第一个 */
    checkedFirst: Boolean,
    /** 是否创建时进行刷新 */
    refreshOnCreated: {
      type: Boolean,
      default: true,
    },
    placeholder: {
      type: String,
      default: '',
    },
  },
  emits: ['update:modelValue', 'update:label', 'change'],
  setup(props, { emit }) {

    const resetMethods = inject('resetMethods', [])
    const sRef = ref()
    const value_ = computed({
      get() {
        return props.modelValue
      },
      set(val) {
        emit('update:modelValue', val)
      },
    })

    //是否首次刷新
    let firstRefresh = true

    const loading = ref(false)
    const options = ref([])

    const refresh = () => {
      loading.value = true
      props
        .action()
        .then(data => {
          //options赋值
          options.value = data
          console.log('--------------------data')
          console.log(data)
          console.log('--------------------data')
          // console.log('firstRefresh:' + firstRefresh)
          // console.log('sRef.multiple:' + sRef.value.multiple)

          //是否首次刷新
          if (firstRefresh) {

            // console.log('value_.value')
            // console.log(value_.value)

            if (value_.value) {
              console.log('::1')
              //首次刷新并且存在初始值
              handleChange(value_.value)
            }
            else if (props.checkedFirst && data.length > 0) {
              console.log('::2')
              //首次刷新并且默认选中第一个选项
              const checkedValue = data[0].value
              value_.value = checkedValue
              handleChange(checkedValue)
            }
            else if (data.length > 0) {
              console.log('::3')
            }

            firstRefresh = false
          }

        })
        .finally(() => {
          loading.value = false
        })
    }

    const handleChange = val => {

      if (val == null || val == '') return;

      console.log('handleChange')
      console.log(val)

      if (sRef.value.multiple) {
        let list = []
        val.forEach(item => {
          //console.log(item.label)
          //console.log('options.length:' + options.value.length)
          for (var i = 0; i < options.value.length; i++) {
            const opt = options.value[i]
            //console.log(opt.label)
            if (opt.value === item.value) {
              list.push(opt)
              break
            }
          }
        })
        console.log('list===============')
        console.log(list)

        const selection = list[0]
        emit('update:label', selection != undefined ? selection.label : '')
        emit('change', val, list, options)

      }
      else {
        const selection = options.value.find(m => m.value === val)
        emit('update:label', selection != undefined ? selection.label : '')
        emit('change', val, selection, options)
      }
    }

    if (props.refreshOnCreated)
      refresh()

    const reset = () => {
      value_.value = ''
      handleChange('')
    }

    watch(value_, handleChange)

    resetMethods.push(reset)

    return {
      value_,
      loading,
      options,
      refresh,
      reset,
      handleChange,
      sRef
    }
  },
}
</script>
