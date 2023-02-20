<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" :label-width="140" v-on="on" @success="handleSuccess"
    @close="handleClose" @opened="handleOpened">
    <el-row>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.terminal.label')" prop="terminalId">
          <m-maindata-select-page ref="terminalIdRef" v-model="model.terminalId" :action="queryTerminalSelectAction" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.terminal.account_name')" prop="accountId">
          <m-maindata-select-page ref="accountIdRef" v-model="model.accountId" :action="queryAccountAction" />
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
    const api = tpm.api.maindata.mterminaluser
    const {queryTerminalSelect} = tpm.api.maindata.mterminal
    const {queryAccountSelect} = tpm.api.admin.account

    const model = reactive({
      terminalId: '',
      accountId: '',
    })

    const rules = computed(() => {
      return {
        terminalId: [{ required: true, message: $t('mod.maindata.terminal.placeholder_select_terminal') }],
        accountId: [{ required: true, message: $t('mod.maindata.terminal.placeholder_account') }],
      }
    })

    const terminalIdRef = ref(null)
    const accountIdRef = ref(null)
    const { isEdit, bind, on } = useSave({ props, api, model, emit, afterEdit: () => {
        accountIdRef.value.remoteMethod('', true)
        terminalIdRef.value.remoteMethod('', true)
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

    const queryAccountAction = (pages, keyword, getValues) => {
      return new Promise(async resolve => {
        let params = {
          page: {index: pages.index, size: pages.size},
          name: keyword
        }
        if(getValues && model.accountId){
          params.ids = [model.accountId]
        }
        const data = await queryAccountSelect(params)
        resolve(data)
      })
    }

    const queryTerminalSelectAction = (pages, keyword, getValues) => {
      return new Promise(async resolve => {
        let params = {
          page: {index: pages.index, size: pages.size},
          name: keyword
        }
        if(getValues && model.terminalId){
          params.ids = [model.terminalId]
        }
        const data = await queryTerminalSelect(params)
        resolve(data)
      })
    }

    if(props.mode === 'add'){
      accountIdRef.value.remoteMethod('')
      terminalIdRef.value.remoteMethod('')
    }

    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      terminalIdRef,
      accountIdRef,
      queryTerminalSelectAction,
      queryAccountAction,
      handleSuccess,
      handleClose,
      handleOpened,
    }
  },
}
</script>
