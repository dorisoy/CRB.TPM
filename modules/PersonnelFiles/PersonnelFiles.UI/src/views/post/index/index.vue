<template>
  <m-container>
    <m-list ref="listRef" :title="$t(`tpm.routes.${page.name}`)" :icon="page.icon" :cols="cols" :query-model="model"
      :query-method="query">
      <template #querybar>
 
      </template>

      <template #buttons>
        <m-button-add :code="page.buttons.add.code" @click="add" />
      </template>

      <!-- 操作 -->
      <template #operation="{ row }">
        <m-button-edit :code="page.buttons.edit.code" @click="edit(row)" @success="refresh"></m-button-edit>
        <m-button-delete :code="page.buttons.remove.code" :action="remove" :data="row.id" @success="refresh">
        </m-button-delete>
      </template>
    </m-list>
  
  </m-container>
</template>

<script>
import { useList, entityBaseCols } from 'tpm-ui'
import { reactive } from 'vue'
import page from './page.json'
export default {
  components: {  },
  setup() {
    const { query, remove } = tpm.api.ps.post
    const model = reactive({  })
    const cols = [
      { prop: 'id', label: 'tpm.id', width: '55', show: false },
      ...entityBaseCols(),
    ]

    const list = useList()

    return {
      page,
      model,
      cols,
      query,
      remove,
      ...list,
    }
  },
}
</script>
