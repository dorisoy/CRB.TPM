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
    const { queryAccountStatusSelect } = tpm.api.admin.common
    const query = () => {
      return queryAccountStatusSelect()
    }
    return {
      query,
    }
  },
}
</script>
