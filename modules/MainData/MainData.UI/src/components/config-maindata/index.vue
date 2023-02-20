<template>
    <div style="padding: 50px">
        <m-form ref="formRef" :action="action" :model="model" :rules="rules" :disabled="disabled"
            @success="handleSuccess">
            <el-form-item label-width="300px" label="SAP链接字符串" prop="sapConnection">
                <el-switch v-model="model.loginLog" />
            </el-form-item>
            <el-form-item label-width="300px" label="同步CRM经销商、终端函数名" prop="syncCRMDtAndTmnFunctionName">
                <el-input v-model="model.defaultPassword" />
            </el-form-item>
            <el-form-item label-width="300px" label="SAP webservice 接口地址" prop="SapWsurl">
                <el-input v-model="model.defaultPassword" />
            </el-form-item>
            <el-form-item label-width="300px">
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
            sapConnection: '',
            syncCRMDtAndTmnFunctionName: '',
            SapWsurl: ''
        })

        const edits = () => {
            edit({
                type: model.type,
                code: model.code
            }).then(data => {
                const res = JSON.parse(data)
                model.sapConnection = res.sapConnection
                model.syncCRMDtAndTmnFunctionName = res.syncCRMDtAndTmnFunctionName
                model.SapWsurl = res.SapWsurl
                console.log('model', model)
            })
        }

        const rules = {
            sapConnection: [{ required: true, message: '请输入SAP链接字符串' }],
            syncCRMDtAndTmnFunctionName: [{ required: true, message: '请输入同步CRM经销商、终端函数名' }],
            SapWsurl: [{ required: true, message: '请输入SAP webservice 接口地址' }],
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
