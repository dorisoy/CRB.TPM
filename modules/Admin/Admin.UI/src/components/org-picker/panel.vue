<template>
    <m-form-dialog :model="model" v-bind="bind" v-on="on" @opened="handleOpened" :loading="loading" title="过滤组织">
        <!-- header -->
        <el-row class="m-roleorg-row">
            <el-col :span="4">
                <m-flex-row>
                    <m-flex-auto>
                        <el-checkbox v-model="checked1" :style="{ color: '#fff' }" @change="checkAll(checked1, 1)">{{
                                checkedLable1
                        }}</el-checkbox>
                    </m-flex-auto>
                    <m-flex-fixed>
                        雪花
                    </m-flex-fixed>
                </m-flex-row>
            </el-col>
            <el-col :span="4">
                <m-flex-row>
                    <m-flex-auto>
                        <el-checkbox ref="checkedRef2" v-model="checked2" :style="{ color: '#fff' }"
                            @change="checkAll(checked2, 2)">{{ checkedLable2
                            }}</el-checkbox>
                    </m-flex-auto>
                    <m-flex-fixed>
                        事业部
                    </m-flex-fixed>
                </m-flex-row>
            </el-col>
            <el-col :span="4">
                <m-flex-row>
                    <m-flex-auto>
                        <el-checkbox ref="checkedRef3" v-model="checked3" :style="{ color: '#fff' }"
                            @change="checkAll(checked3, 3)">{{ checkedLable3
                            }}</el-checkbox>
                    </m-flex-auto>
                    <m-flex-fixed>
                        营销中心
                    </m-flex-fixed>
                </m-flex-row>
            </el-col>
            <el-col :span="4">
                <m-flex-row>
                    <m-flex-auto>
                        <el-checkbox ref="checkedRef3" v-model="checked4" :style="{ color: '#fff' }"
                            @change="checkAll(checked4, 4)">{{ checkedLable4
                            }}</el-checkbox>
                    </m-flex-auto>
                    <m-flex-fixed>
                        大区
                    </m-flex-fixed>
                </m-flex-row>
            </el-col>
            <el-col :span="4">
                <m-flex-row>
                    <m-flex-auto>
                        <el-checkbox ref="checkedRef4" v-model="checked5" :style="{ color: '#fff' }"
                            @change="checkAll(checked5, 5)">{{ checkedLable5
                            }}</el-checkbox>
                    </m-flex-auto>
                    <m-flex-fixed>
                        业务部
                    </m-flex-fixed>
                </m-flex-row>
            </el-col>
            <el-col :span="4">
                <m-flex-row>
                    <m-flex-auto>
                        <el-checkbox ref="checkedRef5" v-model="checked6" :style="{ color: '#fff' }"
                            @change="checkAll(checked6, 6)">{{ checkedLable6
                            }}</el-checkbox>
                    </m-flex-auto>
                    <m-flex-fixed>
                        工作站
                    </m-flex-fixed>
                </m-flex-row>
            </el-col>
        </el-row>

        <!-- tree -->
        <el-row class="m-roleorg-tree">
            <!-- headOffice -->
            <el-col :span="4" class="m-roleorg-border">
                <el-tree v-loading="loading1" ref="treeRef1" :current-node-key="`00000000-0000-0000-0000-000000000000`"
                    :props="nodeProps" :data="headOffice" node-key="id" highlight-current
                    @current-change="headOfficeChange" :expand-on-click-node="false" :check-strictly="true"
                    show-checkbox @check="headOfficeClick" :style="{ height: '300px', overflow: 'auto' }"
                    :default-checked-keys="keys1" :draggable="true" @node-drag-start="handleDragStart"
                    :allow-drop="collapse">
                    <template #default="{ node, data }">
                        <span> {{ data.name }}</span>
                    </template>
                </el-tree>
            </el-col>

            <!-- dbs -->
            <el-col :span="4" class="m-roleorg-border">
                <el-tree v-loading="loading2" ref="treeRef2" :current-node-key="`00000000-0000-0000-0000-000000000000`"
                    :props="nodeProps" :data="dbs" node-key="id" highlight-current @current-change="dbsChange"
                    :expand-on-click-node="false" :check-strictly="true" show-checkbox @check="dbsClick"
                    :style="{ height: '300px', overflow: 'auto' }" :default-checked-keys="keys2" :draggable="true"
                    @node-drag-start="handleDragStart" :allow-drop="collapse">
                    <template #default="{ node, data }">
                        <span> {{ data.name }}</span>
                    </template>
                </el-tree>
            </el-col>

            <!-- marketingCenters -->
            <el-col :span="4" class="m-roleorg-border">
                <el-tree v-loading="loading3" ref="treeRef3" :current-node-key="`00000000-0000-0000-0000-000000000000`"
                    :props="nodeProps" :data="marketingCenters" node-key="id" highlight-current
                    @current-change="marketingCentersChange" :expand-on-click-node="false" :check-strictly="true"
                    show-checkbox @check="marketingCentersClick" :style="{ height: '300px', overflow: 'auto' }"
                    :default-checked-keys="keys3" :draggable="true" @node-drag-start="handleDragStart"
                    :allow-drop="collapse">
                    <template #default="{ node, data }">
                        <span> {{ data.name }}</span>
                    </template>
                </el-tree>
            </el-col>

            <!-- saleRegions -->
            <el-col :span="4" class="m-roleorg-border">
                <el-tree v-loading="loading4" ref="treeRef4" :current-node-key="`00000000-0000-0000-0000-000000000000`"
                    :props="nodeProps" :data="saleRegions" node-key="id" highlight-current
                    @current-change="saleRegionsChange" :expand-on-click-node="false" :check-strictly="true"
                    show-checkbox @check="saleRegionsClick" :style="{ height: '300px', overflow: 'auto' }"
                    :default-checked-keys="keys4" :draggable="true" @node-drag-start="handleDragStart"
                    :allow-drop="collapse">
                    <template #default="{ node, data }">
                        <span> {{ data.name }}</span>
                    </template>
                </el-tree>
            </el-col>

            <!-- departments -->
            <el-col :span="4" class="m-roleorg-border">
                <el-tree v-loading="loading5" ref="treeRef5" :current-node-key="`00000000-0000-0000-0000-000000000000`"
                    :props="nodeProps" :data="departments" node-key="id" highlight-current
                    @current-change="departmentsChange" :expand-on-click-node="false" :check-strictly="true"
                    show-checkbox @check="departmentsClick" :style="{ height: '300px', overflow: 'auto' }"
                    :default-checked-keys="keys5" :draggable="true" @node-drag-start="handleDragStart"
                    :allow-drop="collapse">
                    <template #default="{ node, data }">
                        <span> {{ data.name }}</span>
                    </template>
                </el-tree>
            </el-col>

            <!-- stations -->
            <el-col :span="4" class="m-roleorg-border">
                <el-tree v-loading="loading6" ref="treeRef6" :current-node-key="`00000000-0000-0000-0000-000000000000`"
                    :props="nodeProps" :data="stations" node-key="id" highlight-current @current-change="stationsChange"
                    :expand-on-click-node="false" :check-strictly="true" show-checkbox @check="stationsClick"
                    :style="{ height: '300px', overflow: 'auto' }" :default-checked-keys="keys6" :draggable="true"
                    @node-drag-start="handleDragStart" :allow-drop="collapse">
                    <template #default="{ node, data }">
                        <span> {{ data.name }}</span>
                    </template>
                </el-tree>
            </el-col>
        </el-row>

        <!-- selections -->
        <el-row class="m-roleorg-tree2" :style="{ height: '270px' }">
            <el-col :span="4" class="m-roleorg-border m-roleorg-tag" @drop="drop($event, 1)"
                @dragover="allowDrop($event)">
                <el-tag v-if="headOfficeSelections.length > 0" v-for="tag in headOfficeSelections" :key="tag.id"
                    class="mx-1" closable @close="handleClose(tag, headOfficeSelections, 1)">
                    {{ tag.name }}
                </el-tag>
                <div v-if="headOfficeSelections.length == 0" class="el-tree__empty-block"><span
                        class="el-tree__empty-text">拖拽到这...</span>
                </div>
            </el-col>

            <el-col :span="4" class="m-roleorg-border m-roleorg-tag" @drop="drop($event, 2)"
                @dragover="allowDrop($event)">
                <el-tag v-if="dbsSelections.length > 0" v-for="tag in dbsSelections" :key="tag.id" class="mx-1" closable
                    @close="handleClose(tag, dbsSelections, 2)">
                    {{ tag.name }}
                </el-tag>
                <div v-if="dbsSelections.length == 0" class="el-tree__empty-block"><span
                        class="el-tree__empty-text">拖拽到这...</span>
                </div>
            </el-col>

            <el-col :span="4" class="m-roleorg-border m-roleorg-tag" @drop="drop($event, 3)"
                @dragover="allowDrop($event)">
                <el-tag v-if="marketingCentersSelections.length > 0" v-for="tag in marketingCentersSelections"
                    :key="tag.id" class="mx-1" closable @close="handleClose(tag, marketingCentersSelections, 3)">
                    {{ tag.name }}
                </el-tag>
                <div v-if="marketingCentersSelections.length == 0" class="el-tree__empty-block"><span
                        class="el-tree__empty-text">拖拽到这...</span>
                </div>
            </el-col>

            <el-col :span="4" class="m-roleorg-border m-roleorg-tag" @drop="drop($event, 4)"
                @dragover="allowDrop($event)">
                <el-tag v-if="saleRegionsSelections.length > 0" v-for="tag in saleRegionsSelections" :key="tag.id"
                    class="mx-1" closable @close="handleClose(tag, saleRegionsSelections, 4)">
                    {{ tag.name }}
                </el-tag>
                <div v-if="saleRegionsSelections.length == 0" class="el-tree__empty-block"><span
                        class="el-tree__empty-text">拖拽到这...</span>
                </div>
            </el-col>

            <el-col :span="4" class="m-roleorg-border m-roleorg-tag" @drop="drop($event, 5)"
                @dragover="allowDrop($event)">
                <el-tag v-if="departmentsSelections.length > 0" v-for="tag in departmentsSelections" :key="tag.id"
                    class="mx-1" closable @close="handleClose(tag, departmentsSelections, 5)">
                    {{ tag.name }}
                </el-tag>
                <div v-if="departmentsSelections.length == 0" class="el-tree__empty-block"><span
                        class="el-tree__empty-text">拖拽到这...</span>
                </div>
            </el-col>

            <el-col :span="4" class="m-roleorg-border m-roleorg-tag" @drop="drop($event, 6)"
                @dragover="allowDrop($event)">
                <el-tag v-if="stationsSelections.length > 0" v-for="tag in stationsSelections" :key="tag.id"
                    class="mx-1" closable type="primary" @close="handleClose(tag, stationsSelections, 6)">
                    {{ tag.name }}
                </el-tag>
                <div v-if="stationsSelections.length == 0" class="el-tree__empty-block"><span
                        class="el-tree__empty-text">拖拽到这...</span></div>
            </el-col>
        </el-row>
    </m-form-dialog>
