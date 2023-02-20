using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdHeightConf;

namespace CRB.TPM.Mod.MainData.Core.Application.MdHeightConf.Dto;

/// <summary>
/// 制高点配置 M_HeightConf查询模型
/// </summary>
public class MdHeightConfQueryDto : QueryDto
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
        public string Height { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Text { get; set; }

}