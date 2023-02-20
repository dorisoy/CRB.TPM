<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" :label-width="140" v-on="on" @success="handleSuccess"
    @close="handleClose" @opened="handleOpened">
    <el-row>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.org_level_name.marketing_center')" prop="marketingCode">
          <m-maindata-input-table-filter ref="marketingCodeRef" v-model="model.marketingId" :title="$t('mod.maindata.input_table_filter.placeholder_marketing_center')" :query="parentQuery" :query-select="parentSelectQuery" :select-end="2"></m-maindata-input-table-filter>
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.product.label')" prop="productId">
          <m-maindata-select-page ref="terminalIdRef" v-model="model.productId" :action="queryProductSelect" />
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
    const api = tpm.api.maindata.mmarketingproduct
    const { queryMorgSelect } = tpm.api.maindata.morg
    const { queryProductSelect } = tpm.api.maindata.mproduct

    const model = reactive({
      marketingId: '',
      productId: '',
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
        marketingId: '',
        productId: '',
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

    const parentQuery = (form) => {
      return new Promise(async resolve => {
        let level = 30
        let params = {
          level: level,
          name: form.name
        }
        if(level >= 20){
          params.level2Ids = form.divisionid
        }
        if(level >= 30){
          params.level2Ids = form.divisionid
          params.level3Ids = form.marketingcenterid
        }
        if(level >= 40){
          params.level2Ids = form.divisionid
          params.level3Ids = form.marketingcenterid
          params.level4Ids = form.dutyregionid
        }
        if(level >= 50){
          params.level2Ids = form.divisionid
          params.level3Ids = form.marketingcenterid
          params.level4Ids = form.dutyregionid
          params.level5Ids = form.departmentid
        }
        let data = await queryMorgSelect(params);
        resolve(data)
      })
    }

    const parentSelectQuery = () => {
      return new Promise(async resolve => {
        let level = 30
        let params = {
          level: level,
          ids: [model.marketingId]
        }
        let data = await queryMorgSelect(params);
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
      typeSelect,
      parentQuery,
      parentSelectQuery,
      queryProductSelect,
    }
  },
}
</script>
