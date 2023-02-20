const urls = {
  //查询枚举选项列表
  ENUM_OPTIONS: 'Common/EnumOptions',
  //查询平台选项列表
  PLATFORM_OPTIONS: 'Common/PlatformOptions',
  //查询登录模式下拉列表 
  LOGINMODE_SELECT: 'Common/LoginModeSelect',
  //账户类型下拉列表
  ACCOUNTTYPESELECT_SELECT: 'Common/AccountTypeSelect',
  //账户状态下拉列表
  ACCOUNTSTATUSSELECT_SELECT: 'Common/AccountStatusSelect',

}
export default http => {
  return {
    queryEnumOptions: params => http.get(urls.ENUM_OPTIONS, params),
    queryPlatformOptions: () => http.get(urls.PLATFORM_OPTIONS),
    queryLoginModeSelect: () => http.get(urls.LOGINMODE_SELECT),
    queryAccountTypeSelect: () => http.get(urls.ACCOUNTTYPESELECT_SELECT),
    queryAccountStatusSelect: () => http.get(urls.ACCOUNTSTATUSSELECT_SELECT),
  }
}
