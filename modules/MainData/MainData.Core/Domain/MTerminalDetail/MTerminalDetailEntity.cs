
using System;
using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Data.Abstractions.Entities;


namespace CRB.TPM.Mod.MainData.Core.Domain.MTerminalDetail
{
    /// <summary>
    /// 终端其他信息
    /// </summary>
    [Table("M_TerminalDetail")]
    public partial class MTerminalDetailEntity : Entity<Guid>
    {
        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(30)]
        public string Tel { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string Prov { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string City { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(60)]
        public string Street { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string Village { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(60)]
        public string AddDetail { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(12)]
        public string TmnOwner { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string TmnPhone { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string Per1Nm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string Per1Post { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(8)]
        public string Per1Bir { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string Per1Tel { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string Per2Nm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string Per2Post { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(8)]
        public string Per2Bir { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string Per2Tel { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string Per3Nm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string Per3Post { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(8)]
        public string Per3Bir { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string Per3Tel { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string Geo { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string CoopNature { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string SysNum { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string SysNm { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string SaleChannel { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        //[Length(1)]
        public int IsProtocol { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Precision]
        public decimal? RL { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string XYLY { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(4)]
        public string ZGDFL { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(30)]
        public string FaxNumber { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string NamCountry { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZKASystem1 { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string ZZFMS_MUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZTable { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(5)]
        public string ZZSeat { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ZZWEIXIN_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZAge { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZInner_Area { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZOut_Area { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZBEER { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string ZZCHAIN_NAME { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ZZCHAIN_TEL { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZCHAIN_TYPE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZCHAIN_QUA { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZCHAIN_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZCUISINE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZCHARACTERISTIC { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(15)]
        public string ZZPERCONSUME { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ZZOPEN_TIME { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZFREEZER { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ZZFLD0000CG { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZVIRTUAL { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Precision]
        public decimal? ZZVISIT { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZCHARACTER { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZSTORAGE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZFLD000052 { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZSMALLBOX_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string ZZPRO_NUM2 { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string ZZPRO_NAME2 { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZALCO { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ZZBEST_TIME { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZWHET_CHAIN { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZBIGBOX_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZMIDBOX_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZPORN_PRICE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZPRO_RANK { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZDAY_REVENUE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZCASHIER_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZDISTRI_WAY { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Precision]
        public decimal? ZZFLD00005D { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZRECONCILIATION { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZACCOUNT_WAY { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZZACCCOUNT_TIME { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ZZFIPERSON { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ZZFIPERSON_TEL { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        public string E_MAILSMT { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(100)]
        public string URIURI { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(200)]
        public string BZ { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZDELIVER_NOTE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(40)]
        public string ZZCARLIMIT_DESC { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(5)]
        public string ZZACCOUNT_PERIOD { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZKABEER_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZKABEER_PILE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZKANONBEER_PILE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZKAICE_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZKACOLD_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZKASHELF_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(3)]
        public string ZZKALEVEL_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKAWHOLEBOX_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKAPACKAGE_NUM { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKAPILE_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKANONPILE_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKAPRO_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKASHELF_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKAICE_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKACOLD_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKACASHER_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKAMULTI_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ZZKADISPLAY_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKAPILEOUT_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZFLD0000G2 { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKAFLAG_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKAPOST_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKAAB_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZFLD0000G6 { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKALADDER_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKASERVICE_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(2)]
        public string ZZKAPOP_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(20)]
        public string ZZKALIVELY_USE { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public int? ZZBOX { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(6)]
        public string ZZDECK_NAME { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(10)]
        public string ZbnType { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(30)]
        public string ZZGSYYZZH { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        [Length(80)]
        public string ZZGSZZMC { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        //[Length(1)]
        public int Deleted { get; set; } 

        /// <summary>
        ///  
        /// </summary>
        public Guid? DeletedBy { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        public string Deleter { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public DateTime? DeletedTime { get; set; } = System.DateTime.Now;

        /// <summary>
        ///  
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Creator { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public DateTime CreatedTime { get; set; } = System.DateTime.Now;

        /// <summary>
        ///  
        /// </summary>
        public Guid? ModifiedBy { get; set; }

        /// <summary>
        ///  
        /// </summary>
		[Nullable]
        public string Modifier { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public DateTime? ModifiedTime { get; set; } = System.DateTime.Now;

    }
}
