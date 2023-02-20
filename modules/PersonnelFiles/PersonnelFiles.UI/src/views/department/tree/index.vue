<template>
    <m-box v-bind="box" show-collapse>
        <el-tree ref="treeRef" :data="treeData" :current-node-key="0" node-key="id" draggable highlight-current
            default-expand-all :expand-on-click-node="false" :allow-drop="handleTreeAllowDrop"
            :allow-drag="handleTreeAllowDrag" @current-change="handleTreeChange" @node-drop="handleTreeNodeDrop">
            <template #default="{ node, data }">
                <span>
                    <m-icon :name="data.item.icon || 'folder-o'" :style="{ color: data.item.iconColor }"
                        class="m-margin-r-5" />
                    <span>{{ node.label }}</span>
                </span>
            </template>
        </el-tree>

    </m-box>
</template>

<script>
import { nextTick, ref, toRefs, watch } from 'vue'
//import { reactive, computed, nextTick, toRefs, watch } from 'vue'
import { useMessage } from 'tpm-ui'
export default {
    props: {
        //创建时刷新
        refreshOnCreated: {
            type: Boolean,
            default: true,
        },
    },
    emits: ['change'],
    setup(props, { emit }) {

        const { store } = tpm
        const { getTree } = tpm.api.ps.department
        const api = tpm.api.ps.department
        const { getCompanyget } = tpm.api.ps.company
        const message = useMessage()
        const currentKey = ref(0)
        const treeRef = ref()
        const treeData = ref([])
        const loading = ref(false)

        let waiting = false
        let selection = null

        //刷新
        const refresh = (init) => {
            //获取单位名称
            // /api/ps/Company/Get
            getCompanyget().then((company) => {
                // /api/ps/Department/Tree
                getTree().then((data) => {
                    console.log('getTree:')
                    console.log(data)
                    //根节点
                    const root = {
                        id: 0,
                        label: company.name || '组织机构',
                        item: {
                            id: 0,
                            name: company.name,
                            fullPath: '/',
                        },
                        children: data,
                    }
                    //赋值
                    treeData.value = [root]

                    // var groupName = '组织机构'
                    // treeData.value = [
                    //{
                    //         id: 0,
                    //         label: groupName,
                    //         children: data.map(n => {
                    //             //resolvePage(n)
                    //             return n
                    //         }),
                    //         path: [],
                    //         item: {
                    //             id: 0,
                    //             icon: 'menu',
                    //             type: 0,
                    //             locales: {
                    //                 'zh-cn': groupName,
                    //                 en: groupName,
                    //             },
                    //         },
                    //},
                    // ]

                    console.log('treeData.value:')
                    console.log(treeData.value)

                    if (init) {
                        //初始化触发一次change事件
                        handleTreeChange(root)
                    } else {
                        if (!waiting) {
                            waiting = true
                            //刷新要保留当前点击节点
                            nextTick(() => {
                                if (treeRef != null && treeRef.value != null) {
                                    treeRef.value.setCurrentKey(currentKey.value)
                                }
                            })
                        }
                    }
                })
            })
        }

        //处理更改
        const handleTreeChange = (data) => {
            console.log('handleTreeChange')
            console.log(data)
            if (data != null) {
                currentKey.value = data.id
                //触发change事件
                emit('change', data)
            }
        }

        //允许拖动
        const handleTreeAllowDrag = draggingNode => {
            return draggingNode.data.id > 0
        }

        //允许树放置
        const handleTreeAllowDrop = (draggingNode, dropNode, type) => {
            if (dropNode.data.id === 0) {
                return false
            }
            if (type === 'inner' && dropNode.data.item.type !== 0) {
                return false
            }
            return true
        }

        //允许节点放置
        const handleTreeNodeDrop = (draggingNode, dropNode, type) => {
            if (treeRef == null || treeRef.value == null) {
                return
            }

            let root = treeRef.value.getNode(0)
            if (draggingNode.level === dropNode.level) {
                root = dropNode.parent
            }
            const menus = []

            resolveSort(root, menus)
            console.log(menus)
            api.updateSort(menus).then(() => {
                //listRef.value.refresh()
            })
        }

        //排序
        const resolveSort = (node, menus) => {
            node.childNodes.forEach((n, i) => {
                menus.push({
                    id: n.key,
                    sort: i + 1,
                    parentId: node.key,
                })
                resolveSort(n, menus)
            })
        }

        //============
        //节点展开时
        const onNodeExpand = (data) => {
            //记录展开的节点
            treeRef.value.defaultExpandedKeys.push(data.id)
        }

        //节点折叠时
        const onNodeCollapse = (data) => {
            //移除展开的节点
            $_.pull(treeRef.value.defaultExpandedKeys, data.id)
        }

        //插入
        //data = { id: model.id, label: model.name, item: Object.assign({}, model) }
        const insert = (data) => {
            //设置子节点
            if (!data.children) {
                data.children = []
            }

            let children = selection.children
            //如果不包含子节点，直接push，否则需要根据序号排序
            if (children.length < 1) {
                children.push(data)
                return
            }
            for (let i = 0; i < children.length; i++) {
                if (data.item.sort < children[i].item.sort) {
                    children.splice(i, 0, data)
                    break
                }
                //如果是最后一个，则附加到最后一个节点后面
                if (i === children.length - 1) {
                    children.push(data)
                    break
                }
            }
        }

        //删除
        const remove = (id) => {
            let children = selection.children
            for (let i = 0; i < children.length; i++) {
                let child = children[i]
                if (id === child.id) {
                    children.splice(i, 1)
                    return child
                }
            }
        }

        //更新
        const update = (model) => {
            //先判断是否展开,已展开的先删除
            let expanded = treeRef.value.getNode(model.id).expanded
            if (!expanded) {
                $_.pull(treeRef.value.defaultExpandedKeys, model.id)
            }
            //保存原来的子节点，同时先删除，再添加，这样可以保证排序
            model.children = remove(model.id).children
            insert(model)
            //若是展开状态要再次展开
            if (expanded) {
                treeRef.value.defaultExpandedKeys.push(model.id)
            }
        }

        const setCheckedKeys = (checkedKeys) => {
            nextTick(() => {
                if (showCheckbox) {
                    treeRef.value.setCheckedKeys(checkedKeys)
                }
            })
        }

        //排序，重新刷新
        const sort = () => {
            refresh(true)
        }


        //初始区域数据刷新
        if (props.refreshOnCreated)
            refresh(true)

        //也可以监听刷新
        // watch(props.refreshOnCreated, () => {
        //     refreshTree()
        // })

        return {
            loading,
            treeRef,
            treeData,
            handleTreeChange,
            handleTreeAllowDrop,
            handleTreeAllowDrag,
            handleTreeNodeDrop,
            getTree,
            getCompanyget,
            refresh,
            onNodeExpand,
            onNodeCollapse,
            //页面方法
            insert,
            remove,
            update,
            setCheckedKeys,
            sort,
            //使用v-bind="box"
            box: {
                header: true,
                //可以直接在元素上定义，如 :title="组织架构"
                title: '组织架构',
                icon: 'flow',
                page: true,
            },
            //使用v-bind="tree"
            // tree: {
            //     data: [],
            //     nodeKey: 'id',
            //     highlightCurrent: true,
            //     props: { children: 'children', label: 'label' },
            //     //可以直接在元素上定义,如:current-node-key="currentKey"
            //     //currentNodeKey: 0,
            //     expandOnClickNode: true,
            //     defaultExpandedKeys: [0],
            // },
            //使用v-on="on"
            // on: {
            //     //可以直接在元素上定义,如： @current-change="handleTreeChange"
            //     'current-change': handleTreeChange,
            //     'node-expand': onNodeExpand,
            //     'node-collapse': onNodeCollapse,
            // }
        }
    }
}
</script>
