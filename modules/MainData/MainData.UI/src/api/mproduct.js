const urls = {
    // 导出
    EXPORT: 'MProduct/Export',
    // 产品下拉
    SELECT: 'MProduct/Select',
    // 删除选中
    DELETESELECTED: 'MProduct/DeleteSelected'
}
export default http => {
    return {
        queryProductSelect: params => http.get(urls.SELECT, params),
        deleteSelected: params => http.delete(urls.DELETESELECTED, params),
        exportProduct: params => http.download(urls.EXPORT, params)
    }
}
