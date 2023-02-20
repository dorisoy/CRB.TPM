<template>
  <div class="m-padding-10">
    <el-tree ref="treeRef" :data="treeData" :check-strictly="true" :current-node-key="currentKey" node-key="id"
      draggable highlight-current default-expand-all :expand-on-click-node="false" :allow-drag="handleTreeAllowDrag"
      @current-change="handleTreeChange">
      <template #default="{ node, data }">
        <span>
          <m-icon :name="data.item.icon || 'folder-o'" class="m-margin-r-5" />
          <span>{{ node.label }} </span>
        </span>
      </template>
    </el-tree>
  </div>
</template>
<script>
import { computed, nextTick, reactive, ref, watch } from 'vue'
export default {
  emits: ['change'],
  setup(props, { emit }) {
    const { store } = tpm

    const currentKey = ref(0)
    const treeData = ref([])
    const treeRef = ref()

    const adminStore = store.state.mod.admin
    const groupCode = computed(() => adminStore.dict.groupCode)
    const dictCode = computed(() => adminStore.dict.dictCode)
    const model = reactive({ groupCode, dictCode })

    let waiting = false

    const refresh = () => {
      tpm.api.admin.dict.tree(model).then(data => {
        treeData.value = data
        if (!waiting) {
          waiting = true
          nextTick(() => {
            if (treeRef != null && treeRef.value != null) {
              treeRef.value.setCurrentKey(currentKey.value)
            }
          })
        }
      })
    }

    watch([groupCode, dictCode], refresh)

    refresh()

    const handleTreeChange = ({ data }) => {
      // console.log('handleTreeChange')
      // console.log(data.id)
      if (data != null) {
        currentKey.value = data.id
        emit('change', data.id)
      }
    }

    const handleTreeAllowDrag = draggingNode => {
      return draggingNode.data.id > 0
    }

    return {
      currentKey,
      treeData,
      treeRef,
      refresh,
      handleTreeChange,
      handleTreeAllowDrag
    }
  },
}
</script>
