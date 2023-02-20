using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MdTmnType;

namespace CRB.TPM.Mod.MainData.Core.Application.MdTmnType.Dto;

/// <summary>
/// 终端类型（一二三级） M_TmnType查询模型
/// </summary>
public class MdTmnTypeQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RegionCD { get; set; }

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
        public string Level1TypeCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Level1TypeNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Level2TypeCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Level2TypeNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Level3TypeCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Level3TypeNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string MarketOrgCD { get; set; }

}