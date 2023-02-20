using CRB.TPM.Config.Abstractions;

namespace CRB.TPM.Mod.MainData.Core.Infrastructure;

/// <summary>
/// 这里是创建的是MainData集成模块配置类
/// </summary>
public class MainDataConfig : IConfig
{
    /// <summary>
    /// SAP连接字符串
    /// </summary>
    public string SapConnection { get; set; } = "Name=P02;AppServerHost=10.10.92.135;User=DATA1; Password=Dt@20200801;SystemNumber=00; Client=800; Language=ZH; PoolSize=5;MaxPoolSize=10; IdleTimeout=60";
    /// <summary>
    /// 同步CRM经销商、终端函数名
    /// </summary>
    public string SyncCRMDtAndTmnFunctionName { get; set; } = "ZSNFX_RFC_GET_MTMX_TPM";
    /// <summary>
    /// SAP webservice 接口地址
    /// </summary>
    public string SapWsUrl { get; set; } = "http://xhcrmpoc.crc.com.cn:8000/sap/bc/srt/rfc/sap/zcrb_interface_api001/800/zcrb_interface_api001/zcrb_interface_api001";
}