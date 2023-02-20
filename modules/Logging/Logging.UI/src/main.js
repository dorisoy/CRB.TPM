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
      'zh-cn': '日志模块',
      en: 'Logging Module',
    }
    config.auth.enableButtonPermissions = true
  },
})