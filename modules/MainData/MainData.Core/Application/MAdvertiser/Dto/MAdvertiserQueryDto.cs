using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiser;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiser.Dto;

/// <summary>
/// 广告商查询模型
/// </summary>
public class MAdvertiserQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string AdvertiserCode { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string AdvertiserName { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RegionCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ADJC { get; set; }

}