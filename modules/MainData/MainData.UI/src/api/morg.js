const urls = {
    // 获取指定层级组织
    TREE: 'MOrg/Tree',
    // 批量删除组织
    DELETESELECTED: 'MOrg/DeleteSelected',
    //组织树结构列表
    SELECT: 'MOrg/Select',
    //获取当前用户的组织权限树
    GETCURRENTACCOUNTAROSTREE: 'MOrg/GetCurrentAccountAROSTree',
    //获取指定层级组织数据
    GETORGLEVEL: 'MOrg/GetOrgLevel',
    //获取组织树
    GETTREEBYPARENTID: 'MOrg/GetTreeByParentId',
    // 导出
    EXPORT: 'MOrg/Export'
}
export default http => {
    return {
        tree: (level) => http.get(urls.TREE, level),
        deleteSelected: params => http.delete(urls.DELETESELECTED, params),
        queryMorgSelect: params => http.get(urls.SELECT, params),
        getCurrentAccountAROSTree: params => http.get(urls.GETCURRENTACCOUNTAROSTREE, params),
        getOrgLevel: params => http.get(urls.GETORGLEVEL, params),
        getTreeByParentId: params => http.get(urls.GETTREEBYPARENTID, params),
        export: params => http.download(urls.EXPORT, params)
    }
}
