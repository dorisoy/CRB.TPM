const urls = {
    // 登录日志
    QUERYLOGIN: 'Log/Query',
    // 导出登录日志
    EXPORTLOGIN: 'Log/LoginExport'
}
export default http => {
    return {
        queryLogin: params => http.get(urls.QUERYLOGIN, params),
        exportLogin: params => http.download(urls.EXPORTLOGIN, params),
    }
}
