<template>
    <m-container>
        <m-list ref="listRef" :title="$t(`部门列表`)" :icon="icon" :cols="cols" :query-model="model" :query-method="query">
            <!--查询条件-->
            <template #querybar>
                <el-form-item label="名称" prop="name">
                    <el-input v-model="model.name" clearable />
                </el-form-item>
                <el-form-item label="编码" prop="code">
                    <el-input v-model="model.code" clearable />
                </el-form-item>
            </template>

            <!--按钮-->
            <template #buttons>
                <m-button-add :code="buttons.add.code" @click="add" />
            </template>

            <!-- 操作 -->
            <template #operation="{ row }">
                <m-button-edit :code="buttons.edit.code" @click="edit(row)" @success="refresh" />
                <m-button-delete :code="buttons.remove.code" :action="remove" :data="row.id"
                    @success="onRemoveSuccess" />
            </template>

        </m-list>
        <!--保存页-->
        <save :id="selection.id" :total="0" :parent="parent" v-model="saveVisible" :mode="mode"
            @onSuccess="onSaveSuccess" />
    </m-container>
</template>

<script>
import { useList, entityBaseCols } from 'tpm-ui'
import { reactive, computed, nextTick, toRefs, watch } from 'vue'
import { icon, buttons } from '../index/page.json'
import Save from '../save/index.vue'

export default {
    emits: ['onpSaveSuccess', 'onpRemoveSuccess'],
    components: { Save },
    //定义组件属性，表示一个parent节点对象
    props: {
        parent: {
            type: Object,
            required: true
        },
    },
    setup(props, { emit }) {
        const { query, remove } = tpm.api.ps.department
        const list = useList()
        const parentId = computed(() => props.parent.id)

        //视图模型
        const model = reactive({
            //父节点id
            parentId: 0,
            /** 名称 */
            name: '',
            /**编码 */
            code: ''
        })

        //列定义
        const cols = [
            {
                prop: 'id',
                label: '编号',
                width: 250,
                show: false
            },
            {
                prop: 'name',
                label: '名称'
            },
            {
                prop: 'code',
                label: '编码'
            },
            {
                prop: 'sort',
                label: '排序'
            },
            {
                prop: 'fullPath',
                label: '完整路径',
                show: true
            },
            {
                prop: 'creator',
                label: '创建人',
                width: 200,
                show: true
            },
            {
                prop: 'createdTime',
                label: '创建时间',
                width: 150,
                show: false
            },
            ...entityBaseCols()
        ]

        //Save项目保存成功时
        const onSaveSuccess = (model, data) => {
            console.log('Save项目保存成功时')
            // console.log('model:')
            // console.log(model)
            // console.log('data:')
            // console.log(data)
            // console.log('data.id:' + data.id)
            // console.log(list.mode)
            const isAdd = list.mode.value;
            if (isAdd == 'add') {
                model.id = data.id
            }
            //刷新list
            refresh()
            emit('onpSaveSuccess', model, data, isAdd)
        }

        //项目删除成功时
        const onRemoveSuccess = () => {
            console.log('项目删除成功时')
            //刷新list
            list.refresh()
            emit('onpRemoveSuccess')
        }

        // 编辑后父页面调用刷新
        const refresh = () => {
            console.log('list由父页面调用刷新:' + parentId.value)
            nextTick(() => {
                model.parentId = parentId.value
                //刷新list
                list.refresh()
            })
        }

        //监听parent变化，如在tree选择了节点时，会parent赋值
        //由于在父页面onTreeChange 中会调用 refresh，
        //所以watch这里面不需要处理本页面节点传值处理
        const { parent } = toRefs(props)
        watch(parent, () => {
            console.log('tree选择了节点:')
            console.log(parent)
        })

        return {
            icon,
            buttons,
            cols,
            query,
            remove,
            model,
            ...list,
            refresh,
            onSaveSuccess,
            onRemoveSuccess
        }
    }
}
</script>
