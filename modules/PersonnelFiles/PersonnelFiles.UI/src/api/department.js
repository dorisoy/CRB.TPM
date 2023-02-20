const urls = {
    TREE: 'Department/Tree',
    AREA: 'Department/Tree',
    UPDATE_SORT: 'Department/UpdateSort',
}
export default http => {
    //查询部门树
    const getTree = () => {
        return http.get(urls.TREE)
    }
    const area = () => {
        return http.get(urls.AREA)
    }
    return {
        // getGroupSelect: () => http.get(urls.GROUP_SELECT),
        // getTree: params => http.get(urls.TREE, params),
        getTree,
        area,
        updateSort: sorts => http.post(urls.UPDATE_SORT, sorts),
    }
}
