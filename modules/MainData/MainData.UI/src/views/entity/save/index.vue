<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" :label-width="140" v-on="on" @success="handleSuccess"
    @close="handleClose" @opened="handleOpened">
    <el-row>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.entity.code')" prop="entityCode">
          <el-input ref="entityCodeRef" v-model="model.entityCode" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.entity.name')" prop="entityName">
          <el-input v-model="model.entityName" />
        </el-form-item>
      </el-col>
      <el-col :span="24">
        <el-form-item :label="$t('mod.maindata.entity.erp_code')" prop="erpCode">
          <el-input v-model="model.erpCode" />
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
    const api = tpm.api.maindata.mentity

    const model = reactive({
      entityCode: '',
      entityName: '',
      erpCode: '',
      enabled: true
    })

    const rules = computed(() => {
      return {
        entityCode: [{ required: true, message: $t('mod.maindata.entity.placeholder_code') }],
        entityName: [{ required: true, message: $t('mod.maindata.entity.placeholder_name') }],
        erpCode: [{ required: true, message: $t('mod.maindata.entity.placeholder_erp_code') }],
      }
    })

    const entityCodeRef = ref(null)
    const { isEdit, bind, on } = useSave({ props, api, model, emit })
    bind.autoFocusRef = entityCodeRef
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
      entityCodeRef,
      handleSuccess,
      handleClose,
      handleOpened,
    }
  },
}
</script>
