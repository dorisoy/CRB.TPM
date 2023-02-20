const urls = {
  //获取账户默认密码
  DEFAULT_PASSWORD: 'Account/DefaultPassword',
  //更新皮肤
  UPDATE_SKIN: 'Account/UpdateSkin',
  //更新账户角色组织
  UPDATE_ACCOUNT_ROLE_ORG: 'Account/UpdateAccountRoleOrg',
  //查询账户下拉列表（带分页）
  ACCOUNT_SELECT: 'Account/Select',
}
export default http => {
  return {
    queryAccountSelect: (params) => http.get(urls.ACCOUNT_SELECT, params),
    getDefaultPassword: () => http.get(urls.DEFAULT_PASSWORD),
    updateSkin: params => http.post(urls.UPDATE_SKIN, params),
    updateAccountRoleOrg: (params) => http.post(urls.UPDATE_ACCOUNT_ROLE_ORG, params),
  }
}
