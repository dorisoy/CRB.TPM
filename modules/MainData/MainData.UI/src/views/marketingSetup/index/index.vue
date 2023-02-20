<template>
    <m-container>
      <m-list ref="listRef" :multiple="true" :title="$t(`tpm.routes.${page.name}`)" :icon="page.icon" :cols="cols" :query-model="model"
          :query-method="query" show-export :export-method="exportMarketingSetup" :delete-method="deleteMethod" show-delete-btn>
          <!--查询条件-->
          <template #querybar>
              <el-form-item :label="$t('mod.maindata.code_name')" prop="Name">
                  <el-input v-model="model.Name" clearable />
              </el-form-item>
          </template>
          <!--是否真实营销中心-->
          <template #col-isReal="{ row }">
            <el-tag v-if="parseInt(row.isReal) === 1" size="small" type="success" effect="dark">是</el-tag>
            <el-tag v-else size="small" type="warning" effect="dark">否</el-tag>
          </template>
          <!--是否同步crm组织-->
          <template #col-isSynchronizeCrm	="{ row }">
            <el-tag v-if="parseInt(row.isSynchronizeCrm) === 1" size="small" type="success" effect="dark">是</el-tag>
            <el-tag v-else size="small" type="warning" effect="dark">否</el-tag>
          </template>
          <!--是否同步crm工作站-->
          <template #col-isSynchronizeCrmDistributorStation		="{ row }">
            <el-tag v-if="parseInt(row.isSynchronizeCrmDistributorStation) === 1" size="small" type="success" effect="dark">是</el-tag>
            <el-tag v-else size="small" type="warning" effect="dark">否</el-tag>
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
        const { query, exportMarketingSetup, remove, deleteSelected } = tpm.api.maindata.mmarketingsetup
        const model = ref({
            Name: null,
        })
        const cols = [
            {
                prop: 'orgCode',
                label: 'mod.maindata.marketing_setup.code',
                width: 150
            },
            {
                prop: 'orgName',
                label: 'mod.maindata.marketing_setup.name',
                width: 300,
            },
            {
                prop: 'isReal',
                label: 'mod.maindata.marketing_setup.is_real',
                width: 150
            },
            {
                prop: 'isSynchronizeCrm',
                label: 'mod.maindata.marketing_setup.is_synchronize_crm',
                width: 180
            },
            {
                prop: 'isSynchronizeCrmDistributorStation',
                label: 'mod.maindata.marketing_setup.is_synchronize_crm_distributor_station',
                width: 220
            },
            {
                prop: 'creator',
                label: 'mod.maindata.creator',
                width: 150
            },
            {
                prop: 'createdTime',
                label: 'mod.maindata.created_time',
                width: 180
            },
            {
                prop: 'modifier',
                label: 'mod.maindata.modifier',
                width: 150
            },
            {
                prop: 'modifiedTime',
                label: 'mod.maindata.modified_time',
                width: 180
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
        return {
            current,
            listRef,
            refresh,
            page,
            model,
            cols,
            query,
            remove,
            exportMarketingSetup,
            ...list,
            deleteMethod
        }
    }
}
</script>
