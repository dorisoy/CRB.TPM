import { configure } from 'tpm-ui'
import './index'

configure({
  http: {
    global: {
      baseURL: import.meta.env.CRBTPM_API_URL,
    },
  },
  beforeMount({ config }) {
    config.component.login = 'k'
    config.site.title = {
      'zh-cn': 'TPM费用平台2.0',
      en: 'Version 2.0 of TPM expense management platform',
    }
    //默认首页
    config.site.home = '/admin/home'
    config.auth.enableButtonPermissions = true
  },
})
