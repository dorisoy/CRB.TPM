using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.MainData.Core.Domain.MProduct;
using CRB.TPM.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MProduct.Vo
{
    [ObjectMap(typeof(MProductEntity), true)]
    public class MProductQueryVo
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 瓶/箱
        /// </summary>
        public int? BottleBox { get; set; }
        /// <summary>
        /// 容量
        /// </summary>
        public int? Capacity { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string ClassName { get; set; } = string.Empty;
        /// <summary>
        ///  
        /// </summary>
        public string VolumeName { get; set; } = string.Empty;
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; } = string.Empty;
        /// <summary>
        /// 外包装
        /// </summary>
        public string OutPackName { get; set; } = string.Empty;
        /// <summary>
        /// 内包装
        /// </summary>
        public string InPackName { get; set; } = string.Empty;
        /// <summary>
        /// 类型1（17位码）；2（11位码）；3（促销品）……1（17位码）；2（11位码）；3（促销品）……
        /// </summary>
        public int? ProductType { get; set; }
        /// <summary>
        /// 产品规格
        /// </summary>
        public string ProductSpecName { get; set; } = string.Empty;
        /// <summary>
        /// 升转换 
        /// </summary>
		[Precision]
        public decimal? LitreConversionRate { get; set; }
        /// <summary>
        ///  
        /// </summary>
        [Length(1)]
        public bool Enabled { get; set; }
        /// <summary>
        /// 17位对应的11位码产品id
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 集团码
        /// </summary>
        [Length(20)]
        public string GroupCode { get; set; } = string.Empty;
        /// <summary>
        /// 集团名称
        /// </summary>
		[Nullable]
        [Length(200)]
        public string GroupName { get; set; } = string.Empty;
        /// <summary>
        ///  
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 特征码
        /// </summary>
		[Nullable]
        [Length(10)]
        public string CharacterCode { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
		[Nullable]
        [Length(500)]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 创建人名称
        /// </summary>
        [IgnoreOnEntityEvent]
        public virtual string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [IgnoreOnEntityEvent]
        public virtual DateTime CreatedTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改人名称
        /// </summary>
        [Nullable]
        [IgnoreOnEntityEvent]
        public virtual string Modifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [IgnoreOnEntityEvent]
        public virtual DateTime? ModifiedTime { get; set; }
    }
}
