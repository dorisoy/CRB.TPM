import { withSaveProps, useSave, useList, useMessage, dom } from "tpm-ui";
import { computed, reactive, ref, resolveComponent, openBlock, createBlock, mergeProps, toHandlers, withCtx, createVNode, createCommentVNode, createElementBlock, Fragment, renderList, createTextVNode, toDisplayString, createElementVNode, toRefs, resolveDirective, withDirectives, withModifiers, nextTick, watch, normalizeStyle, inject, renderSlot } from "vue";
const urls$b = {
  ORGENUMTYPESELECT: "Common/OrgEnumTypeSelect"
};
var api_common = (http) => {
  return {
    getOrgEnumTypeSelect: () => http.get(urls$b.ORGENUMTYPESELECT)
  };
};
const urls$a = {
  EXPORT: "MDistributor/Export",
  SELECT: "MDistributor/Select",
  DELETESELECTED: "MTerminal/DeleteSelected"
};
var api_mdistributor = (http) => {
  return {
    queryDistributorSelect: (params) => http.get(urls$a.SELECT, params),
    deleteSelected: (params) => http.delete(urls$a.DELETESELECTED, params),
    exportDistributor: (params) => http.download(urls$a.EXPORT, params)
  };
};
const urls$9 = {
  EXPORT: "MDistributorRelation/Export",
  DELETESELECTED: "MTerminal/DeleteSelected"
};
var api_mdistributorrelation = (http) => {
  return {
    deleteSelected: (params) => http.delete(urls$9.DELETESELECTED, params),
    exportDistributorRelation: (params) => http.download(urls$9.EXPORT, params)
  };
};
const urls$8 = {
  EXPORT: "MEntity/Export",
  DELETESELECTED: "MTerminal/DeleteSelected"
};
var api_mentity = (http) => {
  return {
    deleteSelected: (params) => http.delete(urls$8.DELETESELECTED, params),
    exportEntity: (params) => http.download(urls$8.EXPORT, params)
  };
};
const urls$7 = {
  EXPORT: "MMarketingProduct/Export",
  DELETESELECTED: "MMarketingProduct/DeleteSelected"
};
var api_mmarketingproduct = (http) => {
  return {
    deleteSelected: (params) => http.delete(urls$7.DELETESELECTED, params),
    exportMarketingProduct: (params) => http.download(urls$7.EXPORT, params)
  };
};
const urls$6 = {
  EXPORT: "MMarketingSetup/Export",
  DELETESELECTED: "MTerminal/DeleteSelected"
};
var api_mmarketingsetup = (http) => {
  return {
    deleteSelected: (params) => http.delete(urls$6.DELETESELECTED, params),
    exportMarketingSetup: (params) => http.download(urls$6.EXPORT, params)
  };
};
const urls$5 = {
  TREE: "MOrg/Tree",
  DELETESELECTED: "MOrg/DeleteSelected",
  SELECT: "MOrg/Select",
  GETCURRENTACCOUNTAROSTREE: "MOrg/GetCurrentAccountAROSTree",
  GETORGLEVEL: "MOrg/GetOrgLevel",
  GETTREEBYPARENTID: "MOrg/GetTreeByParentId",
  EXPORT: "MOrg/Export"
};
var api_morg = (http) => {
  return {
    tree: (level) => http.get(urls$5.TREE, level),
    deleteSelected: (params) => http.delete(urls$5.DELETESELECTED, params),
    queryMorgSelect: (params) => http.get(urls$5.SELECT, params),
    getCurrentAccountAROSTree: (params) => http.get(urls$5.GETCURRENTACCOUNTAROSTREE, params),
    getOrgLevel: (params) => http.get(urls$5.GETORGLEVEL, params),
    getTreeByParentId: (params) => http.get(urls$5.GETTREEBYPARENTID, params),
    export: (params) => http.download(urls$5.EXPORT, params)
  };
};
const urls$4 = {
  EXPORT: "MProduct/Export",
  SELECT: "MProduct/Select",
  DELETESELECTED: "MProduct/DeleteSelected"
};
var api_mproduct = (http) => {
  return {
    queryProductSelect: (params) => http.get(urls$4.SELECT, params),
    deleteSelected: (params) => http.delete(urls$4.DELETESELECTED, params),
    exportProduct: (params) => http.download(urls$4.EXPORT, params)
  };
};
const urls$3 = {
  EXPORT: "MProductProperty/Export",
  TYPESELECT: "MProductProperty/TypeSelect",
  DELETESELECTED: "MProductProperty/DeleteSelected"
};
var api_mproductproperty = (http) => {
  return {
    queryTypeSelect: (params) => http.get(urls$3.TYPESELECT, params),
    deleteSelected: (params) => http.delete(urls$3.DELETESELECTED, params),
    exportProductProperty: (params) => http.download(urls$3.EXPORT, params)
  };
};
const urls$2 = {
  EXPORT: "MTerminal/Export",
  SELECT: "MTerminal/Select",
  DELETESELECTED: "MTerminal/DeleteSelected"
};
var api_mterminal = (http) => {
  return {
    queryTerminalSelect: (params) => http.get(urls$2.SELECT, params),
    deleteSelected: (params) => http.delete(urls$2.DELETESELECTED, params),
    exportTermainal: (params) => http.download(urls$2.EXPORT, params)
  };
};
const urls$1 = {
  EXPORT: "MTerminalDistributor/Export",
  DELETESELECTED: "MTerminal/DeleteSelected"
};
var api_mterminaldistributor = (http) => {
  return {
    deleteSelected: (params) => http.delete(urls$1.DELETESELECTED, params),
    exportTerminalDistributor: (params) => http.download(urls$1.EXPORT, params)
  };
};
const urls = {
  EXPORT: "MTerminalUser/Export",
  DELETESELECTED: "MTerminal/DeleteSelected"
};
var api_mterminaluser = (http) => {
  return {
    deleteSelected: (params) => http.delete(urls.DELETESELECTED, params),
    exportTerminalUser: (params) => http.download(urls.EXPORT, params)
  };
};
const state = {
  dict: {
    groupCode: "",
    dictCode: ""
  },
  distributor: {
    type: [
      {
        label: "\u7ECF\u9500\u5546",
        value: 1
      },
      {
        label: "\u5206\u9500\u5546",
        value: 2
      }
    ],
    typeAll: [
      {
        label: "\u5168\u90E8",
        value: 0
      },
      {
        label: "\u7ECF\u9500\u5546",
        value: 1
      },
      {
        label: "\u5206\u9500\u5546",
        value: 2
      }
    ],
    detailType: [
      {
        label: "\u4E3B\u6237",
        value: 1
      },
      {
        label: "\u7BA1\u7406\u5F00\u6237\u7684\u5B50\u6237",
        value: 2
      },
      {
        label: "TPM\u865A\u62DF\u5B50\u6237",
        value: 3
      }
    ]
  }
};
const mutations = {
  setDict(state2, dict) {
    state2.dict = dict;
  }
};
var store = {
  namespaced: true,
  state,
  mutations
};
const name$b = "maindata_distributor";
const icon$b = "captcha";
const path$a = "/maindata/distributor";
const permissions$a = [
  "maindata_mdistributor_query_get"
];
const buttons$a = {
  add: {
    text: "tpm.add",
    code: "maindata_mdistributor_add",
    permissions: [
      "maindata_mdistributor_add_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    code: "maindata_mdistributor_export",
    permissions: [
      "maindata_mdistributor_export_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "maindata_mdistributor_edit",
    permissions: [
      "maindata_mdistributor_edit_get",
      "maindata_mdistributor_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "maindata_mdistributor_delete",
    permissions: [
      "maindata_mdistributor_delete_delete"
    ]
  }
};
var page$l = {
  name: name$b,
  icon: icon$b,
  path: path$a,
  permissions: permissions$a,
  buttons: buttons$a
};
var _export_sfc = (sfc, props) => {
  const target = sfc.__vccOpts || sfc;
  for (const [key, val] of props) {
    target[key] = val;
  }
  return target;
};
const _sfc_main$z = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { store: store2, $t: $t2 } = tpm;
    const api2 = tpm.api.maindata.mdistributor;
    const { queryMorgSelect } = tpm.api.maindata.morg;
    const maindataStore = store2.state.mod.maindata;
    const distributorTypeOptions = computed(() => {
      return maindataStore.distributor.type;
    });
    const distributorDetailTypeOptions = computed(() => {
      return maindataStore.distributor.detailType;
    });
    const queryDistributorTypeOptions = () => {
      return new Promise((resolve) => {
        resolve(distributorTypeOptions.value);
      });
    };
    const queryDistributorDetailTypeOptions = () => {
      return new Promise((resolve) => {
        resolve(distributorDetailTypeOptions.value);
      });
    };
    const model = reactive({
      distributorCode: "",
      distributorName: "",
      distributorType: 1,
      stationId: "",
      crmCode: "",
      detailType: 1,
      customerCode: "",
      parentId: "",
      isSynchronizeCrmStation: true
    });
    const rules = computed(() => {
      return {
        distributorCode: [{ required: true, message: $t2("mod.maindata.distributor.placeholder_code") }],
        distributorName: [{ required: true, message: $t2("mod.maindata.distributor.placeholder_name") }]
      };
    });
    const distributorCodeRef = ref(null);
    const parentIdVisible = ref(false);
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = distributorCodeRef;
    bind.width = "700px";
    const handleDetailTypeChange = (v) => {
      parentIdVisible.value = v === 2;
    };
    const handleClose = () => {
    };
    const handleOpened = () => {
    };
    const handleSuccess = () => {
      emit("success");
    };
    const parentQuery = (form, pages2) => {
      return new Promise(async (resolve) => {
        let level = 60;
        let params = {
          level,
          name: form.name,
          page: {
            index: pages2.index,
            size: pages2.size
          }
        };
        {
          params.level2Ids = form.divisionid;
        }
        {
          params.level2Ids = form.divisionid;
          params.level3Ids = form.marketingcenterid;
        }
        {
          params.level2Ids = form.divisionid;
          params.level3Ids = form.marketingcenterid;
          params.level4Ids = form.dutyregionid;
        }
        {
          params.level2Ids = form.divisionid;
          params.level3Ids = form.marketingcenterid;
          params.level4Ids = form.dutyregionid;
          params.level5Ids = form.departmentid;
        }
        let data = await queryMorgSelect(params);
        resolve(data);
      });
    };
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      distributorCodeRef,
      parentIdVisible,
      distributorTypeOptions,
      distributorDetailTypeOptions,
      queryDistributorTypeOptions,
      queryDistributorDetailTypeOptions,
      handleDetailTypeChange,
      handleSuccess,
      handleClose,
      parentQuery,
      handleOpened
    };
  }
};
function _sfc_render$z(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_m_select = resolveComponent("m-select");
  const _component_m_maindata_input_table_filter = resolveComponent("m-maindata-input-table-filter");
  const _component_el_switch = resolveComponent("el-switch");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, { "label-width": 210 }, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.distributor.code"),
                prop: "distributorCode"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    ref: "distributorCodeRef",
                    modelValue: $setup.model.distributorCode,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.distributorCode = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.distributor.name"),
                prop: "distributorName"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.distributorName,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.distributorName = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.distributor.type"),
                prop: "distributorType"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_select, {
                    modelValue: $setup.model.distributorType,
                    "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.distributorType = $event),
                    action: $setup.queryDistributorTypeOptions
                  }, null, 8, ["modelValue", "action"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.org_level_name.station"),
                prop: "stationId"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_input_table_filter, {
                    ref: "inputTableFilterRef",
                    modelValue: $setup.model.stationId,
                    "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.stationId = $event),
                    title: _ctx.$t("mod.maindata.terminal.placeholder_station_id"),
                    query: $setup.parentQuery,
                    "query-select": _ctx.parentSelectQuery,
                    "select-end": 4
                  }, null, 8, ["modelValue", "title", "query", "query-select"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.distributor.crm_code"),
                prop: "crmCode"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.crmCode,
                    "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.model.crmCode = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.distributor.detail_type"),
                prop: "detailType"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_select, {
                    modelValue: $setup.model.detailType,
                    "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.model.detailType = $event),
                    action: $setup.queryDistributorDetailTypeOptions,
                    onChange: $setup.handleDetailTypeChange
                  }, null, 8, ["modelValue", "action", "onChange"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.distributor.customer_code"),
                prop: "customerCode"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.customerCode,
                    "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => $setup.model.customerCode = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          $setup.parentIdVisible ? (openBlock(), createBlock(_component_el_col, {
            key: 0,
            span: 24
          }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.distributor.parent_id"),
                prop: "parentId"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.parentId,
                    "onUpdate:modelValue": _cache[7] || (_cache[7] = ($event) => $setup.model.parentId = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })) : createCommentVNode("", true),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.distributor.is_synchronize_crm_station"),
                prop: "isSynchronizeCrmStation	"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_switch, {
                    modelValue: $setup.model.isSynchronizeCrmStation,
                    "onUpdate:modelValue": _cache[8] || (_cache[8] = ($event) => $setup.model.isSynchronizeCrmStation = $event),
                    "active-value": true,
                    "inactive-value": false
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save$a = /* @__PURE__ */ _export_sfc(_sfc_main$z, [["render", _sfc_render$z]]);
const _sfc_main$y = {
  components: { Save: Save$a },
  setup() {
    const listRef = ref();
    const current = ref();
    const { store: store2 } = tpm;
    const maindataStore = store2.state.mod.maindata;
    const { query, exportDistributor, remove, deleteSelected } = tpm.api.maindata.mdistributor;
    const distributorTypeOptions = computed(() => {
      return maindataStore.distributor.typeAll;
    });
    const model = ref({
      Name: null,
      DistributorType: 0,
      divisionIds: "",
      marketingIds: "",
      dutyregionIds: "",
      departmentIds: "",
      stationIds: ""
    });
    const cols = [
      {
        prop: "distributorCode",
        label: "mod.maindata.distributor.code",
        width: 150
      },
      {
        prop: "distributorName",
        label: "mod.maindata.distributor.name",
        width: 300
      },
      {
        prop: "distributorType",
        label: "mod.maindata.distributor.type",
        width: 150
      },
      {
        prop: "marketingName",
        label: "mod.maindata.distributor.marketing_name",
        width: 150
      },
      {
        prop: "dutyregionName",
        label: "mod.maindata.distributor.dutyregion_name",
        width: 150
      },
      {
        prop: "departmentName",
        label: "mod.maindata.distributor.department_name",
        width: 150
      },
      {
        prop: "stationName",
        label: "mod.maindata.distributor.station_name",
        width: 150
      },
      {
        prop: "entityName",
        label: "mod.maindata.distributor.entity_name",
        width: 150
      },
      {
        prop: "crmCode",
        label: "mod.maindata.distributor.crm_code",
        width: 150
      },
      {
        prop: "detailType",
        label: "mod.maindata.distributor.detail_type",
        width: 150
      },
      {
        prop: "customerCode",
        label: "mod.maindata.distributor.customer_code",
        width: 150
      },
      {
        prop: "parentId",
        label: "mod.maindata.distributor.parent_id",
        width: 350
      },
      {
        prop: "parentCode",
        label: "mod.maindata.distributor.parent_code",
        width: 160
      },
      {
        prop: "isSynchronizeCrmStation",
        label: "mod.maindata.distributor.is_synchronize_crm_station",
        width: 180
      },
      {
        prop: "creator",
        label: "mod.maindata.creator",
        width: 150
      },
      {
        prop: "createdTime",
        label: "mod.maindata.created_time",
        width: 180
      },
      {
        prop: "Modifier",
        label: "mod.maindata.modifier",
        width: 150
      },
      {
        prop: "ModifiedTime",
        label: "mod.maindata.modified_t ime",
        width: 180
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    const deleteMethod = (ids) => {
      return new Promise(async (resolve) => {
        resolve(await deleteSelected({ ids }));
      });
    };
    const mode = ref("");
    return {
      current,
      listRef,
      refresh,
      page: page$l,
      mode,
      model,
      cols,
      query,
      remove,
      exportDistributor,
      distributorTypeOptions,
      ...list,
      deleteMethod
    };
  }
};
function _sfc_render$y(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_option = resolveComponent("el-option");
  const _component_el_select = resolveComponent("el-select");
  const _component_m_maindata_query_org_group = resolveComponent("m-maindata-query-org-group");
  const _component_el_tag = resolveComponent("el-tag");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        multiple: true,
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        "show-export": "",
        "export-method": $setup.exportDistributor,
        "delete-method": $setup.deleteMethod,
        "show-delete-btn": ""
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("mod.maindata.code_name"),
            prop: "Name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.Name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.Name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_el_form_item, {
            label: _ctx.$t("mod.maindata.distributor.type"),
            prop: "distributorType"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_select, {
                modelValue: $setup.model.DistributorType,
                "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.DistributorType = $event),
                clearable: "",
                class: "m-2",
                placeholder: "\u8BF7\u9009\u62E9\u5BA2\u6237\u7C7B\u578B"
              }, {
                default: withCtx(() => [
                  (openBlock(true), createElementBlock(Fragment, null, renderList($setup.distributorTypeOptions, (item) => {
                    return openBlock(), createBlock(_component_el_option, {
                      key: item.value,
                      label: item.label,
                      value: item.value
                    }, null, 8, ["label", "value"]);
                  }), 128))
                ]),
                _: 1
              }, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_m_maindata_query_org_group, {
            divisionIds: $setup.model.divisionIds,
            "onUpdate:divisionIds": _cache[2] || (_cache[2] = ($event) => $setup.model.divisionIds = $event),
            marketingIds: $setup.model.marketingIds,
            "onUpdate:marketingIds": _cache[3] || (_cache[3] = ($event) => $setup.model.marketingIds = $event),
            dutyregionIds: $setup.model.dutyregionIds,
            "onUpdate:dutyregionIds": _cache[4] || (_cache[4] = ($event) => $setup.model.dutyregionIds = $event),
            departmentIds: $setup.model.departmentIds,
            "onUpdate:departmentIds": _cache[5] || (_cache[5] = ($event) => $setup.model.departmentIds = $event),
            stationIds: $setup.model.stationIds,
            "onUpdate:stationIds": _cache[6] || (_cache[6] = ($event) => $setup.model.stationIds = $event),
            multiple: true,
            "select-start": 3,
            "select-end": 6
          }, null, 8, ["divisionIds", "marketingIds", "dutyregionIds", "departmentIds", "stationIds"])
        ]),
        "col-distributorType": withCtx(({ row }) => [
          parseInt(row.distributorType) === 1 ? (openBlock(), createBlock(_component_el_tag, {
            key: 0,
            size: "small",
            type: "success",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.maindata.distributor.type_list.type_1")), 1)
            ]),
            _: 1
          })) : (openBlock(), createBlock(_component_el_tag, {
            key: 1,
            size: "small",
            type: "warning",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.maindata.distributor.type_list.type_1")), 1)
            ]),
            _: 1
          }))
        ]),
        "col-detailType": withCtx(({ row }) => [
          parseInt(row.detailType) === 1 ? (openBlock(), createBlock(_component_el_tag, {
            key: 0,
            size: "small",
            type: "success",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.maindata.distributor.detailtype_list.type_1")), 1)
            ]),
            _: 1
          })) : parseInt(row.detailType) === 2 ? (openBlock(), createBlock(_component_el_tag, {
            key: 1,
            size: "small",
            type: "warning",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.maindata.distributor.detailtype_list.type_2")), 1)
            ]),
            _: 1
          })) : (openBlock(), createBlock(_component_el_tag, {
            key: 2,
            size: "small",
            type: "info",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.maindata.distributor.detailtype_list.type_3")), 1)
            ]),
            _: 1
          }))
        ]),
        "col-isSynchronizeCrmStation": withCtx(({ row }) => [
          row.isSynchronizeCrmStation ? (openBlock(), createBlock(_component_el_tag, {
            key: 0,
            type: "success",
            size: "small",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.maindata.true")), 1)
            ]),
            _: 1
          })) : (openBlock(), createBlock(_component_el_tag, {
            key: 1,
            size: "small",
            type: "info",
            effect: "error"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.maindata.false")), 1)
            ]),
            _: 1
          }))
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button_edit, {
            code: $setup.page.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: $setup.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.page.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: $setup.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method", "delete-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[7] || (_cache[7] = ($event) => _ctx.saveVisible = $event),
        selection: _ctx.selection,
        mode: $setup.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "selection", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$a = /* @__PURE__ */ _export_sfc(_sfc_main$y, [["render", _sfc_render$y]]);
const page$k = {
  "name": "maindata_distributor",
  "icon": "captcha",
  "path": "/maindata/distributor",
  "permissions": [
    "maindata_mdistributor_query_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "maindata_mdistributor_add",
      "permissions": [
        "maindata_mdistributor_add_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "code": "maindata_mdistributor_export",
      "permissions": [
        "maindata_mdistributor_export_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "maindata_mdistributor_edit",
      "permissions": [
        "maindata_mdistributor_edit_get",
        "maindata_mdistributor_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "maindata_mdistributor_delete",
      "permissions": [
        "maindata_mdistributor_delete_delete"
      ]
    }
  }
};
page$k.component = component$a;
component$a.name = page$k.name;
const name$a = "maindata_distributor_relation";
const icon$a = "captcha";
const path$9 = "/maindata/distributor_relation";
const permissions$9 = [
  "maindata_distributor_relation_query_get"
];
const buttons$9 = {
  add: {
    text: "tpm.add",
    code: "maindata_distributor_relation_add",
    permissions: [
      "maindata_distributor_relation_add_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    code: "maindata_distributor_relation_export",
    permissions: [
      "maindata_distributor_relation_export_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "maindata_distributor_relation_edit",
    permissions: [
      "maindata_distributor_relation_edit_get",
      "maindata_distributor_relation_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "maindata_distributor_relation_delete",
    permissions: [
      "maindata_distributor_relation_delete_delete"
    ]
  }
};
var page$j = {
  name: name$a,
  icon: icon$a,
  path: path$9,
  permissions: permissions$9,
  buttons: buttons$9
};
const _sfc_main$x = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object,
      default: () => {
      }
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t: $t2 } = tpm;
    const api2 = tpm.api.maindata.mdistributorrelation;
    const { queryDistributorSelect } = tpm.api.maindata.mdistributor;
    const model = reactive({
      distributorId1: "",
      distributorId2: ""
    });
    const rules = computed(() => {
      return {
        distributorId1: [{ required: true, message: $t2("mod.maindata.distributor_relation.placeholder_type_1") }],
        distributorId2: [{ required: true, message: $t2("mod.maindata.distributor_relation.placeholder_type_2") }]
      };
    });
    const distributorId1Ref = ref(null);
    const distributorId2Ref = ref(null);
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit, afterEdit: () => {
      distributorId2Ref.value.remoteMethod("", true);
      distributorId1Ref.value.remoteMethod("", true);
    } });
    bind.width = "700px";
    const handleClose = () => {
    };
    const handleOpened = () => {
    };
    const handleSuccess = () => {
      emit("success");
    };
    const queryCustomer = (pages2, keyword, getValues) => {
      return new Promise(async (resolve) => {
        let params = {
          page: { index: pages2.index, size: pages2.size },
          name: keyword,
          type: 1
        };
        if (getValues && model.distributorId1) {
          params.ids = [model.distributorId1];
        }
        const data = await queryDistributorSelect(params);
        resolve(data);
      });
    };
    const queryDistributor = (pages2, keyword, getValues) => {
      return new Promise(async (resolve) => {
        let params = {
          page: { index: pages2.index, size: pages2.size },
          name: keyword,
          type: 2
        };
        if (getValues && model.distributorId2) {
          params.ids = [model.distributorId2];
        }
        const data = await queryDistributorSelect(params);
        resolve(data);
      });
    };
    if (props.mode === "add") {
      distributorId1Ref.value.remoteMethod("");
      distributorId2Ref.value.remoteMethod("");
    }
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      distributorId1Ref,
      distributorId2Ref,
      handleSuccess,
      handleClose,
      handleOpened,
      queryCustomer,
      queryDistributor
    };
  }
};
function _sfc_render$x(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_maindata_select_page = resolveComponent("m-maindata-select-page");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, { "label-width": 140 }, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.distributor.type_list.type_1"),
                prop: "distributorId1"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_select_page, {
                    ref: "distributorId1Ref",
                    modelValue: $setup.model.distributorId1,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.distributorId1 = $event),
                    action: $setup.queryCustomer
                  }, null, 8, ["modelValue", "action"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.distributor.type_list.type_2"),
                prop: "distributorId2"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_select_page, {
                    ref: "distributorId2Ref",
                    modelValue: $setup.model.distributorId2,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.distributorId2 = $event),
                    action: $setup.queryDistributor
                  }, null, 8, ["modelValue", "action"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save$9 = /* @__PURE__ */ _export_sfc(_sfc_main$x, [["render", _sfc_render$x]]);
const _sfc_main$w = {
  components: { Save: Save$9 },
  setup() {
    const listRef = ref();
    const current = ref();
    const { query, exportDistributorRelation, remove, deleteSelected } = tpm.api.maindata.mdistributorrelation;
    const model = ref({
      Name: null
    });
    const cols = [
      {
        prop: "distributorCode1",
        label: "mod.maindata.distributor_relation.distributor_code_1",
        width: 150
      },
      {
        prop: "distributorName1",
        label: "mod.maindata.distributor_relation.distributor_name_1",
        width: 300
      },
      {
        prop: "distributorStationName1",
        label: "mod.maindata.distributor_relation.distributor_station_name_1",
        width: 150
      },
      {
        prop: "distributorCode2",
        label: "mod.maindata.distributor_relation.distributor_code_2",
        width: 150
      },
      {
        prop: "distributorName2",
        label: "mod.maindata.distributor_relation.distributor_name_2",
        width: 300
      },
      {
        prop: "distributorStationName2",
        label: "mod.maindata.distributor_relation.distributor_station_name_2",
        width: 180
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    const deleteMethod = (ids) => {
      return new Promise(async (resolve) => {
        resolve(await deleteSelected({ ids }));
      });
    };
    const mode = ref("");
    return {
      current,
      listRef,
      refresh,
      page: page$j,
      mode,
      model,
      cols,
      query,
      remove,
      exportDistributorRelation,
      ...list,
      deleteMethod
    };
  }
};
function _sfc_render$w(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        multiple: true,
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        "show-export": "",
        "export-method": $setup.exportDistributorRelation,
        "delete-method": $setup.deleteMethod,
        "show-delete-btn": ""
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("mod.maindata.code_name"),
            prop: "Name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.Name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.Name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"])
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button_edit, {
            code: $setup.page.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: $setup.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.page.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: $setup.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method", "delete-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => _ctx.saveVisible = $event),
        selection: _ctx.selection,
        mode: $setup.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "selection", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$9 = /* @__PURE__ */ _export_sfc(_sfc_main$w, [["render", _sfc_render$w]]);
const page$i = {
  "name": "maindata_distributor_relation",
  "icon": "captcha",
  "path": "/maindata/distributor_relation",
  "permissions": [
    "maindata_distributor_relation_query_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "maindata_distributor_relation_add",
      "permissions": [
        "maindata_distributor_relation_add_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "code": "maindata_distributor_relation_export",
      "permissions": [
        "maindata_distributor_relation_export_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "maindata_distributor_relation_edit",
      "permissions": [
        "maindata_distributor_relation_edit_get",
        "maindata_distributor_relation_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "maindata_distributor_relation_delete",
      "permissions": [
        "maindata_distributor_relation_delete_delete"
      ]
    }
  }
};
page$i.component = component$9;
component$9.name = page$i.name;
const name$9 = "maindata_entity";
const icon$9 = "captcha";
const path$8 = "/maindata/entity";
const permissions$8 = [
  "maindata_entity_query_get"
];
const buttons$8 = {
  add: {
    text: "tpm.add",
    code: "maindata_entity_add",
    permissions: [
      "maindata_entity_add_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    code: "maindata_entity_export",
    permissions: [
      "maindata_entity_export_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "maindata_entity_edit",
    permissions: [
      "maindata_entity_edit_get",
      "maindata_entity_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "maindata_entity_delete",
    permissions: [
      "maindata_entity_delete_delete"
    ]
  }
};
var page$h = {
  name: name$9,
  icon: icon$9,
  path: path$8,
  permissions: permissions$8,
  buttons: buttons$8
};
const _sfc_main$v = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object,
      default: () => {
      }
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t: $t2 } = tpm;
    const api2 = tpm.api.maindata.mentity;
    const model = reactive({
      entityCode: "",
      entityName: "",
      erpCode: "",
      enabled: true
    });
    const rules = computed(() => {
      return {
        entityCode: [{ required: true, message: $t2("mod.maindata.entity.placeholder_code") }],
        entityName: [{ required: true, message: $t2("mod.maindata.entity.placeholder_name") }],
        erpCode: [{ required: true, message: $t2("mod.maindata.entity.placeholder_erp_code") }]
      };
    });
    const entityCodeRef = ref(null);
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = entityCodeRef;
    bind.width = "700px";
    const handleClose = () => {
    };
    const handleOpened = () => {
    };
    const handleSuccess = () => {
      emit("success");
    };
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      entityCodeRef,
      handleSuccess,
      handleClose,
      handleOpened
    };
  }
};
function _sfc_render$v(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_switch = resolveComponent("el-switch");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, { "label-width": 140 }, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.entity.code"),
                prop: "entityCode"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    ref: "entityCodeRef",
                    modelValue: $setup.model.entityCode,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.entityCode = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.entity.name"),
                prop: "entityName"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.entityName,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.entityName = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.entity.erp_code"),
                prop: "erpCode"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.erpCode,
                    "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.erpCode = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.enabled"),
                prop: "enabled"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_switch, {
                    modelValue: $setup.model.enabled,
                    "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.enabled = $event),
                    "active-value": true,
                    "inactive-value": false
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save$8 = /* @__PURE__ */ _export_sfc(_sfc_main$v, [["render", _sfc_render$v]]);
const _sfc_main$u = {
  components: { Save: Save$8 },
  setup() {
    const listRef = ref();
    const current = ref();
    const { query, exportEntity, remove, deleteSelected } = tpm.api.maindata.mentity;
    const model = ref({
      Name: null
    });
    const cols = [
      {
        prop: "entityCode",
        label: "mod.maindata.entity.code",
        width: 150
      },
      {
        prop: "entityName",
        label: "mod.maindata.entity.name",
        width: 300
      },
      {
        prop: "erpCode",
        label: "mod.maindata.entity.erp_code",
        width: 150
      },
      {
        prop: "enabled",
        label: "mod.maindata.enabled",
        width: 150
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    const deleteMethod = (ids) => {
      return new Promise(async (resolve) => {
        resolve(await deleteSelected({ ids }));
      });
    };
    const mode = ref("");
    return {
      current,
      listRef,
      refresh,
      page: page$h,
      mode,
      model,
      cols,
      query,
      remove,
      exportEntity,
      ...list,
      deleteMethod
    };
  }
};
function _sfc_render$u(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        multiple: true,
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        "show-export": "",
        "export-method": $setup.exportEntity,
        "delete-method": $setup.deleteMethod,
        "show-delete-btn": ""
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("mod.maindata.code_name"),
            prop: "Name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.Name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.Name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"])
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button_edit, {
            code: $setup.page.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: $setup.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.page.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: $setup.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method", "delete-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => _ctx.saveVisible = $event),
        selection: _ctx.selection,
        mode: $setup.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "selection", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$8 = /* @__PURE__ */ _export_sfc(_sfc_main$u, [["render", _sfc_render$u]]);
const page$g = {
  "name": "maindata_entity",
  "icon": "captcha",
  "path": "/maindata/entity",
  "permissions": [
    "maindata_entity_query_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "maindata_entity_add",
      "permissions": [
        "maindata_entity_add_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "code": "maindata_entity_export",
      "permissions": [
        "maindata_entity_export_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "maindata_entity_edit",
      "permissions": [
        "maindata_entity_edit_get",
        "maindata_entity_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "maindata_entity_delete",
      "permissions": [
        "maindata_entity_delete_delete"
      ]
    }
  }
};
page$g.component = component$8;
component$8.name = page$g.name;
const name$8 = "maindata_marketing_product";
const icon$8 = "captcha";
const path$7 = "/maindata/marketing_product";
const permissions$7 = [
  "maindata_marketing_product_query_get"
];
const buttons$7 = {
  add: {
    text: "tpm.add",
    code: "maindata_marketing_product_add",
    permissions: [
      "maindata_marketing_product_add_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    code: "maindata_marketing_product_export",
    permissions: [
      "maindata_marketing_product_export_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "maindata_marketing_product_edit",
    permissions: [
      "maindata_marketing_product_edit_get",
      "maindata_marketing_product_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "maindata_marketing_product_delete",
    permissions: [
      "maindata_marketing_product_delete_delete"
    ]
  }
};
var page$f = {
  name: name$8,
  icon: icon$8,
  path: path$7,
  permissions: permissions$7,
  buttons: buttons$7
};
const _sfc_main$t = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object,
      default: () => {
      }
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t: $t2 } = tpm;
    const api2 = tpm.api.maindata.mmarketingproduct;
    const { queryMorgSelect } = tpm.api.maindata.morg;
    const { queryProductSelect } = tpm.api.maindata.mproduct;
    const model = reactive({
      marketingId: "",
      productId: ""
    });
    const rules = computed(() => {
      return {
        productPropertiesType: [{ required: true, message: $t2("mod.maindata.product_properties.placeholder_type") }],
        productPropertiesCode: [{ required: true, message: $t2("mod.maindata.product_properties.placeholder_code") }],
        productPropertiesName: [{ required: true, message: $t2("mod.maindata.product_properties.placeholder_name") }]
      };
    });
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit });
    bind.width = "700px";
    const handleClose = () => {
      model.value = {
        marketingId: "",
        productId: ""
      };
    };
    const handleOpened = () => {
    };
    const handleSuccess = () => {
      emit("success");
    };
    const typeSelect = () => {
      return new Promise(async (resolve) => {
        const data = await api2.queryTypeSelect();
        resolve(data.rows);
      });
    };
    const parentQuery = (form) => {
      return new Promise(async (resolve) => {
        let level = 30;
        let params = {
          level,
          name: form.name
        };
        {
          params.level2Ids = form.divisionid;
        }
        {
          params.level2Ids = form.divisionid;
          params.level3Ids = form.marketingcenterid;
        }
        let data = await queryMorgSelect(params);
        resolve(data);
      });
    };
    const parentSelectQuery = () => {
      return new Promise(async (resolve) => {
        let level = 30;
        let params = {
          level,
          ids: [model.marketingId]
        };
        let data = await queryMorgSelect(params);
        resolve(data.rows);
      });
    };
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      api: api2,
      handleSuccess,
      handleClose,
      handleOpened,
      typeSelect,
      parentQuery,
      parentSelectQuery,
      queryProductSelect
    };
  }
};
function _sfc_render$t(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_maindata_input_table_filter = resolveComponent("m-maindata-input-table-filter");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_m_maindata_select_page = resolveComponent("m-maindata-select-page");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, { "label-width": 140 }, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.org_level_name.marketing_center"),
                prop: "marketingCode"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_input_table_filter, {
                    ref: "marketingCodeRef",
                    modelValue: $setup.model.marketingId,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.marketingId = $event),
                    title: _ctx.$t("mod.maindata.input_table_filter.placeholder_marketing_center"),
                    query: $setup.parentQuery,
                    "query-select": $setup.parentSelectQuery,
                    "select-end": 2
                  }, null, 8, ["modelValue", "title", "query", "query-select"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.product.label"),
                prop: "productId"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_select_page, {
                    ref: "terminalIdRef",
                    modelValue: $setup.model.productId,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.productId = $event),
                    action: $setup.queryProductSelect
                  }, null, 8, ["modelValue", "action"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save$7 = /* @__PURE__ */ _export_sfc(_sfc_main$t, [["render", _sfc_render$t]]);
const _sfc_main$s = {
  components: { Save: Save$7 },
  setup() {
    const listRef = ref();
    const current = ref();
    const { query, exportMarketingProduct, remove, deleteSelected } = tpm.api.maindata.mmarketingproduct;
    const model = ref({
      Name: null
    });
    const cols = [
      {
        prop: "marketingCode",
        label: "mod.maindata.product_properties.type_name",
        width: 150
      },
      {
        prop: "marketingName",
        label: "mod.maindata.product_properties.code",
        width: 150
      },
      {
        prop: "productId",
        label: "mod.maindata.product.id",
        width: 350
      },
      {
        prop: "productName",
        label: "mod.maindata.product.name",
        width: 200
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    const deleteMethod = (ids) => {
      return new Promise(async (resolve) => {
        resolve(await deleteSelected({ ids }));
      });
    };
    const mode = ref("");
    return {
      current,
      listRef,
      refresh,
      page: page$f,
      mode,
      model,
      cols,
      query,
      remove,
      exportMarketingProduct,
      ...list,
      deleteMethod
    };
  }
};
function _sfc_render$s(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        multiple: true,
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        "show-export": "",
        "export-method": $setup.exportMarketingProduct,
        "delete-method": $setup.deleteMethod,
        "show-delete-btn": ""
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("mod.maindata.code_name"),
            prop: "Name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.Name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.Name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"])
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button_edit, {
            code: $setup.page.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: $setup.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.page.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: $setup.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method", "delete-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => _ctx.saveVisible = $event),
        selection: _ctx.selection,
        mode: $setup.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "selection", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$7 = /* @__PURE__ */ _export_sfc(_sfc_main$s, [["render", _sfc_render$s]]);
const page$e = {
  "name": "maindata_marketing_product",
  "icon": "captcha",
  "path": "/maindata/marketing_product",
  "permissions": [
    "maindata_marketing_product_query_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "maindata_marketing_product_add",
      "permissions": [
        "maindata_marketing_product_add_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "code": "maindata_marketing_product_export",
      "permissions": [
        "maindata_marketing_product_export_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "maindata_marketing_product_edit",
      "permissions": [
        "maindata_marketing_product_edit_get",
        "maindata_marketing_product_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "maindata_marketing_product_delete",
      "permissions": [
        "maindata_marketing_product_delete_delete"
      ]
    }
  }
};
page$e.component = component$7;
component$7.name = page$e.name;
const name$7 = "maindata_marketingsetup";
const icon$7 = "captcha";
const path$6 = "/maindata/marketingsetup";
const permissions$6 = [
  "maindata_marketingsetup_query_get"
];
const buttons$6 = {
  add: {
    text: "tpm.add",
    code: "maindata_marketingsetup_add",
    permissions: [
      "maindata_marketingsetup_add_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    code: "maindata_marketingsetup_export",
    permissions: [
      "maindata_marketingsetup_export_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "maindata_marketingsetup_edit",
    permissions: [
      "maindata_marketingsetup_edit_get",
      "maindata_marketingsetup_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "maindata_marketingsetup_delete",
    permissions: [
      "maindata_marketingsetup_delete_delete"
    ]
  }
};
var page$d = {
  name: name$7,
  icon: icon$7,
  path: path$6,
  permissions: permissions$6,
  buttons: buttons$6
};
const _sfc_main$r = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t: $t2 } = tpm;
    const api2 = tpm.api.maindata.mmarketingsetup;
    const { queryMorgSelect } = tpm.api.maindata.morg;
    const model = reactive({
      marketingId: "",
      isReal: true,
      isSynchronizeCrm: true,
      isSynchronizeCrmDistributorStation: true
    });
    const rules = computed(() => {
      return {
        marketingId: [{ required: true, message: $t2("mod.maindata.marketing_setup.placeholder_marketing_center") }]
      };
    });
    const inputTableFilterRef = ref(null);
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit, afterEdit: () => {
      inputTableFilterRef.value.queryList(true);
    } });
    bind.width = "700px";
    const handleClose = () => {
    };
    const handleOpened = () => {
    };
    const handleSuccess = () => {
      emit("success");
    };
    const parentQuery = (form) => {
      return new Promise(async (resolve) => {
        let level = 30;
        let params = {
          level,
          name: form.name
        };
        {
          params.level2Ids = form.divisionid;
        }
        {
          params.level2Ids = form.divisionid;
          params.level3Ids = form.marketingcenterid;
        }
        let data = await queryMorgSelect(params);
        resolve(data);
      });
    };
    const parentSelectQuery = () => {
      return new Promise(async (resolve) => {
        let level = 30;
        let params = {
          level,
          ids: [model.marketingId]
        };
        let data = await queryMorgSelect(params);
        resolve(data.rows);
      });
    };
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      inputTableFilterRef,
      parentQuery,
      parentSelectQuery,
      handleSuccess,
      handleClose,
      handleOpened
    };
  }
};
function _sfc_render$r(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_maindata_input_table_filter = resolveComponent("m-maindata-input-table-filter");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_switch = resolveComponent("el-switch");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, { "label-width": 210 }, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.org_level_name.marketing_center"),
                prop: "marketingId"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_input_table_filter, {
                    ref: "inputTableFilterRef",
                    modelValue: $setup.model.marketingId,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.marketingId = $event),
                    title: _ctx.$t("mod.maindata.marketing_setup.placeholder_marketing_center"),
                    query: $setup.parentQuery,
                    "query-select": $setup.parentSelectQuery,
                    "select-end": 2
                  }, null, 8, ["modelValue", "title", "query", "query-select"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.marketing_setup.is_real"),
                prop: "isReal"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_switch, {
                    modelValue: $setup.model.isReal,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.isReal = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.marketing_setup.is_synchronize_crm"),
                prop: "isSynchronizeCrm"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_switch, {
                    modelValue: $setup.model.isSynchronizeCrm,
                    "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.isSynchronizeCrm = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.marketing_setup.is_synchronize_crm_distributor_station"),
                prop: "isSynchronizeCrmDistributorStation"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_switch, {
                    modelValue: $setup.model.isSynchronizeCrmDistributorStation,
                    "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.isSynchronizeCrmDistributorStation = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save$6 = /* @__PURE__ */ _export_sfc(_sfc_main$r, [["render", _sfc_render$r]]);
const _sfc_main$q = {
  components: { Save: Save$6 },
  setup() {
    const listRef = ref();
    const current = ref();
    const { query, exportMarketingSetup, remove, deleteSelected } = tpm.api.maindata.mmarketingsetup;
    const model = ref({
      Name: null
    });
    const cols = [
      {
        prop: "orgCode",
        label: "mod.maindata.marketing_setup.code",
        width: 150
      },
      {
        prop: "orgName",
        label: "mod.maindata.marketing_setup.name",
        width: 300
      },
      {
        prop: "isReal",
        label: "mod.maindata.marketing_setup.is_real",
        width: 150
      },
      {
        prop: "isSynchronizeCrm",
        label: "mod.maindata.marketing_setup.is_synchronize_crm",
        width: 180
      },
      {
        prop: "isSynchronizeCrmDistributorStation",
        label: "mod.maindata.marketing_setup.is_synchronize_crm_distributor_station",
        width: 220
      },
      {
        prop: "creator",
        label: "mod.maindata.creator",
        width: 150
      },
      {
        prop: "createdTime",
        label: "mod.maindata.created_time",
        width: 180
      },
      {
        prop: "modifier",
        label: "mod.maindata.modifier",
        width: 150
      },
      {
        prop: "modifiedTime",
        label: "mod.maindata.modified_time",
        width: 180
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    const deleteMethod = (ids) => {
      return new Promise(async (resolve) => {
        resolve(await deleteSelected({ ids }));
      });
    };
    return {
      current,
      listRef,
      refresh,
      page: page$d,
      model,
      cols,
      query,
      remove,
      exportMarketingSetup,
      ...list,
      deleteMethod
    };
  }
};
function _sfc_render$q(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_tag = resolveComponent("el-tag");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        multiple: true,
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        "show-export": "",
        "export-method": $setup.exportMarketingSetup,
        "delete-method": $setup.deleteMethod,
        "show-delete-btn": ""
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("mod.maindata.code_name"),
            prop: "Name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.Name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.Name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"])
        ]),
        "col-isReal": withCtx(({ row }) => [
          parseInt(row.isReal) === 1 ? (openBlock(), createBlock(_component_el_tag, {
            key: 0,
            size: "small",
            type: "success",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode("\u662F")
            ]),
            _: 1
          })) : (openBlock(), createBlock(_component_el_tag, {
            key: 1,
            size: "small",
            type: "warning",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode("\u5426")
            ]),
            _: 1
          }))
        ]),
        "col-isSynchronizeCrm": withCtx(({ row }) => [
          parseInt(row.isSynchronizeCrm) === 1 ? (openBlock(), createBlock(_component_el_tag, {
            key: 0,
            size: "small",
            type: "success",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode("\u662F")
            ]),
            _: 1
          })) : (openBlock(), createBlock(_component_el_tag, {
            key: 1,
            size: "small",
            type: "warning",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode("\u5426")
            ]),
            _: 1
          }))
        ]),
        "col-isSynchronizeCrmDistributorStation": withCtx(({ row }) => [
          parseInt(row.isSynchronizeCrmDistributorStation) === 1 ? (openBlock(), createBlock(_component_el_tag, {
            key: 0,
            size: "small",
            type: "success",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode("\u662F")
            ]),
            _: 1
          })) : (openBlock(), createBlock(_component_el_tag, {
            key: 1,
            size: "small",
            type: "warning",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode("\u5426")
            ]),
            _: 1
          }))
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button_edit, {
            code: $setup.page.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: $setup.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.page.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: $setup.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method", "delete-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => _ctx.saveVisible = $event),
        selection: _ctx.selection,
        mode: _ctx.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "selection", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$6 = /* @__PURE__ */ _export_sfc(_sfc_main$q, [["render", _sfc_render$q]]);
