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
    config.site.title = '组织架构模块'
  },
})



// import { configure } from 'tpm-ui'
// import 'tpm-ui/lib/style.css'

// // import zhCN from '@tpm-locale/zh-cn'
// // import en from '@tpm-locale/en'

// import 'tpm-mod-admin'
// import 'tpm-mod-admin/lib/style.css'
// import './index'

// // configure({ locale: { messages: [] } })
// // tpm.config.component.login = 'k'
// // tpm.config.site.title = '通用统一认证平台'
// // tpm.config.http.global.baseURL = 'http://localhost:6220/api/'

// configure({
//   http: {
//     global: {
//       baseURL: import.meta.env.TPM_API_URL,
//     },
//   },
//   beforeMount({ config }) {
//     config.component.login = 'k'
//     config.site.title = '组织架构模块'
//   },
// })