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
      'zh-cn': '审计模块',
      en: 'Auditinfo Module',
    }
    config.auth.enableButtonPermissions = true
  },
})