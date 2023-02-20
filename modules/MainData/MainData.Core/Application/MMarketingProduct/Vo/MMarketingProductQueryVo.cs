using CRB.TPM.Data.Abstractions.Annotations;
using CRB.TPM.Mod.MainData.Core.Domain.MMarketingProduct;
using CRB.TPM.Mod.MainData.Core.Domain.MProductProperty;
using CRB.TPM.Utils.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MMarketingProduct.Vo
{
    [ObjectMap(typeof(MMarketingProductEntity), true)]
    public class MMarketingProductQueryVo
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 营销中心id
        /// </summary>
        public Guid MarketingId { get; set; }
        /// <summary>
        /// 营销中心编码
        /// </summary>
        public string MarketingCode { get; set; }
        /// <summary>
        /// 营销中心名称
        /// </summary>
        public string MarketingName { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        ///  1有效；0无效
        /// </summary>
        [Length(1)]
        public bool Enabled { get; set; }
    }
}
