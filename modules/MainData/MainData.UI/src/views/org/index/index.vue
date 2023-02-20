<template>
    <m-container>
      <m-split v-model="leftWidth">
        <template #fixed>
          <div class="left-tree">
            <el-row class="m-box-row" style="height: 100%">
              <el-col :span="24">
                <m-box header page :title="$t(`tpm.routes.${page.name}`)" icon="box">
                  <el-input
                      v-model="treeFilterKey"
                      class="tree-search-input"
                      clearable
                      :placeholder="$t('mod.maindata.org.placeholder_keyword')"
                      size="default"
                      @change="treeChangeKeyword"
                      @clear="treeChangeKeyword"
                      @input="treeChangeKeyword"
                  />
                  <el-tree
                      ref="treeRef"
                      v-loading="treeLoading"
                      :highlight-current="true"
                      :data="mtree"
                      :props="defaultProps"
                      node-key="id"
                      :filter-node-method="treeFilter"
                      :check-on-click-node="true"
                      :default-expanded-keys="defaultExpandedKeys"
                      :expand-on-click-node="false"
                      @node-click="handleNodeClick"
                  >
                    <template #default="{ node, data }">
                      <span class="custom-tree-node">
                        <span>{{ node.label }}</span>
                        <el-button
                            v-if="data.level < 60"
                            class="btn" type="primary"
                            link
                            @click.stop="addChildren(data)"
                        >{{ $t('mod.maindata.org.add_children') }}</el-button>
                      </span>
                    </template>
                  </el-tree>
                </m-box>
              </el-col>
            </el-row>
          </div>
        </template>
        <template #auto>
          <div class="right-table">
            <m-list ref="listRef" :show-export="true" :show-search-btn="false" :export-method="exportOrg" :show-reset-btn="false" :query-model="model" :query-method="queryChild" :title="treeSelected.label || '请选择'" :icon="page.icon" :cols="cols">
              <template #operation="{ row }">
                <m-button v-if="row.children" :link="true" :text="true" type="primary" icon="plus" @click="addChildren(row)">
                  {{ $t('mod.maindata.org.add_children') }}</m-button>
                <m-button-edit :code="page.buttons.edit.code" @click="edit(row)" @success="init" />
                <m-button-delete :code="page.buttons.remove.code" :action="remove" :data="row.id" @success="init" />
              </template>
              <template #buttons>
                <m-button-add v-if="treeSelected.id" :code="page.buttons.add.code" @click="add" />
              </template>
            </m-list>
          </div>
        </template>
      </m-split>
      <save :id="selection.id" v-model="saveVisible" :selection="selection" :mode="mode" @success="init"></save>
    </m-container>
</template>
<script>
import {nextTick, reactive, ref, toRefs} from 'vue'
import page from './page.json'
import Save from '../save/index.vue'
import {useMessage} from "tpm-ui";

export default {
    components:{Save},
    setup() {
      const treeRef = ref()
      const listRef = ref()
      const current = ref()
      const message = useMessage()
        const { tree, remove, exportOrg} = tpm.api.maindata.morg
        const model = ref({
            Name: null,
        })
        const state = reactive({
          mtree: [],
          leftWidth: '500px',
          treeFilterKey: '',
          defaultProps: {
            children: 'children',
            label: 'label',
            customNodeClass: 'org-tree'
          },
          selection: {id: 0, parentId:'', parentLabel: ''},
          saveVisible: false,
          mode: 'add',
          treeSelected: {},
          treeLoading: false,
          tableLoading: false,
          defaultExpandedKeys: []
        })

        const cols = [
            {
                prop: 'label',
                label: 'mod.maindata.org.label',
                width: 400,
                align: 'left'
            },
            {
              prop: 'id',
              label: 'mod.maindata.org.id',
              show: true,
              width: 350
            }
        ]
        const refresh = () => {
            listRef.value.refresh()
        }
        const handleNodeClick = (v) => {
          //总部级别、没有子类数据不允许点击
          if(v.level < 60 && v.level > 10){
            state.treeSelected = v
            refresh()
          }else{
            message.error($t('mod.maindata.org.error_level'))
          }
        }
        const init = async () => {
          //初始组织树列表
          state.treeLoading = true
          state.mtree = await tree({level: 60});
          state.treeLoading = false
          state.defaultExpandedKeys = [state.treeSelected.id || state.mtree[0].id]
          nextTick(() => {
            if(state.treeSelected.id){
              //记录刷新前选中数据，刷新后重新赋值
              const data = treeRef.value.getNode(state.treeSelected.id)
              if(data){
                state.treeSelected = data.data
                refresh()
              }
            }
          })
        }
        init()
        const queryChild = () => {
          return new Promise((resolve, reject) => {
            if(state.treeSelected.id){
              resolve({rows: state.treeSelected.children})
            }else{
              resolve({rows:[]})
            }
          })
        }
        const add = () => {
          state.mode = 'add'
          state.selection.id = ''
          state.selection.parentId = state.treeSelected.id
          state.selection.parentLabel = state.treeSelected.label
          state.selection.level = state.treeSelected.level
          state.saveVisible = true
        }
        const addChildren = (row) => {
          state.mode = 'add'
          state.selection.parentId = row.id
          state.selection.parentLabel = row.label
          state.selection.level = row.level
          state.saveVisible = true
        }
        const edit = (row) => {
          state.selection.id = row.id
          state.mode = 'edit'
          state.saveVisible = true
        }
        const treeFilter = (value, data) => {
          if (!value) {
            return true
          }
          return data.label.includes(value)
        }
        const treeChangeKeyword = (value) => {
          state.treeFilterKey = value
          treeRef.value.filter(value)
        }
        return {
            ...toRefs(state),
            current,
            listRef,
            treeRef,
            refresh,
            page,
            model,
            queryChild,
            cols,
            init,
            add,
            addChildren,
            edit,
            treeFilter,
            treeChangeKeyword,
            remove,
            exportOrg,
            handleNodeClick
        }
    }
}
</script>
<style lang="scss">
.left-tree{
  .el-tree-node__label{
    width: 100%;
  }
}
</style>
<style lang="scss" scoped>
.left-tree{
  background: #fff;
  height: 100%;
}
.right-table{
  background: #fff;
  height: 100%;
}
.custom-tree-node{
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
