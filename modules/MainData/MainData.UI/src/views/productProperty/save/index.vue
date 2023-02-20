<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" :label-width="140" v-on="on" @success="handleSuccess"
    @close="handleClose" @opened="handleOpened">
    <el-row>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.product_properties.type_name')" prop="productPropertiesType">
          <m-select v-model="model.productPropertiesType" :action="typeSelect" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.product_properties.code')" prop="productPropertiesCode">
          <el-input v-model="model.productPropertiesCode" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.product_properties.name')" prop="productPropertiesName">
          <el-input v-model="model.productPropertiesName" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.sort')" prop="sort">
          <el-input v-model="model.sort" />
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
    const api = tpm.api.maindata.mproductproperty

    const model = reactive({
      productPropertiesType: '',
      productPropertiesCode: '',
      productPropertiesName: '',
      sort: '999'
    })

    const rules = computed(() => {
      return {
        productPropertiesType: [{ required: true, message: $t('mod.maindata.product_properties.placeholder_type') }],
        productPropertiesCode: [{ required: true, message: $t('mod.maindata.product_properties.placeholder_code') }],
        productPropertiesName: [{ required: true, message: $t('mod.maindata.product_properties.placeholder_name') }],
      }
    })

    const { isEdit, bind, on } = useSave({ props, api, model, emit })
    bind.width = '700px'

    //窗体关闭时
    const handleClose = () => {
      model.value = {
        productPropertiesType: '',
        productPropertiesCode: '',
        productPropertiesName: '',
        sort: '999'
      }
    }

    //初始默认选择
    const handleOpened = () => {

    }

    const handleSuccess = () => {
      //刷新父组件 refresh
      emit('success')
    }

    const typeSelect = () => {
      return new Promise(async resolve => {
        const data = await api.queryTypeSelect()
        resolve(data.rows)
      })
    }

    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      api,
      handleSuccess,
      handleClose,
      handleOpened,
      typeSelect
    }
  },
}
</script>
