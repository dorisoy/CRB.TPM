using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 数据库C#类型映射
    /// </summary>
    internal class DbCsharpTypeMap
    {
        private static readonly string SqlServerCSharp = "SqlServerCSharp";
        private static readonly string MySqlCSharp = "MySqlCSharp";
        private static readonly string PostgreSqlCSharp = "PostgreSqlCSharp";
        private static readonly string OracleCSharp = "OracleCSharp";
        private static readonly string SqLiteCSharp = "SQLiteCSharp";

        private static Dictionary<string, List<DbCsharpTypeEntity>> _dict;

        private static readonly object _lock = new object();

        private static Dictionary<string, List<DbCsharpTypeEntity>> Dict
        {
            get
            {
                if (_dict == null)
                {
                    lock (_lock)
                    {
                        if (_dict == null)
                        {
                            _dict = new Dictionary<string, List<DbCsharpTypeEntity>>();
                            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Dapper.Sharding.DbTypeMap.xml");
                            XDocument doc = XDocument.Load(stream);
                            XElement DbTypeMapElement = doc.Element("DbTypeMap");

                            foreach (XElement element in DbTypeMapElement.Elements("Database")) //遍历数据库
                            {
                                string dbProvider = element.Attribute("DbProvider").Value;
                                string language = element.Attribute("Language").Value;
                                string key = dbProvider + language;

                                List<DbCsharpTypeEntity> list;
                                if (!_dict.ContainsKey(key))
                                {
                                    list = new List<DbCsharpTypeEntity>();
                                    _dict[key] = list;
                                }
                                else
                                {
                                    list = _dict[key];
                                }

                                foreach (XElement el in element.Elements("DbType")) //遍历语言转换
                                {
                                    string Name = el.Attribute("Name").Value;
                                    string To = el.Attribute("To").Value.Replace("&lt;", "<").Replace("&gt;", ">");
                                    DbCsharpTypeEntity model = new DbCsharpTypeEntity();
                                    model.DbType = Name;
                                    model.CsStringType = To;
                                    list.Add(model);
                                }

                            }

                            stream.Dispose();
                        }
                    }
                }

                return _dict;
            }
        }

        public static List<DbCsharpTypeEntity> SqlServerMap
        {
            get
            {
                return Dict[SqlServerCSharp];
            }
        }

        public static List<DbCsharpTypeEntity> MySqlMap
        {
            get
            {
                return Dict[MySqlCSharp];
            }
        }

        public static List<DbCsharpTypeEntity> PostgreSqlMap
        {
            get
            {
                return Dict[PostgreSqlCSharp];
            }
        }

        public static List<DbCsharpTypeEntity> OracleMap
        {
            get
            {
                return Dict[OracleCSharp];
            }
        }

        public static List<DbCsharpTypeEntity> SqLiteMap
        {
            get
            {
                return Dict[SqLiteCSharp];
            }
        }

    }
}
