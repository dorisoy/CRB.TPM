const urls = {
    //获取职位
  POSITIONGET: 'Position/Get',
}
export default http => {
  const getPositionGet = () => {
    return http.get(urls.POSITIONGET)
  }
  return { getPositionGet }
}
