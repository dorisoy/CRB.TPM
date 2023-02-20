const urls = {
  COMPANY_INFO: 'Company/Get',
}
export default http => {
  const getCompanyget = () => {
    return http.get(urls.COMPANY_INFO)
  }
  return { getCompanyget }
}
