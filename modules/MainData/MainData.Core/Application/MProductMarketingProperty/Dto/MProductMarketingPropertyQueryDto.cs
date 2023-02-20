using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MProductMarketingProperty;

namespace CRB.TPM.Mod.MainData.Core.Application.MProductMarketingProperty.Dto;

/// <summary>
/// 营销产品属性查询模型
/// </summary>
public class MProductMarketingPropertyQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 营销中心id（orgid）
        /// </summary>
        public Guid MarketingId { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 子品牌
        /// </summary>
        public string BrandChild { get; set; }

        /// <summary>
        /// 重点产品
        /// </summary>
        public string KeyProduct { get; set; }

        /// <summary>
        /// 产品简称
        /// </summary>
        public string Abbreviation { get; set; }

}