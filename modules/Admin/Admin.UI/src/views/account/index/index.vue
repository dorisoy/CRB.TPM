<template>
  <m-container>
    <m-list ref="listRef" :title="$t(`tpm.routes.${page.name}`)" :icon="page.icon" :cols="cols" :query-model="model"
      :query-method="query" @expand-change="handleExpand2">
      <template #querybar>
        <!-- 账户类型 -->
        <el-form-item :label="$t('账户类型')" prop="type">
          <m-admin-account-type-select v-model="model.type" />
        </el-form-item>
        <!-- 搜索关键字 -->
        <el-form-item :label="$t('搜索关键字')" prop="keys">
          <el-input v-model="model.keys" clearable placeholder="用户名/姓名/手机号" />
        </el-form-item>
        <!-- 姓名 -->
        <!-- <el-form-item :label="$t('mod.admin.name')" prop="name">
          <el-input v-model="model.name" clearable />
        </el-form-item> -->
        <!-- 手机号 -->
        <!-- <el-form-item :label="$t('tpm.phone')" prop="phone">
          <el-input v-model="model.phone" clearable />
        </el-form-item> -->
        <!-- 账户状态 -->
        <el-form-item :label="$t('账户状态')" prop="status">
          <!-- <el-switch v-model="model.status"></el-switch> -->
          <m-admin-account-status-select v-model="model.status" />
        </el-form-item>
        <!-- 选择组织 -->
        <el-form-item label="" prop="orgs">
          <m-admin-org-picker v-model="model" placeholder="过滤组织" @update="filterOrgs" />
        </el-form-item>
      </template>

      <template #buttons>
        <m-button-add :code="page.buttons.add.code" @click="add" />
        <m-button text type="primary" :icon="page.buttons.import.icon" :code="page.buttons.import.code"
          @click="importData">{{ page.buttons.import.text }}</m-button>
        <m-button text type="primary" :icon="page.buttons.importTemplate.icon" :code="page.buttons.importTemplate.code"
          @click="importData" :text="page.buttons.importTemplate.text">{{ page.buttons.importTemplate.text }}</m-button>
        <m-button text type="primary" :icon="page.buttons.export.icon" :code="page.buttons.export.code"
          @click="importData" :text="page.buttons.export.text">{{ page.buttons.export.text }}</m-button>
        <m-button text type="primary" :icon="page.buttons.batPermission.icon" :code="page.buttons.batPermission.code"
          @click="importData" :text="page.buttons.batPermission.text">{{ page.buttons.batPermission.text }}</m-button>
      </template>

      <!-- 状态 -->
      <template #col-status="{ row }">
        <el-tag v-if="row.status === 0" type="info" size="small" effect="dark">{{ $t('mod.admin.account_inactive') }}
        </el-tag>
        <el-tag v-else-if="row.status === 1" type="success" size="small" effect="dark">{{
            $t('mod.admin.account_activated')
        }}</el-tag>
        <el-tag v-if="row.status === 2" type="warning" size="small" effect="dark">{{ $t('mod.admin.account_disabled') }}
        </el-tag>
      </template>

      <!-- 角色 -->
      <template #col-roleName="{ row }">
        <el-tag v-for="item in row.roles" type="success" size="small" effect="dark" class="m-margin-r-10">
          {{ item.label }}
        </el-tag>
      </template>

      <!-- 操作 -->
      <template #operation="{ row }">
        <m-button :icon="page.buttons.roleorgset.icon" :link="true" type="primary" :code="page.buttons.roleorgset.code"
          @click="roleOrgSet(row)" @success="refresh">{{
              page.buttons.roleorgset.text
          }}</m-button>
        <m-button-edit :code="page.buttons.edit.code" @click="edit(row)" @success="refresh" />
        <m-button-delete :code="page.buttons.remove.code" :action="remove" :data="row.id" @success="refresh" />
      </template>

      <!-- 展开角色路径树 -->
      <template #expand="{ row }">
        <!-- 路径分支 -->
        <el-row :gutter="10">
          <!-- <el-col :span="4">
            <m-box header :title="'详细'" icon="list">
              <el-descriptions size="small">
                <el-descriptions-item :label="$t('mod.admin.extend_data')">{{ row.extend }}</el-descriptions-item>
                <el-descriptions-item :label="$t('tpm.creator')">{{ row.creator }}</el-descriptions-item>
                <el-descriptions-item :label="$t('tpm.created_time')">{{ row.createdTime }}</el-descriptions-item>
                <el-descriptions-item :label="$t('tpm.modifier')">{{ row.modifier }}</el-descriptions-item>
                <el-descriptions-item :label="$t('tpm.modified_time')">{{ row.modifiedTime }}</el-descriptions-item>
              </el-descriptions>
            </m-box>
          </el-col> -->

          <el-col :span="4">
            <el-table class="m-component_table" :data="row.mOrgs.headOffice" border>
              <el-table-column prop="name" label="雪花总部"></el-table-column>
            </el-table>
          </el-col>

          <el-col :span="4">
            <el-table class="m-component_table" :data="row.mOrgs.dbs" border>
              <el-table-column prop="name" label="事业部"></el-table-column>
            </el-table>
          </el-col>

          <el-col :span="4">
            <el-table class="m-component_table" :data="row.mOrgs.marketingCenters" border>
              <el-table-column prop="name" label="营销中心"></el-table-column>
            </el-table>
          </el-col>

          <el-col :span="4">
            <el-table class="m-component_table" :data="row.mOrgs.saleRegions" border>
              <el-table-column prop="name" label="销售大区"></el-table-column>
            </el-table>
          </el-col>

          <el-col :span="4">
            <el-table class="m-component_table" :data="row.mOrgs.departments" border>
              <el-table-column prop="name" label="业务部"></el-table-column>
            </el-table>
          </el-col>

          <el-col :span="4">
            <el-table class="m-component_table" :data="row.mOrgs.stations" border>
              <el-table-column prop="name" label="工作站"></el-table-column>
            </el-table>
          </el-col>
        </el-row>
      </template>
    </m-list>
    <save :selection="selection" :id="selection.id" v-model="saveVisible" :mode="mode" @success="refresh" />
    <role-org :selection="curSelection" :id="curSelection.id" v-model="roleOrgVisible" :mode="mode"
      @success="roleOrgRefresh" />
  </m-container>
