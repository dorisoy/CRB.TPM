using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdKaBigSysNameConf;

namespace CRB.TPM.Mod.MainData.Core.Application.MdKaBigSysNameConf.Dto;

/// <summary>
/// KA大系统 M_KABigSysNameConf查询模型
/// </summary>
public class MdKaBigSysNameConfQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string SaleOrg { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string KASystemNum { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string SaleOrgNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string KASystemName { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string KALx { get; set; }

}