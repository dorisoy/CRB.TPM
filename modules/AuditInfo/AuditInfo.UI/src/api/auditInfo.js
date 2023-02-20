const urls = {
    // 日志详细
    DETAILS: 'AuditInfo/Details',
    // 查询最近一周访问量
    QUERYLATESTWEEKPV: 'AuditInfo/QueryLatestWeekPv',
    // 导出日志
    EXPORT: 'AuditInfo/Export'
}
export default http => {
    return {
        details: id => http.get(urls.DETAILS, id),
        queryLatestWeekPv: () => http.get(urls.QUERYLATESTWEEKPV),
        exportAuditInfo: params => http.download(urls.EXPORT, params)
    }
}
