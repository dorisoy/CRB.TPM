using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdDistrictStreet;

namespace CRB.TPM.Mod.MainData.Core.Application.MdDistrictStreet.Dto;

/// <summary>
/// 区县街道 D_DistrictStreet查询模型
/// </summary>
public class MdDistrictStreetQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string DistrictCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string DistrictNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string StreetCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string StreetNm { get; set; }

}