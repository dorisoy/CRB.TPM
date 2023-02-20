
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MAdvertiserAccount
{
    /// <summary>
    /// 广告商银行账号表 M_ADBankAccount
    /// </summary>
    [Table("M_AdvertiserAccount")]
    public partial class MAdvertiserAccountEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
        public Guid AdvertiserId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Length(35)]
        public string BankAccount { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(18)]
        public string BankActNm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(100)]
        public string BankNm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        [Length(100)]
        public string CustomerNm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string CurrencyCD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string DateStart { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string DateEnd { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(5)]
        public string AccountTYP { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(1)]
        public string IsMainFLG { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string BankInfoNUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string RBKNUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(1)]
        public string IsValid { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string AreaCode { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string CommercialBankDocumentNUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string MainCode { get; set; } = string.Empty;

    }
}
