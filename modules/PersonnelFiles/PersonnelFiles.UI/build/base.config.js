const { resolve } = require('path')
import vue from '@vitejs/plugin-vue'
import mui from 'tpm-ui/lib/plugins'

export default ({ target, mode, command }) => {
  return {
    base: './',
    server: {
      port: 5221,
    },
    envPrefix: 'TPM',
    plugins: [
      mui({
        target,
        mode,
        command,
        /** 依赖模块 */
        dependencyModules: ['admin'],
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
            copyright: 'CRB.TPM',
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
          },
        ],
      },
    },
    resolve: {
      alias: {
        '@': resolve(__dirname),
      },
    },
  }
}
