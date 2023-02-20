<template>
    <div style="padding: 50px">
        <m-form ref="formRef" :action="action" :model="model" :rules="rules" :disabled="disabled"
            @success="handleSuccess">

            <el-form-item labelWidth="200px" prop="uploadPath">
                <template v-slot:label>
                    <el-tooltip effect="dark" content="默认是应用根目录下的Upload目录" placement="top">
                        <nm-icon class="nm-size-20 nm-text-warning" name="warning" />
                    </el-tooltip>
                    文件上传存储根路径：
                </template>
                <el-input v-model="model.uploadPath" />
            </el-form-item>

            <el-form-item labelWidth="200px" prop="tempPath">
                <template v-slot:label>
                    <el-tooltip effect="dark" content="默认是应用根目录下的Temp目录" placement="top">
                        <nm-icon class="nm-size-20 nm-text-warning" name="warning" />
                    </el-tooltip>
                    临时文件存储根路径：
                </template>
                <el-input v-model="model.tempPath" />
            </el-form-item>

            <el-form-item label-width="200px">
                <m-button type="primary" @click="() => formRef.submit()" icon="save">保存</m-button>
                <m-button type="info" @click="() => formRef.reset()" icon="reset">重置</m-button>
            </el-form-item>

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
            code: 'Path',
            uploadPath: '',
            tempPath: ''
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