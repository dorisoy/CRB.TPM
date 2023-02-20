import mod from '$tpm-mod-admin'

mod.callback = ({ app, config }) => {
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

  // // 屏蔽错误信息
  // app.config.errorHandler = (err) => {
  //   console.log(err);
  // };
  // // 屏蔽警告信息
  // app.config.warnHandler = (message) => {
  //   console.log(message);
  // };

  console.log('app', app)
}

