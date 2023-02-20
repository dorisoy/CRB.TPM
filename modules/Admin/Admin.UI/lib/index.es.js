import { useMessage, withSaveProps, regex, useSave, entityBaseCols, useList, dom } from "tpm-ui";
import { ref, reactive, watch, toRefs, resolveComponent, resolveDirective, withDirectives, openBlock, createBlock, withCtx, createElementVNode, createVNode, toDisplayString, computed, mergeProps, toHandlers, createCommentVNode, createElementBlock, Fragment, createTextVNode, renderList, nextTick, pushScopeId, popScopeId, onActivated, onDeactivated, onBeforeMount, onMounted, onBeforeUnmount, normalizeStyle, resolveDynamicComponent, defineComponent, toRef, withModifiers, getCurrentInstance, watchEffect, inject, renderSlot } from "vue";
const urls$a = {
  DEFAULT_PASSWORD: "Account/DefaultPassword",
  UPDATE_SKIN: "Account/UpdateSkin",
  UPDATE_ACCOUNT_ROLE_ORG: "Account/UpdateAccountRoleOrg",
  ACCOUNT_SELECT: "Account/Select"
};
var api_account = (http) => {
  return {
    queryAccountSelect: (params) => http.get(urls$a.ACCOUNT_SELECT, params),
    getDefaultPassword: () => http.get(urls$a.DEFAULT_PASSWORD),
    updateSkin: (params) => http.post(urls$a.UPDATE_SKIN, params),
    updateAccountRoleOrg: (params) => http.post(urls$a.UPDATE_ACCOUNT_ROLE_ORG, params)
  };
};
const _hasbtoa = typeof btoa === "function";
const _hasBuffer = typeof Buffer === "function";
typeof TextDecoder === "function" ? new TextDecoder() : void 0;
const _TE = typeof TextEncoder === "function" ? new TextEncoder() : void 0;
const b64ch = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
const b64chs = Array.prototype.slice.call(b64ch);
((a) => {
  let tab = {};
  a.forEach((c, i) => tab[c] = i);
  return tab;
})(b64chs);
const _fromCC = String.fromCharCode.bind(String);
typeof Uint8Array.from === "function" ? Uint8Array.from.bind(Uint8Array) : (it, fn = (x) => x) => new Uint8Array(Array.prototype.slice.call(it, 0).map(fn));
const _mkUriSafe = (src) => src.replace(/=/g, "").replace(/[+\/]/g, (m0) => m0 == "+" ? "-" : "_");
const btoaPolyfill = (bin) => {
  let u32, c0, c1, c2, asc = "";
  const pad = bin.length % 3;
  for (let i = 0; i < bin.length; ) {
    if ((c0 = bin.charCodeAt(i++)) > 255 || (c1 = bin.charCodeAt(i++)) > 255 || (c2 = bin.charCodeAt(i++)) > 255)
      throw new TypeError("invalid character found");
    u32 = c0 << 16 | c1 << 8 | c2;
    asc += b64chs[u32 >> 18 & 63] + b64chs[u32 >> 12 & 63] + b64chs[u32 >> 6 & 63] + b64chs[u32 & 63];
  }
  return pad ? asc.slice(0, pad - 3) + "===".substring(pad) : asc;
};
const _btoa = _hasbtoa ? (bin) => btoa(bin) : _hasBuffer ? (bin) => Buffer.from(bin, "binary").toString("base64") : btoaPolyfill;
const _fromUint8Array = _hasBuffer ? (u8a) => Buffer.from(u8a).toString("base64") : (u8a) => {
  const maxargs = 4096;
  let strs = [];
  for (let i = 0, l = u8a.length; i < l; i += maxargs) {
    strs.push(_fromCC.apply(null, u8a.subarray(i, i + maxargs)));
  }
  return _btoa(strs.join(""));
};
const cb_utob = (c) => {
  if (c.length < 2) {
    var cc = c.charCodeAt(0);
    return cc < 128 ? c : cc < 2048 ? _fromCC(192 | cc >>> 6) + _fromCC(128 | cc & 63) : _fromCC(224 | cc >>> 12 & 15) + _fromCC(128 | cc >>> 6 & 63) + _fromCC(128 | cc & 63);
  } else {
    var cc = 65536 + (c.charCodeAt(0) - 55296) * 1024 + (c.charCodeAt(1) - 56320);
    return _fromCC(240 | cc >>> 18 & 7) + _fromCC(128 | cc >>> 12 & 63) + _fromCC(128 | cc >>> 6 & 63) + _fromCC(128 | cc & 63);
  }
};
const re_utob = /[\uD800-\uDBFF][\uDC00-\uDFFFF]|[^\x00-\x7F]/g;
const utob = (u) => u.replace(re_utob, cb_utob);
const _encode = _hasBuffer ? (s) => Buffer.from(s, "utf8").toString("base64") : _TE ? (s) => _fromUint8Array(_TE.encode(s)) : (s) => _btoa(utob(s));
const encode = (src, urlsafe = false) => urlsafe ? _mkUriSafe(_encode(src)) : _encode(src);
const urls$9 = {
  LOGIN: "Authorize/Login",
  REFRESH_TOKEN: "Authorize/RefreshToken",
  VERIFY_CODE: "Authorize/VerifyCode",
  PROFILE: "Authorize/Profile"
};
var api_authorize = (http) => {
  return {
    login(params) {
      let data = Object.assign({}, params);
      data.username = encode(data.username);
      data.password = encode(data.password);
      return http.post(urls$9.LOGIN, data);
    },
    refreshToken: (params) => http.post(urls$9.REFRESH_TOKEN, params),
    getVerifyCode: () => http.get(urls$9.VERIFY_CODE),
    getProfile: () => http.get(urls$9.PROFILE)
  };
};
const urls$8 = {
  ENUM_OPTIONS: "Common/EnumOptions",
  PLATFORM_OPTIONS: "Common/PlatformOptions",
  LOGINMODE_SELECT: "Common/LoginModeSelect",
  ACCOUNTTYPESELECT_SELECT: "Common/AccountTypeSelect",
  ACCOUNTSTATUSSELECT_SELECT: "Common/AccountStatusSelect"
};
var api_common = (http) => {
  return {
    queryEnumOptions: (params) => http.get(urls$8.ENUM_OPTIONS, params),
    queryPlatformOptions: () => http.get(urls$8.PLATFORM_OPTIONS),
    queryLoginModeSelect: () => http.get(urls$8.LOGINMODE_SELECT),
    queryAccountTypeSelect: () => http.get(urls$8.ACCOUNTTYPESELECT_SELECT),
    queryAccountStatusSelect: () => http.get(urls$8.ACCOUNTSTATUSSELECT_SELECT)
  };
};
const urls$7 = {
  GETUI: "Config/UI",
  DESCRIPTORS: "Config/Descriptors",
  EDIT: "Config/Edit",
  UPDATE: "Config/Update"
};
var api_config = (http) => {
  return {
    getUI: () => http.get(urls$7.GETUI),
    getDescriptors: () => http.get(urls$7.DESCRIPTORS),
    edit: (params) => http.get(urls$7.EDIT, params),
    update: (params) => http.post(urls$7.UPDATE, params)
  };
};
const urls$6 = {
  SELECT: "Dict/Select",
  TREE: "Dict/Tree",
  CASCADER: "Dict/Cascader"
};
var api_dict = (http) => {
  return {
    select: (params) => http.get(urls$6.SELECT, params),
    tree: (params) => http.get(urls$6.TREE, params),
    cascader: (params) => http.get(urls$6.CASCADER, params)
  };
};
const urls$5 = {
  SELECT: "DictGroup/Select"
};
var api_dictGroup = (http) => {
  return {
    select: () => http.get(urls$5.SELECT)
  };
};
var api_dictItem = (http) => {
  return {};
};
const urls$4 = {
  GROUP_SELECT: "Menu/GroupSelect",
  TREE: "Menu/Tree",
  UPDATE_SORT: "Menu/UpdateSort"
};
var api_menu = (http) => {
  return {
    getGroupSelect: () => http.get(urls$4.GROUP_SELECT),
    getTree: (params) => http.get(urls$4.TREE, params),
    updateSort: (sorts) => http.post(urls$4.UPDATE_SORT, sorts)
  };
};
const urls$3 = {
  SELECT: "MenuGroup/Select"
};
var api_menuGroup = (http) => {
  return {
    select: () => http.get(urls$3.SELECT)
  };
};
const urls$2 = {
  GET_PERMISSIONS: "Module/Permissions"
};
var api_module = (http) => {
  return {
    getPermissions: (params) => http.get(urls$2.GET_PERMISSIONS, params)
  };
};
const urls$1 = {
  TREE: "MOrg/Tree",
  DELETESELECTED: "MOrg/DeleteSelected",
  SELECT: "MOrg/Select",
  GETCURRENTACCOUNTAROSTREE: "MOrg/GetCurrentAccountAROSTree",
  GETORGLEVEL: "MOrg/GetOrgLevel",
  GETTREEBYPARENTID: "MOrg/GetTreeByParentId"
};
var api_morg = (http) => {
  return {
    orgTree: (level) => http.get(urls$1.TREE, level),
    deleteSelected: (params) => http.delete(urls$1.DELETESELECTED, params),
    orgSelect: (params) => http.get(urls$1.SELECT, params),
    getCurrentAccountAROSTree: (params) => http.get(urls$1.GETCURRENTACCOUNTAROSTREE, params),
    getOrgLevel: (params) => http.get(urls$1.GETORGLEVEL, params),
    getTreeByParentId: (params) => http.get(urls$1.GETTREEBYPARENTID, params)
  };
};
const urls = {
  QUERY_BIND_MENUS: "Role/QueryBindMenus",
  UPDATE_BIND_MENUS: "Role/UpdateBindMenus",
  SELECT: "Role/Select",
  GETORGLEVEL: "MOrg/GetOrgLevel",
  GETTREEBYPARENTID: "MOrg/GetTreeByParentId",
  GETNODEBYPARENTID: "MOrg/GetNodeByParentId"
};
var api_role = (http) => {
  const getNodeTree = async (level) => {
    let data = [];
    switch (level) {
      case 1:
        data = await http.get(urls.GETORGLEVEL, { level: 10 });
        break;
      case 2:
        data = await http.get(urls.GETORGLEVEL, { level: 20 });
        break;
      case 3:
        data = await http.get(urls.GETORGLEVEL, { level: 30 });
        break;
      case 4:
        data = await http.get(urls.GETORGLEVEL, { level: 40 });
        break;
      case 5:
        data = await http.get(urls.GETORGLEVEL, { level: 50 });
        break;
      case 6:
        data = await http.get(urls.GETORGLEVEL, { level: 60 });
        break;
      default:
        data = await http.get(urls.GETTREEBYPARENTID, { level: 60 });
        break;
    }
    return treeConcat(data);
  };
  const treeConcat = (data) => {
    data.forEach(function(n, index2) {
      delete n.children;
    });
    var map = {};
    data.forEach(function(n) {
      map[n.id] = n;
    });
    var val = [];
    data.forEach(function(n) {
      var parent = map[n.parentId];
      if (parent) {
        (parent.children || (parent.children = [])).push(n);
      } else {
        val.push(n);
      }
    });
    return val;
  };
  const getTree = async (params) => {
    let root = await http.get(urls.GETTREEBYPARENTID, { level: 10, ignore: true });
    return root;
  };
  const filterDate = (res, orgs, parentId, ignore) => {
    let data = [];
    if (!ignore && orgs.length > 0 && parentId != "null")
      data = res.filter((m) => orgs.includes(m.id.toLowerCase()) && m.parentId.toLowerCase() === parentId.toLowerCase());
    else if (!ignore && parentId != "null")
      data = res.filter((m) => m.parentId.toLowerCase() === parentId.toLowerCase());
    else if (ignore) {
      data = res;
    }
    return data;
  };
  const getChildTree = async (params) => {
    var _a;
    let data = [];
    let orgs = (_a = params.includes) == null ? void 0 : _a.map((s) => s.orgId);
    let parentId = params.parentId;
    if (parentId == null || parentId == void 0) {
      parentId = "null";
    }
    switch (params.level) {
      case 1: {
        let res = await http.get(urls.GETORGLEVEL, { level: 10, ignore: params.ignore });
        data = filterDate(res, orgs, parentId, params.ignore);
        break;
      }
      case 2: {
        let res = await http.get(urls.GETORGLEVEL, { level: 20, ignore: params.ignore });
        data = filterDate(res, orgs, parentId, params.ignore);
        break;
      }
      case 3: {
        let res = await http.get(urls.GETORGLEVEL, { level: 30, ignore: params.ignore });
        data = filterDate(res, orgs, parentId, params.ignore);
        break;
      }
      case 4: {
        let res = await http.get(urls.GETORGLEVEL, { level: 40, ignore: params.ignore });
        data = filterDate(res, orgs, parentId, params.ignore);
        break;
      }
      case 5: {
        let res = await http.get(urls.GETORGLEVEL, { level: 50, ignore: params.ignore });
        data = filterDate(res, orgs, parentId, params.ignore);
        break;
      }
      case 6: {
        let res = await http.get(urls.GETORGLEVEL, { level: 60, ignore: params.ignore });
        data = filterDate(res, orgs, parentId, params.ignore);
        break;
      }
    }
    return data;
  };
  const getOrgLevel = async (params) => {
    let tree = [];
    const data = await getChildTree(params);
    if (data != null) {
      tree = treeConcat(data);
    }
    return tree;
  };
  const getPathByKey = (curKey, data) => {
    let result = [];
    let traverse = (curKey2, path2, data2) => {
      if (data2.length === 0) {
        return;
      }
      for (let item of data2) {
        path2.push(item);
        if (item.id === curKey2) {
          result = JSON.parse(JSON.stringify(path2));
          return;
        }
        const children = Array.isArray(item.children) ? item.children : [];
        traverse(curKey2, path2, children);
        path2.pop();
      }
    };
    traverse(curKey, [], data);
    return result;
  };
  return {
    queryBindMenus: (params) => http.get(urls.QUERY_BIND_MENUS, params),
    updateBindMenus: (params) => http.post(urls.UPDATE_BIND_MENUS, params),
    getNodeByParentId: (params) => http.get(urls.GETNODEBYPARENTID, params),
    select: () => http.get(urls.SELECT),
    getNodeTree,
    getTree,
    treeConcat,
    getOrgLevel,
    getPathByKey,
    getChildTree
  };
};
const state = {
  dict: {
    groupCode: "",
    dictCode: ""
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
const name$4 = "admin_account";
const icon$4 = "user";
const path$3 = "/admin/account";
const permissions$3 = [
  "admin_account_query_get",
  "admin_account_DefaultPassword_get"
];
const buttons$3 = {
  add: {
    text: "tpm.add",
    code: "admin_account_add",
    permissions: [
      "admin_account_add_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "admin_account_edit",
    permissions: [
      "admin_account_edit_get",
      "admin_account_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "admin_account_delete",
    permissions: [
      "admin_account_delete_delete"
    ]
  },
  roleorgset: {
    text: "\u7EC4\u7EC7",
    icon: "cog",
    code: "admin_account_roleorgset",
    permissions: [
      "admin_account_roleorgset_post"
    ]
  },
  "import": {
    text: "\u5BFC\u5165",
    icon: "cog",
    code: "admin_account_import",
    permissions: [
      "admin_account_import_post"
    ]
  },
  importTemplate: {
    text: "\u5BFC\u5165\u6A21\u677F",
    icon: "cog",
    code: "admin_account_importTemplate",
    permissions: [
      "admin_account_importTemplate_post"
    ]
  },
  "export": {
    text: "\u5BFC\u51FA",
    icon: "cog",
    code: "admin_account_export",
    permissions: [
      "admin_account_export_download"
    ]
  },
  batPermission: {
    text: "\u6279\u91CF\u5206\u914D\u6743\u9650",
    icon: "cog",
    code: "admin_account_batPermission",
    permissions: [
      "admin_account_dbatPermission_post"
    ]
  }
};
var page$a = {
  name: name$4,
  icon: icon$4,
  path: path$3,
  permissions: permissions$3,
  buttons: buttons$3
};
var _export_sfc$1 = (sfc, props) => {
  const target = sfc.__vccOpts || sfc;
  for (const [key, val] of props) {
    target[key] = val;
  }
  return target;
};
const _sfc_main$Q = {
  props: {
    nodeProps: {
      children: "children",
      label: "label"
    },
    selection: {
      type: Object
    },
    refreshOnCreated: {
      type: Boolean,
      default: true
    }
  },
  emits: ["change"],
  setup(props, { emit }) {
    const { store: store2, $t } = tpm;
    const { getTree, getOrgLevel } = tpm.api.admin.role;
    useMessage();
    const currentKey = ref("00000000-0000-0000-0000-000000000000");
    const treeRef = ref();
    const treeData = ref([]);
    const loading = ref(false);
    const model = reactive({
      defaultkeys: [],
      defaultExpandedCids: []
    });
    const handleTreeChange = (data) => {
      if (data != null) {
        currentKey.value = data.id;
        emit("change", data.data);
        let getNode = treeRef.value.getCheckedNodes();
        if (getNode.length > 0) {
          getNode.forEach((item) => {
            treeRef.value.setChecked(item.id, false);
            treeRef.value.setChecked(data.key, true);
          });
        } else {
          treeRef.value.setChecked(data.key, true);
        }
      }
    };
    const checkClick = (data) => {
      let getNode = treeRef.value.getCheckedNodes();
      if (getNode.length > 0) {
        getNode.forEach((item) => {
          if (data.id == item.id) {
            treeRef.value.setChecked(item.id, true);
          } else {
            treeRef.value.setChecked(item.id, false);
          }
        });
      }
      emit("change", data);
    };
    const loadfirstnode = async (resolve, includes) => {
      loading.value = true;
      const res = await getTree({
        level: 10,
        includes: includes || []
      });
      let orgs = includes == null ? void 0 : includes.map((s) => s.orgId);
      let data = res.filter((m) => m);
      if (orgs.length > 0)
        ;
      loading.value = false;
      console.log("data", data);
      if (props.selection.orgId !== void 0 && props.selection.orgId !== "" && props.selection.orgId !== "undefined") {
        model.defaultkeys.push(props.selection.orgId);
        model.defaultExpandedCids.push(props.selection.orgId);
      }
      if (data && Array.isArray(data)) {
        data.forEach((item) => {
          model.defaultExpandedCids.push(item.id);
        });
      }
      return resolve(data);
    };
    const loadchildnode = async (node, resolve, includes) => {
      let params = {
        level: node.level + 1,
        parentId: node.key || "00000000-0000-0000-0000-000000000000",
        includes: includes || []
      };
      loading.value = true;
      console.log("loadchildnode - > params", params);
      let res = await getOrgLevel(params);
      if (includes.length == 0)
        ;
      if (node.level >= 3) {
        res = [];
      }
      console.log("loadchildnode", res);
      loading.value = false;
      if (res && Array.isArray(res)) {
        res.forEach((item) => {
        });
      }
      return resolve(res);
    };
    const loadNode = (node, resolve) => {
      let profile = store2.state.app.profile;
      let includes = profile.accountRoleOrgs;
      console.log("node.level", node.level);
      console.log("profile", profile);
      console.log("includes", includes);
      if (node.level == 0) {
        loadfirstnode(resolve, includes);
      }
      if (node.level >= 1) {
        loadchildnode(node, resolve, includes);
      }
    };
    watch(props.selection, () => {
      if (props.selection.orgId !== void 0 && props.selection.orgId !== "" && props.selection.orgId !== "undefined") {
        treeRef.value.setChecked(props.selection.orgId, true);
      }
    });
    return {
      ...toRefs(model),
      loading,
      treeRef,
      treeData,
      handleTreeChange,
      checkClick,
      loadNode,
      loadfirstnode,
      loadchildnode
    };
  }
};
function _sfc_render$Q(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_icon = resolveComponent("m-icon");
  const _component_el_tree = resolveComponent("el-tree");
  const _directive_loading = resolveDirective("loading");
  return withDirectives((openBlock(), createBlock(_component_el_tree, {
    ref: "treeRef",
    data: $setup.treeData,
    "current-node-key": `00000000-0000-0000-0000-000000000000`,
    props: $props.nodeProps,
    load: $setup.loadNode,
    lazy: "",
    "node-key": "id",
    "highlight-current": "",
    onCurrentChange: $setup.handleTreeChange,
    "expand-on-click-node": false,
    "check-strictly": true,
    "show-checkbox": "",
    onCheck: $setup.checkClick,
    "default-checked-keys": _ctx.defaultkeys,
    "default-expanded-keys": _ctx.defaultExpandedCids
  }, {
    default: withCtx(({ node, data }) => [
      createElementVNode("span", null, [
        createVNode(_component_m_icon, {
          name: "folder-o",
          class: "m-margin-r-5"
        }),
        createElementVNode("span", null, toDisplayString(data.label == null ? data.name : data.label), 1)
      ])
    ]),
    _: 1
  }, 8, ["data", "props", "load", "onCurrentChange", "onCheck", "default-checked-keys", "default-expanded-keys"])), [
    [_directive_loading, $setup.loading]
  ]);
}
var TreePage$2 = /* @__PURE__ */ _export_sfc$1(_sfc_main$Q, [["render", _sfc_render$Q]]);
const _sfc_main$P = {
  props: {
    ...withSaveProps,
    selection: {
      type: Object
    }
  },
  emits: ["success"],
  components: { TreePage: TreePage$2 },
  setup(props, { emit }) {
    const { store: store2, $t } = tpm;
    const api2 = tpm.api.admin.account;
    const model = reactive({
      username: "",
      password: "",
      roles: [],
      name: "",
      phone: "",
      email: "",
      status: -1,
      type: -1,
      orgId: "",
      orgName: ""
    });
    const splitRef = ref(0.5);
    const tree = ref();
    const showTree = ref(false);
    const rules = computed(() => {
      return {
        username: [{ required: true, message: $t("mod.admin.input_username") }],
        name: [{ required: true, message: $t("mod.admin.input_name") }],
        roles: [{ required: true, message: $t("mod.admin.select_role") }],
        phone: [{ pattern: regex.phone, message: $t("mod.admin.input_phone") }],
        email: [{ type: "email", message: $t("mod.admin.input_email") }]
      };
    });
    const nameRef = ref(null);
    const { isEdit, bind, on: on2 } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = nameRef;
    bind.width = "1200px";
    bind.height = "600px";
    const defaultPassword = ref("");
    api2.getDefaultPassword().then((data) => {
      defaultPassword.value = data;
    });
    const handleClose = () => {
    };
    const handleOpened = () => {
      if (props.selection.roles.length > 0) {
        let ids = props.selection.roles.map((b) => b.value);
        model.roles = ids;
      }
    };
    const selectChange = () => {
    };
    const handleSuccess = () => {
      console.log("handleSuccess");
      if (props.mode === "edit" && props.id === store2.state.app.profile.accountId) {
        store2.dispatch("app/profile/init", null, { root: true }).then(() => {
          emit("success");
        });
      } else {
        emit("success");
      }
    };
    const onTreeChange = (data) => {
      if (data != null) {
        console.log("onTreeChange", data);
        model.orgId = data.id;
        model.orgName = data.name;
      }
    };
    watch(model, () => {
      showTree.value = true;
    });
    return {
      model,
      rules,
      isEdit,
      bind,
      on: on2,
      nameRef,
      defaultPassword,
      handleSuccess,
      handleClose,
      handleOpened,
      selectChange,
      splitRef,
      tree,
      showTree,
      onTreeChange
    };
  }
};
const _hoisted_1$h = { style: { "padding": "15px" } };
const _hoisted_2$c = { style: { "padding": "15px", "background-color": "#f8f8f9" } };
function _sfc_render$P(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_alert = resolveComponent("el-alert");
  const _component_m_admin_account_type_select = resolveComponent("m-admin-account-type-select");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_input = resolveComponent("el-input");
  const _component_el_col = resolveComponent("el-col");
  const _component_m_select = resolveComponent("m-select");
  const _component_m_admin_account_status_select = resolveComponent("m-admin-account-status-select");
  const _component_el_row = resolveComponent("el-row");
  const _component_tree_page = resolveComponent("tree-page");
  const _component_m_split = resolveComponent("m-split");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, toHandlers($setup.on), {
    onSuccess: $setup.handleSuccess,
    onClose: $setup.handleClose,
    onOpened: $setup.handleOpened
  }), {
    default: withCtx(() => [
      createVNode(_component_m_split, {
        modelValue: $setup.splitRef,
        "onUpdate:modelValue": _cache[9] || (_cache[9] = ($event) => $setup.splitRef = $event)
      }, {
        fixed: withCtx(() => [
          createElementVNode("div", _hoisted_1$h, [
            $setup.isEdit ? (openBlock(), createBlock(_component_el_alert, {
              key: 0,
              class: "m-margin-b-20",
              title: _ctx.$t("mod.admin.not_allow_edit_username"),
              type: "warning"
            }, null, 8, ["title"])) : createCommentVNode("", true),
            createVNode(_component_el_row, null, {
              default: withCtx(() => [
                createVNode(_component_el_col, { span: 24 }, {
                  default: withCtx(() => [
                    createVNode(_component_el_form_item, {
                      label: _ctx.$t("\u8D26\u6237\u7C7B\u578B"),
                      prop: "type"
                    }, {
                      default: withCtx(() => [
                        createVNode(_component_m_admin_account_type_select, {
                          modelValue: $setup.model.type,
                          "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.type = $event)
                        }, null, 8, ["modelValue"])
                      ]),
                      _: 1
                    }, 8, ["label"]),
                    createVNode(_component_el_form_item, {
                      label: _ctx.$t("tpm.login.username"),
                      prop: "username"
                    }, {
                      default: withCtx(() => [
                        createVNode(_component_el_input, {
                          ref: "nameRef",
                          modelValue: $setup.model.username,
                          "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.username = $event),
                          disabled: $setup.isEdit
                        }, null, 8, ["modelValue", "disabled"])
                      ]),
                      _: 1
                    }, 8, ["label"]),
                    createVNode(_component_el_form_item, {
                      label: _ctx.$t("mod.admin.name"),
                      prop: "name"
                    }, {
                      default: withCtx(() => [
                        createVNode(_component_el_input, {
                          modelValue: $setup.model.name,
                          "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.name = $event)
                        }, null, 8, ["modelValue"])
                      ]),
                      _: 1
                    }, 8, ["label"]),
                    createVNode(_component_el_form_item, {
                      label: _ctx.$t("tpm.phone"),
                      prop: "phone"
                    }, {
                      default: withCtx(() => [
                        createVNode(_component_el_input, {
                          modelValue: $setup.model.phone,
                          "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.phone = $event)
                        }, null, 8, ["modelValue"])
                      ]),
                      _: 1
                    }, 8, ["label"]),
                    createVNode(_component_el_form_item, {
                      label: _ctx.$t("tpm.login.password"),
                      prop: "password"
                    }, {
                      default: withCtx(() => [
                        createVNode(_component_el_input, {
                          modelValue: $setup.model.password,
                          "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.model.password = $event),
                          placeholder: `${_ctx.$t("mod.admin.default_password")}\uFF1A${$setup.defaultPassword}`,
                          disabled: $setup.isEdit
                        }, null, 8, ["modelValue", "placeholder", "disabled"])
                      ]),
                      _: 1
                    }, 8, ["label"]),
                    createVNode(_component_el_form_item, {
                      label: _ctx.$t("tpm.email"),
                      prop: "email"
                    }, {
                      default: withCtx(() => [
                        createVNode(_component_el_input, {
                          modelValue: $setup.model.email,
                          "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.model.email = $event)
                        }, null, 8, ["modelValue"])
                      ]),
                      _: 1
                    }, 8, ["label"]),
                    createVNode(_component_el_form_item, {
                      label: _ctx.$t("\u6240\u5C5E\u7EC4\u7EC7"),
                      prop: "orgId"
                    }, {
                      default: withCtx(() => [
                        createVNode(_component_el_input, {
                          modelValue: $setup.model.orgName,
                          "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => $setup.model.orgName = $event),
                          readonly: true,
                          placeholder: "\u4EE3\u8868\u7528\u6237\u6240\u5728\u7EC4\u7EC7\uFF08\u90E8\u95E8\uFF09\uFF0C\u4FBF\u4E8E\u7528\u6237\u5206\u7EA7\u7BA1\u7406"
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
                      label: _ctx.$t("tpm.role"),
                      prop: "roles"
                    }, {
                      default: withCtx(() => [
                        createVNode(_component_m_select, {
                          modelValue: $setup.model.roles,
                          "onUpdate:modelValue": _cache[7] || (_cache[7] = ($event) => $setup.model.roles = $event),
                          action: _ctx.$tpm.api.admin.role.select,
                          multiple: ""
                        }, null, 8, ["modelValue", "action"])
                      ]),
                      _: 1
                    }, 8, ["label"])
                  ]),
                  _: 1
                }),
                createVNode(_component_el_form_item, {
                  label: _ctx.$t("\u72B6\u6001"),
                  prop: "status"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_m_admin_account_status_select, {
                      modelValue: $setup.model.status,
                      "onUpdate:modelValue": _cache[8] || (_cache[8] = ($event) => $setup.model.status = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                }, 8, ["label"])
              ]),
              _: 1
            })
          ])
        ]),
        auto: withCtx(() => [
          createElementVNode("div", _hoisted_2$c, [
            $setup.showTree ? (openBlock(), createBlock(_component_tree_page, {
              key: 0,
              ref: "tree",
              onChange: $setup.onTreeChange,
              selection: $setup.model
            }, null, 8, ["onChange", "selection"])) : createCommentVNode("", true)
          ])
        ]),
        _: 1
      }, 8, ["modelValue"])
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess", "onClose", "onOpened"]);
}
var Save$5 = /* @__PURE__ */ _export_sfc$1(_sfc_main$P, [["render", _sfc_render$P]]);
var commonjsGlobal = typeof globalThis !== "undefined" ? globalThis : typeof window !== "undefined" ? window : typeof global !== "undefined" ? global : typeof self !== "undefined" ? self : {};
function getDefaultExportFromCjs(x) {
  return x && x.__esModule && Object.prototype.hasOwnProperty.call(x, "default") ? x["default"] : x;
}
function getAugmentedNamespace(n) {
  if (n.__esModule)
    return n;
  var a = Object.defineProperty({}, "__esModule", { value: true });
  Object.keys(n).forEach(function(k) {
    var d = Object.getOwnPropertyDescriptor(n, k);
    Object.defineProperty(a, k, d.get ? d : {
      enumerable: true,
      get: function() {
        return n[k];
      }
    });
  });
  return a;
}
var vuedraggable_umd = { exports: {} };
/**!
 * Sortable 1.10.2
 * @author	RubaXa   <trash@rubaxa.org>
 * @author	owenm    <owen23355@gmail.com>
 * @license MIT
 */
function _typeof(obj) {
  if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") {
    _typeof = function(obj2) {
      return typeof obj2;
    };
  } else {
    _typeof = function(obj2) {
      return obj2 && typeof Symbol === "function" && obj2.constructor === Symbol && obj2 !== Symbol.prototype ? "symbol" : typeof obj2;
    };
  }
  return _typeof(obj);
}
function _defineProperty(obj, key, value) {
  if (key in obj) {
    Object.defineProperty(obj, key, {
      value,
      enumerable: true,
      configurable: true,
      writable: true
    });
  } else {
    obj[key] = value;
  }
  return obj;
}
function _extends() {
  _extends = Object.assign || function(target) {
    for (var i = 1; i < arguments.length; i++) {
      var source = arguments[i];
      for (var key in source) {
        if (Object.prototype.hasOwnProperty.call(source, key)) {
          target[key] = source[key];
        }
      }
    }
    return target;
  };
  return _extends.apply(this, arguments);
}
function _objectSpread(target) {
  for (var i = 1; i < arguments.length; i++) {
    var source = arguments[i] != null ? arguments[i] : {};
    var ownKeys = Object.keys(source);
    if (typeof Object.getOwnPropertySymbols === "function") {
      ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function(sym) {
        return Object.getOwnPropertyDescriptor(source, sym).enumerable;
      }));
    }
    ownKeys.forEach(function(key) {
      _defineProperty(target, key, source[key]);
    });
  }
  return target;
}
function _objectWithoutPropertiesLoose(source, excluded) {
  if (source == null)
    return {};
  var target = {};
  var sourceKeys = Object.keys(source);
  var key, i;
  for (i = 0; i < sourceKeys.length; i++) {
    key = sourceKeys[i];
    if (excluded.indexOf(key) >= 0)
      continue;
    target[key] = source[key];
  }
  return target;
}
function _objectWithoutProperties(source, excluded) {
  if (source == null)
    return {};
  var target = _objectWithoutPropertiesLoose(source, excluded);
  var key, i;
  if (Object.getOwnPropertySymbols) {
    var sourceSymbolKeys = Object.getOwnPropertySymbols(source);
    for (i = 0; i < sourceSymbolKeys.length; i++) {
      key = sourceSymbolKeys[i];
      if (excluded.indexOf(key) >= 0)
        continue;
      if (!Object.prototype.propertyIsEnumerable.call(source, key))
        continue;
      target[key] = source[key];
    }
  }
  return target;
}
function _toConsumableArray(arr) {
  return _arrayWithoutHoles(arr) || _iterableToArray(arr) || _nonIterableSpread();
}
function _arrayWithoutHoles(arr) {
  if (Array.isArray(arr)) {
    for (var i = 0, arr2 = new Array(arr.length); i < arr.length; i++)
      arr2[i] = arr[i];
    return arr2;
  }
}
function _iterableToArray(iter) {
  if (Symbol.iterator in Object(iter) || Object.prototype.toString.call(iter) === "[object Arguments]")
    return Array.from(iter);
}
function _nonIterableSpread() {
  throw new TypeError("Invalid attempt to spread non-iterable instance");
}
var version$1 = "1.10.2";
function userAgent(pattern) {
  if (typeof window !== "undefined" && window.navigator) {
    return !!/* @__PURE__ */ navigator.userAgent.match(pattern);
  }
}
var IE11OrLess = userAgent(/(?:Trident.*rv[ :]?11\.|msie|iemobile|Windows Phone)/i);
var Edge = userAgent(/Edge/i);
var FireFox = userAgent(/firefox/i);
var Safari = userAgent(/safari/i) && !userAgent(/chrome/i) && !userAgent(/android/i);
var IOS = userAgent(/iP(ad|od|hone)/i);
var ChromeForAndroid = userAgent(/chrome/i) && userAgent(/android/i);
var captureMode = {
  capture: false,
  passive: false
};
function on(el, event, fn) {
  el.addEventListener(event, fn, !IE11OrLess && captureMode);
}
function off(el, event, fn) {
  el.removeEventListener(event, fn, !IE11OrLess && captureMode);
}
function matches(el, selector) {
  if (!selector)
    return;
  selector[0] === ">" && (selector = selector.substring(1));
  if (el) {
    try {
      if (el.matches) {
        return el.matches(selector);
      } else if (el.msMatchesSelector) {
        return el.msMatchesSelector(selector);
      } else if (el.webkitMatchesSelector) {
        return el.webkitMatchesSelector(selector);
      }
    } catch (_) {
      return false;
    }
  }
  return false;
}
function getParentOrHost(el) {
  return el.host && el !== document && el.host.nodeType ? el.host : el.parentNode;
}
function closest(el, selector, ctx, includeCTX) {
  if (el) {
    ctx = ctx || document;
    do {
      if (selector != null && (selector[0] === ">" ? el.parentNode === ctx && matches(el, selector) : matches(el, selector)) || includeCTX && el === ctx) {
        return el;
      }
      if (el === ctx)
        break;
    } while (el = getParentOrHost(el));
  }
  return null;
}
var R_SPACE = /\s+/g;
function toggleClass(el, name2, state2) {
  if (el && name2) {
    if (el.classList) {
      el.classList[state2 ? "add" : "remove"](name2);
    } else {
      var className = (" " + el.className + " ").replace(R_SPACE, " ").replace(" " + name2 + " ", " ");
      el.className = (className + (state2 ? " " + name2 : "")).replace(R_SPACE, " ");
    }
  }
}
function css(el, prop, val) {
  var style = el && el.style;
  if (style) {
    if (val === void 0) {
      if (document.defaultView && document.defaultView.getComputedStyle) {
        val = document.defaultView.getComputedStyle(el, "");
      } else if (el.currentStyle) {
        val = el.currentStyle;
      }
      return prop === void 0 ? val : val[prop];
    } else {
      if (!(prop in style) && prop.indexOf("webkit") === -1) {
        prop = "-webkit-" + prop;
      }
      style[prop] = val + (typeof val === "string" ? "" : "px");
    }
  }
}
function matrix(el, selfOnly) {
  var appliedTransforms = "";
  if (typeof el === "string") {
    appliedTransforms = el;
  } else {
    do {
      var transform = css(el, "transform");
      if (transform && transform !== "none") {
        appliedTransforms = transform + " " + appliedTransforms;
      }
    } while (!selfOnly && (el = el.parentNode));
  }
  var matrixFn = window.DOMMatrix || window.WebKitCSSMatrix || window.CSSMatrix || window.MSCSSMatrix;
  return matrixFn && new matrixFn(appliedTransforms);
}
function find(ctx, tagName, iterator) {
  if (ctx) {
    var list = ctx.getElementsByTagName(tagName), i = 0, n = list.length;
    if (iterator) {
      for (; i < n; i++) {
        iterator(list[i], i);
      }
    }
    return list;
  }
  return [];
}
function getWindowScrollingElement() {
  var scrollingElement = document.scrollingElement;
  if (scrollingElement) {
    return scrollingElement;
  } else {
    return document.documentElement;
  }
}
function getRect(el, relativeToContainingBlock, relativeToNonStaticParent, undoScale, container) {
  if (!el.getBoundingClientRect && el !== window)
    return;
  var elRect, top, left, bottom, right, height, width;
  if (el !== window && el !== getWindowScrollingElement()) {
    elRect = el.getBoundingClientRect();
    top = elRect.top;
    left = elRect.left;
    bottom = elRect.bottom;
    right = elRect.right;
    height = elRect.height;
    width = elRect.width;
  } else {
    top = 0;
    left = 0;
    bottom = window.innerHeight;
    right = window.innerWidth;
    height = window.innerHeight;
    width = window.innerWidth;
  }
  if ((relativeToContainingBlock || relativeToNonStaticParent) && el !== window) {
    container = container || el.parentNode;
    if (!IE11OrLess) {
      do {
        if (container && container.getBoundingClientRect && (css(container, "transform") !== "none" || relativeToNonStaticParent && css(container, "position") !== "static")) {
          var containerRect = container.getBoundingClientRect();
          top -= containerRect.top + parseInt(css(container, "border-top-width"));
          left -= containerRect.left + parseInt(css(container, "border-left-width"));
          bottom = top + elRect.height;
          right = left + elRect.width;
          break;
        }
      } while (container = container.parentNode);
    }
  }
  if (undoScale && el !== window) {
    var elMatrix = matrix(container || el), scaleX = elMatrix && elMatrix.a, scaleY = elMatrix && elMatrix.d;
    if (elMatrix) {
      top /= scaleY;
      left /= scaleX;
      width /= scaleX;
      height /= scaleY;
      bottom = top + height;
      right = left + width;
    }
  }
  return {
    top,
    left,
    bottom,
    right,
    width,
    height
  };
}
function isScrolledPast(el, elSide, parentSide) {
  var parent = getParentAutoScrollElement(el, true), elSideVal = getRect(el)[elSide];
  while (parent) {
    var parentSideVal = getRect(parent)[parentSide], visible = void 0;
    if (parentSide === "top" || parentSide === "left") {
      visible = elSideVal >= parentSideVal;
    } else {
      visible = elSideVal <= parentSideVal;
    }
    if (!visible)
      return parent;
    if (parent === getWindowScrollingElement())
      break;
    parent = getParentAutoScrollElement(parent, false);
  }
  return false;
}
function getChild(el, childNum, options) {
  var currentChild = 0, i = 0, children = el.children;
  while (i < children.length) {
    if (children[i].style.display !== "none" && children[i] !== Sortable.ghost && children[i] !== Sortable.dragged && closest(children[i], options.draggable, el, false)) {
      if (currentChild === childNum) {
        return children[i];
      }
      currentChild++;
    }
    i++;
  }
  return null;
}
function lastChild(el, selector) {
  var last = el.lastElementChild;
  while (last && (last === Sortable.ghost || css(last, "display") === "none" || selector && !matches(last, selector))) {
    last = last.previousElementSibling;
  }
  return last || null;
}
function index(el, selector) {
  var index2 = 0;
  if (!el || !el.parentNode) {
    return -1;
  }
  while (el = el.previousElementSibling) {
    if (el.nodeName.toUpperCase() !== "TEMPLATE" && el !== Sortable.clone && (!selector || matches(el, selector))) {
      index2++;
    }
  }
  return index2;
}
function getRelativeScrollOffset(el) {
  var offsetLeft = 0, offsetTop = 0, winScroller = getWindowScrollingElement();
  if (el) {
    do {
      var elMatrix = matrix(el), scaleX = elMatrix.a, scaleY = elMatrix.d;
      offsetLeft += el.scrollLeft * scaleX;
      offsetTop += el.scrollTop * scaleY;
    } while (el !== winScroller && (el = el.parentNode));
  }
  return [offsetLeft, offsetTop];
}
function indexOfObject(arr, obj) {
  for (var i in arr) {
    if (!arr.hasOwnProperty(i))
      continue;
    for (var key in obj) {
      if (obj.hasOwnProperty(key) && obj[key] === arr[i][key])
        return Number(i);
    }
  }
  return -1;
}
function getParentAutoScrollElement(el, includeSelf) {
  if (!el || !el.getBoundingClientRect)
    return getWindowScrollingElement();
  var elem = el;
  var gotSelf = false;
  do {
    if (elem.clientWidth < elem.scrollWidth || elem.clientHeight < elem.scrollHeight) {
      var elemCSS = css(elem);
      if (elem.clientWidth < elem.scrollWidth && (elemCSS.overflowX == "auto" || elemCSS.overflowX == "scroll") || elem.clientHeight < elem.scrollHeight && (elemCSS.overflowY == "auto" || elemCSS.overflowY == "scroll")) {
        if (!elem.getBoundingClientRect || elem === document.body)
          return getWindowScrollingElement();
        if (gotSelf || includeSelf)
          return elem;
        gotSelf = true;
      }
    }
  } while (elem = elem.parentNode);
  return getWindowScrollingElement();
}
function extend(dst, src) {
  if (dst && src) {
    for (var key in src) {
      if (src.hasOwnProperty(key)) {
        dst[key] = src[key];
      }
    }
  }
  return dst;
}
function isRectEqual(rect1, rect2) {
  return Math.round(rect1.top) === Math.round(rect2.top) && Math.round(rect1.left) === Math.round(rect2.left) && Math.round(rect1.height) === Math.round(rect2.height) && Math.round(rect1.width) === Math.round(rect2.width);
}
var _throttleTimeout;
function throttle(callback, ms) {
  return function() {
    if (!_throttleTimeout) {
      var args = arguments, _this = this;
      if (args.length === 1) {
        callback.call(_this, args[0]);
      } else {
        callback.apply(_this, args);
      }
      _throttleTimeout = setTimeout(function() {
        _throttleTimeout = void 0;
      }, ms);
    }
  };
}
function cancelThrottle() {
  clearTimeout(_throttleTimeout);
  _throttleTimeout = void 0;
}
function scrollBy(el, x, y) {
  el.scrollLeft += x;
  el.scrollTop += y;
}
function clone(el) {
  var Polymer = window.Polymer;
  var $ = window.jQuery || window.Zepto;
  if (Polymer && Polymer.dom) {
    return Polymer.dom(el).cloneNode(true);
  } else if ($) {
    return $(el).clone(true)[0];
  } else {
    return el.cloneNode(true);
  }
}
function setRect(el, rect) {
  css(el, "position", "absolute");
  css(el, "top", rect.top);
  css(el, "left", rect.left);
  css(el, "width", rect.width);
  css(el, "height", rect.height);
}
function unsetRect(el) {
  css(el, "position", "");
  css(el, "top", "");
  css(el, "left", "");
  css(el, "width", "");
  css(el, "height", "");
}
var expando = "Sortable" + new Date().getTime();
function AnimationStateManager() {
  var animationStates = [], animationCallbackId;
  return {
    captureAnimationState: function captureAnimationState() {
      animationStates = [];
      if (!this.options.animation)
        return;
      var children = [].slice.call(this.el.children);
      children.forEach(function(child) {
        if (css(child, "display") === "none" || child === Sortable.ghost)
          return;
        animationStates.push({
          target: child,
          rect: getRect(child)
        });
        var fromRect = _objectSpread({}, animationStates[animationStates.length - 1].rect);
        if (child.thisAnimationDuration) {
          var childMatrix = matrix(child, true);
          if (childMatrix) {
            fromRect.top -= childMatrix.f;
            fromRect.left -= childMatrix.e;
          }
        }
        child.fromRect = fromRect;
      });
    },
    addAnimationState: function addAnimationState(state2) {
      animationStates.push(state2);
    },
    removeAnimationState: function removeAnimationState(target) {
      animationStates.splice(indexOfObject(animationStates, {
        target
      }), 1);
    },
    animateAll: function animateAll(callback) {
      var _this = this;
      if (!this.options.animation) {
        clearTimeout(animationCallbackId);
        if (typeof callback === "function")
          callback();
        return;
      }
      var animating = false, animationTime = 0;
      animationStates.forEach(function(state2) {
        var time = 0, target = state2.target, fromRect = target.fromRect, toRect = getRect(target), prevFromRect = target.prevFromRect, prevToRect = target.prevToRect, animatingRect = state2.rect, targetMatrix = matrix(target, true);
        if (targetMatrix) {
          toRect.top -= targetMatrix.f;
          toRect.left -= targetMatrix.e;
        }
        target.toRect = toRect;
        if (target.thisAnimationDuration) {
          if (isRectEqual(prevFromRect, toRect) && !isRectEqual(fromRect, toRect) && (animatingRect.top - toRect.top) / (animatingRect.left - toRect.left) === (fromRect.top - toRect.top) / (fromRect.left - toRect.left)) {
            time = calculateRealTime(animatingRect, prevFromRect, prevToRect, _this.options);
          }
        }
        if (!isRectEqual(toRect, fromRect)) {
          target.prevFromRect = fromRect;
          target.prevToRect = toRect;
          if (!time) {
            time = _this.options.animation;
          }
          _this.animate(target, animatingRect, toRect, time);
        }
        if (time) {
          animating = true;
          animationTime = Math.max(animationTime, time);
          clearTimeout(target.animationResetTimer);
          target.animationResetTimer = setTimeout(function() {
            target.animationTime = 0;
            target.prevFromRect = null;
            target.fromRect = null;
            target.prevToRect = null;
            target.thisAnimationDuration = null;
          }, time);
          target.thisAnimationDuration = time;
        }
      });
      clearTimeout(animationCallbackId);
      if (!animating) {
        if (typeof callback === "function")
          callback();
      } else {
        animationCallbackId = setTimeout(function() {
          if (typeof callback === "function")
            callback();
        }, animationTime);
      }
      animationStates = [];
    },
    animate: function animate(target, currentRect, toRect, duration) {
      if (duration) {
        css(target, "transition", "");
        css(target, "transform", "");
        var elMatrix = matrix(this.el), scaleX = elMatrix && elMatrix.a, scaleY = elMatrix && elMatrix.d, translateX = (currentRect.left - toRect.left) / (scaleX || 1), translateY = (currentRect.top - toRect.top) / (scaleY || 1);
        target.animatingX = !!translateX;
        target.animatingY = !!translateY;
        css(target, "transform", "translate3d(" + translateX + "px," + translateY + "px,0)");
        repaint(target);
        css(target, "transition", "transform " + duration + "ms" + (this.options.easing ? " " + this.options.easing : ""));
        css(target, "transform", "translate3d(0,0,0)");
        typeof target.animated === "number" && clearTimeout(target.animated);
        target.animated = setTimeout(function() {
          css(target, "transition", "");
          css(target, "transform", "");
          target.animated = false;
          target.animatingX = false;
          target.animatingY = false;
        }, duration);
      }
    }
  };
}
function repaint(target) {
  return target.offsetWidth;
}
function calculateRealTime(animatingRect, fromRect, toRect, options) {
  return Math.sqrt(Math.pow(fromRect.top - animatingRect.top, 2) + Math.pow(fromRect.left - animatingRect.left, 2)) / Math.sqrt(Math.pow(fromRect.top - toRect.top, 2) + Math.pow(fromRect.left - toRect.left, 2)) * options.animation;
}
var plugins = [];
var defaults = {
  initializeByDefault: true
};
var PluginManager = {
  mount: function mount(plugin) {
    for (var option2 in defaults) {
      if (defaults.hasOwnProperty(option2) && !(option2 in plugin)) {
        plugin[option2] = defaults[option2];
      }
    }
    plugins.push(plugin);
  },
  pluginEvent: function pluginEvent(eventName, sortable, evt) {
    var _this = this;
    this.eventCanceled = false;
    evt.cancel = function() {
      _this.eventCanceled = true;
    };
    var eventNameGlobal = eventName + "Global";
    plugins.forEach(function(plugin) {
      if (!sortable[plugin.pluginName])
        return;
      if (sortable[plugin.pluginName][eventNameGlobal]) {
        sortable[plugin.pluginName][eventNameGlobal](_objectSpread({
          sortable
        }, evt));
      }
      if (sortable.options[plugin.pluginName] && sortable[plugin.pluginName][eventName]) {
        sortable[plugin.pluginName][eventName](_objectSpread({
          sortable
        }, evt));
      }
    });
  },
  initializePlugins: function initializePlugins(sortable, el, defaults2, options) {
    plugins.forEach(function(plugin) {
      var pluginName = plugin.pluginName;
      if (!sortable.options[pluginName] && !plugin.initializeByDefault)
        return;
      var initialized = new plugin(sortable, el, sortable.options);
      initialized.sortable = sortable;
      initialized.options = sortable.options;
      sortable[pluginName] = initialized;
      _extends(defaults2, initialized.defaults);
    });
    for (var option2 in sortable.options) {
      if (!sortable.options.hasOwnProperty(option2))
        continue;
      var modified = this.modifyOption(sortable, option2, sortable.options[option2]);
      if (typeof modified !== "undefined") {
        sortable.options[option2] = modified;
      }
    }
  },
  getEventProperties: function getEventProperties(name2, sortable) {
    var eventProperties = {};
    plugins.forEach(function(plugin) {
      if (typeof plugin.eventProperties !== "function")
        return;
      _extends(eventProperties, plugin.eventProperties.call(sortable[plugin.pluginName], name2));
    });
    return eventProperties;
  },
  modifyOption: function modifyOption(sortable, name2, value) {
    var modifiedValue;
    plugins.forEach(function(plugin) {
      if (!sortable[plugin.pluginName])
        return;
      if (plugin.optionListeners && typeof plugin.optionListeners[name2] === "function") {
        modifiedValue = plugin.optionListeners[name2].call(sortable[plugin.pluginName], value);
      }
    });
    return modifiedValue;
  }
};
function dispatchEvent(_ref) {
  var sortable = _ref.sortable, rootEl2 = _ref.rootEl, name2 = _ref.name, targetEl = _ref.targetEl, cloneEl2 = _ref.cloneEl, toEl = _ref.toEl, fromEl = _ref.fromEl, oldIndex2 = _ref.oldIndex, newIndex2 = _ref.newIndex, oldDraggableIndex2 = _ref.oldDraggableIndex, newDraggableIndex2 = _ref.newDraggableIndex, originalEvent = _ref.originalEvent, putSortable2 = _ref.putSortable, extraEventProperties = _ref.extraEventProperties;
  sortable = sortable || rootEl2 && rootEl2[expando];
  if (!sortable)
    return;
  var evt, options = sortable.options, onName = "on" + name2.charAt(0).toUpperCase() + name2.substr(1);
  if (window.CustomEvent && !IE11OrLess && !Edge) {
    evt = new CustomEvent(name2, {
      bubbles: true,
      cancelable: true
    });
  } else {
    evt = document.createEvent("Event");
    evt.initEvent(name2, true, true);
  }
  evt.to = toEl || rootEl2;
  evt.from = fromEl || rootEl2;
  evt.item = targetEl || rootEl2;
  evt.clone = cloneEl2;
  evt.oldIndex = oldIndex2;
  evt.newIndex = newIndex2;
  evt.oldDraggableIndex = oldDraggableIndex2;
  evt.newDraggableIndex = newDraggableIndex2;
  evt.originalEvent = originalEvent;
  evt.pullMode = putSortable2 ? putSortable2.lastPutMode : void 0;
  var allEventProperties = _objectSpread({}, extraEventProperties, PluginManager.getEventProperties(name2, sortable));
  for (var option2 in allEventProperties) {
    evt[option2] = allEventProperties[option2];
  }
  if (rootEl2) {
    rootEl2.dispatchEvent(evt);
  }
  if (options[onName]) {
    options[onName].call(sortable, evt);
  }
}
var pluginEvent2 = function pluginEvent3(eventName, sortable) {
  var _ref = arguments.length > 2 && arguments[2] !== void 0 ? arguments[2] : {}, originalEvent = _ref.evt, data = _objectWithoutProperties(_ref, ["evt"]);
  PluginManager.pluginEvent.bind(Sortable)(eventName, sortable, _objectSpread({
    dragEl,
    parentEl,
    ghostEl,
    rootEl,
    nextEl,
    lastDownEl,
    cloneEl,
    cloneHidden,
    dragStarted: moved,
    putSortable,
    activeSortable: Sortable.active,
    originalEvent,
    oldIndex,
    oldDraggableIndex,
    newIndex,
    newDraggableIndex,
    hideGhostForTarget: _hideGhostForTarget,
    unhideGhostForTarget: _unhideGhostForTarget,
    cloneNowHidden: function cloneNowHidden() {
      cloneHidden = true;
    },
    cloneNowShown: function cloneNowShown() {
      cloneHidden = false;
    },
    dispatchSortableEvent: function dispatchSortableEvent(name2) {
      _dispatchEvent({
        sortable,
        name: name2,
        originalEvent
      });
    }
  }, data));
};
function _dispatchEvent(info) {
  dispatchEvent(_objectSpread({
    putSortable,
    cloneEl,
    targetEl: dragEl,
    rootEl,
    oldIndex,
    oldDraggableIndex,
    newIndex,
    newDraggableIndex
  }, info));
}
var dragEl, parentEl, ghostEl, rootEl, nextEl, lastDownEl, cloneEl, cloneHidden, oldIndex, newIndex, oldDraggableIndex, newDraggableIndex, activeGroup, putSortable, awaitingDragStarted = false, ignoreNextClick = false, sortables = [], tapEvt, touchEvt, lastDx, lastDy, tapDistanceLeft, tapDistanceTop, moved, lastTarget, lastDirection, pastFirstInvertThresh = false, isCircumstantialInvert = false, targetMoveDistance, ghostRelativeParent, ghostRelativeParentInitialScroll = [], _silent = false, savedInputChecked = [];
var documentExists = typeof document !== "undefined", PositionGhostAbsolutely = IOS, CSSFloatProperty = Edge || IE11OrLess ? "cssFloat" : "float", supportDraggable = documentExists && !ChromeForAndroid && !IOS && "draggable" in document.createElement("div"), supportCssPointerEvents = function() {
  if (!documentExists)
    return;
  if (IE11OrLess) {
    return false;
  }
  var el = document.createElement("x");
  el.style.cssText = "pointer-events:auto";
  return el.style.pointerEvents === "auto";
}(), _detectDirection = function _detectDirection2(el, options) {
  var elCSS = css(el), elWidth = parseInt(elCSS.width) - parseInt(elCSS.paddingLeft) - parseInt(elCSS.paddingRight) - parseInt(elCSS.borderLeftWidth) - parseInt(elCSS.borderRightWidth), child1 = getChild(el, 0, options), child2 = getChild(el, 1, options), firstChildCSS = child1 && css(child1), secondChildCSS = child2 && css(child2), firstChildWidth = firstChildCSS && parseInt(firstChildCSS.marginLeft) + parseInt(firstChildCSS.marginRight) + getRect(child1).width, secondChildWidth = secondChildCSS && parseInt(secondChildCSS.marginLeft) + parseInt(secondChildCSS.marginRight) + getRect(child2).width;
  if (elCSS.display === "flex") {
    return elCSS.flexDirection === "column" || elCSS.flexDirection === "column-reverse" ? "vertical" : "horizontal";
  }
  if (elCSS.display === "grid") {
    return elCSS.gridTemplateColumns.split(" ").length <= 1 ? "vertical" : "horizontal";
  }
  if (child1 && firstChildCSS["float"] && firstChildCSS["float"] !== "none") {
    var touchingSideChild2 = firstChildCSS["float"] === "left" ? "left" : "right";
    return child2 && (secondChildCSS.clear === "both" || secondChildCSS.clear === touchingSideChild2) ? "vertical" : "horizontal";
  }
  return child1 && (firstChildCSS.display === "block" || firstChildCSS.display === "flex" || firstChildCSS.display === "table" || firstChildCSS.display === "grid" || firstChildWidth >= elWidth && elCSS[CSSFloatProperty] === "none" || child2 && elCSS[CSSFloatProperty] === "none" && firstChildWidth + secondChildWidth > elWidth) ? "vertical" : "horizontal";
}, _dragElInRowColumn = function _dragElInRowColumn2(dragRect, targetRect, vertical) {
  var dragElS1Opp = vertical ? dragRect.left : dragRect.top, dragElS2Opp = vertical ? dragRect.right : dragRect.bottom, dragElOppLength = vertical ? dragRect.width : dragRect.height, targetS1Opp = vertical ? targetRect.left : targetRect.top, targetS2Opp = vertical ? targetRect.right : targetRect.bottom, targetOppLength = vertical ? targetRect.width : targetRect.height;
  return dragElS1Opp === targetS1Opp || dragElS2Opp === targetS2Opp || dragElS1Opp + dragElOppLength / 2 === targetS1Opp + targetOppLength / 2;
}, _detectNearestEmptySortable = function _detectNearestEmptySortable2(x, y) {
  var ret;
  sortables.some(function(sortable) {
    if (lastChild(sortable))
      return;
    var rect = getRect(sortable), threshold = sortable[expando].options.emptyInsertThreshold, insideHorizontally = x >= rect.left - threshold && x <= rect.right + threshold, insideVertically = y >= rect.top - threshold && y <= rect.bottom + threshold;
    if (threshold && insideHorizontally && insideVertically) {
      return ret = sortable;
    }
  });
  return ret;
}, _prepareGroup = function _prepareGroup2(options) {
  function toFn(value, pull) {
    return function(to, from, dragEl2, evt) {
      var sameGroup = to.options.group.name && from.options.group.name && to.options.group.name === from.options.group.name;
      if (value == null && (pull || sameGroup)) {
        return true;
      } else if (value == null || value === false) {
        return false;
      } else if (pull && value === "clone") {
        return value;
      } else if (typeof value === "function") {
        return toFn(value(to, from, dragEl2, evt), pull)(to, from, dragEl2, evt);
      } else {
        var otherGroup = (pull ? to : from).options.group.name;
        return value === true || typeof value === "string" && value === otherGroup || value.join && value.indexOf(otherGroup) > -1;
      }
    };
  }
  var group = {};
  var originalGroup = options.group;
  if (!originalGroup || _typeof(originalGroup) != "object") {
    originalGroup = {
      name: originalGroup
    };
  }
  group.name = originalGroup.name;
  group.checkPull = toFn(originalGroup.pull, true);
  group.checkPut = toFn(originalGroup.put);
  group.revertClone = originalGroup.revertClone;
  options.group = group;
}, _hideGhostForTarget = function _hideGhostForTarget2() {
  if (!supportCssPointerEvents && ghostEl) {
    css(ghostEl, "display", "none");
  }
}, _unhideGhostForTarget = function _unhideGhostForTarget2() {
  if (!supportCssPointerEvents && ghostEl) {
    css(ghostEl, "display", "");
  }
};
if (documentExists) {
  document.addEventListener("click", function(evt) {
    if (ignoreNextClick) {
      evt.preventDefault();
      evt.stopPropagation && evt.stopPropagation();
      evt.stopImmediatePropagation && evt.stopImmediatePropagation();
      ignoreNextClick = false;
      return false;
    }
  }, true);
}
var nearestEmptyInsertDetectEvent = function nearestEmptyInsertDetectEvent2(evt) {
  if (dragEl) {
    evt = evt.touches ? evt.touches[0] : evt;
    var nearest = _detectNearestEmptySortable(evt.clientX, evt.clientY);
    if (nearest) {
      var event = {};
      for (var i in evt) {
        if (evt.hasOwnProperty(i)) {
          event[i] = evt[i];
        }
      }
      event.target = event.rootEl = nearest;
      event.preventDefault = void 0;
      event.stopPropagation = void 0;
      nearest[expando]._onDragOver(event);
    }
  }
};
var _checkOutsideTargetEl = function _checkOutsideTargetEl2(evt) {
  if (dragEl) {
    dragEl.parentNode[expando]._isOutsideThisEl(evt.target);
  }
};
function Sortable(el, options) {
  if (!(el && el.nodeType && el.nodeType === 1)) {
    throw "Sortable: `el` must be an HTMLElement, not ".concat({}.toString.call(el));
  }
  this.el = el;
  this.options = options = _extends({}, options);
  el[expando] = this;
  var defaults2 = {
    group: null,
    sort: true,
    disabled: false,
    store: null,
    handle: null,
    draggable: /^[uo]l$/i.test(el.nodeName) ? ">li" : ">*",
    swapThreshold: 1,
    invertSwap: false,
    invertedSwapThreshold: null,
    removeCloneOnHide: true,
    direction: function direction() {
      return _detectDirection(el, this.options);
    },
    ghostClass: "sortable-ghost",
    chosenClass: "sortable-chosen",
    dragClass: "sortable-drag",
    ignore: "a, img",
    filter: null,
    preventOnFilter: true,
    animation: 0,
    easing: null,
    setData: function setData(dataTransfer, dragEl2) {
      dataTransfer.setData("Text", dragEl2.textContent);
    },
    dropBubble: false,
    dragoverBubble: false,
    dataIdAttr: "data-id",
    delay: 0,
    delayOnTouchOnly: false,
    touchStartThreshold: (Number.parseInt ? Number : window).parseInt(window.devicePixelRatio, 10) || 1,
    forceFallback: false,
    fallbackClass: "sortable-fallback",
    fallbackOnBody: false,
    fallbackTolerance: 0,
    fallbackOffset: {
      x: 0,
      y: 0
    },
    supportPointer: Sortable.supportPointer !== false && "PointerEvent" in window,
    emptyInsertThreshold: 5
  };
  PluginManager.initializePlugins(this, el, defaults2);
  for (var name2 in defaults2) {
    !(name2 in options) && (options[name2] = defaults2[name2]);
  }
  _prepareGroup(options);
  for (var fn in this) {
    if (fn.charAt(0) === "_" && typeof this[fn] === "function") {
      this[fn] = this[fn].bind(this);
    }
  }
  this.nativeDraggable = options.forceFallback ? false : supportDraggable;
  if (this.nativeDraggable) {
    this.options.touchStartThreshold = 1;
  }
  if (options.supportPointer) {
    on(el, "pointerdown", this._onTapStart);
  } else {
    on(el, "mousedown", this._onTapStart);
    on(el, "touchstart", this._onTapStart);
  }
  if (this.nativeDraggable) {
    on(el, "dragover", this);
    on(el, "dragenter", this);
  }
  sortables.push(this.el);
  options.store && options.store.get && this.sort(options.store.get(this) || []);
  _extends(this, AnimationStateManager());
}
Sortable.prototype = {
  constructor: Sortable,
  _isOutsideThisEl: function _isOutsideThisEl(target) {
    if (!this.el.contains(target) && target !== this.el) {
      lastTarget = null;
    }
  },
  _getDirection: function _getDirection(evt, target) {
    return typeof this.options.direction === "function" ? this.options.direction.call(this, evt, target, dragEl) : this.options.direction;
  },
  _onTapStart: function _onTapStart(evt) {
    if (!evt.cancelable)
      return;
    var _this = this, el = this.el, options = this.options, preventOnFilter = options.preventOnFilter, type = evt.type, touch = evt.touches && evt.touches[0] || evt.pointerType && evt.pointerType === "touch" && evt, target = (touch || evt).target, originalTarget = evt.target.shadowRoot && (evt.path && evt.path[0] || evt.composedPath && evt.composedPath()[0]) || target, filter = options.filter;
    _saveInputCheckedState(el);
    if (dragEl) {
      return;
    }
    if (/mousedown|pointerdown/.test(type) && evt.button !== 0 || options.disabled) {
      return;
    }
    if (originalTarget.isContentEditable) {
      return;
    }
    target = closest(target, options.draggable, el, false);
    if (target && target.animated) {
      return;
    }
    if (lastDownEl === target) {
      return;
    }
    oldIndex = index(target);
    oldDraggableIndex = index(target, options.draggable);
    if (typeof filter === "function") {
      if (filter.call(this, evt, target, this)) {
        _dispatchEvent({
          sortable: _this,
          rootEl: originalTarget,
          name: "filter",
          targetEl: target,
          toEl: el,
          fromEl: el
        });
        pluginEvent2("filter", _this, {
          evt
        });
        preventOnFilter && evt.cancelable && evt.preventDefault();
        return;
      }
    } else if (filter) {
      filter = filter.split(",").some(function(criteria) {
        criteria = closest(originalTarget, criteria.trim(), el, false);
        if (criteria) {
          _dispatchEvent({
            sortable: _this,
            rootEl: criteria,
            name: "filter",
            targetEl: target,
            fromEl: el,
            toEl: el
          });
          pluginEvent2("filter", _this, {
            evt
          });
          return true;
        }
      });
      if (filter) {
        preventOnFilter && evt.cancelable && evt.preventDefault();
        return;
      }
    }
    if (options.handle && !closest(originalTarget, options.handle, el, false)) {
      return;
    }
    this._prepareDragStart(evt, touch, target);
  },
  _prepareDragStart: function _prepareDragStart(evt, touch, target) {
    var _this = this, el = _this.el, options = _this.options, ownerDocument = el.ownerDocument, dragStartFn;
    if (target && !dragEl && target.parentNode === el) {
      var dragRect = getRect(target);
      rootEl = el;
      dragEl = target;
      parentEl = dragEl.parentNode;
      nextEl = dragEl.nextSibling;
      lastDownEl = target;
      activeGroup = options.group;
      Sortable.dragged = dragEl;
      tapEvt = {
        target: dragEl,
        clientX: (touch || evt).clientX,
        clientY: (touch || evt).clientY
      };
      tapDistanceLeft = tapEvt.clientX - dragRect.left;
      tapDistanceTop = tapEvt.clientY - dragRect.top;
      this._lastX = (touch || evt).clientX;
      this._lastY = (touch || evt).clientY;
      dragEl.style["will-change"] = "all";
      dragStartFn = function dragStartFn2() {
        pluginEvent2("delayEnded", _this, {
          evt
        });
        if (Sortable.eventCanceled) {
          _this._onDrop();
          return;
        }
        _this._disableDelayedDragEvents();
        if (!FireFox && _this.nativeDraggable) {
          dragEl.draggable = true;
        }
        _this._triggerDragStart(evt, touch);
        _dispatchEvent({
          sortable: _this,
          name: "choose",
          originalEvent: evt
        });
        toggleClass(dragEl, options.chosenClass, true);
      };
      options.ignore.split(",").forEach(function(criteria) {
        find(dragEl, criteria.trim(), _disableDraggable);
      });
      on(ownerDocument, "dragover", nearestEmptyInsertDetectEvent);
      on(ownerDocument, "mousemove", nearestEmptyInsertDetectEvent);
      on(ownerDocument, "touchmove", nearestEmptyInsertDetectEvent);
      on(ownerDocument, "mouseup", _this._onDrop);
      on(ownerDocument, "touchend", _this._onDrop);
      on(ownerDocument, "touchcancel", _this._onDrop);
      if (FireFox && this.nativeDraggable) {
        this.options.touchStartThreshold = 4;
        dragEl.draggable = true;
      }
      pluginEvent2("delayStart", this, {
        evt
      });
      if (options.delay && (!options.delayOnTouchOnly || touch) && (!this.nativeDraggable || !(Edge || IE11OrLess))) {
        if (Sortable.eventCanceled) {
          this._onDrop();
          return;
        }
        on(ownerDocument, "mouseup", _this._disableDelayedDrag);
        on(ownerDocument, "touchend", _this._disableDelayedDrag);
        on(ownerDocument, "touchcancel", _this._disableDelayedDrag);
        on(ownerDocument, "mousemove", _this._delayedDragTouchMoveHandler);
        on(ownerDocument, "touchmove", _this._delayedDragTouchMoveHandler);
        options.supportPointer && on(ownerDocument, "pointermove", _this._delayedDragTouchMoveHandler);
        _this._dragStartTimer = setTimeout(dragStartFn, options.delay);
      } else {
        dragStartFn();
      }
    }
  },
  _delayedDragTouchMoveHandler: function _delayedDragTouchMoveHandler(e) {
    var touch = e.touches ? e.touches[0] : e;
    if (Math.max(Math.abs(touch.clientX - this._lastX), Math.abs(touch.clientY - this._lastY)) >= Math.floor(this.options.touchStartThreshold / (this.nativeDraggable && window.devicePixelRatio || 1))) {
      this._disableDelayedDrag();
    }
  },
  _disableDelayedDrag: function _disableDelayedDrag() {
    dragEl && _disableDraggable(dragEl);
    clearTimeout(this._dragStartTimer);
    this._disableDelayedDragEvents();
  },
  _disableDelayedDragEvents: function _disableDelayedDragEvents() {
    var ownerDocument = this.el.ownerDocument;
    off(ownerDocument, "mouseup", this._disableDelayedDrag);
    off(ownerDocument, "touchend", this._disableDelayedDrag);
    off(ownerDocument, "touchcancel", this._disableDelayedDrag);
    off(ownerDocument, "mousemove", this._delayedDragTouchMoveHandler);
    off(ownerDocument, "touchmove", this._delayedDragTouchMoveHandler);
    off(ownerDocument, "pointermove", this._delayedDragTouchMoveHandler);
  },
  _triggerDragStart: function _triggerDragStart(evt, touch) {
    touch = touch || evt.pointerType == "touch" && evt;
    if (!this.nativeDraggable || touch) {
      if (this.options.supportPointer) {
        on(document, "pointermove", this._onTouchMove);
      } else if (touch) {
        on(document, "touchmove", this._onTouchMove);
      } else {
        on(document, "mousemove", this._onTouchMove);
      }
    } else {
      on(dragEl, "dragend", this);
      on(rootEl, "dragstart", this._onDragStart);
    }
    try {
      if (document.selection) {
        _nextTick(function() {
          document.selection.empty();
        });
      } else {
        window.getSelection().removeAllRanges();
      }
    } catch (err) {
    }
  },
  _dragStarted: function _dragStarted(fallback, evt) {
    awaitingDragStarted = false;
    if (rootEl && dragEl) {
      pluginEvent2("dragStarted", this, {
        evt
      });
      if (this.nativeDraggable) {
        on(document, "dragover", _checkOutsideTargetEl);
      }
      var options = this.options;
      !fallback && toggleClass(dragEl, options.dragClass, false);
      toggleClass(dragEl, options.ghostClass, true);
      Sortable.active = this;
      fallback && this._appendGhost();
      _dispatchEvent({
        sortable: this,
        name: "start",
        originalEvent: evt
      });
    } else {
      this._nulling();
    }
  },
  _emulateDragOver: function _emulateDragOver() {
    if (touchEvt) {
      this._lastX = touchEvt.clientX;
      this._lastY = touchEvt.clientY;
      _hideGhostForTarget();
      var target = document.elementFromPoint(touchEvt.clientX, touchEvt.clientY);
      var parent = target;
      while (target && target.shadowRoot) {
        target = target.shadowRoot.elementFromPoint(touchEvt.clientX, touchEvt.clientY);
        if (target === parent)
          break;
        parent = target;
      }
      dragEl.parentNode[expando]._isOutsideThisEl(target);
      if (parent) {
        do {
          if (parent[expando]) {
            var inserted = void 0;
            inserted = parent[expando]._onDragOver({
              clientX: touchEvt.clientX,
              clientY: touchEvt.clientY,
              target,
              rootEl: parent
            });
            if (inserted && !this.options.dragoverBubble) {
              break;
            }
          }
          target = parent;
        } while (parent = parent.parentNode);
      }
      _unhideGhostForTarget();
    }
  },
  _onTouchMove: function _onTouchMove(evt) {
    if (tapEvt) {
      var options = this.options, fallbackTolerance = options.fallbackTolerance, fallbackOffset = options.fallbackOffset, touch = evt.touches ? evt.touches[0] : evt, ghostMatrix = ghostEl && matrix(ghostEl, true), scaleX = ghostEl && ghostMatrix && ghostMatrix.a, scaleY = ghostEl && ghostMatrix && ghostMatrix.d, relativeScrollOffset = PositionGhostAbsolutely && ghostRelativeParent && getRelativeScrollOffset(ghostRelativeParent), dx = (touch.clientX - tapEvt.clientX + fallbackOffset.x) / (scaleX || 1) + (relativeScrollOffset ? relativeScrollOffset[0] - ghostRelativeParentInitialScroll[0] : 0) / (scaleX || 1), dy = (touch.clientY - tapEvt.clientY + fallbackOffset.y) / (scaleY || 1) + (relativeScrollOffset ? relativeScrollOffset[1] - ghostRelativeParentInitialScroll[1] : 0) / (scaleY || 1);
      if (!Sortable.active && !awaitingDragStarted) {
        if (fallbackTolerance && Math.max(Math.abs(touch.clientX - this._lastX), Math.abs(touch.clientY - this._lastY)) < fallbackTolerance) {
          return;
        }
        this._onDragStart(evt, true);
      }
      if (ghostEl) {
        if (ghostMatrix) {
          ghostMatrix.e += dx - (lastDx || 0);
          ghostMatrix.f += dy - (lastDy || 0);
        } else {
          ghostMatrix = {
            a: 1,
            b: 0,
            c: 0,
            d: 1,
            e: dx,
            f: dy
          };
        }
        var cssMatrix = "matrix(".concat(ghostMatrix.a, ",").concat(ghostMatrix.b, ",").concat(ghostMatrix.c, ",").concat(ghostMatrix.d, ",").concat(ghostMatrix.e, ",").concat(ghostMatrix.f, ")");
        css(ghostEl, "webkitTransform", cssMatrix);
        css(ghostEl, "mozTransform", cssMatrix);
        css(ghostEl, "msTransform", cssMatrix);
        css(ghostEl, "transform", cssMatrix);
        lastDx = dx;
        lastDy = dy;
        touchEvt = touch;
      }
      evt.cancelable && evt.preventDefault();
    }
  },
  _appendGhost: function _appendGhost() {
    if (!ghostEl) {
      var container = this.options.fallbackOnBody ? document.body : rootEl, rect = getRect(dragEl, true, PositionGhostAbsolutely, true, container), options = this.options;
      if (PositionGhostAbsolutely) {
        ghostRelativeParent = container;
        while (css(ghostRelativeParent, "position") === "static" && css(ghostRelativeParent, "transform") === "none" && ghostRelativeParent !== document) {
          ghostRelativeParent = ghostRelativeParent.parentNode;
        }
        if (ghostRelativeParent !== document.body && ghostRelativeParent !== document.documentElement) {
          if (ghostRelativeParent === document)
            ghostRelativeParent = getWindowScrollingElement();
          rect.top += ghostRelativeParent.scrollTop;
          rect.left += ghostRelativeParent.scrollLeft;
        } else {
          ghostRelativeParent = getWindowScrollingElement();
        }
        ghostRelativeParentInitialScroll = getRelativeScrollOffset(ghostRelativeParent);
      }
      ghostEl = dragEl.cloneNode(true);
      toggleClass(ghostEl, options.ghostClass, false);
      toggleClass(ghostEl, options.fallbackClass, true);
      toggleClass(ghostEl, options.dragClass, true);
      css(ghostEl, "transition", "");
      css(ghostEl, "transform", "");
      css(ghostEl, "box-sizing", "border-box");
      css(ghostEl, "margin", 0);
      css(ghostEl, "top", rect.top);
      css(ghostEl, "left", rect.left);
      css(ghostEl, "width", rect.width);
      css(ghostEl, "height", rect.height);
      css(ghostEl, "opacity", "0.8");
      css(ghostEl, "position", PositionGhostAbsolutely ? "absolute" : "fixed");
      css(ghostEl, "zIndex", "100000");
      css(ghostEl, "pointerEvents", "none");
      Sortable.ghost = ghostEl;
      container.appendChild(ghostEl);
      css(ghostEl, "transform-origin", tapDistanceLeft / parseInt(ghostEl.style.width) * 100 + "% " + tapDistanceTop / parseInt(ghostEl.style.height) * 100 + "%");
    }
  },
  _onDragStart: function _onDragStart(evt, fallback) {
    var _this = this;
    var dataTransfer = evt.dataTransfer;
    var options = _this.options;
    pluginEvent2("dragStart", this, {
      evt
    });
    if (Sortable.eventCanceled) {
      this._onDrop();
      return;
    }
    pluginEvent2("setupClone", this);
    if (!Sortable.eventCanceled) {
      cloneEl = clone(dragEl);
      cloneEl.draggable = false;
      cloneEl.style["will-change"] = "";
      this._hideClone();
      toggleClass(cloneEl, this.options.chosenClass, false);
      Sortable.clone = cloneEl;
    }
    _this.cloneId = _nextTick(function() {
      pluginEvent2("clone", _this);
      if (Sortable.eventCanceled)
        return;
      if (!_this.options.removeCloneOnHide) {
        rootEl.insertBefore(cloneEl, dragEl);
      }
      _this._hideClone();
      _dispatchEvent({
        sortable: _this,
        name: "clone"
      });
    });
    !fallback && toggleClass(dragEl, options.dragClass, true);
    if (fallback) {
      ignoreNextClick = true;
      _this._loopId = setInterval(_this._emulateDragOver, 50);
    } else {
      off(document, "mouseup", _this._onDrop);
      off(document, "touchend", _this._onDrop);
      off(document, "touchcancel", _this._onDrop);
      if (dataTransfer) {
        dataTransfer.effectAllowed = "move";
        options.setData && options.setData.call(_this, dataTransfer, dragEl);
      }
      on(document, "drop", _this);
      css(dragEl, "transform", "translateZ(0)");
    }
    awaitingDragStarted = true;
    _this._dragStartId = _nextTick(_this._dragStarted.bind(_this, fallback, evt));
    on(document, "selectstart", _this);
    moved = true;
    if (Safari) {
      css(document.body, "user-select", "none");
    }
  },
  _onDragOver: function _onDragOver(evt) {
    var el = this.el, target = evt.target, dragRect, targetRect, revert, options = this.options, group = options.group, activeSortable = Sortable.active, isOwner = activeGroup === group, canSort = options.sort, fromSortable = putSortable || activeSortable, vertical, _this = this, completedFired = false;
    if (_silent)
      return;
    function dragOverEvent(name2, extra) {
      pluginEvent2(name2, _this, _objectSpread({
        evt,
        isOwner,
        axis: vertical ? "vertical" : "horizontal",
        revert,
        dragRect,
        targetRect,
        canSort,
        fromSortable,
        target,
        completed,
        onMove: function onMove(target2, after2) {
          return _onMove(rootEl, el, dragEl, dragRect, target2, getRect(target2), evt, after2);
        },
        changed
      }, extra));
    }
    function capture() {
      dragOverEvent("dragOverAnimationCapture");
      _this.captureAnimationState();
      if (_this !== fromSortable) {
        fromSortable.captureAnimationState();
      }
    }
    function completed(insertion) {
      dragOverEvent("dragOverCompleted", {
        insertion
      });
      if (insertion) {
        if (isOwner) {
          activeSortable._hideClone();
        } else {
          activeSortable._showClone(_this);
        }
        if (_this !== fromSortable) {
          toggleClass(dragEl, putSortable ? putSortable.options.ghostClass : activeSortable.options.ghostClass, false);
          toggleClass(dragEl, options.ghostClass, true);
        }
        if (putSortable !== _this && _this !== Sortable.active) {
          putSortable = _this;
        } else if (_this === Sortable.active && putSortable) {
          putSortable = null;
        }
        if (fromSortable === _this) {
          _this._ignoreWhileAnimating = target;
        }
        _this.animateAll(function() {
          dragOverEvent("dragOverAnimationComplete");
          _this._ignoreWhileAnimating = null;
        });
        if (_this !== fromSortable) {
          fromSortable.animateAll();
          fromSortable._ignoreWhileAnimating = null;
        }
      }
      if (target === dragEl && !dragEl.animated || target === el && !target.animated) {
        lastTarget = null;
      }
      if (!options.dragoverBubble && !evt.rootEl && target !== document) {
        dragEl.parentNode[expando]._isOutsideThisEl(evt.target);
        !insertion && nearestEmptyInsertDetectEvent(evt);
      }
      !options.dragoverBubble && evt.stopPropagation && evt.stopPropagation();
      return completedFired = true;
    }
    function changed() {
      newIndex = index(dragEl);
      newDraggableIndex = index(dragEl, options.draggable);
      _dispatchEvent({
        sortable: _this,
        name: "change",
        toEl: el,
        newIndex,
        newDraggableIndex,
        originalEvent: evt
      });
    }
    if (evt.preventDefault !== void 0) {
      evt.cancelable && evt.preventDefault();
    }
    target = closest(target, options.draggable, el, true);
    dragOverEvent("dragOver");
    if (Sortable.eventCanceled)
      return completedFired;
    if (dragEl.contains(evt.target) || target.animated && target.animatingX && target.animatingY || _this._ignoreWhileAnimating === target) {
      return completed(false);
    }
    ignoreNextClick = false;
    if (activeSortable && !options.disabled && (isOwner ? canSort || (revert = !rootEl.contains(dragEl)) : putSortable === this || (this.lastPutMode = activeGroup.checkPull(this, activeSortable, dragEl, evt)) && group.checkPut(this, activeSortable, dragEl, evt))) {
      vertical = this._getDirection(evt, target) === "vertical";
      dragRect = getRect(dragEl);
      dragOverEvent("dragOverValid");
      if (Sortable.eventCanceled)
        return completedFired;
      if (revert) {
        parentEl = rootEl;
        capture();
        this._hideClone();
        dragOverEvent("revert");
        if (!Sortable.eventCanceled) {
          if (nextEl) {
            rootEl.insertBefore(dragEl, nextEl);
          } else {
            rootEl.appendChild(dragEl);
          }
        }
        return completed(true);
      }
      var elLastChild = lastChild(el, options.draggable);
      if (!elLastChild || _ghostIsLast(evt, vertical, this) && !elLastChild.animated) {
        if (elLastChild === dragEl) {
          return completed(false);
        }
        if (elLastChild && el === evt.target) {
          target = elLastChild;
        }
        if (target) {
          targetRect = getRect(target);
        }
        if (_onMove(rootEl, el, dragEl, dragRect, target, targetRect, evt, !!target) !== false) {
          capture();
          el.appendChild(dragEl);
          parentEl = el;
          changed();
          return completed(true);
        }
      } else if (target.parentNode === el) {
        targetRect = getRect(target);
        var direction = 0, targetBeforeFirstSwap, differentLevel = dragEl.parentNode !== el, differentRowCol = !_dragElInRowColumn(dragEl.animated && dragEl.toRect || dragRect, target.animated && target.toRect || targetRect, vertical), side1 = vertical ? "top" : "left", scrolledPastTop = isScrolledPast(target, "top", "top") || isScrolledPast(dragEl, "top", "top"), scrollBefore = scrolledPastTop ? scrolledPastTop.scrollTop : void 0;
        if (lastTarget !== target) {
          targetBeforeFirstSwap = targetRect[side1];
          pastFirstInvertThresh = false;
          isCircumstantialInvert = !differentRowCol && options.invertSwap || differentLevel;
        }
        direction = _getSwapDirection(evt, target, targetRect, vertical, differentRowCol ? 1 : options.swapThreshold, options.invertedSwapThreshold == null ? options.swapThreshold : options.invertedSwapThreshold, isCircumstantialInvert, lastTarget === target);
        var sibling;
        if (direction !== 0) {
          var dragIndex = index(dragEl);
          do {
            dragIndex -= direction;
            sibling = parentEl.children[dragIndex];
          } while (sibling && (css(sibling, "display") === "none" || sibling === ghostEl));
        }
        if (direction === 0 || sibling === target) {
          return completed(false);
        }
        lastTarget = target;
        lastDirection = direction;
        var nextSibling = target.nextElementSibling, after = false;
        after = direction === 1;
        var moveVector = _onMove(rootEl, el, dragEl, dragRect, target, targetRect, evt, after);
        if (moveVector !== false) {
          if (moveVector === 1 || moveVector === -1) {
            after = moveVector === 1;
          }
          _silent = true;
          setTimeout(_unsilent, 30);
          capture();
          if (after && !nextSibling) {
            el.appendChild(dragEl);
          } else {
            target.parentNode.insertBefore(dragEl, after ? nextSibling : target);
          }
          if (scrolledPastTop) {
            scrollBy(scrolledPastTop, 0, scrollBefore - scrolledPastTop.scrollTop);
          }
          parentEl = dragEl.parentNode;
          if (targetBeforeFirstSwap !== void 0 && !isCircumstantialInvert) {
            targetMoveDistance = Math.abs(targetBeforeFirstSwap - getRect(target)[side1]);
          }
          changed();
          return completed(true);
        }
      }
      if (el.contains(dragEl)) {
        return completed(false);
      }
    }
    return false;
  },
  _ignoreWhileAnimating: null,
  _offMoveEvents: function _offMoveEvents() {
    off(document, "mousemove", this._onTouchMove);
    off(document, "touchmove", this._onTouchMove);
    off(document, "pointermove", this._onTouchMove);
    off(document, "dragover", nearestEmptyInsertDetectEvent);
    off(document, "mousemove", nearestEmptyInsertDetectEvent);
    off(document, "touchmove", nearestEmptyInsertDetectEvent);
  },
  _offUpEvents: function _offUpEvents() {
    var ownerDocument = this.el.ownerDocument;
    off(ownerDocument, "mouseup", this._onDrop);
    off(ownerDocument, "touchend", this._onDrop);
    off(ownerDocument, "pointerup", this._onDrop);
    off(ownerDocument, "touchcancel", this._onDrop);
    off(document, "selectstart", this);
  },
  _onDrop: function _onDrop(evt) {
    var el = this.el, options = this.options;
    newIndex = index(dragEl);
    newDraggableIndex = index(dragEl, options.draggable);
    pluginEvent2("drop", this, {
      evt
    });
    parentEl = dragEl && dragEl.parentNode;
    newIndex = index(dragEl);
    newDraggableIndex = index(dragEl, options.draggable);
    if (Sortable.eventCanceled) {
      this._nulling();
      return;
    }
    awaitingDragStarted = false;
    isCircumstantialInvert = false;
    pastFirstInvertThresh = false;
    clearInterval(this._loopId);
    clearTimeout(this._dragStartTimer);
    _cancelNextTick(this.cloneId);
    _cancelNextTick(this._dragStartId);
    if (this.nativeDraggable) {
      off(document, "drop", this);
      off(el, "dragstart", this._onDragStart);
    }
    this._offMoveEvents();
    this._offUpEvents();
    if (Safari) {
      css(document.body, "user-select", "");
    }
    css(dragEl, "transform", "");
    if (evt) {
      if (moved) {
        evt.cancelable && evt.preventDefault();
        !options.dropBubble && evt.stopPropagation();
      }
      ghostEl && ghostEl.parentNode && ghostEl.parentNode.removeChild(ghostEl);
      if (rootEl === parentEl || putSortable && putSortable.lastPutMode !== "clone") {
        cloneEl && cloneEl.parentNode && cloneEl.parentNode.removeChild(cloneEl);
      }
      if (dragEl) {
        if (this.nativeDraggable) {
          off(dragEl, "dragend", this);
        }
        _disableDraggable(dragEl);
        dragEl.style["will-change"] = "";
        if (moved && !awaitingDragStarted) {
          toggleClass(dragEl, putSortable ? putSortable.options.ghostClass : this.options.ghostClass, false);
        }
        toggleClass(dragEl, this.options.chosenClass, false);
        _dispatchEvent({
          sortable: this,
          name: "unchoose",
          toEl: parentEl,
          newIndex: null,
          newDraggableIndex: null,
          originalEvent: evt
        });
        if (rootEl !== parentEl) {
          if (newIndex >= 0) {
            _dispatchEvent({
              rootEl: parentEl,
              name: "add",
              toEl: parentEl,
              fromEl: rootEl,
              originalEvent: evt
            });
            _dispatchEvent({
              sortable: this,
              name: "remove",
              toEl: parentEl,
              originalEvent: evt
            });
            _dispatchEvent({
              rootEl: parentEl,
              name: "sort",
              toEl: parentEl,
              fromEl: rootEl,
              originalEvent: evt
            });
            _dispatchEvent({
              sortable: this,
              name: "sort",
              toEl: parentEl,
              originalEvent: evt
            });
          }
          putSortable && putSortable.save();
        } else {
          if (newIndex !== oldIndex) {
            if (newIndex >= 0) {
              _dispatchEvent({
                sortable: this,
                name: "update",
                toEl: parentEl,
                originalEvent: evt
              });
              _dispatchEvent({
                sortable: this,
                name: "sort",
                toEl: parentEl,
                originalEvent: evt
              });
            }
          }
        }
        if (Sortable.active) {
          if (newIndex == null || newIndex === -1) {
            newIndex = oldIndex;
            newDraggableIndex = oldDraggableIndex;
          }
          _dispatchEvent({
            sortable: this,
            name: "end",
            toEl: parentEl,
            originalEvent: evt
          });
          this.save();
        }
      }
    }
    this._nulling();
  },
  _nulling: function _nulling() {
    pluginEvent2("nulling", this);
    rootEl = dragEl = parentEl = ghostEl = nextEl = cloneEl = lastDownEl = cloneHidden = tapEvt = touchEvt = moved = newIndex = newDraggableIndex = oldIndex = oldDraggableIndex = lastTarget = lastDirection = putSortable = activeGroup = Sortable.dragged = Sortable.ghost = Sortable.clone = Sortable.active = null;
    savedInputChecked.forEach(function(el) {
      el.checked = true;
    });
    savedInputChecked.length = lastDx = lastDy = 0;
  },
  handleEvent: function handleEvent(evt) {
    switch (evt.type) {
      case "drop":
      case "dragend":
        this._onDrop(evt);
        break;
      case "dragenter":
      case "dragover":
        if (dragEl) {
          this._onDragOver(evt);
          _globalDragOver(evt);
        }
        break;
      case "selectstart":
        evt.preventDefault();
        break;
    }
  },
  toArray: function toArray() {
    var order = [], el, children = this.el.children, i = 0, n = children.length, options = this.options;
    for (; i < n; i++) {
      el = children[i];
      if (closest(el, options.draggable, this.el, false)) {
        order.push(el.getAttribute(options.dataIdAttr) || _generateId(el));
      }
    }
    return order;
  },
  sort: function sort(order) {
    var items = {}, rootEl2 = this.el;
    this.toArray().forEach(function(id2, i) {
      var el = rootEl2.children[i];
      if (closest(el, this.options.draggable, rootEl2, false)) {
        items[id2] = el;
      }
    }, this);
    order.forEach(function(id2) {
      if (items[id2]) {
        rootEl2.removeChild(items[id2]);
        rootEl2.appendChild(items[id2]);
      }
    });
  },
  save: function save() {
    var store2 = this.options.store;
    store2 && store2.set && store2.set(this);
  },
  closest: function closest$1(el, selector) {
    return closest(el, selector || this.options.draggable, this.el, false);
  },
  option: function option(name2, value) {
    var options = this.options;
    if (value === void 0) {
      return options[name2];
    } else {
      var modifiedValue = PluginManager.modifyOption(this, name2, value);
      if (typeof modifiedValue !== "undefined") {
        options[name2] = modifiedValue;
      } else {
        options[name2] = value;
      }
      if (name2 === "group") {
        _prepareGroup(options);
      }
    }
  },
  destroy: function destroy() {
    pluginEvent2("destroy", this);
    var el = this.el;
    el[expando] = null;
    off(el, "mousedown", this._onTapStart);
    off(el, "touchstart", this._onTapStart);
    off(el, "pointerdown", this._onTapStart);
    if (this.nativeDraggable) {
      off(el, "dragover", this);
      off(el, "dragenter", this);
    }
    Array.prototype.forEach.call(el.querySelectorAll("[draggable]"), function(el2) {
      el2.removeAttribute("draggable");
    });
    this._onDrop();
    this._disableDelayedDragEvents();
    sortables.splice(sortables.indexOf(this.el), 1);
    this.el = el = null;
  },
  _hideClone: function _hideClone() {
    if (!cloneHidden) {
      pluginEvent2("hideClone", this);
      if (Sortable.eventCanceled)
        return;
      css(cloneEl, "display", "none");
      if (this.options.removeCloneOnHide && cloneEl.parentNode) {
        cloneEl.parentNode.removeChild(cloneEl);
      }
      cloneHidden = true;
    }
  },
  _showClone: function _showClone(putSortable2) {
    if (putSortable2.lastPutMode !== "clone") {
      this._hideClone();
      return;
    }
    if (cloneHidden) {
      pluginEvent2("showClone", this);
      if (Sortable.eventCanceled)
        return;
      if (rootEl.contains(dragEl) && !this.options.group.revertClone) {
        rootEl.insertBefore(cloneEl, dragEl);
      } else if (nextEl) {
        rootEl.insertBefore(cloneEl, nextEl);
      } else {
        rootEl.appendChild(cloneEl);
      }
      if (this.options.group.revertClone) {
        this.animate(dragEl, cloneEl);
      }
      css(cloneEl, "display", "");
      cloneHidden = false;
    }
  }
};
function _globalDragOver(evt) {
  if (evt.dataTransfer) {
    evt.dataTransfer.dropEffect = "move";
  }
  evt.cancelable && evt.preventDefault();
}
function _onMove(fromEl, toEl, dragEl2, dragRect, targetEl, targetRect, originalEvent, willInsertAfter) {
  var evt, sortable = fromEl[expando], onMoveFn = sortable.options.onMove, retVal;
  if (window.CustomEvent && !IE11OrLess && !Edge) {
    evt = new CustomEvent("move", {
      bubbles: true,
      cancelable: true
    });
  } else {
    evt = document.createEvent("Event");
    evt.initEvent("move", true, true);
  }
  evt.to = toEl;
  evt.from = fromEl;
  evt.dragged = dragEl2;
  evt.draggedRect = dragRect;
  evt.related = targetEl || toEl;
  evt.relatedRect = targetRect || getRect(toEl);
  evt.willInsertAfter = willInsertAfter;
  evt.originalEvent = originalEvent;
  fromEl.dispatchEvent(evt);
  if (onMoveFn) {
    retVal = onMoveFn.call(sortable, evt, originalEvent);
  }
  return retVal;
}
function _disableDraggable(el) {
  el.draggable = false;
}
function _unsilent() {
  _silent = false;
}
function _ghostIsLast(evt, vertical, sortable) {
  var rect = getRect(lastChild(sortable.el, sortable.options.draggable));
  var spacer = 10;
  return vertical ? evt.clientX > rect.right + spacer || evt.clientX <= rect.right && evt.clientY > rect.bottom && evt.clientX >= rect.left : evt.clientX > rect.right && evt.clientY > rect.top || evt.clientX <= rect.right && evt.clientY > rect.bottom + spacer;
}
function _getSwapDirection(evt, target, targetRect, vertical, swapThreshold, invertedSwapThreshold, invertSwap, isLastTarget) {
  var mouseOnAxis = vertical ? evt.clientY : evt.clientX, targetLength = vertical ? targetRect.height : targetRect.width, targetS1 = vertical ? targetRect.top : targetRect.left, targetS2 = vertical ? targetRect.bottom : targetRect.right, invert = false;
  if (!invertSwap) {
    if (isLastTarget && targetMoveDistance < targetLength * swapThreshold) {
      if (!pastFirstInvertThresh && (lastDirection === 1 ? mouseOnAxis > targetS1 + targetLength * invertedSwapThreshold / 2 : mouseOnAxis < targetS2 - targetLength * invertedSwapThreshold / 2)) {
        pastFirstInvertThresh = true;
      }
      if (!pastFirstInvertThresh) {
        if (lastDirection === 1 ? mouseOnAxis < targetS1 + targetMoveDistance : mouseOnAxis > targetS2 - targetMoveDistance) {
          return -lastDirection;
        }
      } else {
        invert = true;
      }
    } else {
      if (mouseOnAxis > targetS1 + targetLength * (1 - swapThreshold) / 2 && mouseOnAxis < targetS2 - targetLength * (1 - swapThreshold) / 2) {
        return _getInsertDirection(target);
      }
    }
  }
  invert = invert || invertSwap;
  if (invert) {
    if (mouseOnAxis < targetS1 + targetLength * invertedSwapThreshold / 2 || mouseOnAxis > targetS2 - targetLength * invertedSwapThreshold / 2) {
      return mouseOnAxis > targetS1 + targetLength / 2 ? 1 : -1;
    }
  }
  return 0;
}
function _getInsertDirection(target) {
  if (index(dragEl) < index(target)) {
    return 1;
  } else {
    return -1;
  }
}
function _generateId(el) {
  var str = el.tagName + el.className + el.src + el.href + el.textContent, i = str.length, sum = 0;
  while (i--) {
    sum += str.charCodeAt(i);
  }
  return sum.toString(36);
}
function _saveInputCheckedState(root) {
  savedInputChecked.length = 0;
  var inputs = root.getElementsByTagName("input");
  var idx = inputs.length;
  while (idx--) {
    var el = inputs[idx];
    el.checked && savedInputChecked.push(el);
  }
}
function _nextTick(fn) {
  return setTimeout(fn, 0);
}
function _cancelNextTick(id2) {
  return clearTimeout(id2);
}
if (documentExists) {
  on(document, "touchmove", function(evt) {
    if ((Sortable.active || awaitingDragStarted) && evt.cancelable) {
      evt.preventDefault();
    }
  });
}
Sortable.utils = {
  on,
  off,
  css,
  find,
  is: function is(el, selector) {
    return !!closest(el, selector, el, false);
  },
  extend,
  throttle,
  closest,
  toggleClass,
  clone,
  index,
  nextTick: _nextTick,
  cancelNextTick: _cancelNextTick,
  detectDirection: _detectDirection,
  getChild
};
Sortable.get = function(element) {
  return element[expando];
};
Sortable.mount = function() {
  for (var _len = arguments.length, plugins2 = new Array(_len), _key = 0; _key < _len; _key++) {
    plugins2[_key] = arguments[_key];
  }
  if (plugins2[0].constructor === Array)
    plugins2 = plugins2[0];
  plugins2.forEach(function(plugin) {
    if (!plugin.prototype || !plugin.prototype.constructor) {
      throw "Sortable: Mounted plugin must be a constructor function, not ".concat({}.toString.call(plugin));
    }
    if (plugin.utils)
      Sortable.utils = _objectSpread({}, Sortable.utils, plugin.utils);
    PluginManager.mount(plugin);
  });
};
Sortable.create = function(el, options) {
  return new Sortable(el, options);
};
Sortable.version = version$1;
var autoScrolls = [], scrollEl, scrollRootEl, scrolling = false, lastAutoScrollX, lastAutoScrollY, touchEvt$1, pointerElemChangedInterval;
function AutoScrollPlugin() {
  function AutoScroll() {
    this.defaults = {
      scroll: true,
      scrollSensitivity: 30,
      scrollSpeed: 10,
      bubbleScroll: true
    };
    for (var fn in this) {
      if (fn.charAt(0) === "_" && typeof this[fn] === "function") {
        this[fn] = this[fn].bind(this);
      }
    }
  }
  AutoScroll.prototype = {
    dragStarted: function dragStarted2(_ref) {
      var originalEvent = _ref.originalEvent;
      if (this.sortable.nativeDraggable) {
        on(document, "dragover", this._handleAutoScroll);
      } else {
        if (this.options.supportPointer) {
          on(document, "pointermove", this._handleFallbackAutoScroll);
        } else if (originalEvent.touches) {
          on(document, "touchmove", this._handleFallbackAutoScroll);
        } else {
          on(document, "mousemove", this._handleFallbackAutoScroll);
        }
      }
    },
    dragOverCompleted: function dragOverCompleted(_ref2) {
      var originalEvent = _ref2.originalEvent;
      if (!this.options.dragOverBubble && !originalEvent.rootEl) {
        this._handleAutoScroll(originalEvent);
      }
    },
    drop: function drop3() {
      if (this.sortable.nativeDraggable) {
        off(document, "dragover", this._handleAutoScroll);
      } else {
        off(document, "pointermove", this._handleFallbackAutoScroll);
        off(document, "touchmove", this._handleFallbackAutoScroll);
        off(document, "mousemove", this._handleFallbackAutoScroll);
      }
      clearPointerElemChangedInterval();
      clearAutoScrolls();
      cancelThrottle();
    },
    nulling: function nulling() {
      touchEvt$1 = scrollRootEl = scrollEl = scrolling = pointerElemChangedInterval = lastAutoScrollX = lastAutoScrollY = null;
      autoScrolls.length = 0;
    },
    _handleFallbackAutoScroll: function _handleFallbackAutoScroll(evt) {
      this._handleAutoScroll(evt, true);
    },
    _handleAutoScroll: function _handleAutoScroll(evt, fallback) {
      var _this = this;
      var x = (evt.touches ? evt.touches[0] : evt).clientX, y = (evt.touches ? evt.touches[0] : evt).clientY, elem = document.elementFromPoint(x, y);
      touchEvt$1 = evt;
      if (fallback || Edge || IE11OrLess || Safari) {
        autoScroll(evt, this.options, elem, fallback);
        var ogElemScroller = getParentAutoScrollElement(elem, true);
        if (scrolling && (!pointerElemChangedInterval || x !== lastAutoScrollX || y !== lastAutoScrollY)) {
          pointerElemChangedInterval && clearPointerElemChangedInterval();
          pointerElemChangedInterval = setInterval(function() {
            var newElem = getParentAutoScrollElement(document.elementFromPoint(x, y), true);
            if (newElem !== ogElemScroller) {
              ogElemScroller = newElem;
              clearAutoScrolls();
            }
            autoScroll(evt, _this.options, newElem, fallback);
          }, 10);
          lastAutoScrollX = x;
          lastAutoScrollY = y;
        }
      } else {
        if (!this.options.bubbleScroll || getParentAutoScrollElement(elem, true) === getWindowScrollingElement()) {
          clearAutoScrolls();
          return;
        }
        autoScroll(evt, this.options, getParentAutoScrollElement(elem, false), false);
      }
    }
  };
  return _extends(AutoScroll, {
    pluginName: "scroll",
    initializeByDefault: true
  });
}
function clearAutoScrolls() {
  autoScrolls.forEach(function(autoScroll2) {
    clearInterval(autoScroll2.pid);
  });
  autoScrolls = [];
}
function clearPointerElemChangedInterval() {
  clearInterval(pointerElemChangedInterval);
}
var autoScroll = throttle(function(evt, options, rootEl2, isFallback) {
  if (!options.scroll)
    return;
  var x = (evt.touches ? evt.touches[0] : evt).clientX, y = (evt.touches ? evt.touches[0] : evt).clientY, sens = options.scrollSensitivity, speed = options.scrollSpeed, winScroller = getWindowScrollingElement();
  var scrollThisInstance = false, scrollCustomFn;
  if (scrollRootEl !== rootEl2) {
    scrollRootEl = rootEl2;
    clearAutoScrolls();
    scrollEl = options.scroll;
    scrollCustomFn = options.scrollFn;
    if (scrollEl === true) {
      scrollEl = getParentAutoScrollElement(rootEl2, true);
    }
  }
  var layersOut = 0;
  var currentParent = scrollEl;
  do {
    var el = currentParent, rect = getRect(el), top = rect.top, bottom = rect.bottom, left = rect.left, right = rect.right, width = rect.width, height = rect.height, canScrollX = void 0, canScrollY = void 0, scrollWidth = el.scrollWidth, scrollHeight = el.scrollHeight, elCSS = css(el), scrollPosX = el.scrollLeft, scrollPosY = el.scrollTop;
    if (el === winScroller) {
      canScrollX = width < scrollWidth && (elCSS.overflowX === "auto" || elCSS.overflowX === "scroll" || elCSS.overflowX === "visible");
      canScrollY = height < scrollHeight && (elCSS.overflowY === "auto" || elCSS.overflowY === "scroll" || elCSS.overflowY === "visible");
    } else {
      canScrollX = width < scrollWidth && (elCSS.overflowX === "auto" || elCSS.overflowX === "scroll");
      canScrollY = height < scrollHeight && (elCSS.overflowY === "auto" || elCSS.overflowY === "scroll");
    }
    var vx = canScrollX && (Math.abs(right - x) <= sens && scrollPosX + width < scrollWidth) - (Math.abs(left - x) <= sens && !!scrollPosX);
    var vy = canScrollY && (Math.abs(bottom - y) <= sens && scrollPosY + height < scrollHeight) - (Math.abs(top - y) <= sens && !!scrollPosY);
    if (!autoScrolls[layersOut]) {
      for (var i = 0; i <= layersOut; i++) {
        if (!autoScrolls[i]) {
          autoScrolls[i] = {};
        }
      }
    }
    if (autoScrolls[layersOut].vx != vx || autoScrolls[layersOut].vy != vy || autoScrolls[layersOut].el !== el) {
      autoScrolls[layersOut].el = el;
      autoScrolls[layersOut].vx = vx;
      autoScrolls[layersOut].vy = vy;
      clearInterval(autoScrolls[layersOut].pid);
      if (vx != 0 || vy != 0) {
        scrollThisInstance = true;
        autoScrolls[layersOut].pid = setInterval(function() {
          if (isFallback && this.layer === 0) {
            Sortable.active._onTouchMove(touchEvt$1);
          }
          var scrollOffsetY = autoScrolls[this.layer].vy ? autoScrolls[this.layer].vy * speed : 0;
          var scrollOffsetX = autoScrolls[this.layer].vx ? autoScrolls[this.layer].vx * speed : 0;
          if (typeof scrollCustomFn === "function") {
            if (scrollCustomFn.call(Sortable.dragged.parentNode[expando], scrollOffsetX, scrollOffsetY, evt, touchEvt$1, autoScrolls[this.layer].el) !== "continue") {
              return;
            }
          }
          scrollBy(autoScrolls[this.layer].el, scrollOffsetX, scrollOffsetY);
        }.bind({
          layer: layersOut
        }), 24);
      }
    }
    layersOut++;
  } while (options.bubbleScroll && currentParent !== winScroller && (currentParent = getParentAutoScrollElement(currentParent, false)));
  scrolling = scrollThisInstance;
}, 30);
var drop = function drop2(_ref) {
  var originalEvent = _ref.originalEvent, putSortable2 = _ref.putSortable, dragEl2 = _ref.dragEl, activeSortable = _ref.activeSortable, dispatchSortableEvent = _ref.dispatchSortableEvent, hideGhostForTarget = _ref.hideGhostForTarget, unhideGhostForTarget = _ref.unhideGhostForTarget;
  if (!originalEvent)
    return;
  var toSortable = putSortable2 || activeSortable;
  hideGhostForTarget();
  var touch = originalEvent.changedTouches && originalEvent.changedTouches.length ? originalEvent.changedTouches[0] : originalEvent;
  var target = document.elementFromPoint(touch.clientX, touch.clientY);
  unhideGhostForTarget();
  if (toSortable && !toSortable.el.contains(target)) {
    dispatchSortableEvent("spill");
    this.onSpill({
      dragEl: dragEl2,
      putSortable: putSortable2
    });
  }
};
function Revert() {
}
Revert.prototype = {
  startIndex: null,
  dragStart: function dragStart(_ref2) {
    var oldDraggableIndex2 = _ref2.oldDraggableIndex;
    this.startIndex = oldDraggableIndex2;
  },
  onSpill: function onSpill(_ref3) {
    var dragEl2 = _ref3.dragEl, putSortable2 = _ref3.putSortable;
    this.sortable.captureAnimationState();
    if (putSortable2) {
      putSortable2.captureAnimationState();
    }
    var nextSibling = getChild(this.sortable.el, this.startIndex, this.options);
    if (nextSibling) {
      this.sortable.el.insertBefore(dragEl2, nextSibling);
    } else {
      this.sortable.el.appendChild(dragEl2);
    }
    this.sortable.animateAll();
    if (putSortable2) {
      putSortable2.animateAll();
    }
  },
  drop
};
_extends(Revert, {
  pluginName: "revertOnSpill"
});
function Remove() {
}
Remove.prototype = {
  onSpill: function onSpill2(_ref4) {
    var dragEl2 = _ref4.dragEl, putSortable2 = _ref4.putSortable;
    var parentSortable = putSortable2 || this.sortable;
    parentSortable.captureAnimationState();
    dragEl2.parentNode && dragEl2.parentNode.removeChild(dragEl2);
    parentSortable.animateAll();
  },
  drop
};
_extends(Remove, {
  pluginName: "removeOnSpill"
});
var lastSwapEl;
function SwapPlugin() {
  function Swap() {
    this.defaults = {
      swapClass: "sortable-swap-highlight"
    };
  }
  Swap.prototype = {
    dragStart: function dragStart2(_ref) {
      var dragEl2 = _ref.dragEl;
      lastSwapEl = dragEl2;
    },
    dragOverValid: function dragOverValid(_ref2) {
      var completed = _ref2.completed, target = _ref2.target, onMove = _ref2.onMove, activeSortable = _ref2.activeSortable, changed = _ref2.changed, cancel = _ref2.cancel;
      if (!activeSortable.options.swap)
        return;
      var el = this.sortable.el, options = this.options;
      if (target && target !== el) {
        var prevSwapEl = lastSwapEl;
        if (onMove(target) !== false) {
          toggleClass(target, options.swapClass, true);
          lastSwapEl = target;
        } else {
          lastSwapEl = null;
        }
        if (prevSwapEl && prevSwapEl !== lastSwapEl) {
          toggleClass(prevSwapEl, options.swapClass, false);
        }
      }
      changed();
      completed(true);
      cancel();
    },
    drop: function drop3(_ref3) {
      var activeSortable = _ref3.activeSortable, putSortable2 = _ref3.putSortable, dragEl2 = _ref3.dragEl;
      var toSortable = putSortable2 || this.sortable;
      var options = this.options;
      lastSwapEl && toggleClass(lastSwapEl, options.swapClass, false);
      if (lastSwapEl && (options.swap || putSortable2 && putSortable2.options.swap)) {
        if (dragEl2 !== lastSwapEl) {
          toSortable.captureAnimationState();
          if (toSortable !== activeSortable)
            activeSortable.captureAnimationState();
          swapNodes(dragEl2, lastSwapEl);
          toSortable.animateAll();
          if (toSortable !== activeSortable)
            activeSortable.animateAll();
        }
      }
    },
    nulling: function nulling() {
      lastSwapEl = null;
    }
  };
  return _extends(Swap, {
    pluginName: "swap",
    eventProperties: function eventProperties() {
      return {
        swapItem: lastSwapEl
      };
    }
  });
}
function swapNodes(n1, n2) {
  var p1 = n1.parentNode, p2 = n2.parentNode, i1, i2;
  if (!p1 || !p2 || p1.isEqualNode(n2) || p2.isEqualNode(n1))
    return;
  i1 = index(n1);
  i2 = index(n2);
  if (p1.isEqualNode(p2) && i1 < i2) {
    i2++;
  }
  p1.insertBefore(n2, p1.children[i1]);
  p2.insertBefore(n1, p2.children[i2]);
}
var multiDragElements = [], multiDragClones = [], lastMultiDragSelect, multiDragSortable, initialFolding = false, folding = false, dragStarted = false, dragEl$1, clonesFromRect, clonesHidden;
function MultiDragPlugin() {
  function MultiDrag(sortable) {
    for (var fn in this) {
      if (fn.charAt(0) === "_" && typeof this[fn] === "function") {
        this[fn] = this[fn].bind(this);
      }
    }
    if (sortable.options.supportPointer) {
      on(document, "pointerup", this._deselectMultiDrag);
    } else {
      on(document, "mouseup", this._deselectMultiDrag);
      on(document, "touchend", this._deselectMultiDrag);
    }
    on(document, "keydown", this._checkKeyDown);
    on(document, "keyup", this._checkKeyUp);
    this.defaults = {
      selectedClass: "sortable-selected",
      multiDragKey: null,
      setData: function setData(dataTransfer, dragEl2) {
        var data = "";
        if (multiDragElements.length && multiDragSortable === sortable) {
          multiDragElements.forEach(function(multiDragElement, i) {
            data += (!i ? "" : ", ") + multiDragElement.textContent;
          });
        } else {
          data = dragEl2.textContent;
        }
        dataTransfer.setData("Text", data);
      }
    };
  }
  MultiDrag.prototype = {
    multiDragKeyDown: false,
    isMultiDrag: false,
    delayStartGlobal: function delayStartGlobal(_ref) {
      var dragged = _ref.dragEl;
      dragEl$1 = dragged;
    },
    delayEnded: function delayEnded() {
      this.isMultiDrag = ~multiDragElements.indexOf(dragEl$1);
    },
    setupClone: function setupClone(_ref2) {
      var sortable = _ref2.sortable, cancel = _ref2.cancel;
      if (!this.isMultiDrag)
        return;
      for (var i = 0; i < multiDragElements.length; i++) {
        multiDragClones.push(clone(multiDragElements[i]));
        multiDragClones[i].sortableIndex = multiDragElements[i].sortableIndex;
        multiDragClones[i].draggable = false;
        multiDragClones[i].style["will-change"] = "";
        toggleClass(multiDragClones[i], this.options.selectedClass, false);
        multiDragElements[i] === dragEl$1 && toggleClass(multiDragClones[i], this.options.chosenClass, false);
      }
      sortable._hideClone();
      cancel();
    },
    clone: function clone2(_ref3) {
      var sortable = _ref3.sortable, rootEl2 = _ref3.rootEl, dispatchSortableEvent = _ref3.dispatchSortableEvent, cancel = _ref3.cancel;
      if (!this.isMultiDrag)
        return;
      if (!this.options.removeCloneOnHide) {
        if (multiDragElements.length && multiDragSortable === sortable) {
          insertMultiDragClones(true, rootEl2);
          dispatchSortableEvent("clone");
          cancel();
        }
      }
    },
    showClone: function showClone(_ref4) {
      var cloneNowShown = _ref4.cloneNowShown, rootEl2 = _ref4.rootEl, cancel = _ref4.cancel;
      if (!this.isMultiDrag)
        return;
      insertMultiDragClones(false, rootEl2);
      multiDragClones.forEach(function(clone2) {
        css(clone2, "display", "");
      });
      cloneNowShown();
      clonesHidden = false;
      cancel();
    },
    hideClone: function hideClone(_ref5) {
      var _this = this;
      _ref5.sortable;
      var cloneNowHidden = _ref5.cloneNowHidden, cancel = _ref5.cancel;
      if (!this.isMultiDrag)
        return;
      multiDragClones.forEach(function(clone2) {
        css(clone2, "display", "none");
        if (_this.options.removeCloneOnHide && clone2.parentNode) {
          clone2.parentNode.removeChild(clone2);
        }
      });
      cloneNowHidden();
      clonesHidden = true;
      cancel();
    },
    dragStartGlobal: function dragStartGlobal(_ref6) {
      _ref6.sortable;
      if (!this.isMultiDrag && multiDragSortable) {
        multiDragSortable.multiDrag._deselectMultiDrag();
      }
      multiDragElements.forEach(function(multiDragElement) {
        multiDragElement.sortableIndex = index(multiDragElement);
      });
      multiDragElements = multiDragElements.sort(function(a, b) {
        return a.sortableIndex - b.sortableIndex;
      });
      dragStarted = true;
    },
    dragStarted: function dragStarted2(_ref7) {
      var _this2 = this;
      var sortable = _ref7.sortable;
      if (!this.isMultiDrag)
        return;
      if (this.options.sort) {
        sortable.captureAnimationState();
        if (this.options.animation) {
          multiDragElements.forEach(function(multiDragElement) {
            if (multiDragElement === dragEl$1)
              return;
            css(multiDragElement, "position", "absolute");
          });
          var dragRect = getRect(dragEl$1, false, true, true);
          multiDragElements.forEach(function(multiDragElement) {
            if (multiDragElement === dragEl$1)
              return;
            setRect(multiDragElement, dragRect);
          });
          folding = true;
          initialFolding = true;
        }
      }
      sortable.animateAll(function() {
        folding = false;
        initialFolding = false;
        if (_this2.options.animation) {
          multiDragElements.forEach(function(multiDragElement) {
            unsetRect(multiDragElement);
          });
        }
        if (_this2.options.sort) {
          removeMultiDragElements();
        }
      });
    },
    dragOver: function dragOver(_ref8) {
      var target = _ref8.target, completed = _ref8.completed, cancel = _ref8.cancel;
      if (folding && ~multiDragElements.indexOf(target)) {
        completed(false);
        cancel();
      }
    },
    revert: function revert(_ref9) {
      var fromSortable = _ref9.fromSortable, rootEl2 = _ref9.rootEl, sortable = _ref9.sortable, dragRect = _ref9.dragRect;
      if (multiDragElements.length > 1) {
        multiDragElements.forEach(function(multiDragElement) {
          sortable.addAnimationState({
            target: multiDragElement,
            rect: folding ? getRect(multiDragElement) : dragRect
          });
          unsetRect(multiDragElement);
          multiDragElement.fromRect = dragRect;
          fromSortable.removeAnimationState(multiDragElement);
        });
        folding = false;
        insertMultiDragElements(!this.options.removeCloneOnHide, rootEl2);
      }
    },
    dragOverCompleted: function dragOverCompleted(_ref10) {
      var sortable = _ref10.sortable, isOwner = _ref10.isOwner, insertion = _ref10.insertion, activeSortable = _ref10.activeSortable, parentEl2 = _ref10.parentEl, putSortable2 = _ref10.putSortable;
      var options = this.options;
      if (insertion) {
        if (isOwner) {
          activeSortable._hideClone();
        }
        initialFolding = false;
        if (options.animation && multiDragElements.length > 1 && (folding || !isOwner && !activeSortable.options.sort && !putSortable2)) {
          var dragRectAbsolute = getRect(dragEl$1, false, true, true);
          multiDragElements.forEach(function(multiDragElement) {
            if (multiDragElement === dragEl$1)
              return;
            setRect(multiDragElement, dragRectAbsolute);
            parentEl2.appendChild(multiDragElement);
          });
          folding = true;
        }
        if (!isOwner) {
          if (!folding) {
            removeMultiDragElements();
          }
          if (multiDragElements.length > 1) {
            var clonesHiddenBefore = clonesHidden;
            activeSortable._showClone(sortable);
            if (activeSortable.options.animation && !clonesHidden && clonesHiddenBefore) {
              multiDragClones.forEach(function(clone2) {
                activeSortable.addAnimationState({
                  target: clone2,
                  rect: clonesFromRect
                });
                clone2.fromRect = clonesFromRect;
                clone2.thisAnimationDuration = null;
              });
            }
          } else {
            activeSortable._showClone(sortable);
          }
        }
      }
    },
    dragOverAnimationCapture: function dragOverAnimationCapture(_ref11) {
      var dragRect = _ref11.dragRect, isOwner = _ref11.isOwner, activeSortable = _ref11.activeSortable;
      multiDragElements.forEach(function(multiDragElement) {
        multiDragElement.thisAnimationDuration = null;
      });
      if (activeSortable.options.animation && !isOwner && activeSortable.multiDrag.isMultiDrag) {
        clonesFromRect = _extends({}, dragRect);
        var dragMatrix = matrix(dragEl$1, true);
        clonesFromRect.top -= dragMatrix.f;
        clonesFromRect.left -= dragMatrix.e;
      }
    },
    dragOverAnimationComplete: function dragOverAnimationComplete() {
      if (folding) {
        folding = false;
        removeMultiDragElements();
      }
    },
    drop: function drop3(_ref12) {
      var evt = _ref12.originalEvent, rootEl2 = _ref12.rootEl, parentEl2 = _ref12.parentEl, sortable = _ref12.sortable, dispatchSortableEvent = _ref12.dispatchSortableEvent, oldIndex2 = _ref12.oldIndex, putSortable2 = _ref12.putSortable;
      var toSortable = putSortable2 || this.sortable;
      if (!evt)
        return;
      var options = this.options, children = parentEl2.children;
      if (!dragStarted) {
        if (options.multiDragKey && !this.multiDragKeyDown) {
          this._deselectMultiDrag();
        }
        toggleClass(dragEl$1, options.selectedClass, !~multiDragElements.indexOf(dragEl$1));
        if (!~multiDragElements.indexOf(dragEl$1)) {
          multiDragElements.push(dragEl$1);
          dispatchEvent({
            sortable,
            rootEl: rootEl2,
            name: "select",
            targetEl: dragEl$1,
            originalEvt: evt
          });
          if (evt.shiftKey && lastMultiDragSelect && sortable.el.contains(lastMultiDragSelect)) {
            var lastIndex = index(lastMultiDragSelect), currentIndex = index(dragEl$1);
            if (~lastIndex && ~currentIndex && lastIndex !== currentIndex) {
              var n, i;
              if (currentIndex > lastIndex) {
                i = lastIndex;
                n = currentIndex;
              } else {
                i = currentIndex;
                n = lastIndex + 1;
              }
              for (; i < n; i++) {
                if (~multiDragElements.indexOf(children[i]))
                  continue;
                toggleClass(children[i], options.selectedClass, true);
                multiDragElements.push(children[i]);
                dispatchEvent({
                  sortable,
                  rootEl: rootEl2,
                  name: "select",
                  targetEl: children[i],
                  originalEvt: evt
                });
              }
            }
          } else {
            lastMultiDragSelect = dragEl$1;
          }
          multiDragSortable = toSortable;
        } else {
          multiDragElements.splice(multiDragElements.indexOf(dragEl$1), 1);
          lastMultiDragSelect = null;
          dispatchEvent({
            sortable,
            rootEl: rootEl2,
            name: "deselect",
            targetEl: dragEl$1,
            originalEvt: evt
          });
        }
      }
      if (dragStarted && this.isMultiDrag) {
        if ((parentEl2[expando].options.sort || parentEl2 !== rootEl2) && multiDragElements.length > 1) {
          var dragRect = getRect(dragEl$1), multiDragIndex = index(dragEl$1, ":not(." + this.options.selectedClass + ")");
          if (!initialFolding && options.animation)
            dragEl$1.thisAnimationDuration = null;
          toSortable.captureAnimationState();
          if (!initialFolding) {
            if (options.animation) {
              dragEl$1.fromRect = dragRect;
              multiDragElements.forEach(function(multiDragElement) {
                multiDragElement.thisAnimationDuration = null;
                if (multiDragElement !== dragEl$1) {
                  var rect = folding ? getRect(multiDragElement) : dragRect;
                  multiDragElement.fromRect = rect;
                  toSortable.addAnimationState({
                    target: multiDragElement,
                    rect
                  });
                }
              });
            }
            removeMultiDragElements();
            multiDragElements.forEach(function(multiDragElement) {
              if (children[multiDragIndex]) {
                parentEl2.insertBefore(multiDragElement, children[multiDragIndex]);
              } else {
                parentEl2.appendChild(multiDragElement);
              }
              multiDragIndex++;
            });
            if (oldIndex2 === index(dragEl$1)) {
              var update = false;
              multiDragElements.forEach(function(multiDragElement) {
                if (multiDragElement.sortableIndex !== index(multiDragElement)) {
                  update = true;
                  return;
                }
              });
              if (update) {
                dispatchSortableEvent("update");
              }
            }
          }
          multiDragElements.forEach(function(multiDragElement) {
            unsetRect(multiDragElement);
          });
          toSortable.animateAll();
        }
        multiDragSortable = toSortable;
      }
      if (rootEl2 === parentEl2 || putSortable2 && putSortable2.lastPutMode !== "clone") {
        multiDragClones.forEach(function(clone2) {
          clone2.parentNode && clone2.parentNode.removeChild(clone2);
        });
      }
    },
    nullingGlobal: function nullingGlobal() {
      this.isMultiDrag = dragStarted = false;
      multiDragClones.length = 0;
    },
    destroyGlobal: function destroyGlobal() {
      this._deselectMultiDrag();
      off(document, "pointerup", this._deselectMultiDrag);
      off(document, "mouseup", this._deselectMultiDrag);
      off(document, "touchend", this._deselectMultiDrag);
      off(document, "keydown", this._checkKeyDown);
      off(document, "keyup", this._checkKeyUp);
    },
    _deselectMultiDrag: function _deselectMultiDrag(evt) {
      if (typeof dragStarted !== "undefined" && dragStarted)
        return;
      if (multiDragSortable !== this.sortable)
        return;
      if (evt && closest(evt.target, this.options.draggable, this.sortable.el, false))
        return;
      if (evt && evt.button !== 0)
        return;
      while (multiDragElements.length) {
        var el = multiDragElements[0];
        toggleClass(el, this.options.selectedClass, false);
        multiDragElements.shift();
        dispatchEvent({
          sortable: this.sortable,
          rootEl: this.sortable.el,
          name: "deselect",
          targetEl: el,
          originalEvt: evt
        });
      }
    },
    _checkKeyDown: function _checkKeyDown(evt) {
      if (evt.key === this.options.multiDragKey) {
        this.multiDragKeyDown = true;
      }
    },
    _checkKeyUp: function _checkKeyUp(evt) {
      if (evt.key === this.options.multiDragKey) {
        this.multiDragKeyDown = false;
      }
    }
  };
  return _extends(MultiDrag, {
    pluginName: "multiDrag",
    utils: {
      select: function select(el) {
        var sortable = el.parentNode[expando];
        if (!sortable || !sortable.options.multiDrag || ~multiDragElements.indexOf(el))
          return;
        if (multiDragSortable && multiDragSortable !== sortable) {
          multiDragSortable.multiDrag._deselectMultiDrag();
          multiDragSortable = sortable;
        }
        toggleClass(el, sortable.options.selectedClass, true);
        multiDragElements.push(el);
      },
      deselect: function deselect(el) {
        var sortable = el.parentNode[expando], index2 = multiDragElements.indexOf(el);
        if (!sortable || !sortable.options.multiDrag || !~index2)
          return;
        toggleClass(el, sortable.options.selectedClass, false);
        multiDragElements.splice(index2, 1);
      }
    },
    eventProperties: function eventProperties() {
      var _this3 = this;
      var oldIndicies = [], newIndicies = [];
      multiDragElements.forEach(function(multiDragElement) {
        oldIndicies.push({
          multiDragElement,
          index: multiDragElement.sortableIndex
        });
        var newIndex2;
        if (folding && multiDragElement !== dragEl$1) {
          newIndex2 = -1;
        } else if (folding) {
          newIndex2 = index(multiDragElement, ":not(." + _this3.options.selectedClass + ")");
        } else {
          newIndex2 = index(multiDragElement);
        }
        newIndicies.push({
          multiDragElement,
          index: newIndex2
        });
      });
      return {
        items: _toConsumableArray(multiDragElements),
        clones: [].concat(multiDragClones),
        oldIndicies,
        newIndicies
      };
    },
    optionListeners: {
      multiDragKey: function multiDragKey(key) {
        key = key.toLowerCase();
        if (key === "ctrl") {
          key = "Control";
        } else if (key.length > 1) {
          key = key.charAt(0).toUpperCase() + key.substr(1);
        }
        return key;
      }
    }
  });
}
function insertMultiDragElements(clonesInserted, rootEl2) {
  multiDragElements.forEach(function(multiDragElement, i) {
    var target = rootEl2.children[multiDragElement.sortableIndex + (clonesInserted ? Number(i) : 0)];
    if (target) {
      rootEl2.insertBefore(multiDragElement, target);
    } else {
      rootEl2.appendChild(multiDragElement);
    }
  });
}
function insertMultiDragClones(elementsInserted, rootEl2) {
  multiDragClones.forEach(function(clone2, i) {
    var target = rootEl2.children[clone2.sortableIndex + (elementsInserted ? Number(i) : 0)];
    if (target) {
      rootEl2.insertBefore(clone2, target);
    } else {
      rootEl2.appendChild(clone2);
    }
  });
}
function removeMultiDragElements() {
  multiDragElements.forEach(function(multiDragElement) {
    if (multiDragElement === dragEl$1)
      return;
    multiDragElement.parentNode && multiDragElement.parentNode.removeChild(multiDragElement);
  });
}
Sortable.mount(new AutoScrollPlugin());
Sortable.mount(Remove, Revert);
var sortable_esm = /* @__PURE__ */ Object.freeze(/* @__PURE__ */ Object.defineProperty({
  __proto__: null,
  "default": Sortable,
  MultiDrag: MultiDragPlugin,
  Sortable,
  Swap: SwapPlugin
}, Symbol.toStringTag, { value: "Module" }));
var require$$0 = /* @__PURE__ */ getAugmentedNamespace(sortable_esm);
(function(module2, exports) {
  (function webpackUniversalModuleDefinition(root, factory) {
    module2.exports = factory(require$$0);
  })(typeof self !== "undefined" ? self : commonjsGlobal, function(__WEBPACK_EXTERNAL_MODULE_a352__) {
    return function(modules) {
      var installedModules = {};
      function __webpack_require__(moduleId) {
        if (installedModules[moduleId]) {
          return installedModules[moduleId].exports;
        }
        var module3 = installedModules[moduleId] = {
          i: moduleId,
          l: false,
          exports: {}
        };
        modules[moduleId].call(module3.exports, module3, module3.exports, __webpack_require__);
        module3.l = true;
        return module3.exports;
      }
      __webpack_require__.m = modules;
      __webpack_require__.c = installedModules;
      __webpack_require__.d = function(exports2, name2, getter) {
        if (!__webpack_require__.o(exports2, name2)) {
          Object.defineProperty(exports2, name2, { enumerable: true, get: getter });
        }
      };
      __webpack_require__.r = function(exports2) {
        if (typeof Symbol !== "undefined" && Symbol.toStringTag) {
          Object.defineProperty(exports2, Symbol.toStringTag, { value: "Module" });
        }
        Object.defineProperty(exports2, "__esModule", { value: true });
      };
      __webpack_require__.t = function(value, mode) {
        if (mode & 1)
          value = __webpack_require__(value);
        if (mode & 8)
          return value;
        if (mode & 4 && typeof value === "object" && value && value.__esModule)
          return value;
        var ns = /* @__PURE__ */ Object.create(null);
        __webpack_require__.r(ns);
        Object.defineProperty(ns, "default", { enumerable: true, value });
        if (mode & 2 && typeof value != "string")
          for (var key in value)
            __webpack_require__.d(ns, key, function(key2) {
              return value[key2];
            }.bind(null, key));
        return ns;
      };
      __webpack_require__.n = function(module3) {
        var getter = module3 && module3.__esModule ? function getDefault() {
          return module3["default"];
        } : function getModuleExports() {
          return module3;
        };
        __webpack_require__.d(getter, "a", getter);
        return getter;
      };
      __webpack_require__.o = function(object, property) {
        return Object.prototype.hasOwnProperty.call(object, property);
      };
      __webpack_require__.p = "";
      return __webpack_require__(__webpack_require__.s = "fb15");
    }({
      "01f9": function(module3, exports2, __webpack_require__) {
        var LIBRARY = __webpack_require__("2d00");
        var $export = __webpack_require__("5ca1");
        var redefine = __webpack_require__("2aba");
        var hide = __webpack_require__("32e9");
        var Iterators = __webpack_require__("84f2");
        var $iterCreate = __webpack_require__("41a0");
        var setToStringTag = __webpack_require__("7f20");
        var getPrototypeOf = __webpack_require__("38fd");
        var ITERATOR = __webpack_require__("2b4c")("iterator");
        var BUGGY = !([].keys && "next" in [].keys());
        var FF_ITERATOR = "@@iterator";
        var KEYS = "keys";
        var VALUES = "values";
        var returnThis = function() {
          return this;
        };
        module3.exports = function(Base, NAME, Constructor, next, DEFAULT, IS_SET, FORCED) {
          $iterCreate(Constructor, NAME, next);
          var getMethod = function(kind) {
            if (!BUGGY && kind in proto)
              return proto[kind];
            switch (kind) {
              case KEYS:
                return function keys() {
                  return new Constructor(this, kind);
                };
              case VALUES:
                return function values() {
                  return new Constructor(this, kind);
                };
            }
            return function entries() {
              return new Constructor(this, kind);
            };
          };
          var TAG = NAME + " Iterator";
          var DEF_VALUES = DEFAULT == VALUES;
          var VALUES_BUG = false;
          var proto = Base.prototype;
          var $native = proto[ITERATOR] || proto[FF_ITERATOR] || DEFAULT && proto[DEFAULT];
          var $default = $native || getMethod(DEFAULT);
          var $entries = DEFAULT ? !DEF_VALUES ? $default : getMethod("entries") : void 0;
          var $anyNative = NAME == "Array" ? proto.entries || $native : $native;
          var methods, key, IteratorPrototype;
          if ($anyNative) {
            IteratorPrototype = getPrototypeOf($anyNative.call(new Base()));
            if (IteratorPrototype !== Object.prototype && IteratorPrototype.next) {
              setToStringTag(IteratorPrototype, TAG, true);
              if (!LIBRARY && typeof IteratorPrototype[ITERATOR] != "function")
                hide(IteratorPrototype, ITERATOR, returnThis);
            }
          }
          if (DEF_VALUES && $native && $native.name !== VALUES) {
            VALUES_BUG = true;
            $default = function values() {
              return $native.call(this);
            };
          }
          if ((!LIBRARY || FORCED) && (BUGGY || VALUES_BUG || !proto[ITERATOR])) {
            hide(proto, ITERATOR, $default);
          }
          Iterators[NAME] = $default;
          Iterators[TAG] = returnThis;
          if (DEFAULT) {
            methods = {
              values: DEF_VALUES ? $default : getMethod(VALUES),
              keys: IS_SET ? $default : getMethod(KEYS),
              entries: $entries
            };
            if (FORCED)
              for (key in methods) {
                if (!(key in proto))
                  redefine(proto, key, methods[key]);
              }
            else
              $export($export.P + $export.F * (BUGGY || VALUES_BUG), NAME, methods);
          }
          return methods;
        };
      },
      "02f4": function(module3, exports2, __webpack_require__) {
        var toInteger = __webpack_require__("4588");
        var defined = __webpack_require__("be13");
        module3.exports = function(TO_STRING) {
          return function(that, pos) {
            var s = String(defined(that));
            var i = toInteger(pos);
            var l = s.length;
            var a, b;
            if (i < 0 || i >= l)
              return TO_STRING ? "" : void 0;
            a = s.charCodeAt(i);
            return a < 55296 || a > 56319 || i + 1 === l || (b = s.charCodeAt(i + 1)) < 56320 || b > 57343 ? TO_STRING ? s.charAt(i) : a : TO_STRING ? s.slice(i, i + 2) : (a - 55296 << 10) + (b - 56320) + 65536;
          };
        };
      },
      "0390": function(module3, exports2, __webpack_require__) {
        var at = __webpack_require__("02f4")(true);
        module3.exports = function(S, index2, unicode) {
          return index2 + (unicode ? at(S, index2).length : 1);
        };
      },
      "0bfb": function(module3, exports2, __webpack_require__) {
        var anObject = __webpack_require__("cb7c");
        module3.exports = function() {
          var that = anObject(this);
          var result = "";
          if (that.global)
            result += "g";
          if (that.ignoreCase)
            result += "i";
          if (that.multiline)
            result += "m";
          if (that.unicode)
            result += "u";
          if (that.sticky)
            result += "y";
          return result;
        };
      },
      "0d58": function(module3, exports2, __webpack_require__) {
        var $keys = __webpack_require__("ce10");
        var enumBugKeys = __webpack_require__("e11e");
        module3.exports = Object.keys || function keys(O) {
          return $keys(O, enumBugKeys);
        };
      },
      "1495": function(module3, exports2, __webpack_require__) {
        var dP = __webpack_require__("86cc");
        var anObject = __webpack_require__("cb7c");
        var getKeys = __webpack_require__("0d58");
        module3.exports = __webpack_require__("9e1e") ? Object.defineProperties : function defineProperties(O, Properties) {
          anObject(O);
          var keys = getKeys(Properties);
          var length = keys.length;
          var i = 0;
          var P;
          while (length > i)
            dP.f(O, P = keys[i++], Properties[P]);
          return O;
        };
      },
      "214f": function(module3, exports2, __webpack_require__) {
        __webpack_require__("b0c5");
        var redefine = __webpack_require__("2aba");
        var hide = __webpack_require__("32e9");
        var fails = __webpack_require__("79e5");
        var defined = __webpack_require__("be13");
        var wks = __webpack_require__("2b4c");
        var regexpExec = __webpack_require__("520a");
        var SPECIES = wks("species");
        var REPLACE_SUPPORTS_NAMED_GROUPS = !fails(function() {
          var re = /./;
          re.exec = function() {
            var result = [];
            result.groups = { a: "7" };
            return result;
          };
          return "".replace(re, "$<a>") !== "7";
        });
        var SPLIT_WORKS_WITH_OVERWRITTEN_EXEC = function() {
          var re = /(?:)/;
          var originalExec = re.exec;
          re.exec = function() {
            return originalExec.apply(this, arguments);
          };
          var result = "ab".split(re);
          return result.length === 2 && result[0] === "a" && result[1] === "b";
        }();
        module3.exports = function(KEY, length, exec) {
          var SYMBOL = wks(KEY);
          var DELEGATES_TO_SYMBOL = !fails(function() {
            var O = {};
            O[SYMBOL] = function() {
              return 7;
            };
            return ""[KEY](O) != 7;
          });
          var DELEGATES_TO_EXEC = DELEGATES_TO_SYMBOL ? !fails(function() {
            var execCalled = false;
            var re = /a/;
            re.exec = function() {
              execCalled = true;
              return null;
            };
            if (KEY === "split") {
              re.constructor = {};
              re.constructor[SPECIES] = function() {
                return re;
              };
            }
            re[SYMBOL]("");
            return !execCalled;
          }) : void 0;
          if (!DELEGATES_TO_SYMBOL || !DELEGATES_TO_EXEC || KEY === "replace" && !REPLACE_SUPPORTS_NAMED_GROUPS || KEY === "split" && !SPLIT_WORKS_WITH_OVERWRITTEN_EXEC) {
            var nativeRegExpMethod = /./[SYMBOL];
            var fns = exec(
              defined,
              SYMBOL,
              ""[KEY],
              function maybeCallNative(nativeMethod, regexp, str, arg2, forceStringMethod) {
                if (regexp.exec === regexpExec) {
                  if (DELEGATES_TO_SYMBOL && !forceStringMethod) {
                    return { done: true, value: nativeRegExpMethod.call(regexp, str, arg2) };
                  }
                  return { done: true, value: nativeMethod.call(str, regexp, arg2) };
                }
                return { done: false };
              }
            );
            var strfn = fns[0];
            var rxfn = fns[1];
            redefine(String.prototype, KEY, strfn);
            hide(
              RegExp.prototype,
              SYMBOL,
              length == 2 ? function(string, arg) {
                return rxfn.call(string, this, arg);
              } : function(string) {
                return rxfn.call(string, this);
              }
            );
          }
        };
      },
      "230e": function(module3, exports2, __webpack_require__) {
        var isObject = __webpack_require__("d3f4");
        var document2 = __webpack_require__("7726").document;
        var is2 = isObject(document2) && isObject(document2.createElement);
        module3.exports = function(it) {
          return is2 ? document2.createElement(it) : {};
        };
      },
      "23c6": function(module3, exports2, __webpack_require__) {
        var cof = __webpack_require__("2d95");
        var TAG = __webpack_require__("2b4c")("toStringTag");
        var ARG = cof(function() {
          return arguments;
        }()) == "Arguments";
        var tryGet = function(it, key) {
          try {
            return it[key];
          } catch (e) {
          }
        };
        module3.exports = function(it) {
          var O, T, B;
          return it === void 0 ? "Undefined" : it === null ? "Null" : typeof (T = tryGet(O = Object(it), TAG)) == "string" ? T : ARG ? cof(O) : (B = cof(O)) == "Object" && typeof O.callee == "function" ? "Arguments" : B;
        };
      },
      "2621": function(module3, exports2) {
        exports2.f = Object.getOwnPropertySymbols;
      },
      "2aba": function(module3, exports2, __webpack_require__) {
        var global2 = __webpack_require__("7726");
        var hide = __webpack_require__("32e9");
        var has = __webpack_require__("69a8");
        var SRC = __webpack_require__("ca5a")("src");
        var $toString = __webpack_require__("fa5b");
        var TO_STRING = "toString";
        var TPL = ("" + $toString).split(TO_STRING);
        __webpack_require__("8378").inspectSource = function(it) {
          return $toString.call(it);
        };
        (module3.exports = function(O, key, val, safe) {
          var isFunction = typeof val == "function";
          if (isFunction)
            has(val, "name") || hide(val, "name", key);
          if (O[key] === val)
            return;
          if (isFunction)
            has(val, SRC) || hide(val, SRC, O[key] ? "" + O[key] : TPL.join(String(key)));
          if (O === global2) {
            O[key] = val;
          } else if (!safe) {
            delete O[key];
            hide(O, key, val);
          } else if (O[key]) {
            O[key] = val;
          } else {
            hide(O, key, val);
          }
        })(Function.prototype, TO_STRING, function toString() {
          return typeof this == "function" && this[SRC] || $toString.call(this);
        });
      },
      "2aeb": function(module3, exports2, __webpack_require__) {
        var anObject = __webpack_require__("cb7c");
        var dPs = __webpack_require__("1495");
        var enumBugKeys = __webpack_require__("e11e");
        var IE_PROTO = __webpack_require__("613b")("IE_PROTO");
        var Empty = function() {
        };
        var PROTOTYPE = "prototype";
        var createDict = function() {
          var iframe = __webpack_require__("230e")("iframe");
          var i = enumBugKeys.length;
          var lt = "<";
          var gt = ">";
          var iframeDocument;
          iframe.style.display = "none";
          __webpack_require__("fab2").appendChild(iframe);
          iframe.src = "javascript:";
          iframeDocument = iframe.contentWindow.document;
          iframeDocument.open();
          iframeDocument.write(lt + "script" + gt + "document.F=Object" + lt + "/script" + gt);
          iframeDocument.close();
          createDict = iframeDocument.F;
          while (i--)
            delete createDict[PROTOTYPE][enumBugKeys[i]];
          return createDict();
        };
        module3.exports = Object.create || function create(O, Properties) {
          var result;
          if (O !== null) {
            Empty[PROTOTYPE] = anObject(O);
            result = new Empty();
            Empty[PROTOTYPE] = null;
            result[IE_PROTO] = O;
          } else
            result = createDict();
          return Properties === void 0 ? result : dPs(result, Properties);
        };
      },
      "2b4c": function(module3, exports2, __webpack_require__) {
        var store2 = __webpack_require__("5537")("wks");
        var uid = __webpack_require__("ca5a");
        var Symbol2 = __webpack_require__("7726").Symbol;
        var USE_SYMBOL = typeof Symbol2 == "function";
        var $exports = module3.exports = function(name2) {
          return store2[name2] || (store2[name2] = USE_SYMBOL && Symbol2[name2] || (USE_SYMBOL ? Symbol2 : uid)("Symbol." + name2));
        };
        $exports.store = store2;
      },
      "2d00": function(module3, exports2) {
        module3.exports = false;
      },
      "2d95": function(module3, exports2) {
        var toString = {}.toString;
        module3.exports = function(it) {
          return toString.call(it).slice(8, -1);
        };
      },
      "2fdb": function(module3, exports2, __webpack_require__) {
        var $export = __webpack_require__("5ca1");
        var context = __webpack_require__("d2c8");
        var INCLUDES = "includes";
        $export($export.P + $export.F * __webpack_require__("5147")(INCLUDES), "String", {
          includes: function includes(searchString) {
            return !!~context(this, searchString, INCLUDES).indexOf(searchString, arguments.length > 1 ? arguments[1] : void 0);
          }
        });
      },
      "32e9": function(module3, exports2, __webpack_require__) {
        var dP = __webpack_require__("86cc");
        var createDesc = __webpack_require__("4630");
        module3.exports = __webpack_require__("9e1e") ? function(object, key, value) {
          return dP.f(object, key, createDesc(1, value));
        } : function(object, key, value) {
          object[key] = value;
          return object;
        };
      },
      "38fd": function(module3, exports2, __webpack_require__) {
        var has = __webpack_require__("69a8");
        var toObject = __webpack_require__("4bf8");
        var IE_PROTO = __webpack_require__("613b")("IE_PROTO");
        var ObjectProto = Object.prototype;
        module3.exports = Object.getPrototypeOf || function(O) {
          O = toObject(O);
          if (has(O, IE_PROTO))
            return O[IE_PROTO];
          if (typeof O.constructor == "function" && O instanceof O.constructor) {
            return O.constructor.prototype;
          }
          return O instanceof Object ? ObjectProto : null;
        };
      },
      "41a0": function(module3, exports2, __webpack_require__) {
        var create = __webpack_require__("2aeb");
        var descriptor = __webpack_require__("4630");
        var setToStringTag = __webpack_require__("7f20");
        var IteratorPrototype = {};
        __webpack_require__("32e9")(IteratorPrototype, __webpack_require__("2b4c")("iterator"), function() {
          return this;
        });
        module3.exports = function(Constructor, NAME, next) {
          Constructor.prototype = create(IteratorPrototype, { next: descriptor(1, next) });
          setToStringTag(Constructor, NAME + " Iterator");
        };
      },
      "456d": function(module3, exports2, __webpack_require__) {
        var toObject = __webpack_require__("4bf8");
        var $keys = __webpack_require__("0d58");
        __webpack_require__("5eda")("keys", function() {
          return function keys(it) {
            return $keys(toObject(it));
          };
        });
      },
      "4588": function(module3, exports2) {
        var ceil = Math.ceil;
        var floor = Math.floor;
        module3.exports = function(it) {
          return isNaN(it = +it) ? 0 : (it > 0 ? floor : ceil)(it);
        };
      },
      "4630": function(module3, exports2) {
        module3.exports = function(bitmap, value) {
          return {
            enumerable: !(bitmap & 1),
            configurable: !(bitmap & 2),
            writable: !(bitmap & 4),
            value
          };
        };
      },
      "4bf8": function(module3, exports2, __webpack_require__) {
        var defined = __webpack_require__("be13");
        module3.exports = function(it) {
          return Object(defined(it));
        };
      },
      "5147": function(module3, exports2, __webpack_require__) {
        var MATCH = __webpack_require__("2b4c")("match");
        module3.exports = function(KEY) {
          var re = /./;
          try {
            "/./"[KEY](re);
          } catch (e) {
            try {
              re[MATCH] = false;
              return !"/./"[KEY](re);
            } catch (f) {
            }
          }
          return true;
        };
      },
      "520a": function(module3, exports2, __webpack_require__) {
        var regexpFlags = __webpack_require__("0bfb");
        var nativeExec = RegExp.prototype.exec;
        var nativeReplace = String.prototype.replace;
        var patchedExec = nativeExec;
        var LAST_INDEX = "lastIndex";
        var UPDATES_LAST_INDEX_WRONG = function() {
          var re1 = /a/, re2 = /b*/g;
          nativeExec.call(re1, "a");
          nativeExec.call(re2, "a");
          return re1[LAST_INDEX] !== 0 || re2[LAST_INDEX] !== 0;
        }();
        var NPCG_INCLUDED = /()??/.exec("")[1] !== void 0;
        var PATCH = UPDATES_LAST_INDEX_WRONG || NPCG_INCLUDED;
        if (PATCH) {
          patchedExec = function exec(str) {
            var re = this;
            var lastIndex, reCopy, match, i;
            if (NPCG_INCLUDED) {
              reCopy = new RegExp("^" + re.source + "$(?!\\s)", regexpFlags.call(re));
            }
            if (UPDATES_LAST_INDEX_WRONG)
              lastIndex = re[LAST_INDEX];
            match = nativeExec.call(re, str);
            if (UPDATES_LAST_INDEX_WRONG && match) {
              re[LAST_INDEX] = re.global ? match.index + match[0].length : lastIndex;
            }
            if (NPCG_INCLUDED && match && match.length > 1) {
              nativeReplace.call(match[0], reCopy, function() {
                for (i = 1; i < arguments.length - 2; i++) {
                  if (arguments[i] === void 0)
                    match[i] = void 0;
                }
              });
            }
            return match;
          };
        }
        module3.exports = patchedExec;
      },
      "52a7": function(module3, exports2) {
        exports2.f = {}.propertyIsEnumerable;
      },
      "5537": function(module3, exports2, __webpack_require__) {
        var core = __webpack_require__("8378");
        var global2 = __webpack_require__("7726");
        var SHARED = "__core-js_shared__";
        var store2 = global2[SHARED] || (global2[SHARED] = {});
        (module3.exports = function(key, value) {
          return store2[key] || (store2[key] = value !== void 0 ? value : {});
        })("versions", []).push({
          version: core.version,
          mode: __webpack_require__("2d00") ? "pure" : "global",
          copyright: "\xA9 2019 Denis Pushkarev (zloirock.ru)"
        });
      },
      "5ca1": function(module3, exports2, __webpack_require__) {
        var global2 = __webpack_require__("7726");
        var core = __webpack_require__("8378");
        var hide = __webpack_require__("32e9");
        var redefine = __webpack_require__("2aba");
        var ctx = __webpack_require__("9b43");
        var PROTOTYPE = "prototype";
        var $export = function(type, name2, source) {
          var IS_FORCED = type & $export.F;
          var IS_GLOBAL = type & $export.G;
          var IS_STATIC = type & $export.S;
          var IS_PROTO = type & $export.P;
          var IS_BIND = type & $export.B;
          var target = IS_GLOBAL ? global2 : IS_STATIC ? global2[name2] || (global2[name2] = {}) : (global2[name2] || {})[PROTOTYPE];
          var exports3 = IS_GLOBAL ? core : core[name2] || (core[name2] = {});
          var expProto = exports3[PROTOTYPE] || (exports3[PROTOTYPE] = {});
          var key, own, out, exp;
          if (IS_GLOBAL)
            source = name2;
          for (key in source) {
            own = !IS_FORCED && target && target[key] !== void 0;
            out = (own ? target : source)[key];
            exp = IS_BIND && own ? ctx(out, global2) : IS_PROTO && typeof out == "function" ? ctx(Function.call, out) : out;
            if (target)
              redefine(target, key, out, type & $export.U);
            if (exports3[key] != out)
              hide(exports3, key, exp);
            if (IS_PROTO && expProto[key] != out)
              expProto[key] = out;
          }
        };
        global2.core = core;
        $export.F = 1;
        $export.G = 2;
        $export.S = 4;
        $export.P = 8;
        $export.B = 16;
        $export.W = 32;
        $export.U = 64;
        $export.R = 128;
        module3.exports = $export;
      },
      "5eda": function(module3, exports2, __webpack_require__) {
        var $export = __webpack_require__("5ca1");
        var core = __webpack_require__("8378");
        var fails = __webpack_require__("79e5");
        module3.exports = function(KEY, exec) {
          var fn = (core.Object || {})[KEY] || Object[KEY];
          var exp = {};
          exp[KEY] = exec(fn);
          $export($export.S + $export.F * fails(function() {
            fn(1);
          }), "Object", exp);
        };
      },
      "5f1b": function(module3, exports2, __webpack_require__) {
        var classof = __webpack_require__("23c6");
        var builtinExec = RegExp.prototype.exec;
        module3.exports = function(R, S) {
          var exec = R.exec;
          if (typeof exec === "function") {
            var result = exec.call(R, S);
            if (typeof result !== "object") {
              throw new TypeError("RegExp exec method returned something other than an Object or null");
            }
            return result;
          }
          if (classof(R) !== "RegExp") {
            throw new TypeError("RegExp#exec called on incompatible receiver");
          }
          return builtinExec.call(R, S);
        };
      },
      "613b": function(module3, exports2, __webpack_require__) {
        var shared = __webpack_require__("5537")("keys");
        var uid = __webpack_require__("ca5a");
        module3.exports = function(key) {
          return shared[key] || (shared[key] = uid(key));
        };
      },
      "626a": function(module3, exports2, __webpack_require__) {
        var cof = __webpack_require__("2d95");
        module3.exports = Object("z").propertyIsEnumerable(0) ? Object : function(it) {
          return cof(it) == "String" ? it.split("") : Object(it);
        };
      },
      "6762": function(module3, exports2, __webpack_require__) {
        var $export = __webpack_require__("5ca1");
        var $includes = __webpack_require__("c366")(true);
        $export($export.P, "Array", {
          includes: function includes(el) {
            return $includes(this, el, arguments.length > 1 ? arguments[1] : void 0);
          }
        });
        __webpack_require__("9c6c")("includes");
      },
      "6821": function(module3, exports2, __webpack_require__) {
        var IObject = __webpack_require__("626a");
        var defined = __webpack_require__("be13");
        module3.exports = function(it) {
          return IObject(defined(it));
        };
      },
      "69a8": function(module3, exports2) {
        var hasOwnProperty = {}.hasOwnProperty;
        module3.exports = function(it, key) {
          return hasOwnProperty.call(it, key);
        };
      },
      "6a99": function(module3, exports2, __webpack_require__) {
        var isObject = __webpack_require__("d3f4");
        module3.exports = function(it, S) {
          if (!isObject(it))
            return it;
          var fn, val;
          if (S && typeof (fn = it.toString) == "function" && !isObject(val = fn.call(it)))
            return val;
          if (typeof (fn = it.valueOf) == "function" && !isObject(val = fn.call(it)))
            return val;
          if (!S && typeof (fn = it.toString) == "function" && !isObject(val = fn.call(it)))
            return val;
          throw TypeError("Can't convert object to primitive value");
        };
      },
      "7333": function(module3, exports2, __webpack_require__) {
        var getKeys = __webpack_require__("0d58");
        var gOPS = __webpack_require__("2621");
        var pIE = __webpack_require__("52a7");
        var toObject = __webpack_require__("4bf8");
        var IObject = __webpack_require__("626a");
        var $assign = Object.assign;
        module3.exports = !$assign || __webpack_require__("79e5")(function() {
          var A = {};
          var B = {};
          var S = Symbol();
          var K = "abcdefghijklmnopqrst";
          A[S] = 7;
          K.split("").forEach(function(k) {
            B[k] = k;
          });
          return $assign({}, A)[S] != 7 || Object.keys($assign({}, B)).join("") != K;
        }) ? function assign(target, source) {
          var T = toObject(target);
          var aLen = arguments.length;
          var index2 = 1;
          var getSymbols = gOPS.f;
          var isEnum = pIE.f;
          while (aLen > index2) {
            var S = IObject(arguments[index2++]);
            var keys = getSymbols ? getKeys(S).concat(getSymbols(S)) : getKeys(S);
            var length = keys.length;
            var j = 0;
            var key;
            while (length > j)
              if (isEnum.call(S, key = keys[j++]))
                T[key] = S[key];
          }
          return T;
        } : $assign;
      },
      "7726": function(module3, exports2) {
        var global2 = module3.exports = typeof window != "undefined" && window.Math == Math ? window : typeof self != "undefined" && self.Math == Math ? self : Function("return this")();
        if (typeof __g == "number")
          __g = global2;
      },
      "77f1": function(module3, exports2, __webpack_require__) {
        var toInteger = __webpack_require__("4588");
        var max = Math.max;
        var min = Math.min;
        module3.exports = function(index2, length) {
          index2 = toInteger(index2);
          return index2 < 0 ? max(index2 + length, 0) : min(index2, length);
        };
      },
      "79e5": function(module3, exports2) {
        module3.exports = function(exec) {
          try {
            return !!exec();
          } catch (e) {
            return true;
          }
        };
      },
      "7f20": function(module3, exports2, __webpack_require__) {
        var def = __webpack_require__("86cc").f;
        var has = __webpack_require__("69a8");
        var TAG = __webpack_require__("2b4c")("toStringTag");
        module3.exports = function(it, tag, stat) {
          if (it && !has(it = stat ? it : it.prototype, TAG))
            def(it, TAG, { configurable: true, value: tag });
        };
      },
      "8378": function(module3, exports2) {
        var core = module3.exports = { version: "2.6.5" };
        if (typeof __e == "number")
          __e = core;
      },
      "84f2": function(module3, exports2) {
        module3.exports = {};
      },
      "86cc": function(module3, exports2, __webpack_require__) {
        var anObject = __webpack_require__("cb7c");
        var IE8_DOM_DEFINE = __webpack_require__("c69a");
        var toPrimitive = __webpack_require__("6a99");
        var dP = Object.defineProperty;
        exports2.f = __webpack_require__("9e1e") ? Object.defineProperty : function defineProperty(O, P, Attributes) {
          anObject(O);
          P = toPrimitive(P, true);
          anObject(Attributes);
          if (IE8_DOM_DEFINE)
            try {
              return dP(O, P, Attributes);
            } catch (e) {
            }
          if ("get" in Attributes || "set" in Attributes)
            throw TypeError("Accessors not supported!");
          if ("value" in Attributes)
            O[P] = Attributes.value;
          return O;
        };
      },
      "9b43": function(module3, exports2, __webpack_require__) {
        var aFunction = __webpack_require__("d8e8");
        module3.exports = function(fn, that, length) {
          aFunction(fn);
          if (that === void 0)
            return fn;
          switch (length) {
            case 1:
              return function(a) {
                return fn.call(that, a);
              };
            case 2:
              return function(a, b) {
                return fn.call(that, a, b);
              };
            case 3:
              return function(a, b, c) {
                return fn.call(that, a, b, c);
              };
          }
          return function() {
            return fn.apply(that, arguments);
          };
        };
      },
      "9c6c": function(module3, exports2, __webpack_require__) {
        var UNSCOPABLES = __webpack_require__("2b4c")("unscopables");
        var ArrayProto = Array.prototype;
        if (ArrayProto[UNSCOPABLES] == void 0)
          __webpack_require__("32e9")(ArrayProto, UNSCOPABLES, {});
        module3.exports = function(key) {
          ArrayProto[UNSCOPABLES][key] = true;
        };
      },
      "9def": function(module3, exports2, __webpack_require__) {
        var toInteger = __webpack_require__("4588");
        var min = Math.min;
        module3.exports = function(it) {
          return it > 0 ? min(toInteger(it), 9007199254740991) : 0;
        };
      },
      "9e1e": function(module3, exports2, __webpack_require__) {
        module3.exports = !__webpack_require__("79e5")(function() {
          return Object.defineProperty({}, "a", { get: function() {
            return 7;
          } }).a != 7;
        });
      },
      "a352": function(module3, exports2) {
        module3.exports = __WEBPACK_EXTERNAL_MODULE_a352__;
      },
      "a481": function(module3, exports2, __webpack_require__) {
        var anObject = __webpack_require__("cb7c");
        var toObject = __webpack_require__("4bf8");
        var toLength = __webpack_require__("9def");
        var toInteger = __webpack_require__("4588");
        var advanceStringIndex = __webpack_require__("0390");
        var regExpExec = __webpack_require__("5f1b");
        var max = Math.max;
        var min = Math.min;
        var floor = Math.floor;
        var SUBSTITUTION_SYMBOLS = /\$([$&`']|\d\d?|<[^>]*>)/g;
        var SUBSTITUTION_SYMBOLS_NO_NAMED = /\$([$&`']|\d\d?)/g;
        var maybeToString = function(it) {
          return it === void 0 ? it : String(it);
        };
        __webpack_require__("214f")("replace", 2, function(defined, REPLACE, $replace, maybeCallNative) {
          return [
            function replace(searchValue, replaceValue) {
              var O = defined(this);
              var fn = searchValue == void 0 ? void 0 : searchValue[REPLACE];
              return fn !== void 0 ? fn.call(searchValue, O, replaceValue) : $replace.call(String(O), searchValue, replaceValue);
            },
            function(regexp, replaceValue) {
              var res = maybeCallNative($replace, regexp, this, replaceValue);
              if (res.done)
                return res.value;
              var rx = anObject(regexp);
              var S = String(this);
              var functionalReplace = typeof replaceValue === "function";
              if (!functionalReplace)
                replaceValue = String(replaceValue);
              var global2 = rx.global;
              if (global2) {
                var fullUnicode = rx.unicode;
                rx.lastIndex = 0;
              }
              var results = [];
              while (true) {
                var result = regExpExec(rx, S);
                if (result === null)
                  break;
                results.push(result);
                if (!global2)
                  break;
                var matchStr = String(result[0]);
                if (matchStr === "")
                  rx.lastIndex = advanceStringIndex(S, toLength(rx.lastIndex), fullUnicode);
              }
              var accumulatedResult = "";
              var nextSourcePosition = 0;
              for (var i = 0; i < results.length; i++) {
                result = results[i];
                var matched = String(result[0]);
                var position = max(min(toInteger(result.index), S.length), 0);
                var captures = [];
                for (var j = 1; j < result.length; j++)
                  captures.push(maybeToString(result[j]));
                var namedCaptures = result.groups;
                if (functionalReplace) {
                  var replacerArgs = [matched].concat(captures, position, S);
                  if (namedCaptures !== void 0)
                    replacerArgs.push(namedCaptures);
                  var replacement = String(replaceValue.apply(void 0, replacerArgs));
                } else {
                  replacement = getSubstitution(matched, S, position, captures, namedCaptures, replaceValue);
                }
                if (position >= nextSourcePosition) {
                  accumulatedResult += S.slice(nextSourcePosition, position) + replacement;
                  nextSourcePosition = position + matched.length;
                }
              }
              return accumulatedResult + S.slice(nextSourcePosition);
            }
          ];
          function getSubstitution(matched, str, position, captures, namedCaptures, replacement) {
            var tailPos = position + matched.length;
            var m = captures.length;
            var symbols = SUBSTITUTION_SYMBOLS_NO_NAMED;
            if (namedCaptures !== void 0) {
              namedCaptures = toObject(namedCaptures);
              symbols = SUBSTITUTION_SYMBOLS;
            }
            return $replace.call(replacement, symbols, function(match, ch) {
              var capture;
              switch (ch.charAt(0)) {
                case "$":
                  return "$";
                case "&":
                  return matched;
                case "`":
                  return str.slice(0, position);
                case "'":
                  return str.slice(tailPos);
                case "<":
                  capture = namedCaptures[ch.slice(1, -1)];
                  break;
                default:
                  var n = +ch;
                  if (n === 0)
                    return match;
                  if (n > m) {
                    var f = floor(n / 10);
                    if (f === 0)
                      return match;
                    if (f <= m)
                      return captures[f - 1] === void 0 ? ch.charAt(1) : captures[f - 1] + ch.charAt(1);
                    return match;
                  }
                  capture = captures[n - 1];
              }
              return capture === void 0 ? "" : capture;
            });
          }
        });
      },
      "aae3": function(module3, exports2, __webpack_require__) {
        var isObject = __webpack_require__("d3f4");
        var cof = __webpack_require__("2d95");
        var MATCH = __webpack_require__("2b4c")("match");
        module3.exports = function(it) {
          var isRegExp;
          return isObject(it) && ((isRegExp = it[MATCH]) !== void 0 ? !!isRegExp : cof(it) == "RegExp");
        };
      },
      "ac6a": function(module3, exports2, __webpack_require__) {
        var $iterators = __webpack_require__("cadf");
        var getKeys = __webpack_require__("0d58");
        var redefine = __webpack_require__("2aba");
        var global2 = __webpack_require__("7726");
        var hide = __webpack_require__("32e9");
        var Iterators = __webpack_require__("84f2");
        var wks = __webpack_require__("2b4c");
        var ITERATOR = wks("iterator");
        var TO_STRING_TAG = wks("toStringTag");
        var ArrayValues = Iterators.Array;
        var DOMIterables = {
          CSSRuleList: true,
          CSSStyleDeclaration: false,
          CSSValueList: false,
          ClientRectList: false,
          DOMRectList: false,
          DOMStringList: false,
          DOMTokenList: true,
          DataTransferItemList: false,
          FileList: false,
          HTMLAllCollection: false,
          HTMLCollection: false,
          HTMLFormElement: false,
          HTMLSelectElement: false,
          MediaList: true,
          MimeTypeArray: false,
          NamedNodeMap: false,
          NodeList: true,
          PaintRequestList: false,
          Plugin: false,
          PluginArray: false,
          SVGLengthList: false,
          SVGNumberList: false,
          SVGPathSegList: false,
          SVGPointList: false,
          SVGStringList: false,
          SVGTransformList: false,
          SourceBufferList: false,
          StyleSheetList: true,
          TextTrackCueList: false,
          TextTrackList: false,
          TouchList: false
        };
        for (var collections = getKeys(DOMIterables), i = 0; i < collections.length; i++) {
          var NAME = collections[i];
          var explicit = DOMIterables[NAME];
          var Collection = global2[NAME];
          var proto = Collection && Collection.prototype;
          var key;
          if (proto) {
            if (!proto[ITERATOR])
              hide(proto, ITERATOR, ArrayValues);
            if (!proto[TO_STRING_TAG])
              hide(proto, TO_STRING_TAG, NAME);
            Iterators[NAME] = ArrayValues;
            if (explicit) {
              for (key in $iterators)
                if (!proto[key])
                  redefine(proto, key, $iterators[key], true);
            }
          }
        }
      },
      "b0c5": function(module3, exports2, __webpack_require__) {
        var regexpExec = __webpack_require__("520a");
        __webpack_require__("5ca1")({
          target: "RegExp",
          proto: true,
          forced: regexpExec !== /./.exec
        }, {
          exec: regexpExec
        });
      },
      "be13": function(module3, exports2) {
        module3.exports = function(it) {
          if (it == void 0)
            throw TypeError("Can't call method on  " + it);
          return it;
        };
      },
      "c366": function(module3, exports2, __webpack_require__) {
        var toIObject = __webpack_require__("6821");
        var toLength = __webpack_require__("9def");
        var toAbsoluteIndex = __webpack_require__("77f1");
        module3.exports = function(IS_INCLUDES) {
          return function($this, el, fromIndex) {
            var O = toIObject($this);
            var length = toLength(O.length);
            var index2 = toAbsoluteIndex(fromIndex, length);
            var value;
            if (IS_INCLUDES && el != el)
              while (length > index2) {
                value = O[index2++];
                if (value != value)
                  return true;
              }
            else
              for (; length > index2; index2++)
                if (IS_INCLUDES || index2 in O) {
                  if (O[index2] === el)
                    return IS_INCLUDES || index2 || 0;
                }
            return !IS_INCLUDES && -1;
          };
        };
      },
      "c649": function(module3, __webpack_exports__, __webpack_require__) {
        (function(global2) {
          __webpack_require__.d(__webpack_exports__, "c", function() {
            return insertNodeAt;
          });
          __webpack_require__.d(__webpack_exports__, "a", function() {
            return camelize;
          });
          __webpack_require__.d(__webpack_exports__, "b", function() {
            return console2;
          });
          __webpack_require__.d(__webpack_exports__, "d", function() {
            return removeNode;
          });
          __webpack_require__("a481");
          function getConsole() {
            if (typeof window !== "undefined") {
              return window.console;
            }
            return global2.console;
          }
          var console2 = getConsole();
          function cached(fn) {
            var cache = /* @__PURE__ */ Object.create(null);
            return function cachedFn(str) {
              var hit = cache[str];
              return hit || (cache[str] = fn(str));
            };
          }
          var regex2 = /-(\w)/g;
          var camelize = cached(function(str) {
            return str.replace(regex2, function(_, c) {
              return c ? c.toUpperCase() : "";
            });
          });
          function removeNode(node) {
            if (node.parentElement !== null) {
              node.parentElement.removeChild(node);
            }
          }
          function insertNodeAt(fatherNode, node, position) {
            var refNode = position === 0 ? fatherNode.children[0] : fatherNode.children[position - 1].nextSibling;
            fatherNode.insertBefore(node, refNode);
          }
        }).call(this, __webpack_require__("c8ba"));
      },
      "c69a": function(module3, exports2, __webpack_require__) {
        module3.exports = !__webpack_require__("9e1e") && !__webpack_require__("79e5")(function() {
          return Object.defineProperty(__webpack_require__("230e")("div"), "a", { get: function() {
            return 7;
          } }).a != 7;
        });
      },
      "c8ba": function(module3, exports2) {
        var g;
        g = function() {
          return this;
        }();
        try {
          g = g || new Function("return this")();
        } catch (e) {
          if (typeof window === "object")
            g = window;
        }
        module3.exports = g;
      },
      "ca5a": function(module3, exports2) {
        var id2 = 0;
        var px = Math.random();
        module3.exports = function(key) {
          return "Symbol(".concat(key === void 0 ? "" : key, ")_", (++id2 + px).toString(36));
        };
      },
      "cadf": function(module3, exports2, __webpack_require__) {
        var addToUnscopables = __webpack_require__("9c6c");
        var step = __webpack_require__("d53b");
        var Iterators = __webpack_require__("84f2");
        var toIObject = __webpack_require__("6821");
        module3.exports = __webpack_require__("01f9")(Array, "Array", function(iterated, kind) {
          this._t = toIObject(iterated);
          this._i = 0;
          this._k = kind;
        }, function() {
          var O = this._t;
          var kind = this._k;
          var index2 = this._i++;
          if (!O || index2 >= O.length) {
            this._t = void 0;
            return step(1);
          }
          if (kind == "keys")
            return step(0, index2);
          if (kind == "values")
            return step(0, O[index2]);
          return step(0, [index2, O[index2]]);
        }, "values");
        Iterators.Arguments = Iterators.Array;
        addToUnscopables("keys");
        addToUnscopables("values");
        addToUnscopables("entries");
      },
      "cb7c": function(module3, exports2, __webpack_require__) {
        var isObject = __webpack_require__("d3f4");
        module3.exports = function(it) {
          if (!isObject(it))
            throw TypeError(it + " is not an object!");
          return it;
        };
      },
      "ce10": function(module3, exports2, __webpack_require__) {
        var has = __webpack_require__("69a8");
        var toIObject = __webpack_require__("6821");
        var arrayIndexOf = __webpack_require__("c366")(false);
        var IE_PROTO = __webpack_require__("613b")("IE_PROTO");
        module3.exports = function(object, names) {
          var O = toIObject(object);
          var i = 0;
          var result = [];
          var key;
          for (key in O)
            if (key != IE_PROTO)
              has(O, key) && result.push(key);
          while (names.length > i)
            if (has(O, key = names[i++])) {
              ~arrayIndexOf(result, key) || result.push(key);
            }
          return result;
        };
      },
      "d2c8": function(module3, exports2, __webpack_require__) {
        var isRegExp = __webpack_require__("aae3");
        var defined = __webpack_require__("be13");
        module3.exports = function(that, searchString, NAME) {
          if (isRegExp(searchString))
            throw TypeError("String#" + NAME + " doesn't accept regex!");
          return String(defined(that));
        };
      },
      "d3f4": function(module3, exports2) {
        module3.exports = function(it) {
          return typeof it === "object" ? it !== null : typeof it === "function";
        };
      },
      "d53b": function(module3, exports2) {
        module3.exports = function(done, value) {
          return { value, done: !!done };
        };
      },
      "d8e8": function(module3, exports2) {
        module3.exports = function(it) {
          if (typeof it != "function")
            throw TypeError(it + " is not a function!");
          return it;
        };
      },
      "e11e": function(module3, exports2) {
        module3.exports = "constructor,hasOwnProperty,isPrototypeOf,propertyIsEnumerable,toLocaleString,toString,valueOf".split(",");
      },
      "f559": function(module3, exports2, __webpack_require__) {
        var $export = __webpack_require__("5ca1");
        var toLength = __webpack_require__("9def");
        var context = __webpack_require__("d2c8");
        var STARTS_WITH = "startsWith";
        var $startsWith = ""[STARTS_WITH];
        $export($export.P + $export.F * __webpack_require__("5147")(STARTS_WITH), "String", {
          startsWith: function startsWith(searchString) {
            var that = context(this, searchString, STARTS_WITH);
            var index2 = toLength(Math.min(arguments.length > 1 ? arguments[1] : void 0, that.length));
            var search = String(searchString);
            return $startsWith ? $startsWith.call(that, search, index2) : that.slice(index2, index2 + search.length) === search;
          }
        });
      },
      "f6fd": function(module3, exports2) {
        (function(document2) {
          var currentScript = "currentScript", scripts2 = document2.getElementsByTagName("script");
          if (!(currentScript in document2)) {
            Object.defineProperty(document2, currentScript, {
              get: function() {
                try {
                  throw new Error();
                } catch (err) {
                  var i, res = (/.*at [^\(]*\((.*):.+:.+\)$/ig.exec(err.stack) || [false])[1];
                  for (i in scripts2) {
                    if (scripts2[i].src == res || scripts2[i].readyState == "interactive") {
                      return scripts2[i];
                    }
                  }
                  return null;
                }
              }
            });
          }
        })(document);
      },
      "f751": function(module3, exports2, __webpack_require__) {
        var $export = __webpack_require__("5ca1");
        $export($export.S + $export.F, "Object", { assign: __webpack_require__("7333") });
      },
      "fa5b": function(module3, exports2, __webpack_require__) {
        module3.exports = __webpack_require__("5537")("native-function-to-string", Function.toString);
      },
      "fab2": function(module3, exports2, __webpack_require__) {
        var document2 = __webpack_require__("7726").document;
        module3.exports = document2 && document2.documentElement;
      },
      "fb15": function(module3, __webpack_exports__, __webpack_require__) {
        __webpack_require__.r(__webpack_exports__);
        if (typeof window !== "undefined") {
          {
            __webpack_require__("f6fd");
          }
          var setPublicPath_i;
          if ((setPublicPath_i = window.document.currentScript) && (setPublicPath_i = setPublicPath_i.src.match(/(.+\/)[^/]+\.js(\?.*)?$/))) {
            __webpack_require__.p = setPublicPath_i[1];
          }
        }
        __webpack_require__("f751");
        __webpack_require__("f559");
        __webpack_require__("ac6a");
        __webpack_require__("cadf");
        __webpack_require__("456d");
        function _arrayWithHoles(arr) {
          if (Array.isArray(arr))
            return arr;
        }
        function _iterableToArrayLimit(arr, i) {
          if (typeof Symbol === "undefined" || !(Symbol.iterator in Object(arr)))
            return;
          var _arr = [];
          var _n = true;
          var _d = false;
          var _e = void 0;
          try {
            for (var _i = arr[Symbol.iterator](), _s; !(_n = (_s = _i.next()).done); _n = true) {
              _arr.push(_s.value);
              if (i && _arr.length === i)
                break;
            }
          } catch (err) {
            _d = true;
            _e = err;
          } finally {
            try {
              if (!_n && _i["return"] != null)
                _i["return"]();
            } finally {
              if (_d)
                throw _e;
            }
          }
          return _arr;
        }
        function _arrayLikeToArray(arr, len) {
          if (len == null || len > arr.length)
            len = arr.length;
          for (var i = 0, arr2 = new Array(len); i < len; i++) {
            arr2[i] = arr[i];
          }
          return arr2;
        }
        function _unsupportedIterableToArray(o, minLen) {
          if (!o)
            return;
          if (typeof o === "string")
            return _arrayLikeToArray(o, minLen);
          var n = Object.prototype.toString.call(o).slice(8, -1);
          if (n === "Object" && o.constructor)
            n = o.constructor.name;
          if (n === "Map" || n === "Set")
            return Array.from(o);
          if (n === "Arguments" || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n))
            return _arrayLikeToArray(o, minLen);
        }
        function _nonIterableRest() {
          throw new TypeError("Invalid attempt to destructure non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
        }
        function _slicedToArray(arr, i) {
          return _arrayWithHoles(arr) || _iterableToArrayLimit(arr, i) || _unsupportedIterableToArray(arr, i) || _nonIterableRest();
        }
        __webpack_require__("6762");
        __webpack_require__("2fdb");
        function _arrayWithoutHoles2(arr) {
          if (Array.isArray(arr))
            return _arrayLikeToArray(arr);
        }
        function _iterableToArray2(iter) {
          if (typeof Symbol !== "undefined" && Symbol.iterator in Object(iter))
            return Array.from(iter);
        }
        function _nonIterableSpread2() {
          throw new TypeError("Invalid attempt to spread non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
        }
        function _toConsumableArray2(arr) {
          return _arrayWithoutHoles2(arr) || _iterableToArray2(arr) || _unsupportedIterableToArray(arr) || _nonIterableSpread2();
        }
        var external_commonjs_sortablejs_commonjs2_sortablejs_amd_sortablejs_root_Sortable_ = __webpack_require__("a352");
        var external_commonjs_sortablejs_commonjs2_sortablejs_amd_sortablejs_root_Sortable_default = /* @__PURE__ */ __webpack_require__.n(external_commonjs_sortablejs_commonjs2_sortablejs_amd_sortablejs_root_Sortable_);
        var helper = __webpack_require__("c649");
        function buildAttribute(object, propName, value) {
          if (value === void 0) {
            return object;
          }
          object = object || {};
          object[propName] = value;
          return object;
        }
        function computeVmIndex(vnodes, element) {
          return vnodes.map(function(elt) {
            return elt.elm;
          }).indexOf(element);
        }
        function _computeIndexes(slots, children, isTransition, footerOffset) {
          if (!slots) {
            return [];
          }
          var elmFromNodes = slots.map(function(elt) {
            return elt.elm;
          });
          var footerIndex = children.length - footerOffset;
          var rawIndexes = _toConsumableArray2(children).map(function(elt, idx) {
            return idx >= footerIndex ? elmFromNodes.length : elmFromNodes.indexOf(elt);
          });
          return isTransition ? rawIndexes.filter(function(ind) {
            return ind !== -1;
          }) : rawIndexes;
        }
        function emit(evtName, evtData) {
          var _this = this;
          this.$nextTick(function() {
            return _this.$emit(evtName.toLowerCase(), evtData);
          });
        }
        function delegateAndEmit(evtName) {
          var _this2 = this;
          return function(evtData) {
            if (_this2.realList !== null) {
              _this2["onDrag" + evtName](evtData);
            }
            emit.call(_this2, evtName, evtData);
          };
        }
        function isTransitionName(name2) {
          return ["transition-group", "TransitionGroup"].includes(name2);
        }
        function vuedraggable_isTransition(slots) {
          if (!slots || slots.length !== 1) {
            return false;
          }
          var _slots = _slicedToArray(slots, 1), componentOptions = _slots[0].componentOptions;
          if (!componentOptions) {
            return false;
          }
          return isTransitionName(componentOptions.tag);
        }
        function getSlot(slot, scopedSlot, key) {
          return slot[key] || (scopedSlot[key] ? scopedSlot[key]() : void 0);
        }
        function computeChildrenAndOffsets(children, slot, scopedSlot) {
          var headerOffset = 0;
          var footerOffset = 0;
          var header = getSlot(slot, scopedSlot, "header");
          if (header) {
            headerOffset = header.length;
            children = children ? [].concat(_toConsumableArray2(header), _toConsumableArray2(children)) : _toConsumableArray2(header);
          }
          var footer = getSlot(slot, scopedSlot, "footer");
          if (footer) {
            footerOffset = footer.length;
            children = children ? [].concat(_toConsumableArray2(children), _toConsumableArray2(footer)) : _toConsumableArray2(footer);
          }
          return {
            children,
            headerOffset,
            footerOffset
          };
        }
        function getComponentAttributes($attrs, componentData) {
          var attributes = null;
          var update = function update2(name2, value) {
            attributes = buildAttribute(attributes, name2, value);
          };
          var attrs = Object.keys($attrs).filter(function(key) {
            return key === "id" || key.startsWith("data-");
          }).reduce(function(res, key) {
            res[key] = $attrs[key];
            return res;
          }, {});
          update("attrs", attrs);
          if (!componentData) {
            return attributes;
          }
          var on2 = componentData.on, props2 = componentData.props, componentDataAttrs = componentData.attrs;
          update("on", on2);
          update("props", props2);
          Object.assign(attributes.attrs, componentDataAttrs);
          return attributes;
        }
        var eventsListened = ["Start", "Add", "Remove", "Update", "End"];
        var eventsToEmit = ["Choose", "Unchoose", "Sort", "Filter", "Clone"];
        var readonlyProperties = ["Move"].concat(eventsListened, eventsToEmit).map(function(evt) {
          return "on" + evt;
        });
        var draggingElement = null;
        var props = {
          options: Object,
          list: {
            type: Array,
            required: false,
            default: null
          },
          value: {
            type: Array,
            required: false,
            default: null
          },
          noTransitionOnDrag: {
            type: Boolean,
            default: false
          },
          clone: {
            type: Function,
            default: function _default(original) {
              return original;
            }
          },
          element: {
            type: String,
            default: "div"
          },
          tag: {
            type: String,
            default: null
          },
          move: {
            type: Function,
            default: null
          },
          componentData: {
            type: Object,
            required: false,
            default: null
          }
        };
        var draggableComponent = {
          name: "draggable",
          inheritAttrs: false,
          props,
          data: function data() {
            return {
              transitionMode: false,
              noneFunctionalComponentMode: false
            };
          },
          render: function render(h) {
            var slots = this.$slots.default;
            this.transitionMode = vuedraggable_isTransition(slots);
            var _computeChildrenAndOf = computeChildrenAndOffsets(slots, this.$slots, this.$scopedSlots), children = _computeChildrenAndOf.children, headerOffset = _computeChildrenAndOf.headerOffset, footerOffset = _computeChildrenAndOf.footerOffset;
            this.headerOffset = headerOffset;
            this.footerOffset = footerOffset;
            var attributes = getComponentAttributes(this.$attrs, this.componentData);
            return h(this.getTag(), attributes, children);
          },
          created: function created() {
            if (this.list !== null && this.value !== null) {
              helper["b"].error("Value and list props are mutually exclusive! Please set one or another.");
            }
            if (this.element !== "div") {
              helper["b"].warn("Element props is deprecated please use tag props instead. See https://github.com/SortableJS/Vue.Draggable/blob/master/documentation/migrate.md#element-props");
            }
            if (this.options !== void 0) {
              helper["b"].warn("Options props is deprecated, add sortable options directly as vue.draggable item, or use v-bind. See https://github.com/SortableJS/Vue.Draggable/blob/master/documentation/migrate.md#options-props");
            }
          },
          mounted: function mounted() {
            var _this3 = this;
            this.noneFunctionalComponentMode = this.getTag().toLowerCase() !== this.$el.nodeName.toLowerCase() && !this.getIsFunctional();
            if (this.noneFunctionalComponentMode && this.transitionMode) {
              throw new Error("Transition-group inside component is not supported. Please alter tag value or remove transition-group. Current tag value: ".concat(this.getTag()));
            }
            var optionsAdded = {};
            eventsListened.forEach(function(elt) {
              optionsAdded["on" + elt] = delegateAndEmit.call(_this3, elt);
            });
            eventsToEmit.forEach(function(elt) {
              optionsAdded["on" + elt] = emit.bind(_this3, elt);
            });
            var attributes = Object.keys(this.$attrs).reduce(function(res, key) {
              res[Object(helper["a"])(key)] = _this3.$attrs[key];
              return res;
            }, {});
            var options = Object.assign({}, this.options, attributes, optionsAdded, {
              onMove: function onMove(evt, originalEvent) {
                return _this3.onDragMove(evt, originalEvent);
              }
            });
            !("draggable" in options) && (options.draggable = ">*");
            this._sortable = new external_commonjs_sortablejs_commonjs2_sortablejs_amd_sortablejs_root_Sortable_default.a(this.rootContainer, options);
            this.computeIndexes();
          },
          beforeDestroy: function beforeDestroy() {
            if (this._sortable !== void 0)
              this._sortable.destroy();
          },
          computed: {
            rootContainer: function rootContainer() {
              return this.transitionMode ? this.$el.children[0] : this.$el;
            },
            realList: function realList() {
              return this.list ? this.list : this.value;
            }
          },
          watch: {
            options: {
              handler: function handler(newOptionValue) {
                this.updateOptions(newOptionValue);
              },
              deep: true
            },
            $attrs: {
              handler: function handler(newOptionValue) {
                this.updateOptions(newOptionValue);
              },
              deep: true
            },
            realList: function realList() {
              this.computeIndexes();
            }
          },
          methods: {
            getIsFunctional: function getIsFunctional() {
              var fnOptions = this._vnode.fnOptions;
              return fnOptions && fnOptions.functional;
            },
            getTag: function getTag() {
              return this.tag || this.element;
            },
            updateOptions: function updateOptions(newOptionValue) {
              for (var property in newOptionValue) {
                var value = Object(helper["a"])(property);
                if (readonlyProperties.indexOf(value) === -1) {
                  this._sortable.option(value, newOptionValue[property]);
                }
              }
            },
            getChildrenNodes: function getChildrenNodes() {
              if (this.noneFunctionalComponentMode) {
                return this.$children[0].$slots.default;
              }
              var rawNodes = this.$slots.default;
              return this.transitionMode ? rawNodes[0].child.$slots.default : rawNodes;
            },
            computeIndexes: function computeIndexes() {
              var _this4 = this;
              this.$nextTick(function() {
                _this4.visibleIndexes = _computeIndexes(_this4.getChildrenNodes(), _this4.rootContainer.children, _this4.transitionMode, _this4.footerOffset);
              });
            },
            getUnderlyingVm: function getUnderlyingVm(htmlElt) {
              var index2 = computeVmIndex(this.getChildrenNodes() || [], htmlElt);
              if (index2 === -1) {
                return null;
              }
              var element = this.realList[index2];
              return {
                index: index2,
                element
              };
            },
            getUnderlyingPotencialDraggableComponent: function getUnderlyingPotencialDraggableComponent(_ref) {
              var vue = _ref.__vue__;
              if (!vue || !vue.$options || !isTransitionName(vue.$options._componentTag)) {
                if (!("realList" in vue) && vue.$children.length === 1 && "realList" in vue.$children[0])
                  return vue.$children[0];
                return vue;
              }
              return vue.$parent;
            },
            emitChanges: function emitChanges(evt) {
              var _this5 = this;
              this.$nextTick(function() {
                _this5.$emit("change", evt);
              });
            },
            alterList: function alterList(onList) {
              if (this.list) {
                onList(this.list);
                return;
              }
              var newList = _toConsumableArray2(this.value);
              onList(newList);
              this.$emit("input", newList);
            },
            spliceList: function spliceList() {
              var _arguments = arguments;
              var spliceList2 = function spliceList3(list) {
                return list.splice.apply(list, _toConsumableArray2(_arguments));
              };
              this.alterList(spliceList2);
            },
            updatePosition: function updatePosition(oldIndex2, newIndex2) {
              var updatePosition2 = function updatePosition3(list) {
                return list.splice(newIndex2, 0, list.splice(oldIndex2, 1)[0]);
              };
              this.alterList(updatePosition2);
            },
            getRelatedContextFromMoveEvent: function getRelatedContextFromMoveEvent(_ref2) {
              var to = _ref2.to, related = _ref2.related;
              var component2 = this.getUnderlyingPotencialDraggableComponent(to);
              if (!component2) {
                return {
                  component: component2
                };
              }
              var list = component2.realList;
              var context = {
                list,
                component: component2
              };
              if (to !== related && list && component2.getUnderlyingVm) {
                var destination = component2.getUnderlyingVm(related);
                if (destination) {
                  return Object.assign(destination, context);
                }
              }
              return context;
            },
            getVmIndex: function getVmIndex(domIndex) {
              var indexes = this.visibleIndexes;
              var numberIndexes = indexes.length;
              return domIndex > numberIndexes - 1 ? numberIndexes : indexes[domIndex];
            },
            getComponent: function getComponent() {
              return this.$slots.default[0].componentInstance;
            },
            resetTransitionData: function resetTransitionData(index2) {
              if (!this.noTransitionOnDrag || !this.transitionMode) {
                return;
              }
              var nodes = this.getChildrenNodes();
              nodes[index2].data = null;
              var transitionContainer = this.getComponent();
              transitionContainer.children = [];
              transitionContainer.kept = void 0;
            },
            onDragStart: function onDragStart(evt) {
              this.context = this.getUnderlyingVm(evt.item);
              evt.item._underlying_vm_ = this.clone(this.context.element);
              draggingElement = evt.item;
            },
            onDragAdd: function onDragAdd(evt) {
              var element = evt.item._underlying_vm_;
              if (element === void 0) {
                return;
              }
              Object(helper["d"])(evt.item);
              var newIndex2 = this.getVmIndex(evt.newIndex);
              this.spliceList(newIndex2, 0, element);
              this.computeIndexes();
              var added = {
                element,
                newIndex: newIndex2
              };
              this.emitChanges({
                added
              });
            },
            onDragRemove: function onDragRemove(evt) {
              Object(helper["c"])(this.rootContainer, evt.item, evt.oldIndex);
              if (evt.pullMode === "clone") {
                Object(helper["d"])(evt.clone);
                return;
              }
              var oldIndex2 = this.context.index;
              this.spliceList(oldIndex2, 1);
              var removed = {
                element: this.context.element,
                oldIndex: oldIndex2
              };
              this.resetTransitionData(oldIndex2);
              this.emitChanges({
                removed
              });
            },
            onDragUpdate: function onDragUpdate(evt) {
              Object(helper["d"])(evt.item);
              Object(helper["c"])(evt.from, evt.item, evt.oldIndex);
              var oldIndex2 = this.context.index;
              var newIndex2 = this.getVmIndex(evt.newIndex);
              this.updatePosition(oldIndex2, newIndex2);
              var moved2 = {
                element: this.context.element,
                oldIndex: oldIndex2,
                newIndex: newIndex2
              };
              this.emitChanges({
                moved: moved2
              });
            },
            updateProperty: function updateProperty(evt, propertyName) {
              evt.hasOwnProperty(propertyName) && (evt[propertyName] += this.headerOffset);
            },
            computeFutureIndex: function computeFutureIndex(relatedContext, evt) {
              if (!relatedContext.element) {
                return 0;
              }
              var domChildren = _toConsumableArray2(evt.to.children).filter(function(el) {
                return el.style["display"] !== "none";
              });
              var currentDOMIndex = domChildren.indexOf(evt.related);
              var currentIndex = relatedContext.component.getVmIndex(currentDOMIndex);
              var draggedInList = domChildren.indexOf(draggingElement) !== -1;
              return draggedInList || !evt.willInsertAfter ? currentIndex : currentIndex + 1;
            },
            onDragMove: function onDragMove(evt, originalEvent) {
              var onMove = this.move;
              if (!onMove || !this.realList) {
                return true;
              }
              var relatedContext = this.getRelatedContextFromMoveEvent(evt);
              var draggedContext = this.context;
              var futureIndex = this.computeFutureIndex(relatedContext, evt);
              Object.assign(draggedContext, {
                futureIndex
              });
              var sendEvt = Object.assign({}, evt, {
                relatedContext,
                draggedContext
              });
              return onMove(sendEvt, originalEvent);
            },
            onDragEnd: function onDragEnd() {
              this.computeIndexes();
              draggingElement = null;
            }
          }
        };
        if (typeof window !== "undefined" && "Vue" in window) {
          window.Vue.component("draggable", draggableComponent);
        }
        var vuedraggable = draggableComponent;
        __webpack_exports__["default"] = vuedraggable;
      }
    })["default"];
  });
})(vuedraggable_umd);
var draggable = /* @__PURE__ */ getDefaultExportFromCjs(vuedraggable_umd.exports);
var index_vue_vue_type_style_index_0_scoped_true_lang$2 = "";
const _sfc_main$O = {
  props: {
    nodeProps: {
      children: "children",
      label: "label",
      isLeaf: "isLeaf"
    },
    role: {
      type: Object
    },
    selection: {
      type: Object
    }
  },
  emits: ["success"],
  components: { draggable },
  setup(props, { emit }) {
    tpm;
    tpm.api.admin.account;
    const { getNodeTree, getOrgLevel } = tpm.api.admin.role;
    const dragTemp = ref({});
    const dialogRef = ref({});
    const loading = ref(false);
    const checked1 = ref(false);
    const checked2 = ref(false);
    const checked3 = ref(false);
    const checked4 = ref(false);
    const checked5 = ref(false);
    const checked6 = ref(false);
    const checkedLable1 = ref("\u5168\u9009");
    const checkedLable2 = ref("\u5168\u9009");
    const checkedLable3 = ref("\u5168\u9009");
    const checkedLable4 = ref("\u5168\u9009");
    const checkedLable5 = ref("\u5168\u9009");
    const checkedLable6 = ref("\u5168\u9009");
    const treeRef1 = ref();
    const treeRef2 = ref();
    const treeRef3 = ref();
    const treeRef4 = ref();
    const treeRef5 = ref();
    const treeRef6 = ref();
    const loading1 = ref(false);
    const loading2 = ref(false);
    const loading3 = ref(false);
    const loading4 = ref(false);
    const loading5 = ref(false);
    const loading6 = ref(false);
    const model = reactive({
      headOffice: [],
      headOfficeSelections: [],
      dbs: [],
      dbsSelections: [],
      marketingCenters: [],
      marketingCentersSelections: [],
      saleRegions: [],
      saleRegionsSelections: [],
      departments: [],
      departmentsSelections: [],
      stations: [],
      stationsSelections: []
    });
    const keys1 = computed(() => {
      return model.headOfficeSelections.filter((s) => s.id);
    });
    const keys2 = computed(() => {
      return model.dbsSelections.filter((s) => s.id);
    });
    const keys3 = computed(() => {
      return model.marketingCentersSelections.filter((s) => s.id);
    });
    const keys4 = computed(() => {
      return model.saleRegionsSelections.filter((s) => s.id);
    });
    const keys5 = computed(() => {
      return model.departmentsSelections.filter((s) => s.id);
    });
    const keys6 = computed(() => {
      return model.stationsSelections.filter((s) => s.id);
    });
    const tagSelected = (db, selections, ref2) => {
      if (Array.isArray(db)) {
        db.forEach((item) => {
          nextTick(() => {
            let exist = selections.find((m) => m.id === item.id);
            if (exist != null) {
              ref2.value.setChecked(item.id, true);
            }
          });
        });
      }
    };
    const concatArry = (arr, arr2) => {
      let temparr = arr;
      if (Array.isArray(arr)) {
        if (arr.length > 0) {
          arr.forEach((item) => {
            let cur = arr2.find((m) => m.id.toLowerCase() === item.id.toLowerCase());
            if (cur != null) {
              temparr.push(cur);
            }
          });
        } else {
          temparr = arr2;
        }
      }
      return temparr.length == 0 ? arr2 : temparr;
    };
    const headOfficeChange = async (data) => {
      var _a;
      loading2.value = true;
      const mOrgs = (_a = props.role) == null ? void 0 : _a.extends;
      if (data != null) {
        const res = await getOrgLevel({
          level: 2,
          ignore: false,
          parentId: data == null ? "" : data.key == null ? data.id : data.key,
          includes: []
        });
        model.dbs = concatArry(res, mOrgs.dbs);
        tagSelected(model.dbs, model.dbsSelections, treeRef2);
      }
      loading2.value = false;
      const nextNodefirst = model.dbs[0];
      if (nextNodefirst != null) {
        await dbsChange(nextNodefirst);
      } else {
        dbsChange([]);
      }
    };
    const dbsChange = async (data) => {
      var _a;
      loading3.value = true;
      const mOrgs = (_a = props.role) == null ? void 0 : _a.extends;
      if (data != null) {
        const res = await getOrgLevel({
          level: 3,
          ignore: false,
          parentId: data == null ? "" : data.key == null ? data.id : data.key,
          includes: []
        });
        model.marketingCenters = concatArry(res, mOrgs.marketingCenters);
        tagSelected(model.marketingCenters, model.marketingCentersSelections, treeRef3);
      }
      loading3.value = false;
      const nextNodefirst = model.marketingCenters[0];
      if (nextNodefirst != null) {
        marketingCentersChange(nextNodefirst);
      } else {
        marketingCentersChange([]);
      }
    };
    const marketingCentersChange = async (data) => {
      var _a;
      loading4.value = true;
      const mOrgs = (_a = props.role) == null ? void 0 : _a.extends;
      if (data != null) {
        const res = await getOrgLevel({
          level: 4,
          ignore: false,
          parentId: data == null ? "" : data.key == null ? data.id : data.key,
          includes: []
        });
        model.saleRegions = concatArry(res, mOrgs.saleRegions);
        tagSelected(model.saleRegions, model.saleRegionsSelections, treeRef4);
      }
      loading4.value = false;
      const nextNodefirst = model.saleRegions[0];
      if (nextNodefirst != null) {
        saleRegionsChange(nextNodefirst);
      } else {
        saleRegionsChange([]);
      }
    };
    const saleRegionsChange = async (data) => {
      var _a;
      loading5.value = true;
      const mOrgs = (_a = props.role) == null ? void 0 : _a.extends;
      if (data != null) {
        const res = await getOrgLevel({
          level: 5,
          ignore: false,
          parentId: data == null ? "" : data.key == null ? data.id : data.key,
          includes: []
        });
        model.departments = concatArry(res, mOrgs.departments);
        tagSelected(model.departments, model.departmentsSelections, treeRef5);
      }
      loading5.value = false;
      const nextNodefirst = model.departments[0];
      if (nextNodefirst != null) {
        departmentsChange(nextNodefirst);
      } else {
        departmentsChange([]);
      }
    };
    const departmentsChange = async (data) => {
      var _a;
      loading6.value = true;
      const mOrgs = (_a = props.role) == null ? void 0 : _a.extends;
      if (data != null) {
        const res = await getOrgLevel({
          level: 6,
          ignore: false,
          parentId: data == null ? "" : data.key == null ? data.id : data.key,
          includes: []
        });
        model.stations = concatArry(res, mOrgs.stations);
        tagSelected(model.stations, model.stationsSelections, treeRef6);
      }
      loading6.value = false;
      stationsChange();
    };
    const stationsChange = (data) => {
      var _a;
      if (props.selection !== null) {
        const mOrgs = (_a = props.role) == null ? void 0 : _a.extends;
        if (mOrgs.headOffice.length >= 0) {
          tagSelected(model.headOffice, model.headOfficeSelections, treeRef1);
        }
        if (mOrgs.dbs.length >= 0) {
          tagSelected(model.dbs, model.dbsSelections, treeRef2);
        }
        if (mOrgs.marketingCenters.length >= 0) {
          tagSelected(model.marketingCenters, model.marketingCentersSelections, treeRef3);
        }
        if (mOrgs.saleRegions.length >= 0) {
          tagSelected(model.saleRegions, model.saleRegionsSelections, treeRef4);
        }
        if (mOrgs.departments.length >= 0) {
          tagSelected(model.departments, model.departmentsSelections, treeRef5);
        }
        if (mOrgs.stations.length >= 0) {
          tagSelected(model.stations, model.stationsSelections, treeRef6);
        }
      }
    };
    const pushOrpop = (cols, db, data, checked, fun) => {
      dragTemp.value = {};
      if (checked.checkedKeys.length > 0) {
        checked.checkedKeys.forEach((key) => {
          let curt = db.find((m) => m.id === key);
          if (curt !== null) {
            const exist = cols.find((m) => m.id === key);
            if (!exist)
              cols.push(curt);
          }
        });
        if (cols.length == 0) {
          const exist = cols.find((m) => m.id === data.id);
          if (!exist)
            cols.push(data);
        }
      } else {
        cols = cols.filter((item) => {
          return item.id !== data.id;
        });
      }
      fun(cols);
    };
    const headOfficeClick = (data, checked) => {
      let cols = model.headOfficeSelections;
      pushOrpop(cols, model.headOffice, data, checked, (ref2) => {
        model.headOfficeSelections = ref2;
      });
    };
    const dbsClick = (data, checked) => {
      let cols = model.dbsSelections;
      pushOrpop(cols, model.dbs, data, checked, (ref2) => {
        model.dbsSelections = ref2;
      });
    };
    const marketingCentersClick = (data, checked) => {
      let cols = model.marketingCentersSelections;
      pushOrpop(cols, model.marketingCenters, data, checked, (ref2) => {
        model.marketingCentersSelections = ref2;
      });
    };
    const saleRegionsClick = (data, checked) => {
      let cols = model.saleRegionsSelections;
      pushOrpop(cols, model.saleRegions, data, checked, (ref2) => {
        model.saleRegionsSelections = ref2;
      });
    };
    const departmentsClick = (data, checked) => {
      let cols = model.departmentsSelections;
      pushOrpop(cols, model.departments, data, checked, (ref2) => {
        model.departmentsSelections = ref2;
      });
    };
    const stationsClick = (data, checked) => {
      let cols = model.stationsSelections;
      pushOrpop(cols, model.stations, data, checked, (ref2) => {
        model.stationsSelections = ref2;
      });
    };
    const checkAll = (value, index2) => {
      let c = "\u53D6\u6D88";
      let a = "\u5168\u9009";
      let treeRef = treeRef1;
      let selections = [];
      switch (index2) {
        case 1:
          treeRef = treeRef1;
          selections = model.headOffice;
          checkedLable1.value = value ? c : a;
          break;
        case 2:
          treeRef = treeRef2;
          selections = model.dbs;
          checkedLable2.value = value ? c : a;
          break;
        case 3:
          treeRef = treeRef3;
          selections = model.marketingCenters;
          checkedLable3.value = value ? c : a;
          break;
        case 4:
          treeRef = treeRef4;
          selections = model.saleRegions;
          checkedLable4.value = value ? c : a;
          break;
        case 5:
          treeRef = treeRef5;
          selections = model.departments;
          checkedLable5.value = value ? c : a;
          break;
        case 6:
          treeRef = treeRef6;
          selections = model.stations;
          checkedLable6.value = value ? c : a;
          break;
      }
      if (selections.length > 0) {
        selections.forEach((item) => {
          treeRef.value.setChecked(item.id, value);
          switch (index2) {
            case 1:
              {
                let cols = model.headOfficeSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
            case 2:
              {
                let cols = model.dbsSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
            case 3:
              {
                let cols = model.marketingCentersSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
            case 4:
              {
                let cols = model.saleRegionsSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
            case 5:
              {
                let cols = model.departmentsSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
            case 6:
              {
                let cols = model.stationsSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
          }
        });
      }
      let getNodes = treeRef.value.getCheckedNodes();
      if (getNodes.length > 0) {
        getNodes.forEach((item) => {
          switch (index2) {
            case 1:
              {
                let cols = model.headOfficeSelections;
                cols.push(item);
              }
              break;
            case 2:
              {
                let cols = model.dbsSelections;
                cols.push(item);
              }
              break;
            case 3:
              {
                let cols = model.marketingCentersSelections;
                cols.push(item);
              }
              break;
            case 4:
              {
                let cols = model.saleRegionsSelections;
                cols.push(item);
              }
              break;
            case 5:
              {
                let cols = model.departmentsSelections;
                cols.push(item);
              }
              break;
            case 6:
              {
                let cols = model.stationsSelections;
                cols.push(item);
              }
              break;
          }
        });
      }
    };
    const handleClose = (tag, cols, index2) => {
      cols.splice(cols.indexOf(tag), 1);
      let treeRef = treeRef1;
      switch (index2) {
        case 1:
          treeRef = treeRef1;
          break;
        case 2:
          treeRef = treeRef2;
          break;
        case 3:
          treeRef = treeRef3;
          break;
        case 4:
          treeRef = treeRef4;
          break;
        case 5:
          treeRef = treeRef5;
          break;
        case 6:
          treeRef = treeRef6;
          break;
      }
      let getNode = treeRef.value.getCheckedNodes();
      if (getNode.length > 0) {
        getNode.forEach((item) => {
          if (tag.id == item.id) {
            treeRef.value.setChecked(item.id, false);
          }
        });
      }
    };
    const handleDragStart = (node) => {
      dragTemp.value = node.data;
    };
    const allowDrop = (ev) => {
      ev.preventDefault();
    };
    const drop3 = (ev, ditem) => {
      ev.preventDefault();
      ev.target;
      let item = dragTemp.value;
      if (item.type !== null) {
        switch (item.type) {
          case 10:
            {
              let cols = model.headOfficeSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.headOffice, cols, treeRef1);
              }
            }
            break;
          case 20:
            {
              let cols = model.dbsSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.dbs, cols, treeRef2);
              }
            }
            break;
          case 30:
            {
              let cols = model.marketingCentersSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.marketingCenters, cols, treeRef3);
              }
            }
            break;
          case 40:
            {
              let cols = model.saleRegionsSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.saleRegions, cols, treeRef4);
              }
            }
            break;
          case 50:
            {
              let cols = model.departmentsSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.departments, cols, treeRef5);
              }
            }
            break;
          case 60:
            {
              let cols = model.stationsSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.stations, cols, treeRef6);
              }
            }
            break;
        }
      }
    };
    const collapse = (moveNode, inNode, type) => {
      return false;
    };
    const handleOpened = async () => {
      var _a;
      loading1.value = true;
      const res = await getNodeTree(1);
      model.headOffice = res;
      loading1.value = false;
      const curRole = props.role;
      const mOrgs = (_a = props.role) == null ? void 0 : _a.extends;
      console.log("curRole", curRole);
      console.log("mOrgs", mOrgs);
      model.headOfficeSelections = mOrgs.headOffice;
      model.dbsSelections = mOrgs.dbs;
      model.marketingCentersSelections = mOrgs.marketingCenters;
      model.saleRegionsSelections = mOrgs.saleRegions;
      model.departmentsSelections = mOrgs.departments;
      model.stationsSelections = mOrgs.stations;
      const nextNodefirst = model.headOffice[0];
      if (nextNodefirst != null) {
        await headOfficeChange(nextNodefirst);
      } else {
        await headOfficeChange([]);
      }
    };
    handleOpened();
    return {
      ...toRefs(model),
      dialogRef,
      model,
      handleOpened,
      checkAll,
      pushOrpop,
      tagSelected,
      dragTemp,
      collapse,
      handleDragStart,
      allowDrop,
      drop: drop3,
      concatArry,
      keys1,
      keys2,
      keys3,
      keys4,
      keys5,
      keys6,
      loading,
      loading1,
      loading2,
      loading3,
      loading4,
      loading5,
      loading6,
      treeRef1,
      treeRef2,
      treeRef3,
      treeRef4,
      treeRef5,
      treeRef6,
      headOfficeChange,
      dbsChange,
      marketingCentersChange,
      saleRegionsChange,
      departmentsChange,
      stationsChange,
      headOfficeClick,
      dbsClick,
      marketingCentersClick,
      saleRegionsClick,
      departmentsClick,
      stationsClick,
      checked1,
      checked2,
      checked3,
      checked4,
      checked5,
      checked6,
      checkedLable1,
      checkedLable2,
      checkedLable3,
      checkedLable4,
      checkedLable5,
      checkedLable6,
      handleClose
    };
  }
};
const _withScopeId$1 = (n) => (pushScopeId("data-v-4e1394e0"), n = n(), popScopeId(), n);
const _hoisted_1$g = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_2$b = /* @__PURE__ */ _withScopeId$1(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_3$6 = [
  _hoisted_2$b
];
const _hoisted_4$3 = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_5$2 = /* @__PURE__ */ _withScopeId$1(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_6$2 = [
  _hoisted_5$2
];
const _hoisted_7$2 = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_8$2 = /* @__PURE__ */ _withScopeId$1(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_9$1 = [
  _hoisted_8$2
];
const _hoisted_10$1 = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_11$1 = /* @__PURE__ */ _withScopeId$1(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_12$1 = [
  _hoisted_11$1
];
const _hoisted_13$1 = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_14$1 = /* @__PURE__ */ _withScopeId$1(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_15$1 = [
  _hoisted_14$1
];
const _hoisted_16$1 = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_17$1 = /* @__PURE__ */ _withScopeId$1(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_18$1 = [
  _hoisted_17$1
];
function _sfc_render$O(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_checkbox = resolveComponent("el-checkbox");
  const _component_m_flex_auto = resolveComponent("m-flex-auto");
  const _component_m_flex_fixed = resolveComponent("m-flex-fixed");
  const _component_m_flex_row = resolveComponent("m-flex-row");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_el_tree = resolveComponent("el-tree");
  const _component_el_tag = resolveComponent("el-tag");
  const _directive_loading = resolveDirective("loading");
  return openBlock(), createElementBlock(Fragment, null, [
    createVNode(_component_el_row, { class: "m-roleorg-row" }, {
      default: withCtx(() => [
        createVNode(_component_el_col, { span: 4 }, {
          default: withCtx(() => [
            createVNode(_component_m_flex_row, null, {
              default: withCtx(() => [
                createVNode(_component_m_flex_auto, null, {
                  default: withCtx(() => [
                    createVNode(_component_el_checkbox, {
                      modelValue: $setup.checked1,
                      "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.checked1 = $event),
                      style: { color: "#fff" },
                      onChange: _cache[1] || (_cache[1] = ($event) => $setup.checkAll($setup.checked1, 1))
                    }, {
                      default: withCtx(() => [
                        createTextVNode(toDisplayString($setup.checkedLable1), 1)
                      ]),
                      _: 1
                    }, 8, ["modelValue"])
                  ]),
                  _: 1
                }),
                createVNode(_component_m_flex_fixed, null, {
                  default: withCtx(() => [
                    createTextVNode(" \u96EA\u82B1 ")
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_col, { span: 4 }, {
          default: withCtx(() => [
            createVNode(_component_m_flex_row, null, {
              default: withCtx(() => [
                createVNode(_component_m_flex_auto, null, {
                  default: withCtx(() => [
                    createVNode(_component_el_checkbox, {
                      ref: "checkedRef2",
                      modelValue: $setup.checked2,
                      "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.checked2 = $event),
                      style: { color: "#fff" },
                      onChange: _cache[3] || (_cache[3] = ($event) => $setup.checkAll($setup.checked2, 2))
                    }, {
                      default: withCtx(() => [
                        createTextVNode(toDisplayString($setup.checkedLable2), 1)
                      ]),
                      _: 1
                    }, 8, ["modelValue"])
                  ]),
                  _: 1
                }),
                createVNode(_component_m_flex_fixed, null, {
                  default: withCtx(() => [
                    createTextVNode(" \u4E8B\u4E1A\u90E8 ")
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_col, { span: 4 }, {
          default: withCtx(() => [
            createVNode(_component_m_flex_row, null, {
              default: withCtx(() => [
                createVNode(_component_m_flex_auto, null, {
                  default: withCtx(() => [
                    createVNode(_component_el_checkbox, {
                      ref: "checkedRef3",
                      modelValue: $setup.checked3,
                      "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.checked3 = $event),
                      style: { color: "#fff" },
                      onChange: _cache[5] || (_cache[5] = ($event) => $setup.checkAll($setup.checked3, 3))
                    }, {
                      default: withCtx(() => [
                        createTextVNode(toDisplayString($setup.checkedLable3), 1)
                      ]),
                      _: 1
                    }, 8, ["modelValue"])
                  ]),
                  _: 1
                }),
                createVNode(_component_m_flex_fixed, null, {
                  default: withCtx(() => [
                    createTextVNode(" \u8425\u9500\u4E2D\u5FC3 ")
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_col, { span: 4 }, {
          default: withCtx(() => [
            createVNode(_component_m_flex_row, null, {
              default: withCtx(() => [
                createVNode(_component_m_flex_auto, null, {
                  default: withCtx(() => [
                    createVNode(_component_el_checkbox, {
                      ref: "checkedRef3",
                      modelValue: $setup.checked4,
                      "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => $setup.checked4 = $event),
                      style: { color: "#fff" },
                      onChange: _cache[7] || (_cache[7] = ($event) => $setup.checkAll($setup.checked4, 4))
                    }, {
                      default: withCtx(() => [
                        createTextVNode(toDisplayString($setup.checkedLable4), 1)
                      ]),
                      _: 1
                    }, 8, ["modelValue"])
                  ]),
                  _: 1
                }),
                createVNode(_component_m_flex_fixed, null, {
                  default: withCtx(() => [
                    createTextVNode(" \u5927\u533A ")
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_col, { span: 4 }, {
          default: withCtx(() => [
            createVNode(_component_m_flex_row, null, {
              default: withCtx(() => [
                createVNode(_component_m_flex_auto, null, {
                  default: withCtx(() => [
                    createVNode(_component_el_checkbox, {
                      ref: "checkedRef4",
                      modelValue: $setup.checked5,
                      "onUpdate:modelValue": _cache[8] || (_cache[8] = ($event) => $setup.checked5 = $event),
                      style: { color: "#fff" },
                      onChange: _cache[9] || (_cache[9] = ($event) => $setup.checkAll($setup.checked5, 5))
                    }, {
                      default: withCtx(() => [
                        createTextVNode(toDisplayString($setup.checkedLable5), 1)
                      ]),
                      _: 1
                    }, 8, ["modelValue"])
                  ]),
                  _: 1
                }),
                createVNode(_component_m_flex_fixed, null, {
                  default: withCtx(() => [
                    createTextVNode(" \u4E1A\u52A1\u90E8 ")
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_col, { span: 4 }, {
          default: withCtx(() => [
            createVNode(_component_m_flex_row, null, {
              default: withCtx(() => [
                createVNode(_component_m_flex_auto, null, {
                  default: withCtx(() => [
                    createVNode(_component_el_checkbox, {
                      ref: "checkedRef5",
                      modelValue: $setup.checked6,
                      "onUpdate:modelValue": _cache[10] || (_cache[10] = ($event) => $setup.checked6 = $event),
                      style: { color: "#fff" },
                      onChange: _cache[11] || (_cache[11] = ($event) => $setup.checkAll($setup.checked6, 6))
                    }, {
                      default: withCtx(() => [
                        createTextVNode(toDisplayString($setup.checkedLable6), 1)
                      ]),
                      _: 1
                    }, 8, ["modelValue"])
                  ]),
                  _: 1
                }),
                createVNode(_component_m_flex_fixed, null, {
                  default: withCtx(() => [
                    createTextVNode(" \u5DE5\u4F5C\u7AD9 ")
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        })
      ]),
      _: 1
    }),
    createVNode(_component_el_row, { class: "m-roleorg-tree" }, {
      default: withCtx(() => [
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border"
        }, {
          default: withCtx(() => [
            withDirectives((openBlock(), createBlock(_component_el_tree, {
              ref: "treeRef1",
              "current-node-key": `00000000-0000-0000-0000-000000000000`,
              props: $props.nodeProps,
              data: _ctx.headOffice,
              "node-key": "id",
              "highlight-current": "",
              onCurrentChange: $setup.headOfficeChange,
              "expand-on-click-node": false,
              "check-strictly": true,
              "show-checkbox": "",
              onCheck: $setup.headOfficeClick,
              style: { height: "300px", overflow: "auto" },
              draggable: true,
              onNodeDragStart: $setup.handleDragStart,
              "allow-drop": $setup.collapse
            }, {
              default: withCtx(({ node, data }) => [
                createElementVNode("span", null, toDisplayString(data.name), 1)
              ]),
              _: 1
            }, 8, ["props", "data", "onCurrentChange", "onCheck", "onNodeDragStart", "allow-drop"])), [
              [_directive_loading, $setup.loading1]
            ])
          ]),
          _: 1
        }),
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border"
        }, {
          default: withCtx(() => [
            withDirectives((openBlock(), createBlock(_component_el_tree, {
              ref: "treeRef2",
              "current-node-key": `00000000-0000-0000-0000-000000000000`,
              props: $props.nodeProps,
              data: _ctx.dbs,
              "node-key": "id",
              "highlight-current": "",
              onCurrentChange: $setup.dbsChange,
              "expand-on-click-node": false,
              "check-strictly": true,
              "show-checkbox": "",
              onCheck: $setup.dbsClick,
              style: { height: "300px", overflow: "auto" },
              draggable: true,
              onNodeDragStart: $setup.handleDragStart,
              "allow-drop": $setup.collapse
            }, {
              default: withCtx(({ node, data }) => [
                createElementVNode("span", null, toDisplayString(data.name), 1)
              ]),
              _: 1
            }, 8, ["props", "data", "onCurrentChange", "onCheck", "onNodeDragStart", "allow-drop"])), [
              [_directive_loading, $setup.loading2]
            ])
          ]),
          _: 1
        }),
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border"
        }, {
          default: withCtx(() => [
            withDirectives((openBlock(), createBlock(_component_el_tree, {
              ref: "treeRef3",
              "current-node-key": `00000000-0000-0000-0000-000000000000`,
              props: $props.nodeProps,
              data: _ctx.marketingCenters,
              "node-key": "id",
              "highlight-current": "",
              onCurrentChange: $setup.marketingCentersChange,
              "expand-on-click-node": false,
              "check-strictly": true,
              "show-checkbox": "",
              onCheck: $setup.marketingCentersClick,
              style: { height: "300px", overflow: "auto" },
              draggable: true,
              onNodeDragStart: $setup.handleDragStart,
              "allow-drop": $setup.collapse
            }, {
              default: withCtx(({ node, data }) => [
                createElementVNode("span", null, toDisplayString(data.name), 1)
              ]),
              _: 1
            }, 8, ["props", "data", "onCurrentChange", "onCheck", "onNodeDragStart", "allow-drop"])), [
              [_directive_loading, $setup.loading3]
            ])
          ]),
          _: 1
        }),
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border"
        }, {
          default: withCtx(() => [
            withDirectives((openBlock(), createBlock(_component_el_tree, {
              ref: "treeRef4",
              "current-node-key": `00000000-0000-0000-0000-000000000000`,
              props: $props.nodeProps,
              data: _ctx.saleRegions,
              "node-key": "id",
              "highlight-current": "",
              onCurrentChange: $setup.saleRegionsChange,
              "expand-on-click-node": false,
              "check-strictly": true,
              "show-checkbox": "",
              onCheck: $setup.saleRegionsClick,
              style: { height: "300px", overflow: "auto" },
              draggable: true,
              onNodeDragStart: $setup.handleDragStart,
              "allow-drop": $setup.collapse
            }, {
              default: withCtx(({ node, data }) => [
                createElementVNode("span", null, toDisplayString(data.name), 1)
              ]),
              _: 1
            }, 8, ["props", "data", "onCurrentChange", "onCheck", "onNodeDragStart", "allow-drop"])), [
              [_directive_loading, $setup.loading4]
            ])
          ]),
          _: 1
        }),
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border"
        }, {
          default: withCtx(() => [
            withDirectives((openBlock(), createBlock(_component_el_tree, {
              ref: "treeRef5",
              "current-node-key": `00000000-0000-0000-0000-000000000000`,
              props: $props.nodeProps,
              data: _ctx.departments,
              "node-key": "id",
              "highlight-current": "",
              onCurrentChange: $setup.departmentsChange,
              "expand-on-click-node": false,
              "check-strictly": true,
              "show-checkbox": "",
              onCheck: $setup.departmentsClick,
              style: { height: "300px", overflow: "auto" },
              draggable: true,
              onNodeDragStart: $setup.handleDragStart,
              "allow-drop": $setup.collapse
            }, {
              default: withCtx(({ node, data }) => [
                createElementVNode("span", null, toDisplayString(data.name), 1)
              ]),
              _: 1
            }, 8, ["props", "data", "onCurrentChange", "onCheck", "onNodeDragStart", "allow-drop"])), [
              [_directive_loading, $setup.loading5]
            ])
          ]),
          _: 1
        }),
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border"
        }, {
          default: withCtx(() => [
            withDirectives((openBlock(), createBlock(_component_el_tree, {
              ref: "treeRef6",
              "current-node-key": `00000000-0000-0000-0000-000000000000`,
              props: $props.nodeProps,
              data: _ctx.stations,
              "node-key": "id",
              "highlight-current": "",
              onCurrentChange: $setup.stationsChange,
              "expand-on-click-node": false,
              "check-strictly": true,
              "show-checkbox": "",
              onCheck: $setup.stationsClick,
              style: { height: "300px", overflow: "auto" },
              draggable: true,
              onNodeDragStart: $setup.handleDragStart,
              "allow-drop": $setup.collapse
            }, {
              default: withCtx(({ node, data }) => [
                createElementVNode("span", null, toDisplayString(data.name), 1)
              ]),
              _: 1
            }, 8, ["props", "data", "onCurrentChange", "onCheck", "onNodeDragStart", "allow-drop"])), [
              [_directive_loading, $setup.loading6]
            ])
          ]),
          _: 1
        })
      ]),
      _: 1
    }),
    createVNode(_component_el_row, {
      class: "m-roleorg-tree2",
      style: { height: "270px" }
    }, {
      default: withCtx(() => [
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border m-roleorg-tag",
          onDrop: _cache[12] || (_cache[12] = ($event) => $setup.drop($event, 1)),
          onDragover: _cache[13] || (_cache[13] = ($event) => $setup.allowDrop($event))
        }, {
          default: withCtx(() => [
            _ctx.headOfficeSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.headOfficeSelections, (tag) => {
              return openBlock(), createBlock(_component_el_tag, {
                key: tag.id,
                class: "mx-1",
                closable: "",
                onClose: ($event) => $setup.handleClose(tag, _ctx.headOfficeSelections, 1)
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString(tag.name), 1)
                ]),
                _: 2
              }, 1032, ["onClose"]);
            }), 128)) : createCommentVNode("", true),
            _ctx.headOfficeSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_1$g, _hoisted_3$6)) : createCommentVNode("", true)
          ]),
          _: 1
        }),
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border m-roleorg-tag",
          onDrop: _cache[14] || (_cache[14] = ($event) => $setup.drop($event, 2)),
          onDragover: _cache[15] || (_cache[15] = ($event) => $setup.allowDrop($event))
        }, {
          default: withCtx(() => [
            _ctx.dbsSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.dbsSelections, (tag) => {
              return openBlock(), createBlock(_component_el_tag, {
                key: tag.id,
                class: "mx-1",
                closable: "",
                onClose: ($event) => $setup.handleClose(tag, _ctx.dbsSelections, 2)
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString(tag.name), 1)
                ]),
                _: 2
              }, 1032, ["onClose"]);
            }), 128)) : createCommentVNode("", true),
            _ctx.dbsSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_4$3, _hoisted_6$2)) : createCommentVNode("", true)
          ]),
          _: 1
        }),
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border m-roleorg-tag",
          onDrop: _cache[16] || (_cache[16] = ($event) => $setup.drop($event, 3)),
          onDragover: _cache[17] || (_cache[17] = ($event) => $setup.allowDrop($event))
        }, {
          default: withCtx(() => [
            _ctx.marketingCentersSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.marketingCentersSelections, (tag) => {
              return openBlock(), createBlock(_component_el_tag, {
                key: tag.id,
                class: "mx-1",
                closable: "",
                onClose: ($event) => $setup.handleClose(tag, _ctx.marketingCentersSelections, 3)
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString(tag.name), 1)
                ]),
                _: 2
              }, 1032, ["onClose"]);
            }), 128)) : createCommentVNode("", true),
            _ctx.marketingCentersSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_7$2, _hoisted_9$1)) : createCommentVNode("", true)
          ]),
          _: 1
        }),
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border m-roleorg-tag",
          onDrop: _cache[18] || (_cache[18] = ($event) => $setup.drop($event, 4)),
          onDragover: _cache[19] || (_cache[19] = ($event) => $setup.allowDrop($event))
        }, {
          default: withCtx(() => [
            _ctx.saleRegionsSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.saleRegionsSelections, (tag) => {
              return openBlock(), createBlock(_component_el_tag, {
                key: tag.id,
                class: "mx-1",
                closable: "",
                onClose: ($event) => $setup.handleClose(tag, _ctx.saleRegionsSelections, 4)
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString(tag.name), 1)
                ]),
                _: 2
              }, 1032, ["onClose"]);
            }), 128)) : createCommentVNode("", true),
            _ctx.saleRegionsSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_10$1, _hoisted_12$1)) : createCommentVNode("", true)
          ]),
          _: 1
        }),
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border m-roleorg-tag",
          onDrop: _cache[20] || (_cache[20] = ($event) => $setup.drop($event, 5)),
          onDragover: _cache[21] || (_cache[21] = ($event) => $setup.allowDrop($event))
        }, {
          default: withCtx(() => [
            _ctx.departmentsSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.departmentsSelections, (tag) => {
              return openBlock(), createBlock(_component_el_tag, {
                key: tag.id,
                class: "mx-1",
                closable: "",
                onClose: ($event) => $setup.handleClose(tag, _ctx.departmentsSelections, 5)
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString(tag.name), 1)
                ]),
                _: 2
              }, 1032, ["onClose"]);
            }), 128)) : createCommentVNode("", true),
            _ctx.departmentsSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_13$1, _hoisted_15$1)) : createCommentVNode("", true)
          ]),
          _: 1
        }),
        createVNode(_component_el_col, {
          span: 4,
          class: "m-roleorg-border m-roleorg-tag",
          onDrop: _cache[22] || (_cache[22] = ($event) => $setup.drop($event, 6)),
          onDragover: _cache[23] || (_cache[23] = ($event) => $setup.allowDrop($event))
        }, {
          default: withCtx(() => [
            _ctx.stationsSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.stationsSelections, (tag) => {
              return openBlock(), createBlock(_component_el_tag, {
                key: tag.id,
                class: "mx-1",
                closable: "",
                onClose: ($event) => $setup.handleClose(tag, _ctx.stationsSelections, 6)
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString(tag.name), 1)
                ]),
                _: 2
              }, 1032, ["onClose"]);
            }), 128)) : createCommentVNode("", true),
            _ctx.stationsSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_16$1, _hoisted_18$1)) : createCommentVNode("", true)
          ]),
          _: 1
        })
      ]),
      _: 1
    })
  ], 64);
}
var OrgTab = /* @__PURE__ */ _export_sfc$1(_sfc_main$O, [["render", _sfc_render$O], ["__scopeId", "data-v-4e1394e0"]]);
var index_vue_vue_type_style_index_0_scoped_true_lang$1 = "";
const _sfc_main$N = {
  props: {
    ...withSaveProps,
    nodeProps: {
      children: "children",
      label: "label",
      isLeaf: "isLeaf"
    },
    selection: {
      type: Object
    },
    refreshOnCreated: {
      type: Boolean,
      default: true
    }
  },
  emits: ["success"],
  components: { OrgTab },
  setup(props, { emit }) {
    const api2 = tpm.api.admin.account;
    const dialogRef = ref({});
    const loading = ref(false);
    const curroles = ref([]);
    const childrefs = ref([]);
    const model = reactive({
      defaulttab: ""
    });
    let listData = {};
    let tabData = [];
    const setref = (el, key) => {
      if (el) {
        listData[key] = el;
      }
    };
    const nameRef = ref(null);
    const { bind, on: on2 } = useSave({ props, api: api2, model, emit });
    bind.closeOnSuccess = true;
    bind.destroyOnClose = true;
    bind.autoFocusRef = nameRef;
    bind.width = "1400px";
    bind.height = "790px";
    bind.title = "\u914D\u7F6E\u7528\u6237\u89D2\u8272\u7EC4\u7EC7";
    bind.icon = "edit";
    bind.beforeSubmit = () => {
      tabData = [];
      for (let key in listData) {
        let m = listData[key].model;
        console.log("listData -> model:", m);
        tabData.push({
          roleId: key,
          headOffice: m.headOfficeSelections.filter((s) => s.id).map((b) => b.id),
          dbs: m.dbsSelections.filter((s) => s.id).map((b) => b.id),
          marketingCenters: m.marketingCentersSelections.filter((s) => s.id).map((b) => b.id),
          saleRegions: m.saleRegionsSelections.filter((s) => s.id).map((b) => b.id),
          departments: m.departmentsSelections.filter((s) => s.id).map((b) => b.id),
          stations: m.stationsSelections.filter((s) => s.id)
        });
      }
      console.log("beforeSubmit -> tabData", tabData);
    };
    bind.action = () => {
      var _a;
      let curt = props.selection;
      if (((_a = curt.roles) == null ? void 0 : _a.length) == 0) {
        bind.successMessage = "\u89D2\u8272\u672A\u6307\u5B9A";
        emit("success", null);
      }
      console.log("updateAccountRoleOrg -> curt", curt);
      let params = {
        id: curt.id,
        datas: tabData
      };
      return new Promise((resolve) => {
        console.log("updateAccountRoleOrg", params);
        api2.updateAccountRoleOrg(params).then((data) => {
          if (data.msg === "success") {
            emit("success", data);
          }
          resolve(data);
        });
      });
    };
    const handleOpened = async () => {
      curroles.value = [];
      const { roles } = props.selection;
      if (Array.isArray(roles)) {
        model.defaulttab = roles[0].value;
        roles.forEach(function(r, index2) {
          let sel = props.selection;
          let curData = {
            role: r,
            childData: deepClone(sel)
          };
          curroles.value.push(curData);
        });
      }
      console.log("curroles.value", curroles.value);
    };
    const handleTabChange = (role) => {
      console.log("handleTabChange", role);
    };
    const deepClone = (source) => {
      var sourceCopy = source instanceof Array ? [] : {};
      for (var item in source) {
        sourceCopy[item] = typeof source[item] === "object" ? deepClone(source[item]) : source[item];
      }
      return sourceCopy;
    };
    return {
      ...toRefs(model),
      dialogRef,
      model,
      bind,
      on: on2,
      childrefs,
      deepClone,
      loading,
      curroles,
      handleOpened,
      handleTabChange,
      setref
    };
  }
};
function _sfc_render$N(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_icon = resolveComponent("m-icon");
  const _component_org_tab = resolveComponent("org-tab");
  const _component_el_tab_pane = resolveComponent("el-tab-pane");
  const _component_el_tabs = resolveComponent("el-tabs");
  const _component_m_tabs = resolveComponent("m-tabs");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    ref: "dialogRef",
    model: $setup.model
  }, $setup.bind, toHandlers($setup.on), {
    onOpened: $setup.handleOpened,
    loading: $setup.loading,
    title: "\u914D\u7F6E\u7528\u6237\u89D2\u8272\u7EC4\u7EC7"
  }), {
    default: withCtx(() => [
      createVNode(_component_m_tabs, null, {
        default: withCtx(() => [
          createVNode(_component_el_tabs, {
            modelValue: _ctx.defaulttab,
            "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => _ctx.defaulttab = $event),
            onTabChange: $setup.handleTabChange
          }, {
            default: withCtx(() => [
              (openBlock(true), createElementBlock(Fragment, null, renderList($setup.curroles, (item, index2) => {
                return openBlock(), createBlock(_component_el_tab_pane, {
                  key: item.role.label,
                  name: item.role.value,
                  ref_for: true,
                  ref: $setup.childrefs,
                  lazy: ""
                }, {
                  label: withCtx(() => [
                    createElementVNode("span", null, [
                      createVNode(_component_m_icon, { name: "user" }),
                      createElementVNode("span", null, toDisplayString(item.role.label), 1)
                    ])
                  ]),
                  default: withCtx(() => [
                    createVNode(_component_org_tab, {
                      ref_for: true,
                      ref: (el) => $setup.setref(el, item.role.value),
                      role: item.role,
                      selection: item.childData
                    }, null, 8, ["role", "selection"])
                  ]),
                  _: 2
                }, 1032, ["name"]);
              }), 128))
            ]),
            _: 1
          }, 8, ["modelValue", "onTabChange"])
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "onOpened", "loading"]);
}
var RoleOrg = /* @__PURE__ */ _export_sfc$1(_sfc_main$N, [["render", _sfc_render$N], ["__scopeId", "data-v-f568f802"]]);
const _sfc_main$M = {
  props: {
    nodeProps: {
      children: "children",
      label: "label"
    },
    selection: {
      type: Object
    },
    refreshOnCreated: {
      type: Boolean,
      default: true
    }
  },
  emits: ["change"],
  setup(props, { emit }) {
    const { store: store2, $t } = tpm;
    const { getTree, getOrgLevel } = tpm.api.admin.role;
    useMessage();
    const currentKey = ref("00000000-0000-0000-0000-000000000000");
    const treeRef = ref();
    const treeData = ref([]);
    const loading = ref(false);
    const model = reactive({
      defaultkeys: [],
      defaultExpandedCids: []
    });
    const handleTreeChange = (data) => {
      if (data != null) {
        currentKey.value = data.id;
        emit("change", data.data);
        let getNode = treeRef.value.getCheckedNodes();
        if (getNode.length > 0) {
          getNode.forEach((item) => {
            treeRef.value.setChecked(item.id, false);
            treeRef.value.setChecked(data.key, true);
          });
        } else {
          treeRef.value.setChecked(data.key, true);
        }
      }
    };
    const checkClick = (data) => {
      let getNode = treeRef.value.getCheckedNodes();
      if (getNode.length > 0) {
        getNode.forEach((item) => {
          if (data.id == item.id) {
            treeRef.value.setChecked(item.id, true);
          } else {
            treeRef.value.setChecked(item.id, false);
          }
        });
      }
      emit("change", data);
    };
    const loadfirstnode = async (resolve, includes) => {
      loading.value = true;
      const res = await getTree({
        level: 10,
        includes: includes || []
      });
      let orgs = includes == null ? void 0 : includes.map((s) => s.orgId);
      let data = res.filter((m) => m);
      if (orgs.length > 0)
        ;
      loading.value = false;
      console.log("data", data);
      if (props.selection.orgId !== void 0 && props.selection.orgId !== "" && props.selection.orgId !== "undefined") {
        model.defaultkeys.push(props.selection.orgId);
        model.defaultExpandedCids.push(props.selection.orgId);
      }
      if (data && Array.isArray(data)) {
        data.forEach((item) => {
          model.defaultExpandedCids.push(item.id);
        });
      }
      return resolve(data);
    };
    const loadchildnode = async (node, resolve, includes) => {
      let params = {
        level: node.level + 1,
        parentId: node.key || "00000000-0000-0000-0000-000000000000",
        includes: includes || []
      };
      loading.value = true;
      console.log("loadchildnode - > params", params);
      let res = await getOrgLevel(params);
      if (includes.length == 0)
        ;
      if (node.level >= 3) {
        res = [];
      }
      console.log("loadchildnode", res);
      loading.value = false;
      if (res && Array.isArray(res)) {
        res.forEach((item) => {
        });
      }
      return resolve(res);
    };
    const loadNode = (node, resolve) => {
      let profile = store2.state.app.profile;
      let includes = profile.accountRoleOrgs;
      console.log("node.level", node.level);
      console.log("profile", profile);
      console.log("includes", includes);
      if (node.level == 0) {
        loadfirstnode(resolve, includes);
      }
      if (node.level >= 1) {
        loadchildnode(node, resolve, includes);
      }
    };
    watch(props.selection, () => {
      if (props.selection.orgId !== void 0 && props.selection.orgId !== "" && props.selection.orgId !== "undefined") {
        treeRef.value.setChecked(props.selection.orgId, true);
      }
    });
    return {
      ...toRefs(model),
      loading,
      treeRef,
      treeData,
      handleTreeChange,
      checkClick,
      loadNode,
      loadfirstnode,
      loadchildnode
    };
  }
};
function _sfc_render$M(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_icon = resolveComponent("m-icon");
  const _component_el_tree = resolveComponent("el-tree");
  const _directive_loading = resolveDirective("loading");
  return withDirectives((openBlock(), createBlock(_component_el_tree, {
    ref: "treeRef",
    data: $setup.treeData,
    "current-node-key": `00000000-0000-0000-0000-000000000000`,
    props: $props.nodeProps,
    load: $setup.loadNode,
    lazy: "",
    "node-key": "id",
    "highlight-current": "",
    onCurrentChange: $setup.handleTreeChange,
    "expand-on-click-node": false,
    "check-strictly": true,
    "show-checkbox": "",
    onCheck: $setup.checkClick,
    "default-checked-keys": _ctx.defaultkeys,
    "default-expanded-keys": _ctx.defaultExpandedCids
  }, {
    default: withCtx(({ node, data }) => [
      createElementVNode("span", null, [
        createVNode(_component_m_icon, {
          name: "folder-o",
          class: "m-margin-r-5"
        }),
        createElementVNode("span", null, toDisplayString(data.label == null ? data.name : data.label), 1)
      ])
    ]),
    _: 1
  }, 8, ["data", "props", "load", "onCurrentChange", "onCheck", "default-checked-keys", "default-expanded-keys"])), [
    [_directive_loading, $setup.loading]
  ]);
}
var TreePage$1 = /* @__PURE__ */ _export_sfc$1(_sfc_main$M, [["render", _sfc_render$M]]);
var index_vue_vue_type_style_index_0_scoped_true_lang = "";
const _sfc_main$L = {
  components: { Save: Save$5, RoleOrg, TreePage: TreePage$1 },
  props: {},
  emits: ["update:roleOrgVisible"],
  setup(props, { emit }) {
    const curSelection = ref({});
    const showTree = ref(false);
    const message = useMessage();
    const { query, remove } = tpm.api.admin.account;
    const { getNodeTree, getPathByKey } = tpm.api.admin.role;
    const rowdatas = ref([{ name: "select", desc: "\u5F53\u7528\u6237\u624B\u52A8\u52FE\u9009\u6570\u636E\u884C\u7684 Checkbox \u65F6\u89E6\u53D1\u7684\u4E8B\u4EF6" }]);
    const model = reactive({
      keys: "",
      type: -1,
      status: -1,
      headOffice: [],
      dbs: [],
      marketingCenters: [],
      saleRegions: [],
      departments: [],
      stations: []
    });
    const cols = [
      { prop: "id", label: "tpm.id", width: "55", show: false },
      { prop: "username", label: "tpm.login.username" },
      { prop: "name", label: "mod.admin.name" },
      { prop: "phone", label: "tpm.phone", show: false },
      { prop: "email", label: "tpm.email", show: false },
      { prop: "status", label: "tpm.status" },
      { prop: "accountRoles", label: "\u8D26\u6237\u89D2\u8272", expand: true },
      { prop: "accountRoleOrgs", label: "\u8D26\u6237\u89D2\u8272\u7EC4\u7EC7", expand: true },
      { prop: "orgId", label: "\u6240\u5728\u7EC4\u7EC7", show: true },
      { prop: "userBP", label: "BP\u7F16\u53F7", show: true },
      { prop: "ldapName", label: "LDAP\u8D26\u6237", show: true },
      { prop: "roleName", label: "\u62E5\u6709\u89D2\u8272" },
      ...entityBaseCols()
    ];
    const list = useList();
    const roleOrgVisible = ref(false);
    const roleOrgSet = (row) => {
      var _a;
      if (((_a = row.roles) == null ? void 0 : _a.length) == 0) {
        message.alert("\u5BF9\u4E0D\u8D77\uFF0C\u62D2\u7EDD\u64CD\u4F5C\uFF0C\u89D2\u8272\u597D\u50CF\u8FD8\u6CA1\u6307\u5B9A\u54C8\uFF01");
        return;
      }
      curSelection.value = row;
      curSelection.value.headOffice = [];
      curSelection.value.dbs = [];
      curSelection.value.marketingCenters = [];
      curSelection.value.saleRegions = [];
      curSelection.value.departments = [];
      curSelection.value.stations = [];
      roleOrgVisible.value = true;
      console.log("curSelection.value", curSelection.value);
    };
    const roleOrgRefresh = (data) => {
      console.log("roleOrgRefresh");
      list.refresh();
      roleOrgVisible.value = false;
    };
    const formatPaths = ref([]);
    const mergePaths = ref([]);
    const handleExpand = async (row, expandedRows) => {
      console.log("handleExpand -> row", row);
      await getNodeTree(0);
      let rolePath = [];
      if (row && Array.isArray(row.roles)) {
        row.roles.forEach((r) => {
        });
      }
      formatPaths.value = rolePath;
    };
    const handleExpand2 = (row, expandedRows) => {
      console.log("handleExpand2 -> row", row);
      let data = {
        dbs: row.dbs,
        departments: row.departments,
        headOffice: row.headOffice,
        marketingCenters: row.marketingCenters,
        saleRegions: row.saleRegions,
        stations: row.stations
      };
      rowdatas.value = data;
      showTree.value = true;
    };
    const importData = () => {
    };
    const filterOrgs = (data) => {
      console.log("filterOrgs", data);
      model.headOffice = data.headOffice;
      model.dbs = data.dbs;
      model.marketingCenters = data.marketingCenters;
      model.saleRegions = data.saleRegions;
      model.departments = data.departments;
      model.stations = data.stations;
      console.log("model", model);
      list.refresh();
    };
    return {
      ...list,
      page: page$a,
      model,
      cols,
      query,
      remove,
      roleOrgSet,
      roleOrgVisible,
      curSelection,
      roleOrgRefresh,
      handleExpand,
      handleExpand2,
      formatPaths,
      mergePaths,
      showTree,
      importData,
      filterOrgs,
      rowdatas
    };
  }
};
function _sfc_render$L(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_admin_account_type_select = resolveComponent("m-admin-account-type-select");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_input = resolveComponent("el-input");
  const _component_m_admin_account_status_select = resolveComponent("m-admin-account-status-select");
  const _component_m_admin_org_picker = resolveComponent("m-admin-org-picker");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_button = resolveComponent("m-button");
  const _component_el_tag = resolveComponent("el-tag");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_el_table_column = resolveComponent("el-table-column");
  const _component_el_table = resolveComponent("el-table");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_role_org = resolveComponent("role-org");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        onExpandChange: $setup.handleExpand2
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("\u8D26\u6237\u7C7B\u578B"),
            prop: "type"
          }, {
            default: withCtx(() => [
              createVNode(_component_m_admin_account_type_select, {
                modelValue: $setup.model.type,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.type = $event)
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_el_form_item, {
            label: _ctx.$t("\u641C\u7D22\u5173\u952E\u5B57"),
            prop: "keys"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.keys,
                "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.keys = $event),
                clearable: "",
                placeholder: "\u7528\u6237\u540D/\u59D3\u540D/\u624B\u673A\u53F7"
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_el_form_item, {
            label: _ctx.$t("\u8D26\u6237\u72B6\u6001"),
            prop: "status"
          }, {
            default: withCtx(() => [
              createVNode(_component_m_admin_account_status_select, {
                modelValue: $setup.model.status,
                "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.status = $event)
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_el_form_item, {
            label: "",
            prop: "orgs"
          }, {
            default: withCtx(() => [
              createVNode(_component_m_admin_org_picker, {
                modelValue: $setup.model,
                "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model = $event),
                placeholder: "\u8FC7\u6EE4\u7EC4\u7EC7",
                onUpdate: $setup.filterOrgs
              }, null, 8, ["modelValue", "onUpdate"])
            ]),
            _: 1
          })
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"]),
          createVNode(_component_m_button, {
            text: "",
            type: "primary",
            icon: $setup.page.buttons.import.icon,
            code: $setup.page.buttons.import.code,
            onClick: $setup.importData
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.page.buttons.import.text), 1)
            ]),
            _: 1
          }, 8, ["icon", "code", "onClick"]),
          createVNode(_component_m_button, {
            text: "",
            type: "primary",
            icon: $setup.page.buttons.importTemplate.icon,
            code: $setup.page.buttons.importTemplate.code,
            onClick: $setup.importData
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.page.buttons.importTemplate.text), 1)
            ]),
            _: 1
          }, 8, ["icon", "code", "onClick", "text"]),
          createVNode(_component_m_button, {
            text: "",
            type: "primary",
            icon: $setup.page.buttons.export.icon,
            code: $setup.page.buttons.export.code,
            onClick: $setup.importData
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.page.buttons.export.text), 1)
            ]),
            _: 1
          }, 8, ["icon", "code", "onClick", "text"]),
          createVNode(_component_m_button, {
            text: "",
            type: "primary",
            icon: $setup.page.buttons.batPermission.icon,
            code: $setup.page.buttons.batPermission.code,
            onClick: $setup.importData
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.page.buttons.batPermission.text), 1)
            ]),
            _: 1
          }, 8, ["icon", "code", "onClick", "text"])
        ]),
        "col-status": withCtx(({ row }) => [
          row.status === 0 ? (openBlock(), createBlock(_component_el_tag, {
            key: 0,
            type: "info",
            size: "small",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.admin.account_inactive")), 1)
            ]),
            _: 1
          })) : row.status === 1 ? (openBlock(), createBlock(_component_el_tag, {
            key: 1,
            type: "success",
            size: "small",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.admin.account_activated")), 1)
            ]),
            _: 1
          })) : createCommentVNode("", true),
          row.status === 2 ? (openBlock(), createBlock(_component_el_tag, {
            key: 2,
            type: "warning",
            size: "small",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("mod.admin.account_disabled")), 1)
            ]),
            _: 1
          })) : createCommentVNode("", true)
        ]),
        "col-roleName": withCtx(({ row }) => [
          (openBlock(true), createElementBlock(Fragment, null, renderList(row.roles, (item) => {
            return openBlock(), createBlock(_component_el_tag, {
              type: "success",
              size: "small",
              effect: "dark",
              class: "m-margin-r-10"
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(item.label), 1)
              ]),
              _: 2
            }, 1024);
          }), 256))
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button, {
            icon: $setup.page.buttons.roleorgset.icon,
            link: true,
            type: "primary",
            code: $setup.page.buttons.roleorgset.code,
            onClick: ($event) => $setup.roleOrgSet(row),
            onSuccess: _ctx.refresh
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.page.buttons.roleorgset.text), 1)
            ]),
            _: 2
          }, 1032, ["icon", "code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_edit, {
            code: $setup.page.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: _ctx.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.page.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: _ctx.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        expand: withCtx(({ row }) => [
          createVNode(_component_el_row, { gutter: 10 }, {
            default: withCtx(() => [
              createVNode(_component_el_col, { span: 4 }, {
                default: withCtx(() => [
                  createVNode(_component_el_table, {
                    class: "m-component_table",
                    data: row.mOrgs.headOffice,
                    border: ""
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_el_table_column, {
                        prop: "name",
                        label: "\u96EA\u82B1\u603B\u90E8"
                      })
                    ]),
                    _: 2
                  }, 1032, ["data"])
                ]),
                _: 2
              }, 1024),
              createVNode(_component_el_col, { span: 4 }, {
                default: withCtx(() => [
                  createVNode(_component_el_table, {
                    class: "m-component_table",
                    data: row.mOrgs.dbs,
                    border: ""
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_el_table_column, {
                        prop: "name",
                        label: "\u4E8B\u4E1A\u90E8"
                      })
                    ]),
                    _: 2
                  }, 1032, ["data"])
                ]),
                _: 2
              }, 1024),
              createVNode(_component_el_col, { span: 4 }, {
                default: withCtx(() => [
                  createVNode(_component_el_table, {
                    class: "m-component_table",
                    data: row.mOrgs.marketingCenters,
                    border: ""
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_el_table_column, {
                        prop: "name",
                        label: "\u8425\u9500\u4E2D\u5FC3"
                      })
                    ]),
                    _: 2
                  }, 1032, ["data"])
                ]),
                _: 2
              }, 1024),
              createVNode(_component_el_col, { span: 4 }, {
                default: withCtx(() => [
                  createVNode(_component_el_table, {
                    class: "m-component_table",
                    data: row.mOrgs.saleRegions,
                    border: ""
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_el_table_column, {
                        prop: "name",
                        label: "\u9500\u552E\u5927\u533A"
                      })
                    ]),
                    _: 2
                  }, 1032, ["data"])
                ]),
                _: 2
              }, 1024),
              createVNode(_component_el_col, { span: 4 }, {
                default: withCtx(() => [
                  createVNode(_component_el_table, {
                    class: "m-component_table",
                    data: row.mOrgs.departments,
                    border: ""
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_el_table_column, {
                        prop: "name",
                        label: "\u4E1A\u52A1\u90E8"
                      })
                    ]),
                    _: 2
                  }, 1032, ["data"])
                ]),
                _: 2
              }, 1024),
              createVNode(_component_el_col, { span: 4 }, {
                default: withCtx(() => [
                  createVNode(_component_el_table, {
                    class: "m-component_table",
                    data: row.mOrgs.stations,
                    border: ""
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_el_table_column, {
                        prop: "name",
                        label: "\u5DE5\u4F5C\u7AD9"
                      })
                    ]),
                    _: 2
                  }, 1032, ["data"])
                ]),
                _: 2
              }, 1024)
            ]),
            _: 2
          }, 1024)
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "onExpandChange"]),
      createVNode(_component_save, {
        selection: _ctx.selection,
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => _ctx.saveVisible = $event),
        mode: _ctx.mode,
        onSuccess: _ctx.refresh
      }, null, 8, ["selection", "id", "modelValue", "mode", "onSuccess"]),
      createVNode(_component_role_org, {
        selection: $setup.curSelection,
        id: $setup.curSelection.id,
        modelValue: $setup.roleOrgVisible,
        "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.roleOrgVisible = $event),
        mode: _ctx.mode,
        onSuccess: $setup.roleOrgRefresh
      }, null, 8, ["selection", "id", "modelValue", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$6 = /* @__PURE__ */ _export_sfc$1(_sfc_main$L, [["render", _sfc_render$L], ["__scopeId", "data-v-378f9b39"]]);
const page$9 = {
  "name": "admin_account",
  "icon": "user",
  "path": "/admin/account",
  "permissions": [
    "admin_account_query_get",
    "admin_account_DefaultPassword_get"
  ],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "admin_account_add",
      "permissions": [
        "admin_account_add_post"
      ]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "admin_account_edit",
      "permissions": [
        "admin_account_edit_get",
        "admin_account_update_post"
      ]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "admin_account_delete",
      "permissions": [
        "admin_account_delete_delete"
      ]
    },
    "roleorgset": {
      "text": "\u7EC4\u7EC7",
      "icon": "cog",
      "code": "admin_account_roleorgset",
      "permissions": [
        "admin_account_roleorgset_post"
      ]
    },
    "import": {
      "text": "\u5BFC\u5165",
      "icon": "cog",
      "code": "admin_account_import",
      "permissions": [
        "admin_account_import_post"
      ]
    },
    "importTemplate": {
      "text": "\u5BFC\u5165\u6A21\u677F",
      "icon": "cog",
      "code": "admin_account_importTemplate",
      "permissions": [
        "admin_account_importTemplate_post"
      ]
    },
    "export": {
      "text": "\u5BFC\u51FA",
      "icon": "cog",
      "code": "admin_account_export",
      "permissions": [
        "admin_account_export_download"
      ]
    },
    "batPermission": {
      "text": "\u6279\u91CF\u5206\u914D\u6743\u9650",
      "icon": "cog",
      "code": "admin_account_batPermission",
      "permissions": [
        "admin_account_dbatPermission_post"
      ]
    }
  }
};
page$9.component = component$6;
component$6.name = page$9.name;
var index_vue_vue_type_style_index_0_lang$9 = "";
const _sfc_main$K = {
  components: {},
  setup() {
    const { modules } = tpm;
    const colors = ["#409EFF", "#67C23A", "#E6A23C", "#F56C6C", "#6d3cf7", "#0079de"];
    const model = reactive({
      tab: "",
      modules: []
    });
    modules.forEach((m) => {
      console.log("modules -> m", m);
      console.log("modules -> name", `m-admin-config-${m.code.toLowerCase()}`);
      m.color = colors[parseInt(Math.random() * colors.length)];
      let mt = m.components.find((s) => s.name == `config-${m.code.toLowerCase()}`);
      console.log("_commponent", mt);
      m.component = mt;
      m.name = `m-${m.code.toLowerCase()}-config-${m.code.toLowerCase()}`;
    });
    const getNo = (item) => {
      return item.id < 10 ? "0" + item.id : "" + item.id;
    };
    const autoheight = ref(0);
    const handleHeight = () => {
      autoheight.value = window.innerHeight - 100;
    };
    onActivated(() => {
      nextTick(() => {
        handleHeight();
      });
      window.addEventListener("resize", handleHeight);
    });
    onDeactivated(() => {
      window.removeEventListener("resize", handleHeight);
    });
    onBeforeMount(() => {
      window.addEventListener("resize", handleHeight);
    });
    onMounted(() => {
      nextTick(() => {
        handleHeight();
      });
    });
    onBeforeUnmount(() => {
      window.removeEventListener("resize", handleHeight);
    });
    return {
      ...toRefs(model),
      editableTabsValue: "m-admin-config-admin",
      model,
      modules,
      getNo,
      autoheight,
      handleHeight
    };
  }
};
const _hoisted_1$f = { class: "textalign" };
function _sfc_render$K(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_icon = resolveComponent("m-icon");
  const _component_el_tab_pane = resolveComponent("el-tab-pane");
  const _component_el_tabs = resolveComponent("el-tabs");
  return openBlock(), createBlock(_component_el_tabs, {
    modelValue: $setup.editableTabsValue,
    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.editableTabsValue = $event),
    class: "m-admin-config-module page",
    "tab-position": "left",
    type: "border-card",
    style: normalizeStyle([{ height: $setup.autoheight + "px" }])
  }, {
    default: withCtx(() => [
      (openBlock(true), createElementBlock(Fragment, null, renderList($setup.modules, (item) => {
        return openBlock(), createBlock(_component_el_tab_pane, {
          key: item.name,
          name: item.name,
          lazy: ""
        }, {
          label: withCtx(() => [
            createElementVNode("span", _hoisted_1$f, [
              createVNode(_component_m_icon, {
                name: item.icon
              }, null, 8, ["name"]),
              createTextVNode(" " + toDisplayString($setup.getNo(item)) + "_" + toDisplayString(item.label), 1)
            ])
          ]),
          default: withCtx(() => [
            (openBlock(), createBlock(resolveDynamicComponent(item.name)))
          ]),
          _: 2
        }, 1032, ["name"]);
      }), 128))
    ]),
    _: 1
  }, 8, ["modelValue", "style"]);
}
var ModulePane = /* @__PURE__ */ _export_sfc$1(_sfc_main$K, [["render", _sfc_render$K]]);
var _export_sfc = (sfc, props) => {
  const target = sfc.__vccOpts || sfc;
  for (const [key, val] of props) {
    target[key] = val;
  }
  return target;
};
const _sfc_main$J = defineComponent({
  name: "UploadFilled"
});
const _hoisted_1$e = {
  viewBox: "0 0 1024 1024",
  xmlns: "http://www.w3.org/2000/svg"
};
const _hoisted_2$a = /* @__PURE__ */ createElementVNode("path", {
  fill: "currentColor",
  d: "M544 864V672h128L512 480 352 672h128v192H320v-1.6c-5.376.32-10.496 1.6-16 1.6A240 240 0 0 1 64 624c0-123.136 93.12-223.488 212.608-237.248A239.808 239.808 0 0 1 512 192a239.872 239.872 0 0 1 235.456 194.752c119.488 13.76 212.48 114.112 212.48 237.248a240 240 0 0 1-240 240c-5.376 0-10.56-1.28-16-1.6v1.6H544z"
}, null, -1);
const _hoisted_3$5 = [
  _hoisted_2$a
];
function _sfc_render$J(_ctx, _cache, $props, $setup, $data, $options) {
  return openBlock(), createElementBlock("svg", _hoisted_1$e, _hoisted_3$5);
}
var uploadFilled = /* @__PURE__ */ _export_sfc(_sfc_main$J, [["render", _sfc_render$J]]);
const _sfc_main$I = {
  components: { UploadFilled: uploadFilled },
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
      type: 0,
      code: "System",
      title: "TPM\u8D39\u7528\u5E73\u53F02.0",
      logo: "",
      copyright: "\u7248\u6743\u6240\u6709:CRB.TPM",
      userPage: ""
    });
    const edits = () => {
    };
    const rules = {};
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
const _hoisted_1$d = { style: { "padding": "50px" } };
const _hoisted_2$9 = /* @__PURE__ */ createElementVNode("div", { class: "el-upload__text" }, [
  /* @__PURE__ */ createElementVNode("em", null, "\u70B9\u51FB\u4E0A\u4F20")
], -1);
const _hoisted_3$4 = /* @__PURE__ */ createElementVNode("div", { class: "el-upload__tip" }, " jpg/png \u6587\u4EF6\u5927\u5C0F\u4E0D\u80FD\u8D85\u8FC7 500kb ", -1);
function _sfc_render$I(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_upload_filled = resolveComponent("upload-filled");
  const _component_el_icon = resolveComponent("el-icon");
  const _component_el_upload = resolveComponent("el-upload");
  const _component_m_button = resolveComponent("m-button");
  const _component_m_form = resolveComponent("m-form");
  return openBlock(), createElementBlock("div", _hoisted_1$d, [
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
          "label-width": "150px",
          label: "\u6807\u9898",
          prop: "title"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_input, {
              modelValue: $setup.model.title,
              "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.title = $event),
              placeholder: "TPM\u8D39\u7528\u5E73\u53F02.0"
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }),
        createVNode(_component_el_form_item, {
          "label-width": "150px",
          label: "\u7248\u6743\u7533\u660E",
          prop: "copyright"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_input, {
              modelValue: $setup.model.copyright,
              "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.copyright = $event),
              placeholder: "\u7248\u6743\u6240\u6709\uFF1ACRB.TPM"
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }),
        createVNode(_component_el_form_item, {
          "label-width": "150px",
          label: "Logo",
          prop: "logo"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_upload, {
              class: "upload-demo",
              drag: "",
              action: "",
              multiple: ""
            }, {
              tip: withCtx(() => [
                _hoisted_3$4
              ]),
              default: withCtx(() => [
                createVNode(_component_el_icon, { class: "el-icon--upload" }, {
                  default: withCtx(() => [
                    createVNode(_component_upload_filled)
                  ]),
                  _: 1
                }),
                _hoisted_2$9
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_form_item, {
          "label-width": "150px",
          label: "\u8D26\u6237\u4FE1\u606F\u9875",
          prop: "userPage"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_input, {
              modelValue: $setup.model.userPage,
              "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.userPage = $event),
              placeholder: "\u8BF7\u586B\u5199\u524D\u7AEF\u8DEF\u7531\u540D\u79F0\uFF0C\u9ED8\u8BA4\u4F7F\u7528\u7CFB\u7EDF\u81EA\u5E26\u7684userinfo"
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }),
        createVNode(_component_el_form_item, { "label-width": "150px" }, {
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
var SystemConfig = /* @__PURE__ */ _export_sfc$1(_sfc_main$I, [["render", _sfc_render$I]]);
const _sfc_main$H = {
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
      type: 0,
      code: "Component",
      login: { defaultAccountType: 0, pageType: null },
      menu: { uniqueOpened: true, defaultExpanded: false },
      dialog: { closeOnClickModal: false, draggable: false },
      list: { serialNumberName: null },
      tabnav: { enabled: true, showHome: true, homeUrl: null, showIcon: true, maxOpenCount: 20 },
      toolbar: { fullscreen: true, skin: true, logout: true, userInfo: true },
      customCss: null
    });
    const edits = () => {
    };
    const rules = {};
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
const _hoisted_1$c = { style: { "padding": "50px" } };
function _sfc_render$H(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_divider = resolveComponent("el-divider");
  const _component_el_option = resolveComponent("el-option");
  const _component_el_select = resolveComponent("el-select");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_el_switch = resolveComponent("el-switch");
  const _component_el_input = resolveComponent("el-input");
  const _component_m_button = resolveComponent("m-button");
  const _component_m_form = resolveComponent("m-form");
  return openBlock(), createElementBlock("div", _hoisted_1$c, [
    createVNode(_component_m_form, {
      ref: "formRef",
      action: $setup.action,
      model: $setup.model,
      rules: $setup.rules,
      disabled: $setup.disabled,
      onSuccess: $setup.handleSuccess
    }, {
      default: withCtx(() => [
        createVNode(_component_el_divider, { "content-position": "left" }, {
          default: withCtx(() => [
            createTextVNode("\u767B\u5F55\u9875")
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 10,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u9ED8\u8BA4\u8D26\u6237\u7C7B\u578B",
                  prop: "defaultAccountType"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_select, {
                      modelValue: $setup.model.login.defaultAccountType,
                      "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.login.defaultAccountType = $event)
                    }, {
                      default: withCtx(() => [
                        createVNode(_component_el_option, {
                          label: "\u7BA1\u7406\u5458",
                          value: 0
                        }),
                        createVNode(_component_el_option, {
                          label: "\u4E2A\u4EBA",
                          value: 1
                        }),
                        createVNode(_component_el_option, {
                          label: "\u4F01\u4E1A",
                          value: 2
                        })
                      ]),
                      _: 1
                    }, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 10 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u9875\u7C7B\u578B",
                  prop: "pageType"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_select, {
                      modelValue: $setup.model.login.pageType,
                      "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.login.pageType = $event)
                    }, {
                      default: withCtx(() => [
                        (openBlock(true), createElementBlock(Fragment, null, renderList(_ctx.pageTypeOptions, (item) => {
                          return openBlock(), createBlock(_component_el_option, {
                            key: item,
                            label: item,
                            value: item
                          }, null, 8, ["label", "value"]);
                        }), 128))
                      ]),
                      _: 1
                    }, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_divider, { "content-position": "left" }, {
          default: withCtx(() => [
            createTextVNode("\u5DE5\u5177\u680F")
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 5,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u5168\u5C4F\u63A7\u5236",
                  prop: "fullscreen"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.toolbar.fullscreen,
                      "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.toolbar.fullscreen = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 5 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u76AE\u80A4\u8BBE\u7F6E",
                  prop: "skin"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.toolbar.skin,
                      "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.toolbar.skin = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 5 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u9000\u51FA\u6309\u94AE",
                  prop: "logout"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.toolbar.logout,
                      "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.model.toolbar.logout = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 5 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u7528\u6237\u4FE1\u606F",
                  prop: "userInfo"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.toolbar.userInfo,
                      "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.model.toolbar.userInfo = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_divider, { "content-position": "left" }, {
          default: withCtx(() => [
            createTextVNode("\u83DC\u5355")
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 10,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u4FDD\u6301\u5B50\u83DC\u5355\u7684\u5C55\u5F00",
                  prop: "menu.uniqueOpened"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.menu.uniqueOpened,
                      "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => $setup.model.menu.uniqueOpened = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, {
              span: 10,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u4FA7\u680F\u662F\u5426\u9ED8\u8BA4\u6536\u8D77",
                  prop: "menu.defaultExpanded"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.menu.defaultExpanded,
                      "onUpdate:modelValue": _cache[7] || (_cache[7] = ($event) => $setup.model.menu.defaultExpanded = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_divider, { "content-position": "left" }, {
          default: withCtx(() => [
            createTextVNode("\u5BF9\u8BDD\u6846")
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 10,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u53EF\u70B9\u51FB\u6A21\u6001\u6846\u5173\u95ED",
                  prop: "dialog.closeOnClickModal"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.dialog.closeOnClickModal,
                      "onUpdate:modelValue": _cache[8] || (_cache[8] = ($event) => $setup.model.dialog.closeOnClickModal = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 10 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u9ED8\u8BA4\u53EF\u62D6\u62FD",
                  prop: "dialog.draggable"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.dialog.draggable,
                      "onUpdate:modelValue": _cache[9] || (_cache[9] = ($event) => $setup.model.dialog.draggable = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_divider, { "content-position": "left" }, {
          default: withCtx(() => [
            createTextVNode("\u5217\u8868\u9875")
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 10,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u5E8F\u53F7\u8868\u5934\u540D\u79F0",
                  prop: "list.serialNumberName"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_input, {
                      modelValue: $setup.model.list.serialNumberName,
                      "onUpdate:modelValue": _cache[10] || (_cache[10] = ($event) => $setup.model.list.serialNumberName = $event),
                      placeholder: "\u9ED8\u8BA4 #"
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_divider, { "content-position": "left" }, {
          default: withCtx(() => [
            createTextVNode("\u6807\u7B7E\u5BFC\u822A")
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 10,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u542F\u7528",
                  prop: "tabnav.enabled"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.tabnav.enabled,
                      "onUpdate:modelValue": _cache[11] || (_cache[11] = ($event) => $setup.model.tabnav.enabled = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 10 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u663E\u793A\u56FE\u6807",
                  prop: "tabnav.showIcon"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.tabnav.showIcon,
                      "onUpdate:modelValue": _cache[12] || (_cache[12] = ($event) => $setup.model.tabnav.showIcon = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 10,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u663E\u793A\u9996\u9875",
                  prop: "tabnav.showHome"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.tabnav.showHome,
                      "onUpdate:modelValue": _cache[13] || (_cache[13] = ($event) => $setup.model.tabnav.showHome = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 10 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u9996\u9875\u5730\u5740",
                  prop: "tabnav.homeUrl"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_input, {
                      modelValue: $setup.model.tabnav.homeUrl,
                      "onUpdate:modelValue": _cache[14] || (_cache[14] = ($event) => $setup.model.tabnav.homeUrl = $event),
                      placeholder: "\u8DEF\u7531\u7684\u5B8C\u6574\u5730\u5740"
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 10,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u6700\u5927\u9875\u9762\u6570\u91CF",
                  prop: "tabnav.maxOpenCount"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_input, {
                      modelValue: $setup.model.tabnav.maxOpenCount,
                      "onUpdate:modelValue": _cache[15] || (_cache[15] = ($event) => $setup.model.tabnav.maxOpenCount = $event),
                      modelModifiers: { number: true },
                      placeholder: "\u9ED8\u8BA4 20"
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_divider, { "content-position": "left" }, {
          default: withCtx(() => [
            createTextVNode("\u81EA\u5B9A\u4E49CSS")
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 22,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  prop: "customCss"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_input, {
                      type: "textarea",
                      rows: 5,
                      placeholder: "\u5982\u679C\u9700\u8981\u91CD\u65B0\u67D0\u4E2A\u7EC4\u4EF6\u7684\u6837\u5F0F\uFF0C\u53EF\u4EE5\u5728\u6B64\u5904\u6DFB\u52A0\u8986\u76D6\u7684CSS",
                      modelValue: $setup.model.customCss,
                      "onUpdate:modelValue": _cache[16] || (_cache[16] = ($event) => $setup.model.customCss = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 5,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, { "label-width": "150px" }, {
                  default: withCtx(() => [
                    createVNode(_component_m_button, {
                      type: "primary",
                      onClick: _cache[17] || (_cache[17] = () => $setup.formRef.submit()),
                      icon: "save"
                    }, {
                      default: withCtx(() => [
                        createTextVNode("\u4FDD\u5B58")
                      ]),
                      _: 1
                    }),
                    createVNode(_component_m_button, {
                      type: "info",
                      onClick: _cache[18] || (_cache[18] = () => $setup.formRef.reset()),
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
            })
          ]),
          _: 1
        })
      ]),
      _: 1
    }, 8, ["action", "model", "rules", "disabled", "onSuccess"])
  ]);
}
var ComponentConfig = /* @__PURE__ */ _export_sfc$1(_sfc_main$H, [["render", _sfc_render$H]]);
const _sfc_main$G = {
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
      type: 0,
      code: "Path",
      uploadPath: "",
      tempPath: ""
    });
    const edits = () => {
    };
    const rules = {};
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
const _hoisted_1$b = { style: { "padding": "50px" } };
function _sfc_render$G(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_nm_icon = resolveComponent("nm-icon");
  const _component_el_tooltip = resolveComponent("el-tooltip");
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button = resolveComponent("m-button");
  const _component_m_form = resolveComponent("m-form");
  return openBlock(), createElementBlock("div", _hoisted_1$b, [
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
          labelWidth: "200px",
          prop: "uploadPath"
        }, {
          label: withCtx(() => [
            createVNode(_component_el_tooltip, {
              effect: "dark",
              content: "\u9ED8\u8BA4\u662F\u5E94\u7528\u6839\u76EE\u5F55\u4E0B\u7684Upload\u76EE\u5F55",
              placement: "top"
            }, {
              default: withCtx(() => [
                createVNode(_component_nm_icon, {
                  class: "nm-size-20 nm-text-warning",
                  name: "warning"
                })
              ]),
              _: 1
            }),
            createTextVNode(" \u6587\u4EF6\u4E0A\u4F20\u5B58\u50A8\u6839\u8DEF\u5F84\uFF1A ")
          ]),
          default: withCtx(() => [
            createVNode(_component_el_input, {
              modelValue: $setup.model.uploadPath,
              "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.uploadPath = $event)
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }),
        createVNode(_component_el_form_item, {
          labelWidth: "200px",
          prop: "tempPath"
        }, {
          label: withCtx(() => [
            createVNode(_component_el_tooltip, {
              effect: "dark",
              content: "\u9ED8\u8BA4\u662F\u5E94\u7528\u6839\u76EE\u5F55\u4E0B\u7684Temp\u76EE\u5F55",
              placement: "top"
            }, {
              default: withCtx(() => [
                createVNode(_component_nm_icon, {
                  class: "nm-size-20 nm-text-warning",
                  name: "warning"
                })
              ]),
              _: 1
            }),
            createTextVNode(" \u4E34\u65F6\u6587\u4EF6\u5B58\u50A8\u6839\u8DEF\u5F84\uFF1A ")
          ]),
          default: withCtx(() => [
            createVNode(_component_el_input, {
              modelValue: $setup.model.tempPath,
              "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.tempPath = $event)
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }),
        createVNode(_component_el_form_item, { "label-width": "200px" }, {
          default: withCtx(() => [
            createVNode(_component_m_button, {
              type: "primary",
              onClick: _cache[2] || (_cache[2] = () => $setup.formRef.submit()),
              icon: "save"
            }, {
              default: withCtx(() => [
                createTextVNode("\u4FDD\u5B58")
              ]),
              _: 1
            }),
            createVNode(_component_m_button, {
              type: "info",
              onClick: _cache[3] || (_cache[3] = () => $setup.formRef.reset()),
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
var PathConfig = /* @__PURE__ */ _export_sfc$1(_sfc_main$G, [["render", _sfc_render$G]]);
const _sfc_main$F = {
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
      type: 0,
      code: "Auth",
      verifyCode: false,
      validate: true,
      button: true,
      singleAccount: true,
      auditing: true,
      jwt: {
        key: "",
        issuer: "",
        audience: "",
        expires: 120,
        refreshTokenExpires: 7
      },
      loginMode: {
        userName: true,
        email: false,
        userNameOrEmail: false,
        phone: false,
        weChatScanCode: false,
        qq: false,
        gitHub: false
      }
    });
    const edits = () => {
    };
    const rules = {};
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
const _hoisted_1$a = { style: { "padding": "50px" } };
const _hoisted_2$8 = /* @__PURE__ */ createElementVNode("template", { slot: "append" }, [
  /* @__PURE__ */ createTextVNode("\u5206\u949F")
], -1);
function _sfc_render$F(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_divider = resolveComponent("el-divider");
  const _component_el_switch = resolveComponent("el-switch");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_el_input = resolveComponent("el-input");
  const _component_m_button = resolveComponent("m-button");
  const _component_m_form = resolveComponent("m-form");
  return openBlock(), createElementBlock("div", _hoisted_1$a, [
    createVNode(_component_m_form, {
      ref: "formRef",
      action: $setup.action,
      model: $setup.model,
      rules: $setup.rules,
      disabled: $setup.disabled,
      onSuccess: $setup.handleSuccess
    }, {
      default: withCtx(() => [
        createVNode(_component_el_divider, { "content-position": "left" }, {
          default: withCtx(() => [
            createTextVNode("\u8BA4\u8BC1&\u6388\u6743")
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 5,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u9A8C\u8BC1\u7801",
                  prop: "verifyCode"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.verifyCode,
                      "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.verifyCode = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 5 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u6743\u9650\u9A8C\u8BC1",
                  prop: "validate"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.validate,
                      "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.validate = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 5 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u6309\u94AE\u9A8C\u8BC1",
                  prop: "button"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.button,
                      "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.button = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 5 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u5355\u8D26\u6237\u767B\u5F55",
                  prop: "singleAccount"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.singleAccount,
                      "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.singleAccount = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 5,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u5BA1\u8BA1\u65E5\u5FD7",
                  prop: "auditing"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.auditing,
                      "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.model.auditing = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_divider, { "content-position": "left" }, {
          default: withCtx(() => [
            createTextVNode("JWT\u53C2\u6570")
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 10,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u5BC6\u94A5(Key)",
                  prop: "jwt.key"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_input, {
                      modelValue: $setup.model.jwt.key,
                      "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.model.jwt.key = $event),
                      clearable: ""
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 10 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u6709\u6548\u671F(Expires)",
                  prop: "jwt.expires"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_input, {
                      modelValue: $setup.model.jwt.expires,
                      "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => $setup.model.jwt.expires = $event),
                      modelModifiers: { number: true }
                    }, {
                      default: withCtx(() => [
                        _hoisted_2$8
                      ]),
                      _: 1
                    }, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 10,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u53D1\u884C\u4EBA(Issuer)",
                  prop: "jwt.issuer"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_input, {
                      modelValue: $setup.model.jwt.issuer,
                      "onUpdate:modelValue": _cache[7] || (_cache[7] = ($event) => $setup.model.jwt.issuer = $event),
                      clearable: ""
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 10 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u6D88\u8D39\u8005(Audience)",
                  prop: "jwt.audience"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_input, {
                      modelValue: $setup.model.jwt.audience,
                      "onUpdate:modelValue": _cache[8] || (_cache[8] = ($event) => $setup.model.jwt.audience = $event),
                      clearable: ""
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_divider, { "content-position": "left" }, {
          default: withCtx(() => [
            createTextVNode("\u767B\u5F55\u65B9\u5F0F")
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 5,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u7528\u6237\u540D"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.loginMode.userName,
                      "onUpdate:modelValue": _cache[9] || (_cache[9] = ($event) => $setup.model.loginMode.userName = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 5 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u90AE\u7BB1"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.loginMode.email,
                      "onUpdate:modelValue": _cache[10] || (_cache[10] = ($event) => $setup.model.loginMode.email = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 5 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u7528\u6237\u540D\u6216\u90AE\u7BB1"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.loginMode.userNameOrEmail,
                      "onUpdate:modelValue": _cache[11] || (_cache[11] = ($event) => $setup.model.loginMode.userNameOrEmail = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 5 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u624B\u673A\u53F7"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.loginMode.phone,
                      "onUpdate:modelValue": _cache[12] || (_cache[12] = ($event) => $setup.model.loginMode.phone = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 5,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "\u6DA6\u5DE5\u4F5C\u626B\u7801\u767B\u5F55"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.loginMode.weChatScanCode,
                      "onUpdate:modelValue": _cache[13] || (_cache[13] = ($event) => $setup.model.loginMode.weChatScanCode = $event),
                      disabled: ""
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 5 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  "label-width": "150px",
                  label: "LDAP\u767B\u5F55"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_switch, {
                      modelValue: $setup.model.loginMode.qq,
                      "onUpdate:modelValue": _cache[14] || (_cache[14] = ($event) => $setup.model.loginMode.qq = $event),
                      disabled: ""
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                })
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, {
              span: 5,
              offset: 1
            }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, { "label-width": "150px" }, {
                  default: withCtx(() => [
                    createVNode(_component_m_button, {
                      type: "primary",
                      onClick: _cache[15] || (_cache[15] = () => $setup.formRef.submit()),
                      icon: "save"
                    }, {
                      default: withCtx(() => [
                        createTextVNode("\u4FDD\u5B58")
                      ]),
                      _: 1
                    }),
                    createVNode(_component_m_button, {
                      type: "info",
                      onClick: _cache[16] || (_cache[16] = () => $setup.formRef.reset()),
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
            })
          ]),
          _: 1
        })
      ]),
      _: 1
    }, 8, ["action", "model", "rules", "disabled", "onSuccess"])
  ]);
}
var AuthConfig = /* @__PURE__ */ _export_sfc$1(_sfc_main$F, [["render", _sfc_render$F]]);
var index_vue_vue_type_style_index_0_lang$8 = "";
const _sfc_main$E = {
  components: { SystemConfig, ComponentConfig, PathConfig, AuthConfig },
  setup() {
    const model = reactive({});
    const autoheight = ref(0);
    const handleHeight = () => {
      autoheight.value = window.innerHeight - 100;
      console.log("height", autoheight.value);
    };
    onActivated(() => {
      nextTick(() => {
        handleHeight();
      });
      window.addEventListener("resize", handleHeight);
    });
    onDeactivated(() => {
      window.removeEventListener("resize", handleHeight);
    });
    onBeforeMount(() => {
      window.addEventListener("resize", handleHeight);
    });
    onMounted(() => {
      nextTick(() => {
        handleHeight();
      });
    });
    onBeforeUnmount(() => {
      window.removeEventListener("resize", handleHeight);
    });
    return {
      ...toRefs(model),
      editableTabsValue: "system",
      model,
      autoheight,
      handleHeight
    };
  }
};
function _sfc_render$E(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_icon = resolveComponent("m-icon");
  const _component_system_config = resolveComponent("system-config");
  const _component_el_tab_pane = resolveComponent("el-tab-pane");
  const _component_component_config = resolveComponent("component-config");
  const _component_path_config = resolveComponent("path-config");
  const _component_auth_config = resolveComponent("auth-config");
  const _component_el_tabs = resolveComponent("el-tabs");
  return openBlock(), createBlock(_component_el_tabs, {
    modelValue: $setup.editableTabsValue,
    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.editableTabsValue = $event),
    class: "m-admin-config-library page",
    "tab-position": "left",
    type: "border-card",
    style: normalizeStyle([{ height: $setup.autoheight + "px" }])
  }, {
    default: withCtx(() => [
      createVNode(_component_el_tab_pane, { name: "system" }, {
        label: withCtx(() => [
          createElementVNode("span", null, [
            createVNode(_component_m_icon, { name: "microsoft" }),
            createTextVNode(" \u7CFB\u7EDF\u4FE1\u606F ")
          ])
        ]),
        default: withCtx(() => [
          createVNode(_component_system_config)
        ]),
        _: 1
      }),
      createVNode(_component_el_tab_pane, {
        name: "component",
        lazy: ""
      }, {
        label: withCtx(() => [
          createElementVNode("span", null, [
            createVNode(_component_m_icon, { name: "table" }),
            createTextVNode(" \u524D\u7AEF\u7EC4\u4EF6 ")
          ])
        ]),
        default: withCtx(() => [
          createVNode(_component_component_config)
        ]),
        _: 1
      }),
      createVNode(_component_el_tab_pane, {
        name: "path",
        lazy: ""
      }, {
        label: withCtx(() => [
          createElementVNode("span", null, [
            createVNode(_component_m_icon, { name: "flow" }),
            createTextVNode(" \u901A\u7528\u8DEF\u5F84 ")
          ])
        ]),
        default: withCtx(() => [
          createVNode(_component_path_config)
        ]),
        _: 1
      }),
      createVNode(_component_el_tab_pane, {
        name: "auth",
        lazy: ""
      }, {
        label: withCtx(() => [
          createElementVNode("span", null, [
            createVNode(_component_m_icon, { name: "captcha" }),
            createTextVNode(" \u8BA4\u8BC1\u6388\u6743 ")
          ])
        ]),
        default: withCtx(() => [
          createVNode(_component_auth_config)
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 8, ["modelValue", "style"]);
}
var LibraryPane = /* @__PURE__ */ _export_sfc$1(_sfc_main$E, [["render", _sfc_render$E]]);
var index_vue_vue_type_style_index_0_lang$7 = "";
const _sfc_main$D = {
  components: { ModulePane, LibraryPane },
  setup() {
    const api2 = tpm.api.admin.config;
    const model = reactive({
      tab: "module",
      descriptors: []
    });
    const libraries = computed(() => {
      var libs = model.descriptors.filter((m) => m.type === 0);
      console.log("libs", libs);
      return libs;
    });
    const getDescriptors = () => {
      api2.getDescriptors().then((data) => {
        console.log("getDescriptors -> data", data);
        model.descriptors = data;
      });
    };
    getDescriptors();
    return {
      ...toRefs(model),
      model,
      libraries,
      getDescriptors
    };
  }
};
const _hoisted_1$9 = /* @__PURE__ */ createElementVNode("span", null, "\u4E1A\u52A1\u6A21\u5757", -1);
const _hoisted_2$7 = /* @__PURE__ */ createElementVNode("span", null, "\u57FA\u7840\u7C7B\u5E93", -1);
function _sfc_render$D(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_icon = resolveComponent("m-icon");
  const _component_module_pane = resolveComponent("module-pane");
  const _component_el_tab_pane = resolveComponent("el-tab-pane");
  const _component_library_pane = resolveComponent("library-pane");
  const _component_el_tabs = resolveComponent("el-tabs");
  const _component_m_tabs = resolveComponent("m-tabs");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_tabs, null, {
        default: withCtx(() => [
          createVNode(_component_el_tabs, {
            modelValue: _ctx.tab,
            "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => _ctx.tab = $event)
          }, {
            default: withCtx(() => [
              createVNode(_component_el_tab_pane, {
                name: "module",
                lazy: ""
              }, {
                label: withCtx(() => [
                  createElementVNode("span", null, [
                    createVNode(_component_m_icon, { name: "app" }),
                    _hoisted_1$9
                  ])
                ]),
                default: withCtx(() => [
                  createVNode(_component_module_pane, { ref: "module" }, null, 512)
                ]),
                _: 1
              }),
              createVNode(_component_el_tab_pane, {
                name: "library",
                lazy: ""
              }, {
                label: withCtx(() => [
                  createElementVNode("span", null, [
                    createVNode(_component_m_icon, { name: "drawer" }),
                    _hoisted_2$7
                  ])
                ]),
                default: withCtx(() => [
                  createVNode(_component_library_pane, {
                    ref: "library",
                    descriptors: $setup.libraries
                  }, null, 8, ["descriptors"])
                ]),
                _: 1
              })
            ]),
            _: 1
          }, 8, ["modelValue"])
        ]),
        _: 1
      })
    ]),
    _: 1
  });
}
var component$5 = /* @__PURE__ */ _export_sfc$1(_sfc_main$D, [["render", _sfc_render$D]]);
const page$8 = {
  "name": "admin_config",
  "icon": "wrench",
  "path": "/admin/config",
  "permissions": [
    "admin_config_query_get",
    "admin_config_edit_get",
    "admin_config_update_post",
    "admin_config_logoUrl_get",
    "admin_config_uploadLogo_post",
    "admin_config_descriptors_get"
  ],
  "buttons": {}
};
page$8.component = component$5;
component$5.name = page$8.name;
const name$3 = "admin_dict";
const icon$3 = "dict";
const path$2 = "/admin/dict";
const permissions$2 = [
  "admin_dict_query_get",
  "admin_dictgroup_query_get",
  "admin_dictitem_query_get"
];
const buttons$2 = {
  add: {
    text: "tpm.add",
    code: "admin_dict_add",
    permissions: [
      "admin_dict_add_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "admin_dict_edit",
    permissions: [
      "admin_dict_edit_get",
      "admin_dict_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "admin_dict_delete",
    permissions: [
      "admin_dict_delete_delete"
    ]
  },
  groupAdd: {
    text: "mod.admin.add_group",
    code: "admin_dictgroup_add",
    permissions: [
      "admin_dictgroup_add_post"
    ]
  },
  groupEdit: {
    text: "mod.admin.edit_group",
    code: "admin_dictgroup_edit",
    permissions: [
      "admin_dictgroup_edit_get",
      "admin_dictgroup_update_post"
    ]
  },
  groupRemove: {
    text: "mod.admin.delete_group",
    code: "admin_dictgroup_delete",
    permissions: [
      "admin_dictgroup_delete_delete"
    ]
  },
  itemAdd: {
    text: "mod.admin.add_dict",
    code: "admin_dictitem_add",
    permissions: [
      "admin_dictitem_add_post"
    ]
  },
  itemEdit: {
    text: "mod.admin.edit_dict",
    code: "admin_dictitem_edit",
    permissions: [
      "admin_dictitem_edit_get",
      "admin_dictitem_update_post"
    ]
  },
  itemRemove: {
    text: "mod.admin.delete_dict",
    code: "admin_dictitem_delete",
    permissions: [
      "admin_dictitem_delete_delete"
    ]
  }
};
var page$7 = {
  name: name$3,
  icon: icon$3,
  path: path$2,
  permissions: permissions$2,
  buttons: buttons$2
};
const _sfc_main$C = {
  props: {
    ...withSaveProps,
    groupCode: {
      type: String,
      default: ""
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t } = tpm;
    const api2 = tpm.api.admin.dict;
    const model = reactive({ groupCode: "", name: "", code: "" });
    const rules = computed(() => {
      return {
        name: [{ required: true, message: $t("mod.admin.input_dict_name") }],
        code: [{ required: true, message: $t("mod.admin.input_dict_code") }]
      };
    });
    const nameRef = ref(null);
    const { bind, on: on2 } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = nameRef;
    bind.width = "500px";
    bind.beforeSubmit = () => {
      model.groupCode = props.groupCode;
    };
    return {
      model,
      rules,
      bind,
      on: on2,
      nameRef
    };
  }
};
function _sfc_render$C(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, toHandlers($setup.on)), {
    default: withCtx(() => [
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.name"),
        prop: "name"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_input, {
            ref: "nameRef",
            modelValue: $setup.model.name,
            "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.name = $event)
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"]),
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.code"),
        prop: "code"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_input, {
            modelValue: $setup.model.code,
            "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.code = $event)
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"])
    ]),
    _: 1
  }, 16, ["model", "rules"]);
}
var Save$4 = /* @__PURE__ */ _export_sfc$1(_sfc_main$C, [["render", _sfc_render$C]]);
const _sfc_main$B = {
  emits: ["change"],
  setup(props, { emit }) {
    const { store: store2 } = tpm;
    const currentKey = ref(0);
    const treeData = ref([]);
    const treeRef = ref();
    const adminStore = store2.state.mod.admin;
    const groupCode = computed(() => adminStore.dict.groupCode);
    const dictCode = computed(() => adminStore.dict.dictCode);
    const model = reactive({ groupCode, dictCode });
    let waiting = false;
    const refresh = () => {
      tpm.api.admin.dict.tree(model).then((data) => {
        treeData.value = data;
        if (!waiting) {
          waiting = true;
          nextTick(() => {
            if (treeRef != null && treeRef.value != null) {
              treeRef.value.setCurrentKey(currentKey.value);
            }
          });
        }
      });
    };
    watch([groupCode, dictCode], refresh);
    refresh();
    const handleTreeChange = ({ data }) => {
      if (data != null) {
        currentKey.value = data.id;
        emit("change", data.id);
      }
    };
    const handleTreeAllowDrag = (draggingNode) => {
      return draggingNode.data.id > 0;
    };
    return {
      currentKey,
      treeData,
      treeRef,
      refresh,
      handleTreeChange,
      handleTreeAllowDrag
    };
  }
};
const _hoisted_1$8 = { class: "m-padding-10" };
function _sfc_render$B(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_icon = resolveComponent("m-icon");
  const _component_el_tree = resolveComponent("el-tree");
  return openBlock(), createElementBlock("div", _hoisted_1$8, [
    createVNode(_component_el_tree, {
      ref: "treeRef",
      data: $setup.treeData,
      "check-strictly": true,
      "current-node-key": $setup.currentKey,
      "node-key": "id",
      draggable: "",
      "highlight-current": "",
      "default-expand-all": "",
      "expand-on-click-node": false,
      "allow-drag": $setup.handleTreeAllowDrag,
      onCurrentChange: $setup.handleTreeChange
    }, {
      default: withCtx(({ node, data }) => [
        createElementVNode("span", null, [
          createVNode(_component_m_icon, {
            name: data.item.icon || "folder-o",
            class: "m-margin-r-5"
          }, null, 8, ["name"]),
          createElementVNode("span", null, toDisplayString(node.label), 1)
        ])
      ]),
      _: 1
    }, 8, ["data", "current-node-key", "allow-drag", "onCurrentChange"])
  ]);
}
var Tree = /* @__PURE__ */ _export_sfc$1(_sfc_main$B, [["render", _sfc_render$B]]);
var index_vue_vue_type_style_index_0_lang$6 = "";
const _sfc_main$A = {
  props: {
    ...withSaveProps,
    parentId: {
      type: Number,
      default: 0
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { store: store2, $t } = tpm;
    const api2 = tpm.api.admin.dictItem;
    const model = reactive({ groupCode: "", dictCode: "", parentId: "", name: "", value: "", icon: "", extend: "", sort: 0 });
    const rules = computed(() => {
      return {
        name: [{ required: true, message: $t("mod.admin.input_dict_item_name") }],
        value: [{ required: true, message: $t("mod.admin.input_dict_item_value") }],
        sort: [{ required: true, message: $t("mod.admin.input_dict_item_sort") }]
      };
    });
    const nameRef = ref(null);
    const { bind, on: on2 } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = nameRef;
    bind.width = "700px";
    bind.beforeSubmit = () => {
      const { groupCode, dictCode } = store2.state.mod.admin.dict;
      model.groupCode = groupCode;
      model.dictCode = dictCode;
      model.parentId = props.parentId;
    };
    const toolbars = tpm.components.filter((m) => m.includes("dict-toolbar-"));
    return {
      model,
      rules,
      bind,
      on: on2,
      nameRef,
      toolbars
    };
  }
};
const _hoisted_1$7 = { class: "m-admin-dict-extend" };
const _hoisted_2$6 = { class: "m-admin-dict-extend_toolbar" };
const _hoisted_3$3 = { class: "m-admin-dict-extend_content" };
function _sfc_render$A(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_icon_picker = resolveComponent("m-icon-picker");
  const _component_el_input_number = resolveComponent("el-input-number");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, toHandlers($setup.on)), {
    default: withCtx(() => [
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.name"),
        prop: "name"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_input, {
            ref: "nameRef",
            modelValue: $setup.model.name,
            "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.name = $event),
            clearable: ""
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"]),
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.value"),
        prop: "value"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_input, {
            modelValue: $setup.model.value,
            "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.value = $event),
            clearable: ""
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"]),
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.icon"),
        prop: "icon"
      }, {
        default: withCtx(() => [
          createVNode(_component_m_icon_picker, {
            modelValue: $setup.model.icon,
            "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.icon = $event)
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"]),
      createVNode(_component_el_form_item, {
        label: _ctx.$t("mod.admin.extend_data"),
        prop: "extend"
      }, {
        default: withCtx(() => [
          createElementVNode("div", _hoisted_1$7, [
            createElementVNode("div", _hoisted_2$6, [
              (openBlock(true), createElementBlock(Fragment, null, renderList($setup.toolbars, (toolbar) => {
                return openBlock(), createBlock(resolveDynamicComponent(toolbar), {
                  key: toolbar,
                  modelValue: $setup.model.extend,
                  "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.extend = $event)
                }, null, 8, ["modelValue"]);
              }), 128))
            ]),
            createElementVNode("div", _hoisted_3$3, [
              createVNode(_component_el_input, {
                modelValue: $setup.model.extend,
                "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.model.extend = $event),
                type: "textarea",
                autosize: { minRows: 5 }
              }, null, 8, ["modelValue"])
            ])
          ])
        ]),
        _: 1
      }, 8, ["label"]),
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.sort"),
        prop: "sort"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_input_number, {
            modelValue: $setup.model.sort,
            "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.model.sort = $event)
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"])
    ]),
    _: 1
  }, 16, ["model", "rules"]);
}
var Save$3 = /* @__PURE__ */ _export_sfc$1(_sfc_main$A, [["render", _sfc_render$A]]);
const _sfc_main$z = {
  components: { Save: Save$3 },
  props: {
    parentId: {
      type: Number,
      default: 0
    }
  },
  emits: ["change"],
  setup(props, { emit }) {
    const { store: store2 } = tpm;
    const { query, remove } = tpm.api.admin.dictItem;
    const parentId = toRef(props, "parentId");
    const adminStore = store2.state.mod.admin;
    const groupCode = computed(() => adminStore.dict.groupCode);
    const dictCode = computed(() => adminStore.dict.dictCode);
    const model = reactive({ groupCode, dictCode, parentId, name: "", value: "" });
    const cols = [
      { prop: "id", label: "tpm.id", width: "55", show: false },
      { prop: "name", label: "tpm.name" },
      { prop: "value", label: "tpm.value" },
      { prop: "icon", label: "tpm.icon" },
      { prop: "level", label: "tpm.level" },
      { prop: "sort", label: "tpm.sort" }
    ];
    const list = useList();
    watch([parentId, groupCode, dictCode], () => {
      list.refresh();
    });
    const handleChange = () => {
      list.refresh();
      emit("change");
    };
    return {
      buttons: buttons$2,
      model,
      cols,
      query,
      remove,
      ...list,
      handleChange
    };
  }
};
function _sfc_render$z(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_el_descriptions_item = resolveComponent("el-descriptions-item");
  const _component_el_descriptions = resolveComponent("el-descriptions");
  const _component_m_icon = resolveComponent("m-icon");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  return openBlock(), createElementBlock(Fragment, null, [
    createVNode(_component_m_list, {
      ref: "listRef",
      class: "m-border-none",
      header: false,
      cols: $setup.cols,
      "query-model": $setup.model,
      "query-method": $setup.query
    }, {
      querybar: withCtx(() => [
        createVNode(_component_el_form_item, {
          label: _ctx.$t("tpm.name"),
          prop: "name"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_input, {
              modelValue: $setup.model.name,
              "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.name = $event),
              clearable: ""
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }, 8, ["label"]),
        createVNode(_component_el_form_item, {
          label: _ctx.$t("tpm.code"),
          prop: "code"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_input, {
              modelValue: $setup.model.code,
              "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.code = $event),
              clearable: ""
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }, 8, ["label"])
      ]),
      buttons: withCtx(() => [
        createVNode(_component_m_button_add, {
          code: $setup.buttons.itemAdd.code,
          onClick: _ctx.add
        }, null, 8, ["code", "onClick"])
      ]),
      expand: withCtx(({ row }) => [
        createVNode(_component_el_descriptions, {
          column: 4,
          size: "small"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_descriptions_item, {
              label: _ctx.$t("mod.admin.extend_data"),
              span: 4
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(row.extend), 1)
              ]),
              _: 2
            }, 1032, ["label"]),
            createVNode(_component_el_descriptions_item, {
              label: _ctx.$t("tpm.creator")
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(row.creator), 1)
              ]),
              _: 2
            }, 1032, ["label"]),
            createVNode(_component_el_descriptions_item, {
              label: _ctx.$t("tpm.created_time")
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(row.createdTime), 1)
              ]),
              _: 2
            }, 1032, ["label"]),
            createVNode(_component_el_descriptions_item, {
              label: _ctx.$t("tpm.modifier")
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(row.modifier), 1)
              ]),
              _: 2
            }, 1032, ["label"]),
            createVNode(_component_el_descriptions_item, {
              label: _ctx.$t("tpm.modified_time")
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(row.modifiedTime), 1)
              ]),
              _: 2
            }, 1032, ["label"])
          ]),
          _: 2
        }, 1024)
      ]),
      "col-icon": withCtx(({ row }) => [
        row.icon ? (openBlock(), createBlock(_component_m_icon, {
          key: 0,
          name: row.icon
        }, null, 8, ["name"])) : createCommentVNode("", true)
      ]),
      operation: withCtx(({ row }) => [
        createVNode(_component_m_button_edit, {
          code: $setup.buttons.itemEdit.code,
          onClick: ($event) => _ctx.edit(row),
          onSuccess: $setup.handleChange
        }, null, 8, ["code", "onClick", "onSuccess"]),
        createVNode(_component_m_button_delete, {
          code: $setup.buttons.itemRemove.code,
          action: $setup.remove,
          data: row.id,
          onSuccess: $setup.handleChange
        }, null, 8, ["code", "action", "data", "onSuccess"])
      ]),
      _: 1
    }, 8, ["cols", "query-model", "query-method"]),
    createVNode(_component_save, {
      id: _ctx.selection.id,
      modelValue: _ctx.saveVisible,
      "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => _ctx.saveVisible = $event),
      "parent-id": $props.parentId,
      mode: _ctx.mode,
      onSuccess: $setup.handleChange
    }, null, 8, ["id", "modelValue", "parent-id", "mode", "onSuccess"])
  ], 64);
}
var List$2 = /* @__PURE__ */ _export_sfc$1(_sfc_main$z, [["render", _sfc_render$z]]);
const _sfc_main$y = {
  components: { Tree, List: List$2 },
  setup() {
    const split = ref("250px");
    const parentId = ref(0);
    const treeRef = ref();
    const handleTreeChange = (id2) => {
      parentId.value = id2;
    };
    const handleListChange = () => {
      treeRef.value.refresh();
    };
    return {
      split,
      parentId,
      treeRef,
      handleTreeChange,
      handleListChange
    };
  }
};
function _sfc_render$y(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_tree = resolveComponent("tree");
  const _component_list = resolveComponent("list");
  const _component_m_split = resolveComponent("m-split");
  const _component_m_dialog = resolveComponent("m-dialog");
  return openBlock(), createBlock(_component_m_dialog, {
    title: _ctx.$t("mod.admin.dict_item_manage"),
    icon: "dict",
    width: "80%",
    height: "80%",
    "no-scrollbar": "",
    "no-padding": "",
    "close-on-click-modal": false
  }, {
    default: withCtx(() => [
      createVNode(_component_m_split, {
        modelValue: $setup.split,
        "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.split = $event)
      }, {
        fixed: withCtx(() => [
          createVNode(_component_tree, {
            ref: "treeRef",
            onChange: $setup.handleTreeChange
          }, null, 8, ["onChange"])
        ]),
        auto: withCtx(() => [
          createVNode(_component_list, {
            "parent-id": $setup.parentId,
            onChange: $setup.handleListChange
          }, null, 8, ["parent-id", "onChange"])
        ]),
        _: 1
      }, 8, ["modelValue"])
    ]),
    _: 1
  }, 8, ["title"]);
}
var ItemDialog = /* @__PURE__ */ _export_sfc$1(_sfc_main$y, [["render", _sfc_render$y]]);
const _sfc_main$x = {
  components: { Save: Save$4, ItemDialog },
  props: {
    groupCode: {
      type: String,
      default: ""
    }
  },
  setup(props) {
    const { store: store2 } = tpm;
    const { query, remove } = tpm.api.admin.dict;
    const { groupCode } = toRefs(props);
    const model = reactive({ groupCode, name: "", code: "" });
    const cols = [{ prop: "id", label: "tpm.id", width: "55", show: false }, { prop: "name", label: "tpm.name" }, { prop: "code", label: "tpm.code" }, ...entityBaseCols()];
    const list = useList();
    const showItemDialog = ref(false);
    const openItemDialog = (row) => {
      store2.commit("mod/admin/setDict", { groupCode, dictCode: row.code });
      list.selection.value = row;
      showItemDialog.value = true;
    };
    watch(groupCode, () => {
      list.reset();
    });
    return {
      buttons: buttons$2,
      model,
      cols,
      query,
      remove,
      ...list,
      showItemDialog,
      openItemDialog
    };
  }
};
function _sfc_render$x(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_button = resolveComponent("m-button");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_item_dialog = resolveComponent("item-dialog");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        title: _ctx.$t("mod.admin.dict_list"),
        icon: "list",
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query,
        "query-on-created": false
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("tpm.name"),
            prop: "name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_el_form_item, {
            label: _ctx.$t("tpm.code"),
            prop: "code"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.code,
                "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.code = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button, {
            text: true,
            icon: "cog",
            onClick: ($event) => $setup.openItemDialog(row)
          }, null, 8, ["onClick"]),
          createVNode(_component_m_button_edit, {
            code: $setup.buttons.edit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: _ctx.refresh
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.buttons.remove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: _ctx.refresh
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        _: 1
      }, 8, ["title", "cols", "query-model", "query-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => _ctx.saveVisible = $event),
        "group-code": $props.groupCode,
        mode: _ctx.mode,
        onSuccess: _ctx.refresh
      }, null, 8, ["id", "modelValue", "group-code", "mode", "onSuccess"]),
      createVNode(_component_item_dialog, {
        modelValue: $setup.showItemDialog,
        "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.showItemDialog = $event)
      }, null, 8, ["modelValue"])
    ]),
    _: 1
  });
}
var List$1 = /* @__PURE__ */ _export_sfc$1(_sfc_main$x, [["render", _sfc_render$x]]);
const _sfc_main$w = {
  props: {
    ...withSaveProps
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t } = tpm;
    const api2 = tpm.api.admin.dictGroup;
    const model = reactive({ name: "", code: "", icon: "", remarks: "" });
    const rules = computed(() => {
      return { name: [{ required: true, message: $t("mod.admin.input_dict_group_name") }], code: [{ required: true, message: $t("mod.admin.input_dict_group_code") }] };
    });
    const nameRef = ref(null);
    const { bind, on: on2 } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = nameRef;
    bind.width = "700px";
    return {
      model,
      rules,
      bind,
      on: on2,
      nameRef
    };
  }
};
function _sfc_render$w(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_icon_picker = resolveComponent("m-icon-picker");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, toHandlers($setup.on)), {
    default: withCtx(() => [
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.name"),
        prop: "name"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_input, {
            ref: "nameRef",
            modelValue: $setup.model.name,
            "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.name = $event)
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"]),
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.code"),
        prop: "code"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_input, {
            modelValue: $setup.model.code,
            "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.code = $event)
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"]),
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.icon"),
        prop: "icon"
      }, {
        default: withCtx(() => [
          createVNode(_component_m_icon_picker, {
            modelValue: $setup.model.icon,
            "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.icon = $event)
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"]),
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.remarks"),
        prop: "remarks"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_input, {
            modelValue: $setup.model.remarks,
            "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.remarks = $event),
            type: "textarea",
            rows: 5
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"])
    ]),
    _: 1
  }, 16, ["model", "rules"]);
}
var GroupSave = /* @__PURE__ */ _export_sfc$1(_sfc_main$w, [["render", _sfc_render$w]]);
const _sfc_main$v = {
  components: { List: List$1, GroupSave },
  setup() {
    const current = ref({});
    const listBoxRef = ref();
    const { selection, mode, saveVisible, add, edit } = useList();
    const handleGroupChange = (val, group) => {
      current.value = group;
    };
    const refresh = () => {
      listBoxRef.value.refresh();
    };
    return { page: page$7, current, listBoxRef, selection, mode, saveVisible, add, edit, refresh, handleGroupChange };
  }
};
function _sfc_render$v(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_button = resolveComponent("m-button");
  const _component_m_icon = resolveComponent("m-icon");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_list_box = resolveComponent("m-list-box");
  const _component_m_flex_fixed = resolveComponent("m-flex-fixed");
  const _component_el_descriptions_item = resolveComponent("el-descriptions-item");
  const _component_el_descriptions = resolveComponent("el-descriptions");
  const _component_m_box = resolveComponent("m-box");
  const _component_list = resolveComponent("list");
  const _component_m_flex_auto = resolveComponent("m-flex-auto");
  const _component_m_flex_col = resolveComponent("m-flex-col");
  const _component_m_flex_row = resolveComponent("m-flex-row");
  const _component_group_save = resolveComponent("group-save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_flex_row, null, {
        default: withCtx(() => [
          createVNode(_component_m_flex_fixed, {
            width: "300px",
            class: "m-margin-r-10"
          }, {
            default: withCtx(() => [
              createVNode(_component_m_list_box, {
                ref: "listBoxRef",
                title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
                icon: $setup.page.icon,
                action: _ctx.$tpm.api.admin.dictGroup.query,
                onChange: $setup.handleGroupChange
              }, {
                toolbar: withCtx(() => [
                  createVNode(_component_m_button, {
                    code: $setup.page.buttons.groupAdd.code,
                    icon: "plus",
                    onClick: $setup.add
                  }, null, 8, ["code", "onClick"])
                ]),
                label: withCtx(({ item }) => [
                  createVNode(_component_m_icon, {
                    name: item.icon || "dict",
                    style: { "font-size": "20px", "margin-right": "5px" }
                  }, null, 8, ["name"]),
                  createElementVNode("span", null, toDisplayString(item.name), 1)
                ]),
                action: withCtx(({ item }) => [
                  createVNode(_component_m_button_edit, {
                    text: "",
                    code: $setup.page.buttons.groupEdit.code,
                    onClick: withModifiers(($event) => $setup.edit(item), ["stop"]),
                    onSuccess: $setup.refresh
                  }, null, 8, ["code", "onClick", "onSuccess"]),
                  createVNode(_component_m_button_delete, {
                    text: "",
                    code: $setup.page.buttons.groupRemove.code,
                    action: _ctx.$tpm.api.admin.dictGroup.remove,
                    data: item.id,
                    onSuccess: $setup.refresh
                  }, null, 8, ["code", "action", "data", "onSuccess"])
                ]),
                _: 1
              }, 8, ["title", "icon", "action", "onChange"])
            ]),
            _: 1
          }),
          createVNode(_component_m_flex_auto, null, {
            default: withCtx(() => [
              createVNode(_component_m_flex_col, { class: "m-fill-h" }, {
                default: withCtx(() => [
                  createVNode(_component_m_flex_fixed, null, {
                    default: withCtx(() => [
                      createVNode(_component_m_box, {
                        title: _ctx.$t("mod.admin.group_info"),
                        icon: "preview",
                        "show-collapse": ""
                      }, {
                        default: withCtx(() => [
                          createVNode(_component_el_descriptions, {
                            column: 2,
                            border: ""
                          }, {
                            default: withCtx(() => [
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("tpm.name")
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.name), 1)
                                ]),
                                _: 1
                              }, 8, ["label"]),
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("tpm.code")
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.code), 1)
                                ]),
                                _: 1
                              }, 8, ["label"]),
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("tpm.remarks"),
                                span: 2
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.remarks), 1)
                                ]),
                                _: 1
                              }, 8, ["label"])
                            ]),
                            _: 1
                          })
                        ]),
                        _: 1
                      }, 8, ["title"])
                    ]),
                    _: 1
                  }),
                  createVNode(_component_m_flex_auto, { class: "m-margin-t-10" }, {
                    default: withCtx(() => [
                      createVNode(_component_list, {
                        "group-code": $setup.current.code
                      }, null, 8, ["group-code"])
                    ]),
                    _: 1
                  })
                ]),
                _: 1
              })
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      createVNode(_component_group_save, {
        id: $setup.selection.id,
        modelValue: $setup.saveVisible,
        "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.saveVisible = $event),
        mode: $setup.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$4 = /* @__PURE__ */ _export_sfc$1(_sfc_main$v, [["render", _sfc_render$v]]);
const page$6 = {
  "name": "admin_dict",
  "icon": "dict",
  "path": "/admin/dict",
  "permissions": ["admin_dict_query_get", "admin_dictgroup_query_get", "admin_dictitem_query_get"],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "admin_dict_add",
      "permissions": ["admin_dict_add_post"]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "admin_dict_edit",
      "permissions": ["admin_dict_edit_get", "admin_dict_update_post"]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "admin_dict_delete",
      "permissions": ["admin_dict_delete_delete"]
    },
    "groupAdd": {
      "text": "mod.admin.add_group",
      "code": "admin_dictgroup_add",
      "permissions": ["admin_dictgroup_add_post"]
    },
    "groupEdit": {
      "text": "mod.admin.edit_group",
      "code": "admin_dictgroup_edit",
      "permissions": ["admin_dictgroup_edit_get", "admin_dictgroup_update_post"]
    },
    "groupRemove": {
      "text": "mod.admin.delete_group",
      "code": "admin_dictgroup_delete",
      "permissions": ["admin_dictgroup_delete_delete"]
    },
    "itemAdd": {
      "text": "mod.admin.add_dict",
      "code": "admin_dictitem_add",
      "permissions": ["admin_dictitem_add_post"]
    },
    "itemEdit": {
      "text": "mod.admin.edit_dict",
      "code": "admin_dictitem_edit",
      "permissions": ["admin_dictitem_edit_get", "admin_dictitem_update_post"]
    },
    "itemRemove": {
      "text": "mod.admin.delete_dict",
      "code": "admin_dictitem_delete",
      "permissions": ["admin_dictitem_delete_delete"]
    }
  }
};
page$6.component = component$4;
component$4.name = page$6.name;
const _sfc_main$u = {
  setup() {
    getCurrentInstance().proxy;
    const option2 = {
      title: {
        text: "\u7EDF\u8BA1"
      },
      tooltip: {
        trigger: "axis",
        axisPointer: {
          type: "shadow"
        }
      },
      legend: {},
      grid: {
        left: "3%",
        right: "4%",
        bottom: "3%",
        containLabel: true
      },
      xAxis: {
        type: "value",
        boundaryGap: [0, 0.01]
      },
      yAxis: {
        type: "category",
        data: ["\u5317\u4EAC", "\u5929\u6D25", "\u6CB3\u5317", "\u5C71\u897F", "\u8FBD\u5B81", "\u5409\u6797"]
      },
      series: [
        {
          name: "2011",
          type: "bar",
          data: [18203, 23489, 29034, 104970, 131744, 630230]
        },
        {
          name: "2012",
          type: "bar",
          data: [19325, 23438, 31e3, 121594, 134141, 681807]
        }
      ]
    };
    return {
      option: option2
    };
  }
};
function _sfc_render$u(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_v_chart = resolveComponent("v-chart");
  const _component_m_box = resolveComponent("m-box");
  return openBlock(), createBlock(_component_m_box, {
    title: "\u62A5\u8868\u4F7F\u7528\u7387",
    icon: "chart-pie-fill"
  }, {
    default: withCtx(() => [
      createVNode(_component_v_chart, {
        option: $setup.option,
        class: "m-fill-h"
      }, null, 8, ["option"])
    ]),
    _: 1
  });
}
var Chart1 = /* @__PURE__ */ _export_sfc$1(_sfc_main$u, [["render", _sfc_render$u]]);
const _sfc_main$t = {
  setup() {
    getCurrentInstance().proxy;
    let dataMap = {};
    const dataFormatter = (obj) => {
      var pList = ["\u5317\u4EAC", "\u5929\u6D25", "\u6CB3\u5317", "\u5C71\u897F", "\u8FBD\u5B81", "\u5409\u6797", "\u6C5F\u82CF", "\u6D59\u6C5F", "\u5B89\u5FBD", "\u5C71\u4E1C", "\u6CB3\u5357", "\u6E56\u5317", "\u6E56\u5357", "\u5E7F\u4E1C", "\u91CD\u5E86", "\u56DB\u5DDD", "\u8D35\u5DDE", "\u4E91\u5357", "\u9655\u897F", "\u7518\u8083", "\u9752\u6D77", "\u65B0\u7586"];
      var temp;
      for (var year = 2002; year <= 2011; year++) {
        var max = 0;
        var sum = 0;
        temp = obj[year];
        for (var i = 0, l = temp.length; i < l; i++) {
          max = Math.max(max, temp[i]);
          sum += temp[i];
          obj[year][i] = {
            name: pList[i],
            value: temp[i]
          };
        }
        obj[year + "max"] = Math.floor(max / 100) * 100;
        obj[year + "sum"] = sum;
      }
      return obj;
    };
    dataMap.dataGDP = dataFormatter({
      2011: [16251.93, 11307.28, 24515.76, 11237.55, 14359.88, 22226.7, 10568.83, 12582, 19195.69, 49110.27, 32318.85, 15300.65, 17560.18, 11702.82, 45361.85, 26931.03, 19632.26, 19669.56, 53210.28, 11720.87, 2522.66, 10011.37, 21026.68, 5701.84, 8893.12, 605.83, 12512.3, 5020.37, 1670.44, 2102.21, 6610.05],
      2010: [14113.58, 9224.46, 20394.26, 9200.86, 11672, 18457.27, 8667.58, 10368.6, 17165.98, 41425.48, 27722.31, 12359.33, 14737.12, 9451.26, 39169.92, 23092.36, 15967.61, 16037.96, 46013.06, 9569.85, 2064.5, 7925.58, 17185.48, 4602.16, 7224.18, 507.46, 10123.48, 4120.75, 1350.43, 1689.65, 5437.47],
      2009: [12153.03, 7521.85, 17235.48, 7358.31, 9740.25, 15212.49, 7278.75, 8587, 15046.45, 34457.3, 22990.35, 10062.82, 12236.53, 7655.18, 33896.65, 19480.46, 12961.1, 13059.69, 39482.56, 7759.16, 1654.21, 6530.01, 14151.28, 3912.68, 6169.75, 441.36, 8169.8, 3387.56, 1081.27, 1353.31, 4277.05],
      2008: [11115, 6719.01, 16011.97, 7315.4, 8496.2, 13668.58, 6426.1, 8314.37, 14069.87, 30981.98, 21462.69, 8851.66, 10823.01, 6971.05, 30933.28, 18018.53, 11328.92, 11555, 36796.71, 7021, 1503.06, 5793.66, 12601.23, 3561.56, 5692.12, 394.85, 7314.58, 3166.82, 1018.62, 1203.92, 4183.21],
      2007: [9846.81, 5252.76, 13607.32, 6024.45, 6423.18, 11164.3, 5284.69, 7104, 12494.01, 26018.48, 18753.73, 7360.92, 9248.53, 5800.25, 25776.91, 15012.46, 9333.4, 9439.6, 31777.01, 5823.41, 1254.17, 4676.13, 10562.39, 2884.11, 4772.52, 341.43, 5757.29, 2703.98, 797.35, 919.11, 3523.16],
      2006: [8117.78, 4462.74, 11467.6, 4878.61, 4944.25, 9304.52, 4275.12, 6211.8, 10572.24, 21742.05, 15718.47, 6112.5, 7583.85, 4820.53, 21900.19, 12362.79, 7617.47, 7688.67, 26587.76, 4746.16, 1065.67, 3907.23, 8690.24, 2338.98, 3988.14, 290.76, 4743.61, 2277.35, 648.5, 725.9, 3045.26],
      2005: [6969.52, 3905.64, 10012.11, 4230.53, 3905.03, 8047.26, 3620.27, 5513.7, 9247.66, 18598.69, 13417.68, 5350.17, 6554.69, 4056.76, 18366.87, 10587.42, 6590.19, 6596.1, 22557.37, 3984.1, 918.75, 3467.72, 7385.1, 2005.42, 3462.73, 248.8, 3933.72, 1933.98, 543.32, 612.61, 2604.19],
      2004: [6033.21, 3110.97, 8477.63, 3571.37, 3041.07, 6672, 3122.01, 4750.6, 8072.83, 15003.6, 11648.7, 4759.3, 5763.35, 3456.7, 15021.84, 8553.79, 5633.24, 5641.94, 18864.62, 3433.5, 819.66, 3034.58, 6379.63, 1677.8, 3081.91, 220.34, 3175.58, 1688.49, 466.1, 537.11, 2209.09],
      2003: [5007.21, 2578.03, 6921.29, 2855.23, 2388.38, 6002.54, 2662.08, 4057.4, 6694.23, 12442.87, 9705.02, 3923.11, 4983.67, 2807.41, 12078.15, 6867.7, 4757.45, 4659.99, 15844.64, 2821.11, 713.96, 2555.72, 5333.09, 1426.34, 2556.02, 185.09, 2587.72, 1399.83, 390.2, 445.36, 1886.35],
      2002: [4315, 2150.76, 6018.28, 2324.8, 1940.94, 5458.22, 2348.54, 3637.2, 5741.03, 10606.85, 8003.67, 3519.72, 4467.55, 2450.48, 10275.5, 6035.48, 4212.82, 4151.54, 13502.42, 2523.73, 642.73, 2232.86, 4725.01, 1243.43, 2312.82, 162.04, 2253.39, 1232.03, 340.65, 377.16, 1612.6]
    });
    dataMap.dataPI = dataFormatter({
      2011: [136.27, 159.72, 2905.73, 641.42, 1306.3, 1915.57, 1277.44, 1701.5, 124.94, 3064.78, 1583.04, 2015.31, 1612.24, 1391.07, 3973.85, 3512.24, 2569.3, 2768.03, 2665.2, 2047.23, 659.23, 844.52, 2983.51, 726.22, 1411.01, 74.47, 1220.9, 678.75, 155.08, 184.14, 1139.03],
      2010: [124.36, 145.58, 2562.81, 554.48, 1095.28, 1631.08, 1050.15, 1302.9, 114.15, 2540.1, 1360.56, 1729.02, 1363.67, 1206.98, 3588.28, 3258.09, 2147, 2325.5, 2286.98, 1675.06, 539.83, 685.38, 2482.89, 625.03, 1108.38, 68.72, 988.45, 599.28, 134.92, 159.29, 1078.63],
      2009: [118.29, 128.85, 2207.34, 477.59, 929.6, 1414.9, 980.57, 1154.33, 113.82, 2261.86, 1163.08, 1495.45, 1182.74, 1098.66, 3226.64, 2769.05, 1795.9, 1969.69, 2010.27, 1458.49, 462.19, 606.8, 2240.61, 550.27, 1067.6, 63.88, 789.64, 497.05, 107.4, 127.25, 759.74],
      2008: [112.83, 122.58, 2034.59, 313.58, 907.95, 1302.02, 916.72, 1088.94, 111.8, 2100.11, 1095.96, 1418.09, 1158.17, 1060.38, 3002.65, 2658.78, 1780, 1892.4, 1973.05, 1453.75, 436.04, 575.4, 2216.15, 539.19, 1020.56, 60.62, 753.72, 462.27, 105.57, 118.94, 691.07],
      2007: [101.26, 110.19, 1804.72, 311.97, 762.1, 1133.42, 783.8, 915.38, 101.84, 1816.31, 986.02, 1200.18, 1002.11, 905.77, 2509.14, 2217.66, 1378, 1626.48, 1695.57, 1241.35, 361.07, 482.39, 2032, 446.38, 837.35, 54.89, 592.63, 387.55, 83.41, 97.89, 628.72],
      2006: [88.8, 103.35, 1461.81, 276.77, 634.94, 939.43, 672.76, 750.14, 93.81, 1545.05, 925.1, 1011.03, 865.98, 786.14, 2138.9, 1916.74, 1140.41, 1272.2, 1532.17, 1032.47, 323.48, 386.38, 1595.48, 382.06, 724.4, 50.9, 484.81, 334, 67.55, 79.54, 527.8],
      2005: [88.68, 112.38, 1400, 262.42, 589.56, 882.41, 625.61, 684.6, 90.26, 1461.51, 892.83, 966.5, 827.36, 727.37, 1963.51, 1892.01, 1082.13, 1100.65, 1428.27, 912.5, 300.75, 463.4, 1481.14, 368.94, 661.69, 48.04, 435.77, 308.06, 65.34, 72.07, 509.99],
      2004: [87.36, 105.28, 1370.43, 276.3, 522.8, 798.43, 568.69, 605.79, 83.45, 1367.58, 814.1, 950.5, 786.84, 664.5, 1778.45, 1649.29, 1020.09, 1022.45, 1248.59, 817.88, 278.76, 428.05, 1379.93, 334.5, 607.75, 44.3, 387.88, 286.78, 60.7, 65.33, 461.26],
      2003: [84.11, 89.91, 1064.05, 215.19, 420.1, 615.8, 488.23, 504.8, 81.02, 1162.45, 717.85, 749.4, 692.94, 560, 1480.67, 1198.7, 798.35, 886.47, 1072.91, 658.78, 244.29, 339.06, 1128.61, 298.69, 494.6, 40.7, 302.66, 237.91, 48.47, 55.63, 412.9],
      2002: [82.44, 84.21, 956.84, 197.8, 374.69, 590.2, 446.17, 474.2, 79.68, 1110.44, 685.2, 783.66, 664.78, 535.98, 1390, 1288.36, 707, 847.25, 1015.08, 601.99, 222.89, 317.87, 1047.95, 281.1, 463.44, 39.75, 282.21, 215.51, 47.31, 52.95, 305]
    });
    dataMap.dataSI = dataFormatter({
      2011: [3752.48, 5928.32, 13126.86, 6635.26, 8037.69, 12152.15, 5611.48, 5962.41, 7927.89, 25203.28, 16555.58, 8309.38, 9069.2, 6390.55, 24017.11, 15427.08, 9815.94, 9361.99, 26447.38, 5675.32, 714.5, 5543.04, 11029.13, 2194.33, 3780.32, 208.79, 6935.59, 2377.83, 975.18, 1056.15, 3225.9],
      2010: [3388.38, 4840.23, 10707.68, 5234, 6367.69, 9976.82, 4506.31, 5025.15, 7218.32, 21753.93, 14297.93, 6436.62, 7522.83, 5122.88, 21238.49, 13226.38, 7767.24, 7343.19, 23014.53, 4511.68, 571, 4359.12, 8672.18, 1800.06, 3223.49, 163.92, 5446.1, 1984.97, 744.63, 827.91, 2592.15],
      2009: [2855.55, 3987.84, 8959.83, 3993.8, 5114, 7906.34, 3541.92, 4060.72, 6001.78, 18566.37, 11908.49, 4905.22, 6005.3, 3919.45, 18901.83, 11010.5, 6038.08, 5687.19, 19419.7, 3381.54, 443.43, 3448.77, 6711.87, 1476.62, 2582.53, 136.63, 4236.42, 1527.24, 575.33, 662.32, 1929.59],
      2008: [2626.41, 3709.78, 8701.34, 4242.36, 4376.19, 7158.84, 3097.12, 4319.75, 6085.84, 16993.34, 11567.42, 4198.93, 5318.44, 3554.81, 17571.98, 10259.99, 5082.07, 5028.93, 18502.2, 3037.74, 423.55, 3057.78, 5823.39, 1370.03, 2452.75, 115.56, 3861.12, 1470.34, 557.12, 609.98, 2070.76],
      2007: [2509.4, 2892.53, 7201.88, 3454.49, 3193.67, 5544.14, 2475.45, 3695.58, 5571.06, 14471.26, 10154.25, 3370.96, 4476.42, 2975.53, 14647.53, 8282.83, 4143.06, 3977.72, 16004.61, 2425.29, 364.26, 2368.53, 4648.79, 1124.79, 2038.39, 98.48, 2986.46, 1279.32, 419.03, 455.04, 1647.55],
      2006: [2191.43, 2457.08, 6110.43, 2755.66, 2374.96, 4566.83, 1915.29, 3365.31, 4969.95, 12282.89, 8511.51, 2711.18, 3695.04, 2419.74, 12574.03, 6724.61, 3365.08, 3187.05, 13469.77, 1878.56, 308.62, 1871.65, 3775.14, 967.54, 1705.83, 80.1, 2452.44, 1043.19, 331.91, 351.58, 1459.3],
      2005: [2026.51, 2135.07, 5271.57, 2357.04, 1773.21, 3869.4, 1580.83, 2971.68, 4381.2, 10524.96, 7164.75, 2245.9, 3175.92, 1917.47, 10478.62, 5514.14, 2852.12, 2612.57, 11356.6, 1510.68, 240.83, 1564, 3067.23, 821.16, 1426.42, 63.52, 1951.36, 838.56, 264.61, 281.05, 1164.79],
      2004: [1853.58, 1685.93, 4301.73, 1919.4, 1248.27, 3061.62, 1329.68, 2487.04, 3892.12, 8437.99, 6250.38, 1844.9, 2770.49, 1566.4, 8478.69, 4182.1, 2320.6, 2190.54, 9280.73, 1253.7, 205.6, 1376.91, 2489.4, 681.5, 1281.63, 52.74, 1553.1, 713.3, 211.7, 244.05, 914.47],
      2003: [1487.15, 1337.31, 3417.56, 1463.38, 967.49, 2898.89, 1098.37, 2084.7, 3209.02, 6787.11, 5096.38, 1535.29, 2340.82, 1204.33, 6485.05, 3310.14, 1956.02, 1777.74, 7592.78, 984.08, 175.82, 1135.31, 2014.8, 569.37, 1047.66, 47.64, 1221.17, 572.02, 171.92, 194.27, 719.54],
      2002: [1249.99, 1069.08, 2911.69, 1134.31, 754.78, 2609.85, 943.49, 1843.6, 2622.45, 5604.49, 4090.48, 1337.04, 2036.97, 941.77, 5184.98, 2768.75, 1709.89, 1523.5, 6143.4, 846.89, 148.88, 958.87, 1733.38, 481.96, 934.88, 32.72, 1007.56, 501.69, 144.51, 153.06, 603.15]
    });
    dataMap.dataTI = dataFormatter({
      2011: [12363.18, 5219.24, 8483.17, 3960.87, 5015.89, 8158.98, 3679.91, 4918.09, 11142.86, 20842.21, 14180.23, 4975.96, 6878.74, 3921.2, 17370.89, 7991.72, 7247.02, 7539.54, 24097.7, 3998.33, 1148.93, 3623.81, 7014.04, 2781.29, 3701.79, 322.57, 4355.81, 1963.79, 540.18, 861.92, 2245.12],
      2010: [10600.84, 4238.65, 7123.77, 3412.38, 4209.03, 6849.37, 3111.12, 4040.55, 9833.51, 17131.45, 12063.82, 4193.69, 5850.62, 3121.4, 14343.14, 6607.89, 6053.37, 6369.27, 20711.55, 3383.11, 953.67, 2881.08, 6030.41, 2177.07, 2892.31, 274.82, 3688.93, 1536.5, 470.88, 702.45, 1766.69],
      2009: [9179.19, 3405.16, 6068.31, 2886.92, 3696.65, 5891.25, 2756.26, 3371.95, 8930.85, 13629.07, 9918.78, 3662.15, 5048.49, 2637.07, 11768.18, 5700.91, 5127.12, 5402.81, 18052.59, 2919.13, 748.59, 2474.44, 5198.8, 1885.79, 2519.62, 240.85, 3143.74, 1363.27, 398.54, 563.74, 1587.72],
      2008: [8375.76, 2886.65, 5276.04, 2759.46, 3212.06, 5207.72, 2412.26, 2905.68, 7872.23, 11888.53, 8799.31, 3234.64, 4346.4, 2355.86, 10358.64, 5099.76, 4466.85, 4633.67, 16321.46, 2529.51, 643.47, 2160.48, 4561.69, 1652.34, 2218.81, 218.67, 2699.74, 1234.21, 355.93, 475, 1421.38],
      2007: [7236.15, 2250.04, 4600.72, 2257.99, 2467.41, 4486.74, 2025.44, 2493.04, 6821.11, 9730.91, 7613.46, 2789.78, 3770, 1918.95, 8620.24, 4511.97, 3812.34, 3835.4, 14076.83, 2156.76, 528.84, 1825.21, 3881.6, 1312.94, 1896.78, 188.06, 2178.2, 1037.11, 294.91, 366.18, 1246.89],
      2006: [5837.55, 1902.31, 3895.36, 1846.18, 1934.35, 3798.26, 1687.07, 2096.35, 5508.48, 7914.11, 6281.86, 2390.29, 3022.83, 1614.65, 7187.26, 3721.44, 3111.98, 3229.42, 11585.82, 1835.12, 433.57, 1649.2, 3319.62, 989.38, 1557.91, 159.76, 1806.36, 900.16, 249.04, 294.78, 1058.16],
      2005: [4854.33, 1658.19, 3340.54, 1611.07, 1542.26, 3295.45, 1413.83, 1857.42, 4776.2, 6612.22, 5360.1, 2137.77, 2551.41, 1411.92, 5924.74, 3181.27, 2655.94, 2882.88, 9772.5, 1560.92, 377.17, 1440.32, 2836.73, 815.32, 1374.62, 137.24, 1546.59, 787.36, 213.37, 259.49, 929.41],
      2004: [4092.27, 1319.76, 2805.47, 1375.67, 1270, 2811.95, 1223.64, 1657.77, 4097.26, 5198.03, 4584.22, 1963.9, 2206.02, 1225.8, 4764.7, 2722.4, 2292.55, 2428.95, 8335.3, 1361.92, 335.3, 1229.62, 2510.3, 661.8, 1192.53, 123.3, 1234.6, 688.41, 193.7, 227.73, 833.36],
      2003: [3435.95, 1150.81, 2439.68, 1176.65, 1000.79, 2487.85, 1075.48, 1467.9, 3404.19, 4493.31, 3890.79, 1638.42, 1949.91, 1043.08, 4112.43, 2358.86, 2003.08, 1995.78, 7178.94, 1178.25, 293.85, 1081.35, 2189.68, 558.28, 1013.76, 96.76, 1063.89, 589.91, 169.81, 195.46, 753.91],
      2002: [2982.57, 997.47, 2149.75, 992.69, 811.47, 2258.17, 958.88, 1319.4, 3038.9, 3891.92, 3227.99, 1399.02, 1765.8, 972.73, 3700.52, 1978.37, 1795.93, 1780.79, 6343.94, 1074.85, 270.96, 956.12, 1943.68, 480.37, 914.5, 89.56, 963.62, 514.83, 148.83, 171.14, 704.5]
    });
    dataMap.dataEstate = dataFormatter({
      2011: [1074.93, 411.46, 918.02, 224.91, 384.76, 876.12, 238.61, 492.1, 1019.68, 2747.89, 1677.13, 634.92, 911.16, 402.51, 1838.14, 987, 634.67, 518.04, 3321.31, 465.68, 208.71, 396.28, 620.62, 160.3, 222.31, 17.44, 398.03, 134.25, 29.05, 79.01, 176.22],
      2010: [1006.52, 377.59, 697.79, 192, 309.25, 733.37, 212.32, 391.89, 1002.5, 2600.95, 1618.17, 532.17, 679.03, 340.56, 1622.15, 773.23, 564.41, 464.21, 2813.95, 405.79, 188.33, 266.38, 558.56, 139.64, 223.45, 14.54, 315.95, 110.02, 25.41, 60.53, 143.44],
      2009: [1062.47, 308.73, 612.4, 173.31, 286.65, 605.27, 200.14, 301.18, 1237.56, 2025.39, 1316.84, 497.94, 656.61, 305.9, 1329.59, 622.98, 546.11, 400.11, 2470.63, 348.98, 121.76, 229.09, 548.14, 136.15, 205.14, 13.28, 239.92, 101.37, 23.05, 47.56, 115.23],
      2008: [844.59, 227.88, 513.81, 166.04, 273.3, 500.81, 182.7, 244.47, 939.34, 1626.13, 1052.03, 431.27, 506.98, 281.96, 1104.95, 512.42, 526.88, 340.07, 2057.45, 282.96, 95.6, 191.21, 453.63, 104.81, 195.48, 15.08, 193.27, 93.8, 19.96, 38.85, 89.79],
      2007: [821.5, 183.44, 467.97, 134.12, 191.01, 410.43, 153.03, 225.81, 958.06, 1365.71, 981.42, 366.57, 511.5, 225.96, 953.69, 447.44, 409.65, 301.8, 2029.77, 239.45, 67.19, 196.06, 376.84, 93.19, 193.59, 13.24, 153.98, 83.52, 16.98, 29.49, 91.28],
      2006: [658.3, 156.64, 397.14, 117.01, 136.5, 318.54, 131.01, 194.7, 773.61, 1017.91, 794.41, 281.98, 435.22, 184.67, 786.51, 348.7, 294.73, 254.81, 1722.07, 192.2, 44.45, 158.2, 336.2, 80.24, 165.92, 11.92, 125.2, 73.21, 15.17, 25.53, 68.9],
      2005: [493.73, 122.67, 330.87, 106, 98.75, 256.77, 112.29, 163.34, 715.97, 799.73, 688.86, 231.66, 331.8, 171.88, 664.9, 298.19, 217.17, 215.63, 1430.37, 165.05, 38.2, 143.88, 286.23, 76.38, 148.69, 10.02, 108.62, 63.78, 14.1, 22.97, 55.79],
      2004: [436.11, 106.14, 231.08, 95.1, 73.81, 203.1, 97.93, 137.74, 666.3, 534.17, 587.83, 188.28, 248.44, 167.2, 473.27, 236.44, 204.8, 191.5, 1103.75, 122.52, 30.64, 129.12, 264.3, 68.3, 116.54, 5.8, 95.9, 56.84, 13, 20.78, 53.55],
      2003: [341.88, 92.31, 185.19, 78.73, 61.05, 188.49, 91.99, 127.2, 487.82, 447.47, 473.16, 162.63, 215.84, 138.02, 418.21, 217.58, 176.8, 186.49, 955.66, 100.93, 25.14, 113.69, 231.72, 59.86, 103.79, 4.35, 83.9, 48.09, 11.41, 16.85, 47.84],
      2002: [298.02, 73.04, 140.89, 65.83, 51.48, 130.94, 76.11, 118.7, 384.86, 371.09, 360.63, 139.18, 188.09, 125.27, 371.13, 199.31, 145.17, 165.29, 808.16, 82.83, 21.45, 90.48, 210.82, 53.49, 95.68, 3.42, 77.68, 41.52, 9.74, 13.46, 43.04]
    });
    dataMap.dataFinancial = dataFormatter({
      2011: [2215.41, 756.5, 746.01, 519.32, 447.46, 755.57, 207.65, 370.78, 2277.4, 2600.11, 2730.29, 503.85, 862.41, 357.44, 1640.41, 868.2, 674.57, 501.09, 2916.13, 445.37, 105.24, 704.66, 868.15, 297.27, 456.23, 31.7, 432.11, 145.05, 62.56, 134.18, 288.77],
      2010: [1863.61, 572.99, 615.42, 448.3, 346.44, 639.27, 190.12, 304.59, 1950.96, 2105.92, 2326.58, 396.17, 767.58, 241.49, 1361.45, 697.68, 561.27, 463.16, 2658.76, 384.53, 78.12, 496.56, 654.7, 231.51, 375.08, 27.08, 384.75, 100.54, 54.53, 97.87, 225.2],
      2009: [1603.63, 461.2, 525.67, 361.64, 291.1, 560.2, 180.83, 227.54, 1804.28, 1596.98, 1899.33, 359.6, 612.2, 165.1, 1044.9, 499.92, 479.11, 402.57, 2283.29, 336.82, 65.73, 389.97, 524.63, 194.44, 351.74, 23.17, 336.21, 88.27, 45.63, 75.54, 198.87],
      2008: [1519.19, 368.1, 420.74, 290.91, 219.09, 455.07, 147.24, 177.43, 1414.21, 1298.48, 1653.45, 313.81, 497.65, 130.57, 880.28, 413.83, 393.05, 334.32, 1972.4, 249.01, 47.33, 303.01, 411.14, 151.55, 277.66, 22.42, 287.16, 72.49, 36.54, 64.8, 171.97],
      2007: [1302.77, 288.17, 347.65, 218.73, 148.3, 386.34, 126.03, 155.48, 1209.08, 1054.25, 1251.43, 223.85, 385.84, 101.34, 734.9, 302.31, 337.27, 260.14, 1705.08, 190.73, 34.43, 247.46, 359.11, 122.25, 168.55, 11.51, 231.03, 61.6, 27.67, 51.05, 149.22],
      2006: [982.37, 186.87, 284.04, 169.63, 108.21, 303.41, 100.75, 74.17, 825.2, 653.25, 906.37, 166.01, 243.9, 79.75, 524.94, 219.72, 174.99, 204.72, 899.91, 129.14, 16.37, 213.7, 299.5, 89.43, 143.62, 6.44, 152.25, 50.51, 23.69, 36.99, 99.25],
      2005: [840.2, 147.4, 213.47, 135.07, 72.52, 232.85, 83.63, 35.03, 675.12, 492.4, 686.32, 127.05, 186.12, 69.55, 448.36, 181.74, 127.32, 162.37, 661.81, 91.93, 13.16, 185.18, 262.26, 73.67, 130.5, 7.57, 127.58, 44.73, 20.36, 32.25, 80.34],
      2004: [713.79, 136.97, 209.1, 110.29, 55.89, 188.04, 77.17, 32.2, 612.45, 440.5, 523.49, 94.1, 171, 65.1, 343.37, 170.82, 118.85, 118.64, 602.68, 74, 11.56, 162.38, 236.5, 60.3, 118.4, 5.4, 90.1, 42.99, 19, 27.92, 70.3],
      2003: [635.56, 112.79, 199.87, 118.48, 55.89, 145.38, 73.15, 32.2, 517.97, 392.11, 451.54, 87.45, 150.09, 64.31, 329.71, 165.11, 107.31, 99.35, 534.28, 61.59, 10.68, 147.04, 206.24, 48.01, 105.48, 4.74, 77.87, 42.31, 17.98, 24.8, 64.92],
      2002: [561.91, 76.86, 179.6, 124.1, 48.39, 137.18, 75.45, 31.6, 485.25, 368.86, 347.53, 81.85, 138.28, 76.51, 310.07, 158.77, 96.95, 92.43, 454.65, 35.86, 10.08, 134.52, 183.13, 41.45, 102.39, 2.81, 67.3, 42.08, 16.75, 21.45, 52.18]
    });
    const option2 = {
      title: {
        text: "\u7EDF\u8BA1"
      },
      baseOption: {
        timeline: {
          axisType: "category",
          autoPlay: true,
          playInterval: 1e3,
          data: [
            "2002-01-01",
            "2003-01-01",
            "2004-01-01",
            {
              value: "2005-01-01",
              tooltip: {
                formatter: "{b} \u8FBE\u5230\u4E00\u4E2A\u9AD8\u5EA6"
              },
              symbol: "diamond",
              symbolSize: 16
            },
            "2006-01-01",
            "2007-01-01",
            "2008-01-01",
            "2009-01-01",
            "2010-01-01",
            {
              value: "2011-01-01",
              tooltip: {
                formatter: function(params) {
                  return params.name + "\u8FBE\u5230\u53C8\u4E00\u4E2A\u9AD8\u5EA6";
                }
              },
              symbol: "diamond",
              symbolSize: 18
            }
          ],
          label: {
            formatter: function(s) {
              return new Date(s).getFullYear();
            }
          }
        },
        title: {
          subtext: "\u6570\u636E\u6765\u81EAXXX"
        },
        tooltip: {},
        legend: {
          left: "right",
          data: ["\u52C7\u95EF", "\u9ED1\u72EE", "\u559C\u529B", "\u91D1\u5A01", "\u8138\u8C31", "\u767D\u5564"],
          selected: {
            \u91D1\u5A01: false,
            \u8138\u8C31: false,
            \u767D\u5564: false
          }
        },
        calculable: true,
        grid: {
          top: 80,
          bottom: 100,
          tooltip: {
            trigger: "axis",
            axisPointer: {
              type: "shadow",
              label: {
                show: true,
                formatter: function(params) {
                  return params.value.replace("\n", "");
                }
              }
            }
          }
        },
        xAxis: [
          {
            type: "category",
            axisLabel: { interval: 0 },
            data: [
              "\u5317\u4EAC",
              "\n\u5929\u6D25",
              "\u6CB3\u5317",
              "\n\u5C71\u897F",
              "\u5409\u6797",
              "\n\u9ED1\u9F99\u6C5F",
              "\u4E0A\u6D77",
              "\n\u6C5F\u82CF",
              "\u6D59\u6C5F",
              "\n\u5B89\u5FBD",
              "\u5C71\u4E1C",
              "\n\u6CB3\u5357",
              "\u5E7F\u4E1C",
              "\n\u5E7F\u897F",
              "\u6D77\u5357",
              "\n\u91CD\u5E86",
              "\u56DB\u5DDD",
              "\n\u8D35\u5DDE",
              "\u4E91\u5357",
              "\n\u897F\u85CF",
              "\u9655\u897F",
              "\n\u7518\u8083",
              "\u9752\u6D77"
            ],
            splitLine: { show: false }
          }
        ],
        yAxis: [
          {
            type: "value",
            name: "\u52C7\u95EF\uFF08\u4EBF\u5143\uFF09"
          }
        ],
        series: [
          { name: "\u52C7\u95EF", type: "bar" },
          { name: "\u9ED1\u72EE", type: "bar" },
          { name: "\u559C\u529B", type: "bar" },
          { name: "\u91D1\u5A01", type: "bar" },
          { name: "\u8138\u8C31", type: "bar" },
          { name: "\u767D\u5564", type: "bar" },
          {
            name: "\u52C7\u95EF\u5360\u6BD4",
            type: "pie",
            center: ["75%", "35%"],
            radius: "28%",
            z: 100
          }
        ]
      },
      options: [
        {
          title: { text: "2002\u5168\u56FD\u6307\u6807" },
          series: [
            { data: dataMap.dataGDP["2002"] },
            { data: dataMap.dataFinancial["2002"] },
            { data: dataMap.dataEstate["2002"] },
            { data: dataMap.dataPI["2002"] },
            { data: dataMap.dataSI["2002"] },
            { data: dataMap.dataTI["2002"] },
            {
              data: [
                { name: "\u52C7\u95EF", value: dataMap.dataPI["2002sum"] },
                { name: "\u9ED1\u72EE", value: dataMap.dataSI["2002sum"] },
                { name: "\u559C\u529B", value: dataMap.dataTI["2002sum"] }
              ]
            }
          ]
        },
        {
          title: { text: "2003\u5168\u56FD\u6307\u6807" },
          series: [
            { data: dataMap.dataGDP["2003"] },
            { data: dataMap.dataFinancial["2003"] },
            { data: dataMap.dataEstate["2003"] },
            { data: dataMap.dataPI["2003"] },
            { data: dataMap.dataSI["2003"] },
            { data: dataMap.dataTI["2003"] },
            {
              data: [
                { name: "\u52C7\u95EF", value: dataMap.dataPI["2003sum"] },
                { name: "\u9ED1\u72EE", value: dataMap.dataSI["2003sum"] },
                { name: "\u559C\u529B", value: dataMap.dataTI["2003sum"] }
              ]
            }
          ]
        },
        {
          title: { text: "2004\u5168\u56FD\u6307\u6807" },
          series: [
            { data: dataMap.dataGDP["2004"] },
            { data: dataMap.dataFinancial["2004"] },
            { data: dataMap.dataEstate["2004"] },
            { data: dataMap.dataPI["2004"] },
            { data: dataMap.dataSI["2004"] },
            { data: dataMap.dataTI["2004"] },
            {
              data: [
                { name: "\u52C7\u95EF", value: dataMap.dataPI["2004sum"] },
                { name: "\u9ED1\u72EE", value: dataMap.dataSI["2004sum"] },
                { name: "\u559C\u529B", value: dataMap.dataTI["2004sum"] }
              ]
            }
          ]
        },
        {
          title: { text: "2005\u5168\u56FD\u6307\u6807" },
          series: [
            { data: dataMap.dataGDP["2005"] },
            { data: dataMap.dataFinancial["2005"] },
            { data: dataMap.dataEstate["2005"] },
            { data: dataMap.dataPI["2005"] },
            { data: dataMap.dataSI["2005"] },
            { data: dataMap.dataTI["2005"] },
            {
              data: [
                { name: "\u52C7\u95EF", value: dataMap.dataPI["2005sum"] },
                { name: "\u9ED1\u72EE", value: dataMap.dataSI["2005sum"] },
                { name: "\u559C\u529B", value: dataMap.dataTI["2005sum"] }
              ]
            }
          ]
        },
        {
          title: { text: "2006\u5168\u56FD\u6307\u6807" },
          series: [
            { data: dataMap.dataGDP["2006"] },
            { data: dataMap.dataFinancial["2006"] },
            { data: dataMap.dataEstate["2006"] },
            { data: dataMap.dataPI["2006"] },
            { data: dataMap.dataSI["2006"] },
            { data: dataMap.dataTI["2006"] },
            {
              data: [
                { name: "\u52C7\u95EF", value: dataMap.dataPI["2006sum"] },
                { name: "\u9ED1\u72EE", value: dataMap.dataSI["2006sum"] },
                { name: "\u559C\u529B", value: dataMap.dataTI["2006sum"] }
              ]
            }
          ]
        },
        {
          title: { text: "2007\u5168\u56FD\u6307\u6807" },
          series: [
            { data: dataMap.dataGDP["2007"] },
            { data: dataMap.dataFinancial["2007"] },
            { data: dataMap.dataEstate["2007"] },
            { data: dataMap.dataPI["2007"] },
            { data: dataMap.dataSI["2007"] },
            { data: dataMap.dataTI["2007"] },
            {
              data: [
                { name: "\u52C7\u95EF", value: dataMap.dataPI["2007sum"] },
                { name: "\u9ED1\u72EE", value: dataMap.dataSI["2007sum"] },
                { name: "\u559C\u529B", value: dataMap.dataTI["2007sum"] }
              ]
            }
          ]
        },
        {
          title: { text: "2008\u5168\u56FD\u6307\u6807" },
          series: [
            { data: dataMap.dataGDP["2008"] },
            { data: dataMap.dataFinancial["2008"] },
            { data: dataMap.dataEstate["2008"] },
            { data: dataMap.dataPI["2008"] },
            { data: dataMap.dataSI["2008"] },
            { data: dataMap.dataTI["2008"] },
            {
              data: [
                { name: "\u52C7\u95EF", value: dataMap.dataPI["2008sum"] },
                { name: "\u9ED1\u72EE", value: dataMap.dataSI["2008sum"] },
                { name: "\u559C\u529B", value: dataMap.dataTI["2008sum"] }
              ]
            }
          ]
        },
        {
          title: { text: "2009\u5168\u56FD\u6307\u6807" },
          series: [
            { data: dataMap.dataGDP["2009"] },
            { data: dataMap.dataFinancial["2009"] },
            { data: dataMap.dataEstate["2009"] },
            { data: dataMap.dataPI["2009"] },
            { data: dataMap.dataSI["2009"] },
            { data: dataMap.dataTI["2009"] },
            {
              data: [
                { name: "\u52C7\u95EF", value: dataMap.dataPI["2009sum"] },
                { name: "\u9ED1\u72EE", value: dataMap.dataSI["2009sum"] },
                { name: "\u559C\u529B", value: dataMap.dataTI["2009sum"] }
              ]
            }
          ]
        },
        {
          title: { text: "2010\u5168\u56FD\u6307\u6807" },
          series: [
            { data: dataMap.dataGDP["2010"] },
            { data: dataMap.dataFinancial["2010"] },
            { data: dataMap.dataEstate["2010"] },
            { data: dataMap.dataPI["2010"] },
            { data: dataMap.dataSI["2010"] },
            { data: dataMap.dataTI["2010"] },
            {
              data: [
                { name: "\u52C7\u95EF", value: dataMap.dataPI["2010sum"] },
                { name: "\u9ED1\u72EE", value: dataMap.dataSI["2010sum"] },
                { name: "\u559C\u529B", value: dataMap.dataTI["2010sum"] }
              ]
            }
          ]
        },
        {
          title: { text: "2011\u5168\u56FD\u6307\u6807" },
          series: [
            { data: dataMap.dataGDP["2011"] },
            { data: dataMap.dataFinancial["2011"] },
            { data: dataMap.dataEstate["2011"] },
            { data: dataMap.dataPI["2011"] },
            { data: dataMap.dataSI["2011"] },
            { data: dataMap.dataTI["2011"] },
            {
              data: [
                { name: "\u52C7\u95EF", value: dataMap.dataPI["2011sum"] },
                { name: "\u9ED1\u72EE", value: dataMap.dataSI["2011sum"] },
                { name: "\u559C\u529B", value: dataMap.dataTI["2011sum"] }
              ]
            }
          ]
        }
      ]
    };
    return {
      option: option2,
      dataMap,
      dataFormatter
    };
  }
};
function _sfc_render$t(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_v_chart = resolveComponent("v-chart");
  const _component_m_box = resolveComponent("m-box");
  return openBlock(), createBlock(_component_m_box, {
    title: "\u6307\u6807",
    icon: "chart-pie-fill"
  }, {
    default: withCtx(() => [
      createVNode(_component_v_chart, {
        option: $setup.option,
        class: "m-fill-h"
      }, null, 8, ["option"])
    ]),
    _: 1
  });
}
var Chart2 = /* @__PURE__ */ _export_sfc$1(_sfc_main$t, [["render", _sfc_render$t]]);
const _sfc_main$s = {
  setup() {
    getCurrentInstance().proxy;
    const option2 = {
      title: {
        text: "\u7EDF\u8BA1"
      },
      dataset: [
        {
          source: [
            ["Product", "Sales", "Price", "Year"],
            ["\u65E6\u89D2330\u74F6", 123, 32, 2011],
            ["\u9A6C\u5C14\u65AF\u7EFF\u5927\u542C\u9ED1\u72EE", 231, 14, 2011],
            ["\u7EAF\u751F8\u5EA6500ml\u542C\u624B\u63D0\u6263", 235, 5, 2011],
            ["\u9177\u723D9\u5EA6500ml\u74F6", 341, 25, 2011],
            ["\u52C7\u95EF\u5929\u6DAF500\u7EB8\u7BB1\u9655\u897F\u4E13\u4F9B", 122, 29, 2011],
            ["8\xB0\u52C7\u95EF330\u542C24\u6597\u5730\u4E3B\u7248", 143, 30, 2012],
            ["high-bar\u83E0\u841D\u5851\u5305", 201, 19, 2012],
            ["\u52C7\u95EF\u5929\u6DAF\u9152\u5E97\u4E13\u4F9B", 255, 7, 2012],
            ["\u52C7\u95EF\u5929\u6DAF\u74F6\u88C51*6\u7EB8\u7BB1\u6709\u5956\u7248", 241, 27, 2012],
            ["\u539F\u6C41\u9EA69\u5EA6500ml\u542C6*4\u5851\u819C\u516D\u8FDE\u5305", 102, 34, 2012],
            ["\u8D85\u7EA7\u52C7\u95EF500\u74F6", 153, 28, 2013],
            ["\u8D85\u7EA7\u52C7\u95EF\u5927\u542C", 181, 21, 2013],
            ["\u7EAF\u751F330m\u5361\u7EB8\u542C", 395, 4, 2013],
            ["\u7EAF\u751F500\u542C", 281, 31, 2013],
            ["\u6E05\u723D330\u5C0F\u542C", 92, 39, 2013],
            ["\u6E05\u723D\u5927\u542C", 223, 29, 2014],
            ["\u52C7\u95EF\u5929\u6DAF\u5361\u7EB8\u542C", 211, 17, 2014],
            ["\u52C7\u95EF\u5929\u6DAF500ml\u542C1", 345, 3, 2014],
            ["\u51B08\u534A\u6258\u542C", 211, 35, 2014],
            ["high-bar\u6A59\u5473\u5851\u5305", 72, 24, 2014]
          ]
        },
        {
          transform: {
            type: "filter",
            config: { dimension: "Year", value: 2011 }
          }
        },
        {
          transform: {
            type: "filter",
            config: { dimension: "Year", value: 2012 }
          }
        },
        {
          transform: {
            type: "filter",
            config: { dimension: "Year", value: 2013 }
          }
        }
      ],
      series: [
        {
          type: "pie",
          radius: 50,
          center: ["50%", "25%"],
          datasetIndex: 1
        },
        {
          type: "pie",
          radius: 50,
          center: ["50%", "50%"],
          datasetIndex: 2
        },
        {
          type: "pie",
          radius: 50,
          center: ["50%", "75%"],
          datasetIndex: 3
        }
      ],
      media: [
        {
          query: { minAspectRatio: 1 },
          option: {
            series: [
              { center: ["25%", "50%"] },
              { center: ["50%", "50%"] },
              { center: ["75%", "50%"] }
            ]
          }
        },
        {
          option: {
            series: [
              { center: ["50%", "25%"] },
              { center: ["50%", "50%"] },
              { center: ["50%", "75%"] }
            ]
          }
        }
      ]
    };
    return {
      option: option2
    };
  }
};
function _sfc_render$s(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_v_chart = resolveComponent("v-chart");
  const _component_m_box = resolveComponent("m-box");
  return openBlock(), createBlock(_component_m_box, {
    title: "\u62A5\u8868\u4F7F\u7528\u7387",
    icon: "chart-pie-fill"
  }, {
    default: withCtx(() => [
      createVNode(_component_v_chart, {
        option: $setup.option,
        class: "m-fill-h"
      }, null, 8, ["option"])
    ]),
    _: 1
  });
}
var Chart3 = /* @__PURE__ */ _export_sfc$1(_sfc_main$s, [["render", _sfc_render$s]]);
const _sfc_main$r = {
  setup() {
    getCurrentInstance().proxy;
    const colors = ["#5470C6", "#91CC75", "#EE6666"];
    const option2 = {
      title: {
        text: "\u7EDF\u8BA1"
      },
      color: colors,
      tooltip: {
        trigger: "axis",
        axisPointer: {
          type: "cross"
        }
      },
      grid: {
        right: "20%"
      },
      toolbox: {
        feature: {
          dataView: { show: true, readOnly: false },
          restore: { show: true },
          saveAsImage: { show: true }
        }
      },
      legend: {
        data: ["\u52C7\u95EF", "\u9ED1\u72EE", "\u559C\u529B"]
      },
      xAxis: [
        {
          type: "category",
          axisTick: {
            alignWithLabel: true
          },
          data: ["1\u6708", "2\u6708", "3\u6708", "4\u6708", "5\u6708", "6\u6708", "7\u6708", "8\u6708", "9\u6708", "10\u6708", "11\u6708", "12\u6708"]
        }
      ],
      yAxis: [
        {
          type: "value",
          name: "\u52C7\u95EF",
          position: "right",
          alignTicks: true,
          axisLine: {
            show: true,
            lineStyle: {
              color: colors[0]
            }
          },
          axisLabel: {
            formatter: "{value} KL"
          }
        },
        {
          type: "value",
          name: "\u9ED1\u72EE",
          position: "right",
          alignTicks: true,
          offset: 80,
          axisLine: {
            show: true,
            lineStyle: {
              color: colors[1]
            }
          },
          axisLabel: {
            formatter: "{value} KL"
          }
        },
        {
          type: "value",
          name: "\u91D1\u989D",
          position: "left",
          alignTicks: true,
          axisLine: {
            show: true,
            lineStyle: {
              color: colors[2]
            }
          },
          axisLabel: {
            formatter: "{value} \uFFE5"
          }
        }
      ],
      series: [
        {
          name: "\u52C7\u95EF",
          type: "bar",
          data: [
            434,
            93,
            232,
            23.2,
            25.6,
            76.7,
            5241,
            162.2,
            326,
            200,
            64,
            33
          ]
        },
        {
          name: "\u9ED1\u72EE",
          type: "bar",
          yAxisIndex: 1,
          data: [
            43,
            565,
            123,
            26.4,
            287,
            70.7,
            175.6,
            1254,
            48.7,
            188,
            60,
            23
          ]
        },
        {
          name: "\u559C\u529B",
          type: "line",
          yAxisIndex: 2,
          data: [345, 56, 676, 343, 631, 102, 203, 43543, 23, 16.5, 120, 62]
        }
      ]
    };
    return {
      option: option2
    };
  }
};
function _sfc_render$r(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_v_chart = resolveComponent("v-chart");
  const _component_m_box = resolveComponent("m-box");
  return openBlock(), createBlock(_component_m_box, {
    title: "\u62A5\u8868\u4F7F\u7528\u7387",
    icon: "chart-pie-fill"
  }, {
    default: withCtx(() => [
      createVNode(_component_v_chart, {
        option: $setup.option,
        class: "m-fill-h"
      }, null, 8, ["option"])
    ]),
    _: 1
  });
}
var Chart4 = /* @__PURE__ */ _export_sfc$1(_sfc_main$r, [["render", _sfc_render$r]]);
const _sfc_main$q = {
  components: { Chart1, Chart2, Chart3, Chart4 },
  setup() {
    const message = useMessage();
    const handleMore = () => {
      message.success("\u70B9\u51FB\u4E86\u66F4\u591A");
    };
    return {
      handleMore
    };
  }
};
function _sfc_render$q(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_box_small = resolveComponent("m-box-small");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_chart_1 = resolveComponent("chart-1");
  const _component_chart_2 = resolveComponent("chart-2");
  const _component_chart_3 = resolveComponent("chart-3");
  const _component_chart_4 = resolveComponent("chart-4");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, { scrollbar: "" }, {
    default: withCtx(() => [
      createVNode(_component_el_row, { gutter: 15 }, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 6 }, {
            default: withCtx(() => [
              createVNode(_component_m_box_small, {
                value: "350",
                label: "xxxx",
                unit: "\u7B14",
                color: "#409eff",
                icon: "app",
                more: "",
                onMore: $setup.handleMore
              }, null, 8, ["onMore"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 6 }, {
            default: withCtx(() => [
              createVNode(_component_m_box_small, {
                value: "53000",
                label: "xxxx",
                unit: "\u4E07\u5143",
                color: "#409eff",
                icon: "money",
                more: ""
              })
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 6 }, {
            default: withCtx(() => [
              createVNode(_component_m_box_small, {
                value: "60",
                label: "xxxx",
                unit: "%",
                color: "#409eff",
                icon: "rocket",
                more: ""
              })
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 6 }, {
            default: withCtx(() => [
              createVNode(_component_m_box_small, {
                value: "3021",
                label: "xxxx",
                unit: "\u4E07\u5143",
                color: "#409eff",
                icon: "form",
                more: ""
              })
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      createVNode(_component_el_row, {
        style: { "height": "400px" },
        gutter: 15
      }, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_chart_1)
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_chart_2)
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      createVNode(_component_el_row, {
        style: normalizeStyle([{ "height": "400px" }, [{ marginTop: "15px" }]]),
        gutter: 15
      }, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_chart_3)
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_chart_4)
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  });
}
var component$3 = /* @__PURE__ */ _export_sfc$1(_sfc_main$q, [["render", _sfc_render$q]]);
const page$5 = {
  "name": "admin_home",
  "icon": "home",
  "path": "/admin/home"
};
page$5.component = component$3;
component$3.name = page$5.name;
const _sfc_main$p = {
  props: {
    ...withSaveProps,
    group: {
      type: Object,
      required: true
    },
    parent: {
      type: Object,
      required: true
    }
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { $t } = tpm;
    const cit = getCurrentInstance().proxy;
    const api2 = tpm.api.admin.menu;
    const localesTable = [{ lang: "zh-cn" }, { lang: "en" }];
    const model = reactive({
      groupId: "00000000-0000-0000-0000-000000000000",
      parentId: "00000000-0000-0000-0000-000000000000",
      type: 1,
      icon: "",
      iconColor: "",
      module: "",
      routeName: "",
      routeParams: "",
      routeQuery: "",
      url: "",
      openTarget: 0,
      dialogWidth: "800px",
      dialogHeight: "600px",
      customJs: "",
      show: true,
      sort: 0,
      remarks: "",
      permissions: [],
      buttons: [],
      locales: { "zh-cn": "", en: "" }
    });
    const baseRules = computed(() => {
      return { name: [{ required: true, message: $t("mod.admin.input_menu_name") }] };
    });
    const IsJsonString = (rule, value, callback) => {
      if (!value) {
        callback();
      } else {
        try {
          JSON.parse(value);
          callback();
        } catch {
          callback(new Error($t("mod.admin.input_standard_json")));
        }
      }
    };
    const rules = computed(() => {
      switch (model.type) {
        case 0:
          return baseRules;
        case 1:
          return {
            ...baseRules,
            module: [{ required: true, message: $t("mod.admin.select_module") }],
            routeName: [{ required: true, message: $t("mod.admin.select_page_route") }],
            routeQuery: [{ validator: IsJsonString, trigger: "blur" }]
          };
        case 3:
          return { ...baseRules, customJs: [{ required: true, message: $t("mod.admin.input_custom_script") }] };
        default:
          return { ...baseRules, url: [{ required: true, message: $t("mod.admin.input_link_url") }], openTarget: [{ required: true, message: $t("mod.admin.select_open_target") }] };
      }
    });
    const state2 = reactive({ pages: [], currPage: null });
    const { bind, on: on2 } = useSave({ props, api: api2, model, emit });
    bind.width = "900px";
    bind.labelWidth = "150px";
    bind.closeOnSuccess = false;
    bind.beforeSubmit = () => {
      model.groupId = props.group.id;
      model.parentId = props.parent.id;
      if (model.type === 1) {
        const { permissions: permissions2, buttons: buttons2 } = state2.currPage;
        model.permissions = permissions2;
        if (buttons2) {
          model.buttons = Object.values(buttons2).map((m) => {
            return {
              name: m.text,
              code: m.code,
              icon: m.icon,
              permissions: m.permissions
            };
          });
        }
      }
    };
    const handleModuleSelectChange = (code2, mod2) => {
      if (mod2) {
        state2.pages = mod2.data.pages.filter((m) => !m.noMenu);
        handleRouteChange(model.routeName);
      } else {
        state2.pages = [];
        model.routeName = "";
      }
    };
    const handleRouteChange = (routeName) => {
      let page2 = state2.pages.find((m) => m.name === routeName);
      if (page2) {
        model.name = page2.title;
        model.icon = page2.icon;
        state2.currPage = page2;
        for (let key in model.locales) {
          model.locales[key] = cit.$i18n.messages[key].tpm.routes[page2.name];
        }
      }
    };
    return {
      localesTable,
      model,
      rules,
      bind,
      on: on2,
      state: state2,
      handleModuleSelectChange,
      handleRouteChange
    };
  }
};
function _sfc_render$p(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_el_option = resolveComponent("el-option");
  const _component_el_select = resolveComponent("el-select");
  const _component_m_admin_module_select = resolveComponent("m-admin-module-select");
  const _component_el_table_column = resolveComponent("el-table-column");
  const _component_el_table = resolveComponent("el-table");
  const _component_m_admin_enum_select = resolveComponent("m-admin-enum-select");
  const _component_el_switch = resolveComponent("el-switch");
  const _component_el_input_number = resolveComponent("el-input-number");
  const _component_m_icon_picker = resolveComponent("m-icon-picker");
  const _component_m_flex_auto = resolveComponent("m-flex-auto");
  const _component_el_color_picker = resolveComponent("el-color-picker");
  const _component_m_flex_fixed = resolveComponent("m-flex-fixed");
  const _component_m_flex_row = resolveComponent("m-flex-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, toHandlers($setup.on)), {
    default: withCtx(() => [
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.admin.parent_menu")
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    "model-value": $props.parent.locales[_ctx.$i18n.locale],
                    disabled: ""
                  }, null, 8, ["model-value"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.admin.menu_group")
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    "model-value": $props.group.name,
                    disabled: ""
                  }, null, 8, ["model-value"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.admin.menu_type"),
                prop: "type"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_select, {
                    modelValue: $setup.model.type,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.type = $event),
                    disabled: _ctx.mode === "edit"
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_el_option, {
                        label: _ctx.$t("mod.admin.node"),
                        value: 0
                      }, null, 8, ["label"]),
                      createVNode(_component_el_option, {
                        label: _ctx.$t("mod.admin.route"),
                        value: 1
                      }, null, 8, ["label"]),
                      createVNode(_component_el_option, {
                        label: _ctx.$t("mod.admin.link"),
                        value: 2
                      }, null, 8, ["label"]),
                      createVNode(_component_el_option, {
                        label: _ctx.$t("mod.admin.script"),
                        value: 3
                      }, null, 8, ["label"])
                    ]),
                    _: 1
                  }, 8, ["modelValue", "disabled"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      $setup.model.type === 1 ? (openBlock(), createBlock(_component_el_row, { key: 0 }, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.admin.menu_module"),
                prop: "module"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_admin_module_select, {
                    modelValue: $setup.model.module,
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.module = $event),
                    onChange: $setup.handleModuleSelectChange
                  }, null, 8, ["modelValue", "onChange"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.admin.page_route"),
                prop: "routeName"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_select, {
                    modelValue: $setup.model.routeName,
                    "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.routeName = $event),
                    onChange: $setup.handleRouteChange
                  }, {
                    default: withCtx(() => [
                      (openBlock(true), createElementBlock(Fragment, null, renderList($setup.state.pages, (page2) => {
                        return openBlock(), createBlock(_component_el_option, {
                          key: page2.name,
                          value: page2.name,
                          label: `${_ctx.$t(`tpm.routes.${page2.name}`)}(${page2.name})`
                        }, null, 8, ["value", "label"]);
                      }), 128))
                    ]),
                    _: 1
                  }, 8, ["modelValue", "onChange"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })) : createCommentVNode("", true),
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.admin.menu_name"),
                prop: "name"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_table, {
                    data: $setup.localesTable,
                    border: "",
                    style: { "width": "100%" },
                    size: "small"
                  }, {
                    default: withCtx(() => [
                      createVNode(_component_el_table_column, {
                        label: _ctx.$t("mod.admin.language"),
                        prop: "lang",
                        align: "center",
                        width: "180"
                      }, null, 8, ["label"]),
                      createVNode(_component_el_table_column, {
                        label: _ctx.$t("tpm.name"),
                        align: "center"
                      }, {
                        default: withCtx(({ row }) => [
                          createVNode(_component_el_input, {
                            modelValue: $setup.model.locales[row.lang],
                            "onUpdate:modelValue": ($event) => $setup.model.locales[row.lang] = $event
                          }, null, 8, ["modelValue", "onUpdate:modelValue"])
                        ]),
                        _: 1
                      }, 8, ["label"])
                    ]),
                    _: 1
                  }, 8, ["data"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      $setup.model.type === 2 ? (openBlock(), createElementBlock(Fragment, { key: 1 }, [
        createVNode(_component_el_row, null, {
          default: withCtx(() => [
            createVNode(_component_el_col, { span: 12 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  label: _ctx.$t("mod.admin.link_url"),
                  prop: "url"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_input, {
                      modelValue: $setup.model.url,
                      "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.url = $event),
                      clearable: ""
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                }, 8, ["label"])
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 12 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  label: _ctx.$t("mod.admin.open_target"),
                  prop: "openTarget"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_m_admin_enum_select, {
                      modelValue: $setup.model.openTarget,
                      "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.model.openTarget = $event),
                      module: "admin",
                      name: "MenuOpenTarget"
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                }, 8, ["label"])
              ]),
              _: 1
            })
          ]),
          _: 1
        }),
        $setup.model.openTarget === 2 ? (openBlock(), createBlock(_component_el_row, { key: 0 }, {
          default: withCtx(() => [
            createVNode(_component_el_col, { span: 12 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  label: _ctx.$t("mod.admin.dialog_width"),
                  prop: "dialogWidth"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_input, {
                      modelValue: $setup.model.dialogWidth,
                      "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.model.dialogWidth = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                }, 8, ["label"])
              ]),
              _: 1
            }),
            createVNode(_component_el_col, { span: 12 }, {
              default: withCtx(() => [
                createVNode(_component_el_form_item, {
                  label: _ctx.$t("mod.admin.dialog_height"),
                  prop: "dialogHeight"
                }, {
                  default: withCtx(() => [
                    createVNode(_component_el_input, {
                      modelValue: $setup.model.dialogHeight,
                      "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => $setup.model.dialogHeight = $event)
                    }, null, 8, ["modelValue"])
                  ]),
                  _: 1
                }, 8, ["label"])
              ]),
              _: 1
            })
          ]),
          _: 1
        })) : createCommentVNode("", true)
      ], 64)) : createCommentVNode("", true),
      $setup.model.type === 3 ? (openBlock(), createBlock(_component_el_row, { key: 2 }, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.admin.custom_script"),
                prop: "customJs"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.customJs,
                    "onUpdate:modelValue": _cache[7] || (_cache[7] = ($event) => $setup.model.customJs = $event),
                    type: "textarea",
                    rows: 5
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })) : createCommentVNode("", true),
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("tpm.is_show"),
                prop: "show"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_switch, {
                    modelValue: $setup.model.show,
                    "onUpdate:modelValue": _cache[8] || (_cache[8] = ($event) => $setup.model.show = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("tpm.serial_number"),
                prop: "sort"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input_number, {
                    modelValue: $setup.model.sort,
                    "onUpdate:modelValue": _cache[9] || (_cache[9] = ($event) => $setup.model.sort = $event),
                    "controls-position": "right"
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.admin.left_icon"),
                prop: "icon"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_icon_picker, {
                    modelValue: $setup.model.icon,
                    "onUpdate:modelValue": _cache[10] || (_cache[10] = ($event) => $setup.model.icon = $event)
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.admin.icon_color"),
                prop: "iconColor"
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_flex_row, null, {
                    default: withCtx(() => [
                      createVNode(_component_m_flex_auto, null, {
                        default: withCtx(() => [
                          createVNode(_component_el_input, {
                            modelValue: $setup.model.iconColor,
                            "onUpdate:modelValue": _cache[11] || (_cache[11] = ($event) => $setup.model.iconColor = $event)
                          }, null, 8, ["modelValue"])
                        ]),
                        _: 1
                      }),
                      createVNode(_component_m_flex_fixed, { class: "m-padding-l-3" }, {
                        default: withCtx(() => [
                          createVNode(_component_el_color_picker, {
                            modelValue: $setup.model.iconColor,
                            "onUpdate:modelValue": _cache[12] || (_cache[12] = ($event) => $setup.model.iconColor = $event),
                            "show-alpha": ""
                          }, null, 8, ["modelValue"])
                        ]),
                        _: 1
                      })
                    ]),
                    _: 1
                  })
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      $setup.model.type === 1 ? (openBlock(), createBlock(_component_el_row, { key: 3 }, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.admin.route_query"),
                prop: "routeQuery"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.routeQuery,
                    "onUpdate:modelValue": _cache[13] || (_cache[13] = ($event) => $setup.model.routeQuery = $event),
                    rows: 5,
                    type: "textarea"
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("mod.admin.route_params"),
                prop: "routeParams"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.routeParams,
                    "onUpdate:modelValue": _cache[14] || (_cache[14] = ($event) => $setup.model.routeParams = $event),
                    rows: 5,
                    type: "textarea"
                  }, null, 8, ["modelValue"])
                ]),
                _: 1
              }, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })) : createCommentVNode("", true),
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 24 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("tpm.remarks"),
                prop: "remarks"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.remarks,
                    "onUpdate:modelValue": _cache[15] || (_cache[15] = ($event) => $setup.model.remarks = $event),
                    type: "textarea",
                    rows: 5
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
  }, 16, ["model", "rules"]);
}
var Save$2 = /* @__PURE__ */ _export_sfc$1(_sfc_main$p, [["render", _sfc_render$p]]);
const name$2 = "admin_menu";
const icon$2 = "menu";
const path$1 = "/admin/menu";
const permissions$1 = [
  "admin_menu_tree_get",
  "admin_menu_query_get",
  "admin_menu_UpdateSort_post",
  "admin_menugroup_select_get"
];
const buttons$1 = {
  add: {
    text: "tpm.add",
    code: "admin_menu_add",
    permissions: [
      "admin_menu_add_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "admin_menu_edit",
    permissions: [
      "admin_menu_edit_get",
      "admin_menu_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "admin_menu_remove",
    permissions: [
      "admin_menu_delete_delete"
    ]
  },
  group: {
    text: "mod.admin.manage_group",
    code: "admin_menugroup_manage",
    permissions: [
      "admin_menugroup_query_get"
    ]
  },
  groupAdd: {
    text: "mod.admin.add_group",
    code: "admin_menugroup_add",
    permissions: [
      "admin_menugroup_add_post"
    ]
  },
  groupEdit: {
    text: "mod.admin.edit_group",
    code: "admin_menugroup_edit",
    permissions: [
      "admin_menugroup_edit_get",
      "admin_menugroup_update_post"
    ]
  },
  groupRemove: {
    text: "mod.admin.delete_group",
    code: "admin_menugroup_delete",
    permissions: [
      "admin_menugroup_delete_delete"
    ]
  }
};
var page$4 = {
  name: name$2,
  icon: icon$2,
  path: path$1,
  permissions: permissions$1,
  buttons: buttons$1
};
const _sfc_main$o = {
  components: { Save: Save$2 },
  props: {
    group: {
      type: Object,
      required: true
    },
    parent: {
      type: Object,
      required: true
    }
  },
  emits: ["change"],
  setup(props, { emit }) {
    const { query, remove } = tpm.api.admin.menu;
    const groupId = computed(() => props.group.id);
    const parentId = computed(() => props.parent.id);
    const model = reactive({ groupId, parentId, name: "" });
    const cols = [
      { prop: "id", label: "tpm.id", width: "55", show: false },
      { prop: "name", label: "tpm.name" },
      { prop: "typeName", label: "tpm.type" },
      { prop: "icon", label: "tpm.icon" },
      { prop: "level", label: "tpm.level" },
      { prop: "show", label: "tpm.show" },
      { prop: "sort", label: "tpm.sort" }
    ];
    const list = useList();
    watch([groupId, parentId], () => {
      list.refresh();
    });
    const handleChange = () => {
      emit("change");
    };
    const handleAdd = () => {
      console.log(list.mode);
      list.add();
    };
    return {
      ...list,
      page: page$4,
      model,
      cols,
      query,
      remove,
      handleChange,
      handleAdd
    };
  }
};
function _sfc_render$o(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_el_descriptions_item = resolveComponent("el-descriptions-item");
  const _component_el_descriptions = resolveComponent("el-descriptions");
  const _component_el_tag = resolveComponent("el-tag");
  const _component_m_icon = resolveComponent("m-icon");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  return openBlock(), createElementBlock(Fragment, null, [
    createVNode(_component_m_list, {
      ref: "listRef",
      title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
      icon: $setup.page.icon,
      cols: $setup.cols,
      "query-model": $setup.model,
      "query-method": $setup.query,
      "query-on-created": false
    }, {
      querybar: withCtx(() => [
        createVNode(_component_el_form_item, {
          label: _ctx.$t("tpm.name"),
          prop: "name"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_input, {
              modelValue: $setup.model.name,
              "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.name = $event),
              clearable: ""
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }, 8, ["label"])
      ]),
      buttons: withCtx(() => [
        createVNode(_component_m_button_add, {
          code: $setup.page.buttons.add.code,
          disabled: $props.parent.type !== 0,
          onClick: $setup.handleAdd
        }, null, 8, ["code", "disabled", "onClick"])
      ]),
      expand: withCtx(({ row }) => [
        createVNode(_component_el_descriptions, { column: 4 }, {
          default: withCtx(() => [
            row.type === 1 ? (openBlock(), createElementBlock(Fragment, { key: 0 }, [
              createVNode(_component_el_descriptions_item, {
                label: _ctx.$t("mod.admin.route_name")
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString(row.routeName), 1)
                ]),
                _: 2
              }, 1032, ["label"]),
              createVNode(_component_el_descriptions_item, {
                label: _ctx.$t("mod.admin.route_params")
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString(row.routeParams), 1)
                ]),
                _: 2
              }, 1032, ["label"]),
              createVNode(_component_el_descriptions_item, {
                label: _ctx.$t("mod.admin.route_query")
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString(row.routeQuery), 1)
                ]),
                _: 2
              }, 1032, ["label"])
            ], 64)) : row.type === 2 ? (openBlock(), createElementBlock(Fragment, { key: 1 }, [
              createVNode(_component_el_descriptions_item, {
                label: _ctx.$t("mod.admin.link_url"),
                span: 2
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString(row.url), 1)
                ]),
                _: 2
              }, 1032, ["label"]),
              createVNode(_component_el_descriptions_item, {
                label: _ctx.$t("mod.admin.open_target")
              }, {
                default: withCtx(() => [
                  createTextVNode(toDisplayString(row.openTargetName), 1)
                ]),
                _: 2
              }, 1032, ["label"]),
              row.openTarget === 2 ? (openBlock(), createElementBlock(Fragment, { key: 0 }, [
                createVNode(_component_el_descriptions_item, {
                  label: _ctx.$t("mod.admin.dialog_width")
                }, {
                  default: withCtx(() => [
                    createTextVNode(toDisplayString(row.dialogWidth), 1)
                  ]),
                  _: 2
                }, 1032, ["label"]),
                createVNode(_component_el_descriptions_item, {
                  label: _ctx.$t("mod.admin.dialog_height")
                }, {
                  default: withCtx(() => [
                    createTextVNode(toDisplayString(row.dialogHeight), 1)
                  ]),
                  _: 2
                }, 1032, ["label"])
              ], 64)) : createCommentVNode("", true)
            ], 64)) : row.type === 3 ? (openBlock(), createBlock(_component_el_descriptions_item, {
              key: 2,
              label: _ctx.$t("mod.admin.custom_script"),
              span: 3
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(row.customJs), 1)
              ]),
              _: 2
            }, 1032, ["label"])) : createCommentVNode("", true),
            createVNode(_component_el_descriptions_item, {
              label: _ctx.$t("tpm.creator")
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(row.creator), 1)
              ]),
              _: 2
            }, 1032, ["label"]),
            createVNode(_component_el_descriptions_item, {
              label: _ctx.$t("tpm.created_time")
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(row.createdTime), 1)
              ]),
              _: 2
            }, 1032, ["label"]),
            createVNode(_component_el_descriptions_item, {
              label: _ctx.$t("tpm.modifier")
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(row.modifier), 1)
              ]),
              _: 2
            }, 1032, ["label"]),
            createVNode(_component_el_descriptions_item, {
              label: _ctx.$t("tpm.modified_time")
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(row.modifiedTime), 1)
              ]),
              _: 2
            }, 1032, ["label"]),
            createVNode(_component_el_descriptions_item, {
              label: _ctx.$t("tpm.remarks"),
              span: 4
            }, {
              default: withCtx(() => [
                createTextVNode(toDisplayString(row.remarks), 1)
              ]),
              _: 2
            }, 1032, ["label"])
          ]),
          _: 2
        }, 1024)
      ]),
      "col-name": withCtx(({ row }) => [
        createTextVNode(toDisplayString(JSON.parse(row.localesConfig)[_ctx.$i18n.locale]), 1)
      ]),
      "col-typeName": withCtx(({ row }) => [
        row.type === 0 ? (openBlock(), createBlock(_component_el_tag, { key: 0 }, {
          default: withCtx(() => [
            createTextVNode(toDisplayString(_ctx.$t("mod.admin.node")), 1)
          ]),
          _: 1
        })) : row.type === 1 ? (openBlock(), createBlock(_component_el_tag, {
          key: 1,
          type: "success"
        }, {
          default: withCtx(() => [
            createTextVNode(toDisplayString(_ctx.$t("mod.admin.route")), 1)
          ]),
          _: 1
        })) : row.type === 2 ? (openBlock(), createBlock(_component_el_tag, {
          key: 2,
          type: "warning"
        }, {
          default: withCtx(() => [
            createTextVNode(toDisplayString(_ctx.$t("mod.admin.link")), 1)
          ]),
          _: 1
        })) : (openBlock(), createBlock(_component_el_tag, {
          key: 3,
          type: "warning"
        }, {
          default: withCtx(() => [
            createTextVNode(toDisplayString(_ctx.$t("mod.admin.script")), 1)
          ]),
          _: 1
        }))
      ]),
      "col-icon": withCtx(({ row }) => [
        row.icon ? (openBlock(), createBlock(_component_m_icon, {
          key: 0,
          name: row.icon,
          style: normalizeStyle({ color: row.iconColor })
        }, null, 8, ["name", "style"])) : createCommentVNode("", true)
      ]),
      "col-show": withCtx(({ row }) => [
        createElementVNode("span", null, toDisplayString(_ctx.$t(row.show ? "tpm.yes" : "tpm.no")), 1)
      ]),
      operation: withCtx(({ row }) => [
        createVNode(_component_m_button_edit, {
          code: $setup.page.buttons.edit.code,
          onClick: withModifiers(($event) => _ctx.edit(row), ["stop"]),
          onSuccess: $setup.handleChange
        }, null, 8, ["code", "onClick", "onSuccess"]),
        createVNode(_component_m_button_delete, {
          code: $setup.page.buttons.remove.code,
          action: $setup.remove,
          data: row.id,
          onSuccess: $setup.handleChange
        }, null, 8, ["code", "action", "data", "onSuccess"])
      ]),
      _: 1
    }, 8, ["title", "icon", "cols", "query-model", "query-method"]),
    createVNode(_component_save, {
      id: _ctx.selection.id,
      modelValue: _ctx.saveVisible,
      "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => _ctx.saveVisible = $event),
      group: $props.group,
      parent: $props.parent,
      mode: _ctx.mode,
      onSuccess: $setup.handleChange
    }, null, 8, ["id", "modelValue", "group", "parent", "mode", "onSuccess"])
  ], 64);
}
var List = /* @__PURE__ */ _export_sfc$1(_sfc_main$o, [["render", _sfc_render$o]]);
const _sfc_main$n = {
  props: {
    ...withSaveProps
  },
  emits: ["success"],
  setup(props, { emit }) {
    const {
      $t,
      api: {
        admin: { menuGroup: api2 }
      }
    } = tpm;
    const model = reactive({ name: "", remarks: "" });
    const rules = computed(() => {
      return { name: [{ required: true, message: $t("mod.admin.input_menu_group_name") }] };
    });
    const nameRef = ref(null);
    const { bind, on: on2 } = useSave({ props, api: api2, model, rules, emit });
    bind.autoFocusRef = nameRef;
    bind.width = "700px";
    return {
      model,
      rules,
      bind,
      on: on2,
      nameRef
    };
  }
};
function _sfc_render$n(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, toHandlers($setup.on)), {
    default: withCtx(() => [
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.name"),
        prop: "name"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_input, {
            ref: "nameRef",
            modelValue: $setup.model.name,
            "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.name = $event)
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"]),
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.remarks"),
        prop: "remarks"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_input, {
            modelValue: $setup.model.remarks,
            "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.remarks = $event),
            type: "textarea",
            rows: 5
          }, null, 8, ["modelValue"])
        ]),
        _: 1
      }, 8, ["label"])
    ]),
    _: 1
  }, 16, ["model", "rules"]);
}
var Save$1 = /* @__PURE__ */ _export_sfc$1(_sfc_main$n, [["render", _sfc_render$n]]);
const _sfc_main$m = {
  components: { Save: Save$1 },
  emits: ["change"],
  setup(props, { emit }) {
    const { query, remove } = tpm.api.admin.menuGroup;
    const model = reactive({ name: "" });
    const cols = [
      {
        prop: "id",
        label: "tpm.id",
        width: "55",
        show: false
      },
      {
        prop: "name",
        label: "tpm.name"
      },
      {
        prop: "remarks",
        label: "tpm.remarks"
      },
      ...entityBaseCols()
    ];
    const drawerRef = ref();
    const list = useList();
    const handleChange = () => {
      list.refresh();
      emit("change");
    };
    console.log("drawerRef.offsetHeight");
    console.log(drawerRef);
    return {
      drawerRef,
      buttons: buttons$1,
      model,
      cols,
      query,
      remove,
      ...list,
      handleChange
    };
  }
};
function _sfc_render$m(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_drawer = resolveComponent("m-drawer");
  return openBlock(), createBlock(_component_m_drawer, {
    ref: "drawerRef",
    title: _ctx.$t("mod.admin.manage_group"),
    icon: "list",
    width: "900px"
  }, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        header: false,
        cols: $setup.cols,
        height: "768px",
        "query-model": $setup.model,
        "query-method": $setup.query
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("tpm.name"),
            prop: "name"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.name,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.name = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.buttons.groupAdd.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button_edit, {
            code: $setup.buttons.groupEdit.code,
            onClick: ($event) => _ctx.edit(row),
            onSuccess: $setup.handleChange
          }, null, 8, ["code", "onClick", "onSuccess"]),
          createVNode(_component_m_button_delete, {
            code: $setup.buttons.groupRemove.code,
            action: $setup.remove,
            data: row.id,
            onSuccess: $setup.handleChange
          }, null, 8, ["code", "action", "data", "onSuccess"])
        ]),
        _: 1
      }, 8, ["cols", "query-model", "query-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => _ctx.saveVisible = $event),
        mode: _ctx.mode,
        onSuccess: $setup.handleChange
      }, null, 8, ["id", "modelValue", "mode", "onSuccess"])
    ]),
    _: 1
  }, 8, ["title"]);
}
var Group = /* @__PURE__ */ _export_sfc$1(_sfc_main$m, [["render", _sfc_render$m]]);
var index_vue_vue_type_style_index_0_lang$5 = "";
const _sfc_main$l = {
  components: { List, Group },
  setup() {
    const api2 = tpm.api.admin.menu;
    getCurrentInstance().proxy;
    const group = reactive({ id: "6722c640-4128-c0b5-e1cf-c05581151111", name: "" });
    const parent = reactive({
      id: "6722c640-4128-c0b5-e1cf-c05581151111",
      type: 0,
      locales: null
    });
    const treeData = ref([]);
    const treeRef = ref();
    const listRef = ref();
    const groupSelectRef = ref();
    const showGroup = ref(false);
    let isInit = false;
    let waiting = false;
    const refreshTree = () => {
      api2.getTree({ groupId: group.id }).then((data) => {
        treeData.value = [
          {
            id: "00000000-0000-0000-0000-000000000000",
            label: group.name,
            children: data,
            path: [],
            item: {
              id: "00000000-0000-0000-0000-000000000000",
              icon: "menu",
              type: 0,
              locales: {
                "zh-cn": group.name,
                en: group.name
              }
            }
          }
        ];
        if (!waiting) {
          waiting = true;
          nextTick(() => {
            if (treeRef != null && treeRef.value != null) {
              treeRef.value.setCurrentKey(parent.id);
              if (!isInit) {
                handleTreeChange(treeData.value[0]);
                isInit = true;
              }
            }
          });
        }
      });
    };
    const handleTreeChange = (d) => {
      let { item, id: id2 } = d;
      if (item == null && d.data != null) {
        item = d.data.item;
        parent.id = item.id;
        parent.locales = item.locales;
        parent.type = item.type;
      } else {
        parent.id = id2;
        parent.locales = item.locales;
        parent.type = item.type;
      }
    };
    const handleTreeAllowDrag = (draggingNode) => {
      return draggingNode.data.id != "00000000-0000-0000-0000-000000000000";
    };
    const handleTreeAllowDrop = (draggingNode, dropNode, type) => {
      if (dropNode.data.id === "00000000-0000-0000-0000-000000000000") {
        return false;
      }
      if (type === "inner" && dropNode.data.item.type !== "00000000-0000-0000-0000-000000000000") {
        return false;
      }
      return true;
    };
    const handleTreeNodeDrop = (draggingNode, dropNode, type) => {
      if (treeRef == null || treeRef.value == null) {
        return;
      }
      let root = treeRef.value.getNode(0);
      if (draggingNode.level === dropNode.level) {
        root = dropNode.parent;
      }
      const menus = [];
      resolveSort(root, menus);
      console.log(menus);
      api2.updateSort(menus).then(() => {
        listRef.value.refresh();
      });
    };
    const resolveSort = (node, menus) => {
      node.childNodes.forEach((n, i) => {
        menus.push({
          id: n.key,
          sort: i + 1,
          parentId: node.key
        });
        resolveSort(n, menus);
      });
    };
    const handleGroupChange = () => {
      groupSelectRef.value.refresh();
    };
    watch(group, () => {
      refreshTree();
    });
    return {
      buttons: buttons$1,
      parent,
      group,
      treeData,
      treeRef,
      listRef,
      groupSelectRef,
      showGroup,
      refreshTree,
      handleTreeChange,
      handleTreeAllowDrag,
      handleTreeAllowDrop,
      handleTreeNodeDrop,
      handleGroupChange
    };
  }
};
function _sfc_render$l(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  const _component_m_flex_auto = resolveComponent("m-flex-auto");
  const _component_m_button = resolveComponent("m-button");
  const _component_m_flex_fixed = resolveComponent("m-flex-fixed");
  const _component_m_flex_row = resolveComponent("m-flex-row");
  const _component_m_icon = resolveComponent("m-icon");
  const _component_el_tree = resolveComponent("el-tree");
  const _component_m_scrollbar = resolveComponent("m-scrollbar");
  const _component_m_container = resolveComponent("m-container");
  const _component_m_flex_col = resolveComponent("m-flex-col");
  const _component_m_box = resolveComponent("m-box");
  const _component_list = resolveComponent("list");
  const _component_group = resolveComponent("group");
  return openBlock(), createBlock(_component_m_container, { class: "m-admin-menu" }, {
    default: withCtx(() => [
      createVNode(_component_m_flex_row, null, {
        default: withCtx(() => [
          createVNode(_component_m_flex_fixed, {
            width: "300px",
            class: "m-margin-r-10"
          }, {
            default: withCtx(() => [
              createVNode(_component_m_box, {
                page: "",
                title: _ctx.$t("mod.admin.menu_preview"),
                icon: "menu",
                "no-scrollbar": ""
              }, {
                default: withCtx(() => [
                  createVNode(_component_m_flex_col, null, {
                    default: withCtx(() => [
                      createVNode(_component_m_flex_fixed, { class: "m-text-center m-padding-b-10" }, {
                        default: withCtx(() => [
                          createVNode(_component_m_flex_row, null, {
                            default: withCtx(() => [
                              createVNode(_component_m_flex_auto, null, {
                                default: withCtx(() => [
                                  createVNode(_component_m_select, {
                                    ref: "groupSelectRef",
                                    modelValue: $setup.group.id,
                                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.group.id = $event),
                                    label: $setup.group.name,
                                    "onUpdate:label": _cache[1] || (_cache[1] = ($event) => $setup.group.name = $event),
                                    action: _ctx.$tpm.api.admin.menuGroup.select,
                                    "checked-first": ""
                                  }, null, 8, ["modelValue", "label", "action"])
                                ]),
                                _: 1
                              }),
                              createVNode(_component_m_flex_fixed, null, {
                                default: withCtx(() => [
                                  createVNode(_component_m_button, {
                                    type: "primary",
                                    code: $setup.buttons.group.code,
                                    class: "m-margin-l-5",
                                    onClick: _cache[2] || (_cache[2] = ($event) => $setup.showGroup = true)
                                  }, {
                                    default: withCtx(() => [
                                      createTextVNode(toDisplayString(_ctx.$t("mod.admin.manage_group")), 1)
                                    ]),
                                    _: 1
                                  }, 8, ["code"])
                                ]),
                                _: 1
                              })
                            ]),
                            _: 1
                          })
                        ]),
                        _: 1
                      }),
                      createVNode(_component_m_flex_auto, null, {
                        default: withCtx(() => [
                          createVNode(_component_m_container, null, {
                            default: withCtx(() => [
                              createVNode(_component_m_scrollbar, null, {
                                default: withCtx(() => [
                                  createVNode(_component_el_tree, {
                                    ref: "treeRef",
                                    data: $setup.treeData,
                                    "current-node-key": 0,
                                    "node-key": "id",
                                    draggable: "",
                                    "highlight-current": "",
                                    "default-expand-all": "",
                                    "expand-on-click-node": false,
                                    "allow-drop": $setup.handleTreeAllowDrop,
                                    "allow-drag": $setup.handleTreeAllowDrag,
                                    onCurrentChange: $setup.handleTreeChange,
                                    onNodeDrop: $setup.handleTreeNodeDrop
                                  }, {
                                    default: withCtx(({ node, data }) => [
                                      createElementVNode("span", null, [
                                        createVNode(_component_m_icon, {
                                          name: data.item.icon || "folder-o",
                                          style: normalizeStyle({ color: data.item.iconColor }),
                                          class: "m-margin-r-5"
                                        }, null, 8, ["name", "style"]),
                                        createElementVNode("span", null, toDisplayString(data.item.locales[_ctx.$i18n.locale] || node.label), 1)
                                      ])
                                    ]),
                                    _: 1
                                  }, 8, ["data", "allow-drop", "allow-drag", "onCurrentChange", "onNodeDrop"])
                                ]),
                                _: 1
                              })
                            ]),
                            _: 1
                          })
                        ]),
                        _: 1
                      })
                    ]),
                    _: 1
                  })
                ]),
                _: 1
              }, 8, ["title"])
            ]),
            _: 1
          }),
          createVNode(_component_m_flex_auto, null, {
            default: withCtx(() => [
              createVNode(_component_list, {
                ref: "listRef",
                group: $setup.group,
                parent: $setup.parent,
                onChange: $setup.refreshTree
              }, null, 8, ["group", "parent", "onChange"])
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      createVNode(_component_group, {
        modelValue: $setup.showGroup,
        "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.showGroup = $event),
        onChange: $setup.handleGroupChange
      }, null, 8, ["modelValue", "onChange"])
    ]),
    _: 1
  });
}
var component$2 = /* @__PURE__ */ _export_sfc$1(_sfc_main$l, [["render", _sfc_render$l]]);
const page$3 = {
  "name": "admin_menu",
  "icon": "menu",
  "path": "/admin/menu",
  "permissions": ["admin_menu_tree_get", "admin_menu_query_get", "admin_menu_UpdateSort_post", "admin_menugroup_select_get"],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "admin_menu_add",
      "permissions": ["admin_menu_add_post"]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "admin_menu_edit",
      "permissions": ["admin_menu_edit_get", "admin_menu_update_post"]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "admin_menu_remove",
      "permissions": ["admin_menu_delete_delete"]
    },
    "group": {
      "text": "mod.admin.manage_group",
      "code": "admin_menugroup_manage",
      "permissions": ["admin_menugroup_query_get"]
    },
    "groupAdd": {
      "text": "mod.admin.add_group",
      "code": "admin_menugroup_add",
      "permissions": ["admin_menugroup_add_post"]
    },
    "groupEdit": {
      "text": "mod.admin.edit_group",
      "code": "admin_menugroup_edit",
      "permissions": ["admin_menugroup_edit_get", "admin_menugroup_update_post"]
    },
    "groupRemove": {
      "text": "mod.admin.delete_group",
      "code": "admin_menugroup_delete",
      "permissions": ["admin_menugroup_delete_delete"]
    }
  }
};
page$3.component = component$2;
component$2.name = page$3.name;
var index_vue_vue_type_style_index_0_lang$4 = "";
const _sfc_main$k = {
  props: {
    mod: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const { getPermissions } = tpm.api.admin.module;
    const pages2 = computed(() => {
      console.log(props.mod.pages);
      return props.mod.pages;
    });
    const tableData = computed(() => {
      console.log("props.row.buttons");
      console.log(props.row.buttons);
      return Object.values(props.row.buttons);
    });
    const permissions2 = ref([]);
    watchEffect(() => {
      if (props.mod.code)
        getPermissions({ moduleCode: props.mod.code }).then((data) => {
          permissions2.value = data;
        });
    });
    return {
      pages: pages2,
      tableData,
      permissions: permissions2
    };
  }
};
const _hoisted_1$6 = { class: "m-margin-l-5" };
const _hoisted_2$5 = { class: "m-margin-b-15" };
const _hoisted_3$2 = { class: "m-margin-l-10" };
const _hoisted_4$2 = { class: "m-margin-l-5" };
function _sfc_render$k(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_icon = resolveComponent("m-icon");
  const _component_el_table_column = resolveComponent("el-table-column");
  const _component_el_table = resolveComponent("el-table");
  const _component_el_tab_pane = resolveComponent("el-tab-pane");
  const _component_el_tag = resolveComponent("el-tag");
  const _component_el_tabs = resolveComponent("el-tabs");
  const _component_m_dialog = resolveComponent("m-dialog");
  return openBlock(), createBlock(_component_m_dialog, {
    title: _ctx.$t("mod.admin.module_details"),
    "custom-class": "m-admin-module-detail",
    icon: "component",
    width: "80%",
    height: "70%",
    "no-padding": "",
    "no-scrollbar": ""
  }, {
    default: withCtx(() => [
      createVNode(_component_el_tabs, {
        "tab-position": "left",
        class: "m-fill-h",
        type: "border-card"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_tab_pane, null, {
            label: withCtx(() => [
              createVNode(_component_m_icon, { name: "form" }),
              createElementVNode("span", _hoisted_1$6, toDisplayString(_ctx.$t("mod.admin.page_info")), 1)
            ]),
            default: withCtx(() => [
              createVNode(_component_el_table, {
                data: $setup.pages,
                border: "",
                style: { "width": "100%" },
                height: "100%"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_table_column, { type: "expand" }, {
                    default: withCtx((props) => [
                      createElementVNode("h2", _hoisted_2$5, toDisplayString(_ctx.$t("mod.admin.relation_button")), 1),
                      createVNode(_component_el_table, {
                        data: $setup.tableData,
                        border: "",
                        size: "mini"
                      }, {
                        default: withCtx(() => [
                          createVNode(_component_el_table_column, {
                            label: _ctx.$t("tpm.name"),
                            prop: "text",
                            width: "150",
                            align: "center"
                          }, null, 8, ["label"]),
                          createVNode(_component_el_table_column, {
                            label: _ctx.$t("tpm.code"),
                            prop: "code",
                            width: "300",
                            align: "center"
                          }, null, 8, ["label"]),
                          createVNode(_component_el_table_column, {
                            label: _ctx.$t("mod.admin.bind_permission"),
                            prop: "permissions"
                          }, {
                            default: withCtx(({ row }) => [
                              (openBlock(true), createElementBlock(Fragment, null, renderList(row.permissions, (p) => {
                                return openBlock(), createElementBlock("p", { key: p }, toDisplayString(p), 1);
                              }), 128))
                            ]),
                            _: 1
                          }, 8, ["label"])
                        ]),
                        _: 1
                      }, 8, ["data"])
                    ]),
                    _: 1
                  }),
                  createVNode(_component_el_table_column, {
                    label: _ctx.$t("tpm.title"),
                    prop: "title"
                  }, {
                    default: withCtx(({ row }) => [
                      createTextVNode(toDisplayString(_ctx.$t("tpm.routes." + row.name)), 1)
                    ]),
                    _: 1
                  }, 8, ["label"]),
                  createVNode(_component_el_table_column, {
                    label: _ctx.$t("tpm.icon"),
                    prop: "icon"
                  }, {
                    default: withCtx(({ row }) => [
                      row.icon ? (openBlock(), createBlock(_component_m_icon, {
                        key: 0,
                        name: row.icon
                      }, null, 8, ["name"])) : createCommentVNode("", true),
                      createElementVNode("span", _hoisted_3$2, toDisplayString(row.icon), 1)
                    ]),
                    _: 1
                  }, 8, ["label"]),
                  createVNode(_component_el_table_column, {
                    label: _ctx.$t("mod.admin.route_name"),
                    prop: "name"
                  }, null, 8, ["label"]),
                  createVNode(_component_el_table_column, {
                    label: _ctx.$t("mod.admin.route_path"),
                    prop: "path"
                  }, null, 8, ["label"]),
                  createVNode(_component_el_table_column, {
                    label: _ctx.$t("mod.admin.bind_permission"),
                    prop: "permissions"
                  }, {
                    default: withCtx(({ row }) => [
                      (openBlock(true), createElementBlock(Fragment, null, renderList(row.permissions, (p) => {
                        return openBlock(), createElementBlock("p", { key: p }, toDisplayString(p), 1);
                      }), 128))
                    ]),
                    _: 1
                  }, 8, ["label"])
                ]),
                _: 1
              }, 8, ["data"])
            ]),
            _: 1
          }),
          createVNode(_component_el_tab_pane, null, {
            label: withCtx(() => [
              createVNode(_component_m_icon, { name: "lock" }),
              createElementVNode("span", _hoisted_4$2, toDisplayString(_ctx.$t("mod.admin.permission_info")), 1)
            ]),
            default: withCtx(() => [
              createVNode(_component_el_table, {
                data: $setup.permissions,
                border: "",
                style: { "width": "100%" },
                height: "100%"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_table_column, {
                    label: _ctx.$t("mod.admin.controller"),
                    prop: "controller"
                  }, null, 8, ["label"]),
                  createVNode(_component_el_table_column, {
                    label: _ctx.$t("tpm.operate"),
                    prop: "action"
                  }, null, 8, ["label"]),
                  createVNode(_component_el_table_column, {
                    label: _ctx.$t("mod.admin.http_method"),
                    prop: "httpMethodName"
                  }, null, 8, ["label"]),
                  createVNode(_component_el_table_column, {
                    label: _ctx.$t("mod.admin.permission_code"),
                    prop: "code"
                  }, null, 8, ["label"]),
                  createVNode(_component_el_table_column, {
                    label: _ctx.$t("mod.admin.permission_mode"),
                    prop: "mode"
                  }, {
                    default: withCtx(({ row }) => [
                      row.mode === 0 ? (openBlock(), createBlock(_component_el_tag, {
                        key: 0,
                        type: "warning"
                      }, {
                        default: withCtx(() => [
                          createTextVNode(toDisplayString(_ctx.$t("mod.admin.permission_mode_0")), 1)
                        ]),
                        _: 1
                      })) : row.mode === 1 ? (openBlock(), createBlock(_component_el_tag, { key: 1 }, {
                        default: withCtx(() => [
                          createTextVNode(toDisplayString(_ctx.$t("mod.admin.permission_mode_1")), 1)
                        ]),
                        _: 1
                      })) : createCommentVNode("", true),
                      row.mode === 2 ? (openBlock(), createBlock(_component_el_tag, {
                        key: 2,
                        type: "success"
                      }, {
                        default: withCtx(() => [
                          createTextVNode(toDisplayString(_ctx.$t("mod.admin.permission_mode_2")), 1)
                        ]),
                        _: 1
                      })) : createCommentVNode("", true)
                    ]),
                    _: 1
                  }, 8, ["label"])
                ]),
                _: 1
              }, 8, ["data"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 8, ["title"]);
}
var Detail = /* @__PURE__ */ _export_sfc$1(_sfc_main$k, [["render", _sfc_render$k]]);
const name$1 = "admin_module";
const icon$1 = "module";
const path = "/admin/module";
const permissions = [
  "admin_Module_Permissions_get"
];
var page$2 = {
  name: name$1,
  icon: icon$1,
  path,
  permissions
};
var index_vue_vue_type_style_index_0_lang$3 = "";
const _sfc_main$j = {
  components: { Detail },
  setup() {
    const { modules } = tpm;
    const curr = ref({});
    const showDetail = ref(false);
    const colors = ["#409EFF", "#67C23A", "#E6A23C", "#F56C6C", "#6d3cf7", "#0079de"];
    modules.forEach((m) => {
      m.color = colors[parseInt(Math.random() * colors.length)];
    });
    const openDetail = (mod2) => {
      curr.value = mod2;
      showDetail.value = true;
    };
    return {
      page: page$2,
      modules,
      curr,
      showDetail,
      openDetail
    };
  }
};
const _hoisted_1$5 = { class: "m-margin-l-10 m-font-12" };
const _hoisted_2$4 = { class: "m-margin-lr-5 m-text-primary m-font-14" };
const _hoisted_3$1 = ["onClick"];
const _hoisted_4$1 = { class: "item_wrapper" };
const _hoisted_5$1 = { class: "item_icon" };
const _hoisted_6$1 = ["src"];
const _hoisted_7$1 = { class: "item_info" };
const _hoisted_8$1 = { class: "item_title" };
function _sfc_render$j(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_icon = resolveComponent("m-icon");
  const _component_m_box = resolveComponent("m-box");
  const _component_detail = resolveComponent("detail");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, { class: "m-admin-module" }, {
    default: withCtx(() => [
      createVNode(_component_m_box, {
        page: "",
        icon: "component",
        "show-fullscreen": ""
      }, {
        title: withCtx(() => [
          createElementVNode("span", null, toDisplayString(_ctx.$t(`tpm.routes.${$setup.page.name}`)), 1),
          createElementVNode("span", _hoisted_1$5, [
            createTextVNode(toDisplayString(_ctx.$t("mod.admin.module_total_prefix")), 1),
            createElementVNode("span", _hoisted_2$4, toDisplayString($setup.modules.length), 1),
            createTextVNode(toDisplayString(_ctx.$t("mod.admin.module_total_suffix")), 1)
          ])
        ]),
        default: withCtx(() => [
          (openBlock(true), createElementBlock(Fragment, null, renderList($setup.modules, (mod2) => {
            return openBlock(), createElementBlock("div", {
              key: mod2.code,
              class: "item",
              onClick: ($event) => $setup.openDetail(mod2)
            }, [
              createElementVNode("div", _hoisted_4$1, [
                createElementVNode("div", {
                  class: "item_bar",
                  style: normalizeStyle({ backgroundColor: mod2.color })
                }, null, 4),
                createElementVNode("div", _hoisted_5$1, [
                  createElementVNode("div", {
                    style: normalizeStyle({ backgroundColor: mod2.color })
                  }, [
                    !mod2.icon ? (openBlock(), createBlock(_component_m_icon, {
                      key: 0,
                      name: "app"
                    })) : mod2.icon.startsWith("http://") || mod2.icon.startsWith("https://") ? (openBlock(), createElementBlock("img", {
                      key: 1,
                      src: mod2.icon
                    }, null, 8, _hoisted_6$1)) : (openBlock(), createBlock(_component_m_icon, {
                      key: 2,
                      name: mod2.icon
                    }, null, 8, ["name"]))
                  ], 4)
                ]),
                createElementVNode("div", _hoisted_7$1, [
                  createElementVNode("div", _hoisted_8$1, [
                    createElementVNode("span", null, toDisplayString(mod2.id) + "_" + toDisplayString(mod2.label), 1)
                  ]),
                  createElementVNode("div", null, toDisplayString(_ctx.$t("tpm.code")) + "\uFF1A" + toDisplayString(mod2.code), 1),
                  createElementVNode("div", null, toDisplayString(_ctx.$t("mod.admin.version")) + "\uFF1A" + toDisplayString(mod2.version), 1),
                  createElementVNode("div", null, toDisplayString(_ctx.$t("mod.admin.description")) + "\uFF1A" + toDisplayString(mod2.description), 1)
                ])
              ])
            ], 8, _hoisted_3$1);
          }), 128))
        ]),
        _: 1
      }),
      createVNode(_component_detail, {
        modelValue: $setup.showDetail,
        "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.showDetail = $event),
        mod: $setup.curr
      }, null, 8, ["modelValue", "mod"])
    ]),
    _: 1
  });
}
var component$1 = /* @__PURE__ */ _export_sfc$1(_sfc_main$j, [["render", _sfc_render$j]]);
const page$1 = {
  "name": "admin_module",
  "icon": "module",
  "path": "/admin/module",
  "permissions": ["admin_Module_Permissions_get"]
};
page$1.component = component$1;
component$1.name = page$1.name;
const buttons = {
  add: {
    text: "tpm.add",
    code: "admin_role_add",
    permissions: [
      "admin_role_add_post"
    ]
  },
  edit: {
    text: "tpm.edit",
    code: "admin_role_edit",
    permissions: [
      "admin_role_edit_get",
      "admin_role_update_post"
    ]
  },
  remove: {
    text: "tpm.delete",
    code: "admin_role_delete",
    permissions: [
      "admin_role_delete_delete"
    ]
  },
  bindMenu: {
    text: "mod.admin.menu_bind",
    code: "admin_role_bindmenu",
    permissions: [
      "admin_menu_tree_get",
      "admin_role_QueryBindMenus_get",
      "admin_role_UpdateBindMenus_post"
    ]
  }
};
const _sfc_main$i = {
  props: {
    nodeProps: {
      children: "children",
      label: "label"
    },
    selection: {
      type: Object
    },
    refreshOnCreated: {
      type: Boolean,
      default: true
    }
  },
  emits: ["change"],
  setup(props, { emit }) {
    const { store: store2, $t } = tpm;
    const { getTree, getOrgLevel } = tpm.api.admin.role;
    useMessage();
    const currentKey = ref("00000000-0000-0000-0000-000000000000");
    const treeRef = ref();
    const treeData = ref([]);
    const loading = ref(false);
    const model = reactive({
      defaultkeys: [],
      defaultExpandedCids: []
    });
    const handleTreeChange = (data) => {
      if (data != null) {
        currentKey.value = data.id;
        emit("change", data.data);
        let getNode = treeRef.value.getCheckedNodes();
        if (getNode.length > 0) {
          getNode.forEach((item) => {
            treeRef.value.setChecked(item.id, false);
            treeRef.value.setChecked(data.key, true);
          });
        } else {
          treeRef.value.setChecked(data.key, true);
        }
      }
    };
    const checkClick = (data) => {
      let getNode = treeRef.value.getCheckedNodes();
      if (getNode.length > 0) {
        getNode.forEach((item) => {
          if (data.id == item.id) {
            treeRef.value.setChecked(item.id, true);
          } else {
            treeRef.value.setChecked(item.id, false);
          }
        });
      }
      emit("change", data);
    };
    const loadfirstnode = async (resolve, includes) => {
      loading.value = true;
      const res = await getTree({
        level: 10,
        includes: includes || []
      });
      let orgs = includes == null ? void 0 : includes.map((s) => s.orgId);
      let data = res.filter((m) => m);
      if (orgs.length > 0)
        ;
      loading.value = false;
      console.log("data", data);
      if (props.selection.orgId !== void 0 && props.selection.orgId !== "" && props.selection.orgId !== "undefined") {
        model.defaultkeys.push(props.selection.orgId);
        model.defaultExpandedCids.push(props.selection.orgId);
      }
      if (data && Array.isArray(data)) {
        data.forEach((item) => {
          model.defaultExpandedCids.push(item.id);
        });
      }
      return resolve(data);
    };
    const loadchildnode = async (node, resolve, includes) => {
      let params = {
        level: node.level + 1,
        parentId: node.key || "00000000-0000-0000-0000-000000000000",
        includes: includes || []
      };
      loading.value = true;
      console.log("loadchildnode - > params", params);
      let res = await getOrgLevel(params);
      if (includes.length == 0)
        ;
      if (node.level >= 3) {
        res = [];
      }
      console.log("loadchildnode", res);
      loading.value = false;
      if (res && Array.isArray(res)) {
        res.forEach((item) => {
        });
      }
      return resolve(res);
    };
    const loadNode = (node, resolve) => {
      let profile = store2.state.app.profile;
      let includes = profile.accountRoleOrgs;
      console.log("node.level", node.level);
      console.log("profile", profile);
      console.log("includes", includes);
      if (node.level == 0) {
        loadfirstnode(resolve, includes);
      }
      if (node.level >= 1) {
        loadchildnode(node, resolve, includes);
      }
    };
    watch(props.selection, () => {
      if (props.selection.orgId !== void 0 && props.selection.orgId !== "" && props.selection.orgId !== "undefined") {
        treeRef.value.setChecked(props.selection.orgId, true);
      }
    });
    return {
      ...toRefs(model),
      loading,
      treeRef,
      treeData,
      handleTreeChange,
      checkClick,
      loadNode,
      loadfirstnode,
      loadchildnode
    };
  }
};
function _sfc_render$i(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_icon = resolveComponent("m-icon");
  const _component_el_tree = resolveComponent("el-tree");
  const _directive_loading = resolveDirective("loading");
  return withDirectives((openBlock(), createBlock(_component_el_tree, {
    ref: "treeRef",
    data: $setup.treeData,
    "current-node-key": `00000000-0000-0000-0000-000000000000`,
    props: $props.nodeProps,
    load: $setup.loadNode,
    lazy: "",
    "node-key": "id",
    "highlight-current": "",
    onCurrentChange: $setup.handleTreeChange,
    "expand-on-click-node": false,
    "check-strictly": true,
    "show-checkbox": "",
    onCheck: $setup.checkClick,
    "default-checked-keys": _ctx.defaultkeys,
    "default-expanded-keys": _ctx.defaultExpandedCids
  }, {
    default: withCtx(({ node, data }) => [
      createElementVNode("span", null, [
        createVNode(_component_m_icon, {
          name: "folder-o",
          class: "m-margin-r-5"
        }),
        createElementVNode("span", null, toDisplayString(data.label == null ? data.name : data.label), 1)
      ])
    ]),
    _: 1
  }, 8, ["data", "props", "load", "onCurrentChange", "onCheck", "default-checked-keys", "default-expanded-keys"])), [
    [_directive_loading, $setup.loading]
  ]);
}
var TreePage = /* @__PURE__ */ _export_sfc$1(_sfc_main$i, [["render", _sfc_render$i]]);
const _sfc_main$h = {
  props: withSaveProps,
  emits: ["success"],
  components: { TreePage },
  setup(props, { emit }) {
    const {
      $t,
      api: {
        admin: { role: api2 }
      }
    } = tpm;
    const lableWidth = "120px";
    const model = reactive({
      menuGroupId: "",
      name: "",
      code: "",
      orgId: "",
      orgName: "",
      crmCode: "",
      locked: false,
      remarks: ""
    });
    const rules = computed(() => {
      return {
        menuGroupId: [{ required: true, message: $t("mod.admin.select_menu_group") }],
        name: [{ required: true, message: $t("mod.admin.input_role_name") }],
        code: [{ required: true, message: $t("mod.admin.input_role_code") }]
      };
    });
    const nameRef = ref(null);
    const { bind, on: on2 } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = nameRef;
    bind.width = "1000px";
    const splitRef = ref(0.5);
    const tree = ref();
    const showTree = ref(false);
    const onTreeChange = (data) => {
      model.orgId = data.id;
      model.orgName = data.label;
    };
    const handleOpen = () => {
    };
    watch(model, () => {
      showTree.value = true;
    });
    return {
      model,
      handleOpen,
      rules,
      bind,
      on: on2,
      nameRef,
      splitRef,
      onTreeChange,
      tree,
      showTree,
      lableWidth
    };
  }
};
const _hoisted_1$4 = { style: { "padding": "15px" } };
const _hoisted_2$3 = { style: { "padding": "15px", "background-color": "#f8f8f9" } };
function _sfc_render$h(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_input = resolveComponent("el-input");
  const _component_el_switch = resolveComponent("el-switch");
  const _component_tree_page = resolveComponent("tree-page");
  const _component_m_split = resolveComponent("m-split");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, toHandlers($setup.on), { onOpen: $setup.handleOpen }), {
    default: withCtx(() => [
      createVNode(_component_m_split, {
        modelValue: $setup.splitRef,
        "onUpdate:modelValue": _cache[7] || (_cache[7] = ($event) => $setup.splitRef = $event)
      }, {
        fixed: withCtx(() => [
          createElementVNode("div", _hoisted_1$4, [
            createVNode(_component_el_form_item, {
              label: _ctx.$t("\u8BBF\u95EE\u83DC\u5355\u7EC4"),
              prop: "menuGroupId",
              "label-width": $setup.lableWidth
            }, {
              default: withCtx(() => [
                createVNode(_component_m_select, {
                  modelValue: $setup.model.menuGroupId,
                  "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.menuGroupId = $event),
                  action: _ctx.$tpm.api.admin.menuGroup.select,
                  "checked-first": ""
                }, null, 8, ["modelValue", "action"])
              ]),
              _: 1
            }, 8, ["label", "label-width"]),
            createVNode(_component_el_form_item, {
              label: _ctx.$t("\u89D2\u8272\u540D\u79F0"),
              prop: "name",
              "label-width": $setup.lableWidth
            }, {
              default: withCtx(() => [
                createVNode(_component_el_input, {
                  ref: "nameRef",
                  modelValue: $setup.model.name,
                  "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.name = $event)
                }, null, 8, ["modelValue"])
              ]),
              _: 1
            }, 8, ["label", "label-width"]),
            createVNode(_component_el_form_item, {
              label: _ctx.$t("\u552F\u4E00\u7F16\u7801"),
              prop: "code",
              "label-width": $setup.lableWidth
            }, {
              default: withCtx(() => [
                createVNode(_component_el_input, {
                  modelValue: $setup.model.code,
                  "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.code = $event)
                }, null, 8, ["modelValue"])
              ]),
              _: 1
            }, 8, ["label", "label-width"]),
            createVNode(_component_el_form_item, {
              label: _ctx.$t("\u6240\u5C5E\u7EC4\u7EC7"),
              prop: "orgId",
              "label-width": $setup.lableWidth
            }, {
              default: withCtx(() => [
                createVNode(_component_el_input, {
                  modelValue: $setup.model.orgName,
                  "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.orgName = $event),
                  readonly: true,
                  placeholder: "\u8981\u6307\u5B9A\u89D2\u8272\u6240\u5C5E\u7EC4\u7EC7\uFF0C\u8BF7\u5148\u5206\u914D\u5F53\u524D\u7528\u6237\u7EC4\u7EC7\u6743\u9650"
                }, null, 8, ["modelValue"])
              ]),
              _: 1
            }, 8, ["label", "label-width"]),
            createVNode(_component_el_form_item, {
              label: _ctx.$t("CRM\u5C97\u4F4D\u4EE3\u7801"),
              prop: "crmCode",
              "label-width": $setup.lableWidth
            }, {
              default: withCtx(() => [
                createVNode(_component_el_input, {
                  modelValue: $setup.model.crmCode,
                  "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.model.crmCode = $event),
                  placeholder: "\u7528\u4E8E\u548CCRM\u540C\u6B65\u6570\u636E\u4F7F\u7528, \u6B64\u5904\u7531\u7528\u6237\u624B\u5DE5\u7EF4\u62A4"
                }, null, 8, ["modelValue"])
              ]),
              _: 1
            }, 8, ["label", "label-width"]),
            createVNode(_component_el_form_item, {
              label: _ctx.$t("\u662F\u5426\u9501\u5B9A"),
              prop: "locked",
              "label-width": $setup.lableWidth
            }, {
              default: withCtx(() => [
                createVNode(_component_el_switch, {
                  modelValue: $setup.model.locked,
                  "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.model.locked = $event)
                }, null, 8, ["modelValue"])
              ]),
              _: 1
            }, 8, ["label", "label-width"]),
            createVNode(_component_el_form_item, {
              label: _ctx.$t("tpm.remarks"),
              prop: "remarks",
              "label-width": $setup.lableWidth
            }, {
              default: withCtx(() => [
                createVNode(_component_el_input, {
                  modelValue: $setup.model.remarks,
                  "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => $setup.model.remarks = $event),
                  type: "textarea",
                  rows: 5
                }, null, 8, ["modelValue"])
              ]),
              _: 1
            }, 8, ["label", "label-width"])
          ])
        ]),
        auto: withCtx(() => [
          createElementVNode("div", _hoisted_2$3, [
            $setup.showTree ? (openBlock(), createBlock(_component_tree_page, {
              key: 0,
              ref: "tree",
              onChange: $setup.onTreeChange,
              selection: $setup.model
            }, null, 8, ["onChange", "selection"])) : createCommentVNode("", true)
          ])
        ]),
        _: 1
      }, 8, ["modelValue"])
    ]),
    _: 1
  }, 16, ["model", "rules", "onOpen"]);
}
var Save = /* @__PURE__ */ _export_sfc$1(_sfc_main$h, [["render", _sfc_render$h]]);
var index_vue_vue_type_style_index_0_lang$2 = "";
const _sfc_main$g = {
  props: {
    role: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const { store: store2 } = tpm;
    getCurrentInstance().proxy;
    const message = useMessage();
    const { menu: menuApi, role: roleApi } = tpm.api.admin;
    const { role } = toRefs(props);
    const treeRef = ref();
    const treeData = ref([]);
    const checkedAllButton = ref(false);
    const allButtons = ref([]);
    const loading = ref(false);
    let pages2 = [];
    tpm.modules.forEach((m) => {
      pages2 = pages2.concat(m.pages);
    });
    const refreshTree = () => {
      menuApi.getTree({ groupId: role.value.menuGroupId }).then((data) => {
        allButtons.value = [];
        var groupName = role.value.menuGroupName;
        treeData.value = [
          {
            id: 0,
            label: groupName,
            children: data.map((n) => {
              resolvePage(n);
              return n;
            }),
            path: [],
            item: {
              id: 0,
              icon: "menu",
              type: 0,
              locales: {
                "zh-cn": groupName,
                en: groupName
              }
            }
          }
        ];
        refreshBindMenus();
      });
    };
    const refreshBindMenus = () => {
      roleApi.queryBindMenus({ id: role.value.id }).then((data) => {
        nextTick(() => {
          data.menus.forEach((m) => {
            if (m.menuType !== 0) {
              if (treeRef != null && treeRef.value != null) {
                treeRef.value.setChecked(m.menuId, true);
                if (m.menuType === 1 && m.buttons && m.buttons.length > 0) {
                  m.buttons.forEach((b) => {
                    const button = allButtons.value.find((x) => x.menuId === m.menuId && x.code === b);
                    if (button) {
                      button.checked = true;
                    }
                  });
                }
              }
            }
          });
          handleCheckedButton();
        });
      });
    };
    const resolvePage = (node) => {
      let type = node.item.type;
      if (type === 1) {
        const page2 = pages2.find((p) => p.name === node.item.routeName);
        if (page2) {
          node.page = page2;
          if (page2.buttons) {
            node.buttons = Object.values(page2.buttons).map((m) => {
              let btn = { ...m };
              btn.menuId = node.item.id;
              btn.checked = false;
              return btn;
            });
            allButtons.value.push(...node.buttons);
          } else {
            node.buttons = [];
          }
        }
      } else if (type === 0) {
        node.children.forEach((m) => {
          resolvePage(m);
        });
      }
    };
    const handleCheckedAllButton = (val) => {
      allButtons.value.forEach((b) => {
        b.checked = val;
      });
    };
    const handleCheckedButton = () => {
      checkedAllButton.value = allButtons.value.every((m) => m.checked);
    };
    const submit = () => {
      const model = {
        roleId: role.value.id,
        menus: []
      };
      if (treeRef == null || treeRef.value == null) {
        return;
      }
      try {
        treeRef.value.getCheckedNodes().forEach((n) => {
          if (n.id !== 0) {
            const menu = {
              menuId: n.id,
              menuType: n.item.type,
              buttons: [],
              permissions: []
            };
            if (n.item.type === 1) {
              console.log("n.buttons");
              console.log(n);
              console.log(n.buttons);
              if (n.buttons == null) {
                throw new Error("\u6309\u94AE\u6E05\u5355\u4E0D\u80FD\u52A0\u8F7D\uFF01");
              }
              const buttons2 = n.buttons.filter((b) => b.checked);
              menu.buttons = buttons2.map((b) => b.code);
              if (n.page.permissions)
                menu.permissions.push(...n.page.permissions);
              buttons2.forEach((b) => {
                if (b.permissions)
                  menu.permissions.push(...b.permissions);
              });
            }
            model.menus.push(menu);
          }
        });
        treeRef.value.getHalfCheckedNodes().forEach((n) => {
          if (n.id !== 0) {
            model.menus.push({
              menuId: n.id,
              menuType: n.item.type
            });
          }
        });
        loading.value = true;
        roleApi.updateBindMenus(model).then(() => {
          if (role.value.id === store2.state.app.profile.roleId) {
            store2.dispatch("app/profile/init", null, { root: true }).then(() => {
              message.success(tpm.$t("tpm.save_success_msg"));
              loading.value = false;
            });
          } else {
            loading.value = false;
          }
        });
        message.success("\u64CD\u4F5C\u6210\u529F\uFF01");
      } catch (e) {
        message.success(e.message);
      }
    };
    watch(role, () => {
      refreshTree();
    });
    return {
      treeRef,
      treeData,
      checkedAllButton,
      loading,
      handleCheckedAllButton,
      handleCheckedButton,
      submit
    };
  }
};
const _hoisted_1$3 = { class: "m-admin-bind-menu_label" };
const _hoisted_2$2 = { class: "m-admin-bind-menu_buttons" };
function _sfc_render$g(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_alert = resolveComponent("el-alert");
  const _component_m_icon = resolveComponent("m-icon");
  const _component_el_checkbox = resolveComponent("el-checkbox");
  const _component_el_tree = resolveComponent("el-tree");
  const _component_m_button = resolveComponent("m-button");
  const _component_m_box = resolveComponent("m-box");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_box, {
        page: "",
        title: _ctx.$t("mod.admin.menu_authorization"),
        icon: "link",
        loading: $setup.loading,
        "show-fullscreen": ""
      }, {
        footer: withCtx(() => [
          createVNode(_component_m_button, {
            type: "success",
            icon: "save",
            onClick: $setup.submit
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString(_ctx.$t("tpm.save")), 1)
            ]),
            _: 1
          }, 8, ["onClick"])
        ]),
        default: withCtx(() => [
          createVNode(_component_el_alert, {
            title: _ctx.$t("mod.admin.menu_authorization_alert"),
            type: "warning",
            class: "m-margin-b-20"
          }, null, 8, ["title"]),
          createVNode(_component_el_tree, {
            ref: "treeRef",
            class: "m-admin-bind-menu",
            data: $setup.treeData,
            "node-key": "id",
            "show-checkbox": "",
            "default-expand-all": ""
          }, {
            default: withCtx(({ node, data }) => [
              createVNode(_component_m_icon, {
                class: "m-admin-bind-menu_icon",
                name: data.item.icon || "folder-o",
                style: normalizeStyle({ color: data.item.iconColor })
              }, null, 8, ["name", "style"]),
              createElementVNode("span", _hoisted_1$3, toDisplayString(data.item.locales[_ctx.$i18n.locale] || node.label), 1),
              createElementVNode("div", _hoisted_2$2, [
                data.id === 0 ? (openBlock(), createBlock(_component_el_checkbox, {
                  key: 0,
                  modelValue: $setup.checkedAllButton,
                  "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.checkedAllButton = $event),
                  label: _ctx.$t("mod.admin.select_all"),
                  onChange: $setup.handleCheckedAllButton
                }, null, 8, ["modelValue", "label", "onChange"])) : (openBlock(true), createElementBlock(Fragment, { key: 1 }, renderList(data.buttons, (b) => {
                  return openBlock(), createBlock(_component_el_checkbox, {
                    key: b.code,
                    modelValue: b.checked,
                    "onUpdate:modelValue": ($event) => b.checked = $event,
                    label: _ctx.$t(b.text),
                    disabled: !node.checked,
                    onChange: $setup.handleCheckedButton
                  }, null, 8, ["modelValue", "onUpdate:modelValue", "label", "disabled", "onChange"]);
                }), 128))
              ])
            ]),
            _: 1
          }, 8, ["data"])
        ]),
        _: 1
      }, 8, ["title", "loading"])
    ]),
    _: 1
  });
}
var BindMenu = /* @__PURE__ */ _export_sfc$1(_sfc_main$g, [["render", _sfc_render$g]]);
var index_vue_vue_type_style_index_0_lang$1 = "";
const _sfc_main$f = {
  components: { Save, BindMenu },
  setup() {
    const { query, remove } = tpm.api.admin.role;
    const roles = ref([]);
    const roleId = ref("");
    const current = ref({ id: 0 });
    const listBoxRef = ref();
    const { selection, mode, saveVisible, add, edit } = useList();
    const handleRoleChange = (val, role) => {
      current.value = role;
    };
    const refresh = () => {
      console.log("listBoxRef.refresh");
      listBoxRef.value.refresh();
    };
    return {
      buttons,
      roles,
      roleId,
      current,
      listBoxRef,
      selection,
      mode,
      saveVisible,
      add,
      edit,
      remove,
      query,
      handleRoleChange,
      refresh
    };
  }
};
function _sfc_render$f(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_button = resolveComponent("m-button");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_list_box = resolveComponent("m-list-box");
  const _component_m_flex_fixed = resolveComponent("m-flex-fixed");
  const _component_el_descriptions_item = resolveComponent("el-descriptions-item");
  const _component_el_tag = resolveComponent("el-tag");
  const _component_el_descriptions = resolveComponent("el-descriptions");
  const _component_m_box = resolveComponent("m-box");
  const _component_bind_menu = resolveComponent("bind-menu");
  const _component_m_flex_auto = resolveComponent("m-flex-auto");
  const _component_m_flex_col = resolveComponent("m-flex-col");
  const _component_m_flex_row = resolveComponent("m-flex-row");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, { class: "m-admin-role" }, {
    default: withCtx(() => [
      createVNode(_component_m_flex_row, null, {
        default: withCtx(() => [
          createVNode(_component_m_flex_fixed, {
            width: "300px",
            class: "m-margin-r-10"
          }, {
            default: withCtx(() => [
              createVNode(_component_m_list_box, {
                ref: "listBoxRef",
                modelValue: $setup.roleId,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.roleId = $event),
                title: _ctx.$t("mod.admin.role_list"),
                icon: "role",
                action: $setup.query,
                onChange: $setup.handleRoleChange,
                header: true
              }, {
                toolbar: withCtx(() => [
                  createVNode(_component_m_button, {
                    code: $setup.buttons.add.code,
                    icon: "plus",
                    onClick: $setup.add
                  }, null, 8, ["code", "onClick"])
                ]),
                action: withCtx(({ item }) => [
                  createVNode(_component_m_button_edit, {
                    code: $setup.buttons.edit.code,
                    onClick: withModifiers(($event) => $setup.edit(item), ["stop"]),
                    onSuccess: $setup.refresh
                  }, null, 8, ["code", "onClick", "onSuccess"]),
                  createVNode(_component_m_button_delete, {
                    code: $setup.buttons.remove.code,
                    action: $setup.remove,
                    data: item.id,
                    onSuccess: $setup.refresh
                  }, null, 8, ["code", "action", "data", "onSuccess"])
                ]),
                _: 1
              }, 8, ["modelValue", "title", "action", "onChange"])
            ]),
            _: 1
          }),
          createVNode(_component_m_flex_auto, null, {
            default: withCtx(() => [
              createVNode(_component_m_flex_col, { class: "m-fill-h" }, {
                default: withCtx(() => [
                  createVNode(_component_m_flex_fixed, null, {
                    default: withCtx(() => [
                      createVNode(_component_m_box, {
                        title: _ctx.$t("mod.admin.role_info"),
                        icon: "preview",
                        "show-collapse": ""
                      }, {
                        default: withCtx(() => [
                          createVNode(_component_el_descriptions, {
                            column: 4,
                            border: ""
                          }, {
                            default: withCtx(() => [
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("tpm.name")
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.name), 1)
                                ]),
                                _: 1
                              }, 8, ["label"]),
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("mod.admin.bind_menu_group")
                              }, {
                                default: withCtx(() => [
                                  createVNode(_component_el_tag, {
                                    type: "danger",
                                    size: "small",
                                    effect: "dark"
                                  }, {
                                    default: withCtx(() => [
                                      createTextVNode(toDisplayString($setup.current.menuGroupName), 1)
                                    ]),
                                    _: 1
                                  })
                                ]),
                                _: 1
                              }, 8, ["label"]),
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("\u6240\u5C5E\u7EC4\u7EC7")
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.orgId), 1)
                                ]),
                                _: 1
                              }, 8, ["label"]),
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("CRM\u5C97\u4F4D\u4EE3\u7801")
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.crmCode), 1)
                                ]),
                                _: 1
                              }, 8, ["label"]),
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("tpm.code")
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.code), 1)
                                ]),
                                _: 1
                              }, 8, ["label"]),
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("mod.admin.is_lock")
                              }, {
                                default: withCtx(() => [
                                  $setup.current.locked ? (openBlock(), createBlock(_component_el_tag, {
                                    key: 0,
                                    type: "danger",
                                    effect: "dark",
                                    size: "small"
                                  }, {
                                    default: withCtx(() => [
                                      createTextVNode(toDisplayString(_ctx.$t("mod.admin.lock")), 1)
                                    ]),
                                    _: 1
                                  })) : (openBlock(), createBlock(_component_el_tag, {
                                    key: 1,
                                    type: "info",
                                    effect: "dark",
                                    size: "small"
                                  }, {
                                    default: withCtx(() => [
                                      createTextVNode(toDisplayString(_ctx.$t("mod.admin.not_lock")), 1)
                                    ]),
                                    _: 1
                                  }))
                                ]),
                                _: 1
                              }, 8, ["label"]),
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("tpm.creator")
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.creator), 1)
                                ]),
                                _: 1
                              }, 8, ["label"]),
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("tpm.created_time")
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.createdTime), 1)
                                ]),
                                _: 1
                              }, 8, ["label"]),
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("tpm.remarks"),
                                span: 2
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.remarks), 1)
                                ]),
                                _: 1
                              }, 8, ["label"])
                            ]),
                            _: 1
                          })
                        ]),
                        _: 1
                      }, 8, ["title"])
                    ]),
                    _: 1
                  }),
                  createVNode(_component_m_flex_auto, { class: "m-margin-t-10" }, {
                    default: withCtx(() => [
                      createVNode(_component_bind_menu, { role: $setup.current }, null, 8, ["role"])
                    ]),
                    _: 1
                  })
                ]),
                _: 1
              })
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      createVNode(_component_save, {
        id: $setup.selection.id,
        modelValue: $setup.saveVisible,
        "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.saveVisible = $event),
        mode: $setup.mode,
        onSuccess: $setup.refresh
      }, null, 8, ["id", "modelValue", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component = /* @__PURE__ */ _export_sfc$1(_sfc_main$f, [["render", _sfc_render$f]]);
const page = {
  "name": "admin_role",
  "icon": "role",
  "path": "/admin/role",
  "permissions": ["admin_role_query_get"],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "admin_role_add",
      "permissions": ["admin_role_add_post"]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "admin_role_edit",
      "permissions": ["admin_role_edit_get", "admin_role_update_post"]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "admin_role_delete",
      "permissions": ["admin_role_delete_delete"]
    },
    "bindMenu": {
      "text": "mod.admin.menu_bind",
      "code": "admin_role_bindmenu",
      "permissions": ["admin_menu_tree_get", "admin_role_QueryBindMenus_get", "admin_role_UpdateBindMenus_post"]
    }
  }
};
page.component = component;
component.name = page.name;
const _sfc_main$e = {
  props: {
    clearable: {
      type: Boolean,
      default: true
    }
  },
  setup() {
    const { queryAccountStatusSelect } = tpm.api.admin.common;
    const query = () => {
      return queryAccountStatusSelect();
    };
    return {
      query
    };
  }
};
function _sfc_render$e(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, {
    action: $setup.query,
    clearable: $props.clearable
  }, null, 8, ["action", "clearable"]);
}
var component_0 = /* @__PURE__ */ _export_sfc$1(_sfc_main$e, [["render", _sfc_render$e]]);
const _sfc_main$d = {
  props: {
    clearable: {
      type: Boolean,
      default: true
    }
  },
  setup() {
    const { queryAccountTypeSelect } = tpm.api.admin.common;
    const query = () => {
      return queryAccountTypeSelect();
    };
    return {
      query
    };
  }
};
function _sfc_render$d(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, {
    action: $setup.query,
    clearable: $props.clearable
  }, null, 8, ["action", "clearable"]);
}
var component_1 = /* @__PURE__ */ _export_sfc$1(_sfc_main$d, [["render", _sfc_render$d]]);
const id = 0;
const name = "tpm-mod-admin";
const code = "admin";
const label = "\u6743\u9650\u7BA1\u7406\u6A21\u5757";
const version = "1.0.9";
const icon = "lock";
const description = "CRB.TPM\u6743\u9650\u7BA1\u7406\u6A21\u5757";
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
  "js-base64": "^3.7.2",
  "tpm-ui": "^1.2.0",
  vuedraggable: "^2.24.3"
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
  code,
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
  code,
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
console.log("pack", pack);
var module = {
  id: pack.id,
  name: pack.label,
  code: "admin",
  version: pack.version,
  description: pack.description
};
const _sfc_main$c = {
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
      loginLog: false,
      defaultPassword: ""
    });
    const edits = () => {
      edit({
        type: model.type,
        code: model.code
      }).then((data) => {
        const res = JSON.parse(data);
        model.loginLog = res.loginLog;
        model.defaultPassword = res.defaultPassword;
        console.log("model", model);
      });
    };
    const rules = {
      defaultPassword: [{ required: true, message: "\u8BF7\u8F93\u5165\u9ED8\u8BA4\u5BC6\u7801" }]
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
const _hoisted_1$2 = { style: { "padding": "50px" } };
function _sfc_render$c(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_switch = resolveComponent("el-switch");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_input = resolveComponent("el-input");
  const _component_m_button = resolveComponent("m-button");
  const _component_m_form = resolveComponent("m-form");
  return openBlock(), createElementBlock("div", _hoisted_1$2, [
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
          "label-width": "150px",
          label: "\u542F\u7528\u767B\u5F55\u65E5\u5FD7",
          prop: "loginLog"
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
          "label-width": "150px",
          label: "\u8D26\u6237\u9ED8\u8BA4\u5BC6\u7801",
          prop: "defaultPassword"
        }, {
          default: withCtx(() => [
            createVNode(_component_el_input, {
              modelValue: $setup.model.defaultPassword,
              "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.defaultPassword = $event)
            }, null, 8, ["modelValue"])
          ]),
          _: 1
        }),
        createVNode(_component_el_form_item, { "label-width": "150px" }, {
          default: withCtx(() => [
            createVNode(_component_m_button, {
              type: "primary",
              onClick: _cache[2] || (_cache[2] = () => $setup.formRef.submit()),
              icon: "save"
            }, {
              default: withCtx(() => [
                createTextVNode("\u4FDD\u5B58")
              ]),
              _: 1
            }),
            createVNode(_component_m_button, {
              type: "info",
              onClick: _cache[3] || (_cache[3] = () => $setup.formRef.reset()),
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
var component_2 = /* @__PURE__ */ _export_sfc$1(_sfc_main$c, [["render", _sfc_render$c]]);
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
      return tpm.api.admin.dict.cascader({ groupCode: group, dictCode: dict });
    };
    return {
      query
    };
  }
};
function _sfc_render$b(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_cascader = resolveComponent("m-cascader");
  return openBlock(), createBlock(_component_m_cascader, { action: $setup.query }, null, 8, ["action"]);
}
var component_3 = /* @__PURE__ */ _export_sfc$1(_sfc_main$b, [["render", _sfc_render$b]]);
const _sfc_main$a = {
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
function _sfc_render$a(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, { action: $setup.query }, null, 8, ["action"]);
}
var component_4 = /* @__PURE__ */ _export_sfc$1(_sfc_main$a, [["render", _sfc_render$a]]);
var index_vue_vue_type_style_index_0_lang = "";
const _sfc_main$9 = {
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
function _sfc_render$9(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_button = resolveComponent("m-button");
  return openBlock(), createBlock(_component_m_button, {
    icon: $setup.isFullscreen ? "full-screen-exit" : "full-screen",
    onClick: $setup.handleClick
  }, null, 8, ["icon", "onClick"]);
}
var component_5 = /* @__PURE__ */ _export_sfc$1(_sfc_main$9, [["render", _sfc_render$9]]);
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
  const _component_m_checkbox = resolveComponent("m-checkbox");
  return openBlock(), createBlock(_component_m_checkbox, { action: $setup.query }, null, 8, ["action"]);
}
var component_6 = /* @__PURE__ */ _export_sfc$1(_sfc_main$8, [["render", _sfc_render$8]]);
const _sfc_main$7 = {
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
function _sfc_render$7(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_radio = resolveComponent("m-radio");
  return openBlock(), createBlock(_component_m_radio, { action: $setup.query }, null, 8, ["action"]);
}
var component_7 = /* @__PURE__ */ _export_sfc$1(_sfc_main$7, [["render", _sfc_render$7]]);
const _sfc_main$6 = {
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
function _sfc_render$6(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, {
    action: $setup.query,
    clearable: $props.clearable
  }, null, 8, ["action", "clearable"]);
}
var component_8 = /* @__PURE__ */ _export_sfc$1(_sfc_main$6, [["render", _sfc_render$6]]);
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
var component_9 = /* @__PURE__ */ _export_sfc$1(_sfc_main$5, [["render", _sfc_render$5]]);
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
var component_10 = /* @__PURE__ */ _export_sfc$1(_sfc_main$4, [["render", _sfc_render$4]]);
var panel_vue_vue_type_style_index_0_scoped_true_lang = "";
const _sfc_main$3 = {
  props: {
    nodeProps: {
      children: "children",
      label: "label",
      isLeaf: "isLeaf"
    },
    selection: {
      type: Object
    },
    refreshOnCreated: {
      type: Boolean,
      default: true
    }
  },
  emits: ["success"],
  components: { draggable },
  setup(props, { emit }) {
    const { store: store2, $t } = tpm;
    useMessage();
    const { getNodeTree, getOrgLevel, getNodeByParentId } = tpm.api.admin.role;
    const dragTemp = ref({});
    const loading = ref(false);
    const bind = reactive({
      title: "",
      icon: "",
      action: null,
      disabled: false,
      footer: true,
      destroyOnClose: true,
      loading: false
    });
    const checked1 = ref(false);
    const checked2 = ref(false);
    const checked3 = ref(false);
    const checked4 = ref(false);
    const checked5 = ref(false);
    const checked6 = ref(false);
    const checkedLable1 = ref("\u5168\u9009");
    const checkedLable2 = ref("\u5168\u9009");
    const checkedLable3 = ref("\u5168\u9009");
    const checkedLable4 = ref("\u5168\u9009");
    const checkedLable5 = ref("\u5168\u9009");
    const checkedLable6 = ref("\u5168\u9009");
    const treeRef1 = ref();
    const treeRef2 = ref();
    const treeRef3 = ref();
    const treeRef4 = ref();
    const treeRef5 = ref();
    const treeRef6 = ref();
    const loading1 = ref(false);
    const loading2 = ref(false);
    const loading3 = ref(false);
    const loading4 = ref(false);
    const loading5 = ref(false);
    const loading6 = ref(false);
    const model = reactive({
      headOffice: [{}],
      headOfficeSelections: [],
      dbs: [{}],
      dbsSelections: [],
      marketingCenters: [{}],
      marketingCentersSelections: [],
      saleRegions: [{}],
      saleRegionsSelections: [],
      departments: [{}],
      departmentsSelections: [],
      stations: [{}],
      stationsSelections: []
    });
    const keys1 = computed(() => {
      return model.headOfficeSelections.filter((s) => s.id);
    });
    const keys2 = computed(() => {
      return model.dbsSelections.filter((s) => s.id);
    });
    const keys3 = computed(() => {
      return model.marketingCentersSelections.filter((s) => s.id);
    });
    const keys4 = computed(() => {
      return model.saleRegionsSelections.filter((s) => s.id);
    });
    const keys5 = computed(() => {
      return model.departmentsSelections.filter((s) => s.id);
    });
    const keys6 = computed(() => {
      return model.stationsSelections.filter((s) => s.id);
    });
    const nameRef = ref(null);
    bind.closeOnSuccess = true;
    bind.destroyOnClose = true;
    bind.autoFocusRef = nameRef;
    bind.width = "1400px";
    bind.height = "760px";
    bind.title = "\u8FC7\u6EE4\u7EC4\u7EC7";
    bind.btnOkText = "\u786E\u8BA4\u9009\u62E9";
    bind.icon = "search";
    bind.successMessage = "\u6570\u636E\u5DF2\u7ECF\u9009\u62E9";
    bind.beforeSubmit = () => {
      props.selection.headOffice = keys1.value.map((b) => b.id);
      props.selection.dbs = keys2.value.map((b) => b.id);
      props.selection.marketingCenters = keys3.value.map((b) => b.id);
      props.selection.saleRegions = keys4.value.map((b) => b.id);
      props.selection.departments = keys5.value.map((b) => b.id);
      props.selection.stations = keys6.value.map((b) => b.id);
    };
    bind.action = () => {
      let selection = props.selection;
      console.log("action -> selection", selection);
      return new Promise((resolve) => {
        emit("success", selection);
        resolve(selection);
      });
    };
    const tagSelected = (db, selections, ref2) => {
      db.forEach((item) => {
        nextTick(() => {
          let exist = selections.find((m) => m.id === item.id);
          if (exist != null) {
            ref2.value.setChecked(item.id, true);
          }
        });
      });
    };
    const headOfficeChange = async (data, node) => {
      loading2.value = true;
      if (data != null) {
        const res = await getNodeByParentId({
          level: 20,
          ignore: node != null,
          parentId: data == null ? "" : data.key == null ? data.id : data.key
        });
        model.dbs = res;
        tagSelected(model.dbs, model.dbsSelections, treeRef2);
      }
      loading2.value = false;
      const nextNodefirst = model.dbs[0];
      if (nextNodefirst != null) {
        await dbsChange(nextNodefirst);
      } else {
        dbsChange([]);
      }
    };
    const dbsChange = async (data, node) => {
      loading3.value = true;
      if (data != null) {
        const res = await getNodeByParentId({
          level: 30,
          ignore: node != null,
          parentId: data == null ? "" : data.key == null ? data.id : data.key
        });
        model.marketingCenters = res;
        tagSelected(model.marketingCenters, model.marketingCentersSelections, treeRef3);
      }
      loading3.value = false;
      const nextNodefirst = model.marketingCenters[0];
      if (nextNodefirst != null) {
        marketingCentersChange(nextNodefirst);
      } else {
        marketingCentersChange([]);
      }
    };
    const marketingCentersChange = async (data, node) => {
      loading4.value = true;
      if (data != null) {
        const res = await getNodeByParentId({
          level: 40,
          ignore: node != null,
          parentId: data == null ? "" : data.key == null ? data.id : data.key
        });
        model.saleRegions = res;
        tagSelected(model.saleRegions, model.saleRegionsSelections, treeRef4);
      }
      loading4.value = false;
      const nextNodefirst = model.saleRegions[0];
      if (nextNodefirst != null) {
        saleRegionsChange(nextNodefirst);
      } else {
        saleRegionsChange([]);
      }
    };
    const saleRegionsChange = async (data, node) => {
      loading5.value = true;
      if (data != null) {
        const res = await getNodeByParentId({
          level: 50,
          ignore: node != null,
          parentId: data == null ? "" : data.key == null ? data.id : data.key
        });
        model.departments = res;
        tagSelected(model.departments, model.departmentsSelections, treeRef5);
      }
      loading5.value = false;
      const nextNodefirst = model.departments[0];
      if (nextNodefirst != null) {
        departmentsChange(nextNodefirst);
      } else {
        departmentsChange([]);
      }
    };
    const departmentsChange = async (data, node) => {
      loading6.value = true;
      if (data != null) {
        const res = await getNodeByParentId({
          level: 60,
          ignore: node != null,
          parentId: data == null ? "" : data.key == null ? data.id : data.key
        });
        model.stations = res;
        tagSelected(model.stations, model.stationsSelections, treeRef6);
      }
      loading6.value = false;
    };
    const stationsChange = (data) => {
    };
    const pushOrpop = (cols, db, data, checked, fun) => {
      dragTemp.value = {};
      if (checked.checkedKeys.length > 0) {
        checked.checkedKeys.forEach((key) => {
          let curt = db.find((m) => m.id === key);
          if (curt !== null) {
            const exist = cols.find((m) => m.id === key);
            if (!exist)
              cols.push(curt);
          }
        });
        if (cols.length == 0) {
          const exist = cols.find((m) => m.id === data.id);
          if (!exist)
            cols.push(data);
        }
      } else {
        cols = cols.filter((item) => {
          return item.id !== data.id;
        });
      }
      fun(cols);
    };
    const headOfficeClick = (data, checked) => {
      let cols = model.headOfficeSelections;
      pushOrpop(cols, model.headOffice, data, checked, (ref2) => {
        model.headOfficeSelections = ref2;
      });
    };
    const dbsClick = (data, checked) => {
      let cols = model.dbsSelections;
      pushOrpop(cols, model.dbs, data, checked, (ref2) => {
        model.dbsSelections = ref2;
      });
    };
    const marketingCentersClick = (data, checked) => {
      let cols = model.marketingCentersSelections;
      pushOrpop(cols, model.marketingCenters, data, checked, (ref2) => {
        model.marketingCentersSelections = ref2;
      });
    };
    const saleRegionsClick = (data, checked) => {
      let cols = model.saleRegionsSelections;
      pushOrpop(cols, model.saleRegions, data, checked, (ref2) => {
        model.saleRegionsSelections = ref2;
      });
    };
    const departmentsClick = (data, checked) => {
      let cols = model.departmentsSelections;
      pushOrpop(cols, model.departments, data, checked, (ref2) => {
        model.departmentsSelections = ref2;
      });
    };
    const stationsClick = (data, checked) => {
      let cols = model.stationsSelections;
      pushOrpop(cols, model.stations, data, checked, (ref2) => {
        model.stationsSelections = ref2;
      });
    };
    const checkAll = (value, index2) => {
      let c = "\u53D6\u6D88";
      let a = "\u5168\u9009";
      let treeRef = treeRef1;
      let selections = [];
      switch (index2) {
        case 1:
          treeRef = treeRef1;
          selections = model.headOffice;
          checkedLable1.value = value ? c : a;
          break;
        case 2:
          treeRef = treeRef2;
          selections = model.dbs;
          checkedLable2.value = value ? c : a;
          break;
        case 3:
          treeRef = treeRef3;
          selections = model.marketingCenters;
          checkedLable3.value = value ? c : a;
          break;
        case 4:
          treeRef = treeRef4;
          selections = model.saleRegions;
          checkedLable4.value = value ? c : a;
          break;
        case 5:
          treeRef = treeRef5;
          selections = model.departments;
          checkedLable5.value = value ? c : a;
          break;
        case 6:
          treeRef = treeRef6;
          selections = model.stations;
          checkedLable6.value = value ? c : a;
          break;
      }
      if (selections.length > 0) {
        selections.forEach((item) => {
          console.log("treeRef.value", treeRef.value);
          treeRef.value.setChecked(item.id, value);
          switch (index2) {
            case 1:
              {
                let cols = model.headOfficeSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
            case 2:
              {
                let cols = model.dbsSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
            case 3:
              {
                let cols = model.marketingCentersSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
            case 4:
              {
                let cols = model.saleRegionsSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
            case 5:
              {
                let cols = model.departmentsSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
            case 6:
              {
                let cols = model.stationsSelections;
                cols.splice(cols.indexOf(item), 1);
              }
              break;
          }
        });
      }
      let getNodes = treeRef.value.getCheckedNodes();
      if (getNodes.length > 0) {
        getNodes.forEach((item) => {
          switch (index2) {
            case 1:
              {
                let cols = model.headOfficeSelections;
                cols.push(item);
              }
              break;
            case 2:
              {
                let cols = model.dbsSelections;
                cols.push(item);
              }
              break;
            case 3:
              {
                let cols = model.marketingCentersSelections;
                cols.push(item);
              }
              break;
            case 4:
              {
                let cols = model.saleRegionsSelections;
                cols.push(item);
              }
              break;
            case 5:
              {
                let cols = model.departmentsSelections;
                cols.push(item);
              }
              break;
            case 6:
              {
                let cols = model.stationsSelections;
                cols.push(item);
              }
              break;
          }
        });
      }
    };
    const handleClose = (tag, cols, index2) => {
      cols.splice(cols.indexOf(tag), 1);
      let treeRef = treeRef1;
      switch (index2) {
        case 1:
          treeRef = treeRef1;
          break;
        case 2:
          treeRef = treeRef2;
          break;
        case 3:
          treeRef = treeRef3;
          break;
        case 4:
          treeRef = treeRef4;
          break;
        case 5:
          treeRef = treeRef5;
          break;
        case 6:
          treeRef = treeRef6;
          break;
      }
      let getNode = treeRef.value.getCheckedNodes();
      if (getNode.length > 0) {
        getNode.forEach((item) => {
          if (tag.id == item.id) {
            treeRef.value.setChecked(item.id, false);
          }
        });
      }
    };
    const handleDragStart = (node) => {
      console.log("handleDragStart -> node", node, node.data.type);
      dragTemp.value = node.data;
    };
    const allowDrop = (ev) => {
      ev.preventDefault();
    };
    const drop3 = (ev, ditem) => {
      console.log("ditem", ditem);
      console.log("ev", ev);
      ev.preventDefault();
      let treeNode = ev.target;
      console.log("treeNode", treeNode);
      let item = dragTemp.value;
      if (item.type !== null) {
        switch (item.type) {
          case 10:
            {
              let cols = model.headOfficeSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.headOffice, cols, treeRef1);
              }
            }
            break;
          case 20:
            {
              let cols = model.dbsSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.dbs, cols, treeRef2);
              }
            }
            break;
          case 30:
            {
              let cols = model.marketingCentersSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.marketingCenters, cols, treeRef3);
              }
            }
            break;
          case 40:
            {
              let cols = model.saleRegionsSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.saleRegions, cols, treeRef4);
              }
            }
            break;
          case 50:
            {
              let cols = model.departmentsSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.departments, cols, treeRef5);
              }
            }
            break;
          case 60:
            {
              let cols = model.stationsSelections;
              const exist = cols.find((m) => m.id === item.id);
              if (exist == null) {
                cols.push(item);
                tagSelected(model.stations, cols, treeRef6);
              }
            }
            break;
        }
      }
    };
    const collapse = (moveNode, inNode, type) => {
      return false;
    };
    const handleOpened = async () => {
      loading1.value = true;
      const res = await getNodeTree(1);
      model.headOffice = res;
      loading1.value = false;
      console.log("model.headOffice", model.headOffice);
      console.log("store.state.app.profile.accountRoleOrgs.length > 0", store2.state.app.profile.accountRoleOrgs.length > 0);
      const nextNodefirst = model.headOffice[0];
      if (nextNodefirst != null) {
        await headOfficeChange(nextNodefirst, null);
      } else {
        headOfficeChange([], null);
      }
    };
    const handleOpen = () => {
    };
    const handleReset = () => {
    };
    const handleSuccess = () => {
    };
    const handleError = () => {
    };
    return {
      ...toRefs(model),
      model,
      bind,
      on: { open: handleOpen, reset: handleReset, success: handleSuccess, error: handleError },
      nameRef,
      handleOpen,
      handleOpened,
      handleSuccess,
      handleError,
      checkAll,
      pushOrpop,
      tagSelected,
      dragTemp,
      collapse,
      handleDragStart,
      allowDrop,
      drop: drop3,
      keys1,
      keys2,
      keys3,
      keys4,
      keys5,
      keys6,
      loading,
      loading1,
      loading2,
      loading3,
      loading4,
      loading5,
      loading6,
      treeRef1,
      treeRef2,
      treeRef3,
      treeRef4,
      treeRef5,
      treeRef6,
      headOfficeChange,
      dbsChange,
      marketingCentersChange,
      saleRegionsChange,
      departmentsChange,
      stationsChange,
      headOfficeClick,
      dbsClick,
      marketingCentersClick,
      saleRegionsClick,
      departmentsClick,
      stationsClick,
      checked1,
      checked2,
      checked3,
      checked4,
      checked5,
      checked6,
      checkedLable1,
      checkedLable2,
      checkedLable3,
      checkedLable4,
      checkedLable5,
      checkedLable6,
      handleClose
    };
  }
};
const _withScopeId = (n) => (pushScopeId("data-v-a8fd9eb8"), n = n(), popScopeId(), n);
const _hoisted_1$1 = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_2$1 = /* @__PURE__ */ _withScopeId(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_3 = [
  _hoisted_2$1
];
const _hoisted_4 = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_5 = /* @__PURE__ */ _withScopeId(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_6 = [
  _hoisted_5
];
const _hoisted_7 = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_8 = /* @__PURE__ */ _withScopeId(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_9 = [
  _hoisted_8
];
const _hoisted_10 = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_11 = /* @__PURE__ */ _withScopeId(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_12 = [
  _hoisted_11
];
const _hoisted_13 = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_14 = /* @__PURE__ */ _withScopeId(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_15 = [
  _hoisted_14
];
const _hoisted_16 = {
  key: 1,
  class: "el-tree__empty-block"
};
const _hoisted_17 = /* @__PURE__ */ _withScopeId(() => /* @__PURE__ */ createElementVNode("span", { class: "el-tree__empty-text" }, "\u62D6\u62FD\u5230\u8FD9...", -1));
const _hoisted_18 = [
  _hoisted_17
];
function _sfc_render$3(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_checkbox = resolveComponent("el-checkbox");
  const _component_m_flex_auto = resolveComponent("m-flex-auto");
  const _component_m_flex_fixed = resolveComponent("m-flex-fixed");
  const _component_m_flex_row = resolveComponent("m-flex-row");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_el_tree = resolveComponent("el-tree");
  const _component_el_tag = resolveComponent("el-tag");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  const _directive_loading = resolveDirective("loading");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({ model: $setup.model }, $setup.bind, toHandlers($setup.on), {
    onOpened: $setup.handleOpened,
    loading: $setup.loading,
    title: "\u8FC7\u6EE4\u7EC4\u7EC7"
  }), {
    default: withCtx(() => [
      createVNode(_component_el_row, { class: "m-roleorg-row" }, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 4 }, {
            default: withCtx(() => [
              createVNode(_component_m_flex_row, null, {
                default: withCtx(() => [
                  createVNode(_component_m_flex_auto, null, {
                    default: withCtx(() => [
                      createVNode(_component_el_checkbox, {
                        modelValue: $setup.checked1,
                        "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.checked1 = $event),
                        style: { color: "#fff" },
                        onChange: _cache[1] || (_cache[1] = ($event) => $setup.checkAll($setup.checked1, 1))
                      }, {
                        default: withCtx(() => [
                          createTextVNode(toDisplayString($setup.checkedLable1), 1)
                        ]),
                        _: 1
                      }, 8, ["modelValue"])
                    ]),
                    _: 1
                  }),
                  createVNode(_component_m_flex_fixed, null, {
                    default: withCtx(() => [
                      createTextVNode(" \u96EA\u82B1 ")
                    ]),
                    _: 1
                  })
                ]),
                _: 1
              })
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 4 }, {
            default: withCtx(() => [
              createVNode(_component_m_flex_row, null, {
                default: withCtx(() => [
                  createVNode(_component_m_flex_auto, null, {
                    default: withCtx(() => [
                      createVNode(_component_el_checkbox, {
                        ref: "checkedRef2",
                        modelValue: $setup.checked2,
                        "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.checked2 = $event),
                        style: { color: "#fff" },
                        onChange: _cache[3] || (_cache[3] = ($event) => $setup.checkAll($setup.checked2, 2))
                      }, {
                        default: withCtx(() => [
                          createTextVNode(toDisplayString($setup.checkedLable2), 1)
                        ]),
                        _: 1
                      }, 8, ["modelValue"])
                    ]),
                    _: 1
                  }),
                  createVNode(_component_m_flex_fixed, null, {
                    default: withCtx(() => [
                      createTextVNode(" \u4E8B\u4E1A\u90E8 ")
                    ]),
                    _: 1
                  })
                ]),
                _: 1
              })
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 4 }, {
            default: withCtx(() => [
              createVNode(_component_m_flex_row, null, {
                default: withCtx(() => [
                  createVNode(_component_m_flex_auto, null, {
                    default: withCtx(() => [
                      createVNode(_component_el_checkbox, {
                        ref: "checkedRef3",
                        modelValue: $setup.checked3,
                        "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.checked3 = $event),
                        style: { color: "#fff" },
                        onChange: _cache[5] || (_cache[5] = ($event) => $setup.checkAll($setup.checked3, 3))
                      }, {
                        default: withCtx(() => [
                          createTextVNode(toDisplayString($setup.checkedLable3), 1)
                        ]),
                        _: 1
                      }, 8, ["modelValue"])
                    ]),
                    _: 1
                  }),
                  createVNode(_component_m_flex_fixed, null, {
                    default: withCtx(() => [
                      createTextVNode(" \u8425\u9500\u4E2D\u5FC3 ")
                    ]),
                    _: 1
                  })
                ]),
                _: 1
              })
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 4 }, {
            default: withCtx(() => [
              createVNode(_component_m_flex_row, null, {
                default: withCtx(() => [
                  createVNode(_component_m_flex_auto, null, {
                    default: withCtx(() => [
                      createVNode(_component_el_checkbox, {
                        ref: "checkedRef3",
                        modelValue: $setup.checked4,
                        "onUpdate:modelValue": _cache[6] || (_cache[6] = ($event) => $setup.checked4 = $event),
                        style: { color: "#fff" },
                        onChange: _cache[7] || (_cache[7] = ($event) => $setup.checkAll($setup.checked4, 4))
                      }, {
                        default: withCtx(() => [
                          createTextVNode(toDisplayString($setup.checkedLable4), 1)
                        ]),
                        _: 1
                      }, 8, ["modelValue"])
                    ]),
                    _: 1
                  }),
                  createVNode(_component_m_flex_fixed, null, {
                    default: withCtx(() => [
                      createTextVNode(" \u5927\u533A ")
                    ]),
                    _: 1
                  })
                ]),
                _: 1
              })
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 4 }, {
            default: withCtx(() => [
              createVNode(_component_m_flex_row, null, {
                default: withCtx(() => [
                  createVNode(_component_m_flex_auto, null, {
                    default: withCtx(() => [
                      createVNode(_component_el_checkbox, {
                        ref: "checkedRef4",
                        modelValue: $setup.checked5,
                        "onUpdate:modelValue": _cache[8] || (_cache[8] = ($event) => $setup.checked5 = $event),
                        style: { color: "#fff" },
                        onChange: _cache[9] || (_cache[9] = ($event) => $setup.checkAll($setup.checked5, 5))
                      }, {
                        default: withCtx(() => [
                          createTextVNode(toDisplayString($setup.checkedLable5), 1)
                        ]),
                        _: 1
                      }, 8, ["modelValue"])
                    ]),
                    _: 1
                  }),
                  createVNode(_component_m_flex_fixed, null, {
                    default: withCtx(() => [
                      createTextVNode(" \u4E1A\u52A1\u90E8 ")
                    ]),
                    _: 1
                  })
                ]),
                _: 1
              })
            ]),
            _: 1
          }),
          createVNode(_component_el_col, { span: 4 }, {
            default: withCtx(() => [
              createVNode(_component_m_flex_row, null, {
                default: withCtx(() => [
                  createVNode(_component_m_flex_auto, null, {
                    default: withCtx(() => [
                      createVNode(_component_el_checkbox, {
                        ref: "checkedRef5",
                        modelValue: $setup.checked6,
                        "onUpdate:modelValue": _cache[10] || (_cache[10] = ($event) => $setup.checked6 = $event),
                        style: { color: "#fff" },
                        onChange: _cache[11] || (_cache[11] = ($event) => $setup.checkAll($setup.checked6, 6))
                      }, {
                        default: withCtx(() => [
                          createTextVNode(toDisplayString($setup.checkedLable6), 1)
                        ]),
                        _: 1
                      }, 8, ["modelValue"])
                    ]),
                    _: 1
                  }),
                  createVNode(_component_m_flex_fixed, null, {
                    default: withCtx(() => [
                      createTextVNode(" \u5DE5\u4F5C\u7AD9 ")
                    ]),
                    _: 1
                  })
                ]),
                _: 1
              })
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      createVNode(_component_el_row, { class: "m-roleorg-tree" }, {
        default: withCtx(() => [
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border"
          }, {
            default: withCtx(() => [
              withDirectives((openBlock(), createBlock(_component_el_tree, {
                ref: "treeRef1",
                "current-node-key": `00000000-0000-0000-0000-000000000000`,
                props: $props.nodeProps,
                data: _ctx.headOffice,
                "node-key": "id",
                "highlight-current": "",
                onCurrentChange: $setup.headOfficeChange,
                "expand-on-click-node": false,
                "check-strictly": true,
                "show-checkbox": "",
                onCheck: $setup.headOfficeClick,
                style: { height: "300px", overflow: "auto" },
                "default-checked-keys": $setup.keys1,
                draggable: true,
                onNodeDragStart: $setup.handleDragStart,
                "allow-drop": $setup.collapse
              }, {
                default: withCtx(({ node, data }) => [
                  createElementVNode("span", null, toDisplayString(data.name), 1)
                ]),
                _: 1
              }, 8, ["props", "data", "onCurrentChange", "onCheck", "default-checked-keys", "onNodeDragStart", "allow-drop"])), [
                [_directive_loading, $setup.loading1]
              ])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border"
          }, {
            default: withCtx(() => [
              withDirectives((openBlock(), createBlock(_component_el_tree, {
                ref: "treeRef2",
                "current-node-key": `00000000-0000-0000-0000-000000000000`,
                props: $props.nodeProps,
                data: _ctx.dbs,
                "node-key": "id",
                "highlight-current": "",
                onCurrentChange: $setup.dbsChange,
                "expand-on-click-node": false,
                "check-strictly": true,
                "show-checkbox": "",
                onCheck: $setup.dbsClick,
                style: { height: "300px", overflow: "auto" },
                "default-checked-keys": $setup.keys2,
                draggable: true,
                onNodeDragStart: $setup.handleDragStart,
                "allow-drop": $setup.collapse
              }, {
                default: withCtx(({ node, data }) => [
                  createElementVNode("span", null, toDisplayString(data.name), 1)
                ]),
                _: 1
              }, 8, ["props", "data", "onCurrentChange", "onCheck", "default-checked-keys", "onNodeDragStart", "allow-drop"])), [
                [_directive_loading, $setup.loading2]
              ])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border"
          }, {
            default: withCtx(() => [
              withDirectives((openBlock(), createBlock(_component_el_tree, {
                ref: "treeRef3",
                "current-node-key": `00000000-0000-0000-0000-000000000000`,
                props: $props.nodeProps,
                data: _ctx.marketingCenters,
                "node-key": "id",
                "highlight-current": "",
                onCurrentChange: $setup.marketingCentersChange,
                "expand-on-click-node": false,
                "check-strictly": true,
                "show-checkbox": "",
                onCheck: $setup.marketingCentersClick,
                style: { height: "300px", overflow: "auto" },
                "default-checked-keys": $setup.keys3,
                draggable: true,
                onNodeDragStart: $setup.handleDragStart,
                "allow-drop": $setup.collapse
              }, {
                default: withCtx(({ node, data }) => [
                  createElementVNode("span", null, toDisplayString(data.name), 1)
                ]),
                _: 1
              }, 8, ["props", "data", "onCurrentChange", "onCheck", "default-checked-keys", "onNodeDragStart", "allow-drop"])), [
                [_directive_loading, $setup.loading3]
              ])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border"
          }, {
            default: withCtx(() => [
              withDirectives((openBlock(), createBlock(_component_el_tree, {
                ref: "treeRef4",
                "current-node-key": `00000000-0000-0000-0000-000000000000`,
                props: $props.nodeProps,
                data: _ctx.saleRegions,
                "node-key": "id",
                "highlight-current": "",
                onCurrentChange: $setup.saleRegionsChange,
                "expand-on-click-node": false,
                "check-strictly": true,
                "show-checkbox": "",
                onCheck: $setup.saleRegionsClick,
                style: { height: "300px", overflow: "auto" },
                "default-checked-keys": $setup.keys4,
                draggable: true,
                onNodeDragStart: $setup.handleDragStart,
                "allow-drop": $setup.collapse
              }, {
                default: withCtx(({ node, data }) => [
                  createElementVNode("span", null, toDisplayString(data.name), 1)
                ]),
                _: 1
              }, 8, ["props", "data", "onCurrentChange", "onCheck", "default-checked-keys", "onNodeDragStart", "allow-drop"])), [
                [_directive_loading, $setup.loading4]
              ])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border"
          }, {
            default: withCtx(() => [
              withDirectives((openBlock(), createBlock(_component_el_tree, {
                ref: "treeRef5",
                "current-node-key": `00000000-0000-0000-0000-000000000000`,
                props: $props.nodeProps,
                data: _ctx.departments,
                "node-key": "id",
                "highlight-current": "",
                onCurrentChange: $setup.departmentsChange,
                "expand-on-click-node": false,
                "check-strictly": true,
                "show-checkbox": "",
                onCheck: $setup.departmentsClick,
                style: { height: "300px", overflow: "auto" },
                "default-checked-keys": $setup.keys5,
                draggable: true,
                onNodeDragStart: $setup.handleDragStart,
                "allow-drop": $setup.collapse
              }, {
                default: withCtx(({ node, data }) => [
                  createElementVNode("span", null, toDisplayString(data.name), 1)
                ]),
                _: 1
              }, 8, ["props", "data", "onCurrentChange", "onCheck", "default-checked-keys", "onNodeDragStart", "allow-drop"])), [
                [_directive_loading, $setup.loading5]
              ])
            ]),
            _: 1
          }),
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border"
          }, {
            default: withCtx(() => [
              withDirectives((openBlock(), createBlock(_component_el_tree, {
                ref: "treeRef6",
                "current-node-key": `00000000-0000-0000-0000-000000000000`,
                props: $props.nodeProps,
                data: _ctx.stations,
                "node-key": "id",
                "highlight-current": "",
                onCurrentChange: $setup.stationsChange,
                "expand-on-click-node": false,
                "check-strictly": true,
                "show-checkbox": "",
                onCheck: $setup.stationsClick,
                style: { height: "300px", overflow: "auto" },
                "default-checked-keys": $setup.keys6,
                draggable: true,
                onNodeDragStart: $setup.handleDragStart,
                "allow-drop": $setup.collapse
              }, {
                default: withCtx(({ node, data }) => [
                  createElementVNode("span", null, toDisplayString(data.name), 1)
                ]),
                _: 1
              }, 8, ["props", "data", "onCurrentChange", "onCheck", "default-checked-keys", "onNodeDragStart", "allow-drop"])), [
                [_directive_loading, $setup.loading6]
              ])
            ]),
            _: 1
          })
        ]),
        _: 1
      }),
      createVNode(_component_el_row, {
        class: "m-roleorg-tree2",
        style: { height: "270px" }
      }, {
        default: withCtx(() => [
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border m-roleorg-tag",
            onDrop: _cache[12] || (_cache[12] = ($event) => $setup.drop($event, 1)),
            onDragover: _cache[13] || (_cache[13] = ($event) => $setup.allowDrop($event))
          }, {
            default: withCtx(() => [
              _ctx.headOfficeSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.headOfficeSelections, (tag) => {
                return openBlock(), createBlock(_component_el_tag, {
                  key: tag.id,
                  class: "mx-1",
                  closable: "",
                  onClose: ($event) => $setup.handleClose(tag, _ctx.headOfficeSelections, 1)
                }, {
                  default: withCtx(() => [
                    createTextVNode(toDisplayString(tag.name), 1)
                  ]),
                  _: 2
                }, 1032, ["onClose"]);
              }), 128)) : createCommentVNode("", true),
              _ctx.headOfficeSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_1$1, _hoisted_3)) : createCommentVNode("", true)
            ]),
            _: 1
          }),
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border m-roleorg-tag",
            onDrop: _cache[14] || (_cache[14] = ($event) => $setup.drop($event, 2)),
            onDragover: _cache[15] || (_cache[15] = ($event) => $setup.allowDrop($event))
          }, {
            default: withCtx(() => [
              _ctx.dbsSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.dbsSelections, (tag) => {
                return openBlock(), createBlock(_component_el_tag, {
                  key: tag.id,
                  class: "mx-1",
                  closable: "",
                  onClose: ($event) => $setup.handleClose(tag, _ctx.dbsSelections, 2)
                }, {
                  default: withCtx(() => [
                    createTextVNode(toDisplayString(tag.name), 1)
                  ]),
                  _: 2
                }, 1032, ["onClose"]);
              }), 128)) : createCommentVNode("", true),
              _ctx.dbsSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_4, _hoisted_6)) : createCommentVNode("", true)
            ]),
            _: 1
          }),
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border m-roleorg-tag",
            onDrop: _cache[16] || (_cache[16] = ($event) => $setup.drop($event, 3)),
            onDragover: _cache[17] || (_cache[17] = ($event) => $setup.allowDrop($event))
          }, {
            default: withCtx(() => [
              _ctx.marketingCentersSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.marketingCentersSelections, (tag) => {
                return openBlock(), createBlock(_component_el_tag, {
                  key: tag.id,
                  class: "mx-1",
                  closable: "",
                  onClose: ($event) => $setup.handleClose(tag, _ctx.marketingCentersSelections, 3)
                }, {
                  default: withCtx(() => [
                    createTextVNode(toDisplayString(tag.name), 1)
                  ]),
                  _: 2
                }, 1032, ["onClose"]);
              }), 128)) : createCommentVNode("", true),
              _ctx.marketingCentersSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_7, _hoisted_9)) : createCommentVNode("", true)
            ]),
            _: 1
          }),
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border m-roleorg-tag",
            onDrop: _cache[18] || (_cache[18] = ($event) => $setup.drop($event, 4)),
            onDragover: _cache[19] || (_cache[19] = ($event) => $setup.allowDrop($event))
          }, {
            default: withCtx(() => [
              _ctx.saleRegionsSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.saleRegionsSelections, (tag) => {
                return openBlock(), createBlock(_component_el_tag, {
                  key: tag.id,
                  class: "mx-1",
                  closable: "",
                  onClose: ($event) => $setup.handleClose(tag, _ctx.saleRegionsSelections, 4)
                }, {
                  default: withCtx(() => [
                    createTextVNode(toDisplayString(tag.name), 1)
                  ]),
                  _: 2
                }, 1032, ["onClose"]);
              }), 128)) : createCommentVNode("", true),
              _ctx.saleRegionsSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_10, _hoisted_12)) : createCommentVNode("", true)
            ]),
            _: 1
          }),
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border m-roleorg-tag",
            onDrop: _cache[20] || (_cache[20] = ($event) => $setup.drop($event, 5)),
            onDragover: _cache[21] || (_cache[21] = ($event) => $setup.allowDrop($event))
          }, {
            default: withCtx(() => [
              _ctx.departmentsSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.departmentsSelections, (tag) => {
                return openBlock(), createBlock(_component_el_tag, {
                  key: tag.id,
                  class: "mx-1",
                  closable: "",
                  onClose: ($event) => $setup.handleClose(tag, _ctx.departmentsSelections, 5)
                }, {
                  default: withCtx(() => [
                    createTextVNode(toDisplayString(tag.name), 1)
                  ]),
                  _: 2
                }, 1032, ["onClose"]);
              }), 128)) : createCommentVNode("", true),
              _ctx.departmentsSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_13, _hoisted_15)) : createCommentVNode("", true)
            ]),
            _: 1
          }),
          createVNode(_component_el_col, {
            span: 4,
            class: "m-roleorg-border m-roleorg-tag",
            onDrop: _cache[22] || (_cache[22] = ($event) => $setup.drop($event, 6)),
            onDragover: _cache[23] || (_cache[23] = ($event) => $setup.allowDrop($event))
          }, {
            default: withCtx(() => [
              _ctx.stationsSelections.length > 0 ? (openBlock(true), createElementBlock(Fragment, { key: 0 }, renderList(_ctx.stationsSelections, (tag) => {
                return openBlock(), createBlock(_component_el_tag, {
                  key: tag.id,
                  class: "mx-1",
                  closable: "",
                  type: "primary",
                  onClose: ($event) => $setup.handleClose(tag, _ctx.stationsSelections, 6)
                }, {
                  default: withCtx(() => [
                    createTextVNode(toDisplayString(tag.name), 1)
                  ]),
                  _: 2
                }, 1032, ["onClose"]);
              }), 128)) : createCommentVNode("", true),
              _ctx.stationsSelections.length == 0 ? (openBlock(), createElementBlock("div", _hoisted_16, _hoisted_18)) : createCommentVNode("", true)
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "onOpened", "loading"]);
}
var Panel = /* @__PURE__ */ _export_sfc$1(_sfc_main$3, [["render", _sfc_render$3], ["__scopeId", "data-v-a8fd9eb8"]]);
const _sfc_main$2 = {
  components: { Panel },
  props: {
    modelValue: {
      type: Object,
      default: null
    },
    placeholder: {
      type: String,
      default: ""
    }
  },
  emits: ["update"],
  setup(props, { emit }) {
    const curSelection = ref({});
    const showReset = ref(false);
    const resetMethods = inject("resetMethods", []);
    const orgval = computed({
      get() {
        return props.modelValue;
      },
      set(val) {
        emit("update", val);
      }
    });
    const showPannel = ref(false);
    const handleSelect = (val) => {
      console.log("\u9009\u62E9", val);
      orgval.value = val;
      showReset.value = true;
    };
    const reset = () => {
      orgval.value = "";
      showReset.value = false;
    };
    resetMethods.push(reset);
    return {
      orgval,
      showPannel,
      handleSelect,
      showReset,
      reset,
      curSelection
    };
  }
};
const _hoisted_1 = { class: "m-icon-picker" };
const _hoisted_2 = { class: "m-icon-picker_input" };
function _sfc_render$2(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_button = resolveComponent("m-button");
  const _component_el_button_group = resolveComponent("el-button-group");
  const _component_panel = resolveComponent("panel");
  return openBlock(), createElementBlock("section", _hoisted_1, [
    createElementVNode("div", _hoisted_2, [
      createVNode(_component_el_button_group, null, {
        default: withCtx(() => [
          createVNode(_component_m_button, {
            icon: "fold-b",
            onClick: _cache[0] || (_cache[0] = ($event) => $setup.showPannel = true)
          }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($props.placeholder), 1)
            ]),
            _: 1
          }),
          $setup.showReset ? (openBlock(), createBlock(_component_m_button, {
            key: 0,
            type: "danger",
            icon: "close",
            onClick: $setup.reset
          }, {
            default: withCtx(() => [
              createTextVNode("\u91CD\u7F6E")
            ]),
            _: 1
          }, 8, ["onClick"])) : createCommentVNode("", true)
        ]),
        _: 1
      })
    ]),
    createVNode(_component_panel, {
      selection: $setup.curSelection,
      modelValue: $setup.showPannel,
      "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.showPannel = $event),
      onSuccess: $setup.handleSelect
    }, null, 8, ["selection", "modelValue", "onSuccess"])
  ]);
}
var component_11 = /* @__PURE__ */ _export_sfc$1(_sfc_main$2, [["render", _sfc_render$2]]);
const _sfc_main$1 = {
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
function _sfc_render$1(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, {
    action: $setup.query,
    clearable: $props.clearable
  }, null, 8, ["action", "clearable"]);
}
var component_12 = /* @__PURE__ */ _export_sfc$1(_sfc_main$1, [["render", _sfc_render$1]]);
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
function _sfc_render(_ctx, _cache, $props, $setup, $data, $options) {
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
var component_13 = /* @__PURE__ */ _export_sfc$1(_sfc_main, [["render", _sfc_render]]);
const pages = [];
pages.push(page$9);
pages.push(page$8);
pages.push(page$6);
pages.push(page$5);
pages.push(page$3);
pages.push(page$1);
pages.push(page);
const components = [];
components.push({ name: "account-status-select", component: component_0 });
components.push({ name: "account-type-select", component: component_1 });
components.push({ name: "config-admin", component: component_2 });
components.push({ name: "dict-cascader", component: component_3 });
components.push({ name: "dict-select", component: component_4 });
components.push({ name: "dict-toolbar-fullscreen", component: component_5 });
components.push({ name: "enum-checkbox", component: component_6 });
components.push({ name: "enum-radio", component: component_7 });
components.push({ name: "enum-select", component: component_8 });
components.push({ name: "loginmode-select", component: component_9 });
components.push({ name: "module-select", component: component_10 });
components.push({ name: "org-picker", component: component_11 });
components.push({ name: "platform-select", component: component_12 });
components.push({ name: "role-select", component: component_13 });
const api = {};
api["account"] = api_account;
api["authorize"] = api_authorize;
api["common"] = api_common;
api["config"] = api_config;
api["dict"] = api_dict;
api["dictGroup"] = api_dictGroup;
api["dictItem"] = api_dictItem;
api["menu"] = api_menu;
api["menuGroup"] = api_menuGroup;
api["module"] = api_module;
api["morg"] = api_morg;
api["role"] = api_role;
const mod = { id: 0, code: "admin", version: "1.0.9", label: "\u6743\u9650\u7BA1\u7406\u6A21\u5757", icon: "lock", description: "CRB.TPM\u6743\u9650\u7BA1\u7406\u6A21\u5757", store, pages, components, api };
tpm.useModule(mod);
mod.callback = ({ app, config }) => {
  const { login, refreshToken, getVerifyCode, getProfile } = tpm.api.admin.authorize;
  const { updateSkin } = tpm.api.admin.account;
  config.actions.login = login;
  config.actions.refreshToken = refreshToken;
  config.actions.getVerifyCode = getVerifyCode;
  config.actions.getProfile = getProfile;
  config.actions.toggleSkin = updateSkin;
  console.log("app", app);
};
import './style.css';