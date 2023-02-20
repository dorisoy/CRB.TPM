using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CRB.TPM.Utils.Helpers
{
    public class SapWebServiceHelper
    {
        public static IResultModel<string> GetWebServiceResult(string requestParam, string url)
        {
            try
            {
                string xmlString = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:sap-com:document:sap:soap:functions:mc-style\"><soapenv:Header/><soapenv:Body><urn:ZcrbInterfaceApi001><API_ATTRS><App_Sub_Id></App_Sub_Id><App_Token></App_Token><Api_ID></Api_ID><Api_Version></Api_Version><Time_Stamp></Time_Stamp><User_Token></User_Token><Sign></Sign><Format></Format><Partner_ID></Partner_ID><Sys_ID></Sys_ID><App_ID></App_ID><App_Version></App_Version><Divice_ID></Divice_ID><Divice_Version></Divice_Version><OS_Version></OS_Version></API_ATTRS><REQUEST_DATA><BUS_PARA>{0}</BUS_PARA><BUS_DATA></BUS_DATA></REQUEST_DATA></urn:ZcrbInterfaceApi001></soapenv:Body></soapenv:Envelope>";

                string param = string.Format(xmlString, Base64Helper.Base64Encode(requestParam));

                XmlDocument result = null;
                //发起请求
                System.Net.ServicePointManager.DefaultConnectionLimit = 200000;
                Uri uri = new Uri(url);
                WebRequest webRequest = WebRequest.Create(uri);
                webRequest.ContentType = "text/xml; charset=utf-8";
                webRequest.Method = "POST";

                //默认TimeOut时间为100000:4个小时
                webRequest.Timeout = 1000 * 60 * 60 * 4;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    byte[] paramBytes = Encoding.UTF8.GetBytes(param);
                    requestStream.Write(paramBytes, 0, paramBytes.Length);
                }

                //响应
                WebResponse webResponse = webRequest.GetResponse();
                using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    result = new XmlDocument();
                    //禁止使用外部实体
                    result.XmlResolver = null;
                    result.LoadXml(myStreamReader.ReadToEnd());
                }

                if (result == null)
                {
                    return ResultModel.Failed<string>("获取SAP WebService失败！");
                }

                string strReturnCode = result.SelectSingleNode("//RETURN_CODE").InnerText;
                string strReturnMsg = string.Empty;

                if (strReturnCode.Equals("S"))
                {
                    return ResultModel.Success<string>(Base64Helper.Base64Decode(result.SelectSingleNode("//RETURN_DATA").InnerText));

                }
                else
                {
                    return ResultModel.Failed<string>(result.SelectSingleNode("//RETURN_DESC").InnerText);
                }
            }
            catch (Exception ex)
            {
                return ResultModel.Failed<string>(ex.ToString());
            }
        }
    }
}
