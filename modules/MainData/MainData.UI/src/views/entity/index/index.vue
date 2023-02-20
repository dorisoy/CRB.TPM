<template>
    <m-container>
      <m-list ref="listRef" :multiple="true" :title="$t(`tpm.routes.${page.name}`)" :icon="page.icon" :cols="cols" :query-model="model"
          :query-method="query" show-export :export-method="exportEntity" :delete-method="deleteMethod" show-delete-btn>
          <!--查询条件-->
          <template #querybar>
              <el-form-item :label="$t('mod.maindata.code_name')" prop="Name">
                  <el-input v-model="model.Name" clearable />
              </el-form-item>
          </template>
          <!--操作列-->
          <template #operation="{ row }">
            <m-button-edit :code="page.buttons.edit.code" @click="edit(row)" @success="refresh" />
            <m-button-delete :code="page.buttons.remove.code" :action="remove" :data="row.id" @success="refresh" />
          </template>
          <template #buttons>
            <m-button-add :code="page.buttons.add.code" @click="add" />
          </template>
      </m-list>
      <save :id="selection.id" v-model="saveVisible" :selection="selection" :mode="mode" @success="refresh"></save>
    </m-container>
</template>
<script>
import { useList } from 'tpm-ui'
import { ref } from 'vue'
import page from './page.json'
import Save from '../save/index.vue'

export default {
    components:{Save},
    setup() {
        const listRef = ref()
        const current = ref()
        const { query, exportEntity, remove, deleteSelected } = tpm.api.maindata.mentity
        const model = ref({
            Name: null,
        })
        const cols = [
            {
                prop: 'entityCode',
                label: 'mod.maindata.entity.code',
                width: 150
            },
            {
                prop: 'entityName',
                label: 'mod.maindata.entity.name',
                width: 300,
            },
            {
                prop: 'erpCode',
                label: 'mod.maindata.entity.erp_code',
                width: 150
            },
            {
                prop: 'enabled',
                label: 'mod.maindata.enabled',
                width: 150
            }
        ]
        const list = useList()
        const refresh = () => {
            listRef.value.refresh()
        }

        const deleteMethod = ids => {
          return new Promise(async resolve => {
            resolve(await deleteSelected({ids: ids}))
          })
        }
        const mode = ref("");
        return {
            current,
            listRef,
            refresh,
            page,
            mode,
            model,
            cols,
            query,
            remove,
            exportEntity,
            ...list,
            deleteMethod
        }
    }
}
</script>
