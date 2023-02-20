const urls = {
    // 导出
    EXPORT: 'MMarketingProduct/Export',
    // 删除选中
    DELETESELECTED: 'MMarketingProduct/DeleteSelected'
}
export default http => {
    return {
        deleteSelected: params => http.delete(urls.DELETESELECTED, params),
        exportMarketingProduct: params => http.download(urls.EXPORT, params)
    }
}
