<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" v-on="on" @open="handleOpen">
    <m-split v-model="splitRef">
      <template #fixed>
        <div style="padding:15px;">
          <el-form-item :label="$t('访问菜单组')" prop="menuGroupId" :label-width="lableWidth">
            <m-select v-model="model.menuGroupId" :action="$tpm.api.admin.menuGroup.select" checked-first></m-select>
          </el-form-item>
          <el-form-item :label="$t('角色名称')" prop="name" :label-width="lableWidth">
            <el-input ref="nameRef" v-model="model.name" />
          </el-form-item>
          <el-form-item :label="$t('唯一编码')" prop="code" :label-width="lableWidth">
            <el-input v-model="model.code" />
          </el-form-item>

          <el-form-item :label="$t('所属组织')" prop="orgId" :label-width="lableWidth">
            <el-input v-model="model.orgName" :readonly="true" placeholder="要指定角色所属组织，请先分配当前用户组织权限" />
          </el-form-item>

          <el-form-item :label="$t('CRM岗位代码')" prop="crmCode" :label-width="lableWidth">
            <el-input v-model="model.crmCode" placeholder="用于和CRM同步数据使用, 此处由用户手工维护" />
          </el-form-item>

          <el-form-item :label="$t('是否锁定')" prop="locked" :label-width="lableWidth">
            <el-switch v-model="model.locked"></el-switch>
          </el-form-item>

          <el-form-item :label="$t('tpm.remarks')" prop="remarks" :label-width="lableWidth">
            <el-input v-model="model.remarks" type="textarea" :rows="5" />
          </el-form-item>
        </div>
      </template>
      <template #auto>
        <div style="padding:15px;background-color: #f8f8f9;">
          <tree-page v-if="showTree" ref="tree" @change="onTreeChange" :selection="model" />
        </div>
      </template>
    </m-split>
  </m-form-dialog>
</template>
<script>
import { computed, reactive, ref, watch, nextTick } from 'vue'
import { useSave, withSaveProps as props } from 'tpm-ui'
import TreePage from '../tree/index.vue'
export default {
  props,
  emits: ['success'],
  components: { TreePage },
  setup(props, { emit }) {
    const {
      $t,
      api: {
        admin: { role: api },
      },
    } = tpm

    const lableWidth = '120px'
    const model = reactive({
      menuGroupId: '',
      name: '',
      code: '',
      orgId: '',
      orgName: '',
      crmCode: '',
      locked: false,
      remarks: ''
    })

    const rules = computed(() => {
      return {
        menuGroupId: [{ required: true, message: $t('mod.admin.select_menu_group') }],
        name: [{ required: true, message: $t('mod.admin.input_role_name') }],
        code: [{ required: true, message: $t('mod.admin.input_role_code') }],
      }
    })

    const nameRef = ref(null)
    const { bind, on } = useSave({ props, api, model, emit })
    bind.autoFocusRef = nameRef
    bind.width = '1000px'

    const splitRef = ref(0.5)
    const tree = ref()
    const showTree = ref(false)

    //当tree选择更改时，赋值 current 为当前数据
    const onTreeChange = (data) => {
      //console.log('onTreeChange', data)
      model.orgId = data.id
      model.orgName = data.label
    }

    //初始
    const handleOpen = () => {
    }

    watch(model, () => {
      showTree.value = true
    });

    return {
      model,
      handleOpen,
      rules,
      bind,
      on,
      nameRef,
      splitRef,
      onTreeChange,
      tree,
      showTree,
      lableWidth
    }
  },
}
</script>
