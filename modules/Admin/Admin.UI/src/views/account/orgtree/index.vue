<template>
    <el-tree v-loading="loading" ref="treeRef" :data="treeData"
        :current-node-key="`00000000-0000-0000-0000-000000000000`" :props="nodeProps" :load="loadNode" lazy
        node-key="id" highlight-current @current-change="handleTreeChange" :expand-on-click-node="false"
        :check-strictly="true" show-checkbox @check="checkClick" :default-checked-keys="defaultkeys"
        :default-expanded-keys="defaultExpandedCids">
        <template #default="{ node, data }">
            <span>
                <m-icon :name="'folder-o'" class="m-margin-r-5" />
                <span>{{ data.label == null ? data.name : data.label }}</span>
            </span>
        </template>
    </el-tree>
</template>

<script>
import { ref, toRefs, watch, reactive } from 'vue'
import { useMessage } from 'tpm-ui'
export default {
    props: {
        nodeProps: {
            children: "children",
            label: "label"
        },
        selection: {
            type: Object
        },
        //创建时刷新
        refreshOnCreated: {
            type: Boolean,
            default: true,
        },
    },
    emits: ['change'],
    setup(props, { emit }) {
        const { store, $t } = tpm
        //maindata 集成后开始
        const { getTree, getOrgLevel } = tpm.api.admin.role
        const message = useMessage()
        const currentKey = ref('00000000-0000-0000-0000-000000000000')
        const treeRef = ref()
        const treeData = ref([])
        const loading = ref(false)

        const model = reactive({
            //选出所有cid
            defaultkeys: [],
            //选出所有pid为0的 cid节点
            defaultExpandedCids: []
        })

        //选择行时处理更改
        const handleTreeChange = (data) => {
            if (data != null) {
                currentKey.value = data.id
                //触发change事件
                emit('change', data.data)
                //选择行时触发checkbox
                let getNode = treeRef.value.getCheckedNodes()
                //console.log(getNode)
                if (getNode.length > 0) {
                    getNode.forEach(item => {
                        treeRef.value.setChecked(item.id, false);
                        treeRef.value.setChecked(data.key, true);
                    })
                }
                else {
                    treeRef.value.setChecked(data.key, true);
                }
            }
        }

        //checkbox 点击的时候触发
        const checkClick = (data) => {
            //console.log('checkClick', data)
            //先默认为多选模式， 获取目前所有被选中 checkbox 的集合 这里的集合是每个节点的数据。
            let getNode = treeRef.value.getCheckedNodes()
            //console.log('getNode', getNode)
            if (getNode.length > 0) {
                getNode.forEach(item => {
                    if (data.id == item.id) {
                        treeRef.value.setChecked(item.id, true);
                    } else {
                        treeRef.value.setChecked(item.id, false);
                    }
                })
            }
            //触发change事件
            emit('change', data)
        }

        //加载第一级节点
        const loadfirstnode = async (resolve, includes) => {
            loading.value = true;
            const res = await getTree({
                //root 根节点
                level: 10,
                includes: includes || []
            });
            //console.log('getTree -> res', res);
            let orgs = includes?.map(s => s.orgId)
            let data = res.filter((m) => m);
            if (orgs.length > 0) {
                //data = res.filter((m) => orgs?.includes(m.id.toLowerCase()));
            }
            loading.value = false;

            console.log('data', data);

            if (props.selection.orgId !== undefined && props.selection.orgId !== '' && props.selection.orgId !== 'undefined') {
                //console.log('props.selection.orgId', props.selection.orgId);
                model.defaultkeys.push(props.selection.orgId)
                model.defaultExpandedCids.push(props.selection.orgId)
            }

            if (data && Array.isArray(data)) {
                data.forEach(item => {
                    model.defaultExpandedCids.push(item.id)
                });
            }
            return resolve(data);
        }

        //加载节点的子节点集合
        const loadchildnode = async (node, resolve, includes) => {
            let params = {
                level: node.level + 1,
                parentId: node.key || "00000000-0000-0000-0000-000000000000",
                includes: includes || []
            };
            loading.value = true;
            console.log('loadchildnode - > params', params);
            let res = await getOrgLevel(params);
            //如果用户没有组织数据访问时，子节点不加载
            if (includes.length == 0) {
                //res = []
            }
            //只展开2级
            if (node.level >= 3) {
                res = []
            }
            console.log('loadchildnode', res);
            loading.value = false;
            if (res && Array.isArray(res)) {
                res.forEach(item => {
                    //添加展开节点
                    //model.defaultExpandedCids.push(item.id)
                });
            }
            return resolve(res);
        }

        //加载节点
        const loadNode = (node, resolve) => {
            let profile = store.state.app.profile
            let includes = profile.accountRoleOrgs

            console.log('node.level', node.level);
            console.log('profile', profile);
            console.log('includes', includes);
            //如果展开第一级节点，从后台加载一级节点列表
            if (node.level == 0) {
                loadfirstnode(resolve, includes);
            }
            //如果展开其他级节点，动态从后台加载下一级节点列表
            if (node.level >= 1) {
                loadchildnode(node, resolve, includes);
            }
        }

        //watch
        watch(props.selection, () => {
            if (props.selection.orgId !== undefined && props.selection.orgId !== '' && props.selection.orgId !== 'undefined') {
                treeRef.value.setChecked(props.selection.orgId, true);
            }
        });

        return {
            ...toRefs(model),
            loading,
            treeRef,
            treeData,
            handleTreeChange,
            checkClick,
            loadNode,
            loadfirstnode,
            loadchildnode
        }
    }
}
</script>
