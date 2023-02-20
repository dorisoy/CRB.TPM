<template>
    <m-container>
      <m-list ref="listRef" :multiple="true" :title="$t(`tpm.routes.${page.name}`)" :icon="page.icon" :cols="cols" :query-model="model"
          :query-method="query" show-export :export-method="exportTerminalDistributor" :delete-method="deleteMethod" show-delete-btn>
          <!--查询条件-->
          <template #querybar>
              <el-form-item :label="$t('mod.maindata.code_name')" prop="Name">
                  <el-input v-model="model.Name" clearable />
              </el-form-item>
              <m-maindata-query-org-group
                  v-model:divisionIds="model.divisionIds"
                  v-model:marketingIds="model.marketingIds"
                  v-model:dutyregionIds="model.dutyregionIds"
                  v-model:departmentIds="model.departmentIds"
                  v-model:stationIds="model.stationIds"
                  :select-start="3" :select-end="6"></m-maindata-query-org-group>
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
        const { query, exportTerminalDistributor, remove, deleteSelected } = tpm.api.maindata.mterminaldistributor
        const model = ref({
            Name: null,
        })
        const cols = [
            {
                prop: 'terminalCode',
                label: 'mod.maindata.terminal.code',
                width: 150,
            },
            {
                prop: 'terminalName',
                label: 'mod.maindata.terminal.name',
                width: 200
            },
            {
                prop: 'distributorCode',
                label: 'mod.maindata.distributor.code',
                width: 150
            },
            {
                prop: 'distributorName',
                label: 'mod.maindata.distributor.type_list.type_1',
                width: 200,
            }
        ]
        const list = useList()
        const refresh = () => {
            listRef.value.refresh()
        }
        const mode = ref("");

        const deleteMethod = ids => {
          return new Promise(async resolve => {
            resolve(await deleteSelected({ids: ids}))
          })
        }

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
            exportTerminalDistributor,
            ...list,
            deleteMethod
        }
    }
}
</script>
