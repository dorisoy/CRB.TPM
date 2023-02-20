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
            color: colors,
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'cross'
                }
            },
            grid: {
                right: '20%'
            },
            toolbox: {
                feature: {
                    dataView: { show: true, readOnly: false },
                    restore: { show: true },
                    saveAsImage: { show: true }
                }
            },
            legend: {
                //'勇闯', '黑狮', '喜力'
                data: ['勇闯', '黑狮', '喜力']
            },
            xAxis: [
                {
                    type: 'category',
                    axisTick: {
                        alignWithLabel: true
                    },
                    // prettier-ignore
                    data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    name: '勇闯',
                    position: 'right',
                    alignTicks: true,
                    axisLine: {
                        show: true,
                        lineStyle: {
                            color: colors[0]
                        }
                    },
                    axisLabel: {
                        formatter: '{value} KL'
                    }
                },
                {
                    type: 'value',
                    name: '黑狮',
                    position: 'right',
                    alignTicks: true,
                    offset: 80,
                    axisLine: {
                        show: true,
                        lineStyle: {
                            color: colors[1]
                        }
                    },
                    axisLabel: {
                        formatter: '{value} KL'
                    }
                },
                {
                    type: 'value',
                    name: '金额',
                    position: 'left',
                    alignTicks: true,
                    axisLine: {
                        show: true,
                        lineStyle: {
                            color: colors[2]
                        }
                    },
                    axisLabel: {
                        formatter: '{value} ￥'
                    }
                }
            ],
            series: [
                {
                    name: '勇闯',
                    type: 'bar',
                    data: [
                        434, 93, 232, 23.2, 25.6, 76.7, 5241, 162.2, 326, 200, 64, 33
                    ]
                },
                {
                    name: '黑狮',
                    type: 'bar',
                    yAxisIndex: 1,
                    data: [
                        43, 565, 123, 26.4, 287, 70.7, 175.6, 1254, 48.7, 188, 60, 23
                    ]
                },
                {
                    name: '喜力',
                    type: 'line',
                    yAxisIndex: 2,
                    data: [345, 56, 676, 343, 631, 102, 203, 43543, 23.0, 16.5, 120, 62]
                }
            ]
        };

        return {
            option,
        }
    },
}
</script>