</template>

<script>
import { computed, reactive, ref, toRefs, nextTick } from 'vue'
import { useMessage } from 'tpm-ui'
import draggable from 'vuedraggable'
export default {
    props: {
        nodeProps: {
            children: "children",
            label: "label",
            isLeaf: "isLeaf"
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
    emits: ['success'],
    components: { draggable },
    setup(props, { emit }) {
        const { store, $t } = tpm
        const message = useMessage()
        const { getNodeTree, getOrgLevel, getNodeByParentId } = tpm.api.admin.role
        const dragTemp = ref({})
        const loading = ref(false)
        const bind = reactive({
            title: "",
            icon: "",
            action: null,
            disabled: false,
            footer: true,
            destroyOnClose: true,
            loading: false
        });

        //checked
        const checked1 = ref(false)
        const checked2 = ref(false)
        const checked3 = ref(false)
        const checked4 = ref(false)
        const checked5 = ref(false)
        const checked6 = ref(false)

        //checkedLable
        const checkedLable1 = ref('全选')
        const checkedLable2 = ref('全选')
        const checkedLable3 = ref('全选')
        const checkedLable4 = ref('全选')
        const checkedLable5 = ref('全选')
        const checkedLable6 = ref('全选')

        //treeRef
        const treeRef1 = ref()
        const treeRef2 = ref()
        const treeRef3 = ref()
        const treeRef4 = ref()
        const treeRef5 = ref()
        const treeRef6 = ref()

        //loading
        const loading1 = ref(false)
        const loading2 = ref(false)
        const loading3 = ref(false)
        const loading4 = ref(false)
        const loading5 = ref(false)
        const loading6 = ref(false)

        //model
        const model = reactive({
            //雪花
            headOffice: [{}],
            headOfficeSelections: [],
            //事业部
            dbs: [{}],
            dbsSelections: [],
            //营销中心
            marketingCenters: [{}],
            marketingCentersSelections: [],
            //大区
            saleRegions: [{}],
            saleRegionsSelections: [],
            //业务部
            departments: [{}],
            departmentsSelections: [],
            //工作站
            stations: [{}],
            stationsSelections: [],
        })

        //keys
        const keys1 = computed(() => {
            return model.headOfficeSelections.filter((s) => s.id);
        })
        const keys2 = computed(() => {
            return model.dbsSelections.filter((s) => s.id);
        })
        const keys3 = computed(() => {
            return model.marketingCentersSelections.filter((s) => s.id);
        })
        const keys4 = computed(() => {
            return model.saleRegionsSelections.filter((s) => s.id);
        })
        const keys5 = computed(() => {
            return model.departmentsSelections.filter((s) => s.id);
        })
        const keys6 = computed(() => {
            return model.stationsSelections.filter((s) => s.id);
        })

        //窗体声明
        const nameRef = ref(null)

        bind.closeOnSuccess = true
        bind.destroyOnClose = true
        bind.autoFocusRef = nameRef
        bind.width = '1400px'
        bind.height = '760px'
        bind.title = "过滤组织"
        bind.btnOkText = "确认选择"
        bind.icon = 'search'
        bind.successMessage = '数据已经选择'
        //提交前处理
        bind.beforeSubmit = () => {
            //赋值 selection ，回传父组件
            props.selection.headOffice = keys1.value.map(b => b.id)
            props.selection.dbs = keys2.value.map(b => b.id)
            props.selection.marketingCenters = keys3.value.map(b => b.id)
            props.selection.saleRegions = keys4.value.map(b => b.id)
            props.selection.departments = keys5.value.map(b => b.id)
            props.selection.stations = keys6.value.map(b => b.id)
            //console.log('beforeSubmit', props.selection)
        }

        //保存提交数据
        bind.action = () => {
            let selection = props.selection
            console.log('action -> selection', selection)
            return new Promise(resolve => {
                emit('success', selection)
                resolve(selection)
            })
        }

        //用于标记是否已经选择
        const tagSelected = (db, selections, ref) => {
            //遍历
            db.forEach(item => {
                nextTick(() => {
                    //标记选中
                    let exist = selections.find((m) => m.id === item.id)
                    //console.log('exist', exist)
                    if (exist != null) {
                        ref.value.setChecked(item.id, true)
                    }
                })
            })
        }

        //选择行时处理更改
        //级联 to -> dbs
        const headOfficeChange = async (data, node) => {
            loading2.value = true
            if (data != null) {
                const res = await getNodeByParentId({
                    level: 20,
                    ignore: node != null,
                    parentId: data == null ? '' : (data.key == null ? data.id : data.key)
                })
                model.dbs = res;
                //标记是否已经选择
                tagSelected(model.dbs, model.dbsSelections, treeRef2)
            }
            loading2.value = false

            //next
            const nextNodefirst = model.dbs[0]
            if (nextNodefirst != null) {
                //console.log('next - > dbsChange')
                //console.log(nextNodefirst)
                await dbsChange(nextNodefirst)
            } else {
                dbsChange([])
            }
        }
        //级联 to -> marketingCenters
        const dbsChange = async (data, node) => {
            loading3.value = true
            if (data != null) {
                const res = await getNodeByParentId({
                    level: 30,
                    ignore: node != null,
                    parentId: data == null ? '' : (data.key == null ? data.id : data.key)
                })
                model.marketingCenters = res;
                //标记是否已经选择
                tagSelected(model.marketingCenters, model.marketingCentersSelections, treeRef3)
            }
            loading3.value = false

            //next 4
            const nextNodefirst = model.marketingCenters[0]
            if (nextNodefirst != null) {
                //console.log('next - > marketingCentersChange')
                marketingCentersChange(nextNodefirst)
            }
            else {
                marketingCentersChange([])
            }
        }

        //级联 to -> saleRegions
        const marketingCentersChange = async (data, node) => {
            loading4.value = true
            if (data != null) {
                const res = await getNodeByParentId({
                    level: 40,
                    ignore: node != null,
                    parentId: data == null ? '' : (data.key == null ? data.id : data.key)
                })
                model.saleRegions = res;
                //标记是否已经选择
                tagSelected(model.saleRegions, model.saleRegionsSelections, treeRef4)
            }
            loading4.value = false

            //next 5
            const nextNodefirst = model.saleRegions[0]
            if (nextNodefirst != null) {
                //console.log('next - > saleRegionsChange')
                saleRegionsChange(nextNodefirst)
            } else {
                saleRegionsChange([])
            }
        }
        //级联 to -> departments
        const saleRegionsChange = async (data, node) => {
            loading5.value = true
            if (data != null) {
                const res = await getNodeByParentId({
                    level: 50,
                    ignore: node != null,
                    parentId: data == null ? '' : (data.key == null ? data.id : data.key)
                })
                model.departments = res;
                //标记是否已经选择
                tagSelected(model.departments, model.departmentsSelections, treeRef5)
            }
            loading5.value = false

            //next 6
            const nextNodefirst = model.departments[0]
            if (nextNodefirst != null) {
                //console.log('next - > departmentsChange')
                departmentsChange(nextNodefirst)
            } else {
                departmentsChange([])
            }
        }
        //级联 to -> stations
        const departmentsChange = async (data, node) => {
            loading6.value = true
            if (data != null) {
                const res = await getNodeByParentId({
                    level: 60,
                    ignore: node != null,
                    parentId: data == null ? '' : (data.key == null ? data.id : data.key)
                })
                model.stations = res;
                //标记是否已经选择
                tagSelected(model.stations, model.stationsSelections, treeRef6)
            }
            loading6.value = false
        }
        //级联 end <- stationsChange
        const stationsChange = (data) => {
            //console.log(data)
        }

        //用于条目添加移除
        const pushOrpop = (cols, db, data, checked, fun) => {
            dragTemp.value = {}
            if (checked.checkedKeys.length > 0) {
                //清空
                //cols = []
                checked.checkedKeys.forEach(key => {
                    //console.log('key:', key)
                    let curt = db.find((m) => m.id === key)
                    if (curt !== null) {
                        //console.log('push:', curt)
                        const exist = cols.find((m) => m.id === key)
                        if (!exist)
                            cols.push(curt)
                    }
                })
                //从未添加时，追加
                if (cols.length == 0) {
                    const exist = cols.find((m) => m.id === data.id)
                    if (!exist)
                        cols.push(data)
                }
            } else {
                cols = cols.filter((item) => { return item.id !== data.id })
            }
            //回调
            fun(cols)
        }

        //tree checkbox 点击的时候触发
        const headOfficeClick = (data, checked) => {
            let cols = model.headOfficeSelections
            pushOrpop(cols, model.headOffice, data, checked, (ref) => {
                model.headOfficeSelections = ref
            })
        }

        //tree checkbox 点击的时候触发
        const dbsClick = (data, checked) => {
            let cols = model.dbsSelections
            pushOrpop(cols, model.dbs, data, checked, (ref) => {
                model.dbsSelections = ref
            })
        }

        //tree checkbox 点击的时候触发
        const marketingCentersClick = (data, checked) => {
            let cols = model.marketingCentersSelections
            pushOrpop(cols, model.marketingCenters, data, checked, (ref) => {
                model.marketingCentersSelections = ref
            })
        }

        //tree checkbox 点击的时候触发
        const saleRegionsClick = (data, checked) => {
            let cols = model.saleRegionsSelections
            pushOrpop(cols, model.saleRegions, data, checked, (ref) => {
                model.saleRegionsSelections = ref
            })
        }

        //tree checkbox 点击的时候触发
        const departmentsClick = (data, checked) => {
            let cols = model.departmentsSelections
            pushOrpop(cols, model.departments, data, checked, (ref) => {
                model.departmentsSelections = ref
            })
        }

        //tree checkbox 点击的时候触发
        const stationsClick = (data, checked) => {
            let cols = model.stationsSelections
            pushOrpop(cols, model.stations, data, checked, (ref) => {
                model.stationsSelections = ref
            })
        }

        //全选/取消
        const checkAll = (value, index) => {
            //console.log('checkAll', value)
            //console.log('index', index)

            let c = '取消'
            let a = '全选'
            let treeRef = treeRef1
            let selections = []
            switch (index) {
                case 1:
                    treeRef = treeRef1
                    selections = model.headOffice
                    checkedLable1.value = value ? c : a
                    break;
                case 2:
                    treeRef = treeRef2
                    selections = model.dbs
                    checkedLable2.value = value ? c : a
                    break;
                case 3:
                    treeRef = treeRef3
                    selections = model.marketingCenters
                    checkedLable3.value = value ? c : a
                    break;
                case 4:
                    treeRef = treeRef4
                    selections = model.saleRegions
                    checkedLable4.value = value ? c : a
                    break;
                case 5:
                    treeRef = treeRef5
                    selections = model.departments
                    checkedLable5.value = value ? c : a
                    break;
                case 6:
                    treeRef = treeRef6
                    selections = model.stations
                    checkedLable6.value = value ? c : a
                    break;
            }
            //全选/取消tree
            if (selections.length > 0) {
                selections.forEach(item => {
                    console.log('treeRef.value', treeRef.value)
                    treeRef.value.setChecked(item.id, value);
                    switch (index) {
                        case 1:
                            {
                                let cols = model.headOfficeSelections
                                cols.splice(cols.indexOf(item), 1)
                            }
                            break;
                        case 2:
                            {
                                let cols = model.dbsSelections
                                cols.splice(cols.indexOf(item), 1)
                            }
                            break;
                        case 3:
                            {
                                let cols = model.marketingCentersSelections
                                cols.splice(cols.indexOf(item), 1)
                            }
                            break;
                        case 4:
                            {
                                let cols = model.saleRegionsSelections
                                cols.splice(cols.indexOf(item), 1)
                            }
                            break;
                        case 5:
                            {
                                let cols = model.departmentsSelections
                                cols.splice(cols.indexOf(item), 1)
                            }
                            break;
                        case 6:
                            {
                                let cols = model.stationsSelections
                                cols.splice(cols.indexOf(item), 1)
                            }
                            break;
                    }
                })
            }
            //追加到Selections
            let getNodes = treeRef.value.getCheckedNodes()
            if (getNodes.length > 0) {
                getNodes.forEach(item => {
                    switch (index) {
                        case 1:
                            {
                                let cols = model.headOfficeSelections
                                cols.push(item)
                            }
                            break;
                        case 2:
                            {
                                let cols = model.dbsSelections
                                cols.push(item)
                            }
                            break;
                        case 3:
                            {
                                let cols = model.marketingCentersSelections
                                cols.push(item)
                            }
                            break;
                        case 4:
                            {
                                let cols = model.saleRegionsSelections
                                cols.push(item)
                            }
                            break;
                        case 5:
                            {
                                let cols = model.departmentsSelections
                                cols.push(item)
                            }
                            break;
                        case 6:
                            {
                                let cols = model.stationsSelections
                                cols.push(item)
                            }
                            break;
                    }
                })
            }
        }

        //移除选择项目
        const handleClose = (tag, cols, index) => {
            //console.log('tag', tag)
            //console.log('cols', cols)
            //console.log('index', index)
            //移除选择
            cols.splice(cols.indexOf(tag), 1)

            //取消tree选择
            let treeRef = treeRef1
            switch (index) {
                case 1:
                    treeRef = treeRef1
                    break;
                case 2:
                    treeRef = treeRef2
                    break;
                case 3:
                    treeRef = treeRef3
                    break;
                case 4:
                    treeRef = treeRef4
                    break;
                case 5:
                    treeRef = treeRef5
                    break;
                case 6:
                    treeRef = treeRef6
                    break;
            }
            //获取tree的树的当前选择节点
            let getNode = treeRef.value.getCheckedNodes()
            //console.log('getNode', getNode)
            if (getNode.length > 0) {
                // 循环 getNode 判断如果当前选中 checkbox 中的id 与 getNode 数组中有相同值的 id 的一项
                getNode.forEach(item => {
                    //console.log('tag.id - item.id:', tag.id, item.id)
                    if (tag.id == item.id) {
                        //取消选择
                        treeRef.value.setChecked(item.id, false);
                    }
                })
            }
        }

        //节点开始拖拽时触发的事件
        const handleDragStart = (node) => {
            console.log('handleDragStart -> node', node, node.data.type)
            dragTemp.value = node.data
        }
        // 允许放下拖拽
        const allowDrop = (ev) => {
            ev.preventDefault()
        }
        // 放下事件
        const drop = (ev, ditem) => {
            console.log("ditem", ditem)
            console.log('ev', ev)

            ev.preventDefault()
            let treeNode = ev.target;

            console.log('treeNode', treeNode)

            let item = dragTemp.value
            if (item.type !== null) {
                switch (item.type) {
                    case 10:
                        {
                            let cols = model.headOfficeSelections
                            const exist = cols.find((m) => m.id === item.id)
                            if (exist == null) {
                                cols.push(item)
                                //标记是否已经选择
                                tagSelected(model.headOffice, cols, treeRef1)
                            }
                        }
                        break;
                    case 20:
                        {
                            let cols = model.dbsSelections
                            const exist = cols.find((m) => m.id === item.id)
                            if (exist == null) {
                                cols.push(item)
                                //标记是否已经选择
                                tagSelected(model.dbs, cols, treeRef2)
                            }
                        }
                        break;
                    case 30:
                        {
                            let cols = model.marketingCentersSelections
                            const exist = cols.find((m) => m.id === item.id)
                            if (exist == null) {
                                cols.push(item)
                                //标记是否已经选择
                                tagSelected(model.marketingCenters, cols, treeRef3)
                            }
                        }
                        break;
                    case 40:
                        {
                            let cols = model.saleRegionsSelections
                            const exist = cols.find((m) => m.id === item.id)
                            if (exist == null) {
                                cols.push(item)
                                //标记是否已经选择
                                tagSelected(model.saleRegions, cols, treeRef4)
                            }
                        }
                        break;
                    case 50:
                        {
                            let cols = model.departmentsSelections
                            const exist = cols.find((m) => m.id === item.id)
                            if (exist == null) {
                                cols.push(item)
                                //标记是否已经选择
                                tagSelected(model.departments, cols, treeRef5)
                            }
                        }
                        break;
                    case 60:
                        {
                            let cols = model.stationsSelections
                            const exist = cols.find((m) => m.id === item.id)
                            if (exist == null) {
                                cols.push(item)
                                //标记是否已经选择
                                tagSelected(model.stations, cols, treeRef6)
                            }
                        }
                        break;
                }
            }
        }

        //拖拽时判定目标节点能否成为拖动目标位置
        const collapse = (moveNode, inNode, type) => {
            //不允许放置
            return false;
        }

        //初始默认加载总部节点
        const handleOpened = async () => {
            //初始化根节点
            loading1.value = true
            const res = await getNodeTree(1)
            model.headOffice = res;
            loading1.value = false

            console.log('model.headOffice', model.headOffice)
            console.log('store.state.app.profile.accountRoleOrgs.length > 0', store.state.app.profile.accountRoleOrgs.length > 0)

            //初始化级联
            const nextNodefirst = model.headOffice[0]
            if (nextNodefirst != null) {
                await headOfficeChange(nextNodefirst, null)
            } else {
                headOfficeChange([], null)
            }
        }

        const handleOpen = () => { }
        const handleReset = () => { }
        const handleSuccess = () => { }
        const handleError = () => { }

        //return
        return {
            ...toRefs(model),
            model,
            bind,
            on: { open: handleOpen, reset: handleReset, success: handleSuccess, error: handleError },
            nameRef,
            handleOpen,
            handleOpened,
            handleSuccess,
            handleError,
            checkAll,
            pushOrpop,
            tagSelected,
            dragTemp,
            collapse,
            //==
            handleDragStart,
            allowDrop,
            drop,
            //==
            keys1,
            keys2,
            keys3,
            keys4,
            keys5,
            keys6,
            //==
            loading,
            loading1,
            loading2,
            loading3,
            loading4,
            loading5,
            loading6,
            //==
            treeRef1,
            treeRef2,
            treeRef3,
            treeRef4,
            treeRef5,
            treeRef6,
            //==
            headOfficeChange,
            dbsChange,
            marketingCentersChange,
            saleRegionsChange,
            departmentsChange,
            stationsChange,
            //==
            headOfficeClick,
            dbsClick,
            marketingCentersClick,
            saleRegionsClick,
            departmentsClick,
            stationsClick,
            //==
            checked1,
            checked2,
            checked3,
            checked4,
            checked5,
            checked6,
            //==
            checkedLable1,
            checkedLable2,
            checkedLable3,
            checkedLable4,
            checkedLable5,
            checkedLable6,
            //=
            handleClose
        }
    },
}
</script>

<style scoped>
.m-roleorg-tree {
    padding-right: 0px;
}

.m-flex-auto {
    padding-left: 25px;
}

.m-flex-fixed {
    line-height: 30px;
    padding-right: 5px;
}

.m-roleorg-row {
    padding-right: 0px;
    background: #2965dd;
    color: #fff;
}

.m-roleorg-tree2 {
    padding-right: 0px;
    margin-top: 10px;
}

.m-roleorg-tree2>.m-roleorg-tag {
    padding: 10px;
    height: 270px;
    overflow: auto;
}

.m-roleorg-tree2>.m-roleorg-tag>.mx-1 {
    margin: 5px;
}

.m-roleorg-row>.el-col {
    /* text-align: -webkit-center; */
    font-weight: normal;
    /* margin-bottom: 10px; */
}

/* .m-roleorg-tree>.el-col {
  text-align: -webkit-center;
} */


.m-roleorg-border {
    border-top: 1px #ddd solid;
    border-bottom: 1px #ddd solid;
    border-right: 1px #ddd solid;
}

.m-roleorg-tree>.el-col:first-child,
.m-roleorg-tree2>.el-col:first-child {
    border-left: 1px #ddd solid;
}

.m-roleorg-tree>.el-col:last-child,
.m-roleorg-tree2>.el-col:last-child {
    border-right: 1px #ddd solid;
}
</style>