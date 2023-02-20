<template>
    <m-container>
        <m-list ref="listRef" :title="$t(`tpm.routes.${page.name}`)" :icon="page.icon" :cols="cols" :query-model="model"
            :query-method="query" show-export :export-method="exportAuditInfo">
            <!--查询条件-->
            <template #querybar>
                <el-form-item :label="$t('账户编号')" prop="accountId">
                    <el-input v-model="model.accountId" clearable />
                </el-form-item>
                <el-form-item :label="$t('模块')" prop="moduleCode">
                    <m-admin-module-select v-model="model.moduleCode" clearable />
                </el-form-item>
                <el-form-item :label="$t('控制器')" prop="controller">
                    <el-input v-model="model.controller" clearable />
                </el-form-item>
                <el-form-item :label="$t('操作')" prop="action">
                    <el-input v-model="model.action" clearable />
                </el-form-item>
                <el-form-item :label="$t('访问来源')" prop="platform">
                    <m-admin-platform-select v-model="model.platform" clearable />
                </el-form-item>
            </template>

            <!-- 工具栏 -->
            <template #toolbar>
                <m-date-range-picker :start.sync="model.startDate" :end.sync="model.endDate" @change="refresh"
                    class="auditInfo-range-picker" />
            </template>

            <template #col-moduleName="{ row }">
                <span>{{ row.moduleName }}({{ row.area }})</span>
            </template>

            <template #col-controller="{ row }">
                <span>{{ row.controllerDesc }}({{ row.controller }})</span>
            </template>

            <template #col-action="{ row }">
                <span>{{ row.actionDesc }}({{ row.action }})</span>
            </template>

            <template #col-executionDuration="{ row }">
                <span>{{ row.executionDuration }}ms</span>
            </template>

            <!--操作列-->
            <template #operation="{ row }">
                <m-button :text="true" :code="page.buttons.details.code" type="primary" icon="preview"
                    @click="openDetails(row)">详细
                </m-button>
            </template>
        </m-list>
        <!-- 审计信息 -->
        <detail v-model="showDetailDialog" :id="current" />
    </m-container>
</template>
<style>
.auditInfo-range-picker {
    margin-right: 15px;
}
</style>
<script>
import { useList } from 'tpm-ui'
import { ref } from 'vue'
import page from './page.json'
import Detail from '../detail/index.vue'

export default {
    components: { Detail },
    setup() {
        const listRef = ref()

        const current = ref()

        //显示审计详细对话框
        const showDetailDialog = ref(false)

        const { query, exportAuditInfo } = tpm.api.admin.auditInfo

        const model = ref({
            accountId: null,
            moduleCode: '',
            controller: null,
            action: null,
            platform: '',
            startDate: null,
            endDate: null
        })

        // const detailsPage = ref({
        //     visible: false,
        //     id: 0
        // })

        const cols = [
            {
                prop: 'id',
                label: '编号',
                show: false
            },
            {
                prop: 'accountName',
                label: '账户',
                export: {
                    width: 15
                }
            },
            {
                prop: 'module',
                label: '模块',
                export: {
                    width: 15
                }
            },
            {
                prop: 'controller',
                label: '控制器',
                export: {
                    width: 15
                }
            },
            {
                prop: 'action',
                label: '方法',
                export: {
                    width: 15
                }
            },
            {
                prop: 'platformName',
                label: '平台',
                export: {
                    width: 15
                }
            },
            {
                prop: 'ip',
                label: 'IP',
                export: {
                    width: 15
                }
            },
            {
                prop: 'executionTime',
                label: '执行时间',
                export: {
                    width: 20
                }
            },
            {
                prop: 'executionDuration',
                label: '执行用时(ms)',
                export: {
                    width: 15
                }
            }
        ]

        const list = useList()

        const refresh = () => {
            listRef.value.refresh()
        }

        const details = (row) => {
            console.log("details--------------->" + row.id)
            current.value = row.id
        }


        const openDetails = (row) => {
            showDetailDialog.value = true
            console.log("details--------------->" + row.id)
            current.value = row.id
        }

        return {
            current,
            showDetailDialog,
            listRef,
            refresh,
            page,
            model,
            cols,
            query,
            exportAuditInfo,
            ...list,
            details,
            openDetails
        }
    }
}
</script>