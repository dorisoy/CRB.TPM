const urls = {
    // 导出
    EXPORT: 'MTerminal/Export',
    // 下拉
    SELECT: 'MTerminal/Select',
    // 删除选中
    DELETESELECTED: 'MTerminal/DeleteSelected'
}
export default http => {
    return {
        queryTerminalSelect: params => http.get(urls.SELECT, params),
        deleteSelected: params => http.delete(urls.DELETESELECTED, params),
        exportTermainal: params => http.download(urls.EXPORT, params)
    }
}
