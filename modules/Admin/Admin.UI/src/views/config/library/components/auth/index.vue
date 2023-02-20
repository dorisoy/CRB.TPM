<template>
    <div style="padding: 50px">
        <m-form ref="formRef" :action="action" :model="model" :rules="rules" :disabled="disabled"
            @success="handleSuccess">
            <el-divider content-position="left">认证&授权</el-divider>
            <el-row>
                <el-col :span="5" :offset="1">
                    <el-form-item label-width="150px" label="验证码" prop="verifyCode">
                        <el-switch v-model="model.verifyCode" />
                    </el-form-item>
                </el-col>
                <el-col :span="5">
                    <el-form-item label-width="150px" label="权限验证" prop="validate">
                        <el-switch v-model="model.validate" />
                    </el-form-item>
                </el-col>
                <el-col :span="5">
                    <el-form-item label-width="150px" label="按钮验证" prop="button">
                        <el-switch v-model="model.button" />
                    </el-form-item>
                </el-col>
                <el-col :span="5">
                    <el-form-item label-width="150px" label="单账户登录" prop="singleAccount">
                        <el-switch v-model="model.singleAccount" />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="5" :offset="1">
                    <el-form-item label-width="150px" label="审计日志" prop="auditing">
                        <el-switch v-model="model.auditing" />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-divider content-position="left">JWT参数</el-divider>
            <el-row>
                <el-col :span="10" :offset="1">
                    <el-form-item label-width="150px" label="密钥(Key)" prop="jwt.key">
                        <el-input v-model="model.jwt.key" clearable />
                    </el-form-item>
                </el-col>
                <el-col :span="10">
                    <el-form-item label-width="150px" label="有效期(Expires)" prop="jwt.expires">
                        <el-input v-model.number="model.jwt.expires">
                            <template slot="append">分钟</template>
                        </el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="10" :offset="1">
                    <el-form-item label-width="150px" label="发行人(Issuer)" prop="jwt.issuer">
                        <el-input v-model="model.jwt.issuer" clearable />
                    </el-form-item>
                </el-col>
                <el-col :span="10">
                    <el-form-item label-width="150px" label="消费者(Audience)" prop="jwt.audience">
                        <el-input v-model="model.jwt.audience" clearable />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-divider content-position="left">登录方式</el-divider>
            <el-row>
                <el-col :span="5" :offset="1">
                    <el-form-item label-width="150px" label="用户名">
                        <el-switch v-model="model.loginMode.userName" />
                    </el-form-item>
                </el-col>
                <el-col :span="5">
                    <el-form-item label-width="150px" label="邮箱">
                        <el-switch v-model="model.loginMode.email" />
                    </el-form-item>
                </el-col>
                <el-col :span="5">
                    <el-form-item label-width="150px" label="用户名或邮箱">
                        <el-switch v-model="model.loginMode.userNameOrEmail" />
                    </el-form-item>
                </el-col>
                <el-col :span="5">
                    <el-form-item label-width="150px" label="手机号">
                        <el-switch v-model="model.loginMode.phone" />
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="5" :offset="1">
                    <el-form-item label-width="150px" label="润工作扫码登录">
                        <el-switch v-model="model.loginMode.weChatScanCode" disabled />
                    </el-form-item>
                </el-col>
                <el-col :span="5">
                    <el-form-item label-width="150px" label="LDAP登录">
                        <el-switch v-model="model.loginMode.qq" disabled />
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
            code: 'Auth',
            verifyCode: false,
            validate: true,
            button: true,
            singleAccount: true,
            auditing: true,
            jwt: {
                key: '',
                issuer: '',
                audience: '',
                expires: 120,
                refreshTokenExpires: 7
            },
            loginMode: {
                userName: true,
                email: false,
                userNameOrEmail: false,
                phone: false,
                weChatScanCode: false,
                qq: false,
                gitHub: false
            }
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
