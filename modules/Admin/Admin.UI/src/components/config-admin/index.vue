<template>
    <div style="padding: 50px">
        <m-form ref="formRef" :action="action" :model="model" :rules="rules" :disabled="disabled"
            @success="handleSuccess">
            <el-form-item label-width="150px" label="启用登录日志" prop="loginLog">
                <el-switch v-model="model.loginLog" />
            </el-form-item>
            <el-form-item label-width="150px" label="账户默认密码" prop="defaultPassword">
                <el-input v-model="model.defaultPassword" />
            </el-form-item>
            <el-form-item label-width="150px">
                <m-button type="primary" @click="() => formRef.submit()" icon="save">保存</m-button>
                <m-button type="info" @click="() => formRef.reset()" icon="reset">重置</m-button>
            </el-form-item>
        </m-form>
    </div>
</template>

<script>
import module from '../../module'
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
            type: 1,
            code: module.code,
            loginLog: false,
            defaultPassword: ''
        })

        const edits = () => {
            edit({
                type: model.type,
                code: model.code
            }).then(data => {
                const res = JSON.parse(data)
                model.loginLog = res.loginLog
                model.defaultPassword = res.defaultPassword
                console.log('model', model)
            })
        }

        const rules = {
            defaultPassword: [{ required: true, message: '请输入默认密码' }],
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
