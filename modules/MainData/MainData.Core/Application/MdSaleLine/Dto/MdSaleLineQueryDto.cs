using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdSaleLine;

namespace CRB.TPM.Mod.MainData.Core.Application.MdSaleLine.Dto;

/// <summary>
/// 业务线 D_SaleLine查询模型
/// </summary>
public class MdSaleLineQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string LineCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string LineNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

}