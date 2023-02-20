const urls = {
    // 导出
    EXPORT: 'MDistributor/Export',
    //下拉组件
    SELECT: 'MDistributor/Select',
    // 删除选中
    DELETESELECTED: 'MTerminal/DeleteSelected'
}
export default http => {
    return {
        queryDistributorSelect: params => http.get(urls.SELECT, params),
        deleteSelected: params => http.delete(urls.DELETESELECTED, params),
        exportDistributor: params => http.download(urls.EXPORT, params),
    }
}
