<template>
    <m-container scrollbar>

        <el-row :gutter="15">
            <el-col :span="6">
                <m-box-small value="350" label="xxxx" unit="笔" color="#409eff" icon="app" more @more="handleMore">
                </m-box-small>
            </el-col>
            <el-col :span="6">
                <m-box-small value="53000" label="xxxx" unit="万元" color="#409eff" icon="money" more> </m-box-small>
            </el-col>
            <el-col :span="6">
                <m-box-small value="60" label="xxxx" unit="%" color="#409eff" icon="rocket" more> </m-box-small>
            </el-col>
            <el-col :span="6">
                <m-box-small value="3021" label="xxxx" unit="万元" color="#409eff" icon="form" more> </m-box-small>
            </el-col>
        </el-row>

        <el-row style="height: 400px" :gutter="15">
            <el-col :span="12">
                <chart-1 />
            </el-col>
            <el-col :span="12">
                <chart-2 />
            </el-col>
        </el-row>

        <el-row style="height: 400px" :gutter="15" :style="[{ marginTop: '15px' }]">
            <el-col :span="12">
                <chart-3 />
            </el-col>
            <el-col :span="12">
                <chart-4 />
            </el-col>
        </el-row>

    </m-container>
</template>
<script>
import { useMessage } from 'tpm-ui'
import Chart1 from '../chart-left/index.vue'
import Chart2 from '../chart-right/index.vue'
import Chart3 from '../chart-left-bottom/index.vue'
import Chart4 from '../chart-right-bottom/index.vue'
export default {
    components: { Chart1, Chart2, Chart3, Chart4 },
    setup() {
        const message = useMessage()
        const handleMore = () => {
            message.success('点击了更多')
        }
        return {
            handleMore,
        }
    }
}
</script>
