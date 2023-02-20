<template>
    <div style="padding: 50px">
        <m-form ref="formRef" :action="action" :model="model" :rules="rules" :disabled="disabled"
            @success="handleSuccess">

            <el-form-item label-width="150px" label="标题" prop="title">
                <el-input v-model="model.title" placeholder="TPM费用平台2.0" />
            </el-form-item>

            <el-form-item label-width="150px" label="版权申明" prop="copyright">
                <el-input v-model="model.copyright" placeholder="版权所有：CRB.TPM" />
            </el-form-item>

            <el-form-item label-width="150px" label="Logo" prop="logo">
                <!-- <m-file-upload-img v-model="model.logo" module="Admin" group="Logo" :access-mode="1" width="75px"
                    height="75px" no-text icon-size="3em" @success="onSuccess" /> -->
                <el-upload class="upload-demo" drag action="" multiple>
                    <!-- <m-icon name="upload" size="30px">
                        <upload-filled />
                    </m-icon> -->
                    <el-icon class="el-icon--upload"><upload-filled /></el-icon>
                    <div class="el-upload__text">
                        <em>点击上传</em>
                    </div>
                    <template #tip>
                        <div class="el-upload__tip">
                            jpg/png 文件大小不能超过 500kb
                        </div>
                    </template>
                </el-upload>
            </el-form-item>

            <el-form-item label-width="150px" label="账户信息页" prop="userPage">
                <el-input v-model="model.userPage" placeholder="请填写前端路由名称，默认使用系统自带的userinfo" />
            </el-form-item>

            <el-form-item label-width="150px">
                <m-button type="primary" @click="() => formRef.submit()" icon="save">保存</m-button>
                <m-button type="info" @click="() => formRef.reset()" icon="reset">重置</m-button>
            </el-form-item>
        </m-form>
    </div>
</template>

<script>
import { useMessage } from 'tpm-ui'
import { reactive, toRefs, ref } from 'vue'
import { UploadFilled } from '@element-plus/icons-vue'
export default {
    components: { UploadFilled },
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
            code: 'System',
            title: 'TPM费用平台2.0',
            logo: '',
            copyright: '版权所有:CRB.TPM',
            userPage: ''
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