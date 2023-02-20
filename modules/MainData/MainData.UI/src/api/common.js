const urls = {
    // 主数据组织架构层级枚举下拉列表
    ORGENUMTYPESELECT: 'Common/OrgEnumTypeSelect',
}
export default http => {
    return {
        getOrgEnumTypeSelect: () => http.get(urls.ORGENUMTYPESELECT),
    }
}
