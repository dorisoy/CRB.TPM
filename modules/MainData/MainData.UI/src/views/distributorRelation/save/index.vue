<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" :label-width="140" v-on="on" @success="handleSuccess"
    @close="handleClose" @opened="handleOpened">
    <el-row>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.distributor.type_list.type_1')" prop="distributorId1">
          <m-maindata-select-page ref="distributorId1Ref" v-model="model.distributorId1" :action="queryCustomer" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.distributor.type_list.type_2')" prop="distributorId2">
          <m-maindata-select-page ref="distributorId2Ref" v-model="model.distributorId2" :action="queryDistributor" />
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
    const api = tpm.api.maindata.mdistributorrelation
    const {queryDistributorSelect} = tpm.api.maindata.mdistributor

    const model = reactive({
      distributorId1: '',
      distributorId2: '',
    })

    const rules = computed(() => {
      return {
        distributorId1: [{ required: true, message: $t('mod.maindata.distributor_relation.placeholder_type_1') }],
        distributorId2: [{ required: true, message: $t('mod.maindata.distributor_relation.placeholder_type_2') }],
      }
    })

    const distributorId1Ref = ref(null)
    const distributorId2Ref = ref(null)
    const { isEdit, bind, on } = useSave({ props, api, model, emit, afterEdit: () => {
        distributorId2Ref.value.remoteMethod('', true)
        distributorId1Ref.value.remoteMethod('', true)
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

    const queryCustomer = (pages, keyword, getValues) => {
      return new Promise(async resolve => {
        let params = {
          page: {index: pages.index, size: pages.size},
          name: keyword,
          type: 1
        }
        if(getValues && model.distributorId1){
          params.ids = [model.distributorId1]
        }
        const data = await queryDistributorSelect(params)
        resolve(data)
      })
    }

    const queryDistributor = (pages, keyword, getValues) => {
      return new Promise(async resolve => {
        let params = {
          page: {index: pages.index, size: pages.size},
          name: keyword,
          type: 2
        }
        if(getValues && model.distributorId2){
          params.ids = [model.distributorId2]
        }
        const data = await queryDistributorSelect(params)
        resolve(data)
      })
    }

    if(props.mode === 'add'){
      distributorId1Ref.value.remoteMethod('')
      distributorId2Ref.value.remoteMethod('')
    }

    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      distributorId1Ref,
      distributorId2Ref,
      handleSuccess,
      handleClose,
      handleOpened,
      queryCustomer,
      queryDistributor,
    }
  },
}
</script>
