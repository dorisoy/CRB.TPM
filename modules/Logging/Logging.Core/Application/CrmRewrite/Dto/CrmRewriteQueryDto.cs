using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.Logging.Core.Domain.CrmRewrite;

namespace CRB.TPM.Mod.Logging.Core.Application.CrmRewrite.Dto;

/// <summary>
/// 返写CRM记录查询模型
/// </summary>
public class CrmRewriteQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// CODE1
        /// </summary>
        public string PARTNER1 { get; set; }

        /// <summary>
        /// CODE2
        /// </summary>
        public string PARTNER2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ZUSER { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ZTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ZTYPE_1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ZDATE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ZBAIOS { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

}