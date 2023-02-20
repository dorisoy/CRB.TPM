<template>
  <m-container class="m-admin-role">
    <m-flex-row>
      <!-- 角色列表 -->
      <m-flex-fixed width="300px" class="m-margin-r-10">
        <m-list-box ref="listBoxRef" v-model="roleId" :title="$t('mod.admin.role_list')" icon="role" :action="query"
          @change="handleRoleChange" :header="true">
          <template #toolbar>
            <m-button :code="buttons.add.code" icon="plus" @click="add"></m-button>
          </template>

          <!-- 菜单组 -->
          <!-- <template #label="{ item }">
            {{ item.name }}
            <el-tag type="danger" size="small" effect="dark">
              {{ item.menuGroupName }}
            </el-tag>
          </template> -->

          <template #action="{ item }">
            <m-button-edit :code="buttons.edit.code" @click.stop="edit(item)" @success="refresh"></m-button-edit>
            <m-button-delete :code="buttons.remove.code" :action="remove" :data="item.id" @success="refresh">
            </m-button-delete>
          </template>
        </m-list-box>
      </m-flex-fixed>
      <!-- 菜单绑定树 -->
      <m-flex-auto>
        <m-flex-col class="m-fill-h">
          <m-flex-fixed>
            <m-box :title="$t('mod.admin.role_info')" icon="preview" show-collapse>
              <el-descriptions :column="4" border>
                <el-descriptions-item :label="$t('tpm.name')">{{ current.name }}</el-descriptions-item>

                <!-- 绑定菜单组 -->
                <el-descriptions-item :label="$t('mod.admin.bind_menu_group')">
                  <el-tag type="danger" size="small" effect="dark">{{ current.menuGroupName }}</el-tag>
                </el-descriptions-item>

                <!-- 所属组织 -->
                <el-descriptions-item :label="$t('所属组织')">
                  {{ current.orgId }}
                </el-descriptions-item>

                <!-- CRMCode -->
                <el-descriptions-item :label="$t('CRM岗位代码')">
                  {{ current.crmCode }}
                </el-descriptions-item>

                <el-descriptions-item :label="$t('tpm.code')">{{ current.code }}</el-descriptions-item>
                <el-descriptions-item :label="$t('mod.admin.is_lock')">
                  <el-tag v-if="current.locked" type="danger" effect="dark" size="small">{{ $t('mod.admin.lock') }}
                  </el-tag>
                  <el-tag v-else type="info" effect="dark" size="small">{{ $t('mod.admin.not_lock') }}</el-tag>
                </el-descriptions-item>
                <el-descriptions-item :label="$t('tpm.creator')">{{ current.creator }}</el-descriptions-item>
                <el-descriptions-item :label="$t('tpm.created_time')">{{ current.createdTime }}</el-descriptions-item>
                <el-descriptions-item :label="$t('tpm.remarks')" :span="2">{{ current.remarks }}</el-descriptions-item>
              </el-descriptions>
            </m-box>
          </m-flex-fixed>
          <m-flex-auto class="m-margin-t-10">
            <bind-menu :role="current" />
          </m-flex-auto>
        </m-flex-col>
      </m-flex-auto>
    </m-flex-row>
    <save :id="selection.id" v-model="saveVisible" :mode="mode" @success="refresh" />
  </m-container>
</template>
<script>
import { ref } from 'vue'
import { useList } from 'tpm-ui'
import { buttons } from './page.json'
import Save from '../save/index.vue'
import BindMenu from '../bind-menu/index.vue'
export default {
  components: { Save, BindMenu },
  setup() {
    const { query, remove } = tpm.api.admin.role
    const roles = ref([])
    const roleId = ref('')
    const current = ref({ id: 0 })
    const listBoxRef = ref()
    const { selection, mode, saveVisible, add, edit } = useList()

    const handleRoleChange = (val, role) => {
      current.value = role
    }

    const refresh = () => {
      console.log('listBoxRef.refresh')
      listBoxRef.value.refresh()
    }

    return {
      buttons,
      roles,
      roleId,
      current,
      listBoxRef,
      selection,
      mode,
      saveVisible,
      add,
      edit,
      remove,
      query,
      handleRoleChange,
      refresh,
    }
  },
}
</script>
<style lang="scss">
.m-admin-role {
  &_item {
    padding: 0 20px;
    height: 45px;
    line-height: 45px;
    cursor: pointer;
    border-bottom: 1px solid #ebeef5;

    &:hover {
      background-color: #ecf5ff;
    }

    &.active {
      background-color: #ecf5ff;
      border-left: 3px solid #409eff;
    }
  }
}
</style>
