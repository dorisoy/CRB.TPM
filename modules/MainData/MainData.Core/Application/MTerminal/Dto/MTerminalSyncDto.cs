using System;
using System.ComponentModel.DataAnnotations;
using CRB.TPM.Utils.Annotations;
using CRB.TPM.Utils.Validations;

using CRB.TPM.Mod.MainData.Core.Domain.MTerminal;

namespace CRB.TPM.Mod.MainData.Core.Application.MTerminal.Dto;

/// <summary>
/// 终端同步写临时表模型
/// </summary>
public class MTerminalSyncDto
{
    /// <summary>
    ///  
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    public string TerminalCode { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string TerminalName { get; set; }

    /// <summary>
    /// 工作站id
    /// </summary>
    public Guid StationId { get; set; }

    /// <summary>
    /// 业务线
    /// </summary>
    public string SaleLine { get; set; }

    /// <summary>
    /// 一级类型
    /// </summary>
    public string Lvl1Type { get; set; }

    /// <summary>
    /// 二级类型
    /// </summary>
    public string Lvl2Type { get; set; }

    /// <summary>
    /// 三级类型
    /// </summary>
    public string Lvl3Type { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// CRM数据
    /// </summary>
    public string JsonString { get; set; }
    /// <summary>
    /// CRM时间
    /// </summary>
    public DateTime ZDATE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Tel { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Prov { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string City { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Village { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string AddDetail { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string TmnOwner { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string TmnPhone { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per1Nm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per1Post { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per1Bir { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per1Tel { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per2Nm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per2Post { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per2Bir { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per2Tel { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per3Nm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per3Post { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per3Bir { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Per3Tel { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string Geo { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string CoopNature { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string SysNum { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string SysNm { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string SaleChannel { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public int IsProtocol { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public decimal? RL { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string XYLY { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZGDFL { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string FaxNumber { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string NamCountry { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKASystem1 { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZFMS_MUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZTable { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZSeat { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZWEIXIN_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZAge { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZInner_Area { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZOut_Area { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZBEER { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZCHAIN_NAME { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZCHAIN_TEL { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZCHAIN_TYPE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZCHAIN_QUA { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZCHAIN_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZCUISINE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZCHARACTERISTIC { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZPERCONSUME { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZOPEN_TIME { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZFREEZER { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZFLD0000CG { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZVIRTUAL { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public decimal? ZZVISIT { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZCHARACTER { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZSTORAGE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZFLD000052 { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZSMALLBOX_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZPRO_NUM2 { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZPRO_NAME2 { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZALCO { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZBEST_TIME { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZWHET_CHAIN { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZBIGBOX_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZMIDBOX_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZPORN_PRICE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZPRO_RANK { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZDAY_REVENUE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZCASHIER_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZDISTRI_WAY { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public decimal? ZZFLD00005D { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZRECONCILIATION { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZACCOUNT_WAY { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZACCCOUNT_TIME { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZFIPERSON { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZFIPERSON_TEL { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string E_MAILSMT { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string URIURI { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string BZ { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZDELIVER_NOTE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZCARLIMIT_DESC { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZACCOUNT_PERIOD { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKABEER_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKABEER_PILE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKANONBEER_PILE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAICE_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKACOLD_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKASHELF_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKALEVEL_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAWHOLEBOX_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAPACKAGE_NUM { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAPILE_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKANONPILE_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAPRO_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKASHELF_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAICE_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKACOLD_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKACASHER_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAMULTI_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKADISPLAY_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAPILEOUT_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZFLD0000G2 { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAFLAG_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAPOST_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAAB_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZFLD0000G6 { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKALADDER_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKASERVICE_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKAPOP_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZKALIVELY_USE { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public int? ZZBOX { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZDECK_NAME { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZbnType { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZGSYYZZH { get; set; }

    /// <summary>
    ///  
    /// </summary>
    public string ZZGSZZMC { get; set; }
}