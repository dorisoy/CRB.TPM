<template>
    <m-container>
      <m-list ref="listRef" :multiple="true" :title="$t(`tpm.routes.${page.name}`)" :icon="page.icon" :cols="cols" :query-model="model"
          :query-method="query" show-export :export-method="exportProduct" :delete-method="deleteMethod" show-delete-btn>
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
        const { query, exportProduct, remove, deleteSelected } = tpm.api.maindata.mproduct
        const model = ref({
            Name: null,
        })
        const cols = [
            {
                prop: 'productCode',
                label: 'mod.maindata.product.code',
                width: 150
            },
            {
                prop: 'productName',
                label: 'mod.maindata.product.name',
                width: 150,
            },
            {
                prop: 'bottleBox',
                label: 'mod.maindata.product.bottle_box',
                width: 150,
            },
            {
                prop: 'capacity',
                label: 'mod.maindata.product.capacity',
                width: 150,
            },
            {
                prop: 'className',
                label: 'mod.maindata.product.class_name',
                width: 150,
            },
            {
                prop: 'volumeName',
                label: 'mod.maindata.product.volume_name',
                width: 150,
            },
            {
                prop: 'brandName',
                label: 'mod.maindata.product.brand_name',
                width: 150,
            },
            {
                prop: 'outPackName',
                label: 'mod.maindata.product.out_pack_name',
                width: 150,
            },
            {
                prop: 'inPackName',
                label: 'mod.maindata.product.in_pack_name',
                width: 150,
            },
            {
                prop: 'productType',
                label: 'mod.maindata.product.product_type',
                width: 150,
            },
            {
                prop: 'productSpecName',
                label: 'mod.maindata.product.product_spec_name',
                width: 150,
            },
            {
                prop: 'litreConversionRate',
                label: 'mod.maindata.product.litre_conversion_rate',
                width: 150,
            },
            {
                prop: 'enabled',
                label: 'mod.maindata.enabled',
                width: 150,
            },
            {
                prop: 'parentId',
                label: 'mod.maindata.product.parent_id',
                width: 330,
            },
            {
                prop: 'groupCode',
                label: 'mod.maindata.product.group_code',
                width: 150,
            },
            {
                prop: 'groupName',
                label: 'mod.maindata.product.group_code',
                width: 150,
            },
            {
                prop: 'sort',
                label: 'mod.maindata.sort',
                width: 150,
            },
            {
                prop: 'characterCode',
                label: 'mod.maindata.product.character_code',
                width: 150,
            },
            {
                prop: 'remark',
                label: 'mod.maindata.remark',
                width: 150,
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
              label: 'mod.maindata.modified_time',
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
            exportProduct,
            ...list,
            deleteMethod
        }
    }
}
</script>
