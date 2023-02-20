using System.Collections.Generic;
using Z.Dapper.Plus;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// Dapper.Plus 辅助工具
    /// </summary>
    public class DapperPlusUtils
    {
        private static readonly object _dapperPlusLocker = new object();

        private static readonly HashSet<string> _dapperPlusDict = new HashSet<string>();

        public static string Map<T>(string tableName) where T : class
        {
            var key = typeof(T).FullName + tableName;
            if (!_dapperPlusDict.Contains(key))
            {
                lock (_dapperPlusLocker)
                {
                    if (!_dapperPlusDict.Contains(key))
                    {
                        var sqlField = new SqlFieldEntity(ClassToTableEntityUtils.Get<T>(DataBaseType.MySql), "", "", "@");
                        DapperPlusEntityMapper<T> map;
                        if (sqlField.IsIdentity)
                        {
                            //map = DapperPlusManager.Entity<T>(key).Identity(sqlField.PrimaryKey).Table(tableName);
                            map = DapperPlusManager.Entity<T>(key).Table(tableName);
                        }
                        else
                        {
                            map = DapperPlusManager.Entity<T>(key).Key(sqlField.PrimaryKey).Table(tableName);
                        }
                        //去除忽略
                        foreach (var item in sqlField.IgnoreFieldList)
                        {
                            map.Ignore(item);
                        }
                        _dapperPlusDict.Add(key);
                    }
                }
            }
            return key;
        }

    }
}
