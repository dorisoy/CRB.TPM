const urls = {
    // 导出
    EXPORT: 'MProductProperty/Export',
    // 产品属性类型下拉
    TYPESELECT: 'MProductProperty/TypeSelect',
    // 删除选中
    DELETESELECTED: 'MProductProperty/DeleteSelected'
}
export default http => {
    return {
        queryTypeSelect: params => http.get(urls.TYPESELECT, params),
        deleteSelected: params => http.delete(urls.DELETESELECTED, params),
        exportProductProperty: params => http.download(urls.EXPORT, params)
    }
}
