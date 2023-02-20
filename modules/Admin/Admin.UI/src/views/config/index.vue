<template>
    <m-container>
        <m-tabs>
            <el-tabs v-model="tab">
                <el-tab-pane name="module" lazy>
                    <template #label>
                        <span>
                            <m-icon name="app"></m-icon>
                            <span>业务模块</span>
                        </span>
                    </template>
                    <module-pane ref="module" />
                </el-tab-pane>
                <el-tab-pane name="library" lazy>
                    <template #label>
                        <span>
                            <m-icon name="drawer"></m-icon>
                            <span>基础类库</span>
                        </span>
                    </template>
                    <library-pane ref="library" :descriptors="libraries" />
                </el-tab-pane>
            </el-tabs>
        </m-tabs>
    </m-container>
</template>

<script>
import { computed, reactive, toRefs } from 'vue'
import ModulePane from './module/index.vue'
import LibraryPane from './library/index.vue'
// import { watch } from 'vue'
// import page from './page.json'
export default {
    components: { ModulePane, LibraryPane },
    setup() {
        const api = tpm.api.admin.config
        const model = reactive({
            tab: 'module',
            descriptors: []
        })

        //获取类库描述
        const libraries = computed(() => {
            var libs = model.descriptors.filter(m => m.type === 0)
            console.log('libs', libs)
            return libs
        })

        //获取描述
        const getDescriptors = () => {
            api.getDescriptors().then(data => {
                console.log('getDescriptors -> data', data)
                model.descriptors = data
            })
        }

        //初始获取描述
        getDescriptors()

        return {
            ...toRefs(model),
            model,
            libraries,
            getDescriptors
        }
    }
}
</script>
<style lang="scss">
.m-tabs .el-tabs__content {
    height: 100%;
}
</style>
