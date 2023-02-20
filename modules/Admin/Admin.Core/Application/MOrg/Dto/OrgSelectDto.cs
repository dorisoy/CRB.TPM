using CRB.TPM.Data.Abstractions.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRB.TPM.Mod.Admin.Core.Application.MOrg.Dto
{
    public class OrgSelectDto : QueryDto
    {
        private IList<Guid> level1Ids;
        private IList<Guid> level2Ids;
        private IList<Guid> level3Ids;
        private IList<Guid> level4Ids;
        private IList<Guid> level5Ids;
        private IList<Guid> level6Ids;

        /// <summary>
        /// 哪一层级的数据
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 雪花id
        /// </summary>
        public IList<Guid> Level1Ids { get => level1Ids.RemoveGuidEmpty(); set => level1Ids = value; }
        /// <summary>
        /// 事业部id
        /// </summary>
        public IList<Guid> Level2Ids { get => level2Ids.RemoveGuidEmpty(); set => level2Ids = value; }
        /// <summary>
        /// 营销中心id
        /// </summary>
        public IList<Guid> Level3Ids { get => level3Ids.RemoveGuidEmpty(); set => level3Ids = value; }
        /// <summary>
        /// 大区id
        /// </summary>
        public IList<Guid> Level4Ids { get => level4Ids.RemoveGuidEmpty(); set => level4Ids = value; }
        /// <summary>
        /// 业务部id
        /// </summary>
        public IList<Guid> Level5Ids { get => level5Ids.RemoveGuidEmpty(); set => level5Ids = value; }
        /// <summary>
        /// 工作站id
        /// </summary>
        public IList<Guid> Level6Ids { get => level6Ids.RemoveGuidEmpty(); set => level6Ids = value; }
        /// <summary>
        /// 传了ids那么直接按照ids筛选出来返回全部列表 不要分页
        /// </summary>
        public IList<Guid> Ids { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 检查是否传了组织id
        /// </summary>
        /// <returns></returns>
        public bool AnyInputOrg()
        {
            return Level1Ids.NotNullAndEmpty() || Level2Ids.NotNullAndEmpty() || Level3Ids.NotNullAndEmpty() || Level4Ids.NotNullAndEmpty() || Level5Ids.NotNullAndEmpty() || Level6Ids.NotNullAndEmpty();
        }
    }
}
