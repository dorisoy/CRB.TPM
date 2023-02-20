using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.MainData.Core.Application.SyncSetting.Dto
{
    public class Result_Setting
    {
        /// <summary>
        /// CRM字典数据
        /// </summary>
        public List<ET_DATA> ET_DATA { get; set; }
        /// <summary>
        /// 业务实体
        /// </summary>
        public List<ET_ZSNTM0024> ET_ZSNTM0024 { get; set; }
        /// <summary>
        /// 终端一二三级类型
        /// </summary>
        public List<ET_ZSNTM0040> ET_ZSNTM0040 { get; set; }
        /// <summary>
        /// 终端业态配置
        /// </summary>
        public List<ET_BYTE> ET_BYTE { get; set; }
        /// <summary>
        /// 职位优先级
        /// </summary>
        public List<ET_PRIORTY> ET_PRIORTY { get; set; }
        /// <summary>
        /// 制高点配置
        /// </summary>
        public List<ET_HEIGHT_CONF> ET_HEIGHT_CONF { get; set; }
        /// <summary>
        /// KA大系统
        /// </summary>
        public List<ET_093> ET_093 { get; set; }
        /// <summary>
        /// KA系统名称配置表
        /// </summary>
        public List<ET_ZSNTM0011> ET_ZSNTM0011 { get; set; }
        /// <summary>
        /// 国家省份
        /// </summary>
        public List<ET_ZSNS_COUNTRY_PROVINCE> ET_ZSNS_COUNTRY_PROVINCE { get; set; }
        /// <summary>
        /// 省份城市
        /// </summary>
        public List<ET_ZSNTM0015> ET_ZSNTM0015 { get; set; }
        /// <summary>
        /// 城市区县
        /// </summary>
        public List<ET_ZSNTM0016> ET_ZSNTM0016 { get; set; }
        /// <summary>
        /// 区县街道
        /// </summary>
        public List<ET_ZSNTM0078> ET_ZSNTM0078 { get; set; }
        /// <summary>
        /// 街道村
        /// </summary>
        public List<ET_ZSNTM0079> ET_ZSNTM0079 { get; set; }
    }
}
