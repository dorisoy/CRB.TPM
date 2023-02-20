const urls = {
  GETUI: 'Config/UI',
  DESCRIPTORS: 'Config/Descriptors',
  EDIT: 'Config/Edit',
  UPDATE: 'Config/Update',
}

export default http => {
  return {
    getUI: () => http.get(urls.GETUI),
    getDescriptors: () => http.get(urls.DESCRIPTORS),
    edit: (params) => http.get(urls.EDIT, params),
    update: (params) => http.post(urls.UPDATE, params),
  }
}
