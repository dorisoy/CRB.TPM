<template>
    <m-container>
      <m-list ref="listRef" :multiple="true" :title="$t(`tpm.routes.${page.name}`)" :icon="page.icon" :cols="cols" :query-model="model"
          :query-method="query" show-export :export-method="exportDistributor" :delete-method="deleteMethod" show-delete-btn>
          <!--查询条件-->
          <template #querybar>
              <el-form-item :label="$t('mod.maindata.code_name')" prop="Name">
                  <el-input v-model="model.Name" clearable />
              </el-form-item>
              <el-form-item :label="$t('mod.maindata.distributor.type')" prop="distributorType">
                <el-select v-model="model.DistributorType" clearable class="m-2" placeholder="请选择客户类型">
                  <el-option
                      v-for="item in distributorTypeOptions"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                  />
                </el-select>
              </el-form-item>
              <m-maindata-query-org-group
                  v-model:divisionIds="model.divisionIds"
                  v-model:marketingIds="model.marketingIds"
                  v-model:dutyregionIds="model.dutyregionIds"
                  v-model:departmentIds="model.departmentIds"
                  v-model:stationIds="model.stationIds"
                  :multiple="true"
                  :select-start="3" :select-end="6"></m-maindata-query-org-group>
          </template>
          <!--类型-->
          <template #col-distributorType="{ row }">
            <el-tag v-if="parseInt(row.distributorType) === 1" size="small" type="success" effect="dark">{{$t('mod.maindata.distributor.type_list.type_1')}}</el-tag>
            <el-tag v-else size="small" type="warning" effect="dark">{{ $t('mod.maindata.distributor.type_list.type_1') }}</el-tag>
          </template>
          <!--Detail类型-->
          <template #col-detailType="{ row }">
            <el-tag v-if="parseInt(row.detailType) === 1" size="small" type="success" effect="dark">{{ $t('mod.maindata.distributor.detailtype_list.type_1') }}</el-tag>
            <el-tag v-else-if="parseInt(row.detailType) === 2" size="small" type="warning" effect="dark">{{ $t('mod.maindata.distributor.detailtype_list.type_2') }}</el-tag>
            <el-tag v-else size="small" type="info" effect="dark">{{ $t('mod.maindata.distributor.detailtype_list.type_3') }}</el-tag>
          </template>
          <!--是否随crm同步工作站-->
          <template #col-isSynchronizeCrmStation="{ row }">
            <el-tag v-if="row.isSynchronizeCrmStation" type="success" size="small" effect="dark">{{ $t('mod.maindata.true') }}</el-tag>
            <el-tag v-else size="small" type="info" effect="error">{{$t('mod.maindata.false')}}</el-tag>
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
import {computed, ref} from 'vue'
import page from './page.json'
import Save from '../save/index.vue'

export default {
    components:{Save},
    setup() {
        const listRef = ref()
        const current = ref()
        const { store } = tpm;
        const maindataStore = store.state.mod.maindata;
        const { query, exportDistributor, remove, deleteSelected } = tpm.api.maindata.mdistributor
        const distributorTypeOptions = computed(() => {
          return maindataStore.distributor.typeAll
        })
        const model = ref({
            Name: null,
            DistributorType: 0,
            divisionIds: '',
            marketingIds: '',
            dutyregionIds: '',
            departmentIds: '',
            stationIds: ''
        })
        const cols = [
            {
                prop: 'distributorCode',
                label: 'mod.maindata.distributor.code',
                width: 150
            },
            {
                prop: 'distributorName',
                label: 'mod.maindata.distributor.name',
                width: 300,
            },
            {
                prop: 'distributorType',
                label: 'mod.maindata.distributor.type',
                width: 150
            },
            {
                prop: 'marketingName',
                label: 'mod.maindata.distributor.marketing_name',
                width: 150
            },
            {
                prop: 'dutyregionName',
                label: 'mod.maindata.distributor.dutyregion_name',
                width: 150
            },
            {
                prop: 'departmentName',
                label: 'mod.maindata.distributor.department_name',
                width: 150
            },
            {
                prop: 'stationName',
                label: 'mod.maindata.distributor.station_name',
                width: 150
            },
            {
                prop: 'entityName',
                label: 'mod.maindata.distributor.entity_name',
                width: 150
            },
            {
                prop: 'crmCode',
                label: 'mod.maindata.distributor.crm_code',
                width: 150
            },
            {
                prop: 'detailType',
                label: 'mod.maindata.distributor.detail_type',
                width: 150
            },
            {
                prop: 'customerCode',
                label: 'mod.maindata.distributor.customer_code',
                width: 150
            },
            {
                prop: 'parentId',
                label: 'mod.maindata.distributor.parent_id',
                width: 350
            },
            {
                prop: 'parentCode',
                label: 'mod.maindata.distributor.parent_code',
                width: 160
            },
            {
                prop: 'isSynchronizeCrmStation',
                label: 'mod.maindata.distributor.is_synchronize_crm_station',
                width: 180
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
                prop: 'Modifier',
                label: 'mod.maindata.modifier',
                width: 150
            },
            {
                prop: 'ModifiedTime',
                label: 'mod.maindata.modified_t ime',
                width: 180
            },
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
            exportDistributor,
            distributorTypeOptions,
            ...list,
            deleteMethod
        }
    }
}
</script>
