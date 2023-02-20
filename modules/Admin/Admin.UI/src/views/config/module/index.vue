<template>
    <el-tabs v-model="editableTabsValue" class="m-admin-config-module page" tab-position="left" type="border-card"
        :style="[{ height: autoheight + 'px' }]">
        <el-tab-pane v-for="item in modules" :key="item.name" :name="item.name" lazy>
            <template #label>
                <span class="textalign">
                    <m-icon :name="item.icon"></m-icon> {{ getNo(item) }}_{{ item.label }}
                </span>
            </template>
            <component :is="item.name"></component>
        </el-tab-pane>
    </el-tabs>
</template>

<script>
import {
    reactive,
    toRefs,
    nextTick,
    onActivated,
    onDeactivated,
    onBeforeMount,
    onBeforeUnmount,
    onMounted,
    ref
} from 'vue'

export default {
    components: {},
    setup() {
        const { modules } = tpm
        const colors = ['#409EFF', '#67C23A', '#E6A23C', '#F56C6C', '#6d3cf7', '#0079de']
        const model = reactive({
            tab: '',
            modules: []
        })

        modules.forEach(m => {
            console.log('modules -> m', m)
            //m-admin-role-select
            console.log('modules -> name', `m-admin-config-${m.code.toLowerCase()}`)
            m.color = colors[parseInt(Math.random() * colors.length)]
            let mt = m.components.find((s) => s.name == `config-${m.code.toLowerCase()}`)
            console.log('_commponent', mt)
            m.component = mt
            m.name = `m-${m.code.toLowerCase()}-config-${m.code.toLowerCase()}`
        })

        const getNo = (item) => {
            return item.id < 10 ? '0' + item.id : '' + item.id
        }


        const autoheight = ref(0)
        const handleHeight = () => {
            autoheight.value = window.innerHeight - 100
            //console.log('height', autoheight.value)
        }
        onActivated(() => {
            nextTick(() => {
                handleHeight()
            })
            window.addEventListener('resize', handleHeight)
        })
        onDeactivated(() => {
            window.removeEventListener('resize', handleHeight)
        })
        onBeforeMount(() => {
            window.addEventListener('resize', handleHeight)
        })
        onMounted(() => {
            nextTick(() => {
                handleHeight()
            })
        })
        onBeforeUnmount(() => {
            window.removeEventListener('resize', handleHeight)
        })

        return {
            ...toRefs(model),
            editableTabsValue: 'm-admin-config-admin',
            model,
            modules,
            getNo,
            autoheight,
            handleHeight,
        }
    },
}
</script>
<style lang="scss">
.m-tabs .el-tabs__content {
    height: 100%;
}

.textalign {
    text-align: left;
}
</style>
