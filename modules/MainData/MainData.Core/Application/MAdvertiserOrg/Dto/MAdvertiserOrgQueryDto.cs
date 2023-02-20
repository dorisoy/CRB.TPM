using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserOrg;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiserOrg.Dto;

/// <summary>
/// 广告商营销组织关系表 M_Re_Org_AD查询模型
/// </summary>
public class MAdvertiserOrgQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string MarketOrg { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string BigAreaOrg { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string OfficeOrg { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string StationOrg { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public Guid AdvertiserId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

}