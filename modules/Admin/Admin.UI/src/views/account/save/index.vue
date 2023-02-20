<template>
  <m-form-dialog :model="model" :rules="rules" v-bind="bind" v-on="on" @success="handleSuccess" @close="handleClose"
    @opened="handleOpened">
    <m-split v-model="splitRef">
      <template #fixed>
        <div style="padding:15px;">
          <el-alert v-if="isEdit" class="m-margin-b-20" :title="$t('mod.admin.not_allow_edit_username')" type="warning">
          </el-alert>
          <el-row>
            <el-col :span="24">
              <!-- 账户类型 -->
              <el-form-item :label="$t('账户类型')" prop="type">
                <m-admin-account-type-select v-model="model.type" />
              </el-form-item>

              <!-- 用户名 -->
              <el-form-item :label="$t('tpm.login.username')" prop="username">
                <el-input ref="nameRef" v-model="model.username" :disabled="isEdit" />
              </el-form-item>

              <!-- 姓名 -->
              <el-form-item :label="$t('mod.admin.name')" prop="name">
                <el-input v-model="model.name" />
              </el-form-item>

              <!-- 电话 -->
              <el-form-item :label="$t('tpm.phone')" prop="phone">
                <el-input v-model="model.phone" />
              </el-form-item>

              <!-- 密码 -->
              <el-form-item :label="$t('tpm.login.password')" prop="password">
                <el-input v-model="model.password"
                  :placeholder="`${$t('mod.admin.default_password')}：${defaultPassword}`" :disabled="isEdit" />
              </el-form-item>

              <!-- 邮箱 -->
              <el-form-item :label="$t('tpm.email')" prop="email">
                <el-input v-model="model.email" />
              </el-form-item>

              <el-form-item :label="$t('所属组织')" prop="orgId">
                <el-input v-model="model.orgName" :readonly="true" placeholder="代表用户所在组织（部门），便于用户分级管理" />
              </el-form-item>

            </el-col>

            <el-col :span="24">
              <!-- 选择角色 -->
              <el-form-item :label="$t('tpm.role')" prop="roles">
                <m-select v-model="model.roles" :action="$tpm.api.admin.role.select" multiple />
              </el-form-item>
            </el-col>

            <el-form-item :label="$t('状态')" prop="status">
              <!-- <el-switch v-model="model.status"></el-switch> -->
              <m-admin-account-status-select v-model="model.status" />
            </el-form-item>

          </el-row>
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
import { computed, reactive, ref, watch } from 'vue'
import { regex, useSave, withSaveProps } from 'tpm-ui'
import TreePage from '../orgtree/index.vue'
export default {
  props: {
    ...withSaveProps,
    selection: {
      type: Object
    }
  },
  emits: ['success'],
  components: { TreePage },
  setup(props, { emit }) {
    const { store, $t } = tpm
    const api = tpm.api.admin.account
    const model = reactive({
      username: '',
      password: '',
      roles: [],
      name: '',
      phone: '',
      email: '',
      status: -1,
      //账户类型
      type: -1,
      orgId: '',
      orgName: ''
    })

    const splitRef = ref(0.5)
    const tree = ref()
    const showTree = ref(false)

    const rules = computed(() => {
      return {
        username: [{ required: true, message: $t('mod.admin.input_username') }],
        name: [{ required: true, message: $t('mod.admin.input_name') }],
        roles: [{ required: true, message: $t('mod.admin.select_role') }],
        phone: [{ pattern: regex.phone, message: $t('mod.admin.input_phone') }],
        email: [{ type: 'email', message: $t('mod.admin.input_email') }],
      }
    })

    const nameRef = ref(null)
    const { isEdit, bind, on } = useSave({ props, api, model, emit })
    bind.autoFocusRef = nameRef
    bind.width = '1200px'
    bind.height = '600px'

    const defaultPassword = ref('')
    api.getDefaultPassword().then(data => {
      defaultPassword.value = data
    })

    //窗体关闭时
    const handleClose = () => {
      //console.log('handleClose')
      //刷新父组件 refresh
      //emit('success')
    }

    //初始默认选择
    const handleOpened = () => {
      if (props.selection.roles.length > 0) {
        let ids = props.selection.roles.map(b => b.value)
        model.roles = ids;
      }
    }

    //角色选择规格时
    const selectChange = () => {
      // console.log('selectChange...')
      // console.log(model.roles)
    }

    const handleSuccess = () => {
      console.log('handleSuccess')
      //如果编辑的是当前登录人的信息，则执行刷新操作
      if (props.mode === 'edit' && props.id === store.state.app.profile.accountId) {
        store.dispatch('app/profile/init', null, { root: true })
          .then(() => {
            //刷新父组件 refresh
            emit('success')
          })
      } else {
        //刷新父组件 refresh
        emit('success')
      }
    }

    //当tree选择更改时，赋值 current 为当前数据
    const onTreeChange = (data) => {
      if (data != null) {
        console.log('onTreeChange', data)
        model.orgId = data.id
        model.orgName = data.name
      }
    }

    watch(model, () => {
      showTree.value = true
    });


    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      nameRef,
      defaultPassword,
      handleSuccess,
      handleClose,
      handleOpened,
      selectChange,
      //---
      splitRef,
      tree,
      showTree,
      onTreeChange
    }
  },
}
</script>
