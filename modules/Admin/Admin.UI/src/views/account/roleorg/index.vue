<template>
  <m-form-dialog ref="dialogRef" :model="model" v-bind="bind" v-on="on" @opened="handleOpened" :loading="loading"
    title="配置用户角色组织">
    <m-tabs>
      <el-tabs v-model="defaulttab" @tab-change="handleTabChange">
        <!-- v-for="(res, key) in options.columns" -->
        <el-tab-pane v-for="(item, index) in curroles" :key="item.role.label" :name="item.role.value" :ref="childrefs"
          lazy>
          <template #label>
            <span>
              <m-icon name="user"></m-icon>
              <span>{{ item.role.label }}</span>
            </span>
          </template>
          <org-tab :ref="(el) => setref(el, item.role.value)" :role="item.role" :selection="item.childData" />
        </el-tab-pane>
      </el-tabs>
    </m-tabs>
  </m-form-dialog>
</template>
<script>
import { computed, reactive, ref, toRefs, nextTick } from 'vue'
import { regex, useSave, withSaveProps, useMessage } from 'tpm-ui'
import OrgTab from '../orgtab/index.vue'
export default {
  props: {
    ...withSaveProps,
    nodeProps: {
      children: "children",
      label: "label",
      isLeaf: "isLeaf"
    },
    selection: {
      type: Object
    },
    //创建时刷新
    refreshOnCreated: {
      type: Boolean,
      default: true,
    },
  },
  emits: ['success'],
  components: { OrgTab },
  setup(props, { emit }) {
    const api = tpm.api.admin.account
    //const { getNodeTree, getOrgLevel } = tpm.api.admin.role
    //const message = useMessage()
    const dialogRef = ref({})
    const loading = ref(false)
    const curroles = ref([])
    const childrefs = ref([]);

    //model
    const model = reactive({
      defaulttab: '',
    })

    let listData = {}
    let tabData = []
    const setref = (el, key) => {
      if (el) {
        listData[key] = el
      }
    }

    //窗体声明
    const nameRef = ref(null)
    const { bind, on } = useSave({ props, api, model, emit })
    bind.closeOnSuccess = true
    bind.destroyOnClose = true
    bind.autoFocusRef = nameRef
    bind.width = '1400px'
    bind.height = '790px'
    bind.title = "配置用户角色组织"
    bind.icon = 'edit'

    /**
 * 提交前处理
 */
    bind.beforeSubmit = () => {
      tabData = []
      for (let key in listData) {
        let m = listData[key].model
        console.log('listData -> model:', m)
        tabData.push({
          roleId: key,
          headOffice: m.headOfficeSelections.filter((s) => s.id).map(b => b.id),
          dbs: m.dbsSelections.filter((s) => s.id).map(b => b.id),
          marketingCenters: m.marketingCentersSelections.filter((s) => s.id).map(b => b.id),
          saleRegions: m.saleRegionsSelections.filter((s) => s.id).map(b => b.id),
          departments: m.departmentsSelections.filter((s) => s.id).map(b => b.id),
          stations: m.stationsSelections.filter((s) => s.id)
        })
      }
      //回传
      console.log('beforeSubmit -> tabData', tabData)
    }
    /**
     * 提交数据,更新账户角色组织
     */
    bind.action = () => {
      let curt = props.selection
      if (curt.roles?.length == 0) {
        bind.successMessage = '角色未指定'
        emit('success', null)
      }

      console.log('updateAccountRoleOrg -> curt', curt)

      let params = {
        id: curt.id,
        datas: tabData
      }

      return new Promise(resolve => {
        console.log('updateAccountRoleOrg', params)
        api.updateAccountRoleOrg(params).then((data) => {
          if (data.msg === 'success') {
            emit('success', data)
          }
          resolve(data)
        })
      })
    }

    /**
     * 初始加载
     */
    const handleOpened = async () => {
      curroles.value = []
      const { roles } = props.selection
      //console.log('roles', roles)
      if (Array.isArray(roles)) {
        model.defaulttab = roles[0].value
        roles.forEach(function (r, index) {
          let sel = props.selection
          let curData =
          {
            role: r,
            //childData: { ...sel }
            childData: deepClone(sel)
          }
          curroles.value.push(curData)
        });
      }
      console.log('curroles.value', curroles.value)
    }

    /**
     * 切换Tab角色时
     * @param {*} role 
     */
    const handleTabChange = (role) => {
      console.log('handleTabChange', role)
    }

    /**
     * 深度拷贝
     * @param {*} source 
     */
    const deepClone = (source) => {
      var sourceCopy = source instanceof Array ? [] : {}
      for (var item in source) {
        sourceCopy[item] = typeof source[item] === 'object' ? deepClone(source[item]) : source[item]
      }
      return sourceCopy
    }

    //return
    return {
      ...toRefs(model),
      dialogRef,
      model,
      bind,
      on,
      childrefs,
      deepClone,
      loading,
      curroles,
      handleOpened,
      handleTabChange,
      setref
    }
  }
}
</script>

<style scoped>
.m-roleorg-tree {
  padding-right: 0px;
}

.m-flex-auto {
  padding-left: 25px;
}

.m-flex-fixed {
  line-height: 30px;
  padding-right: 5px;
}

.m-roleorg-row {
  padding-right: 0px;
  background: #2965dd;
  color: #fff;
}

.m-roleorg-tree2 {
  padding-right: 0px;
  margin-top: 10px;
}

.m-roleorg-tree2>.m-roleorg-tag {
  padding: 10px;
  height: 270px;
  overflow: auto;
}

.m-roleorg-tree2>.m-roleorg-tag>.mx-1 {
  margin: 5px;
}

.m-roleorg-row>.el-col {
  /* text-align: -webkit-center; */
  font-weight: normal;
  /* margin-bottom: 10px; */
}

/* .m-roleorg-tree>.el-col {
  text-align: -webkit-center;
} */


.m-roleorg-border {
  border-top: 1px #ddd solid;
  border-bottom: 1px #ddd solid;
  border-right: 1px #ddd solid;
}

.m-roleorg-tree>.el-col:first-child,
.m-roleorg-tree2>.el-col:first-child {
  border-left: 1px #ddd solid;
}

.m-roleorg-tree>.el-col:last-child,
.m-roleorg-tree2>.el-col:last-child {
  border-right: 1px #ddd solid;
}
</style>