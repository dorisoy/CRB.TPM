<template>
  <el-select v-model="value_" :collapse-tags="true" :loading="loading" filterable remote :remote-method="remoteMethod" @visible-change="visibleChange" @change="handleChange">
    <slot :options="options">
      <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value" :disabled="item.disabled" />
    </slot>
    <el-pagination
      v-model="pages.index"
      small
      background
      layout="prev, pager, next"
      :total="pages.total"
      class="mt"
      @current-change="pageChange"
  />
  </el-select>
</template>
<script>
import {computed, inject, nextTick, ref} from 'vue'
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
    /** 搜索间隔，单位毫秒 */
    searchInterval: {
      type: Number,
      default: 700,
    },
  },
  emits: ['update:modelValue', 'change'],
  setup(props, { emit }) {
    const resetMethods = inject('resetMethods', [])

    const value_ = computed({
      get() {
        return props.modelValue
      },
      set(val) {
        emit('update:modelValue', val || '')
      },
    })

    let timer = null
    const first = ref(true)
    const loading = ref(false)
    const options = ref([])
    const searchKeyword = ref('')
    const pages = ref({
      total: 0,
      index: 1,
      size: 15
    })

    const remoteMethod = (keyword, getValues=false) => {
      searchKeyword.value = keyword
      if(!getValues){
        first.value = false
      }
      if (timer) clearTimeout(timer)
      timer = setTimeout(() => {
        // if (keyword !== '') {
          loading.value = true
          props
              .action(pages.value, keyword, getValues)
              .then(data => {
                options.value = data.rows
                pages.value.total = data.total
                if(first.value){
                  first.value = false
                  nextTick(() => {
                    setTimeout(() => {
                      options.value = []
                      pages.value.total = 0
                    }, 300)
                  })
                }
              })
              .finally(() => {
                loading.value = false
              })
        // } else {
        //   options.value = []
        // }
      }, props.searchInterval)
    }

    const handleChange = val => {
      const option = options.value.find(m => m.value === val)
      emit('change', val, option, options)
    }

    const reset = () => {
      value_.value = ''
    }

    const pageChange = (v) => {
      pages.value.index = v
      remoteMethod(searchKeyword.value)
    }

    const visibleChange = (v) => {
      if(v && options.value.length === 0){
        pages.value.index = 1
        remoteMethod(searchKeyword.value)
      }
    }

    resetMethods.push(reset)

    return {
      value_,
      loading,
      options,
      remoteMethod,
      handleChange,
      reset,
      pages,
      visibleChange,
      pageChange
    }
  },
}
</script>
