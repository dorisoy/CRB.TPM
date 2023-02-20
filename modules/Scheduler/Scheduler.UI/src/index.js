import mod from '$tpm-mod-admin'

mod.callback = ({ config }) => {
  const { login, refreshToken, getVerifyCode, getProfile } = tpm.api.admin.authorize
  const { updateSkin } = tpm.api.admin.account
  //设置登录方法
  config.actions.login = login
  //设置刷新令牌方法
  config.actions.refreshToken = refreshToken
  //设置获取验证码方法
  config.actions.getVerifyCode = getVerifyCode
  //设置获取账户信息方法
  config.actions.getProfile = getProfile
  //设置保存皮肤的方法
  config.actions.toggleSkin = updateSkin
}

/*
import api_account from '/src/api/account.js'
import api_auditInfo from '/src/api/auditInfo.js'
import api_authorize from '/src/api/authorize.js'
import api_common from '/src/api/common.js'
import api_dict from '/src/api/dict.js'
import api_dictGroup from '/src/api/dictGroup.js'
import api_dictItem from '/src/api/dictItem.js'
import api_log from '/src/api/log.js'
import api_menu from '/src/api/menu.js'
import api_menuGroup from '/src/api/menuGroup.js'
import api_module from '/src/api/module.js'
import api_role from '/src/api/role.js'
import store from '/src/store/index.js'
import page_0 from '/src/views/account/index/$tpm-page'
import page_1 from '/src/views/auditInfo/index/$tpm-page'
import page_2 from '/src/views/dict/index/$tpm-page'
import page_3 from '/src/views/log/index/$tpm-page'
import page_4 from '/src/views/menu/index/$tpm-page'
import page_5 from '/src/views/module/index/$tpm-page'
import page_6 from '/src/views/role/index/$tpm-page'
import component_0 from '/src/components/dict-cascader/index.vue'
import component_1 from '/src/components/dict-select/index.vue'
import component_2 from '/src/components/dict-toolbar-fullscreen/index.vue'
import component_3 from '/src/components/enum-checkbox/index.vue'
import component_4 from '/src/components/enum-radio/index.vue'
import component_5 from '/src/components/enum-select/index.vue'
import component_6 from '/src/components/loginmode-select/index.vue'
import component_7 from '/src/components/module-select/index.vue'
import component_8 from '/src/components/platform-select/index.vue'
import component_9 from '/src/components/role-select/index.vue'
const pages = []
pages.push(page_0)
pages.push(page_1)
pages.push(page_2)
pages.push(page_3)
pages.push(page_4)
pages.push(page_5)
pages.push(page_6)
const components = []
components.push({name:'dict-cascader',component:component_0})
components.push({name:'dict-select',component:component_1})
components.push({name:'dict-toolbar-fullscreen',component:component_2})
components.push({name:'enum-checkbox',component:component_3})
components.push({name:'enum-radio',component:component_4})
components.push({name:'enum-select',component:component_5})
components.push({name:'loginmode-select',component:component_6})
components.push({name:'module-select',component:component_7})
components.push({name:'platform-select',component:component_8})
components.push({name:'role-select',component:component_9})
const api = {}
api['account'] = api_account
api['auditInfo'] = api_auditInfo
api['authorize'] = api_authorize
api['common'] = api_common
api['dict'] = api_dict
api['dictGroup'] = api_dictGroup
api['dictItem'] = api_dictItem
api['log'] = api_log
api['menu'] = api_menu
api['menuGroup'] = api_menuGroup
api['module'] = api_module
api['role'] = api_role
const mod = {id:0, code:'admin', version:'1.0.3', label:'权限管理', icon:'lock', description:'CRB.TPM权限管理模块',store, pages, components, api }
tpm.useModule(mod);
export default mod
*/