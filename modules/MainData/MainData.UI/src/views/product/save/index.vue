<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" :label-width="140" v-on="on" @success="handleSuccess"
    @close="handleClose" @opened="handleOpened">
    <el-row>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.product.code')" prop="productCode">
          <el-input ref="productCodeRef" v-model="model.productCode" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.product.name')" prop="productName">
          <el-input v-model="model.productName" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.enabled')" prop="enabled">
          <el-switch v-model="model.enabled" :active-value="true" :inactive-value="false"></el-switch>
        </el-form-item>
      </el-col>
    </el-row>
  </m-form-dialog>
</template>
<script>
import { computed, reactive, ref } from 'vue'
import { useSave, withSaveProps } from 'tpm-ui'

export default {
  props: {
    ...withSaveProps,
    selection: {
      type: Object,
      default: () => {}
    }
  },
  emits: ['success'],
  setup(props, { emit }) {
    const { $t } = tpm
    const api = tpm.api.maindata.mproduct

    const model = reactive({
      productCode: '',
      productName: '',
      enabled: true
    })

    const rules = computed(() => {
      return {
        productCode: [{ required: true, message: $t('mod.maindata.product.placeholder_code') }],
        productName: [{ required: true, message: $t('mod.maindata.product.placeholder_name') }],
      }
    })

    const productCodeRef = ref(null)
    const { isEdit, bind, on } = useSave({ props, api, model, emit })
    bind.autoFocusRef = productCodeRef
    bind.width = '700px'

    //窗体关闭时
    const handleClose = () => {

    }

    //初始默认选择
    const handleOpened = () => {

    }

    const handleSuccess = () => {
      //刷新父组件 refresh
      emit('success')
    }

    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      productCodeRef,
      handleSuccess,
      handleClose,
      handleOpened,
    }
  },
}
</script>
