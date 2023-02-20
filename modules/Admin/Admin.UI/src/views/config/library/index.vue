<template>
    <el-tabs v-model="editableTabsValue" class="m-admin-config-library page" tab-position="left" type="border-card"
        :style="[{ height: autoheight + 'px' }]">
        <!--系统信息-->
        <el-tab-pane name="system">
            <template #label>
                <span>
                    <m-icon name="microsoft" /> 系统信息
                </span>
            </template>
            <system-config />
        </el-tab-pane>
        <!--前端组件-->
        <el-tab-pane name="component" lazy>
            <template #label>
                <span>
                    <m-icon name="table" /> 前端组件
                </span>
            </template>
            <component-config />
        </el-tab-pane>
        <!--通用路径-->
        <el-tab-pane name="path" lazy>
            <template #label>
                <span>
                    <m-icon name="flow" /> 通用路径
                </span>
            </template>
            <path-config />
        </el-tab-pane>
        <!--认证授权-->
        <el-tab-pane name="auth" lazy>
            <template #label>
                <span>
                    <m-icon name="captcha" /> 认证授权
                </span>
            </template>
            <auth-config />
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

import SystemConfig from './components/system/index.vue'
import ComponentConfig from './components/component/index.vue'
import PathConfig from './components/path/index.vue'
import AuthConfig from './components/auth/index.vue'

export default {
    components: { SystemConfig, ComponentConfig, PathConfig, AuthConfig },
    setup() {

        const model = reactive({

        })

        const autoheight = ref(0)
        const handleHeight = () => {
            autoheight.value = window.innerHeight - 100
            console.log('height', autoheight.value)
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
            editableTabsValue: 'system',
            model,
            autoheight,
            handleHeight,
        }
    }
}
</script>

<style lang="scss">
.m-admin-config-library {
    .el-tabs__header {
        margin-right: 0 !important;
    }

    .el-tabs__item {
        text-align: left !important;

        &.is-active {
            color: #67c23a !important;
        }
    }

    .el-tabs__content {
        .nm-box {
            border-left: none !important;
        }
    }
}
</style>