</template>

<script>
import { useList, entityBaseCols, useMessage } from 'tpm-ui'
import { reactive, ref, watch } from 'vue'
import page from './page.json'
import Save from '../save/index.vue'
import RoleOrg from '../roleorg/index.vue'
import TreePage from '../tree/index.vue'

export default {
  components: { Save, RoleOrg, TreePage },
  props: {
  },
  emits: ["update:roleOrgVisible"],
  setup(props, { emit }) {
    //当前操作选择的列
    const curSelection = ref({})
    const showTree = ref(false)
    const message = useMessage()
    //Api
    const { query, remove } = tpm.api.admin.account
    const { getNodeTree, getPathByKey } = tpm.api.admin.role

    //{ name: 'select', desc: '当用户手动勾选数据行的 Checkbox 时触发的事件', params: 'selection, row' },

    const rowdatas = ref([{ name: 'select', desc: '当用户手动勾选数据行的 Checkbox 时触发的事件' }])

    const model = reactive({
      keys: '',
      // name: '',
      // phone: '',
      //账户类型
      type: -1,
      //账户状态
      status: -1,
      //组织过滤
      //雪花
      headOffice: [],
      //事业部
      dbs: [],
      //营销中心
      marketingCenters: [],
      //大区
      saleRegions: [],
      //业务部
      departments: [],
      //工作站
      stations: []
    })
    const cols = [
      { prop: 'id', label: 'tpm.id', width: '55', show: false },
      { prop: 'username', label: 'tpm.login.username' },
      { prop: 'name', label: 'mod.admin.name' },
      { prop: 'phone', label: 'tpm.phone', show: false },
      { prop: 'email', label: 'tpm.email', show: false },
      { prop: 'status', label: 'tpm.status' },
      { prop: 'accountRoles', label: '账户角色', expand: true },
      { prop: 'accountRoleOrgs', label: '账户角色组织', expand: true },
      { prop: 'orgId', label: '所在组织', show: true },
      { prop: 'userBP', label: 'BP编号', show: true },
      { prop: 'ldapName', label: 'LDAP账户', show: true },
      { prop: 'roleName', label: '拥有角色' },
      ...entityBaseCols(),
    ]
    const list = useList()
    const roleOrgVisible = ref(false)

    /**
     * 用户角色组织配置
     * @param {*} row 
     */
    const roleOrgSet = (row) => {
      if (row.roles?.length == 0) {
        message.alert('对不起，拒绝操作，角色好像还没指定哈！')
        return;
      }
      curSelection.value = row
      curSelection.value.headOffice = []
      curSelection.value.dbs = []
      curSelection.value.marketingCenters = []
      curSelection.value.saleRegions = []
      curSelection.value.departments = []
      curSelection.value.stations = []
      roleOrgVisible.value = true
      console.log('curSelection.value', curSelection.value)
    }

    /**
     * 更新
     * @param {*} data 
     */
    const roleOrgRefresh = (data) => {
      console.log('roleOrgRefresh')
      list.refresh()
      roleOrgVisible.value = false
    }

    const formatPaths = ref([])
    const mergePaths = ref([])

    /**
     * 折叠展开时：显示Step
     * @param {*} row 
     * @param {*} expandedRows 
     */
    const handleExpand = async (row, expandedRows) => {
      console.log('handleExpand -> row', row)
      const res = await getNodeTree(0)
      //let pathList = getPathByKey('cbb66d4d-cc1e-44e1-8a4e-ca2dae0e0270', res);
      //console.log('pathList', pathList)
      //let path = pathList.map((item) => item.label).join(" / ");
      //console.log('path', path);
      //formatPaths.value = pathList
      let rolePath = []
      if (row && Array.isArray(row.roles)) {
        row.roles.forEach(r => {
          // if (r.extends && Array.isArray(r.extends)) {
          //   r.extends.forEach(p => {
          //     let path = getPathByKey(p.orgId, res);
          //     paths.push(path)
          //   })
          // }
          // rolePath.push({
          //   role: r.label,
          //   roleId: r.value,
          //   paths: paths,
          //   tree: []
          // })
        });
      }
      formatPaths.value = rolePath
    }

    /**
     * 折叠展开时：显示树
     * @param {*} row 
     * @param {*} expandedRows 
     */
    const handleExpand2 = (row, expandedRows) => {
      console.log('handleExpand2 -> row', row)
      let data = {
        dbs: row.dbs,
        departments: row.departments,
        headOffice: row.headOffice,
        marketingCenters: row.marketingCenters,
        saleRegions: row.saleRegions,
        stations: row.stations,
      }
      rowdatas.value = data
      showTree.value = true
    }

    //导入
    const importData = () => { }

    //过滤结果
    const filterOrgs = (data) => {
      console.log('filterOrgs', data)
      model.headOffice = data.headOffice;
      model.dbs = data.dbs;
      model.marketingCenters = data.marketingCenters;
      model.saleRegions = data.saleRegions;
      model.departments = data.departments;
      model.stations = data.stations;
      console.log('model', model)
      //刷新list
      list.refresh()
    }



    return {
      ...list,
      page,
      model,
      cols,
      query,
      remove,
      roleOrgSet,
      roleOrgVisible,
      curSelection,
      roleOrgRefresh,
      handleExpand,
      handleExpand2,
      formatPaths,
      mergePaths,
      showTree,
      //--
      importData,
      filterOrgs,
      rowdatas
    }
  },
}
</script>
<style scoped>
.m-component_table {
  background: #eee;
}
</style>
