<template>
    <section class="m-icon-picker">
        <div class="m-icon-picker_input">
            <el-button-group>
                <m-button icon="fold-b" @click="showPannel = true">{{ placeholder }}</m-button>
                <m-button v-if="showReset" type="danger" icon="close" @click="reset">重置</m-button>
            </el-button-group>
        </div>
        <panel :selection="curSelection" v-model="showPannel" @success="handleSelect" />
    </section>
</template>

<script>
import { computed, inject, ref } from 'vue'
import Panel from './panel.vue'
export default {
    components: { Panel },
    props: {
        modelValue: {
            type: Object,
            default: null,
        },
        placeholder: {
            type: String,
            default: '',
        },
    },
    emits: ['update'],
    setup(props, { emit }) {
        //当前操作选择
        const curSelection = ref({})
        const showReset = ref(false)
        const resetMethods = inject('resetMethods', [])
        const orgval = computed({
            get() {
                return props.modelValue
            },
            set(val) {
                emit('update', val)
            },
        })

        const showPannel = ref(false)

        //选择提交时处理
        const handleSelect = val => {
            console.log('选择', val)
            orgval.value = val
            showReset.value = true
            //emit('update', val)
        }

        const reset = () => {
            orgval.value = ''
            showReset.value = false
        }

        resetMethods.push(reset)

        return {
            orgval,
            showPannel,
            handleSelect,
            showReset,
            reset,
            curSelection
        }
    },
}
</script>
