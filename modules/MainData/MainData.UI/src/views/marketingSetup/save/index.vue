<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" :label-width="210" v-on="on" @success="handleSuccess"
    @close="handleClose" @opened="handleOpened">
    <el-row>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.org_level_name.marketing_center')" prop="marketingId">
          <m-maindata-input-table-filter ref="inputTableFilterRef" v-model="model.marketingId" :title="$t('mod.maindata.marketing_setup.placeholder_marketing_center')" :query="parentQuery" :query-select="parentSelectQuery" :select-end="2"></m-maindata-input-table-filter>
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.marketing_setup.is_real')" prop="isReal">
          <el-switch v-model="model.isReal"></el-switch>
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.marketing_setup.is_synchronize_crm')" prop="isSynchronizeCrm">
          <el-switch v-model="model.isSynchronizeCrm"></el-switch>
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.marketing_setup.is_synchronize_crm_distributor_station')" prop="isSynchronizeCrmDistributorStation">
          <el-switch v-model="model.isSynchronizeCrmDistributorStation"></el-switch>
        </el-form-item>
      </el-col>
    </el-row>
  </m-form-dialog>
</template>
<script>
import { computed, reactive, ref } from 'vue'
import { regex, useSave, withSaveProps } from 'tpm-ui'

export default {
  props: {
    ...withSaveProps,
    selection: {
      type: Object
    }
  },
  emits: ['success'],
  setup(props, { emit }) {
    const { $t } = tpm
    const api = tpm.api.maindata.mmarketingsetup
    const {queryMorgSelect} = tpm.api.maindata.morg

    const model = reactive({
      marketingId: '',
      isReal: true,
      isSynchronizeCrm: true,
      isSynchronizeCrmDistributorStation: true,
    })

    const rules = computed(() => {
      return {
        marketingId: [{ required: true, message: $t('mod.maindata.marketing_setup.placeholder_marketing_center') }],
      }
    })

    const inputTableFilterRef = ref(null)
    const { isEdit, bind, on } = useSave({ props, api, model, emit, afterEdit: () => {
      //编辑时获取到信息后执行
      inputTableFilterRef.value.queryList(true)
    }})
    bind.width = '700px'

    //窗体关闭时
    const handleClose = () => {
      //console.log('handleClose')
      //刷新父组件 refresh
      //emit('success')
    }

    //初始默认选择
    const handleOpened = () => {

    }

    const handleSuccess = () => {
      //刷新父组件 refresh
      emit('success')
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
      inputTableFilterRef,
      parentQuery,
      parentSelectQuery,
      handleSuccess,
      handleClose,
      handleOpened,
    }
  },
}
</script>
