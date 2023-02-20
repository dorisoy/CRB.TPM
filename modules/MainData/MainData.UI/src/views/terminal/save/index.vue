<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" :label-width="140" v-on="on" @success="handleSuccess"
    @close="handleClose" @opened="handleOpened">
    <el-row>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.terminal.code')" prop="terminalCode">
          <el-input ref="terminalCodeRef" v-model="model.terminalCode" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.terminal.name')" prop="terminalName">
          <el-input v-model="model.terminalName" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.org_level_name.station')" prop="stationId">
          <m-maindata-input-table-filter ref="inputTableFilterRef" v-model="model.stationId" :title="$t('mod.maindata.terminal.placeholder_station_id')" :query="parentQuery" :query-select="parentSelectQuery" :select-end="5"></m-maindata-input-table-filter>
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.terminal.sale_line')" prop="saleLine">
          <el-input v-model="model.saleLine" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.terminal.level_1_type')" prop="lvl1Type">
          <el-input v-model="model.lvl1Type" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.terminal.level_2_type')" prop="lvl2Type">
          <el-input v-model="model.lvl2Type" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.terminal.level_3_type')" prop="lvl3Type">
          <el-input v-model="model.lvl3Type" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.address')" prop="address">
          <el-input v-model="model.address" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.status')" prop="status">
          <el-switch v-model="model.status" :active-value="true" :inactive-value="false"></el-switch>
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
    const api = tpm.api.maindata.mterminal
    const { queryMorgSelect } = tpm.api.maindata.morg

    const model = reactive({
      terminalCode: '',
      terminalName: '',
      distributorId: '',
      stationId: ''
    })

    const rules = computed(() => {
      return {
        terminalCode: [{ required: true, message: $t('mod.maindata.terminal.placeholder_terminal') }],
        terminalName: [{ required: true, message: $t('mod.maindata.terminal.placeholder_terminal_name') }],
        distributorId: [{ required: true, message: $t('mod.maindata.terminal.placeholder_distributor') }],
        stationId: [{ required: true, message: $t('mod.maindata.terminal.placeholder_station_id') }],
      }
    })

    const terminalIdRef = ref(null)
    const inputTableFilterRef = ref(null)
    const { isEdit, bind, on } = useSave({ props, api, model, emit, afterEdit: () => {
        //编辑时获取到信息后执行
        inputTableFilterRef.value.queryList(true)
      } })
    bind.autoFocusRef = terminalIdRef
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

    const parentSelectQuery = () => {
      return new Promise(async resolve => {
        let level = 60
        let params = {
          level: level,
          ids: [model.stationId]
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
      terminalIdRef,
      handleSuccess,
      handleClose,
      handleOpened,
      parentQuery,
      parentSelectQuery,
    }
  },
}
</script>
