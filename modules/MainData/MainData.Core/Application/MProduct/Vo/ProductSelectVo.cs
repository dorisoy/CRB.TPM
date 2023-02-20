using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MProduct.Vo
{
    public class ProductSelectVo
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public Guid Value { get; set; }
    }
}
