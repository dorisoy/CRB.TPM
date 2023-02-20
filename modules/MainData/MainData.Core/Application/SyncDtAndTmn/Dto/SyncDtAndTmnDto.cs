using System;
using System.ComponentModel.DataAnnotations;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncDtAndTmn.Dto
{
    /// <summary>
    /// 同步经销商、终端主数据请求模型
    /// </summary>
    public class SyncDtAndTmnDto
    {
        /// <summary>
        /// 营销中心编码
        /// </summary> 
        [Required(ErrorMessage = "请输入营销中心编码")]
        public string MarketOrgCD { get; set; }
    }
}
