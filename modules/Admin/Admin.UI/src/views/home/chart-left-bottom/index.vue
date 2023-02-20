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
            dataset: [
                {
                    //'勇闯', '黑狮', '喜力'
                    source: [
                        ['Product', 'Sales', 'Price', 'Year'],
                        ['旦角330瓶', 123, 32, 2011],
                        ['马尔斯绿大听黑狮', 231, 14, 2011],
                        ['纯生8度500ml听手提扣', 235, 5, 2011],
                        ['酷爽9度500ml瓶', 341, 25, 2011],
                        ['勇闯天涯500纸箱陕西专供', 122, 29, 2011],
                        ['8°勇闯330听24斗地主版', 143, 30, 2012],
                        ['high-bar菠萝塑包', 201, 19, 2012],
                        ['勇闯天涯酒店专供', 255, 7, 2012],
                        ['勇闯天涯瓶装1*6纸箱有奖版', 241, 27, 2012],
                        ['原汁麦9度500ml听6*4塑膜六连包', 102, 34, 2012],
                        ['超级勇闯500瓶', 153, 28, 2013],
                        ['超级勇闯大听', 181, 21, 2013],
                        ['纯生330m卡纸听', 395, 4, 2013],
                        ['纯生500听', 281, 31, 2013],
                        ['清爽330小听', 92, 39, 2013],
                        ['清爽大听', 223, 29, 2014],
                        ['勇闯天涯卡纸听', 211, 17, 2014],
                        ['勇闯天涯500ml听1', 345, 3, 2014],
                        ['冰8半托听', 211, 35, 2014],
                        ['high-bar橙味塑包', 72, 24, 2014]
                    ]
                },
                {
                    transform: {
                        type: 'filter',
                        config: { dimension: 'Year', value: 2011 }
                    }
                },
                {
                    transform: {
                        type: 'filter',
                        config: { dimension: 'Year', value: 2012 }
                    }
                },
                {
                    transform: {
                        type: 'filter',
                        config: { dimension: 'Year', value: 2013 }
                    }
                }
            ],
            series: [
                {
                    type: 'pie',
                    radius: 50,
                    center: ['50%', '25%'],
                    datasetIndex: 1
                },
                {
                    type: 'pie',
                    radius: 50,
                    center: ['50%', '50%'],
                    datasetIndex: 2
                },
                {
                    type: 'pie',
                    radius: 50,
                    center: ['50%', '75%'],
                    datasetIndex: 3
                }
            ],
            // Optional. Only for responsive layout:
            media: [
                {
                    query: { minAspectRatio: 1 },
                    option: {
                        series: [
                            { center: ['25%', '50%'] },
                            { center: ['50%', '50%'] },
                            { center: ['75%', '50%'] }
                        ]
                    }
                },
                {
                    option: {
                        series: [
                            { center: ['50%', '25%'] },
                            { center: ['50%', '50%'] },
                            { center: ['50%', '75%'] }
                        ]
                    }
                }
            ]
        };
        return {
            option,
        }
    },
}
</script>
