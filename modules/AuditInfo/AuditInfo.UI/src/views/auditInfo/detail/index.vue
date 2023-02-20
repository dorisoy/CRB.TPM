<template>
    <m-form-drawer :title="$t('审计日志详情')" icon="captcha" width="30%" :model="model" label-width="130px"
        :loading-text="$t('正在加载数据，请稍后...')" :btnOk="false" :btnReset="false">
        <el-form label-width="120px" disabled>
            <el-form-item label="账户：">{{ model.accountName }}</el-form-item>
            <el-form-item label="模块：">{{ model.module }}({{ model.area }})</el-form-item>
            <el-form-item label="控制器：">{{ model.controller }}({{ model.controllerDesc }})</el-form-item>
            <el-form-item label="方法：">{{ model.action }}({{ model.actionDesc }})</el-form-item>
            <el-form-item label="执行时间：">{{ model.executionTime }}</el-form-item>
            <el-form-item label="用时：">{{ model.executionDuration }}ms</el-form-item>
            <el-form-item label="IP：">{{ model.ip }}</el-form-item>
            <el-form-item label="平台：">{{ model.platformName }}</el-form-item>
            <el-form-item label="浏览器信息：">
                <el-input type="textarea" :value="model.browserInfo" :rows="5" />
            </el-form-item>
            <el-form-item label="参数：">
                <el-input type="textarea" :value="model.parameters" :rows="10" />
            </el-form-item>
            <el-form-item label="结果：">
                <el-input type="textarea" :value="model.result" :rows="10" />
            </el-form-item>
        </el-form>
    </m-form-drawer>
</template>

<script>
import { ref, toRefs, watch } from 'vue'
import { useMessage } from 'tpm-ui'
export default {
    components: {},
    props: {
        id: {
            type: Number,
            default: 0,
            required: true,
        },
    },
    setup(props) {
        const message = useMessage()
        const { id } = toRefs(props)
        const { details } = tpm.api.admin.auditInfo
        const model = ref({

        })

        const get = () => {
            let cid = id.value
            if (cid === '') {
                message.success('请选择要查看的数据~')
                return
            }
            details({ id: cid }).then(data => {
                //console.log(data)
                model.value = data
            })
        }

        const drawer = {
            header: true,
            title: '审计日志详情',
            icon: 'log',
            width: '600px',
            draggable: true
        }

        watch(id, () => {
            //console.log("watch--------------->" + id.value)
            if (id.value > 0) {
                get()
            }
        })

        return {
            drawer,
            model,
            get,
            details,
        }
    }
}
</script>
