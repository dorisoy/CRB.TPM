using CRB.TPM.Mod.Admin.Core.Domain.WarningInfo;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncDtAndTmn.Dto
{
    /// <summary>
    /// 同步经销商、终端主数据错误日志模型
    /// </summary>
    public class SyncDtAndTmnErrorLogDto
    {
        /// <summary>
        /// 营销中心编码
        /// </summary> 
        public string MarketOrgCD { get; set; }
        /// <summary>
        /// 类型
        /// </summary> 
        public WarningInfoType Type { get; set; }
        /// <summary>
        /// 代码1
        /// </summary> 
        public string Code1 { get; set; }
        /// <summary>
        /// 代码2
        /// </summary> 
        public string Code2 { get; set; }
        /// <summary>
        /// 代码2
        /// </summary> 
        public string Message { get; set; }
        /// <summary>
        /// 是否已保存
        /// </summary>
        public bool IsSave { get; set; } = false;
    }
}
