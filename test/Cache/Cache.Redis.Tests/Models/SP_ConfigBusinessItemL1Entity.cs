using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Cache.Redis.Tests.Models
{
    public partial class SP_ConfigBusinessItemL1Entity
    {
        public long ID { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public string ItemName { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public string Creator { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public DateTime CreatedDate { get; set; } = System.DateTime.Now;

        /// <summary>
        ///  
        /// </summary>
        public string Modifier { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public DateTime ModifiedDate { get; set; } = System.DateTime.Now;

        /// <summary>
        ///  
        /// </summary>
        public string IsValid { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public string Explain { get; set; } = string.Empty;

        /// <summary>
        ///  
        /// </summary>
        public string IsEnable { get; set; } = string.Empty;

        /// <summary>
        /// 生成测试数据集合
        /// </summary>
        /// <param name="num">生成多少数量</param>
        /// <returns></returns>
        public static IList<SP_ConfigBusinessItemL1Entity> CreateTestDataList(int num)
        {
            IList<SP_ConfigBusinessItemL1Entity> res = new List<SP_ConfigBusinessItemL1Entity>();
            var nowTime = DateTime.Now;
            var codeStr = "ABCDEFGHIJKMLN";
            for (int i = 0; i < num; i++)
            {
                SP_ConfigBusinessItemL1Entity dto = new SP_ConfigBusinessItemL1Entity();
                for (int j = 0; j < 3; j++)
                {
                    dto.Code += codeStr[new Random().Next(0, codeStr.Length)];
                }
                for (int k = 0; k < 5; k++)
                {
                    dto.Code += new Random().Next(0, 9);
                }
                dto.ID = i;
                dto.ItemName = "TEST_" + i.ToString();
                dto.Creator = "0010081095";
                dto.CreatedDate = nowTime;
                dto.Modifier = "0010081095";
                dto.ModifiedDate = nowTime;
                dto.IsValid = "0";
                dto.Explain = "TEST：根据对经销商考核管理需要，公司在经销商正常利润之外，事前分产品规划的、事后根据考核结果，向经销商兑现";
                dto.IsEnable = "0";
                res.Add(dto);
            }
            return res;
        }
    }
}
