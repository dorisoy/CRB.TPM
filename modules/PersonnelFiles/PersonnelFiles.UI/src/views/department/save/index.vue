<template>
    <m-form-dialog :model="model" :rules="rules" v-bind="bind" v-on="on" @success="handleSuccess">
        <el-alert v-if="isEdit" class="m-margin-b-20" :title="$t('不允许修改路径')" type="warning">
        </el-alert>
        <el-row>
            <el-col :span="20" :offset="1">
                <el-form-item label="路径">
                    <el-input v-model="parent.path" disabled />
                </el-form-item>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="20" :offset="1">
                <el-form-item label="名称" prop="name">
                    <el-input ref="nameRef" v-model="model.name" clearable />
                </el-form-item>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="20" :offset="1">
                <el-form-item label="排序" prop="sort">
                    <el-input v-model="model.sort" clearable />
                </el-form-item>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="20" :offset="1">
                <el-form-item label="编码" prop="code">
                    <el-input v-model="model.code" clearable />
                </el-form-item>
            </el-col>
        </el-row>
    </m-form-dialog>
</template>

<script>

import { computed, reactive, ref } from 'vue'
import { useSave, withSaveProps } from 'tpm-ui'

export default {
    props: {
        ...withSaveProps,
        parent: Object,
        total: Number
    },
    emits: ['onSuccess'],
    setup(props, { emit }) {
        const { add, edit, update } = tpm.api.ps.department
        //const { store, $t } = tpm
        const api = tpm.api.ps.department

        //视图模型
        const model = reactive({
            parentId: '',
            //名称
            name: '',
            //唯一编码
            code: '',
            //负责人
            leader: '',
            //排序
            sort: 0,
            //省（不启用）
            region: '',
            //市（不启用）
            zzcity: '',
            //区县（不启用）
            zzcounty: '',
            //街道（不启用）
            zzstreet_num: '',
        })

        //表单验证规则
        const rules = computed(() => {
            return {
                parentId: [{ required: true, message: '父路径没有指定' }],
                name: [{ required: true, message: '请输入名称' }],
                sort: [{ required: true, message: '请输入序号' }],
            };
        });

        const nameRef = ref(null)
        const { isEdit, bind, on } = useSave({ props, api, model, rules, emit })
        bind.autoFocusRef = nameRef
        bind.width = '700px'
        //表单提交成功后关闭对话框
        bind.closeOnSuccess = true
        bind.beforeSubmit = () => {
            //提交前设置父路径ID
            model.parentId = props.parent.id
        }

        //重置
        const onReset = () => {
            console.log('onReset:')
            console.log(props.parent.id)
            console.log(props.total)
            model.parentId = props.parent.id
            model.sort = props.total
            nameRef.focus()
        }

        //表单提交成功时
        const handleSuccess = (data) => {
            console.log('表单提交成功时->handleSuccess:')
            //通知saveSuccess
            emit('onSuccess', model, data)
        }

        return {
            model,
            rules,
            add,
            edit,
            update,
            onReset,
            handleSuccess,
            isEdit,
            bind,
            on,
            nameRef
        }
    }
}
</script>