const urls = {
    // 导出
    EXPORT: 'MEntity/Export',
    // 删除选中
    DELETESELECTED: 'MTerminal/DeleteSelected'
}
export default http => {
    return {
        deleteSelected: params => http.delete(urls.DELETESELECTED, params),
        exportEntity: params => http.download(urls.EXPORT, params)
    }
}
