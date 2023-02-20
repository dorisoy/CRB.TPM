<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" :label-width="210" v-on="on" @success="handleSuccess"
    @close="handleClose" @opened="handleOpened">
    <el-row>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.distributor.code')" prop="distributorCode">
          <el-input ref="distributorCodeRef" v-model="model.distributorCode" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.distributor.name')" prop="distributorName">
          <el-input v-model="model.distributorName" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.distributor.type')" prop="distributorType">
          <m-select v-model="model.distributorType" :action="queryDistributorTypeOptions" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.org_level_name.station')" prop="stationId">
          <m-maindata-input-table-filter ref="inputTableFilterRef" v-model="model.stationId" :title="$t('mod.maindata.terminal.placeholder_station_id')" :query="parentQuery" :query-select="parentSelectQuery" :select-end="4"></m-maindata-input-table-filter>
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.distributor.crm_code')" prop="crmCode">
          <el-input v-model="model.crmCode" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.distributor.detail_type')" prop="detailType">
          <m-select v-model="model.detailType" :action="queryDistributorDetailTypeOptions" @change="handleDetailTypeChange" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.distributor.customer_code')" prop="customerCode">
          <el-input v-model="model.customerCode" />
        </el-form-item>
      </el-col>
      <el-col v-if="parentIdVisible" :span="24">
        <el-form-item :label="$t('mod.maindata.distributor.parent_id')" prop="parentId">
          <el-input v-model="model.parentId" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.distributor.is_synchronize_crm_station')" prop="isSynchronizeCrmStation	">
          <el-switch v-model="model.isSynchronizeCrmStation" :active-value="true" :inactive-value="false"></el-switch>
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
      type: Object
    }
  },
  emits: ['success'],
  setup(props, { emit }) {
    const { store, $t } = tpm
    const api = tpm.api.maindata.mdistributor
    const { queryMorgSelect } = tpm.api.maindata.morg
    const maindataStore = store.state.mod.maindata;
    const distributorTypeOptions = computed(() => {
      return maindataStore.distributor.type
    })

    const distributorDetailTypeOptions = computed(() => {
      return maindataStore.distributor.detailType
    })

    const queryDistributorTypeOptions = () => {
      return new Promise(resolve => {
        resolve(distributorTypeOptions.value)
      })
    }

    const queryDistributorDetailTypeOptions = () => {
      return new Promise(resolve => {
        resolve(distributorDetailTypeOptions.value)
      })
    }

    const model = reactive({
      distributorCode: '',
      distributorName: '',
      distributorType: 1,
      stationId: '',
      crmCode: '',
      detailType: 1,
      customerCode: '',
      parentId: '',
      isSynchronizeCrmStation: true
    })

    const rules = computed(() => {
      return {
        distributorCode: [{ required: true, message: $t('mod.maindata.distributor.placeholder_code') }],
        distributorName: [{ required: true, message: $t('mod.maindata.distributor.placeholder_name') }],
      }
    })

    const distributorCodeRef = ref(null)
    const parentIdVisible = ref(false);
    const { isEdit, bind, on } = useSave({ props, api, model, emit })
    bind.autoFocusRef = distributorCodeRef
    bind.width = '700px'

    //账户类型切换
    const handleDetailTypeChange = (v) => {
      parentIdVisible.value = v === 2
    }

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

    const parentQuery = (form, pages) => {
      return new Promise(async resolve => {
        let level = 60
        let params = {
          level: level,
          name: form.name,
          page: {
            index: pages.index,
            size: pages.size
          }
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

    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      distributorCodeRef,
      parentIdVisible,
      distributorTypeOptions,
      distributorDetailTypeOptions,
      queryDistributorTypeOptions,
      queryDistributorDetailTypeOptions,
      handleDetailTypeChange,
      handleSuccess,
      handleClose,
      parentQuery,
      handleOpened,
    }
  },
}
</script>
