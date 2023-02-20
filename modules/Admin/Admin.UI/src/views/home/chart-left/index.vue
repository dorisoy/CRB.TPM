<template>
    <m-box title="报表使用率" icon="chart-pie-fill">
        <v-chart :option="option" class="m-fill-h" />
    </m-box>
</template>
<script>
import { getCurrentInstance } from 'vue'
export default {
    setup() {
        const { $echarts } = getCurrentInstance().proxy
        const colors = ['#5470C6', '#91CC75', '#EE6666'];

        const option = {
            title: {
                text: '统计'
            },
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'shadow'
                }
            },
            legend: {},
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            xAxis: {
                type: 'value',
                boundaryGap: [0, 0.01]
            },
            //'勇闯', '黑狮', '喜力' '北京', '天津', '河北', '山西', '辽宁', '吉林', '江苏', '浙江',
            yAxis: {
                type: 'category',
                data: ['北京', '天津', '河北', '山西', '辽宁', '吉林']
            },
            series: [
                {
                    name: '2011',
                    type: 'bar',
                    data: [18203, 23489, 29034, 104970, 131744, 630230]
                },
                {
                    name: '2012',
                    type: 'bar',
                    data: [19325, 23438, 31000, 121594, 134141, 681807]
                }
            ]
        };


        return {
            option,
        }
    },
}
</script>
