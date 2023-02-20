const state = {
  dict: {
    groupCode: '',
    dictCode: '',
  },
  distributor: {
    type: [
      {
        label: '经销商',
        value: 1
      },
      {
        label: '分销商',
        value: 2
      }
    ],
    typeAll: [
      {
        label: '全部',
        value: 0
      },
      {
        label: '经销商',
        value: 1
      },
      {
        label: '分销商',
        value: 2
      }
    ],
    detailType: [
      {
        label: '主户',
        value: 1
      },
      {
        label: '管理开户的子户',
        value: 2
      },
      {
        label: 'TPM虚拟子户',
        value: 3
      }
    ]
  }
}

const mutations = {
  setDict(state, dict) {
    state.dict = dict
  },
}

export default {
  namespaced: true,
  state,
  mutations,
}
