const urls = {
    //岗位下拉列表
  POSTSELECT: 'Post/Select',
}
export default http => {
  const getPostSelect = () => {
    return http.get(urls.POSTSELECT)
  }
  return { getPostSelect }
}
