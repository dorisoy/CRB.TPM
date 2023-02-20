using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdReTmnBTyteConfig;

namespace CRB.TPM.Mod.MainData.Core.Application.MdReTmnBTyteConfig.Dto;

/// <summary>
///  终端业态关系表 M_Re_Tmn_BTyte_Config查询模型
/// </summary>
public class MdReTmnBTyteConfigQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 终端编码
        /// </summary>
        public string TmnStoreType1 { get; set; }

        /// <summary>
        /// 关系类型(ZS003:终端负责员工/ZS001:经销商业务员)
        /// </summary>
        public string ZbnType { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string ZbnTypeTxt { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

}