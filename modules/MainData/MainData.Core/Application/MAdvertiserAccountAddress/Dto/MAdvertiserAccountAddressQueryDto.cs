using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccountAddress;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAccountAddress.Dto;

/// <summary>
/// 广告商地点分配表 M_Re_ADAddressAccount查询模型
/// </summary>
public class MAdvertiserAccountAddressQueryDto : QueryDto
{
        /// <summary>
        ///  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public Guid AdvertiserId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ADDRNO { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public Guid AdvertiserAccountId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ASSIGNSTAU { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string UUID { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string STDATE { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ENDATE { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string MAINCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string BUCODE { get; set; }

}