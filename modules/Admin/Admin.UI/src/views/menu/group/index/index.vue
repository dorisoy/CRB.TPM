<template>
  <m-drawer ref="drawerRef" :title="$t('mod.admin.manage_group')" icon="list" width="900px">
    <!-- 菜单组列表 -->

    <m-list ref="listRef" :header="false" :cols="cols" height="768px" :query-model="model" :query-method="query">
      <template #querybar>
        <el-form-item :label="$t('tpm.name')" prop="name">
          <el-input v-model="model.name" clearable />
        </el-form-item>
      </template>
      <template #buttons>
        <m-button-add :code="buttons.groupAdd.code" @click="add" />
      </template>

      <template #operation="{ row }">
        <m-button-edit :code="buttons.groupEdit.code" @click="edit(row)" @success="handleChange"></m-button-edit>
        <m-button-delete :code="buttons.groupRemove.code" :action="remove" :data="row.id" @success="handleChange">
        </m-button-delete>
      </template>
    </m-list>
    <save :id="selection.id" v-model="saveVisible" :mode="mode" @success="handleChange" />
  </m-drawer>
</template>
<script>
import { useList, entityBaseCols } from 'tpm-ui'
import { ref, reactive } from 'vue'
import { buttons } from '../../index/page.json'
import Save from '../save/index.vue'
export default {
  components: { Save },
  emits: ['change'],
  setup(props, { emit }) {
    const { query, remove } = tpm.api.admin.menuGroup
    const model = reactive({ name: '' })
    const cols = [
      {
        prop: 'id',
        label: 'tpm.id',
        width: '55',
        show: false
      },
      {
        prop: 'name',
        label: 'tpm.name'
      },
      {
        prop: 'remarks',
        label: 'tpm.remarks'
      },
      ...entityBaseCols()
    ]
    const drawerRef = ref()
    const list = useList()

    const handleChange = () => {
      list.refresh()
      emit('change')
    }


    console.log('drawerRef.offsetHeight')
    console.log(drawerRef)


    return {
      drawerRef,
      buttons,
      model,
      cols,
      query,
      remove,
      ...list,
      handleChange,
    }
  },
}
</script>

