using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdStreetVillage;

namespace CRB.TPM.Mod.MainData.Core.Application.MdStreetVillage.Dto;

/// <summary>
/// 街道村 D_StreetVillage查询模型
/// </summary>
public class MdStreetVillageQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string StreetCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string StreetNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string VillageCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string VillageNm { get; set; }

}