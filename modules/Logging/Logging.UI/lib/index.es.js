import { withSaveProps, regex, useSave, entityBaseCols, useList, useMessage, dom } from "tpm-ui";
import { reactive, computed, ref, resolveComponent, openBlock, createBlock, mergeProps, toHandlers, withCtx, createCommentVNode, createVNode, createTextVNode, toDisplayString, toRefs, watch, createElementVNode, createElementBlock, nextTick, Fragment, renderList, resolveDynamicComponent, toRef, withModifiers, getCurrentInstance, normalizeStyle, watchEffect, inject, resolveDirective, withDirectives, renderSlot } from "vue";
const urls$a = {
  DEFAULT_PASSWORD: "Account/DefaultPassword",
  UPDATE_SKIN: "Account/UpdateSkin"
};
var api_account = (http) => {
  return {
    getDefaultPassword: () => http.get(urls$a.DEFAULT_PASSWORD),
    updateSkin: (params) => http.post(urls$a.UPDATE_SKIN, params)
  };
};
const urls$9 = {
  DETAILS: "AuditInfo/Details",
  QUERYLATESTWEEKPV: "AuditInfo/QueryLatestWeekPv",
  EXPORT: "AuditInfo/Export"
};
var api_auditInfo = (http) => {
  return {
    details: (id) => http.get(urls$9.DETAILS, id),
    queryLatestWeekPv: () => http.get(urls$9.QUERYLATESTWEEKPV),
    exportAuditInfo: (params) => http.download(urls$9.EXPORT, params)
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
const urls$8 = {
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
      return http.post(urls$8.LOGIN, data);
    },
    refreshToken: (params) => http.post(urls$8.REFRESH_TOKEN, params),
    getVerifyCode: () => http.get(urls$8.VERIFY_CODE),
    getProfile: () => http.get(urls$8.PROFILE)
  };
};
const urls$7 = {
  ENUM_OPTIONS: "Common/EnumOptions",
  PLATFORM_OPTIONS: "Common/PlatformOptions",
  LOGINMODE_SELECT: "Common/LoginModeSelect"
};
var api_common = (http) => {
  return {
    queryEnumOptions: (params) => http.get(urls$7.ENUM_OPTIONS, params),
    queryPlatformOptions: () => http.get(urls$7.PLATFORM_OPTIONS),
    queryLoginModeSelect: () => http.get(urls$7.LOGINMODE_SELECT)
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
  QUERYLOGIN: "Log/Query",
  EXPORTLOGIN: "Log/LoginExport"
};
var api_log = (http) => {
  return {
    queryLogin: (params) => http.get(urls$4.QUERYLOGIN, params),
    exportLogin: (params) => http.download(urls$4.EXPORTLOGIN, params)
  };
};
const urls$3 = {
  GROUP_SELECT: "Menu/GroupSelect",
  TREE: "Menu/Tree",
  UPDATE_SORT: "Menu/UpdateSort"
};
var api_menu = (http) => {
  return {
    getGroupSelect: () => http.get(urls$3.GROUP_SELECT),
    getTree: (params) => http.get(urls$3.TREE, params),
    updateSort: (sorts) => http.post(urls$3.UPDATE_SORT, sorts)
  };
};
const urls$2 = {
  SELECT: "MenuGroup/Select"
};
var api_menuGroup = (http) => {
  return {
    select: () => http.get(urls$2.SELECT)
  };
};
const urls$1 = {
  GET_PERMISSIONS: "Module/Permissions"
};
var api_module = (http) => {
  return {
    getPermissions: (params) => http.get(urls$1.GET_PERMISSIONS, params)
  };
};
const urls = {
  QUERY_BIND_MENUS: "Role/QueryBindMenus",
  UPDATE_BIND_MENUS: "Role/UpdateBindMenus",
  SELECT: "Role/Select"
};
var api_role = (http) => {
  return {
    queryBindMenus: (params) => http.get(urls.QUERY_BIND_MENUS, params),
    updateBindMenus: (params) => http.post(urls.UPDATE_BIND_MENUS, params),
    select: () => http.get(urls.SELECT)
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
const name$5 = "admin_account";
const icon$5 = "user";
const path$5 = "/admin/account";
const permissions$5 = [
  "admin_account_query_get",
  "admin_account_DefaultPassword_get"
];
const buttons$5 = {
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
  }
};
var page$c = {
  name: name$5,
  icon: icon$5,
  path: path$5,
  permissions: permissions$5,
  buttons: buttons$5
};
var _export_sfc = (sfc, props) => {
  const target = sfc.__vccOpts || sfc;
  for (const [key, val] of props) {
    target[key] = val;
  }
  return target;
};
const _sfc_main$w = {
  props: {
    ...withSaveProps
  },
  emits: ["success"],
  setup(props, { emit }) {
    const { store: store2, $t } = tpm;
    const api2 = tpm.api.admin.account;
    const model = reactive({
      username: "",
      password: "",
      roles: [],
      name: "",
      phone: "",
      email: ""
    });
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
    const { isEdit, bind, on } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = nameRef;
    bind.width = "700px";
    const defaultPassword = ref("");
    api2.getDefaultPassword().then((data) => {
      defaultPassword.value = data;
    });
    const handleSuccess = () => {
      if (props.mode === "edit" && props.id === store2.state.app.profile.accountId) {
        store2.dispatch("app/profile/init", null, { root: true }).then(() => {
          emit("success");
        });
      } else {
        emit("success");
      }
    };
    const value5 = ref("");
    const query5 = () => {
      return new Promise((resolve) => {
        resolve([
          { label: "\u5F20\u4E09", value: 1 },
          { label: "\u674E\u56DB", value: 2 }
        ]);
      });
    };
    const handleChange = (val) => {
      console.log(val);
    };
    return {
      model,
      rules,
      isEdit,
      bind,
      on,
      nameRef,
      defaultPassword,
      handleSuccess,
      value5,
      query5,
      handleChange
    };
  }
};
function _sfc_render$w(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_alert = resolveComponent("el-alert");
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_col = resolveComponent("el-col");
  const _component_el_row = resolveComponent("el-row");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, toHandlers($setup.on), { onSuccess: $setup.handleSuccess }), {
    default: withCtx(() => [
      $setup.isEdit ? (openBlock(), createBlock(_component_el_alert, {
        key: 0,
        class: "m-margin-b-20",
        title: _ctx.$t("mod.admin.not_allow_edit_username"),
        type: "warning"
      }, null, 8, ["title"])) : createCommentVNode("", true),
      createVNode(_component_el_row, null, {
        default: withCtx(() => [
          createVNode(_component_el_col, { span: 12 }, {
            default: withCtx(() => [
              createVNode(_component_el_form_item, {
                label: _ctx.$t("tpm.login.username"),
                prop: "username"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    ref: "nameRef",
                    modelValue: $setup.model.username,
                    "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.username = $event),
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
                    "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.name = $event)
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
                    "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.phone = $event)
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
                label: _ctx.$t("tpm.login.password"),
                prop: "password"
              }, {
                default: withCtx(() => [
                  createVNode(_component_el_input, {
                    modelValue: $setup.model.password,
                    "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.password = $event),
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
                    "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.model.email = $event)
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
              }, null, 8, ["label"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 16, ["model", "rules", "onSuccess"]);
}
var Save$5 = /* @__PURE__ */ _export_sfc(_sfc_main$w, [["render", _sfc_render$w]]);
const _sfc_main$v = {
  components: { Save: Save$5 },
  setup() {
    const { query, remove } = tpm.api.admin.account;
    const model = reactive({ username: "", name: "", phone: "" });
    const cols = [
      { prop: "id", label: "tpm.id", width: "55", show: false },
      { prop: "username", label: "tpm.login.username" },
      { prop: "name", label: "mod.admin.name" },
      { prop: "roleName", label: "tpm.role" },
      { prop: "phone", label: "tpm.phone" },
      { prop: "email", label: "tpm.email" },
      { prop: "status", label: "tpm.status" },
      ...entityBaseCols()
    ];
    const list = useList();
    return {
      page: page$c,
      model,
      cols,
      query,
      remove,
      ...list
    };
  }
};
function _sfc_render$v(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_el_tag = resolveComponent("el-tag");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_container = resolveComponent("m-container");
  return openBlock(), createBlock(_component_m_container, null, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
        title: _ctx.$t(`tpm.routes.${$setup.page.name}`),
        icon: $setup.page.icon,
        cols: $setup.cols,
        "query-model": $setup.model,
        "query-method": $setup.query
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("tpm.login.username"),
            prop: "username"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.username,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.username = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
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
                "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.name = $event),
                clearable: ""
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
                "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.phone = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"])
        ]),
        buttons: withCtx(() => [
          createVNode(_component_m_button_add, {
            code: $setup.page.buttons.add.code,
            onClick: _ctx.add
          }, null, 8, ["code", "onClick"])
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
        operation: withCtx(({ row }) => [
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
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method"]),
      createVNode(_component_save, {
        id: _ctx.selection.id,
        modelValue: _ctx.saveVisible,
        "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => _ctx.saveVisible = $event),
        mode: _ctx.mode,
        onSuccess: _ctx.refresh
      }, null, 8, ["id", "modelValue", "mode", "onSuccess"])
    ]),
    _: 1
  });
}
var component$6 = /* @__PURE__ */ _export_sfc(_sfc_main$v, [["render", _sfc_render$v]]);
const page$b = {
  "name": "admin_account",
  "icon": "user",
  "path": "/admin/account",
  "permissions": ["admin_account_query_get", "admin_account_DefaultPassword_get"],
  "buttons": {
    "add": {
      "text": "tpm.add",
      "code": "admin_account_add",
      "permissions": ["admin_account_add_post"]
    },
    "edit": {
      "text": "tpm.edit",
      "code": "admin_account_edit",
      "permissions": ["admin_account_edit_get", "admin_account_update_post"]
    },
    "remove": {
      "text": "tpm.delete",
      "code": "admin_account_delete",
      "permissions": ["admin_account_delete_delete"]
    }
  }
};
page$b.component = component$6;
component$6.name = page$b.name;
const name$4 = "admin_auditinfo";
const icon$4 = "captcha";
const path$4 = "/admin/auditinfo";
const permissions$4 = [
  "admin_auditinfo_query_get"
];
const buttons$4 = {
  details: {
    text: "tpm.details",
    code: "admin_auditinfo_details",
    permissions: [
      "admin_auditinfo_details_get"
    ]
  },
  "export": {
    text: "tpm.export",
    code: "admin_auditinfo_export",
    permissions: [
      "admin_auditinfo_export_post"
    ]
  }
};
var page$a = {
  name: name$4,
  icon: icon$4,
  path: path$4,
  permissions: permissions$4,
  buttons: buttons$4
};
const _sfc_main$u = {
  components: {},
  props: {
    id: {
      type: Number,
      default: 0,
      required: true
    }
  },
  setup(props) {
    const message = useMessage();
    const { id } = toRefs(props);
    const { details } = tpm.api.admin.auditInfo;
    const model = ref({});
    const get = () => {
      let cid = id.value;
      if (cid === "") {
        message.success("\u8BF7\u9009\u62E9\u8981\u67E5\u770B\u7684\u6570\u636E~");
        return;
      }
      details({ id: cid }).then((data) => {
        model.value = data;
      });
    };
    const drawer = {
      header: true,
      title: "\u5BA1\u8BA1\u65E5\u5FD7\u8BE6\u60C5",
      icon: "log",
      width: "600px",
      draggable: true
    };
    watch(id, () => {
      if (id.value > 0) {
        get();
      }
    });
    return {
      drawer,
      model,
      get,
      details
    };
  }
};
function _sfc_render$u(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form = resolveComponent("el-form");
  const _component_m_form_drawer = resolveComponent("m-form-drawer");
  return openBlock(), createBlock(_component_m_form_drawer, {
    title: _ctx.$t("\u5BA1\u8BA1\u65E5\u5FD7\u8BE6\u60C5"),
    icon: "captcha",
    width: "30%",
    model: $setup.model,
    "label-width": "130px",
    "loading-text": _ctx.$t("\u6B63\u5728\u52A0\u8F7D\u6570\u636E\uFF0C\u8BF7\u7A0D\u540E..."),
    btnOk: false,
    btnReset: false
  }, {
    default: withCtx(() => [
      createVNode(_component_el_form, {
        "label-width": "120px",
        disabled: ""
      }, {
        default: withCtx(() => [
          createVNode(_component_el_form_item, { label: "\u8D26\u6237\uFF1A" }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.model.accountName), 1)
            ]),
            _: 1
          }),
          createVNode(_component_el_form_item, { label: "\u6A21\u5757\uFF1A" }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.model.module) + "(" + toDisplayString($setup.model.area) + ")", 1)
            ]),
            _: 1
          }),
          createVNode(_component_el_form_item, { label: "\u63A7\u5236\u5668\uFF1A" }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.model.controller) + "(" + toDisplayString($setup.model.controllerDesc) + ")", 1)
            ]),
            _: 1
          }),
          createVNode(_component_el_form_item, { label: "\u65B9\u6CD5\uFF1A" }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.model.action) + "(" + toDisplayString($setup.model.actionDesc) + ")", 1)
            ]),
            _: 1
          }),
          createVNode(_component_el_form_item, { label: "\u6267\u884C\u65F6\u95F4\uFF1A" }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.model.executionTime), 1)
            ]),
            _: 1
          }),
          createVNode(_component_el_form_item, { label: "\u7528\u65F6\uFF1A" }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.model.executionDuration) + "ms", 1)
            ]),
            _: 1
          }),
          createVNode(_component_el_form_item, { label: "IP\uFF1A" }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.model.ip), 1)
            ]),
            _: 1
          }),
          createVNode(_component_el_form_item, { label: "\u5E73\u53F0\uFF1A" }, {
            default: withCtx(() => [
              createTextVNode(toDisplayString($setup.model.platformName), 1)
            ]),
            _: 1
          }),
          createVNode(_component_el_form_item, { label: "\u6D4F\u89C8\u5668\u4FE1\u606F\uFF1A" }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                type: "textarea",
                value: $setup.model.browserInfo,
                rows: 5
              }, null, 8, ["value"])
            ]),
            _: 1
          }),
          createVNode(_component_el_form_item, { label: "\u53C2\u6570\uFF1A" }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                type: "textarea",
                value: $setup.model.parameters,
                rows: 10
              }, null, 8, ["value"])
            ]),
            _: 1
          }),
          createVNode(_component_el_form_item, { label: "\u7ED3\u679C\uFF1A" }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                type: "textarea",
                value: $setup.model.result,
                rows: 10
              }, null, 8, ["value"])
            ]),
            _: 1
          })
        ]),
        _: 1
      })
    ]),
    _: 1
  }, 8, ["title", "model", "loading-text"]);
}
var Detail$1 = /* @__PURE__ */ _export_sfc(_sfc_main$u, [["render", _sfc_render$u]]);
var index_vue_vue_type_style_index_0_lang$8 = "";
const _sfc_main$t = {
  components: { Detail: Detail$1 },
  setup() {
    const listRef = ref();
    const current = ref();
    const showDetailDialog = ref(false);
    const { query, exportAuditInfo } = tpm.api.admin.auditInfo;
    const model = ref({
      accountId: null,
      moduleCode: "",
      controller: null,
      action: null,
      platform: "",
      startDate: null,
      endDate: null
    });
    const cols = [
      {
        prop: "id",
        label: "\u7F16\u53F7",
        show: false
      },
      {
        prop: "accountName",
        label: "\u8D26\u6237",
        export: {
          width: 15
        }
      },
      {
        prop: "module",
        label: "\u6A21\u5757",
        export: {
          width: 15
        }
      },
      {
        prop: "controller",
        label: "\u63A7\u5236\u5668",
        export: {
          width: 15
        }
      },
      {
        prop: "action",
        label: "\u65B9\u6CD5",
        export: {
          width: 15
        }
      },
      {
        prop: "platformName",
        label: "\u5E73\u53F0",
        export: {
          width: 15
        }
      },
      {
        prop: "ip",
        label: "IP",
        export: {
          width: 15
        }
      },
      {
        prop: "executionTime",
        label: "\u6267\u884C\u65F6\u95F4",
        export: {
          width: 20
        }
      },
      {
        prop: "executionDuration",
        label: "\u6267\u884C\u7528\u65F6(ms)",
        export: {
          width: 15
        }
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    const details = (row) => {
      console.log("details--------------->" + row.id);
      current.value = row.id;
    };
    const openDetails = (row) => {
      showDetailDialog.value = true;
      console.log("details--------------->" + row.id);
      current.value = row.id;
    };
    return {
      current,
      showDetailDialog,
      listRef,
      refresh,
      page: page$a,
      model,
      cols,
      query,
      exportAuditInfo,
      ...list,
      details,
      openDetails
    };
  }
};
function _sfc_render$t(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_admin_module_select = resolveComponent("m-admin-module-select");
  const _component_m_admin_platform_select = resolveComponent("m-admin-platform-select");
  const _component_m_date_range_picker = resolveComponent("m-date-range-picker");
  const _component_m_button = resolveComponent("m-button");
  const _component_m_list = resolveComponent("m-list");
  const _component_detail = resolveComponent("detail");
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
        "show-export": "",
        "export-method": $setup.exportAuditInfo
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("\u8D26\u6237\u7F16\u53F7"),
            prop: "accountId"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.accountId,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.accountId = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_el_form_item, {
            label: _ctx.$t("\u6A21\u5757"),
            prop: "moduleCode"
          }, {
            default: withCtx(() => [
              createVNode(_component_m_admin_module_select, {
                modelValue: $setup.model.moduleCode,
                "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.moduleCode = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_el_form_item, {
            label: _ctx.$t("\u63A7\u5236\u5668"),
            prop: "controller"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.controller,
                "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.controller = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_el_form_item, {
            label: _ctx.$t("\u64CD\u4F5C"),
            prop: "action"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.action,
                "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.action = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_el_form_item, {
            label: _ctx.$t("\u8BBF\u95EE\u6765\u6E90"),
            prop: "platform"
          }, {
            default: withCtx(() => [
              createVNode(_component_m_admin_platform_select, {
                modelValue: $setup.model.platform,
                "onUpdate:modelValue": _cache[4] || (_cache[4] = ($event) => $setup.model.platform = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"])
        ]),
        toolbar: withCtx(() => [
          createVNode(_component_m_date_range_picker, {
            start: $setup.model.startDate,
            end: $setup.model.endDate,
            onChange: $setup.refresh,
            class: "auditInfo-range-picker"
          }, null, 8, ["start", "end", "onChange"])
        ]),
        "col-moduleName": withCtx(({ row }) => [
          createElementVNode("span", null, toDisplayString(row.moduleName) + "(" + toDisplayString(row.area) + ")", 1)
        ]),
        "col-controller": withCtx(({ row }) => [
          createElementVNode("span", null, toDisplayString(row.controllerDesc) + "(" + toDisplayString(row.controller) + ")", 1)
        ]),
        "col-action": withCtx(({ row }) => [
          createElementVNode("span", null, toDisplayString(row.actionDesc) + "(" + toDisplayString(row.action) + ")", 1)
        ]),
        "col-executionDuration": withCtx(({ row }) => [
          createElementVNode("span", null, toDisplayString(row.executionDuration) + "ms", 1)
        ]),
        operation: withCtx(({ row }) => [
          createVNode(_component_m_button, {
            text: true,
            code: $setup.page.buttons.details.code,
            type: "primary",
            icon: "preview",
            onClick: ($event) => $setup.openDetails(row)
          }, {
            default: withCtx(() => [
              createTextVNode("\u8BE6\u7EC6 ")
            ]),
            _: 2
          }, 1032, ["code", "onClick"])
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method"]),
      createVNode(_component_detail, {
        modelValue: $setup.showDetailDialog,
        "onUpdate:modelValue": _cache[5] || (_cache[5] = ($event) => $setup.showDetailDialog = $event),
        id: $setup.current
      }, null, 8, ["modelValue", "id"])
    ]),
    _: 1
  });
}
var component$5 = /* @__PURE__ */ _export_sfc(_sfc_main$t, [["render", _sfc_render$t]]);
const page$9 = {
  "name": "admin_auditinfo",
  "icon": "captcha",
  "path": "/admin/auditinfo",
  "permissions": [
    "admin_auditinfo_query_get"
  ],
  "buttons": {
    "details": {
      "text": "tpm.details",
      "code": "admin_auditinfo_details",
      "permissions": [
        "admin_auditinfo_details_get"
      ]
    },
    "export": {
      "text": "tpm.export",
      "code": "admin_auditinfo_export",
      "permissions": [
        "admin_auditinfo_export_post"
      ]
    }
  }
};
page$9.component = component$5;
component$5.name = page$9.name;
const name$3 = "admin_dict";
const icon$3 = "dict";
const path$3 = "/admin/dict";
const permissions$3 = [
  "admin_dict_query_get",
  "admin_dictgroup_query_get",
  "admin_dictitem_query_get"
];
const buttons$3 = {
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
var page$8 = {
  name: name$3,
  icon: icon$3,
  path: path$3,
  permissions: permissions$3,
  buttons: buttons$3
};
const _sfc_main$s = {
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
    const { bind, on } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = nameRef;
    bind.width = "500px";
    bind.beforeSubmit = () => {
      model.groupCode = props.groupCode;
    };
    return {
      model,
      rules,
      bind,
      on,
      nameRef
    };
  }
};
function _sfc_render$s(_ctx, _cache, $props, $setup, $data, $options) {
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
var Save$4 = /* @__PURE__ */ _export_sfc(_sfc_main$s, [["render", _sfc_render$s]]);
const _sfc_main$r = {
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
const _hoisted_1$4 = { class: "m-padding-10" };
function _sfc_render$r(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_icon = resolveComponent("m-icon");
  const _component_el_tree = resolveComponent("el-tree");
  return openBlock(), createElementBlock("div", _hoisted_1$4, [
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
var Tree = /* @__PURE__ */ _export_sfc(_sfc_main$r, [["render", _sfc_render$r]]);
var index_vue_vue_type_style_index_0_lang$7 = "";
const _sfc_main$q = {
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
    const { bind, on } = useSave({ props, api: api2, model, emit });
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
      on,
      nameRef,
      toolbars
    };
  }
};
const _hoisted_1$3 = { class: "m-admin-dict-extend" };
const _hoisted_2$3 = { class: "m-admin-dict-extend_toolbar" };
const _hoisted_3$2 = { class: "m-admin-dict-extend_content" };
function _sfc_render$q(_ctx, _cache, $props, $setup, $data, $options) {
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
          createElementVNode("div", _hoisted_1$3, [
            createElementVNode("div", _hoisted_2$3, [
              (openBlock(true), createElementBlock(Fragment, null, renderList($setup.toolbars, (toolbar) => {
                return openBlock(), createBlock(resolveDynamicComponent(toolbar), {
                  key: toolbar,
                  modelValue: $setup.model.extend,
                  "onUpdate:modelValue": _cache[3] || (_cache[3] = ($event) => $setup.model.extend = $event)
                }, null, 8, ["modelValue"]);
              }), 128))
            ]),
            createElementVNode("div", _hoisted_3$2, [
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
var Save$3 = /* @__PURE__ */ _export_sfc(_sfc_main$q, [["render", _sfc_render$q]]);
const _sfc_main$p = {
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
      buttons: buttons$3,
      model,
      cols,
      query,
      remove,
      ...list,
      handleChange
    };
  }
};
function _sfc_render$p(_ctx, _cache, $props, $setup, $data, $options) {
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
var List$2 = /* @__PURE__ */ _export_sfc(_sfc_main$p, [["render", _sfc_render$p]]);
const _sfc_main$o = {
  components: { Tree, List: List$2 },
  setup() {
    const split = ref("250px");
    const parentId = ref(0);
    const treeRef = ref();
    const handleTreeChange = (id) => {
      parentId.value = id;
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
function _sfc_render$o(_ctx, _cache, $props, $setup, $data, $options) {
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
var ItemDialog = /* @__PURE__ */ _export_sfc(_sfc_main$o, [["render", _sfc_render$o]]);
const _sfc_main$n = {
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
      buttons: buttons$3,
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
function _sfc_render$n(_ctx, _cache, $props, $setup, $data, $options) {
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
var List$1 = /* @__PURE__ */ _export_sfc(_sfc_main$n, [["render", _sfc_render$n]]);
const _sfc_main$m = {
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
    const { bind, on } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = nameRef;
    bind.width = "700px";
    return {
      model,
      rules,
      bind,
      on,
      nameRef
    };
  }
};
function _sfc_render$m(_ctx, _cache, $props, $setup, $data, $options) {
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
var GroupSave = /* @__PURE__ */ _export_sfc(_sfc_main$m, [["render", _sfc_render$m]]);
const _sfc_main$l = {
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
    return { page: page$8, current, listBoxRef, selection, mode, saveVisible, add, edit, refresh, handleGroupChange };
  }
};
function _sfc_render$l(_ctx, _cache, $props, $setup, $data, $options) {
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
var component$4 = /* @__PURE__ */ _export_sfc(_sfc_main$l, [["render", _sfc_render$l]]);
const page$7 = {
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
page$7.component = component$4;
component$4.name = page$7.name;
const name$2 = "admin_log";
const icon$2 = "form";
const path$2 = "/admin/log";
const permissions$2 = [
  "admin_log_query_get"
];
const buttons$2 = {
  "export": {
    text: "tpm.export",
    code: "admin_log_loginexport",
    permissions: [
      "admin_log_loginexport_post"
    ]
  }
};
var page$6 = {
  name: name$2,
  icon: icon$2,
  path: path$2,
  permissions: permissions$2,
  buttons: buttons$2
};
var index_vue_vue_type_style_index_0_lang$6 = "";
const _sfc_main$k = {
  components: {},
  setup() {
    const listRef = ref();
    const { query, exportLogin } = tpm.api.admin.log;
    const model = ref({
      accountId: null,
      platform: null,
      loginMode: null,
      startDate: null,
      endDate: null
    });
    const cols = [
      {
        prop: "id",
        label: "\u7F16\u53F7",
        show: false
      },
      {
        prop: "platformName",
        label: "\u767B\u5F55\u5E73\u53F0"
      },
      {
        prop: "loginModeName",
        label: "\u767B\u5F55\u65B9\u5F0F"
      },
      {
        prop: "userName",
        label: "\u7528\u6237\u540D"
      },
      {
        prop: "email",
        label: "\u90AE\u7BB1"
      },
      {
        prop: "phone",
        label: "\u624B\u673A\u53F7"
      },
      {
        prop: "loginTime",
        label: "\u767B\u5F55\u65F6\u95F4"
      },
      {
        prop: "ip",
        label: "IP"
      },
      {
        prop: "success",
        label: "\u7ED3\u679C"
      },
      {
        prop: "userAgent",
        label: "UA"
      }
    ];
    const list = useList();
    const refresh = () => {
      listRef.value.refresh();
    };
    return {
      listRef,
      refresh,
      page: page$6,
      model,
      cols,
      query,
      exportLogin,
      ...list
    };
  }
};
function _sfc_render$k(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_admin_platform_select = resolveComponent("m-admin-platform-select");
  const _component_m_admin_loginmode_select = resolveComponent("m-admin-loginmode-select");
  const _component_m_date_range_picker = resolveComponent("m-date-range-picker");
  const _component_el_tag = resolveComponent("el-tag");
  const _component_m_list = resolveComponent("m-list");
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
        "show-export": "",
        "export-method": $setup.exportLogin
      }, {
        querybar: withCtx(() => [
          createVNode(_component_el_form_item, {
            label: _ctx.$t("\u8D26\u6237\u7F16\u53F7"),
            prop: "accountId"
          }, {
            default: withCtx(() => [
              createVNode(_component_el_input, {
                modelValue: $setup.model.accountId,
                "onUpdate:modelValue": _cache[0] || (_cache[0] = ($event) => $setup.model.accountId = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_el_form_item, {
            label: _ctx.$t("\u767B\u5F55\u5E73\u53F0"),
            prop: "platform"
          }, {
            default: withCtx(() => [
              createVNode(_component_m_admin_platform_select, {
                modelValue: $setup.model.platform,
                "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.platform = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"]),
          createVNode(_component_el_form_item, {
            label: _ctx.$t("\u767B\u5F55\u65B9\u5F0F"),
            prop: "loginMode"
          }, {
            default: withCtx(() => [
              createVNode(_component_m_admin_loginmode_select, {
                modelValue: $setup.model.loginMode,
                "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.loginMode = $event),
                clearable: ""
              }, null, 8, ["modelValue"])
            ]),
            _: 1
          }, 8, ["label"])
        ]),
        toolbar: withCtx(() => [
          createVNode(_component_m_date_range_picker, {
            start: $setup.model.startDate,
            end: $setup.model.endDate,
            onChange: $setup.refresh,
            class: "auditInfo-range-picker"
          }, null, 8, ["start", "end", "onChange"])
        ]),
        "col-success": withCtx(({ row }) => [
          row.success === 0 ? (openBlock(), createBlock(_component_el_tag, {
            key: 0,
            type: "info",
            size: "small",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode(" \u5931\u8D25 ")
            ]),
            _: 1
          })) : (openBlock(), createBlock(_component_el_tag, {
            key: 1,
            type: "success",
            size: "small",
            effect: "dark"
          }, {
            default: withCtx(() => [
              createTextVNode("\u6210\u529F")
            ]),
            _: 1
          }))
        ]),
        _: 1
      }, 8, ["title", "icon", "cols", "query-model", "query-method", "export-method"])
    ]),
    _: 1
  });
}
var component$3 = /* @__PURE__ */ _export_sfc(_sfc_main$k, [["render", _sfc_render$k]]);
const page$5 = {
  "name": "admin_log",
  "icon": "form",
  "path": "/admin/log",
  "permissions": ["admin_log_query_get"],
  "buttons": {
    "export": {
      "text": "tpm.export",
      "code": "admin_log_loginexport",
      "permissions": ["admin_log_loginexport_post"]
    }
  }
};
page$5.component = component$3;
component$3.name = page$5.name;
const _sfc_main$j = {
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
      groupId: 0,
      parentId: 0,
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
    const { bind, on } = useSave({ props, api: api2, model, emit });
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
    const handleModuleSelectChange = (code, mod2) => {
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
      on,
      state: state2,
      handleModuleSelectChange,
      handleRouteChange
    };
  }
};
function _sfc_render$j(_ctx, _cache, $props, $setup, $data, $options) {
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
var Save$2 = /* @__PURE__ */ _export_sfc(_sfc_main$j, [["render", _sfc_render$j]]);
const name$1 = "admin_menu";
const icon$1 = "menu";
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
  name: name$1,
  icon: icon$1,
  path: path$1,
  permissions: permissions$1,
  buttons: buttons$1
};
const _sfc_main$i = {
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
function _sfc_render$i(_ctx, _cache, $props, $setup, $data, $options) {
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
var List = /* @__PURE__ */ _export_sfc(_sfc_main$i, [["render", _sfc_render$i]]);
const _sfc_main$h = {
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
    const { bind, on } = useSave({ props, api: api2, model, rules, emit });
    bind.autoFocusRef = nameRef;
    bind.width = "700px";
    return {
      model,
      rules,
      bind,
      on,
      nameRef
    };
  }
};
function _sfc_render$h(_ctx, _cache, $props, $setup, $data, $options) {
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
var Save$1 = /* @__PURE__ */ _export_sfc(_sfc_main$h, [["render", _sfc_render$h]]);
const _sfc_main$g = {
  components: { Save: Save$1 },
  emits: ["change"],
  setup(props, { emit }) {
    const { query, remove } = tpm.api.admin.menuGroup;
    const model = reactive({ name: "" });
    const cols = [{ prop: "id", label: "tpm.id", width: "55", show: false }, { prop: "name", label: "tpm.name" }, { prop: "remarks", label: "tpm.remarks" }, ...entityBaseCols()];
    const list = useList();
    const handleChange = () => {
      list.refresh();
      emit("change");
    };
    return {
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
function _sfc_render$g(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_input = resolveComponent("el-input");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_m_button_add = resolveComponent("m-button-add");
  const _component_m_button_edit = resolveComponent("m-button-edit");
  const _component_m_button_delete = resolveComponent("m-button-delete");
  const _component_m_list = resolveComponent("m-list");
  const _component_save = resolveComponent("save");
  const _component_m_drawer = resolveComponent("m-drawer");
  return openBlock(), createBlock(_component_m_drawer, {
    title: _ctx.$t("mod.admin.manage_group"),
    icon: "list",
    width: "900px",
    "no-scrollbar": ""
  }, {
    default: withCtx(() => [
      createVNode(_component_m_list, {
        ref: "listRef",
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
var Group = /* @__PURE__ */ _export_sfc(_sfc_main$g, [["render", _sfc_render$g]]);
var index_vue_vue_type_style_index_0_lang$5 = "";
const _sfc_main$f = {
  components: { List, Group },
  setup() {
    const api2 = tpm.api.admin.menu;
    getCurrentInstance().proxy;
    const group = reactive({ id: 0, name: "" });
    const parent = reactive({
      id: 0,
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
            id: 0,
            label: group.name,
            children: data,
            path: [],
            item: {
              id: 0,
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
      let { item, id } = d;
      if (item == null && d.data != null) {
        item = d.data.item;
        parent.id = item.id;
        parent.locales = item.locales;
        parent.type = item.type;
      } else {
        parent.id = id;
        parent.locales = item.locales;
        parent.type = item.type;
      }
    };
    const handleTreeAllowDrag = (draggingNode) => {
      return draggingNode.data.id > 0;
    };
    const handleTreeAllowDrop = (draggingNode, dropNode, type) => {
      if (dropNode.data.id === 0) {
        return false;
      }
      if (type === "inner" && dropNode.data.item.type !== 0) {
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
function _sfc_render$f(_ctx, _cache, $props, $setup, $data, $options) {
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
var component$2 = /* @__PURE__ */ _export_sfc(_sfc_main$f, [["render", _sfc_render$f]]);
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
const _sfc_main$e = {
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
const _hoisted_1$2 = { class: "m-margin-l-5" };
const _hoisted_2$2 = { class: "m-margin-b-15" };
const _hoisted_3$1 = { class: "m-margin-l-10" };
const _hoisted_4$1 = { class: "m-margin-l-5" };
function _sfc_render$e(_ctx, _cache, $props, $setup, $data, $options) {
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
              createElementVNode("span", _hoisted_1$2, toDisplayString(_ctx.$t("mod.admin.page_info")), 1)
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
                      createElementVNode("h2", _hoisted_2$2, toDisplayString(_ctx.$t("mod.admin.relation_button")), 1),
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
                      createElementVNode("span", _hoisted_3$1, toDisplayString(row.icon), 1)
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
              createElementVNode("span", _hoisted_4$1, toDisplayString(_ctx.$t("mod.admin.permission_info")), 1)
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
var Detail = /* @__PURE__ */ _export_sfc(_sfc_main$e, [["render", _sfc_render$e]]);
const name = "admin_module";
const icon = "module";
const path = "/admin/module";
const permissions = [
  "admin_Module_Permissions_get"
];
var page$2 = {
  name,
  icon,
  path,
  permissions
};
var index_vue_vue_type_style_index_0_lang$3 = "";
const _sfc_main$d = {
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
const _hoisted_1$1 = { class: "m-margin-l-10 m-font-12" };
const _hoisted_2$1 = { class: "m-margin-lr-5 m-text-primary m-font-14" };
const _hoisted_3 = ["onClick"];
const _hoisted_4 = { class: "item_wrapper" };
const _hoisted_5 = { class: "item_icon" };
const _hoisted_6 = ["src"];
const _hoisted_7 = { class: "item_info" };
const _hoisted_8 = { class: "item_title" };
function _sfc_render$d(_ctx, _cache, $props, $setup, $data, $options) {
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
          createElementVNode("span", _hoisted_1$1, [
            createTextVNode(toDisplayString(_ctx.$t("mod.admin.module_total_prefix")), 1),
            createElementVNode("span", _hoisted_2$1, toDisplayString($setup.modules.length), 1),
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
              createElementVNode("div", _hoisted_4, [
                createElementVNode("div", {
                  class: "item_bar",
                  style: normalizeStyle({ backgroundColor: mod2.color })
                }, null, 4),
                createElementVNode("div", _hoisted_5, [
                  createElementVNode("div", {
                    style: normalizeStyle({ backgroundColor: mod2.color })
                  }, [
                    !mod2.icon ? (openBlock(), createBlock(_component_m_icon, {
                      key: 0,
                      name: "app"
                    })) : mod2.icon.startsWith("http://") || mod2.icon.startsWith("https://") ? (openBlock(), createElementBlock("img", {
                      key: 1,
                      src: mod2.icon
                    }, null, 8, _hoisted_6)) : (openBlock(), createBlock(_component_m_icon, {
                      key: 2,
                      name: mod2.icon
                    }, null, 8, ["name"]))
                  ], 4)
                ]),
                createElementVNode("div", _hoisted_7, [
                  createElementVNode("div", _hoisted_8, [
                    createElementVNode("span", null, toDisplayString(mod2.id) + "_" + toDisplayString(mod2.label), 1)
                  ]),
                  createElementVNode("div", null, toDisplayString(_ctx.$t("tpm.code")) + "\uFF1A" + toDisplayString(mod2.code), 1),
                  createElementVNode("div", null, toDisplayString(_ctx.$t("mod.admin.version")) + "\uFF1A" + toDisplayString(mod2.version), 1),
                  createElementVNode("div", null, toDisplayString(_ctx.$t("mod.admin.description")) + "\uFF1A" + toDisplayString(mod2.description), 1)
                ])
              ])
            ], 8, _hoisted_3);
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
var component$1 = /* @__PURE__ */ _export_sfc(_sfc_main$d, [["render", _sfc_render$d]]);
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
const _sfc_main$c = {
  props: withSaveProps,
  emits: ["success"],
  setup(props, { emit }) {
    const {
      $t,
      api: {
        admin: { role: api2 }
      }
    } = tpm;
    const model = reactive({ menuGroupId: "", name: "", code: "", remarks: "" });
    const rules = computed(() => {
      return {
        menuGroupId: [{ required: true, message: $t("mod.admin.select_menu_group") }],
        name: [{ required: true, message: $t("mod.admin.input_role_name") }],
        code: [{ required: true, message: $t("mod.admin.input_role_code") }]
      };
    });
    const nameRef = ref(null);
    const { bind, on } = useSave({ props, api: api2, model, emit });
    bind.autoFocusRef = nameRef;
    bind.width = "500px";
    return {
      model,
      rules,
      bind,
      on,
      nameRef
    };
  }
};
function _sfc_render$c(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  const _component_el_form_item = resolveComponent("el-form-item");
  const _component_el_input = resolveComponent("el-input");
  const _component_m_form_dialog = resolveComponent("m-form-dialog");
  return openBlock(), createBlock(_component_m_form_dialog, mergeProps({
    model: $setup.model,
    rules: $setup.rules
  }, $setup.bind, toHandlers($setup.on)), {
    default: withCtx(() => [
      createVNode(_component_el_form_item, {
        label: _ctx.$t("mod.admin.menu_group"),
        prop: "menuGroupId"
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
      }, 8, ["label"]),
      createVNode(_component_el_form_item, {
        label: _ctx.$t("tpm.name"),
        prop: "name"
      }, {
        default: withCtx(() => [
          createVNode(_component_el_input, {
            ref: "nameRef",
            modelValue: $setup.model.name,
            "onUpdate:modelValue": _cache[1] || (_cache[1] = ($event) => $setup.model.name = $event)
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
            "onUpdate:modelValue": _cache[2] || (_cache[2] = ($event) => $setup.model.code = $event)
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
var Save = /* @__PURE__ */ _export_sfc(_sfc_main$c, [["render", _sfc_render$c]]);
var index_vue_vue_type_style_index_0_lang$2 = "";
const _sfc_main$b = {
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
      treeRef.value.getCheckedNodes().forEach((n) => {
        if (n.id !== 0) {
          const menu = {
            menuId: n.id,
            menuType: n.item.type,
            buttons: [],
            permissions: []
          };
          if (n.item.type === 1) {
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
const _hoisted_1 = { class: "m-admin-bind-menu_label" };
const _hoisted_2 = { class: "m-admin-bind-menu_buttons" };
function _sfc_render$b(_ctx, _cache, $props, $setup, $data, $options) {
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
              createElementVNode("span", _hoisted_1, toDisplayString(data.item.locales[_ctx.$i18n.locale] || node.label), 1),
              createElementVNode("div", _hoisted_2, [
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
var BindMenu = /* @__PURE__ */ _export_sfc(_sfc_main$b, [["render", _sfc_render$b]]);
var index_vue_vue_type_style_index_0_lang$1 = "";
const _sfc_main$a = {
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
      console.log(listBoxRef.value);
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
function _sfc_render$a(_ctx, _cache, $props, $setup, $data, $options) {
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
                onChange: $setup.handleRoleChange
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
                                label: _ctx.$t("tpm.code")
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.code), 1)
                                ]),
                                _: 1
                              }, 8, ["label"]),
                              createVNode(_component_el_descriptions_item, {
                                label: _ctx.$t("mod.admin.bind_menu_group")
                              }, {
                                default: withCtx(() => [
                                  createTextVNode(toDisplayString($setup.current.menuGroupName), 1)
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
var component = /* @__PURE__ */ _export_sfc(_sfc_main$a, [["render", _sfc_render$a]]);
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
const _sfc_main$9 = {
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
function _sfc_render$9(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_cascader = resolveComponent("m-cascader");
  return openBlock(), createBlock(_component_m_cascader, { action: $setup.query }, null, 8, ["action"]);
}
var component_0 = /* @__PURE__ */ _export_sfc(_sfc_main$9, [["render", _sfc_render$9]]);
const _sfc_main$8 = {
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
function _sfc_render$8(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, { action: $setup.query }, null, 8, ["action"]);
}
var component_1 = /* @__PURE__ */ _export_sfc(_sfc_main$8, [["render", _sfc_render$8]]);
var index_vue_vue_type_style_index_0_lang = "";
const _sfc_main$7 = {
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
function _sfc_render$7(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_button = resolveComponent("m-button");
  return openBlock(), createBlock(_component_m_button, {
    icon: $setup.isFullscreen ? "full-screen-exit" : "full-screen",
    onClick: $setup.handleClick
  }, null, 8, ["icon", "onClick"]);
}
var component_2 = /* @__PURE__ */ _export_sfc(_sfc_main$7, [["render", _sfc_render$7]]);
const _sfc_main$6 = {
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
function _sfc_render$6(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_checkbox = resolveComponent("m-checkbox");
  return openBlock(), createBlock(_component_m_checkbox, { action: $setup.query }, null, 8, ["action"]);
}
var component_3 = /* @__PURE__ */ _export_sfc(_sfc_main$6, [["render", _sfc_render$6]]);
const _sfc_main$5 = {
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
function _sfc_render$5(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_radio = resolveComponent("m-radio");
  return openBlock(), createBlock(_component_m_radio, { action: $setup.query }, null, 8, ["action"]);
}
var component_4 = /* @__PURE__ */ _export_sfc(_sfc_main$5, [["render", _sfc_render$5]]);
const _sfc_main$4 = {
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
function _sfc_render$4(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, {
    action: $setup.query,
    clearable: $props.clearable
  }, null, 8, ["action", "clearable"]);
}
var component_5 = /* @__PURE__ */ _export_sfc(_sfc_main$4, [["render", _sfc_render$4]]);
const _sfc_main$3 = {
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
function _sfc_render$3(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, {
    action: $setup.query,
    clearable: $props.clearable
  }, null, 8, ["action", "clearable"]);
}
var component_6 = /* @__PURE__ */ _export_sfc(_sfc_main$3, [["render", _sfc_render$3]]);
const _sfc_main$2 = {
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
function _sfc_render$2(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_m_select = resolveComponent("m-select");
  return openBlock(), createBlock(_component_m_select, {
    action: $setup.query,
    clearable: ""
  }, null, 8, ["action"]);
}
var component_7 = /* @__PURE__ */ _export_sfc(_sfc_main$2, [["render", _sfc_render$2]]);
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
var component_8 = /* @__PURE__ */ _export_sfc(_sfc_main$1, [["render", _sfc_render$1]]);
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
        if (firstRefresh) {
          if (value_.value) {
            handleChange(value_.value);
          } else if (props.checkedFirst && data.length > 0) {
            const checkedValue = data[0].value;
            value_.value = checkedValue;
            handleChange(checkedValue);
          }
          firstRefresh = false;
        }
      }).finally(() => {
        loading.value = false;
      });
    };
    const handleChange = (val) => {
      const option = options.value.find((m) => m.value === val);
      emit("update:label", option != void 0 ? option.label : "");
      emit("change", val, option, options);
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
      handleChange
    };
  }
};
function _sfc_render(_ctx, _cache, $props, $setup, $data, $options) {
  const _component_el_option = resolveComponent("el-option");
  const _component_el_select = resolveComponent("el-select");
  const _directive_loading = resolveDirective("loading");
  return withDirectives((openBlock(), createBlock(_component_el_select, {
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
var component_9 = /* @__PURE__ */ _export_sfc(_sfc_main, [["render", _sfc_render]]);
const pages = [];
pages.push(page$b);
pages.push(page$9);
pages.push(page$7);
pages.push(page$5);
pages.push(page$3);
pages.push(page$1);
pages.push(page);
const components = [];
components.push({ name: "dict-cascader", component: component_0 });
components.push({ name: "dict-select", component: component_1 });
components.push({ name: "dict-toolbar-fullscreen", component: component_2 });
components.push({ name: "enum-checkbox", component: component_3 });
components.push({ name: "enum-radio", component: component_4 });
components.push({ name: "enum-select", component: component_5 });
components.push({ name: "loginmode-select", component: component_6 });
components.push({ name: "module-select", component: component_7 });
components.push({ name: "platform-select", component: component_8 });
components.push({ name: "role-select", component: component_9 });
const api = {};
api["account"] = api_account;
api["auditInfo"] = api_auditInfo;
api["authorize"] = api_authorize;
api["common"] = api_common;
api["dict"] = api_dict;
api["dictGroup"] = api_dictGroup;
api["dictItem"] = api_dictItem;
api["log"] = api_log;
api["menu"] = api_menu;
api["menuGroup"] = api_menuGroup;
api["module"] = api_module;
api["role"] = api_role;
const mod = { id: 0, code: "admin", version: "1.0.1", label: "\u6743\u9650\u7BA1\u7406", icon: "lock", description: "CRB.TPM\u6743\u9650\u7BA1\u7406\u6A21\u5757", store, pages, components, api };
tpm.useModule(mod);
mod.callback = ({ config }) => {
  const { login, refreshToken, getVerifyCode, getProfile } = tpm.api.admin.authorize;
  const { updateSkin } = tpm.api.admin.account;
  config.actions.login = login;
  config.actions.refreshToken = refreshToken;
  config.actions.getVerifyCode = getVerifyCode;
  config.actions.getProfile = getProfile;
  config.actions.toggleSkin = updateSkin;
};
import './style.css';