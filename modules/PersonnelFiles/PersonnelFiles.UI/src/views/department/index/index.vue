<template>
  <m-container>
    <m-split v-model="model.split">
      <!-- 组织树 -->
      <template #fixed>
        <tree-page ref="tree" @change="onTreeChange" />
      </template>
      <!-- 列表 -->
      <template #auto>
        <list ref="listRef" :parent="current" @onpSaveSuccess="onSaveSuccess" @onpRemoveSuccess="onRemoveSuccess" />
      </template>
    </m-split>
  </m-container>
</template>

<script>
import { reactive, ref } from 'vue'
import TreePage from '../tree/index.vue'
import List from '../list/index.vue'

export default {
  components: { TreePage, List },
  setup() {
    const tree = ref()
    const listRef = ref()

    //表示当前选择的节点
    const current = ref({ id: 0 })
    const model = reactive({
      split: 0.3
    })

    //当tree选择更改时，赋值 current 为当前数据
    const onTreeChange = (data) => {
      // console.log('onTreeChange')
      // console.log(data)
      // console.log(data.item.fullPath)
      // console.log('当前选择的节点为：')
      current.value = data
      current.value.path = data.item.fullPath
      console.log(current.value)
      //刷新list
      refresh()
    }

    //当tree页面选择节点或者更新节点时刷新list
    const refresh = () => {
      listRef.value.refresh()
      //console.log('刷新list...')
    }

    //当list页面save保存成功时刷新list
    const onSaveSuccess = (model, data, isAdd) => {
      // console.log('当list页面save保存成功时刷新list')
      // console.log(model)
      // console.log(data)
      // console.log(isAdd)

      //const nodeData = { id: model.id, label: model.name, item: Object.assign({}, model) }
      // if (isAdd == 'add') {
      //   tree.value.insert(nodeData)
      // }
      // else {
      //   tree.value.update(nodeData)
      // }
      //refresh()

      //通知树刷新
      tree.value.refresh(true)
    }

    //当list页面save删除成功时刷新list
    const onRemoveSuccess = () => {

      //console.log('当list页面save删除成功时刷新list')
      //tree.value.remove(id)

      //刷新list
      refresh()
      //通知树刷新
      tree.value.refresh(true)
    }

    return {
      model,
      current,
      onTreeChange,
      onSaveSuccess,
      onRemoveSuccess,
      tree,
      listRef
    }
  }
}
</script>
