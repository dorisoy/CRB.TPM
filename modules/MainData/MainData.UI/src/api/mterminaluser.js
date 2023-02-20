const urls = {
    // 导出
    EXPORT: 'MTerminalUser/Export',
    // 删除选中
    DELETESELECTED: 'MTerminal/DeleteSelected'
}
export default http => {
    return {
        deleteSelected: params => http.delete(urls.DELETESELECTED, params),
        exportTerminalUser: params => http.download(urls.EXPORT, params)
    }
}
