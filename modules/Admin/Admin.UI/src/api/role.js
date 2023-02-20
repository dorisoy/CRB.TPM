const urls = {
  /** 查询角色绑定的菜单信息 */
  QUERY_BIND_MENUS: 'Role/QueryBindMenus',
  /** 更新角色绑定的菜单信息 */
  UPDATE_BIND_MENUS: 'Role/UpdateBindMenus',
  /** 下拉列表 */
  SELECT: 'Role/Select',
  //获取指定层级组织数据
  GETORGLEVEL: 'MOrg/GetOrgLevel',
  //获取组织树
  GETTREEBYPARENTID: 'MOrg/GetTreeByParentId',
  //根据父ID获取组织节点
  GETNODEBYPARENTID: 'MOrg/GetNodeByParentId',
}

export default http => {

  /**
   * getNodeTree
   * @param {层级} level 
   * @returns 
   */
  const getNodeTree = async (level) => {
    let data = []
    switch (level) {
      case 1:
        data = await http.get(urls.GETORGLEVEL, { level: 10 })
        break;
      case 2:
        data = await http.get(urls.GETORGLEVEL, { level: 20 })
        break;
      case 3:
        data = await http.get(urls.GETORGLEVEL, { level: 30 })
        break;
      case 4:
        data = await http.get(urls.GETORGLEVEL, { level: 40 })
        break;
      case 5:
        data = await http.get(urls.GETORGLEVEL, { level: 50 })
        break;
      case 6:
        data = await http.get(urls.GETORGLEVEL, { level: 60 })
        break;
      default:
        data = await http.get(urls.GETTREEBYPARENTID, { level: 60 })
        break;
    }
    //过滤数据权限
    ////console.log('getNodeTree', data)
    return treeConcat(data);
  }

  /**
   * 树形递归
   * @param {*} data 
   * @returns 
   */
  const treeConcat = (data) => {
    // 删除 所有 children,以防止多次调用
    data.forEach(function (n, index) {
      //n.label = n.name
      delete n.children;
    });
    // 将数据存储为 以 id 为 KEY 的 map 索引数据列
    var map = {};
    data.forEach(function (n) {
      map[n.id] = n;
    });

    ////////console.log(map);
    var val = [];
    data.forEach(function (n) {
      // 以当前遍历项，的pid,去map对象中找到索引的id
      var parent = map[n.parentId];
      // 好绕啊，如果找到索引，那么说明此项不在顶级当中,那么需要把此项添加到，他对应的父级中
      if (parent) {
        (parent.children || (parent.children = [])).push(n);
      } else {
        //如果没有在map中找到对应的索引ID,那么直接把 当前的item添加到 val结果集中，作为顶级
        val.push(n);
      }
    });
    return val;
  }

  /**
   * getTree
   * @param {*} params 
   * @returns 
   */
  const getTree = async (params) => {
    let root = await http.get(urls.GETTREEBYPARENTID, { level: 10, ignore: true });
    ////console.log('getTree -> root', root)
    return root
  }

  /**
   * filterDate
   * @param {*} data 
   * @param {*} orgs 
   * @param {*} parentId 
   * @returns 
   */
  const filterDate = (res, orgs, parentId, ignore) => {
    let data = []
    //console.log('orgs.length,parentId,ignore', orgs.length, parentId, ignore)
    if (!ignore && orgs.length > 0 && parentId != 'null')
      data = res.filter((m) => orgs.includes(m.id.toLowerCase()) && m.parentId.toLowerCase() === parentId.toLowerCase());
    else if (!ignore && parentId != 'null')
      data = res.filter((m) => m.parentId.toLowerCase() === parentId.toLowerCase());
    else if (ignore) {
      data = res;
    }
    return data
  }

  /**
   * 获取节点数据
   * @param {*} params 
   * @returns 
   */
  const getChildTree = async (params) => {
    let data = []
    let orgs = params.includes?.map(s => s.orgId)
    let parentId = params.parentId;
    if (parentId == null || parentId == undefined) {
      parentId = 'null'
    }
    //console.log('getChildTree - > params', params)
    switch (params.level) {
      case 1:
        {
          let res = await http.get(urls.GETORGLEVEL, { level: 10, ignore: params.ignore });
          data = filterDate(res, orgs, parentId, params.ignore)
          break;
        }
      case 2:
        {
          let res = await http.get(urls.GETORGLEVEL, { level: 20, ignore: params.ignore });
          data = filterDate(res, orgs, parentId, params.ignore)
          break;
        }
      case 3:
        {
          let res = await http.get(urls.GETORGLEVEL, { level: 30, ignore: params.ignore });
          data = filterDate(res, orgs, parentId, params.ignore)
          break;
        }
      case 4:
        {
          let res = await http.get(urls.GETORGLEVEL, { level: 40, ignore: params.ignore });
          data = filterDate(res, orgs, parentId, params.ignore)
          break;
        }
      case 5:
        {
          let res = await http.get(urls.GETORGLEVEL, { level: 50, ignore: params.ignore });
          data = filterDate(res, orgs, parentId, params.ignore)
          break;
        }
      case 6:
        {
          let res = await http.get(urls.GETORGLEVEL, { level: 60, ignore: params.ignore });
          data = filterDate(res, orgs, parentId, params.ignore)
          break;
        }
    }
    return data
  }


  /**
   * 
   * @param {*} params 
   * @returns 
   */
  const getOrgLevel = async (params) => {
    let tree = []
    const data = await getChildTree(params)
    if (data != null) {
      tree = treeConcat(data)
    }
    return tree
  }

  /**
   * 获取树节点路径
   * @param {*} curKey 树节点标识的值
   * @param {array} data 树
   * @returns {array} result 存放搜索到的树节点到顶部节点的路径节点
   */
  const getPathByKey = (curKey, data) => {
    /** 存放搜索到的树节点到顶部节点的路径节点 */
    let result = [];
    /**
     * 路径节点追踪
     * @param {*} curKey 树节点标识的值
     * @param {array} path 存放搜索到的树节点到顶部节点的路径节点
     * @param {*} data 树
     * @returns undefined
     */
    let traverse = (curKey, path, data) => {
      // 树为空时，不执行函数
      if (data.length === 0) {
        return;
      }

      // 遍历存放树的数组
      for (let item of data) {
        // 遍历的数组元素存入path参数数组中
        path.push(item);
        // 如果目的节点的id值等于当前遍历元素的节点id值
        if (item.id === curKey) {
          // 把获取到的节点路径数组path赋值到result数组
          result = JSON.parse(JSON.stringify(path));
          return;
        }

        // 当前元素的children是数组
        const children = Array.isArray(item.children) ? item.children : [];
        // 递归遍历子数组内容
        traverse(curKey, path, children);
        // 利用回溯思想，当没有在当前叶树找到目的节点，依次删除存入到的path数组路径
        path.pop();
      }
    };
    traverse(curKey, [], data);
    // 返回找到的树节点路径
    return result;
  };

  return {
    queryBindMenus: params => http.get(urls.QUERY_BIND_MENUS, params),
    updateBindMenus: params => http.post(urls.UPDATE_BIND_MENUS, params),
    getNodeByParentId: (params) => http.get(urls.GETNODEBYPARENTID, params),
    select: () => http.get(urls.SELECT),
    getNodeTree,
    getTree,
    treeConcat,
    getOrgLevel,
    getPathByKey,
    getChildTree
  }
}
