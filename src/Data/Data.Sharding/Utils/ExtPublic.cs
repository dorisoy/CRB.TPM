using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.CustomTypeProviders;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// 公共扩展方法
    /// </summary>
    public static class ExtPublic
    {
        #region string

        public static string AsPgsqlField(this string data, char separator = ',')
        {
            var sb = new StringBuilder();
            var arr = data.Split(separator);
            foreach (var item in arr)
            {
                sb.Append($"{item} as \"{item}\"");
                if (item != arr.Last())
                {
                    sb.Append(',');
                }
            }
            return sb.ToString();
        }

        private static string AsMySqlField(this string data)
        {
            var sb = new StringBuilder();
            var arr = data.Split(',');
            foreach (var item in arr)
            {
                sb.Append($"{item} as `{item}`");
                if (item != arr.Last())
                {
                    sb.Append(',');
                }
            }
            return sb.ToString();
        }

        private static string AsSqlServerField(this string data)
        {
            var sb = new StringBuilder();
            var arr = data.Split(',');
            foreach (var item in arr)
            {
                sb.Append($"{item} as [{item}]");
                if (item != arr.Last())
                {
                    sb.Append(',');
                }
            }
            return sb.ToString();
        }

        public static List<string> SplitToList(this string data, char separator = ',')
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            return data.Split(separator).ToList();
        }

        #endregion

        #region dapper

        public static DataTable GetDataTable(this IDbConnection conn, string sql, object param = null, IDbTransaction tran = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var reader = conn.ExecuteReader(sql, param, tran, commandTimeout, commandType))
            {
                var dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
        }

        public static DataSet GetDataSet(this IDbConnection conn, string sql, object param = null, IDbTransaction tran = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var reader = conn.ExecuteReader(sql, param, tran, commandTimeout, commandType))
            {
                var ds = new DataSet();
                int i = 0;
                while (!reader.IsClosed)
                {
                    var dt = new DataTable();
                    dt.TableName = "t" + i;
                    dt.Load(reader);
                    ds.Tables.Add(dt);
                    i++;
                }
                return ds;
            }
        }

        public static async Task<DataTable> GetDataTableAsync(this IDbConnection conn, string sql, object param = null, IDbTransaction tran = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var reader = await conn.ExecuteReaderAsync(sql, param, tran, commandTimeout, commandType))
            {
                var dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
        }

        public static async Task<DataSet> GetDataSetAsync(this IDbConnection conn, string sql, object param = null, IDbTransaction tran = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var reader = await conn.ExecuteReaderAsync(sql, param, tran, commandTimeout, commandType))
            {
                var ds = new DataSet();
                int i = 0;
                while (!reader.IsClosed)
                {
                    var dt = new DataTable();
                    dt.TableName = "t" + i;
                    dt.Load(reader);
                    ds.Tables.Add(dt);
                    i++;
                }
                return ds;
            }
        }

        #endregion

        #region fastmember

        public static DataTable ToDataTable<T>(this IEnumerable<T> dataList) where T : class
        {
            var table = new DataTable();
            using (var reader = ObjectReader.Create(dataList))
            {
                table.Load(reader);
            }
            return table;
        }
        public static IEnumerable<T> ToEnumerableList<T>(this DataTable table) where T : class, new()
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var proList = accessor.GetMembers().Select(s => s.Name);
            foreach (DataRow row in table.Rows)
            {
                var model = new T();
                foreach (var name in proList)
                {
                    var val = row[name];
                    if (val != DBNull.Value)
                    {
                        accessor[model, name] = val;
                    }
                }
                yield return model;
            }
        }

        #endregion

        #region IEnumerable map base

        public static void ListOneToOne<T, T2>(this IEnumerable<T> list, string field, string propertyName, IEnumerable<T2> mapList, string mapField) where T : class where T2 : class
        {
            if (list == null || !list.Any())
                return;
            if (mapList == null || !mapList.Any())
                return;
            var accessor = TypeAccessor.Create(typeof(T));
            var queryable = mapList.AsQueryable();
            var isDynamic = mapList.First() is IDictionary<string, object>;
            foreach (var item in list)
            {
                var id = accessor[item, field];
                if (id == null)
                {
                    continue;
                }
                if (!isDynamic)
                {
                    accessor[item, propertyName] = queryable.FirstOrDefault($"{mapField}=@0", id);
                }
                else
                {
                    if (id is int)
                    {
                        accessor[item, propertyName] = queryable.FirstOrDefault($"ExtUtils.Int({mapField})=@0", id);
                    }
                    else if (id is long)
                    {
                        accessor[item, propertyName] = queryable.FirstOrDefault($"ExtUtils.Long({mapField})=@0", id);
                    }
                    else if (id is decimal)
                    {
                        accessor[item, propertyName] = queryable.FirstOrDefault($"ExtUtils.Decimal({mapField})=@0", id);
                    }
                    else if (id is string)
                    {
                        accessor[item, propertyName] = queryable.FirstOrDefault($"ExtUtils.String({mapField})=@0", id);
                    }
                    else
                    {
                        accessor[item, propertyName] = queryable.FirstOrDefault($"ExtUtils.Object({mapField})=@0", id.ToString());
                    }
                }
            }
        }

        public static void ListOneToMany<T, T2>(this IEnumerable<T> list, string field, string propertyName, IEnumerable<T2> mapList, string mapField) where T : class where T2 : class
        {
            if (list == null || !list.Any())
                return;
            if (mapList == null || !mapList.Any())
                return;
            var accessor = TypeAccessor.Create(typeof(T));
            var queryable = mapList.AsQueryable();
            var isDynamic = mapList.First() is IDictionary<string, object>;
            foreach (var item in list)
            {
                var id = accessor[item, field];
                if (id == null)
                {
                    continue;
                }
                if (!isDynamic)
                {
                    accessor[item, propertyName] = queryable.Where($"{mapField}=@0", id).ToList();
                }
                else
                {
                    if (id is int)
                    {
                        accessor[item, propertyName] = queryable.Where($"ExtUtils.Int({mapField})==@0", id).ToList();
                    }
                    else if (id is long)
                    {
                        accessor[item, propertyName] = queryable.Where($"ExtUtils.Long({mapField})==@0", id).ToList();
                    }
                    else if (id is decimal)
                    {
                        accessor[item, propertyName] = queryable.Where($"ExtUtils.Decimal({mapField})=@0", id).ToList();
                    }
                    else if (id is string)
                    {
                        accessor[item, propertyName] = queryable.Where($"ExtUtils.String({mapField})=@0", id).ToList();
                    }
                    else
                    {
                        accessor[item, propertyName] = queryable.Where($"ExtUtils.Object({mapField})=@0", id.ToString());
                    }
                }
            }
        }

        public static void ListCenterToMany<T, T2, T3>(this IEnumerable<T> list, string field, string propertyName, IEnumerable<T2> centerList, string prevField, string nextField, IEnumerable<T3> mapList, string mapField) where T : class where T2 : class where T3 : class
        {
            if (list == null || !list.Any())
                return;
            if (centerList == null || !centerList.Any())
            {
                return;
            }
            if (mapList == null || !mapList.Any())
                return;
            var accessor = TypeAccessor.Create(typeof(T));
            var centerqueryable = centerList.AsQueryable();
            var queryable = mapList.AsQueryable();
            var isDynamic = mapList.First() is IDictionary<string, object>;
            foreach (var item in list)
            {
                var id = accessor[item, field];
                if (id == null)
                {
                    continue;
                }
                var ids = centerqueryable.Where($"{prevField}=@0", id).Select(nextField);
                if (!ids.Any())
                    continue;

                if (!isDynamic)
                {
                    accessor[item, propertyName] = queryable.Where($"@0.Contains({mapField})", ids).ToList();
                }
                else
                {
                    var first = ids.FirstOrDefault("it!=null");

                    if (first is int)
                    {
                        accessor[item, propertyName] = queryable.Where($"@0.Contains(ExtUtils.Int({mapField}))", ids).ToList();
                    }
                    else if (first is long)
                    {
                        accessor[item, propertyName] = queryable.Where($"@0.Contains(ExtUtils.Long({mapField}))", ids).ToList();
                    }
                    else if (first is decimal)
                    {
                        accessor[item, propertyName] = queryable.Where($"@0.Contains(ExtUtils.Decimal({mapField}))", ids).ToList();
                    }
                    else if (first is string)
                    {
                        accessor[item, propertyName] = queryable.Where($"@0.Contains(ExtUtils.String({mapField}))", ids).ToList();
                    }
                    else
                    {
                        ids = ids.Select("new(it.ToString())");
                        accessor[item, propertyName] = queryable.Where($"@0.Contains(ExtUtils.Object({mapField}))", ids).ToList();
                    }
                }
            }
        }

        #endregion

        #region IEnumerable map oneToOne oneToMany

        private static IEnumerable<T2> Com<T, T2>(this IEnumerable<T> list, string field, ITable<T2> table, string mapField, string returnFields = null) where T : class where T2 : class
        {
            if (list == null || !list.Any())
                return Enumerable.Empty<T2>();
            var ids = list.AsQueryable().Where($"{field}!=null").Select(field).Distinct();
            var idsCount = ids.Count();
            if (idsCount == 0)
            {
                return Enumerable.Empty<T2>();
            }
            var first = ids.First();
            Type t = first.GetType();
            IEnumerable<T2> data;
            if (mapField.ToLower() == table.SqlField.PrimaryKey.ToLower())
            {
                if (idsCount > 1)
                {
                    if (t == typeof(long))
                    {
                        data = table.GetByIds(ids.OfType<long>().ToList(), returnFields);
                    }
                    else if (t == typeof(string))
                    {
                        data = table.GetByIds(ids.OfType<string>().ToList(), returnFields);
                    }
                    else if (t == typeof(int))
                    {
                        data = table.GetByIds(ids.OfType<int>().ToList(), returnFields);
                    }
                    else if (t == typeof(decimal))
                    {
                        data = table.GetByIds(ids.OfType<decimal>().ToList(), returnFields);
                    }
                    else
                    {
                        data = table.GetByIds(ids.ToDynamicList(), returnFields);
                    }
                }
                else
                {
                    if (t == typeof(long))
                    {
                        data = new List<T2> { table.GetById((long)first, returnFields) };
                    }
                    else if (t == typeof(string))
                    {
                        data = new List<T2> { table.GetById((string)first, returnFields) };
                    }
                    else if (t == typeof(int))
                    {
                        data = new List<T2> { table.GetById((int)first, returnFields) };
                    }
                    else if (t == typeof(decimal))
                    {
                        data = new List<T2> { table.GetById((decimal)first, returnFields) };
                    }
                    else
                    {
                        data = new List<T2> { table.GetById(first, returnFields) };
                    }

                }

            }
            else
            {
                if (idsCount > 1)
                {
                    if (t == typeof(long))
                    {
                        data = table.GetByIdsWithField(ids.OfType<long>().ToList(), mapField, returnFields);
                    }
                    else if (t == typeof(string))
                    {
                        data = table.GetByIdsWithField(ids.OfType<string>().ToList(), mapField, returnFields);
                    }
                    else if (t == typeof(int))
                    {
                        data = table.GetByIdsWithField(ids.OfType<int>().ToList(), mapField, returnFields);
                    }
                    else if (t == typeof(decimal))
                    {
                        data = table.GetByIdsWithField(ids.OfType<decimal>().ToList(), mapField, returnFields);
                    }
                    else
                    {
                        data = table.GetByIdsWithField(ids.ToDynamicList(), mapField, returnFields);
                    }

                }
                else
                {
                    if (t == typeof(long))
                    {
                        data = table.GetByWhere($"WHERE {mapField}=@id", new { id = (long)first }, returnFields);
                    }
                    else if (t == typeof(string))
                    {
                        data = table.GetByWhere($"WHERE {mapField}=@id", new { id = (string)first }, returnFields);
                    }
                    else if (t == typeof(int))
                    {
                        data = table.GetByWhere($"WHERE {mapField}=@id", new { id = (int)first }, returnFields);
                    }
                    else if (t == typeof(decimal))
                    {
                        data = table.GetByWhere($"WHERE {mapField}=@id", new { id = (decimal)first }, returnFields);
                    }
                    else
                    {
                        data = table.GetByWhere($"WHERE {mapField}=@id", new { id = first }, returnFields);
                    }
                }
            }
            return data;
        }

        public static void TableOneToOne<T, T2>(this IEnumerable<T> list, string field, string propertyName, ITable<T2> table, string mapField, string returnFields = null) where T : class where T2 : class
        {
            var data = Com(list, field, table, mapField, returnFields);
            list.ListOneToOne(field, propertyName, data, mapField);
        }

        public static void TableOneToMany<T, T2>(this IEnumerable<T> list, string field, string propertyName, ITable<T2> table, string mapField, string returnFields = null) where T : class where T2 : class
        {
            var data = Com(list, field, table, mapField, returnFields);
            list.ListOneToMany(field, propertyName, data, mapField);
        }

        public static void TableOneToMany<T, T2>(this IEnumerable<T> list, string field, string propertyName, ITable<T2> table, string mapField, string returnFields, string and, DynamicParameters par = null, string orderby = null) where T : class where T2 : class
        {
            if (list == null || !list.Any())
                return;
            var ids = list.AsQueryable().Where($"{field}!=null").Select(field).Distinct();
            var idsCount = ids.Count();
            if (idsCount == 0)
            {
                return;
            };
            var t = ids.First().GetType();
            if (par == null)
                par = new DynamicParameters();
            if (t == typeof(long))
            {
                par.Add("@ids", ids.OfType<long>().ToList());
            }
            else if (t == typeof(string))
            {
                par.Add("@ids", ids.OfType<string>().ToList());
            }
            else if (t == typeof(int))
            {
                par.Add("@ids", ids.OfType<int>().ToList());
            }
            else if (t == typeof(decimal))
            {
                par.Add("@ids", ids.OfType<decimal>().ToList());
            }
            else
            {
                par.Add("@ids", ids.ToDynamicList());
            }
            string where;
            if (table.DataBase.Client.DbType == DataBaseType.Postgresql)
            {
                where = $"WHERE {mapField}=ANY(@ids) {and}";
            }
            else
            {
                where = $"WHERE {mapField} IN @ids {and}";
            }
            var data = table.GetByWhere(where, par, returnFields, orderby);
            list.ListOneToMany(field, propertyName, data, mapField);
        }

        #endregion

        #region IEnumerable map oneToOne oneToMany Dynamic

        private static IEnumerable<dynamic> ComDynamic<T, T2>(this IEnumerable<T> list, string field, ITable<T2> table, string mapField, string returnFields = null) where T : class where T2 : class
        {
            if (list == null || !list.Any())
                return Enumerable.Empty<dynamic>();
            var ids = list.AsQueryable().Where($"{field}!=null").Select(field).Distinct();
            var idsCount = ids.Count();
            if (idsCount == 0)
            {
                return Enumerable.Empty<dynamic>();
            }
            var first = ids.First();
            Type t = first.GetType();
            IEnumerable<dynamic> data;
            if (mapField.ToLower() == table.SqlField.PrimaryKey.ToLower())
            {
                if (idsCount > 1)
                {
                    if (t == typeof(long))
                    {
                        data = table.GetByIdsDynamic(ids.OfType<long>().ToList(), returnFields);
                    }
                    else if (t == typeof(string))
                    {
                        data = table.GetByIdsDynamic(ids.OfType<string>().ToList(), returnFields);
                    }
                    else if (t == typeof(int))
                    {
                        data = table.GetByIdsDynamic(ids.OfType<int>().ToList(), returnFields);
                    }
                    else if (t == typeof(decimal))
                    {
                        data = table.GetByIdsDynamic(ids.OfType<decimal>().ToList(), returnFields);
                    }
                    else
                    {
                        data = table.GetByIdsDynamic(ids.ToDynamicList(), returnFields);
                    }
                }
                else
                {
                    if (t == typeof(long))
                    {
                        data = new List<dynamic> { table.GetByIdDynamic((long)first, returnFields) };
                    }
                    else if (t == typeof(string))
                    {
                        data = new List<dynamic> { table.GetByIdDynamic((string)first, returnFields) };
                    }
                    else if (t == typeof(int))
                    {
                        data = new List<dynamic> { table.GetByIdDynamic((int)first, returnFields) };
                    }
                    else if (t == typeof(decimal))
                    {
                        data = new List<dynamic> { table.GetByIdDynamic((decimal)first, returnFields) };
                    }
                    else
                    {
                        data = new List<dynamic> { table.GetByIdDynamic(first, returnFields) };
                    }

                }

            }
            else
            {
                if (idsCount > 1)
                {
                    if (t == typeof(long))
                    {
                        data = table.GetByIdsWithFieldDynamic(ids.OfType<long>().ToList(), mapField, returnFields);
                    }
                    else if (t == typeof(string))
                    {
                        data = table.GetByIdsWithFieldDynamic(ids.OfType<string>().ToList(), mapField, returnFields);
                    }
                    else if (t == typeof(int))
                    {
                        data = table.GetByIdsWithFieldDynamic(ids.OfType<int>().ToList(), mapField, returnFields);
                    }
                    else if (t == typeof(decimal))
                    {
                        data = table.GetByIdsWithFieldDynamic(ids.OfType<decimal>().ToList(), mapField, returnFields);
                    }
                    else
                    {
                        data = table.GetByIdsWithFieldDynamic(ids.ToDynamicList(), mapField, returnFields);
                    }

                }
                else
                {
                    if (t == typeof(long))
                    {
                        data = table.GetByWhereDynamic($"WHERE {mapField}=@id", new { id = (long)first }, returnFields);
                    }
                    else if (t == typeof(string))
                    {
                        data = table.GetByWhereDynamic($"WHERE {mapField}=@id", new { id = (string)first }, returnFields);
                    }
                    else if (t == typeof(int))
                    {
                        data = table.GetByWhereDynamic($"WHERE {mapField}=@id", new { id = (int)first }, returnFields);
                    }
                    else if (t == typeof(decimal))
                    {
                        data = table.GetByWhereDynamic($"WHERE {mapField}=@id", new { id = (decimal)first }, returnFields);
                    }
                    else
                    {
                        data = table.GetByWhereDynamic($"WHERE {mapField}=@id", new { id = first }, returnFields);
                    }
                }
            }
            return data;
        }

        public static void TableOneToOneDynamic<T, T2>(this IEnumerable<T> list, string field, string propertyName, ITable<T2> table, string mapField, string returnFields = null) where T : class where T2 : class
        {
            var data = ComDynamic(list, field, table, mapField, returnFields);
            list.ListOneToOne(field, propertyName, data, mapField);
        }

        public static void TableOneToManyDynamic<T, T2>(this IEnumerable<T> list, string field, string propertyName, ITable<T2> table, string mapField, string returnFields = null) where T : class where T2 : class
        {
            var data = ComDynamic(list, field, table, mapField, returnFields);
            list.ListOneToMany(field, propertyName, data, mapField);
        }

        public static void TableOneToManyDynamic<T, T2>(this IEnumerable<T> list, string field, string propertyName, ITable<T2> table, string mapField, string returnFields, string and, DynamicParameters par = null, string orderby = null) where T : class where T2 : class
        {
            if (list == null || !list.Any())
                return;
            var ids = list.AsQueryable().Where($"{field}!=null").Select(field).Distinct();
            var idsCount = ids.Count();
            if (idsCount == 0)
            {
                return;
            };
            var t = ids.First().GetType();
            if (par == null)
                par = new DynamicParameters();
            if (t == typeof(long))
            {
                par.Add("@ids", ids.OfType<long>().ToList());
            }
            else if (t == typeof(string))
            {
                par.Add("@ids", ids.OfType<string>().ToList());
            }
            else if (t == typeof(int))
            {
                par.Add("@ids", ids.OfType<int>().ToList());
            }
            else if (t == typeof(decimal))
            {
                par.Add("@ids", ids.OfType<decimal>().ToList());
            }
            else
            {
                par.Add("@ids", ids.ToDynamicList());
            }
            string where;
            if (table.DataBase.Client.DbType == DataBaseType.Postgresql)
            {
                where = $"WHERE {mapField}=ANY(@ids) {and}";
            }
            else
            {
                where = $"WHERE {mapField} IN @ids {and}";
            }
            var data = table.GetByWhereDynamic(where, par, returnFields, orderby);
            list.ListOneToMany(field, propertyName, data, mapField);
        }

        #endregion

        #region IEnumerable map center to many

        public static void TableCenterToMany<T, T2, T3>(this IEnumerable<T> list, string field, string propertyName, ITable<T2> centerTable, string prevField, string nextField, ITable<T3> mapTable, string mapField, string returnFields = null) where T : class where T2 : class where T3 : class
        {
            if (list == null || !list.Any())
                return;
            var ids = list.AsQueryable().Where($"{field}!=null").Select(field).Distinct();
            var idsCount = ids.Count();
            if (idsCount == 0)
            {
                return;
            }
            var first = ids.First();
            Type t = first.GetType();
            IEnumerable<T2> data;
            if (idsCount > 1)
            {
                if (t == typeof(long))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<long>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(string))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<string>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(int))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<int>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(decimal))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<decimal>().ToList(), prevField, returnFields);
                }
                else
                {
                    data = centerTable.GetByIdsWithField(ids.ToDynamicList(), prevField, returnFields);
                }

            }
            else
            {
                if (t == typeof(long))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (long)first });
                }
                else if (t == typeof(string))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (string)first });

                }
                else if (t == typeof(int))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (int)first });
                }
                else if (t == typeof(decimal))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (decimal)first });
                }
                else
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = first });
                }

            }

            var ids2 = data.AsQueryable().Where($"{nextField}!=null").Select(nextField).Distinct();
            var ids2Count = ids2.Count();
            IEnumerable<T3> data2;
            if (ids2Count > 0)
            {
                var first2 = ids2.First();
                Type t2 = first2.GetType();
                if (nextField.ToLower() == mapTable.SqlField.PrimaryKey.ToLower()) //主键
                {
                    if (ids2Count > 1)
                    {
                        if (t2 == typeof(long))
                        {
                            data2 = mapTable.GetByIds(ids2.OfType<long>().ToList(), returnFields);
                        }
                        else if (t2 == typeof(string))
                        {
                            data2 = mapTable.GetByIds(ids2.OfType<string>().ToList(), returnFields);
                        }
                        else if (t2 == typeof(int))
                        {
                            data2 = mapTable.GetByIds(ids2.OfType<int>().ToList(), returnFields);
                        }
                        else if (t2 == typeof(decimal))
                        {
                            data2 = mapTable.GetByIds(ids2.OfType<decimal>().ToList(), returnFields);
                        }
                        else
                        {
                            data2 = mapTable.GetByIds(ids2.ToDynamicList(), returnFields);
                        }

                    }
                    else
                    {
                        if (t2 == typeof(long))
                        {
                            data2 = new List<T3> { mapTable.GetById((long)first2, returnFields) };
                        }
                        else if (t2 == typeof(string))
                        {
                            data2 = new List<T3> { mapTable.GetById((string)first2, returnFields) };
                        }
                        else if (t2 == typeof(int))
                        {
                            data2 = new List<T3> { mapTable.GetById((int)first2, returnFields) };
                        }
                        else if (t2 == typeof(decimal))
                        {
                            data2 = new List<T3> { mapTable.GetById((decimal)first2, returnFields) };
                        }
                        else
                        {
                            data2 = new List<T3> { mapTable.GetById(first2, returnFields) };
                        }

                    }
                }
                else
                {
                    if (ids2Count == 1)
                    {
                        if (t2 == typeof(long))
                        {
                            data2 = mapTable.GetByIdsWithField(ids2.OfType<long>().ToList(), mapField, returnFields);
                        }
                        else if (t2 == typeof(string))
                        {
                            data2 = mapTable.GetByIdsWithField(ids2.OfType<string>().ToList(), mapField, returnFields);
                        }
                        else if (t2 == typeof(int))
                        {
                            data2 = mapTable.GetByIdsWithField(ids2.OfType<int>().ToList(), mapField, returnFields);
                        }
                        else if (t2 == typeof(decimal))
                        {
                            data2 = mapTable.GetByIdsWithField(ids2.OfType<decimal>().ToList(), mapField, returnFields);
                        }
                        else
                        {
                            data2 = mapTable.GetByIdsWithField(ids2.ToDynamicList(), mapField, returnFields);
                        }

                    }
                    else
                    {
                        if (t2 == typeof(long))
                        {
                            data2 = mapTable.GetByWhere($"WHERE {mapField}=@id", new { id = (long)first2 }, returnFields);
                        }
                        else if (t2 == typeof(string))
                        {
                            data2 = mapTable.GetByWhere($"WHERE {mapField}=@id", new { id = (string)first2 }, returnFields);
                        }
                        else if (t2 == typeof(int))
                        {
                            data2 = mapTable.GetByWhere($"WHERE {mapField}=@id", new { id = (int)first2 }, returnFields);
                        }
                        else if (t2 == typeof(decimal))
                        {
                            data2 = mapTable.GetByWhere($"WHERE {mapField}=@id", new { id = (decimal)first2 }, returnFields);
                        }
                        else
                        {
                            data2 = mapTable.GetByWhere($"WHERE {mapField}=@id", new { id = first2 }, returnFields);
                        }

                    }
                }
            }
            else
            {
                data2 = Enumerable.Empty<T3>();
            }
            list.ListCenterToMany(field, propertyName, data, prevField, nextField, data2, mapField);
        }

        public static void TableCenterToMany<T, T2, T3>(this IEnumerable<T> list, string field, string propertyName, ITable<T2> centerTable, string prevField, string nextField, ITable<T3> mapTable, string mapField, string returnFields, string and, DynamicParameters par = null, string orderby = null) where T : class where T2 : class where T3 : class
        {
            if (list == null || !list.Any())
                return;
            var ids = list.AsQueryable().Where($"{field}!=null").Select(field).Distinct();
            var idsCount = ids.Count();
            if (idsCount == 0)
            {
                return;
            }
            var first = ids.First();
            Type t = first.GetType();
            IEnumerable<T2> data;
            if (idsCount > 1)
            {
                if (t == typeof(long))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<long>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(string))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<string>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(int))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<int>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(decimal))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<decimal>().ToList(), prevField, returnFields);
                }
                else
                {
                    data = centerTable.GetByIdsWithField(ids.ToDynamicList(), prevField, returnFields);
                }

            }
            else
            {
                if (t == typeof(long))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (long)first });
                }
                else if (t == typeof(string))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (string)first });
                }
                else if (t == typeof(int))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (int)first });
                }
                else if (t == typeof(decimal))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (decimal)first });
                }
                else
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = first });
                }

            }

            var ids2 = data.AsQueryable().Where($"{nextField}!=null").Select(nextField).Distinct();
            var ids2Count = ids2.Count();
            IEnumerable<T3> data2;
            if (ids2Count > 0)
            {
                Type t2 = ids2.First().GetType();
                if (par == null)
                    par = new DynamicParameters();

                if (t2 == typeof(long))
                {
                    par.Add("@ids", ids2.OfType<long>().ToList());
                }
                else if (t2 == typeof(string))
                {
                    par.Add("@ids", ids2.OfType<string>().ToList());
                }
                else if (t2 == typeof(int))
                {
                    par.Add("@ids", ids2.OfType<int>().ToList());
                }
                else if (t2 == typeof(decimal))
                {
                    par.Add("@ids", ids2.OfType<decimal>().ToList());
                }
                else
                {
                    par.Add("@ids", ids2.ToDynamicList());
                }

                string where;
                if (mapTable.DataBase.Client.DbType == DataBaseType.Postgresql)
                {
                    where = $"WHERE {mapField}=ANY(@ids) {and}";
                }
                else
                {
                    where = $"WHERE {mapField} IN @ids {and}";
                }
                data2 = mapTable.GetByWhere(where, par, returnFields, orderby);
            }
            else
            {
                data2 = Enumerable.Empty<T3>();
            }
            list.ListCenterToMany(field, propertyName, data, prevField, nextField, data2, mapField);

        }

        #endregion

        #region IEnumerable map center to many Dynamic

        public static void TableCenterToManyDynamic<T, T2, T3>(this IEnumerable<T> list, string field, string propertyName, ITable<T2> centerTable, string prevField, string nextField, ITable<T3> mapTable, string mapField, string returnFields = null) where T : class where T2 : class where T3 : class
        {
            if (list == null || !list.Any())
                return;
            var ids = list.AsQueryable().Where($"{field}!=null").Select(field).Distinct();
            var idsCount = ids.Count();
            if (idsCount == 0)
            {
                return;
            }
            var first = ids.First();
            Type t = first.GetType();
            IEnumerable<T2> data;
            if (idsCount > 1)
            {
                if (t == typeof(long))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<long>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(string))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<string>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(int))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<int>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(decimal))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<decimal>().ToList(), prevField, returnFields);
                }
                else
                {
                    data = centerTable.GetByIdsWithField(ids.ToDynamicList(), prevField, returnFields);
                }

            }
            else
            {
                if (t == typeof(long))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (long)first });
                }
                else if (t == typeof(string))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (string)first });

                }
                else if (t == typeof(int))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (int)first });
                }
                else if (t == typeof(decimal))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (decimal)first });
                }
                else
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = first });
                }

            }

            var ids2 = data.AsQueryable().Where($"{nextField}!=null").Select(nextField).Distinct();
            var ids2Count = ids2.Count();
            IEnumerable<dynamic> data2;
            if (ids2Count > 0)
            {
                var first2 = ids2.First();
                Type t2 = first2.GetType();
                if (nextField.ToLower() == mapTable.SqlField.PrimaryKey.ToLower()) //主键
                {
                    if (ids2Count > 1)
                    {
                        if (t2 == typeof(long))
                        {
                            data2 = mapTable.GetByIdsDynamic(ids2.OfType<long>().ToList(), returnFields);
                        }
                        else if (t2 == typeof(string))
                        {
                            data2 = mapTable.GetByIdsDynamic(ids2.OfType<string>().ToList(), returnFields);
                        }
                        else if (t2 == typeof(int))
                        {
                            data2 = mapTable.GetByIdsDynamic(ids2.OfType<int>().ToList(), returnFields);
                        }
                        else if (t2 == typeof(decimal))
                        {
                            data2 = mapTable.GetByIdsDynamic(ids2.OfType<decimal>().ToList(), returnFields);
                        }
                        else
                        {
                            data2 = mapTable.GetByIdsDynamic(ids2.ToDynamicList(), returnFields);
                        }

                    }
                    else
                    {
                        if (t2 == typeof(long))
                        {
                            data2 = new List<dynamic> { mapTable.GetByIdDynamic((long)first2, returnFields) };
                        }
                        else if (t2 == typeof(string))
                        {
                            data2 = new List<dynamic> { mapTable.GetByIdDynamic((string)first2, returnFields) };
                        }
                        else if (t2 == typeof(int))
                        {
                            data2 = new List<dynamic> { mapTable.GetByIdDynamic((int)first2, returnFields) };
                        }
                        else if (t2 == typeof(decimal))
                        {
                            data2 = new List<dynamic> { mapTable.GetByIdDynamic((decimal)first2, returnFields) };
                        }
                        else
                        {
                            data2 = new List<dynamic> { mapTable.GetByIdDynamic(first2, returnFields) };
                        }

                    }
                }
                else
                {
                    if (ids2Count == 1)
                    {
                        if (t2 == typeof(long))
                        {
                            data2 = mapTable.GetByIdsWithFieldDynamic(ids2.OfType<long>().ToList(), mapField, returnFields);
                        }
                        else if (t2 == typeof(string))
                        {
                            data2 = mapTable.GetByIdsWithFieldDynamic(ids2.OfType<string>().ToList(), mapField, returnFields);
                        }
                        else if (t2 == typeof(int))
                        {
                            data2 = mapTable.GetByIdsWithFieldDynamic(ids2.OfType<int>().ToList(), mapField, returnFields);
                        }
                        else if (t2 == typeof(decimal))
                        {
                            data2 = mapTable.GetByIdsWithFieldDynamic(ids2.OfType<decimal>().ToList(), mapField, returnFields);
                        }
                        else
                        {
                            data2 = mapTable.GetByIdsWithFieldDynamic(ids2.ToDynamicList(), mapField, returnFields);
                        }

                    }
                    else
                    {
                        if (t2 == typeof(long))
                        {
                            data2 = mapTable.GetByWhereDynamic($"WHERE {mapField}=@id", new { id = (long)first2 }, returnFields);
                        }
                        else if (t2 == typeof(string))
                        {
                            data2 = mapTable.GetByWhereDynamic($"WHERE {mapField}=@id", new { id = (string)first2 }, returnFields);
                        }
                        else if (t2 == typeof(int))
                        {
                            data2 = mapTable.GetByWhereDynamic($"WHERE {mapField}=@id", new { id = (int)first2 }, returnFields);
                        }
                        else if (t2 == typeof(decimal))
                        {
                            data2 = mapTable.GetByWhereDynamic($"WHERE {mapField}=@id", new { id = (decimal)first2 }, returnFields);
                        }
                        else
                        {
                            data2 = mapTable.GetByWhereDynamic($"WHERE {mapField}=@id", new { id = first2 }, returnFields);
                        }

                    }
                }
            }
            else
            {
                data2 = Enumerable.Empty<dynamic>();
            }
            list.ListCenterToMany(field, propertyName, data, prevField, nextField, data2, mapField);
        }

        public static void TableCenterToManyDynamic<T, T2, T3>(this IEnumerable<T> list, string field, string propertyName, ITable<T2> centerTable, string prevField, string nextField, ITable<T3> mapTable, string mapField, string returnFields, string and, DynamicParameters par = null, string orderby = null) where T : class where T2 : class where T3 : class
        {
            if (list == null || !list.Any())
                return;
            var ids = list.AsQueryable().Where($"{field}!=null").Select(field).Distinct();
            var idsCount = ids.Count();
            if (idsCount == 0)
            {
                return;
            }
            var first = ids.First();
            Type t = first.GetType();
            IEnumerable<T2> data;
            if (idsCount > 1)
            {
                if (t == typeof(long))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<long>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(string))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<string>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(int))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<int>().ToList(), prevField, returnFields);
                }
                else if (t == typeof(decimal))
                {
                    data = centerTable.GetByIdsWithField(ids.OfType<decimal>().ToList(), prevField, returnFields);
                }
                else
                {
                    data = centerTable.GetByIdsWithField(ids.ToDynamicList(), prevField, returnFields);
                }

            }
            else
            {
                if (t == typeof(long))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (long)first });
                }
                else if (t == typeof(string))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (string)first });
                }
                else if (t == typeof(int))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (int)first });
                }
                else if (t == typeof(decimal))
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = (decimal)first });
                }
                else
                {
                    data = centerTable.GetByWhere($"WHERE {prevField}=@id", new { id = first });
                }

            }

            var ids2 = data.AsQueryable().Where($"{nextField}!=null").Select(nextField).Distinct();
            var ids2Count = ids2.Count();
            IEnumerable<dynamic> data2;
            if (ids2Count > 0)
            {
                Type t2 = ids2.First().GetType();
                if (par == null)
                    par = new DynamicParameters();

                if (t2 == typeof(long))
                {
                    par.Add("@ids", ids2.OfType<long>().ToList());
                }
                else if (t2 == typeof(string))
                {
                    par.Add("@ids", ids2.OfType<string>().ToList());
                }
                else if (t2 == typeof(int))
                {
                    par.Add("@ids", ids2.OfType<int>().ToList());
                }
                else if (t2 == typeof(decimal))
                {
                    par.Add("@ids", ids2.OfType<decimal>().ToList());
                }
                else
                {
                    par.Add("@ids", ids2.ToDynamicList());
                }

                string where;
                if (mapTable.DataBase.Client.DbType == DataBaseType.Postgresql)
                {
                    where = $"WHERE {mapField}=ANY(@ids) {and}";
                }
                else
                {
                    where = $"WHERE {mapField} IN @ids {and}";
                }
                data2 = mapTable.GetByWhereDynamic(where, par, returnFields, orderby);
            }
            else
            {
                data2 = Enumerable.Empty<dynamic>();
            }
            list.ListCenterToMany(field, propertyName, data, prevField, nextField, data2, mapField);

        }

        #endregion

        #region string
        public static string FirstCharToUpper(this string txt)
        {
            return txt.Substring(0, 1).ToUpper() + txt.Substring(1);
        }

        public static string FirstCharToLower(this string txt)
        {
            return txt.Substring(0, 1).ToLower() + txt.Substring(1);
        }

        public static string SetOrderBy(this string str, string key)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "ORDER BY " + key;
            }
            return "ORDER BY " + str;
        }

        #endregion

        #region IEnumerable

        public static IEnumerable<T> ConcatItem<T>(this IEnumerable<T>[] arrayList)
        {
            var list = Enumerable.Empty<T>();
            foreach (var item in arrayList)
            {
                list = list.Concat(item);
            }
            return list;
        }

        #endregion
    }

    #region MyRegion

    [DynamicLinqType]
    public static class ExtUtils
    {
        public static int Int(object val)
        {
            return (int)val;
        }

        public static long Long(object val)
        {
            return (long)val;
        }

        public static decimal Decimal(object val)
        {
            return (decimal)val;
        }

        public static string String(object val)
        {
            if (val == null)
            {
                return "";
            }
            return (string)val;
        }
        public static string Object(object val)
        {
            if (val == null)
            {
                return "";
            }
            return val.ToString();
        }
    }
    #endregion

}