const page$c = {
  "name": "maindata_marketingsetup",
  "icon": "captcha",
  "path": "/maindata/marketingsetup",
  "permissions": [
    "maindata_marketingsetup_query_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "maindata_marketingsetup_add",
      "permissions": [
        "maindata_marketingsetup_add_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "code": "maindata_marketingsetup_export",
      "permissions": [
        "maindata_marketingsetup_export_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "maindata_marketingsetup_edit",
      "permissions": [
        "maindata_marketingsetup_edit_get",
        "maindata_marketingsetup_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "maindata_marketingsetup_delete",
      "permissions": [
        "maindata_marketingsetup_delete_delete"
      ]
    }
  }
};
page$c.component = component$6;
component$6.name = page$c.name;
const name$6 = "maindata_org";
const icon$6 = "captcha";
const path$5 = "/maindata/org";
const permissions$5 = [
  "maindata_org_query_get",
  "maindata_org_tree_get"
];
const buttons$5 = {
  add: {
    text: "tpm.add",
    code: "maindata_org_add",
    permissions: [
      "maindata_org_add_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    code: "maindata_org_export",
    permissions: [
      "maindata_org_export_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "maindata_org_edit",
    permissions: [
      "maindata_org_edit_get",
      "maindata_org_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "maindata_org_delete",
    permissions: [
      "maindata_org_delete_delete"
    ]
  }
};
var page$b = {
  name: name$6,
  icon: icon$6,
  path: path$5,
  permissions: permissions$5,
  buttons: buttons$5
};
const _sfc_main$p = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t: $t2 } = tpm;
    const api2 = tpm.api.maindata.morg;
    const inputTableFilterRef = ref(null);
    const model = reactive({
      orgCode: "",
      orgName: "",
      parentId: "",
      parentLabel: "",
      invalidMapping: "",
      remark: "",
      startTime: "",
      endTime: "",
      type: "",
      enabled: true
    });
    const rules = computed(() => {
      return {
        orgCode: [{ required: true, message: $t2("\u8BF7\u8F93\u5165\u7EC4\u7EC7\u7F16\u7801") }],
        orgName: [{ required: true, message: $t2("\u8BF7\u8F93\u5165\u7EC4\u7EC7\u540D\u79F0") }],
        parentId: [{ required: true, message: $t2("\u7236\u7EA7Id\u4E0D\u80FD\u4E3A\u7A7A") }]
      };
    });
    const orgCodeCodeRef = ref(null);
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit, afterEdit: () => {
      inputTableFilterRef.value.queryList(true);
    } });
    bind.autoFocusRef = orgCodeCodeRef;
    bind.width = "700px";
    const parentQuery = (form) => {
      return new Promise(async (resolve) => {
        if (!model.type) {
          return "";
        }
        let level = model.type - 10;
        let params = {
          level,
          name: form.name
        };
        if (level >= 20) {
          params.level2Ids = form.divisionid;
        }
        if (level >= 30) {
          params.level2Ids = form.divisionid;
          params.level3Ids = form.marketingcenterid;
        }
        if (level >= 40) {
          params.level2Ids = form.divisionid;
          params.level3Ids = form.marketingcenterid;
          params.level4Ids = form.dutyregionid;
        }
        if (level >= 50) {
          params.level2Ids = form.divisionid;
          params.level3Ids = form.marketingcenterid;
          params.level4Ids = form.dutyregionid;
          params.level5Ids = form.departmentid;
        }
        let data = await api2.queryMorgSelect(params);
        resolve(data);
      });
    };
    const parentSelectQuery = () => {
      return new Promise(async (resolve) => {
        let level = model.type - 10;
        let params = {
          level,
          ids: [model.parentId]
        };
        let data = await api2.queryMorgSelect(params);
        resolve(data.rows);
      });
    };
    const handleClose = () => {
    };
    const handleOpened = () => {
      if (props.mode === "add") {
        model.parentId = props.selection.parentId;
        model.parentLabel = props.selection.parentLabel;
        model.type = props.selection.level + 10;
        inputTableFilterRef.value.queryList(true);
      }
    };
    const handleSuccess = () => {
      emit("success");
    };
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      orgCodeCodeRef,
      inputTableFilterRef,
      parentQuery,
      parentSelectQuery,
      handleSuccess,
      handleClose,
      handleOpened
    };
  }
};
function _sfc_render$p(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_m_maindata_input_table_filter = resolveComponent("m-maindata-input-table-filter");
  const _component_el_date_picker = resolveComponent("el-date-picker");
  const _component_el_switch = resolveComponent("el-switch");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, { "label-width": 140 }, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.org.org_code"),
                prop: "orgCode"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    ref: "orgCodeCodeRef",
                    modelValue: $setup.model.orgCode,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.orgCode = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.org.label"),
                prop: "orgName"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.orgName,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.orgName = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.org.parent"),
                prop: "parentId"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_input_table_filter, {
                    ref: "inputTableFilterRef",
                    modelValue: $setup.model.parentId,
                    "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.parentId = $event),
                    title: _ctx.$t("mod.maindata.org.placeholder_parent_id"),
                    disabled: _ctx.mode === `add`,
                    query: $setup.parentQuery,
                    "query-select": $setup.parentSelectQuery,
                    "select-end": ($setup.model.type - 10) / 10 - 1
                  }, null, 8, ["modelValue", "title", "disabled", "query", "query-select", "select-end"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.org.invalid_mapping"),
                prop: "invalidMapping"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.invalidMapping,
                    "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.invalidMapping = $event)
                  }, null, 8, ["modelValue"]),
                  createElementVNode("p", null, toDisplayString(_ctx.$t("mod.maindata.org.invalid_mapping_tip")), 1)
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.remark"),
                prop: "remark"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.remark,
                    "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.model.remark = $event),
                    type: "textarea"
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.start_time"),
                prop: "startTime"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_date_picker, {
                    modelValue: $setup.model.startTime,
                    "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.model.startTime = $event),
                    type: "date",
                    placeholder: _ctx.$t("mod.maindata.placeholder_start_time")
                  }, null, 8, ["modelValue", "placeholder"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.end_time"),
                prop: "endTime"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_date_picker, {
                    modelValue: $setup.model.endTime,
                    "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => $setup.model.endTime = $event),
                    type: "date",
                    placeholder: _ctx.$t("mod.maindata.placeholder_end_time")
                  }, null, 8, ["modelValue", "placeholder"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.enabled"),
                prop: "enabled"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_switch, {
                    modelValue: $setup.model.enabled,
                    "onUpdate:modelValue": _cache[7] || (_cache[7] = ($event) => $setup.model.enabled = $event),
                    "active-value": true,
                    "inactive-value": false
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save$5 = /* @__PURE__ */ _export_sfc(_sfc_main$p, [["render", _sfc_render$p]]);
var index_vue_vue_type_style_index_0_lang$3 = "";
var index_vue_vue_type_style_index_1_scoped_true_lang$2 = "";
const _sfc_main$o = {
  components: { Save: Save$5 },
  setup() {
    const treeRef = ref();
    const listRef = ref();
    const current = ref();
    const message = useMessage();
    const { tree, remove, exportOrg } = tpm.api.maindata.morg;
    const model = ref({
      Name: null
    });
    const state2 = reactive({
      mtree: [],
      leftWidth: "500px",
      treeFilterKey: "",
      defaultProps: {
        children: "children",
        label: "label",
        customNodeClass: "org-tree"
      },
      selection: { id: 0, parentId: "", parentLabel: "" },
      saveVisible: false,
      mode: "add",
      treeSelected: {},
      treeLoading: false,
      tableLoading: false,
      defaultExpandedKeys: []
    });
    const cols = [
      {
        prop: "label",
        label: "mod.maindata.org.label",
        width: 400,
        align: "left"
      },
      {
        prop: "id",
        label: "mod.maindata.org.id",
        show: true,
        width: 350
      }
    ];
    const refresh = () => {
      listRef.value.refresh();
    };
    const handleNodeClick = (v) => {
      if (v.level < 60 && v.level > 10) {
        state2.treeSelected = v;
        refresh();
      } else {
        message.error($t("mod.maindata.org.error_level"));
      }
    };
    const init = async () => {
      state2.treeLoading = true;
      state2.mtree = await tree({ level: 60 });
      state2.treeLoading = false;
      state2.defaultExpandedKeys = [state2.treeSelected.id || state2.mtree[0].id];
      nextTick(() => {
        if (state2.treeSelected.id) {
          const data = treeRef.value.getNode(state2.treeSelected.id);
          if (data) {
            state2.treeSelected = data.data;
            refresh();
          }
        }
      });
    };
    init();
    const queryChild = () => {
      return new Promise((resolve, reject) => {
        if (state2.treeSelected.id) {
          resolve({ rows: state2.treeSelected.children });
        } else {
          resolve({ rows: [] });
        }
      });
    };
    const add = () => {
      state2.mode = "add";
      state2.selection.id = "";
      state2.selection.parentId = state2.treeSelected.id;
      state2.selection.parentLabel = state2.treeSelected.label;
      state2.selection.level = state2.treeSelected.level;
      state2.saveVisible = true;
    };
    const addChildren = (row) => {
      state2.mode = "add";
      state2.selection.parentId = row.id;
      state2.selection.parentLabel = row.label;
      state2.selection.level = row.level;
      state2.saveVisible = true;
    };
    const edit = (row) => {
      state2.selection.id = row.id;
      state2.mode = "edit";
      state2.saveVisible = true;
    };
    const treeFilter = (value, data) => {
      if (!value) {
        return true;
      }
      return data.label.includes(value);
    };
    const treeChangeKeyword = (value) => {
      state2.treeFilterKey = value;
      treeRef.value.filter(value);
    };
    return {
      ...toRefs(state2),
      current,
      listRef,
      treeRef,
      refresh,
      page: page$b,
      model,
      queryChild,
      cols,
      init,
      add,
      addChildren,
      edit,
      treeFilter,
      treeChangeKeyword,
      remove,
      exportOrg,
      handleNodeClick
    };
  }
};
const _hoisted_1$1 = { class: "left-tree" };
const _hoisted_2 = { class: "custom-tree-node" };
const _hoisted_3 = { class: "right-table" };
function _sfc_render$o(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_button = resolveComponent("el-button");
  const _component_el_tree = resolveComponent("el-tree");
  const _component_m_box = resolveComponent("m-box");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_button = resolveComponent("m-button");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_list = resolveComponent("m-list");
  const _component_m_split = resolveComponent("m-split");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  const _directive_loading = resolveDirective("loading");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_split, {
        modelValue: _ctx.leftWidth,
        "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => _ctx.leftWidth = $event)
      }, {
        fixed: withCtx(() => [
          createElementVNode("div", _hoisted_1$1, [
            createVNode(_component_el_row, {
              class: "m-box-row",
              style: { "height": "100%" }
            }, {
              default: withCtx(() => [
                createVNode(_component_el_col, { span: 24 }, {
                  default: withCtx(() => [
                    createVNode(_component_m_box, {
                      header: "",
                      page: "",
                      title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
                      icon: "box"
                    }, {
                      default: withCtx(() => [
                        createVNode(_component_el_input, {
                          modelValue: _ctx.treeFilterKey,
                          "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => _ctx.treeFilterKey = $event),
                          class: "tree-search-input",
                          clearable: "",
                          placeholder: _ctx.$t("mod.maindata.org.placeholder_keyword"),
                          size: "default",
                          onChange: $setup.treeChangeKeyword,
                          onClear: $setup.treeChangeKeyword,
                          onInput: $setup.treeChangeKeyword
                        }, null, 8, ["modelValue", "placeholder", "onChange", "onClear", "onInput"]),
                        withDirectives((openBlock(), createBlock(_component_el_tree, {
                          ref: "treeRef",
                          "highlight-current": true,
                          data: _ctx.mtree,
                          props: _ctx.defaultProps,
                          "node-key": "id",
                          "filter-node-method": $setup.treeFilter,
                          "check-on-click-node": true,
                          "default-expanded-keys": _ctx.defaultExpandedKeys,
                          "expand-on-click-node": false,
                          onNodeClick: $setup.handleNodeClick
                        }, {
                          default: withCtx(({ node, data }) => [
                            createElementVNode("span", _hoisted_2, [
                              createElementVNode("span", null, toDisplayString(node.label), 1),
                              data.level < 60 ? (openBlock(), createBlock(_component_el_button, {
                                key: 0,
                                class: "btn",
                                type: "primary",
                                link: "",
                                onClick: withModifiers(($event) => $setup.addChildren(data), ["stop"])
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString(_ctx.$t("mod.maindata.org.add_children")), 1)
                                ]),
                                _: 2
                              }, 1032, ["onClick"])) : createCommentVNode("", true)
                            ])
                          ]),
                          _: 1
                        }, 8, ["data", "props", "filter-node-method", "default-expanded-keys", "onNodeClick"])), [
                          [_directive_loading, _ctx.treeLoading]
                        ])
                      ]),
                      _: 1
                    }, 8, ["title"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ])
        ]),
        auto: withCtx(() => [
          createElementVNode("div", _hoisted_3, [
            createVNode(_component_m_list, {
              ref: "listRef",
              "show-export": true,
              "show-search-btn": false,
              "export-method": $setup.exportOrg,
              "show-reset-btn": false,
              "query-model": $setup.model,
              "query-method": $setup.queryChild,
              title: _ctx.treeSelected.label || "\u8BF7\u9009\u62E9",
              icon: $setup.page.icon,
              cols: $setup.cols
            }, {
              operation: withCtx(({ row }) => [
                row.children ? (openBlock(), createBlock(_component_m_button, {
                  key: 0,
                  link: true,
                  text: true,
                  type: "primary",
                  icon: "plus",
                  onClick: ($event) => $setup.addChildren(row)
                }, {
                  default: withCtx(() => [
                    createTextVNode(toDisplayString(_ctx.$t("mod.maindata.org.add_children")), 1)
                  ]),
                  _: 2
                }, 1032, ["onClick"])) : createCommentVNode("", true),
                createVNode(_component_m_button_edit, {
                  code: $setup.page.buttons.edit.code,
                  onClick: ($event) => $setup.edit(row),
                  onSuccess: $setup.init
                }, null, 8, ["code", "onClick", "onSuccess"]),
                createVNode(_component_m_button_delete, {
                  code: $setup.page.buttons.remove.code,
                  action: $setup.remove,
                  data: row.id,
                  onSuccess: $setup.init
                }, null, 8, ["code", "action", "data", "onSuccess"])
              ]),
              buttons: withCtx(() => [
                _ctx.treeSelected.id ? (openBlock(), createBlock(_component_m_button_add, {
                  key: 0,
                  code: $setup.page.buttons.add.code,
                  onClick: $setup.add
                }, null, 8, ["code", "onClick"])) : createCommentVNode("", true)
              ]),
              _: 1
            }, 8, ["export-method", "query-model", "query-method", "title", "icon", "cols"])
          ])
        ]),
        _: 1
      }, 8, ["modelValue"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => _ctx.saveVisible = $event),
        selection: _ctx.selection,
        mode: _ctx.mode,
        onSuccess: $setup.init
      }, null, 8, ["id", "modelValue", "selection", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$5 = /* @__PURE__ */ _export_sfc(_sfc_main$o, [["render", _sfc_render$o], ["__scopeId", "data-v-99999702"]]);
const page$a = {
  "name": "maindata_org",
  "icon": "captcha",
  "path": "/maindata/org",
  "permissions": [
    "maindata_org_query_get",
    "maindata_org_tree_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "maindata_org_add",
      "permissions": [
        "maindata_org_add_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "code": "maindata_org_export",
      "permissions": [
        "maindata_org_export_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "maindata_org_edit",
      "permissions": [
        "maindata_org_edit_get",
        "maindata_org_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "maindata_org_delete",
      "permissions": [
        "maindata_org_delete_delete"
      ]
    }
  }
};
page$a.component = component$5;
component$5.name = page$a.name;
const name$5 = "maindata_product";
const icon$5 = "captcha";
const path$4 = "/maindata/product";
const permissions$4 = [
  "maindata_product_query_get"
];
const buttons$4 = {
  add: {
    text: "tpm.add",
    code: "maindata_product_add",
    permissions: [
      "maindata_product_add_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    code: "maindata_product_export",
    permissions: [
      "maindata_product_export_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "maindata_product_edit",
    permissions: [
      "maindata_product_edit_get",
      "maindata_product_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "maindata_product_delete",
    permissions: [
      "maindata_product_delete_delete"
    ]
  }
};
var page$9 = {
  name: name$5,
  icon: icon$5,
  path: path$4,
  permissions: permissions$4,
  buttons: buttons$4
};
const _sfc_main$n = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object,
      default: () => {
      }
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t: $t2 } = tpm;
    const api2 = tpm.api.maindata.mproduct;
    const model = reactive({
      productCode: "",
      productName: "",
      enabled: true
    });
    const rules = computed(() => {
      return {
        productCode: [{ required: true, message: $t2("mod.maindata.product.placeholder_code") }],
        productName: [{ required: true, message: $t2("mod.maindata.product.placeholder_name") }]
      };
    });
    const productCodeRef = ref(null);
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = productCodeRef;
    bind.width = "700px";
    const handleClose = () => {
    };
    const handleOpened = () => {
    };
    const handleSuccess = () => {
      emit("success");
    };
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      productCodeRef,
      handleSuccess,
      handleClose,
      handleOpened
    };
  }
};
function _sfc_render$n(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_switch = resolveComponent("el-switch");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, { "label-width": 140 }, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.product.code"),
                prop: "productCode"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    ref: "productCodeRef",
                    modelValue: $setup.model.productCode,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.productCode = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.product.name"),
                prop: "productName"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.productName,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.productName = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.enabled"),
                prop: "enabled"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_switch, {
                    modelValue: $setup.model.enabled,
                    "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.enabled = $event),
                    "active-value": true,
                    "inactive-value": false
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save$4 = /* @__PURE__ */ _export_sfc(_sfc_main$n, [["render", _sfc_render$n]]);
const _sfc_main$m = {
  components: { Save: Save$4 },
  setup() {
    const listRef = ref();
    const current = ref();
    const { query, exportProduct, remove, deleteSelected } = tpm.api.maindata.mproduct;
    const model = ref({
      Name: null
    });
    const cols = [
      {
        prop: "productCode",
        label: "mod.maindata.product.code",
        width: 150
      },
      {
        prop: "productName",
        label: "mod.maindata.product.name",
        width: 150
      },
      {
        prop: "bottleBox",
        label: "mod.maindata.product.bottle_box",
        width: 150
      },
      {
        prop: "capacity",
        label: "mod.maindata.product.capacity",
        width: 150
      },
      {
        prop: "className",
        label: "mod.maindata.product.class_name",
        width: 150
      },
      {
        prop: "volumeName",
        label: "mod.maindata.product.volume_name",
        width: 150
      },
      {
        prop: "brandName",
        label: "mod.maindata.product.brand_name",
        width: 150
      },
      {
        prop: "outPackName",
        label: "mod.maindata.product.out_pack_name",
        width: 150
      },
      {
        prop: "inPackName",
        label: "mod.maindata.product.in_pack_name",
        width: 150
      },
      {
        prop: "productType",
        label: "mod.maindata.product.product_type",
        width: 150
      },
      {
        prop: "productSpecName",
        label: "mod.maindata.product.product_spec_name",
        width: 150
      },
      {
        prop: "litreConversionRate",
        label: "mod.maindata.product.litre_conversion_rate",
        width: 150
      },
      {
        prop: "enabled",
        label: "mod.maindata.enabled",
        width: 150
      },
      {
        prop: "parentId",
        label: "mod.maindata.product.parent_id",
        width: 330
      },
      {
        prop: "groupCode",
        label: "mod.maindata.product.group_code",
        width: 150
      },
      {
        prop: "groupName",
        label: "mod.maindata.product.group_code",
        width: 150
      },
      {
        prop: "sort",
        label: "mod.maindata.sort",
        width: 150
      },
      {
        prop: "characterCode",
        label: "mod.maindata.product.character_code",
        width: 150
      },
      {
        prop: "remark",
        label: "mod.maindata.remark",
        width: 150
      },
      {
        prop: "creator",
        label: "mod.maindata.creator",
        width: 150
      },
      {
        prop: "createdTime",
        label: "mod.maindata.created_time",
        width: 180
      },
      {
        prop: "Modifier",
        label: "mod.maindata.modifier",
        width: 150
      },
      {
        prop: "ModifiedTime",
        label: "mod.maindata.modified_time",
        width: 180
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    const deleteMethod = (ids) => {
      return new Promise(async (resolve) => {
        resolve(await deleteSelected({ ids }));
      });
    };
    const mode = ref("");
    return {
      current,
      listRef,
      refresh,
      page: page$9,
      mode,
      model,
      cols,
      query,
      remove,
      exportProduct,
      ...list,
      deleteMethod
    };
  }
};
function _sfc_render$m(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        multiple: true,
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        "show-export": "",
        "export-method": $setup.exportProduct,
        "delete-method": $setup.deleteMethod,
        "show-delete-btn": ""
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("mod.maindata.code_name"),
            prop: "Name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.Name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.Name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"])
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button_edit, {
            code: $setup.page.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: $setup.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.page.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: $setup.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method", "delete-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => _ctx.saveVisible = $event),
        selection: _ctx.selection,
        mode: $setup.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "selection", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$4 = /* @__PURE__ */ _export_sfc(_sfc_main$m, [["render", _sfc_render$m]]);
const page$8 = {
  "name": "maindata_product",
  "icon": "captcha",
  "path": "/maindata/product",
  "permissions": [
    "maindata_product_query_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "maindata_product_add",
      "permissions": [
        "maindata_product_add_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "code": "maindata_product_export",
      "permissions": [
        "maindata_product_export_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "maindata_product_edit",
      "permissions": [
        "maindata_product_edit_get",
        "maindata_product_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "maindata_product_delete",
      "permissions": [
        "maindata_product_delete_delete"
      ]
    }
  }
};
page$8.component = component$4;
component$4.name = page$8.name;
const name$4 = "maindata_product_property";
const icon$4 = "captcha";
const path$3 = "/maindata/product_property";
const permissions$3 = [
  "maindata_product_property_query_get"
];
const buttons$3 = {
  add: {
    text: "tpm.add",
    code: "maindata_product_property_add",
    permissions: [
      "maindata_product_property_add_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    code: "maindata_product_property_export",
    permissions: [
      "maindata_product_property_export_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "maindata_product_property_edit",
    permissions: [
      "maindata_product_property_edit_get",
      "maindata_product_property_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "maindata_product_property_delete",
    permissions: [
      "maindata_product_property_delete_delete"
    ]
  }
};
var page$7 = {
  name: name$4,
  icon: icon$4,
  path: path$3,
  permissions: permissions$3,
  buttons: buttons$3
};
const _sfc_main$l = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object,
      default: () => {
      }
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t: $t2 } = tpm;
    const api2 = tpm.api.maindata.mproductproperty;
    const model = reactive({
      productPropertiesType: "",
      productPropertiesCode: "",
      productPropertiesName: "",
      sort: "999"
    });
    const rules = computed(() => {
      return {
        productPropertiesType: [{ required: true, message: $t2("mod.maindata.product_properties.placeholder_type") }],
        productPropertiesCode: [{ required: true, message: $t2("mod.maindata.product_properties.placeholder_code") }],
        productPropertiesName: [{ required: true, message: $t2("mod.maindata.product_properties.placeholder_name") }]
      };
    });
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit });
    bind.width = "700px";
    const handleClose = () => {
      model.value = {
        productPropertiesType: "",
        productPropertiesCode: "",
        productPropertiesName: "",
        sort: "999"
      };
    };
    const handleOpened = () => {
    };
    const handleSuccess = () => {
      emit("success");
    };
    const typeSelect = () => {
      return new Promise(async (resolve) => {
        const data = await api2.queryTypeSelect();
        resolve(data.rows);
      });
    };
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      api: api2,
      handleSuccess,
      handleClose,
      handleOpened,
      typeSelect
    };
  }
};
function _sfc_render$l(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_input = resolveComponent("el-input");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, { "label-width": 140 }, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.product_properties.type_name"),
                prop: "productPropertiesType"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_select, {
                    modelValue: $setup.model.productPropertiesType,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.productPropertiesType = $event),
                    action: $setup.typeSelect
                  }, null, 8, ["modelValue", "action"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.product_properties.code"),
                prop: "productPropertiesCode"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.productPropertiesCode,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.productPropertiesCode = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.product_properties.name"),
                prop: "productPropertiesName"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.productPropertiesName,
                    "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.productPropertiesName = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.sort"),
                prop: "sort"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.sort,
                    "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.sort = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save$3 = /* @__PURE__ */ _export_sfc(_sfc_main$l, [["render", _sfc_render$l]]);
const _sfc_main$k = {
  components: { Save: Save$3 },
  setup() {
    const listRef = ref();
    const current = ref();
    const { query, exportProductProperty, remove, deleteSelected } = tpm.api.maindata.mproductproperty;
    const model = ref({
      Name: null
    });
    const cols = [
      {
        prop: "productPropertiesTypeName",
        label: "mod.maindata.product_properties.type_name",
        width: 150
      },
      {
        prop: "productPropertiesCode",
        label: "mod.maindata.product_properties.code",
        width: 150
      },
      {
        prop: "productPropertiesName",
        label: "mod.maindata.product_properties.name",
        width: 150
      },
      {
        prop: "sort",
        label: "mod.maindata.sort",
        width: 150
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    const deleteMethod = (ids) => {
      return new Promise(async (resolve) => {
        resolve(await deleteSelected({ ids }));
      });
    };
    const mode = ref("");
    return {
      current,
      listRef,
      refresh,
      page: page$7,
      mode,
      model,
      cols,
      query,
      remove,
      exportProductProperty,
      ...list,
      deleteMethod
    };
  }
};
function _sfc_render$k(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        multiple: true,
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        "show-export": "",
        "export-method": $setup.exportProductProperty,
        "delete-method": $setup.deleteMethod,
        "show-delete-btn": ""
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("mod.maindata.code_name"),
            prop: "Name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.Name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.Name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"])
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button_edit, {
            code: $setup.page.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: $setup.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.page.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: $setup.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method", "delete-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => _ctx.saveVisible = $event),
        selection: _ctx.selection,
        mode: $setup.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "selection", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$3 = /* @__PURE__ */ _export_sfc(_sfc_main$k, [["render", _sfc_render$k]]);
const page$6 = {
  "name": "maindata_product_property",
  "icon": "captcha",
  "path": "/maindata/product_property",
  "permissions": [
    "maindata_product_property_query_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "maindata_product_property_add",
      "permissions": [
        "maindata_product_property_add_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "code": "maindata_product_property_export",
      "permissions": [
        "maindata_product_property_export_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "maindata_product_property_edit",
      "permissions": [
        "maindata_product_property_edit_get",
        "maindata_product_property_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "maindata_product_property_delete",
      "permissions": [
        "maindata_product_property_delete_delete"
      ]
    }
  }
};
page$6.component = component$3;
component$3.name = page$6.name;
const name$3 = "maindata_terminal";
const icon$3 = "captcha";
const path$2 = "/maindata/terminal";
const permissions$2 = [
  "maindata_terminal_query_get"
];
const buttons$2 = {
  add: {
    text: "tpm.add",
    code: "maindata_terminal_add",
    permissions: [
      "maindata_terminal_add_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    code: "maindata_terminal_export",
    permissions: [
      "maindata_terminal_export_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "maindata_terminal_edit",
    permissions: [
      "maindata_terminal_edit_get",
      "maindata_terminal_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "maindata_terminal_delete",
    permissions: [
      "maindata_terminal_delete_delete",
      "maindata_terminal_delete_selected_dellete"
    ]
  }
};
var page$5 = {
  name: name$3,
  icon: icon$3,
  path: path$2,
  permissions: permissions$2,
  buttons: buttons$2
};
const _sfc_main$j = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object,
      default: () => {
      }
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t: $t2 } = tpm;
    const api2 = tpm.api.maindata.mterminal;
    const { queryMorgSelect } = tpm.api.maindata.morg;
    const model = reactive({
      terminalCode: "",
      terminalName: "",
      distributorId: "",
      stationId: ""
    });
    const rules = computed(() => {
      return {
        terminalCode: [{ required: true, message: $t2("mod.maindata.terminal.placeholder_terminal") }],
        terminalName: [{ required: true, message: $t2("mod.maindata.terminal.placeholder_terminal_name") }],
        distributorId: [{ required: true, message: $t2("mod.maindata.terminal.placeholder_distributor") }],
        stationId: [{ required: true, message: $t2("mod.maindata.terminal.placeholder_station_id") }]
      };
    });
    const terminalIdRef = ref(null);
    const inputTableFilterRef = ref(null);
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit, afterEdit: () => {
      inputTableFilterRef.value.queryList(true);
    } });
    bind.autoFocusRef = terminalIdRef;
    bind.width = "700px";
    const handleClose = () => {
    };
    const handleOpened = () => {
    };
    const handleSuccess = () => {
      emit("success");
    };
    const parentQuery = (form, pages2) => {
      return new Promise(async (resolve) => {
        let level = 60;
        let params = {
          level,
          name: form.name,
          page: {
            index: pages2.index,
            size: pages2.size
          }
        };
        {
          params.level2Ids = form.divisionid;
        }
        {
          params.level2Ids = form.divisionid;
          params.level3Ids = form.marketingcenterid;
        }
        {
          params.level2Ids = form.divisionid;
          params.level3Ids = form.marketingcenterid;
          params.level4Ids = form.dutyregionid;
        }
        {
          params.level2Ids = form.divisionid;
          params.level3Ids = form.marketingcenterid;
          params.level4Ids = form.dutyregionid;
          params.level5Ids = form.departmentid;
        }
        let data = await queryMorgSelect(params);
        resolve(data);
      });
    };
    const parentSelectQuery = () => {
      return new Promise(async (resolve) => {
        let level = 60;
        let params = {
          level,
          ids: [model.stationId]
        };
        let data = await queryMorgSelect(params);
        resolve(data.rows);
      });
    };
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      inputTableFilterRef,
      terminalIdRef,
      handleSuccess,
      handleClose,
      handleOpened,
      parentQuery,
      parentSelectQuery
    };
  }
};
function _sfc_render$j(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_m_maindata_input_table_filter = resolveComponent("m-maindata-input-table-filter");
  const _component_el_switch = resolveComponent("el-switch");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, { "label-width": 140 }, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.terminal.code"),
                prop: "terminalCode"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    ref: "terminalCodeRef",
                    modelValue: $setup.model.terminalCode,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.terminalCode = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.terminal.name"),
                prop: "terminalName"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.terminalName,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.terminalName = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.org_level_name.station"),
                prop: "stationId"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_input_table_filter, {
                    ref: "inputTableFilterRef",
                    modelValue: $setup.model.stationId,
                    "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.stationId = $event),
                    title: _ctx.$t("mod.maindata.terminal.placeholder_station_id"),
                    query: $setup.parentQuery,
                    "query-select": $setup.parentSelectQuery,
                    "select-end": 5
                  }, null, 8, ["modelValue", "title", "query", "query-select"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.terminal.sale_line"),
                prop: "saleLine"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.saleLine,
                    "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.saleLine = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.terminal.level_1_type"),
                prop: "lvl1Type"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.lvl1Type,
                    "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.model.lvl1Type = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.terminal.level_2_type"),
                prop: "lvl2Type"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.lvl2Type,
                    "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.model.lvl2Type = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.terminal.level_3_type"),
                prop: "lvl3Type"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.lvl3Type,
                    "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => $setup.model.lvl3Type = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.address"),
                prop: "address"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.address,
                    "onUpdate:modelValue": _cache[7] || (_cache[7] = ($event) => $setup.model.address = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.status"),
                prop: "status"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_switch, {
                    modelValue: $setup.model.status,
                    "onUpdate:modelValue": _cache[8] || (_cache[8] = ($event) => $setup.model.status = $event),
                    "active-value": true,
                    "inactive-value": false
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save$2 = /* @__PURE__ */ _export_sfc(_sfc_main$j, [["render", _sfc_render$j]]);
const _sfc_main$i = {
  components: { Save: Save$2 },
  setup() {
    const listRef = ref();
    const current = ref();
    const { query, exportTermainal, remove, deleteSelected } = tpm.api.maindata.mterminal;
    const model = ref({
      Name: null
    });
    const cols = [
      {
        prop: "terminalCode",
        label: "mod.maindata.terminal.code",
        width: 150
      },
      {
        prop: "terminalName",
        label: "mod.maindata.terminal.name",
        width: 200
      },
      {
        prop: "marketingName",
        label: "mod.maindata.org_level_name.marketing_center",
        width: 150
      },
      {
        prop: "dutyregionName",
        label: "mod.maindata.org_level_name.dutyregion",
        width: 150
      },
      {
        prop: "departmentName",
        label: "mod.maindata.org_level_name.department",
        width: 150
      },
      {
        prop: "stationName",
        label: "mod.maindata.org_level_name.station",
        width: 150
      },
      {
        prop: "saleLine",
        label: "mod.maindata.terminal.sale_line",
        width: 200
      },
      {
        prop: "Lvl1Type",
        label: "mod.maindata.terminal.level_1_type",
        width: 200
      },
      {
        prop: "Lvl2Type",
        label: "mod.maindata.terminal.level_2_type",
        width: 200
      },
      {
        prop: "Lvl3Type",
        label: "mod.maindata.terminal.level_3_type",
        width: 200
      },
      {
        prop: "status",
        label: "mod.maindata.status",
        width: 200
      },
      {
        prop: "address",
        label: "mod.maindata.address",
        width: 200
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    const deleteMethod = (ids) => {
      return new Promise(async (resolve) => {
        resolve(await deleteSelected({ ids }));
      });
    };
    const mode = ref("");
    return {
      current,
      listRef,
      refresh,
      page: page$5,
      mode,
      model,
      cols,
      query,
      remove,
      exportTermainal,
      ...list,
      deleteMethod
    };
  }
};
function _sfc_render$i(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_maindata_query_org_group = resolveComponent("m-maindata-query-org-group");
  const _component_el_tag = resolveComponent("el-tag");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        multiple: true,
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        "show-export": "",
        "export-method": $setup.exportTermainal,
        "delete-method": $setup.deleteMethod,
        "show-delete-btn": ""
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("mod.maindata.code_name"),
            prop: "Name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.Name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.Name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_m_maindata_query_org_group, {
            divisionIds: $setup.model.divisionIds,
            "onUpdate:divisionIds": _cache[1] || (_cache[1] = ($event) => $setup.model.divisionIds = $event),
            marketingIds: $setup.model.marketingIds,
            "onUpdate:marketingIds": _cache[2] || (_cache[2] = ($event) => $setup.model.marketingIds = $event),
            dutyregionIds: $setup.model.dutyregionIds,
            "onUpdate:dutyregionIds": _cache[3] || (_cache[3] = ($event) => $setup.model.dutyregionIds = $event),
            departmentIds: $setup.model.departmentIds,
            "onUpdate:departmentIds": _cache[4] || (_cache[4] = ($event) => $setup.model.departmentIds = $event),
            stationIds: $setup.model.stationIds,
            "onUpdate:stationIds": _cache[5] || (_cache[5] = ($event) => $setup.model.stationIds = $event),
            "select-start": 3,
            "select-end": 6
          }, null, 8, ["divisionIds", "marketingIds", "dutyregionIds", "departmentIds", "stationIds"])
        ]),
        "col-status": withCtx(({ row }) => [
          parseInt(row.status) === 0 ? (openBlock(), createBlock(_component_el_tag, {
            key: 0,
            type: "info",
            size: "small",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.miandata.enabled")), 1)
            ]),
            _: 1
          })) : (openBlock(), createBlock(_component_el_tag, {
            key: 1,
            type: "warning",
            size: "small",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.maindata.disabled")), 1)
            ]),
            _: 1
          }))
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button_edit, {
            code: $setup.page.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: $setup.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.page.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: $setup.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method", "delete-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => _ctx.saveVisible = $event),
        selection: _ctx.selection,
        mode: $setup.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "selection", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$2 = /* @__PURE__ */ _export_sfc(_sfc_main$i, [["render", _sfc_render$i]]);
const page$4 = {
  "name": "maindata_terminal",
  "icon": "captcha",
  "path": "/maindata/terminal",
  "permissions": [
    "maindata_terminal_query_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "maindata_terminal_add",
      "permissions": [
        "maindata_terminal_add_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "code": "maindata_terminal_export",
      "permissions": [
        "maindata_terminal_export_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "maindata_terminal_edit",
      "permissions": [
        "maindata_terminal_edit_get",
        "maindata_terminal_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "maindata_terminal_delete",
      "permissions": [
        "maindata_terminal_delete_delete",
        "maindata_terminal_delete_selected_dellete"
      ]
    }
  }
};
page$4.component = component$2;
component$2.name = page$4.name;
const name$2 = "maindata_terminal_distributor";
const icon$2 = "captcha";
const path$1 = "/maindata/terminal_distributor";
const permissions$1 = [
  "maindata_terminal_distributor_query_get"
];
const buttons$1 = {
  add: {
    text: "tpm.add",
    code: "maindata_terminal_distributor_add",
    permissions: [
      "maindata_terminal_distributor_add_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    code: "maindata_terminal_distributor_export",
    permissions: [
      "maindata_terminal_distributor_export_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "maindata_terminal_distributor_edit",
    permissions: [
      "maindata_terminal_distributor_edit_get",
      "maindata_terminal_distributor_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "maindata_terminal_distributor_delete",
    permissions: [
      "maindata_terminal_distributor_delete_delete",
      "maindata_terminal_delete_selected_dellete"
    ]
  }
};
var page$3 = {
  name: name$2,
  icon: icon$2,
  path: path$1,
  permissions: permissions$1,
  buttons: buttons$1
};
const _sfc_main$h = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object,
      default: () => {
      }
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t: $t2 } = tpm;
    const api2 = tpm.api.maindata.mterminaldistributor;
    const { queryDistributorSelect } = tpm.api.maindata.mdistributor;
    const { queryTerminalSelect } = tpm.api.maindata.mterminal;
    const model = reactive({
      terminalId: "",
      distributorId: ""
    });
    const rules = computed(() => {
      return {
        terminalId: [{ required: true, message: $t2("mod.maindata.terminal.placeholder_select_terminal") }],
        distributorId: [{ required: true, message: $t2("mod.maindata.terminal.placeholder_distributor") }]
      };
    });
    const terminalIdRef = ref(null);
    const distributorIdRef = ref(null);
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit, afterEdit: () => {
      distributorIdRef.value.remoteMethod("", true);
      terminalIdRef.value.remoteMethod("", true);
    } });
    bind.width = "700px";
    const handleClose = () => {
    };
    const handleOpened = () => {
    };
    const handleSuccess = () => {
      emit("success");
    };
    const queryCustomerAction = (pages2, keyword, getValues) => {
      return new Promise(async (resolve) => {
        let params = {
          page: { index: pages2.index, size: pages2.size },
          name: keyword,
          type: 1
        };
        if (getValues && model.distributorId) {
          params.ids = [model.distributorId];
        }
        const data = await queryDistributorSelect(params);
        resolve(data);
      });
    };
    const queryTerminalSelectAction = (pages2, keyword, getValues) => {
      return new Promise(async (resolve) => {
        let params = {
          page: { index: pages2.index, size: pages2.size },
          name: keyword
        };
        if (getValues && model.terminalId) {
          params.ids = [model.terminalId];
        }
        const data = await queryTerminalSelect(params);
        resolve(data);
      });
    };
    if (props.mode === "add") {
      distributorIdRef.value.remoteMethod("");
      terminalIdRef.value.remoteMethod("");
    }
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      terminalIdRef,
      distributorIdRef,
      queryTerminalSelectAction,
      queryCustomerAction,
      handleSuccess,
      handleClose,
      handleOpened
    };
  }
};
function _sfc_render$h(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_maindata_select_page = resolveComponent("m-maindata-select-page");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, { "label-width": 140 }, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.terminal.label"),
                prop: "terminalId"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_select_page, {
                    ref: "terminalIdRef",
                    modelValue: $setup.model.terminalId,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.terminalId = $event),
                    action: $setup.queryTerminalSelectAction
                  }, null, 8, ["modelValue", "action"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.distributor.type_list.type_1"),
                prop: "distributorId"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_select_page, {
                    ref: "distributorIdRef",
                    modelValue: $setup.model.distributorId,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.distributorId = $event),
                    action: $setup.queryCustomerAction
                  }, null, 8, ["modelValue", "action"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save$1 = /* @__PURE__ */ _export_sfc(_sfc_main$h, [["render", _sfc_render$h]]);
const _sfc_main$g = {
  components: { Save: Save$1 },
  setup() {
    const listRef = ref();
    const current = ref();
    const { query, exportTerminalDistributor, remove, deleteSelected } = tpm.api.maindata.mterminaldistributor;
    const model = ref({
      Name: null
    });
    const cols = [
      {
        prop: "terminalCode",
        label: "mod.maindata.terminal.code",
        width: 150
      },
      {
        prop: "terminalName",
        label: "mod.maindata.terminal.name",
        width: 200
      },
      {
        prop: "distributorCode",
        label: "mod.maindata.distributor.code",
        width: 150
      },
      {
        prop: "distributorName",
        label: "mod.maindata.distributor.type_list.type_1",
        width: 200
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    const mode = ref("");
    const deleteMethod = (ids) => {
      return new Promise(async (resolve) => {
        resolve(await deleteSelected({ ids }));
      });
    };
    return {
      current,
      listRef,
      refresh,
      page: page$3,
      mode,
      model,
      cols,
      query,
      remove,
      exportTerminalDistributor,
      ...list,
      deleteMethod
    };
  }
};
function _sfc_render$g(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_maindata_query_org_group = resolveComponent("m-maindata-query-org-group");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        multiple: true,
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        "show-export": "",
        "export-method": $setup.exportTerminalDistributor,
        "delete-method": $setup.deleteMethod,
        "show-delete-btn": ""
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("mod.maindata.code_name"),
            prop: "Name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.Name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.Name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_m_maindata_query_org_group, {
            divisionIds: $setup.model.divisionIds,
            "onUpdate:divisionIds": _cache[1] || (_cache[1] = ($event) => $setup.model.divisionIds = $event),
            marketingIds: $setup.model.marketingIds,
            "onUpdate:marketingIds": _cache[2] || (_cache[2] = ($event) => $setup.model.marketingIds = $event),
            dutyregionIds: $setup.model.dutyregionIds,
            "onUpdate:dutyregionIds": _cache[3] || (_cache[3] = ($event) => $setup.model.dutyregionIds = $event),
            departmentIds: $setup.model.departmentIds,
            "onUpdate:departmentIds": _cache[4] || (_cache[4] = ($event) => $setup.model.departmentIds = $event),
            stationIds: $setup.model.stationIds,
            "onUpdate:stationIds": _cache[5] || (_cache[5] = ($event) => $setup.model.stationIds = $event),
            "select-start": 3,
            "select-end": 6
          }, null, 8, ["divisionIds", "marketingIds", "dutyregionIds", "departmentIds", "stationIds"])
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button_edit, {
            code: $setup.page.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: $setup.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.page.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: $setup.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method", "delete-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => _ctx.saveVisible = $event),
        selection: _ctx.selection,
        mode: $setup.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "selection", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$1 = /* @__PURE__ */ _export_sfc(_sfc_main$g, [["render", _sfc_render$g]]);
const page$2 = {
  "name": "maindata_terminal_distributor",
  "icon": "captcha",
  "path": "/maindata/terminal_distributor",
  "permissions": [
    "maindata_terminal_distributor_query_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "maindata_terminal_distributor_add",
      "permissions": [
        "maindata_terminal_distributor_add_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "code": "maindata_terminal_distributor_export",
      "permissions": [
        "maindata_terminal_distributor_export_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "maindata_terminal_distributor_edit",
      "permissions": [
        "maindata_terminal_distributor_edit_get",
        "maindata_terminal_distributor_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "maindata_terminal_distributor_delete",
      "permissions": [
        "maindata_terminal_distributor_delete_delete",
        "maindata_terminal_delete_selected_dellete"
      ]
    }
  }
};
page$2.component = component$1;
component$1.name = page$2.name;
const name$1 = "maindata_terminal_user";
const icon$1 = "captcha";
const path = "/maindata/terminal_user";
const permissions = [
  "maindata_terminal_user_query_get"
];
const buttons = {
  add: {
    text: "tpm.add",
    code: "maindata_terminal_user_add",
    permissions: [
      "maindata_terminal_user_add_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    code: "maindata_terminal_user_export",
    permissions: [
      "maindata_terminal_user_export_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "maindata_terminal_user_edit",
    permissions: [
      "maindata_terminal_user_edit_get",
      "maindata_terminal_user_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "maindata_terminal_user_delete",
    permissions: [
      "maindata_terminal_user_delete_delete"
    ]
  }
};
var page$1 = {
  name: name$1,
  icon: icon$1,
  path,
  permissions,
  buttons
};
const _sfc_main$f = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object,
      default: () => {
      }
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t: $t2 } = tpm;
    const api2 = tpm.api.maindata.mterminaluser;
    const { queryTerminalSelect } = tpm.api.maindata.mterminal;
    const { queryAccountSelect } = tpm.api.admin.account;
    const model = reactive({
      terminalId: "",
      accountId: ""
    });
    const rules = computed(() => {
      return {
        terminalId: [{ required: true, message: $t2("mod.maindata.terminal.placeholder_select_terminal") }],
        accountId: [{ required: true, message: $t2("mod.maindata.terminal.placeholder_account") }]
      };
    });
    const terminalIdRef = ref(null);
    const accountIdRef = ref(null);
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit, afterEdit: () => {
      accountIdRef.value.remoteMethod("", true);
      terminalIdRef.value.remoteMethod("", true);
    } });
    bind.width = "700px";
    const handleClose = () => {
    };
    const handleOpened = () => {
    };
    const handleSuccess = () => {
      emit("success");
    };
    const queryAccountAction = (pages2, keyword, getValues) => {
      return new Promise(async (resolve) => {
        let params = {
          page: { index: pages2.index, size: pages2.size },
          name: keyword
        };
        if (getValues && model.accountId) {
          params.ids = [model.accountId];
        }
        const data = await queryAccountSelect(params);
        resolve(data);
      });
    };
    const queryTerminalSelectAction = (pages2, keyword, getValues) => {
      return new Promise(async (resolve) => {
        let params = {
          page: { index: pages2.index, size: pages2.size },
          name: keyword
        };
        if (getValues && model.terminalId) {
          params.ids = [model.terminalId];
        }
        const data = await queryTerminalSelect(params);
        resolve(data);
      });
    };
    if (props.mode === "add") {
      accountIdRef.value.remoteMethod("");
      terminalIdRef.value.remoteMethod("");
    }
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      terminalIdRef,
      accountIdRef,
      queryTerminalSelectAction,
      queryAccountAction,
      handleSuccess,
      handleClose,
      handleOpened
    };
  }
};
function _sfc_render$f(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_maindata_select_page = resolveComponent("m-maindata-select-page");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, { "label-width": 140 }, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.terminal.label"),
                prop: "terminalId"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_select_page, {
                    ref: "terminalIdRef",
                    modelValue: $setup.model.terminalId,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.terminalId = $event),
                    action: $setup.queryTerminalSelectAction
                  }, null, 8, ["modelValue", "action"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.maindata.terminal.account_name"),
                prop: "accountId"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_maindata_select_page, {
                    ref: "accountIdRef",
                    modelValue: $setup.model.accountId,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.accountId = $event),
                    action: $setup.queryAccountAction
                  }, null, 8, ["modelValue", "action"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save = /* @__PURE__ */ _export_sfc(_sfc_main$f, [["render", _sfc_render$f]]);
const _sfc_main$e = {
  components: { Save },
  setup() {
    const listRef = ref();
    const current = ref();
    const { query, exportTerminalUser, remove, deleteSelected } = tpm.api.maindata.mterminaluser;
    const model = ref({
      Name: null
    });
    const cols = [
      {
        prop: "terminalCode",
        label: "mod.maindata.terminal.code",
        width: 150
      },
      {
        prop: "terminalName",
        label: "mod.maindata.terminal.name",
        width: 200
      },
      {
        prop: "accountName",
        label: "mod.maindata.terminal.account_name",
        width: 150
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    const deleteMethod = (ids) => {
      return new Promise(async (resolve) => {
        resolve(await deleteSelected({ ids }));
      });
    };
    const mode = ref("");
    return {
      current,
      listRef,
      refresh,
      page: page$1,
      mode,
      model,
      cols,
      query,
      remove,
      exportTerminalUser,
      ...list,
      deleteMethod
    };
  }
};
function _sfc_render$e(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_maindata_query_org_group = resolveComponent("m-maindata-query-org-group");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        multiple: true,
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        "show-export": "",
        "export-method": $setup.exportTerminalUser,
        "delete-method": $setup.deleteMethod,
        "show-delete-btn": ""
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("mod.maindata.code_name"),
            prop: "Name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.Name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.Name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_m_maindata_query_org_group, {
            divisionIds: $setup.model.divisionIds,
            "onUpdate:divisionIds": _cache[1] || (_cache[1] = ($event) => $setup.model.divisionIds = $event),
            marketingIds: $setup.model.marketingIds,
            "onUpdate:marketingIds": _cache[2] || (_cache[2] = ($event) => $setup.model.marketingIds = $event),
            dutyregionIds: $setup.model.dutyregionIds,
            "onUpdate:dutyregionIds": _cache[3] || (_cache[3] = ($event) => $setup.model.dutyregionIds = $event),
            departmentIds: $setup.model.departmentIds,
            "onUpdate:departmentIds": _cache[4] || (_cache[4] = ($event) => $setup.model.departmentIds = $event),
            stationIds: $setup.model.stationIds,
            "onUpdate:stationIds": _cache[5] || (_cache[5] = ($event) => $setup.model.stationIds = $event),
            "select-start": 3,
            "select-end": 6
          }, null, 8, ["divisionIds", "marketingIds", "dutyregionIds", "departmentIds", "stationIds"])
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button_edit, {
            code: $setup.page.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: $setup.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.page.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: $setup.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method", "delete-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => _ctx.saveVisible = $event),
        selection: _ctx.selection,
        mode: $setup.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "selection", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component = /* @__PURE__ */ _export_sfc(_sfc_main$e, [["render", _sfc_render$e]]);
const page = {
  "name": "maindata_terminal_user",
  "icon": "captcha",
  "path": "/maindata/terminal_user",
  "permissions": [
    "maindata_terminal_user_query_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "maindata_terminal_user_add",
      "permissions": [
        "maindata_terminal_user_add_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "code": "maindata_terminal_user_export",
      "permissions": [
        "maindata_terminal_user_export_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "maindata_terminal_user_edit",
      "permissions": [
        "maindata_terminal_user_edit_get",
        "maindata_terminal_user_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "maindata_terminal_user_delete",
      "permissions": [
        "maindata_terminal_user_delete_delete"
      ]
    }
  }
};
page.component = component;
component.name = page.name;
const id = 2;
const name = "tpm-mod-maindata";
const label = "TPM\u4E3B\u6570\u636E\u6A21\u5757";
const version = "1.0.3";
const icon = "lock";
const description = "CRB.TPM\u4E3B\u6570\u636E\u6A21\u5757";
const main = "lib/index.es.js";
const scripts = {
  dev: "vite --host --config=./build/app.config.js",
  serve: "vite preview",
  build: "vite build --config=./build/app.config.js",
  lib: "rimraf lib && vite build --config=./build/lib.config.js && npm run locale",
  locale: "rollup -c node_modules/tpm-ui/build/locales.config.js",
  clean: "rimraf lib && rimraf public && rimraf dist",
  cm: "rimraf package-lock.json && rimraf node_modules",
  cv: "rimraf node_modules/.vite"
};
const dependencies = {
  "element-plus": "^2.2.5",
  "js-base64": "^3.7.2",
  "tpm-mod-admin": "^1.0.6",
  "tpm-ui": "^1.1.8"
};
const devDependencies = {
  "@rollup/plugin-image": "^2.1.1",
  "@rollup/pluginutils": "^4.1.1",
  "@vitejs/plugin-vue": "^2.0.0",
  ejs: "^3.1.6",
  eslint: "^7.32.0",
  "eslint-config-prettier": "^8.1.0",
  "eslint-plugin-vue": "^7.20.0",
  "html-minifier-terser": "^5.1.1",
  rimraf: "^3.0.2",
  sass: "^1.43.3",
  vite: "^2.7.13"
};
const files = [
  "lib"
];
const publishConfig = {
  registry: "https://registry.npmjs.org/"
};
var _package = {
  id,
  name,
  label,
  version,
  icon,
  description,
  main,
  scripts,
  dependencies,
  devDependencies,
  files,
  publishConfig
};
var __glob_0_0 = /* @__PURE__ */ Object.freeze(/* @__PURE__ */ Object.defineProperty({
  __proto__: null,
  id,
  name,
  label,
  version,
  icon,
  description,
  main,
  scripts,
  dependencies,
  devDependencies,
  files,
  publishConfig,
  "default": _package
}, Symbol.toStringTag, { value: "Module" }));
const packs = { "../package.json": __glob_0_0 };
let pack = {};
Object.keys(packs).forEach((item) => {
  pack = packs[item];
});
var module = {
  id: pack.id,
  name: pack.label,
  code: "maindata",
  version: pack.version,
  description: pack.description
};
const _sfc_main$d = {
  components: {},
  setup() {
    const { edit, update } = tpm.api.admin.config;
    const formRef = ref(null);
    const disabled = ref(false);
    const bind = reactive({
      header: false,
      page: false,
      labelWidth: "150px"
    });
    const message = useMessage();
    const model = reactive({
      type: 1,
      code: module.code,
      sapConnection: "",
      syncCRMDtAndTmnFunctionName: "",
      SapWsurl: ""
    });
    const edits = () => {
      edit({
        type: model.type,
        code: model.code
      }).then((data) => {
        const res = JSON.parse(data);
        model.sapConnection = res.sapConnection;
        model.syncCRMDtAndTmnFunctionName = res.syncCRMDtAndTmnFunctionName;
        model.SapWsurl = res.SapWsurl;
        console.log("model", model);
      });
    };
    const rules = {
      sapConnection: [{ required: true, message: "\u8BF7\u8F93\u5165SAP\u94FE\u63A5\u5B57\u7B26\u4E32" }],
      syncCRMDtAndTmnFunctionName: [{ required: true, message: "\u8BF7\u8F93\u5165\u540C\u6B65CRM\u7ECF\u9500\u5546\u3001\u7EC8\u7AEF\u51FD\u6570\u540D" }],
      SapWsurl: [{ required: true, message: "\u8BF7\u8F93\u5165SAP webservice \u63A5\u53E3\u5730\u5740" }]
    };
    const action = () => {
      return update({
        type: model.type,
        code: model.code,
        json: JSON.stringify(model)
      });
    };
    const handleSuccess = () => {
      message.success("\u606D\u559C\uFF0C\u914D\u7F6E\u4FDD\u5B58\u6210\u529F");
    };
    edits();
    return {
      ...toRefs(model),
      formRef,
      bind,
      model,
      edits,
      rules,
      action,
      disabled,
      handleSuccess
    };
  }
};
const _hoisted_1 = { style: { "padding": "50px" } };
function _sfc_render$d(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_switch = resolveComponent("el-switch");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_input = resolveComponent("el-input");
  const _component_m_button = resolveComponent("m-button");
  const _component_m_form = resolveComponent("m-form");
  return openBlock(), createElementBlock("div", _hoisted_1, [
    createVNode(_component_m_form, {
      ref: "formRef",
      action: $setup.action,
      model: $setup.model,
      rules: $setup.rules,
      disabled: $setup.disabled,
      onSuccess: $setup.handleSuccess
    }, {
      default: withCtx(() => [
        createVNode(_component_el_form_item, {
          "label-width": "300px",
          label: "SAP\u94FE\u63A5\u5B57\u7B26\u4E32",
          prop: "sapConnection"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_switch, {
              modelValue: $setup.model.loginLog,
              "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.loginLog = $event)
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }),
        createVNode(_component_el_form_item, {
          "label-width": "300px",
          label: "\u540C\u6B65CRM\u7ECF\u9500\u5546\u3001\u7EC8\u7AEF\u51FD\u6570\u540D",
          prop: "syncCRMDtAndTmnFunctionName"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_input, {
              modelValue: $setup.model.defaultPassword,
              "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.defaultPassword = $event)
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }),
        createVNode(_component_el_form_item, {
          "label-width": "300px",
          label: "SAP webservice \u63A5\u53E3\u5730\u5740",
          prop: "SapWsurl"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_input, {
              modelValue: $setup.model.defaultPassword,
              "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.defaultPassword = $event)
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }),
        createVNode(_component_el_form_item, { "label-width": "300px" }, {
          default: withCtx(() => [
            createVNode(_component_m_button, {
              type: "primary",
              onClick: _cache[3] || (_cache[3] = () => $setup.formRef.submit()),
              icon: "save"
            }, {
              default: withCtx(() => [
                createTextVNode("\u4FDD\u5B58")
              ]),
              _: 1
            }),
            createVNode(_component_m_button, {
              type: "info",
              onClick: _cache[4] || (_cache[4] = () => $setup.formRef.reset()),
              icon: "reset"
            }, {
              default: withCtx(() => [
                createTextVNode("\u91CD\u7F6E")
              ]),
              _: 1
            })
          ]),
          _: 1
        })
      ]),
      _: 1
    }, 8, ["action", "model", "rules", "disabled", "onSuccess"])
  ]);
}
var component_0 = /* @__PURE__ */ _export_sfc(_sfc_main$d, [["render", _sfc_render$d]]);
const _sfc_main$c = {
  props: {
    group: {
      type: String,
      required: true
    },
    dict: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const query = () => {
      const { group, dict } = props;
      return tpm.api.admin.dict.cascader({ groupCode: group, dictCode: dict });
    };
    return {
      query
    };
  }
};
function _sfc_render$c(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_cascader = resolveComponent("m-cascader");
  return openBlock(), createBlock(_component_m_cascader, { action: $setup.query }, null, 8, ["action"]);
}
var component_1 = /* @__PURE__ */ _export_sfc(_sfc_main$c, [["render", _sfc_render$c]]);
const _sfc_main$b = {
  props: {
    group: {
      type: String,
      required: true
    },
    dict: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const query = () => {
      const { group, dict } = props;
      return tpm.api.admin.dict.select({ groupCode: group, dictCode: dict });
    };
    return {
      query
    };
  }
};
function _sfc_render$b(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, { action: $setup.query }, null, 8, ["action"]);
}
var component_2 = /* @__PURE__ */ _export_sfc(_sfc_main$b, [["render", _sfc_render$b]]);
var index_vue_vue_type_style_index_0_lang$2 = "";
const _sfc_main$a = {
  setup() {
    const isFullscreen = ref(false);
    const handleClick = () => {
      isFullscreen.value = !isFullscreen.value;
      let el = document.querySelector(".m-admin-dict-extend");
      if (isFullscreen.value) {
        dom.addClass(el, "fullscreen");
      } else {
        dom.removeClass(el, "fullscreen");
      }
    };
    return {
      isFullscreen,
      handleClick
    };
  }
};
function _sfc_render$a(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_button = resolveComponent("m-button");
  return openBlock(), createBlock(_component_m_button, {
    icon: $setup.isFullscreen ? "full-screen-exit" : "full-screen",
    onClick: $setup.handleClick
  }, null, 8, ["icon", "onClick"]);
}
var component_3 = /* @__PURE__ */ _export_sfc(_sfc_main$a, [["render", _sfc_render$a]]);
const _sfc_main$9 = {
  props: {
    module: {
      type: String,
      required: true
    },
    name: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const { queryEnumOptions } = tpm.api.admin.common;
    const query = () => {
      return queryEnumOptions({ moduleCode: props.module, enumName: props.name });
    };
    return {
      query
    };
  }
};
function _sfc_render$9(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_checkbox = resolveComponent("m-checkbox");
  return openBlock(), createBlock(_component_m_checkbox, { action: $setup.query }, null, 8, ["action"]);
}
var component_4 = /* @__PURE__ */ _export_sfc(_sfc_main$9, [["render", _sfc_render$9]]);
const _sfc_main$8 = {
  props: {
    module: {
      type: String,
      required: true
    },
    name: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const { queryEnumOptions } = tpm.api.admin.common;
    const query = () => {
      return queryEnumOptions({ moduleCode: props.module, enumName: props.name });
    };
    return {
      query
    };
  }
};
function _sfc_render$8(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_radio = resolveComponent("m-radio");
  return openBlock(), createBlock(_component_m_radio, { action: $setup.query }, null, 8, ["action"]);
}
var component_5 = /* @__PURE__ */ _export_sfc(_sfc_main$8, [["render", _sfc_render$8]]);
const _sfc_main$7 = {
  props: {
    module: {
      type: String,
      required: true
    },
    name: {
      type: String,
      required: true
    },
    clearable: {
      type: Boolean,
      default: true
    }
  },
  setup(props) {
    const { queryEnumOptions } = tpm.api.admin.common;
    const query = () => {
      return queryEnumOptions({ moduleCode: props.module, enumName: props.name });
    };
    return {
      query
    };
  }
};
function _sfc_render$7(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, {
    action: $setup.query,
    clearable: $props.clearable
  }, null, 8, ["action", "clearable"]);
}
var component_6 = /* @__PURE__ */ _export_sfc(_sfc_main$7, [["render", _sfc_render$7]]);
var index_vue_vue_type_style_index_0_lang$1 = "";
var index_vue_vue_type_style_index_1_scoped_true_lang$1 = "";
const _sfc_main$6 = {
  components: {},
  props: {
    multiple: {
      type: Boolean,
      default: () => false
    },
    title: {
      type: String,
      default: () => ""
    },
    modelValue: {
      type: [String, Number, Array],
      required: true
    },
    maxWidth: {
      type: [Number, String],
      default: () => "760"
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
      default: () => () => {
      }
    },
    querySelect: {
      type: Function,
      default: () => () => {
      }
    }
  },
  emits: ["update:modelValue"],
  setup(props, { emit }) {
    const { queryMorgSelect } = tpm.api.maindata.morg;
    const state2 = reactive({
      inputTableVisible: false,
      el: {
        width: 0
      },
      data: {
        rows: []
      },
      selected: [],
      loading: false
    });
    const form = ref({});
    const tableRef = ref();
    const divisionRef = ref();
    const marketingcenterRef = ref();
    const dutyregionRef = ref();
    const departmentRef = ref();
    const stationRef = ref();
    const pages2 = ref({
      total: 0,
      index: 1,
      size: 15
    });
    const value_ = computed({
      get() {
        return props.modelValue;
      },
      set(val) {
        emit("update:modelValue", val);
      }
    });
    const handleFocus = (el) => {
      state2.el = el.target.getBoundingClientRect();
      state2.inputTableVisible = true;
      nextTick(() => {
        tableRef.value.setCurrentRow(state2.data.rows.filter((row) => row.value === value_.value));
      });
    };
    const handleBlur = () => {
      state2.inputTableVisible = false;
    };
    const queryList = async (selectValue = false) => {
      if (selectValue) {
        state2.selected = await props.querySelect();
      } else {
        state2.loading = true;
        const data = await props.query(form.value, pages2.value);
        state2.data.rows = data.rows;
        pages2.value.total = data.total;
        state2.loading = false;
        nextTick(() => {
          tableRef.value.setCurrentRow(state2.data.rows.filter((row) => row.value === value_.value));
        });
      }
    };
    const querySelectFilter = (level, pages3, keyword) => {
      return new Promise(async (resolve) => {
        let params = {
          level,
          page: {
            index: pages3.index,
            size: pages3.size
          },
          name: keyword
        };
        if (level >= 30 && form.value.divisionid) {
          params.level2Ids = [form.value.divisionid];
        }
        if (level >= 40 && form.value.marketingid) {
          params.level3Ids = [form.value.marketingid];
        }
        if (level >= 50 && form.value.dutyregionid) {
          params.level4Ids = [form.value.dutyregionid];
        }
        if (level >= 60 && form.value.departmentid) {
          params.level5Ids = [form.value.departmentid];
        }
        const data = await queryMorgSelect(params);
        resolve(data);
      });
    };
    const handleHeadOfficeChange = () => {
      divisionRef.value && divisionRef.value.reset();
      divisionRef.value && divisionRef.value.remoteMethod("");
      handleDivisionChange();
    };
    const handleDivisionChange = () => {
      marketingcenterRef.value && marketingcenterRef.value.reset();
      marketingcenterRef.value && marketingcenterRef.value.remoteMethod("");
      handleMarketingChange();
    };
    const handleMarketingChange = () => {
      dutyregionRef.value && dutyregionRef.value.reset();
      dutyregionRef.value && dutyregionRef.value.remoteMethod("");
      handleDutyregionChange();
    };
    const handleDutyregionChange = () => {
      departmentRef.value && departmentRef.value.reset();
      departmentRef.value && departmentRef.value.remoteMethod("");
      handleDepartmentChange();
    };
    const handleDepartmentChange = () => {
      stationRef.value && stationRef.value.reset();
      stationRef.value && stationRef.value.remoteMethod("");
    };
    const handleStationChange = () => {
    };
    const rowClassName = ({ row }) => {
      if (value_.value.indexOf(row.value) > -1) {
        return "highlight";
      }
      return "";
    };
    const handleRowClick = (row) => {
      state2.selected = [row];
      value_.value = row.value;
      if (!props.multiple) {
        state2.inputTableVisible = false;
      }
    };
    watch(form.value.divisionid, (newValue, oldValue) => {
      console.log("watch", newValue, oldValue);
    });
    const pageChange = (v) => {
      pages2.value.index = v;
      queryList(false);
    };
    window.addEventListener("resize", () => {
      state2.inputTableVisible = false;
    });
    window.removeEventListener("resize", () => {
    });
    return {
      ...toRefs(state2),
      value_,
      tableRef,
      divisionRef,
      marketingcenterRef,
      dutyregionRef,
      departmentRef,
      stationRef,
      pages: pages2,
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
    };
  }
};
function _sfc_render$6(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_option = resolveComponent("el-option");
  const _component_el_select = resolveComponent("el-select");
  const _component_el_col = resolveComponent("el-col");
  const _component_m_icon = resolveComponent("m-icon");
  const _component_m_maindata_select_page = resolveComponent("m-maindata-select-page");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_input = resolveComponent("el-input");
  const _component_m_button = resolveComponent("m-button");
  const _component_el_table_column = resolveComponent("el-table-column");
  const _component_el_table = resolveComponent("el-table");
  const _component_el_pagination = resolveComponent("el-pagination");
  const _component_el_row = resolveComponent("el-row");
  const _component_el_form = resolveComponent("el-form");
  const _directive_loading = resolveDirective("loading");
  return openBlock(), createElementBlock(Fragment, null, [
    createVNode(_component_el_select, {
      modelValue: $setup.value_,
      "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.value_ = $event),
      disabled: $props.disabled,
      class: "input-table-filter-select m-2",
      teleported: false,
      placeholder: "\u8BF7\u9009\u62E9",
      onFocus: $setup.handleFocus
    }, {
      default: withCtx(() => [
        (openBlock(true), createElementBlock(Fragment, null, renderList(_ctx.selected, (item) => {
          return openBlock(), createBlock(_component_el_option, {
            key: item.value,
            label: item.label,
            value: item.value
          }, null, 8, ["label", "value"]);
        }), 128))
      ]),
      _: 1
    }, 8, ["modelValue", "disabled", "onFocus"]),
    _ctx.inputTableVisible ? (openBlock(), createElementBlock("div", {
      key: 0,
      class: "input-table",
      style: normalizeStyle({ width: "100%", maxWidth: $props.maxWidth + "px", left: _ctx.el.left - 11 + "px", top: _ctx.el.top + _ctx.el.height + 5 + "px" })
    }, [
      createVNode(_component_el_form, {
        model: $setup.form,
        "label-width": "70px"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_row, {
            class: "input-table-row",
            gutter: 20
          }, {
            default: withCtx(() => [
              createVNode(_component_el_col, {
                span: 12,
                class: "col title"
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString($props.title), 1)
                ]),
                _: 1
              }),
              createVNode(_component_el_col, {
                span: 12,
                class: "col m-text-right"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_icon, {
                    name: "ri-close-fill",
                    size: "25px",
                    onClick: _cache[1] || (_cache[1] = withModifiers(($event) => _ctx.inputTableVisible = false, ["stop"]))
                  })
                ]),
                _: 1
              }),
              $props.selectStart <= 1 && $props.selectEnd >= 1 ? (openBlock(), createBlock(_component_el_col, {
                key: 0,
                span: 8,
                class: "col"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_form_item, {
                    label: _ctx.$t("mod.maindata.org_level_name.head_office")
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_m_maindata_select_page, {
                        modelValue: $setup.form.headofficeid,
                        "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.form.headofficeid = $event),
                        clearable: "",
                        action: (pages2, keyword) => $setup.querySelectFilter(10, pages2, keyword),
                        onChange: $setup.handleHeadOfficeChange,
                        onRemoveTag: $setup.handleHeadOfficeChange,
                        onClear: $setup.handleHeadOfficeChange
                      }, null, 8, ["modelValue", "action", "onChange", "onRemoveTag", "onClear"])
                    ]),
                    _: 1
                  }, 8, ["label"])
                ]),
                _: 1
              })) : createCommentVNode("", true),
              $props.selectStart <= 2 && $props.selectEnd >= 2 ? (openBlock(), createBlock(_component_el_col, {
                key: 1,
                span: 8,
                class: "col"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_form_item, {
                    label: _ctx.$t("mod.maindata.org_level_name.division")
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_m_maindata_select_page, {
                        ref: "divisionRef",
                        modelValue: $setup.form.divisionid,
                        "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.form.divisionid = $event),
                        clearable: "",
                        action: (pages2, keyword) => $setup.querySelectFilter(20, pages2, keyword),
                        onChange: $setup.handleDivisionChange,
                        onRemoveTag: $setup.handleDivisionChange,
                        onClear: $setup.handleDivisionChange
                      }, null, 8, ["modelValue", "action", "onChange", "onRemoveTag", "onClear"])
                    ]),
                    _: 1
                  }, 8, ["label"])
                ]),
                _: 1
              })) : createCommentVNode("", true),
              $props.selectStart <= 3 && $props.selectEnd >= 3 ? (openBlock(), createBlock(_component_el_col, {
                key: 2,
                span: 8,
                class: "col"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_form_item, {
                    label: _ctx.$t("mod.maindata.org_level_name.marketing_center")
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_m_maindata_select_page, {
                        ref: "marketingcenterRef",
                        modelValue: $setup.form.marketingcenterid,
                        "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.form.marketingcenterid = $event),
                        clearable: "",
                        action: (pages2, keyword) => $setup.querySelectFilter(30, pages2, keyword),
                        onChange: $setup.handleMarketingChange,
                        onRemoveTag: $setup.handleMarketingChange,
                        onClear: $setup.handleMarketingChange
                      }, null, 8, ["modelValue", "action", "onChange", "onRemoveTag", "onClear"])
                    ]),
                    _: 1
                  }, 8, ["label"])
                ]),
                _: 1
              })) : createCommentVNode("", true),
              $props.selectStart <= 4 && $props.selectEnd >= 4 ? (openBlock(), createBlock(_component_el_col, {
                key: 3,
                span: 8,
                class: "col"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_form_item, {
                    label: _ctx.$t("mod.maindata.org_level_name.dutyregion")
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_m_maindata_select_page, {
                        ref: "dutyregionRef",
                        modelValue: $setup.form.dutyregionid,
                        "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.form.dutyregionid = $event),
                        clearable: "",
                        action: (pages2, keyword) => $setup.querySelectFilter(40, pages2, keyword),
                        onChange: $setup.handleDutyregionChange,
                        onRemoveTag: $setup.handleDutyregionChange,
                        onClear: $setup.handleDutyregionChange
                      }, null, 8, ["modelValue", "action", "onChange", "onRemoveTag", "onClear"])
                    ]),
                    _: 1
                  }, 8, ["label"])
                ]),
                _: 1
              })) : createCommentVNode("", true),
              $props.selectStart <= 5 && $props.selectEnd >= 5 ? (openBlock(), createBlock(_component_el_col, {
                key: 4,
                span: 8,
                class: "col"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_form_item, {
                    label: _ctx.$t("mod.maindata.org_level_name.department")
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_m_maindata_select_page, {
                        ref: "departmentRef",
                        modelValue: $setup.form.departmentid,
                        "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => $setup.form.departmentid = $event),
                        clearable: "",
                        action: (pages2, keyword) => $setup.querySelectFilter(50, pages2, keyword),
                        onChange: $setup.handleDepartmentChange,
                        onRemoveTag: $setup.handleDepartmentChange,
                        onClear: $setup.handleDepartmentChange
                      }, null, 8, ["modelValue", "action", "onChange", "onRemoveTag", "onClear"])
                    ]),
                    _: 1
                  }, 8, ["label"])
                ]),
                _: 1
              })) : createCommentVNode("", true),
              $props.selectStart <= 6 && $props.selectEnd >= 6 ? (openBlock(), createBlock(_component_el_col, {
                key: 5,
                span: 8,
                class: "col"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_form_item, {
                    label: _ctx.$t("mod.maindata.org_level_name.station")
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_m_maindata_select_page, {
                        ref: "stationRef",
                        modelValue: $setup.form.stationid,
                        "onUpdate:modelValue": _cache[7] || (_cache[7] = ($event) => $setup.form.stationid = $event),
                        clearable: "",
                        action: (pages2, keyword) => $setup.querySelectFilter(60, pages2, keyword),
                        onChange: $setup.handleStationChange,
                        onRemoveTag: $setup.handleStationChange,
                        onClear: $setup.handleStationChange
                      }, null, 8, ["modelValue", "action", "onChange", "onRemoveTag", "onClear"])
                    ]),
                    _: 1
                  }, 8, ["label"])
                ]),
                _: 1
              })) : createCommentVNode("", true),
              createVNode(_component_el_col, {
                span: 8,
                class: "col"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_form_item, {
                    label: _ctx.$t("mod.maindata.name_label")
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_el_input, {
                        modelValue: $setup.form.name,
                        "onUpdate:modelValue": _cache[8] || (_cache[8] = ($event) => $setup.form.name = $event)
                      }, null, 8, ["modelValue"])
                    ]),
                    _: 1
                  }, 8, ["label"])
                ]),
                _: 1
              }),
              createVNode(_component_el_col, {
                span: 8,
                class: "col"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_button, {
                    type: "primary",
                    onClick: _cache[9] || (_cache[9] = withModifiers(($event) => $setup.queryList(false), ["stop"]))
                  }, {
                    default: withCtx(() => [
                      createTextVNode(toDisplayString(_ctx.$t("mod.maindata.search")), 1)
                    ]),
                    _: 1
                  })
                ]),
                _: 1
              }),
              createVNode(_component_el_col, { span: 24 }, {
                default: withCtx(() => [
                  withDirectives((openBlock(), createBlock(_component_el_table, {
                    ref: "tableRef",
                    "row-class-name": $setup.rowClassName,
                    border: "",
                    stripe: "",
                    height: 300,
                    class: "table",
                    data: _ctx.data.rows,
                    onRowClick: $setup.handleRowClick
                  }, {
                    default: withCtx(() => [
                      $props.multiple ? (openBlock(), createBlock(_component_el_table_column, {
                        key: 0,
                        type: "selection",
                        width: "40"
                      })) : createCommentVNode("", true),
                      createVNode(_component_el_table_column, {
                        prop: "label",
                        label: _ctx.$t("mod.maindata.label"),
                        width: "180"
                      }, null, 8, ["label"]),
                      createVNode(_component_el_table_column, {
                        prop: "value",
                        label: _ctx.$t("mod.maindata.value")
                      }, null, 8, ["label"])
                    ]),
                    _: 1
                  }, 8, ["row-class-name", "data", "onRowClick"])), [
                    [_directive_loading, _ctx.loading]
                  ]),
                  createVNode(_component_el_pagination, {
                    modelValue: $setup.pages.index,
                    "onUpdate:modelValue": _cache[10] || (_cache[10] = ($event) => $setup.pages.index = $event),
                    small: "",
                    background: "",
                    layout: "prev, pager, next",
                    total: $setup.pages.total,
                    class: "mt",
                    onCurrentChange: $setup.pageChange
                  }, null, 8, ["modelValue", "total", "onCurrentChange"])
                ]),
                _: 1
              })
            ]),
            _: 1
          })
        ]),
        _: 1
      }, 8, ["model"])
    ], 4)) : createCommentVNode("", true)
  ], 64);
}
var component_7 = /* @__PURE__ */ _export_sfc(_sfc_main$6, [["render", _sfc_render$6], ["__scopeId", "data-v-19526f1f"]]);
const _sfc_main$5 = {
  props: {
    clearable: {
      type: Boolean,
      default: true
    }
  },
  setup() {
    const { queryLoginModeSelect } = tpm.api.admin.common;
    const query = () => {
      return queryLoginModeSelect();
    };
    return {
      query
    };
  }
};
function _sfc_render$5(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, {
    action: $setup.query,
    clearable: $props.clearable
  }, null, 8, ["action", "clearable"]);
}
var component_8 = /* @__PURE__ */ _export_sfc(_sfc_main$5, [["render", _sfc_render$5]]);
const _sfc_main$4 = {
  setup() {
    const modules = tpm.modules.map((m) => {
      return {
        label: m.label,
        value: m.code,
        data: m
      };
    });
    const query = () => {
      return new Promise((resolve) => {
        resolve(modules);
      });
    };
    return {
      query
    };
  }
};
function _sfc_render$4(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, {
    action: $setup.query,
    clearable: ""
  }, null, 8, ["action"]);
}
var component_9 = /* @__PURE__ */ _export_sfc(_sfc_main$4, [["render", _sfc_render$4]]);
const _sfc_main$3 = {
  props: {
    clearable: {
      type: Boolean,
      default: true
    }
  },
  setup() {
    const { queryPlatformOptions } = tpm.api.admin.common;
    const query = () => {
      return queryPlatformOptions();
    };
    return {
      query
    };
  }
};
function _sfc_render$3(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, {
    action: $setup.query,
    clearable: $props.clearable
  }, null, 8, ["action", "clearable"]);
}
var component_10 = /* @__PURE__ */ _export_sfc(_sfc_main$3, [["render", _sfc_render$3]]);
var index_vue_vue_type_style_index_0_lang = "";
var index_vue_vue_type_style_index_1_scoped_true_lang = "";
const _sfc_main$2 = {
  components: {},
  props: {
    multiple: {
      type: Boolean,
      default: () => false
    },
    modelValue: {
      type: [String, Number, Array],
      required: true
    },
    divisionIds: {
      type: [String, Number, Array],
      required: true
    },
    marketingIds: {
      type: [String, Number, Array],
      required: true
    },
    dutyregionIds: {
      type: [String, Number, Array],
      required: true
    },
    departmentIds: {
      type: [String, Number, Array],
      required: true
    },
    stationIds: {
      type: [String, Number, Array],
      required: true
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
  emits: ["update:divisionIds", "update:marketingIds", "update:dutyregionIds", "update:departmentIds", "update:stationIds"],
  setup(props, { emit }) {
    const { queryMorgSelect } = tpm.api.maindata.morg;
    const state2 = reactive();
    const divisionRef = ref();
    const marketingcenterRef = ref();
    const dutyregionRef = ref();
    const departmentRef = ref();
    const stationRef = ref();
    const value_divisionIds = computed({
      get() {
        return props.divisionIds;
      },
      set(val) {
        emit("update:divisionIds", val);
      }
    });
    const value_marketingIds = computed({
      get() {
        return props.marketingIds;
      },
      set(val) {
        emit("update:marketingIds", val);
      }
    });
    const value_dutyregionIds = computed({
      get() {
        return props.dutyregionIds;
      },
      set(val) {
        emit("update:dutyregionIds", val);
      }
    });
    const value_departmentIds = computed({
      get() {
        return props.departmentIds;
      },
      set(val) {
        emit("update:departmentIds", val);
      }
    });
    const value_stationIds = computed({
      get() {
        return props.stationIds;
      },
      set(val) {
        emit("update:stationIds", val);
      }
    });
    const querySelectFilter = (level, pages2, keyword) => {
      return new Promise(async (resolve) => {
        let params = {
          level,
          page: {
            index: pages2.index,
            size: pages2.size
          },
          name: keyword
        };
        if (level >= 30 && value_divisionIds.value) {
          params.level2Ids = value_divisionIds.value;
        }
        if (level >= 40 && value_marketingIds.value) {
          params.level3Ids = value_marketingIds.value;
        }
        if (level >= 50 && value_dutyregionIds.value) {
          params.level4Ids = value_dutyregionIds.value;
        }
        if (level >= 60 && value_departmentIds.value) {
          params.level5Ids = value_departmentIds.value;
        }
        const data = await queryMorgSelect(params);
        resolve(data);
      });
    };
    const handleHeadOfficeChange = () => {
      divisionRef.value && divisionRef.value.reset();
      divisionRef.value && divisionRef.value.remoteMethod();
      handleDivisionChange();
    };
    const handleDivisionChange = () => {
      marketingcenterRef.value && marketingcenterRef.value.reset();
      marketingcenterRef.value && marketingcenterRef.value.remoteMethod();
      handleMarketingChange();
    };
    const handleMarketingChange = () => {
      dutyregionRef.value && dutyregionRef.value.reset();
      dutyregionRef.value && dutyregionRef.value.remoteMethod();
      handleDutyregionChange();
    };
    const handleDutyregionChange = () => {
      departmentRef.value && departmentRef.value.reset();
      departmentRef.value && departmentRef.value.remoteMethod();
      handleDepartmentChange();
    };
    const handleDepartmentChange = () => {
      stationRef.value && stationRef.value.reset();
      stationRef.value && stationRef.value.remoteMethod();
    };
    const handleStationChange = () => {
    };
    return {
      ...toRefs(state2),
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
    };
  }
};
function _sfc_render$2(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_maindata_select_page = resolveComponent("m-maindata-select-page");
  const _component_el_form_item = resolveComponent("el-form-item");
  return openBlock(), createElementBlock(Fragment, null, [
    $props.selectStart <= 1 && $props.selectEnd >= 1 ? (openBlock(), createBlock(_component_el_form_item, {
      key: 0,
      label: _ctx.$t("mod.maindata.org_level_name.head_office")
    }, {
      default: withCtx(() => [
        createVNode(_component_m_maindata_select_page, {
          modelValue: _ctx.form.headofficeid,
          "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => _ctx.form.headofficeid = $event),
          multiple: $props.multiple,
          clearable: "",
          action: (pages2, keyword) => $setup.querySelectFilter(10, pages2, keyword),
          onChange: $setup.handleHeadOfficeChange
        }, null, 8, ["modelValue", "multiple", "action", "onChange"])
      ]),
      _: 1
    }, 8, ["label"])) : createCommentVNode("", true),
    $props.selectStart <= 2 && $props.selectEnd >= 2 ? (openBlock(), createBlock(_component_el_form_item, {
      key: 1,
      label: _ctx.$t("mod.maindata.org_level_name.division")
    }, {
      default: withCtx(() => [
        createVNode(_component_m_maindata_select_page, {
          ref: "divisionRef",
          modelValue: $setup.value_divisionIds,
          "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.value_divisionIds = $event),
          multiple: $props.multiple,
          clearable: "",
          action: (pages2, keyword) => $setup.querySelectFilter(20, pages2, keyword),
          onChange: $setup.handleDivisionChange
        }, null, 8, ["modelValue", "multiple", "action", "onChange"])
      ]),
      _: 1
    }, 8, ["label"])) : createCommentVNode("", true),
    $props.selectStart <= 3 && $props.selectEnd >= 3 ? (openBlock(), createBlock(_component_el_form_item, {
      key: 2,
      label: _ctx.$t("mod.maindata.org_level_name.marketing_center")
    }, {
      default: withCtx(() => [
        createVNode(_component_m_maindata_select_page, {
          ref: "marketingcenterRef",
          modelValue: $setup.value_marketingIds,
          "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.value_marketingIds = $event),
          multiple: $props.multiple,
          clearable: "",
          action: (pages2, keyword) => $setup.querySelectFilter(30, pages2, keyword),
          onChange: $setup.handleMarketingChange
        }, null, 8, ["modelValue", "multiple", "action", "onChange"])
      ]),
      _: 1
    }, 8, ["label"])) : createCommentVNode("", true),
    $props.selectStart <= 4 && $props.selectEnd >= 4 ? (openBlock(), createBlock(_component_el_form_item, {
      key: 3,
      label: _ctx.$t("mod.maindata.org_level_name.dutyregion")
    }, {
      default: withCtx(() => [
        createVNode(_component_m_maindata_select_page, {
          ref: "dutyregionRef",
          modelValue: $setup.value_dutyregionIds,
          "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.value_dutyregionIds = $event),
          multiple: $props.multiple,
          clearable: "",
          action: (pages2, keyword) => $setup.querySelectFilter(40, pages2, keyword),
          onChange: $setup.handleDutyregionChange
        }, null, 8, ["modelValue", "multiple", "action", "onChange"])
      ]),
      _: 1
    }, 8, ["label"])) : createCommentVNode("", true),
    $props.selectStart <= 5 && $props.selectEnd >= 5 ? (openBlock(), createBlock(_component_el_form_item, {
      key: 4,
      label: _ctx.$t("mod.maindata.org_level_name.department")
    }, {
      default: withCtx(() => [
        createVNode(_component_m_maindata_select_page, {
          ref: "departmentRef",
          modelValue: $setup.value_departmentIds,
          "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.value_departmentIds = $event),
          multiple: $props.multiple,
          clearable: "",
          action: (pages2, keyword) => $setup.querySelectFilter(50, pages2, keyword),
          onChange: $setup.handleDepartmentChange
        }, null, 8, ["modelValue", "multiple", "action", "onChange"])
      ]),
      _: 1
    }, 8, ["label"])) : createCommentVNode("", true),
    $props.selectStart <= 6 && $props.selectEnd >= 6 ? (openBlock(), createBlock(_component_el_form_item, {
      key: 5,
      label: _ctx.$t("mod.maindata.org_level_name.station")
    }, {
      default: withCtx(() => [
        createVNode(_component_m_maindata_select_page, {
          ref: "stationRef",
          modelValue: $setup.value_stationIds,
          "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.value_stationIds = $event),
          multiple: $props.multiple,
          clearable: "",
          action: (pages2, keyword) => $setup.querySelectFilter(60, pages2, keyword),
          onChange: $setup.handleStationChange
        }, null, 8, ["modelValue", "multiple", "action", "onChange"])
      ]),
      _: 1
    }, 8, ["label"])) : createCommentVNode("", true)
  ], 64);
}
var component_11 = /* @__PURE__ */ _export_sfc(_sfc_main$2, [["render", _sfc_render$2], ["__scopeId", "data-v-021e0588"]]);
const _sfc_main$1 = {
  props: {
    modelValue: {
      type: [String, Number, Array],
      required: true
    },
    action: {
      type: Function,
      required: true
    },
    checkedFirst: Boolean,
    refreshOnCreated: {
      type: Boolean,
      default: true
    },
    placeholder: {
      type: String,
      default: ""
    }
  },
  emits: ["update:modelValue", "update:label", "change"],
  setup(props, { emit }) {
    const resetMethods = inject("resetMethods", []);
    const sRef = ref();
    const value_ = computed({
      get() {
        return props.modelValue;
      },
      set(val) {
        emit("update:modelValue", val);
      }
    });
    let firstRefresh = true;
    const loading = ref(false);
    const options = ref([]);
    const refresh = () => {
      loading.value = true;
      props.action().then((data) => {
        options.value = data;
        console.log("--------------------data");
        console.log(data);
        console.log("--------------------data");
        if (firstRefresh) {
          if (value_.value) {
            console.log("::1");
            handleChange(value_.value);
          } else if (props.checkedFirst && data.length > 0) {
            console.log("::2");
            const checkedValue = data[0].value;
            value_.value = checkedValue;
            handleChange(checkedValue);
          } else if (data.length > 0) {
            console.log("::3");
          }
          firstRefresh = false;
        }
      }).finally(() => {
        loading.value = false;
      });
    };
    const handleChange = (val) => {
      if (val == null || val == "")
        return;
      console.log("handleChange");
      console.log(val);
      if (sRef.value.multiple) {
        let list = [];
        val.forEach((item) => {
          for (var i = 0; i < options.value.length; i++) {
            const opt = options.value[i];
            if (opt.value === item.value) {
              list.push(opt);
              break;
            }
          }
        });
        console.log("list===============");
        console.log(list);
        const selection = list[0];
        emit("update:label", selection != void 0 ? selection.label : "");
        emit("change", val, list, options);
      } else {
        const selection = options.value.find((m) => m.value === val);
        emit("update:label", selection != void 0 ? selection.label : "");
        emit("change", val, selection, options);
      }
    };
    if (props.refreshOnCreated)
      refresh();
    const reset = () => {
      value_.value = "";
      handleChange("");
    };
    watch(value_, handleChange);
    resetMethods.push(reset);
    return {
      value_,
      loading,
      options,
      refresh,
      reset,
      handleChange,
      sRef
    };
  }
};
function _sfc_render$1(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_option = resolveComponent("el-option");
  const _component_el_select = resolveComponent("el-select");
  const _directive_loading = resolveDirective("loading");
  return withDirectives((openBlock(), createBlock(_component_el_select, {
    ref: "sRef",
    modelValue: $setup.value_,
    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.value_ = $event),
    class: "m-select",
    "element-loading-background": "rgba(255,255,255,.6)",
    placeholder: $props.placeholder || _ctx.$t("tpm.please_select")
  }, {
    default: withCtx(() => [
      renderSlot(_ctx.$slots, "default", { options: $setup.options }, () => [
        (openBlock(true), createElementBlock(Fragment, null, renderList($setup.options, (item) => {
          return openBlock(), createBlock(_component_el_option, {
            key: item.value,
            label: item.label,
            value: item.value,
            disabled: item.disabled
          }, null, 8, ["label", "value", "disabled"]);
        }), 128))
      ])
    ]),
    _: 3
  }, 8, ["modelValue", "placeholder"])), [
    [_directive_loading, $setup.loading]
  ]);
}
var component_12 = /* @__PURE__ */ _export_sfc(_sfc_main$1, [["render", _sfc_render$1]]);
const _sfc_main = {
  props: {
    modelValue: {
      type: [String, Number, Array],
      required: true
    },
    action: {
      type: Function,
      required: true
    },
    searchInterval: {
      type: Number,
      default: 700
    }
  },
  emits: ["update:modelValue", "change"],
  setup(props, { emit }) {
    const resetMethods = inject("resetMethods", []);
    const value_ = computed({
      get() {
        return props.modelValue;
      },
      set(val) {
        emit("update:modelValue", val || "");
      }
    });
    let timer = null;
    const first = ref(true);
    const loading = ref(false);
    const options = ref([]);
    const searchKeyword = ref("");
    const pages2 = ref({
      total: 0,
      index: 1,
      size: 15
    });
    const remoteMethod = (keyword, getValues = false) => {
      searchKeyword.value = keyword;
      if (!getValues) {
        first.value = false;
      }
      if (timer)
        clearTimeout(timer);
      timer = setTimeout(() => {
        loading.value = true;
        props.action(pages2.value, keyword, getValues).then((data) => {
          options.value = data.rows;
          pages2.value.total = data.total;
          if (first.value) {
            first.value = false;
            nextTick(() => {
              setTimeout(() => {
                options.value = [];
                pages2.value.total = 0;
              }, 300);
            });
          }
        }).finally(() => {
          loading.value = false;
        });
      }, props.searchInterval);
    };
    const handleChange = (val) => {
      const option = options.value.find((m) => m.value === val);
      emit("change", val, option, options);
    };
    const reset = () => {
      value_.value = "";
    };
    const pageChange = (v) => {
      pages2.value.index = v;
      remoteMethod(searchKeyword.value);
    };
    const visibleChange = (v) => {
      if (v && options.value.length === 0) {
        pages2.value.index = 1;
        remoteMethod(searchKeyword.value);
      }
    };
    resetMethods.push(reset);
    return {
      value_,
      loading,
      options,
      remoteMethod,
      handleChange,
      reset,
      pages: pages2,
      visibleChange,
      pageChange
    };
  }
};
function _sfc_render(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_option = resolveComponent("el-option");
  const _component_el_pagination = resolveComponent("el-pagination");
  const _component_el_select = resolveComponent("el-select");
  return openBlock(), createBlock(_component_el_select, {
    modelValue: $setup.value_,
    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.value_ = $event),
    "collapse-tags": true,
    loading: $setup.loading,
    filterable: "",
    remote: "",
    "remote-method": $setup.remoteMethod,
    onVisibleChange: $setup.visibleChange,
    onChange: $setup.handleChange
  }, {
    default: withCtx(() => [
      renderSlot(_ctx.$slots, "default", { options: $setup.options }, () => [
        (openBlock(true), createElementBlock(Fragment, null, renderList($setup.options, (item) => {
          return openBlock(), createBlock(_component_el_option, {
            key: item.value,
            label: item.label,
            value: item.value,
            disabled: item.disabled
          }, null, 8, ["label", "value", "disabled"]);
        }), 128))
      ]),
      createVNode(_component_el_pagination, {
        modelValue: $setup.pages.index,
        "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.pages.index = $event),
        small: "",
        background: "",
        layout: "prev, pager, next",
        total: $setup.pages.total,
        class: "mt",
        onCurrentChange: $setup.pageChange
      }, null, 8, ["modelValue", "total", "onCurrentChange"])
    ]),
    _: 3
  }, 8, ["modelValue", "loading", "remote-method", "onVisibleChange", "onChange"]);
}
var component_13 = /* @__PURE__ */ _export_sfc(_sfc_main, [["render", _sfc_render]]);
const pages = [];
pages.push(page$k);
pages.push(page$i);
pages.push(page$g);
pages.push(page$e);
pages.push(page$c);
pages.push(page$a);
pages.push(page$8);
pages.push(page$6);
pages.push(page$4);
pages.push(page$2);
pages.push(page);
const components = [];
components.push({ name: "config-maindata", component: component_0 });
components.push({ name: "dict-cascader", component: component_1 });
components.push({ name: "dict-select", component: component_2 });
components.push({ name: "dict-toolbar-fullscreen", component: component_3 });
components.push({ name: "enum-checkbox", component: component_4 });
components.push({ name: "enum-radio", component: component_5 });
components.push({ name: "enum-select", component: component_6 });
components.push({ name: "input-table-filter", component: component_7 });
components.push({ name: "loginmode-select", component: component_8 });
components.push({ name: "module-select", component: component_9 });
components.push({ name: "platform-select", component: component_10 });
components.push({ name: "query-org-group", component: component_11 });
components.push({ name: "role-select", component: component_12 });
components.push({ name: "select-page", component: component_13 });
const api = {};
api["common"] = api_common;
api["mdistributor"] = api_mdistributor;
api["mdistributorrelation"] = api_mdistributorrelation;
api["mentity"] = api_mentity;
api["mmarketingproduct"] = api_mmarketingproduct;
api["mmarketingsetup"] = api_mmarketingsetup;
api["morg"] = api_morg;
api["mproduct"] = api_mproduct;
api["mproductproperty"] = api_mproductproperty;
api["mterminal"] = api_mterminal;
api["mterminaldistributor"] = api_mterminaldistributor;
api["mterminaluser"] = api_mterminaluser;
const mod = { id: 2, code: "maindata", version: "1.0.3", label: "TPM\u4E3B\u6570\u636E\u6A21\u5757", icon: "lock", description: "CRB.TPM\u4E3B\u6570\u636E\u6A21\u5757", store, pages, components, api };
tpm.useModule(mod);
import './style.css';