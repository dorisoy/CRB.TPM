const urls = {
    LEAVE: 'Employee/Leave',
    LEAVEINFO: 'Employee/LeaveInfo',
    EDITACCOUNT: 'Employee/EditAccount',
    UPDATEACCOUNT: 'Employee/updateAccount',
    QUERYWITHSAMEDEPARTMENT: 'Employee/QueryWithSameDepartment',
    QUERYLATESTSELECT: 'Employee/QueryLatestSelect',
    SAVELATESTSELECT: 'Employee/SaveLatestSelect',
    TREE: 'Employee/Tree',
    BASEINFOLIST: 'Employee/BaseInfoList'
}
export default http => {

    //离职
    const postLeave = params => {
        return http.post(urls.LEAVE, params)
    }

    // 获取离职信息
    const getLeaveInfo = id => {
        return http.get(urls.LEAVEINFO, { id })
    }

    //编辑账户
    const editAccount = id => {
        return http.get(urls.EDITACCOUNT, { id })
    }

    //更新账户
    const updateAccount = params => {
        return http.post(urls.UPDATEACCOUNT, params)
    }

    //查询同一部门下的人员信息
    const queryWithSameDepartment = params => {
        return http.get(urls.QUERYWITHSAMEDEPARTMENT, params)
    }

    //查询最近选择人员列表
    const queryLatestSelect = params => {
        return http.get(urls.QUERYLATESTSELECT, params)
    }

    //保存最近人员选择记录
    const saveLatestSelect = ids => {
        return http.post(urls.SAVELATESTSELECT, ids)
    }

    //查询人员树
    const getTree = () => {
        return http.get(urls.TREE)
    }

    //批量查询人员基本信息
    const getBaseInfoList = ids => {
        return http.get(urls.BASEINFOLIST, { ids })
    }

    return {
        postLeave,
        getLeaveInfo,
        editAccount,
        updateAccount,
        queryWithSameDepartment,
        queryLatestSelect,
        saveLatestSelect,
        getTree,
        getBaseInfoList
    }
}
