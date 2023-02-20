<template>
  <m-container>
    <!-- 字典列表 -->
    <m-list ref="listRef" :title="$t('mod.admin.dict_list')" icon="list" :cols="cols" :query-model="model"
      :query-method="query" :query-on-created="false">
      <!-- 工具栏查询 -->
      <template #querybar>
        <el-form-item :label="$t('tpm.name')" prop="name">
          <el-input v-model="model.name" clearable />
        </el-form-item>
        <el-form-item :label="$t('tpm.code')" prop="code">
          <el-input v-model="model.code" clearable />
        </el-form-item>
      </template>
      <template #buttons>
        <!-- 添加字典 -->
        <m-button-add :code="buttons.add.code" @click="add" />
      </template>
      <template #operation="{ row }">
        <!-- 编辑字典项目明细 -->
        <m-button :text="true" icon="cog" @click="openItemDialog(row)"></m-button>
        <!-- 编辑字典 -->
        <m-button-edit :code="buttons.edit.code" @click="edit(row)" @success="refresh"></m-button-edit>
        <!-- 删除字典 -->
        <m-button-delete :code="buttons.remove.code" :action="remove" :data="row.id" @success="refresh">
        </m-button-delete>
      </template>
    </m-list>
    <!-- 保存 -->
    <save :id="selection.id" v-model="saveVisible" :group-code="groupCode" :mode="mode" @success="refresh" />
    <!-- 明细窗体 -->
    <item-dialog v-model="showItemDialog" />
  </m-container>
</template>
<script>
import { reactive, ref, toRefs, watch } from 'vue'
import { useList, entityBaseCols } from 'tpm-ui'
import { buttons } from '../index/page.json'
import Save from '../save/index.vue'
import ItemDialog from '../item/index/index.vue'
export default {
  components: { Save, ItemDialog },
  props: {
    groupCode: {
      type: String,
      default: '',
    },
  },
  setup(props) {
    const { store } = tpm

    const { query, remove } = tpm.api.admin.dict
    const { groupCode } = toRefs(props)

    const model = reactive({ groupCode, name: '', code: '' })
    const cols = [{ prop: 'id', label: 'tpm.id', width: '55', show: false }, { prop: 'name', label: 'tpm.name' }, { prop: 'code', label: 'tpm.code' }, ...entityBaseCols()]

    const list = useList()
    const showItemDialog = ref(false)

    const openItemDialog = row => {
      store.commit('mod/admin/setDict', { groupCode, dictCode: row.code })
      list.selection.value = row
      showItemDialog.value = true
    }

    watch(groupCode, () => {
      list.reset()
    })

    return {
      buttons,
      model,
      cols,
      query,
      remove,
      ...list,
      showItemDialog,
      openItemDialog,
    }
  },
}
</script>
