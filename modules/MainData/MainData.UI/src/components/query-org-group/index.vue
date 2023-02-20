<template>
  <el-form-item v-if="selectStart <= 1 && selectEnd >= 1" :label="$t('mod.maindata.org_level_name.head_office')">
    <m-maindata-select-page v-model="form.headofficeid" :multiple="multiple" clearable :action="(pages, keyword) => querySelectFilter(10, pages, keyword)" @change="handleHeadOfficeChange" />
  </el-form-item>
  <el-form-item v-if="selectStart <= 2 && selectEnd >= 2" :label="$t('mod.maindata.org_level_name.division')">
    <m-maindata-select-page ref="divisionRef" v-model="value_divisionIds" :multiple="multiple" clearable :action="(pages, keyword) => querySelectFilter(20, pages, keyword)" @change="handleDivisionChange" />
  </el-form-item>
  <el-form-item v-if="selectStart <= 3 && selectEnd >= 3" :label="$t('mod.maindata.org_level_name.marketing_center')">
    <m-maindata-select-page ref="marketingcenterRef" v-model="value_marketingIds" :multiple="multiple" clearable :action="(pages, keyword) => querySelectFilter(30, pages, keyword)" @change="handleMarketingChange" />
  </el-form-item>
  <el-form-item v-if="selectStart <= 4 && selectEnd >= 4" :label="$t('mod.maindata.org_level_name.dutyregion')">
    <m-maindata-select-page ref="dutyregionRef" v-model="value_dutyregionIds" :multiple="multiple" clearable :action="(pages, keyword) => querySelectFilter(40, pages, keyword)" @change="handleDutyregionChange" />
  </el-form-item>
  <el-form-item v-if="selectStart <= 5 && selectEnd >= 5" :label="$t('mod.maindata.org_level_name.department')">
    <m-maindata-select-page ref="departmentRef" v-model="value_departmentIds" :multiple="multiple" clearable :action="(pages, keyword) => querySelectFilter(50, pages, keyword)" @change="handleDepartmentChange" />
  </el-form-item>
  <el-form-item v-if="selectStart <= 6 && selectEnd >= 6" :label="$t('mod.maindata.org_level_name.station')">
    <m-maindata-select-page ref="stationRef" v-model="value_stationIds" :multiple="multiple" clearable :action="(pages, keyword) => querySelectFilter(60, pages, keyword)" @change="handleStationChange" />
  </el-form-item>
</template>
<script>
import {computed, reactive, ref, toRefs} from "vue";
export default {
  components: {},
  props: {
    multiple: {
      type: Boolean,
      default: () => false
    },
    modelValue: {
      type: [String, Number, Array],
      required: true,
    },
    divisionIds: {
      type: [String, Number, Array],
      required: true,
    },
    marketingIds: {
      type: [String, Number, Array],
      required: true,
    },
    dutyregionIds: {
      type: [String, Number, Array],
      required: true,
    },
    departmentIds: {
      type: [String, Number, Array],
      required: true,
    },
    stationIds: {
      type: [String, Number, Array],
      required: true,
    },
    selectStart: {
      type: Number,
      default: () => 2
    },
    selectEnd: {
      type: Number,
      default: () => 5
    }
  },
  emits: ['update:divisionIds', 'update:marketingIds', 'update:dutyregionIds', 'update:departmentIds', 'update:stationIds'],
  setup(props, { emit }) {
    const { queryMorgSelect } = tpm.api.maindata.morg
    const state = reactive()

    const divisionRef = ref()
    const marketingcenterRef = ref()
    const dutyregionRef = ref()
    const departmentRef = ref()
    const stationRef = ref()

    const value_divisionIds = computed({
      get() {
        return props.divisionIds
      },
      set(val) {
        emit('update:divisionIds', val)
      },
    })

    const value_marketingIds = computed({
      get() {
        return props.marketingIds
      },
      set(val) {
        emit('update:marketingIds', val)
      },
    })

    const value_dutyregionIds = computed({
      get() {
        return props.dutyregionIds
      },
      set(val) {
        emit('update:dutyregionIds', val)
      },
    })

    const value_departmentIds = computed({
      get() {
        return props.departmentIds
      },
      set(val) {
        emit('update:departmentIds', val)
      },
    })

    const value_stationIds = computed({
      get() {
        return props.stationIds
      },
      set(val) {
        emit('update:stationIds', val)
      },
    })

    const querySelectFilter = (level, pages, keyword) => {
      return new Promise(async resolve => {
        let params = {
          level,
          page: {
            index: pages.index,
            size: pages.size
          },
          name: keyword
        }
        if(level >= 30 && value_divisionIds.value){
          params.level2Ids = value_divisionIds.value
        }
        if(level >= 40 && value_marketingIds.value){
          params.level3Ids = value_marketingIds.value
        }
        if(level >= 50 && value_dutyregionIds.value){
          params.level4Ids = value_dutyregionIds.value
        }
        if(level >= 60 && value_departmentIds.value){
          params.level5Ids = value_departmentIds.value
        }
        const data = await queryMorgSelect(params)
        resolve(data)
      })
    }

    const handleHeadOfficeChange = () => {
      divisionRef.value && divisionRef.value.reset()
      divisionRef.value && divisionRef.value.remoteMethod()
      handleDivisionChange()
    }

    const handleDivisionChange = () => {
      marketingcenterRef.value && marketingcenterRef.value.reset()
      marketingcenterRef.value && marketingcenterRef.value.remoteMethod()
      handleMarketingChange()
    }

    const handleMarketingChange = () => {
      dutyregionRef.value && dutyregionRef.value.reset()
      dutyregionRef.value && dutyregionRef.value.remoteMethod()
      handleDutyregionChange()
    }

    const handleDutyregionChange = () => {
      departmentRef.value && departmentRef.value.reset()
      departmentRef.value && departmentRef.value.remoteMethod()
      handleDepartmentChange()
    }

    const handleDepartmentChange = () => {
      stationRef.value && stationRef.value.reset()
      stationRef.value && stationRef.value.remoteMethod()
      handleStationChange()
    }

    const handleStationChange = () => {

    }

    return ({
      ...toRefs(state),
      value_divisionIds,
      value_marketingIds,
      value_dutyregionIds,
      value_departmentIds,
      value_stationIds,
      divisionRef,
      marketingcenterRef,
      dutyregionRef,
      departmentRef,
      stationRef,
      querySelectFilter,
      handleHeadOfficeChange,
      handleDivisionChange,
      handleMarketingChange,
      handleDutyregionChange,
      handleDepartmentChange,
      handleStationChange
    })
  }
}
</script>
<style lang="scss">
.input-table-filter-select{
  .el-popper{
    display: none!important;
  }
}
.input-table{
  .highlight{
    background-color: #d9ecff!important;
    &.el-table__row--striped{
      .el-table__cell{
        background: #d9ecff!important;
      }
    }
  }
}
</style>
<style lang="scss" scoped>
.input-table{
  border-radius: 5px;
  padding: 11px;
  position: fixed;
  left: 0;
  top: 30px;
  width: 100%;
  z-index: 3000;
  background: #f9f9f9;
  .col{
    margin-bottom: 10px;
  }
  .title{
    font-size: 16px;
  }
  .input-table-row{
    padding-right: 0!important;
  }
  .mt{
    margin-top: 8px
  }
}
</style>
