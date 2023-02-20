<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" :label-width="140" v-on="on" @success="handleSuccess"
    @close="handleClose" @opened="handleOpened">
    <el-row>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.org.org_code')" prop="orgCode">
          <el-input ref="orgCodeCodeRef" v-model="model.orgCode" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.org.label')" prop="orgName">
          <el-input v-model="model.orgName" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.org.parent')" prop="parentId">
         <m-maindata-input-table-filter ref="inputTableFilterRef" v-model="model.parentId" :title="$t('mod.maindata.org.placeholder_parent_id')" :disabled="mode===`add`" :query="parentQuery" :query-select="parentSelectQuery" :select-end="((model.type - 10) / 10 - 1)"></m-maindata-input-table-filter>
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.org.invalid_mapping')" prop="invalidMapping">
          <el-input v-model="model.invalidMapping" />
          <p>{{$t('mod.maindata.org.invalid_mapping_tip')}}</p>
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.remark')" prop="remark">
          <el-input v-model="model.remark" type="textarea" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.start_time')" prop="startTime">
          <el-date-picker
              v-model="model.startTime"
              type="date"
              :placeholder="$t('mod.maindata.placeholder_start_time')"
          />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.end_time')" prop="endTime">
          <el-date-picker
              v-model="model.endTime"
              type="date"
              :placeholder="$t('mod.maindata.placeholder_end_time')"
          />
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
import {computed, nextTick, reactive, ref} from 'vue'
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
    const { $t } = tpm
    const api = tpm.api.maindata.morg
    const inputTableFilterRef = ref(null)
    const model = reactive({
      orgCode: '',
      orgName: '',
      parentId: '',
      parentLabel: '',
      invalidMapping: '',
      remark: '',
      startTime: '',
      endTime: '',
      type: '',
      enabled: true
    })

    const rules = computed(() => {
      return {
        orgCode: [{ required: true, message: $t('请输入组织编码') }],
        orgName: [{ required: true, message: $t('请输入组织名称') }],
        parentId: [{ required: true, message: $t('父级Id不能为空') }],
      }
    })

    const orgCodeCodeRef = ref(null)
    const { isEdit, bind, on } = useSave({ props, api, model, emit, afterEdit: () => {
      //编辑时获取到信息后执行
      inputTableFilterRef.value.queryList(true)
    }})
    bind.autoFocusRef = orgCodeCodeRef
    bind.width = '700px'

    const parentQuery = (form) => {
        return new Promise(async resolve => {
            if(!model.type){
              return ''
            }
            let level = model.type - 10
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
            let data = await api.queryMorgSelect(params);
           resolve(data)
        })
    }

    const parentSelectQuery = () => {
        return new Promise(async resolve => {
            let level = model.type - 10
            let params = {
              level: level,
              ids: [model.parentId]
            }
            let data = await api.queryMorgSelect(params);
           resolve(data.rows)
        })
    }

    //窗体关闭时
    const handleClose = () => {
      //console.log('handleClose')
      //刷新父组件 refresh
      //emit('success')
    }

    //初始默认选择
    const handleOpened = () => {
      if(props.mode === 'add'){
        model.parentId = props.selection.parentId
        model.parentLabel = props.selection.parentLabel
        model.type = props.selection.level + 10
        inputTableFilterRef.value.queryList(true)
      }
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
      orgCodeCodeRef,
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
