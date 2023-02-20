using CRB.TPM.Data.Abstractions.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.MProduct.Dto
{
    public class ProductSelectDto : QueryDto
    {
        /// <summary>
        /// 已选ids
        /// </summary>
        public IList<Guid> Ids { get; set; }
        /// <summary>
        /// 产品编码/名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public int ProductType { get; set; }
    }
}
