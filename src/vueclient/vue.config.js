const path = require('node:path');
const { defineConfig } = require('@vue/cli-service')

module.exports = defineConfig({
  transpileDependencies: true,

  publicPath: process.env.NODE_ENV === 'production'
  ? '/'
  : '/dev/',

  devServer: {
    https: {
      key: path.resolve('certs/localhost-key.pem'),
      cert: path.resolve('certs/localhost.pem'),
      //key: path.resolve(process.env.CERT_KEY),
      //cert: path.resolve(process.env.CERT),
    },
    //public: 'https://localhost:5192/',

    proxy: {
        '/': {
            target: 'https://localhost:5192/'
        }
    },
    port: 5192
  },

  chainWebpack: config => {

      config.module
        .rule('vue')
        .use('vue-loader')
        .tap(options => ({
          ...options,
          compilerOptions: {
            // treat any tag that starts with ion- as custom elements
            isCustomElement: tag => tag.startsWith('ion-'),
          }
        }))
    }
})
