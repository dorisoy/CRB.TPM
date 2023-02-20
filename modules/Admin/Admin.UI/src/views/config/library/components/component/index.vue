<template>
    <div style="padding: 50px">
        <m-form ref="formRef" :action="action" :model="model" :rules="rules" :disabled="disabled"
            @success="handleSuccess">
            <el-divider content-position="left">登录页</el-divider>
            <el-row>
                <el-col :span="10" :offset="1">
                    <el-form-item label-width="150px" label="默认账户类型" prop="defaultAccountType">
                        <el-select v-model="model.login.defaultAccountType">
                            <el-option label="管理员" :value="0"></el-option>
                            <el-option label="个人" :value="1"></el-option>
                            <el-option label="企业" :value="2"></el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
                <el-col :span="10">
                    <el-form-item label-width="150px" label="页类型" prop="pageType">
                        <el-select v-model="model.login.pageType">
                            <el-option v-for="item in pageTypeOptions" :key="item" :label="item"
                                :value="item"></el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-divider content-position="left">工具栏</el-divider>
            <el-row>
                <el-col :span="5" :offset="1">
                    <el-form-item label-width="150px" label="全屏控制" prop="fullscreen">
                        <el-switch v-model="model.toolbar.fullscreen" />
                    </el-form-item>
                </el-col>
                <el-col :span="5">
                    <el-form-item label-width="150px" label="皮肤设置" prop="skin">
                        <el-switch v-model="model.toolbar.skin" />
                    </el-form-item>
                </el-col>
                <el-col :span="5">
                    <el-form-item label-width="150px" label="退出按钮" prop="logout">
                        <el-switch v-model="model.toolbar.logout" />
                    </el-form-item>
                </el-col>
                <el-col :span="5">
                    <el-form-item label-width="150px" label="用户信息" prop="userInfo">
                        <el-switch v-model="model.toolbar.userInfo" />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-divider content-position="left">菜单</el-divider>
            <el-row>
                <el-col :span="10" :offset="1">
                    <el-form-item label-width="150px" label="保持子菜单的展开" prop="menu.uniqueOpened">
                        <el-switch v-model="model.menu.uniqueOpened" />
                    </el-form-item>
                </el-col>
                <el-col :span="10" :offset="1">
                    <el-form-item label-width="150px" label="侧栏是否默认收起" prop="menu.defaultExpanded">
                        <el-switch v-model="model.menu.defaultExpanded" />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-divider content-position="left">对话框</el-divider>
            <el-row>
                <el-col :span="10" :offset="1">
                    <el-form-item label-width="150px" label="可点击模态框关闭" prop="dialog.closeOnClickModal">
                        <el-switch v-model="model.dialog.closeOnClickModal" />
                    </el-form-item>
                </el-col>
                <el-col :span="10">
                    <el-form-item label-width="150px" label="默认可拖拽" prop="dialog.draggable">
                        <el-switch v-model="model.dialog.draggable" />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-divider content-position="left">列表页</el-divider>
            <el-row>
                <el-col :span="10" :offset="1">
                    <el-form-item label-width="150px" label="序号表头名称" prop="list.serialNumberName">
                        <el-input v-model="model.list.serialNumberName" placeholder="默认 #" />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-divider content-position="left">标签导航</el-divider>
            <el-row>
                <el-col :span="10" :offset="1">
                    <el-form-item label-width="150px" label="启用" prop="tabnav.enabled">
                        <el-switch v-model="model.tabnav.enabled" />
                    </el-form-item>
                </el-col>
                <el-col :span="10">
                    <el-form-item label-width="150px" label="显示图标" prop="tabnav.showIcon">
                        <el-switch v-model="model.tabnav.showIcon" />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="10" :offset="1">
                    <el-form-item label-width="150px" label="显示首页" prop="tabnav.showHome">
                        <el-switch v-model="model.tabnav.showHome" />
                    </el-form-item>
                </el-col>
                <el-col :span="10">
                    <el-form-item label-width="150px" label="首页地址" prop="tabnav.homeUrl">
                        <el-input v-model="model.tabnav.homeUrl" placeholder="路由的完整地址" />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="10" :offset="1">
                    <el-form-item label-width="150px" label="最大页面数量" prop="tabnav.maxOpenCount">
                        <el-input v-model.number="model.tabnav.maxOpenCount" placeholder="默认 20" />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-divider content-position="left">自定义CSS</el-divider>
            <el-row>
                <el-col :span="22" :offset="1">
                    <el-form-item label-width="150px" prop="customCss">
                        <el-input type="textarea" :rows="5" placeholder="如果需要重新某个组件的样式，可以在此处添加覆盖的CSS"
                            v-model="model.customCss"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>

            <el-row>
                <el-col :span="5" :offset="1">
                    <el-form-item label-width="150px">
                        <m-button type="primary" @click="() => formRef.submit()" icon="save">保存</m-button>
                        <m-button type="info" @click="() => formRef.reset()" icon="reset">重置</m-button>
                    </el-form-item>
                </el-col>
            </el-row>
        </m-form>
    </div>
</template>

<script>
import { useMessage } from 'tpm-ui'
import { reactive, toRefs, ref } from 'vue'
export default {
    components: {},
    setup() {
        const { edit, update } = tpm.api.admin.config
        const formRef = ref(null)
        const disabled = ref(false)
        const bind = reactive({
            header: false,
            page: false,
            labelWidth: '150px',
        });

        const message = useMessage()
        const model = reactive({
            type: 0,
            code: 'Component',
            login: { defaultAccountType: 0, pageType: null },
            menu: { uniqueOpened: true, defaultExpanded: false },
            dialog: { closeOnClickModal: false, draggable: false },
            list: { serialNumberName: null },
            tabnav: { enabled: true, showHome: true, homeUrl: null, showIcon: true, maxOpenCount: 20 },
            toolbar: { fullscreen: true, skin: true, logout: true, userInfo: true },
            customCss: null,
        })

        const edits = () => {

        }

        const rules = {
            //defaultPassword: [{ required: true, message: '请输入' }],
        }

        const action = () => {
            return update({
                type: model.type,
                code: model.code,
                json: JSON.stringify(model)
            })
        }

        const handleSuccess = () => {
            message.success('恭喜，配置保存成功')
        }

        //初始获取
        edits()

        return {
            ...toRefs(model),
            formRef,
            bind,
            model,
            edits,
            rules,
            action,
            disabled,
            handleSuccess
        }
    }
}
</script>

<style lang="scss">

</style>