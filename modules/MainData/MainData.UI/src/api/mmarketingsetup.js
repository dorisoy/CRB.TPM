const urls = {
    // 导出
    EXPORT: 'MMarketingSetup/Export',
    // 删除选中
    DELETESELECTED: 'MTerminal/DeleteSelected'
}
export default http => {
    return {
        deleteSelected: params => http.delete(urls.DELETESELECTED, params),
        exportMarketingSetup: params => http.download(urls.EXPORT, params)
    }
}
