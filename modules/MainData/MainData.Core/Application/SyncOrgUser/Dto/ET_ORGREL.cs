using CRB.TPM.Mod.Admin.Core.Infrastructure.Enums;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncOrgUser.Dto
{
    /// <summary>
    /// 组织数据
    /// </summary>
    public class ET_ORGREL
    {
        public string ORG_ID { get; set; }
        public string ORG_SHORT { get; set; }
        public string ORG_TEXT { get; set; }
        public string ORG_UP { get; set; }
        public string ORG_UP_SHORT { get; set; }
        public string ORG_UP_TEXT { get; set; }
        public string ORG_FUN { get; set; }
        public string ZSNCHDAT { get; set; }
        public int ORG_LVL
        {
            get
            {
                int result;
                int.TryParse(System.Convert.ToString(this.ORG_FUN), out result);

                int lvl = 0;
                switch (result)
                {
                    case 10:
                        lvl = (int)OrgEnumType.HeadOffice;
                        break;
                    case 20:
                        lvl = (int)OrgEnumType.BD;
                        break;
                    case 30:
                        lvl = (int)OrgEnumType.MarketingCenter;
                        break;
                    case 50:
                        lvl = (int)OrgEnumType.SaleRegion;
                        break;
                    case 60:
                        lvl = (int)OrgEnumType.Department;
                        break;
                    case 70:
                        lvl = (int)OrgEnumType.Station;
                        break;
                    default:
                        lvl = 0;
                        break;
                }
                return lvl;
            }
        }
    }
}
