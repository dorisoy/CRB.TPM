using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAddress;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAddress.Dto;

/// <summary>
/// 广告商地点表 M_ADAddress查询模型
/// </summary>
public class MAdvertiserAddressQueryDto : QueryDto
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
        public string ADDRDESC { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ZDESC { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string MAINPLACE { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ORGID { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ORGCODE { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ORGNAME { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string LOCSTATU { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string INVDATE { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string FKTJ { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string COA { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string EXPACOUNT { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ADVPACOUNT { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string FUACOUNT { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string MAINCD { get; set; }

}