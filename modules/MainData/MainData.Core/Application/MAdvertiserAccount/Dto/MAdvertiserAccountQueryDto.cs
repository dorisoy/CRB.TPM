using System;
using System.ComponentModel.DataAnnotations;

using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;
using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccount;

namespace CRB.TPM.Mod.MainData.Core.Application.MAdvertiserAccount.Dto;

/// <summary>
/// 广告商银行账号表 M_ADBankAccount查询模型
/// </summary>
public class MAdvertiserAccountQueryDto : QueryDto
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
        public string BankAccount { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string BankActNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string BankNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CustomerNm { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CurrencyCD { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string DateStart { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string DateEnd { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string AccountTYP { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string IsMainFLG { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string BankInfoNUM { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string RBKNUM { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string IsValid { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string CommercialBankDocumentNUM { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string ModelCode { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string MainCode { get; set; }

}