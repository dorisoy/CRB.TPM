const { resolve } = require('path')
import vue from '@vitejs/plugin-vue'
import mui from 'tpm-ui/lib/plugins'

export default ({ target, mode, command }) => {
  return {
    base: './',
    server: {
      port: 5220,
    },
    envPrefix: 'CRBTPM',
    plugins: [
      mui({
        target,
        mode,
        command,
        /** 依赖模块 */
        dependencyModules: ['maindata'],
        /** 皮肤 */
        skins: [],
        /** 语言包 */
        locales: ['zh-cn', 'en'],
        /** index.html文件转换 */
        htmlTransform: {
          /** 模板渲染数据，如果使用自己的模板，则自己定义渲染数据 */
          render: {
            //图标
            favicon: './assets/tpm/favicon.ico',
            /** 版权信息 */
            copyright: '版权所有：CRB.TPM',
            /** Logo */
            logo: './assets/tpm/logo.png',
          },
          /** 压缩配置 */
          htmlMinify: {},
        },
      }),
      vue(),
    ],
    css: {
      postcss: {
        plugins: [
          {
            /** 解决打包时出现 warning: "@charset" must be the first rule in the file */
            postcssPlugin: 'internal:charset-removal',
            AtRule: {
              charset: atRule => {
                if (atRule.name === 'charset') {
                  atRule.remove()
                }
              },
            },
            /**转换css中图片的相对路径 */
            Declaration(decl) {
              let reg = /url\((.+?)\)/gi
              if (decl.value.match(reg)) {
                decl.value = decl.value.replace(/\.\.\//g, '')
              }
            },
          },
        ],
      },
    },
    resolve: {
      alias: {
        '@': resolve(__dirname),
        'vue-i18n': 'vue-i18n/dist/vue-i18n.cjs.js'
      },
    },
  }
}
