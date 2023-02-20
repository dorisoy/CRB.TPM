import { configure } from 'tpm-ui'
import './index'

configure({
  http: {
    global: {
      baseURL: import.meta.env.TPM_API_URL,
    },
  },
  beforeMount({ config }) {
    config.component.login = 'k'
    config.site.title = {
      'zh-cn': '主数据模块',
      en: 'MainData Module',
    }
    //config.auth.enableButtonPermissions = true
  },
})


