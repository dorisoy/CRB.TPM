<template>
  <el-select v-model="value_" :disabled="disabled" class="input-table-filter-select m-2" :teleported="false" placeholder="请选择" @focus="handleFocus">
    <el-option
        v-for="item in selected"
        :key="item.value"
        :label="item.label"
        :value="item.value"
    />
  </el-select>
  <div v-if="inputTableVisible" class="input-table" :style="{width: '100%', maxWidth: maxWidth + 'px',  left: (el.left - 11) + 'px', top: (el.top + el.height + 5) + 'px'}">
    <el-form :model="form" label-width="70px">
      <el-row class="input-table-row" :gutter="20">
        <el-col :span="12" class="col title">
          {{ title }}
        </el-col>
        <el-col :span="12" class="col m-text-right">
          <m-icon name="ri-close-fill" size="25px" @click.stop="inputTableVisible=false" />
        </el-col>
        <el-col v-if="selectStart <= 1 && selectEnd >= 1" :span="8" class="col">
          <el-form-item :label="$t('mod.maindata.org_level_name.head_office')">
            <m-maindata-select-page v-model="form.headofficeid" clearable :action="(pages, keyword) => querySelectFilter(10, pages, keyword)" @change="handleHeadOfficeChange"  @remove-tag="handleHeadOfficeChange" @clear="handleHeadOfficeChange" />
          </el-form-item>
        </el-col>
        <el-col v-if="selectStart <= 2 && selectEnd >= 2" :span="8" class="col">
          <el-form-item :label="$t('mod.maindata.org_level_name.division')">
            <m-maindata-select-page ref="divisionRef" v-model="form.divisionid" clearable :action="(pages, keyword) => querySelectFilter(20, pages, keyword)" @change="handleDivisionChange" @remove-tag="handleDivisionChange" @clear="handleDivisionChange" />
          </el-form-item>
        </el-col>
        <el-col v-if="selectStart <= 3 && selectEnd >= 3" :span="8" class="col">
          <el-form-item :label="$t('mod.maindata.org_level_name.marketing_center')">
            <m-maindata-select-page ref="marketingcenterRef" v-model="form.marketingcenterid" clearable :action="(pages, keyword) => querySelectFilter(30, pages, keyword)" @change="handleMarketingChange" @remove-tag="handleMarketingChange" @clear="handleMarketingChange" />
          </el-form-item>
        </el-col>
        <el-col v-if="selectStart <= 4 && selectEnd >= 4" :span="8" class="col">
          <el-form-item :label="$t('mod.maindata.org_level_name.dutyregion')">
            <m-maindata-select-page ref="dutyregionRef" v-model="form.dutyregionid" clearable :action="(pages, keyword) => querySelectFilter(40, pages, keyword)" @change="handleDutyregionChange" @remove-tag="handleDutyregionChange" @clear="handleDutyregionChange" />
          </el-form-item>
        </el-col>
        <el-col v-if="selectStart <= 5 && selectEnd >= 5" :span="8" class="col">
          <el-form-item :label="$t('mod.maindata.org_level_name.department')">
            <m-maindata-select-page ref="departmentRef" v-model="form.departmentid" clearable :action="(pages, keyword) => querySelectFilter(50, pages, keyword)" @change="handleDepartmentChange" @remove-tag="handleDepartmentChange" @clear="handleDepartmentChange"/>
          </el-form-item>
        </el-col>
        <el-col v-if="selectStart <= 6 && selectEnd >= 6" :span="8" class="col">
          <el-form-item :label="$t('mod.maindata.org_level_name.station')">
            <m-maindata-select-page ref="stationRef" v-model="form.stationid" clearable :action="(pages, keyword) => querySelectFilter(60, pages, keyword)" @change="handleStationChange" @remove-tag="handleStationChange" @clear="handleStationChange" />
          </el-form-item>
        </el-col>
        <el-col :span="8" class="col">
          <el-form-item :label="$t('mod.maindata.name_label')">
            <el-input v-model="form.name" />
          </el-form-item>
        </el-col>
        <el-col :span="8" class="col">
          <m-button type="primary" @click.stop="queryList(false)">{{$t('mod.maindata.search')}}</m-button>
        </el-col>
        <el-col :span="24">
          <el-table ref="tableRef" v-loading="loading" :row-class-name="rowClassName" border stripe :height="300" class="table" :data="data.rows" @row-click="handleRowClick">
            <el-table-column v-if="multiple" type="selection" width="40" />
            <el-table-column prop="label" :label="$t('mod.maindata.label')" width="180" />
            <el-table-column prop="value" :label="$t('mod.maindata.value')" />
          </el-table>
          <el-pagination
              v-model="pages.index"
              small
              background
              layout="prev, pager, next"
              :total="pages.total"
              class="mt"
              @current-change="pageChange"
          />
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>
<script>
import {computed, nextTick, reactive, ref, toRefs, watch} from "vue";
export default {
  components: {},
  props: {
    multiple: {
      type: Boolean,
      default: () => false
    },
    title: {
      type: String,
      default: () => ''
    },
    modelValue: {
      type: [String, Number, Array],
      required: true,
    },
    maxWidth: {
      type: [Number, String],
      default: () => '760'
    },
    selectStart: {
      type: Number,
      default: () => 2
    },
    selectEnd: {
      type: Number,
      default: () => 5
    },
    disabled: {
      type: Boolean,
      default: () => false
    },
    query: {
      type: Function,
      default: () => () => {}
    },
    querySelect: {
      type: Function,
      default: () => () => {}
    }
  },
  emits: ['update:modelValue'],
  setup(props, { emit }) {
    const { queryMorgSelect } = tpm.api.maindata.morg
    const state = reactive({
      inputTableVisible: false,
      el: {
        width: 0,
      },
      data: {
        rows: []
      },
      selected: [],
      loading: false
    })
    const form = ref({})
    const tableRef = ref()
    const divisionRef = ref()
    const marketingcenterRef = ref()
    const dutyregionRef = ref()
    const departmentRef = ref()
    const stationRef = ref()
    const pages = ref({
      total: 0,
      index: 1,
      size: 15
    })

    const value_ = computed({
      get() {
        return props.modelValue
      },
      set(val) {
        emit('update:modelValue', val)
      },
    })

    const handleFocus = (el) => {
      state.el = el.target.getBoundingClientRect();
      state.inputTableVisible = true
      nextTick(() => {
        tableRef.value.setCurrentRow(state.data.rows.filter((row => row.value === value_.value)))
      })
    }

    const handleBlur = () => {
      state.inputTableVisible = false
    }

    const queryList = async (selectValue = false) => {
      if(selectValue){
        //首次进入时，获取select已选值列表
        state.selected = await props.querySelect()
      }else{
        //点击查询时，获取全部数据
        state.loading = true
        const data = await props.query(form.value, pages.value)
        state.data.rows = data.rows
        pages.value.total = data.total
        state.loading = false
        nextTick(() => {
          tableRef.value.setCurrentRow(state.data.rows.filter((row => row.value === value_.value)))
        })
      }
    }

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
        if(level >= 30 && form.value.divisionid){
          params.level2Ids = [form.value.divisionid]
        }
        if(level >= 40 && form.value.marketingid){
          params.level3Ids = [form.value.marketingid]
        }
        if(level >= 50 && form.value.dutyregionid){
          params.level4Ids = [form.value.dutyregionid]
        }
        if(level >= 60 && form.value.departmentid){
          params.level5Ids = [form.value.departmentid]
        }
        const data = await queryMorgSelect(params)
        resolve(data)
      })
    }

    const handleHeadOfficeChange = () => {
      divisionRef.value && divisionRef.value.reset()
      divisionRef.value && divisionRef.value.remoteMethod('')
      handleDivisionChange()
    }

    const handleDivisionChange = () => {
      marketingcenterRef.value && marketingcenterRef.value.reset()
      marketingcenterRef.value && marketingcenterRef.value.remoteMethod('')
      handleMarketingChange()
    }

    const handleMarketingChange = () => {
      dutyregionRef.value && dutyregionRef.value.reset()
      dutyregionRef.value && dutyregionRef.value.remoteMethod('')
      handleDutyregionChange()
    }

    const handleDutyregionChange = () => {
      departmentRef.value && departmentRef.value.reset()
      departmentRef.value && departmentRef.value.remoteMethod('')
      handleDepartmentChange()
    }

    const handleDepartmentChange = () => {
      stationRef.value && stationRef.value.reset()
      stationRef.value && stationRef.value.remoteMethod('')
    }

    const handleStationChange = () => {

    }

    const rowClassName = ({row}) => {
      if(value_.value.indexOf(row.value) > -1){
        return 'highlight'
      }
      return ''
    }

    const handleRowClick = (row) => {
      state.selected = [row]
      value_.value = row.value
      if(!props.multiple){
        //如果是单选那么直接关闭表格筛选出啊昂口
        state.inputTableVisible = false
      }
    }

    watch(form.value.divisionid, (newValue, oldValue) => {
      console.log('watch', newValue, oldValue)
    })

    const pageChange = (v) => {
      pages.value.index = v
      queryList(false)
    }

    window.addEventListener("resize", () => {
      state.inputTableVisible = false
    });

    window.removeEventListener("resize", () => {});
    return ({
      ...toRefs(state),
      value_,
      tableRef,
      divisionRef,
      marketingcenterRef,
      dutyregionRef,
      departmentRef,
      stationRef,
      pages,
      form,
      pageChange,
      rowClassName,
      querySelectFilter,
      queryList,
      handleFocus,
      handleRowClick,
      handleBlur,
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
