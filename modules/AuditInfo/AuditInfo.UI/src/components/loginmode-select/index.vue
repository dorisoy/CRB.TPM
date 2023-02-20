<template>
  <m-select :action="query" :clearable="clearable"></m-select>
</template>
<script>
export default {
  props: {
    clearable: {
      type: Boolean,
      default: true,
    },
  },
  setup() {
    const { queryLoginModeSelect } = tpm.api.admin.common
    const query = () => {
      return queryLoginModeSelect()
    }
    return {
      query,
    }
  },
}
</script>
