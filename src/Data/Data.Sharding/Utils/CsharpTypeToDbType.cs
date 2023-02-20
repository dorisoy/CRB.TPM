using System;
using System.Collections.Generic;

namespace CRB.TPM.Data.Sharding
{
    /// <summary>
    /// C# 数据类型转Db数据库类型
    /// </summary>
    internal class CsharpTypeToDbType
    {
        /// <summary>
        /// 创建数据库类型
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <param name="columnType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string Create(DataBaseType dbType, Type type, double length = 0, string columnType = null)
        {
            if (!string.IsNullOrEmpty(columnType))
            {
                if (columnType == "jsons")
                {
                    if (dbType == DataBaseType.MySql)
                    {
                        return "longtext";
                    }
                    if (dbType == DataBaseType.Postgresql)
                    {
                        return "text";
                    }
                    if (dbType == DataBaseType.Sqlite)
                    {
                        return "text";
                    }
                    if (dbType == DataBaseType.Oracle)
                    {
                        return "CLOB";
                    }
                    return "nvarchar(max)"; //sqlserver
                }

                if (dbType != DataBaseType.Postgresql)
                {
                    if (columnType == "json" || columnType == "jsonb")
                    {
                        if (dbType == DataBaseType.MySql)
                        {
                            return "json";
                        }
                        else if (dbType == DataBaseType.SqlServer2012 || dbType == DataBaseType.SqlServer2008 || dbType == DataBaseType.SqlServer2005)
                        {
                            return "nvarchar(max)";
                        }
                        else if (dbType == DataBaseType.Oracle)
                        {
                            return "CLOB";
                        }
                        else
                        {
                            return "text";
                        }
                    }
                }

                if (dbType != DataBaseType.MySql)
                {
                    if (columnType == "longtext")
                    {
                        if (dbType == DataBaseType.SqlServer2012 || dbType == DataBaseType.SqlServer2008 || dbType == DataBaseType.SqlServer2005)
                        {
                            return "nvarchar(max)";
                        }
                        else if (dbType == DataBaseType.Oracle)
                        {
                            return "CLOB";
                        }
                        else
                        {
                            return "text";
                        }
                    }
                }
               
                return columnType;
            }
            switch (dbType)
            {
                case DataBaseType.MySql: return CreateMySqlType(type, length);
                case DataBaseType.Sqlite: return CreateSqliteType(type);
                case DataBaseType.SqlServer2005: return CreateSqlServerType(type, length);
                case DataBaseType.SqlServer2008: return CreateSqlServerType(type, length);
                case DataBaseType.SqlServer2012: return CreateSqlServerType(type, length);
                case DataBaseType.Postgresql: return CreatePostgresqlType(type, length);
                case DataBaseType.Oracle: return CreateOracleType(type, length);
            }
            throw new Exception("CsharpTypeToDbType no found");
        }

        private static string UnknownTypeMessage(string t)
        {
            return $"未知类型 {t}, 请将ColumnAttribute ColumnType 设置为如 [Column(columnType:\"jsonb\")] 或其他数据库类型.";
        }

        /// <summary>
        /// SqlServer 类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string CreateSqlServerType(Type type, double length = 0)
        {
            if (type == typeof(Guid) || type == typeof(Nullable<Guid>))
            {
                if (length <= 0)
                {
                    length = 36;
                }
                return $"nvarchar({length})";
            }

            if (type == typeof(string))
            {
                if (length == -2)
                    return "text";
                if (length == -3)
                    return "ntext";
                if (length <= -1)
                    return "nvarchar(max)";
                if (length == 0)
                    length = 50;
                return $"nvarchar({length})";
            }

            if (type == typeof(bool))
            {
                return "bit";
            }

            if (type == typeof(byte) || type == typeof(sbyte))
            {
                return "tinyint";
            }

            if (type == typeof(short) || type == typeof(ushort))
            {
                return "smallint";
            }

            if (type == typeof(int) || type.BaseType == typeof(Enum) || type == typeof(uint) || type == typeof(Nullable<int>))
            {
                return "int";
            }

            if (type == typeof(long) || type == typeof(ulong) || type == typeof(Nullable<long>))
            {
                return "bigint";
            }

            if (type == typeof(float) || type == typeof(Nullable<float>))
            {
                return "real";
            }

            if (type == typeof(double) || type == typeof(Nullable<double>))
            {
                return "float";
            }

            if (type == typeof(decimal) || type == typeof(Nullable<decimal>))
            {
                var len = length.ToString();
                if (len.Contains("."))
                {
                    len = len.Replace(".", ",");
                    return $"decimal({len})";
                }
                if (length <= 0)
                    return "decimal(18,2)";
                return $"decimal({length},0)";
            }

            if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
            {
                if (length >= 0)
                {
                    if (length > 7)
                    {
                        length = 7;
                    }
                    return $"datetime2({length})";
                }
                if (length == -1)
                    return "date";
                if (length == -2)
                    return "timestamp";
                return $"datetime";
            }

            if (type == typeof(DateTimeOffset) || type == typeof(Nullable<DateTimeOffset>))
            {
                if (length >= 0)
                {
                    if (length > 7)
                    {
                        length = 7;
                    }
                }
                return $"datetimeoffset({length})";
            }

#if CORE6
            if (type == typeof(DateOnly))
            {
                if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Date)
                {
                    return "date";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.DateTime)
                {
                    return "datetime2(0)";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Number)
                {
                    return "int";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.String)
                {
                    return "varchar(10)";
                }
                return "int";
            }

            if (type == typeof(TimeOnly))
            {
                if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.TimeSpan)
                {
                    return "time";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Time)
                {
                    return "time";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.DateTime)
                {
                    return "datetime2(7)";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Number)
                {
                    return "bigint";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.String)
                {
                    return "varchar(8)";
                }
                return "bigint";
            }
#endif

            throw new Exception(UnknownTypeMessage(type.Name));

        }

        /// <summary>
        /// MySql 类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string CreateMySqlType(Type type, double length = 0)
        {
            if (type == typeof(Guid) || type == typeof(Nullable<Guid>))
            {
                if (length <= 0)
                {
                    length = 36;
                }
                return $"varchar({length})";
            }

            if (type == typeof(string))
            {
                if (length == -1)
                    return "longtext";
                if (length == -2)
                    return "text";
                if (length == -3)
                    return "mediumtext";
                if (length == -4)
                    return "tinytext";
                if (length <= 0)
                    length = 50;
                return $"varchar({length})";
            }

            if (type == typeof(bool))
            {
                return "bit(1)";
            }

            if (type == typeof(byte) || type == typeof(sbyte))
            {
                return "tinyint(4)";
            }

            if (type == typeof(short) || type == typeof(ushort))
            {
                return "smallint(6)";
            }

            if (type == typeof(int) || type.BaseType == typeof(Enum) || type == typeof(uint) || type == typeof(Nullable<int>))
            {
                return "int(11)";
            }

            if (type == typeof(long) || type == typeof(ulong) || type == typeof(Nullable<ulong>))
            {
                return "bigint(20)";
            }

            if (type == typeof(float) || type == typeof(Nullable<float>))
            {
                return "float";
            }

            if (type == typeof(double) || type == typeof(Nullable<double>))
            {
                return "double";
            }

            if (type == typeof(decimal) || type == typeof(Nullable<decimal>))
            {
                var len = length.ToString();
                if (len.Contains("."))
                {
                    len = len.Replace(".", ",");
                    return $"decimal({len})";
                }
                if (length <= 0)
                    return "decimal(18,2)";
                return $"decimal({length},0)";
            }

            if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
            {
                if (length == 0)
                    return "datetime";

                if (length > 0)
                {
                    if (length > 6)
                    {
                        length = 6;
                    }
                    return $"datetime({length})";
                }

                if (length == -1)
                    return "datetime2";

                if (length == -2)
                    return "date";

                if (length == -3)
                    return "smalldatetime";

                return "timestamp";

            }

            if (type == typeof(DateTimeOffset) || type == typeof(Nullable<DateTimeOffset>))
            {
                return "timestamp";
            }

#if CORE6
            if (type == typeof(DateOnly))
            {
                if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Date)
                {
                    return "date";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.DateTime)
                {
                    return "datetime";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Number)
                {
                    return "int(11)";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.String)
                {
                    return "varchar(10)";
                }
                return "int(11)";
            }

            if (type == typeof(TimeOnly))
            {
                if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.TimeSpan)
                {
                    return "time";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Time)
                {
                    return "time";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.DateTime)
                {
                    return "datetime(6)";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Number)
                {
                    return "bigint(20)";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.String)
                {
                    return "varchar(8)";
                }
                return "bigint(20)";
            }
#endif

            throw new Exception(UnknownTypeMessage(type.Name));

            //if (length >= 0)
            //    return "blob";
            //if (length == -1)
            //    return "tinyblob";
            //if (length == -2)
            //    return "mediumblob";
            //return "longblob";

        }

        /// <summary>
        /// Sqlite 类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string CreateSqliteType(Type type)
        {
            if (type == typeof(Guid) || type == typeof(Nullable<Guid>))
            {
                return "TEXT";
            }

            if (type == typeof(string))
            {
                return "TEXT";
            }

            if (type == typeof(bool))
            {
                return "NUMERIC";
            }

            if (type == typeof(byte) || type == typeof(sbyte))
            {
                return "NUMERIC";
            }

            if (type == typeof(short) || type == typeof(ushort))
            {
                return "NUMERIC";
            }

            if (type == typeof(int) || type.BaseType == typeof(Enum) || type == typeof(uint) || type == typeof(Nullable<int>))
            {
                return "NUMERIC";
            }

            if (type == typeof(long) || type == typeof(ulong) || type == typeof(Nullable<long>))
            {
                return "NUMERIC";
            }

            if (type == typeof(float) || type == typeof(Nullable<float>))
            {
                return "NUMERIC";
            }

            if (type == typeof(double) || type == typeof(Nullable<double>))
            {
                return "NUMERIC";
            }

            if (type == typeof(decimal) || type == typeof(Nullable<decimal>))
            {
                return "NUMERIC";
            }

            if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
            {
                return "DATETIME";
            }

#if CORE6
            if (type == typeof(DateOnly))
            {
                if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Date)
                {
                    return "DATE";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.DateTime)
                {
                    return "DATETIME";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Number)
                {
                    return "NUMERIC";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.String)
                {
                    return "TEXT";
                }
                return "NUMERIC";
            }

            if (type == typeof(TimeOnly))
            {
                if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.TimeSpan)
                {
                    return "DATETIME";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Time)
                {
                    return "TIME";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.DateTime)
                {
                    return "DATETIME";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Number)
                {
                    return "NUMERIC";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.String)
                {
                    return "TEXT";
                }
                return "NUMERIC";
            }
#endif

            throw new Exception(UnknownTypeMessage(type.Name));

            //return "BLOB";
        }

        /// <summary>
        /// Postgresql 类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string CreatePostgresqlType(Type type, double length = 0)
        {
            if (type == typeof(Guid) || type == typeof(Nullable<Guid>))
            {
                if (length <= 0)
                {
                    length = 36;
                }
                return $"varchar({length})";
            }

            if (type == typeof(string))
            {
                if (length == -1)
                    return "text";
                if (length == -10)
                    return "jsonb";
                if (length == -11)
                    return "json";
                if (length == -20)
                    return "geometry";
                if (length > -30 && length < -20)
                {
                    var str = length.ToString();
                    if (str.Contains("."))
                    {
                        var srid = str.Split('.')[1];
                        return $"geometry(geometry,{srid})";
                    }
                    else
                    {
                        return "geometry";
                    }
                }
                if (length <= 0)
                    length = 50;
                return $"varchar({length})";

            }

            if (type == typeof(bool))
            {
                return "bool";
            }

            if (type == typeof(byte) || type == typeof(sbyte))
            {
                return "int2";
            }

            if (type == typeof(short) || type == typeof(ushort))
            {
                return "int2";
            }

            if (type == typeof(int) || type.BaseType == typeof(Enum) || type == typeof(uint) || type == typeof(Nullable<int>))
            {
                return "int4";
            }

            if (type == typeof(long) || type == typeof(ulong) || type == typeof(Nullable<long>))
            {
                return "int8";
            }

            if (type == typeof(float) || type == typeof(Nullable<float>))
            {
                return "float4";
            }

            if (type == typeof(double) || type == typeof(Nullable<double>))
            {
                return "float8";
            }

            if (type == typeof(decimal) || type == typeof(Nullable<decimal>))
            {
                var len = length.ToString();
                if (len.Contains("."))
                {
                    len = len.Replace(".", ",");
                    return $"numeric({len})";
                }
                if (length <= 0)
                    return "numeric(18,2)";
                return $"numeric({length},0)";
            }

            if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
            {
                if (length >= 0)
                {
                    if (length > 6)
                    {
                        length = 6;
                    }
                    return $"timestamp({length})";
                }
                if (length == -1)
                    return "timestamptz";
                return "date";
            }

            if (type == typeof(DateTimeOffset) || type == typeof(Nullable<DateTimeOffset>))
            {
                if (length > 6)
                {
                    length = 6;
                }
                return $"timetz({length})";
            }

            if (type == typeof(TimeSpan) || type == typeof(Nullable<TimeSpan>))
            {
                if (length >= 0)
                    return "time";
                return "interval";
            }

#if CORE6
            if (type == typeof(DateOnly))
            {
                if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Date)
                {
                    return "date";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.DateTime)
                {
                    return "timestamp(0)";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Number)
                {
                    return "int4";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.String)
                {
                    return "varchar(10)";
                }
                return "int4";
            }

            if (type == typeof(TimeOnly))
            {
                if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.TimeSpan)
                {
                    return "time";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Time)
                {
                    return "timestamp(6)";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.DateTime)
                {
                    return "timestamp(6)";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Number)
                {
                    return "int8";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.String)
                {
                    return "varchar(8)";
                }
                return "int8";
            }
#endif

            var typeList = new List<string>
            {
                "Point", "MultiPoint", "LineString",
                "MultiLineString", "Polygon", "MultiPolygon", "GeometryCollection",
                "Feature","FeatureCollection","GeoJSONObject","Geometry"
            };

            if (typeList.Contains(type.Name))
            {
                return "geometry";
            }

            throw new Exception(UnknownTypeMessage(type.Name));

            //return "bytea";

        }

        /// <summary>
        /// Oracle 类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string CreateOracleType(Type type, double length = 0)
        {
            if (type == typeof(Guid) || type == typeof(Nullable<Guid>))
            {
                if (length <= 0)
                {
                    length = 36;
                }
                return $"NVARCHAR2({length})";

            }

            if (type == typeof(string))
            {
                if (length <= -1)
                    return "CLOB";
                if (length <= 0)
                    length = 50;
                return $"NVARCHAR2({length})";

            }

            if (type == typeof(bool))
            {
                return "NUMBER(1)";
            }

            if (type == typeof(byte) || type == typeof(sbyte))
            {
                return "NUMBER(4)";
            }

            if (type == typeof(short) || type == typeof(ushort))
            {
                return "NUMBER(4)";
            }

            if (type == typeof(int) || type.BaseType == typeof(Enum) || type == typeof(uint) || type == typeof(Nullable<int>))
            {
                return "NUMBER(9)";
            }

            if (type == typeof(long) || type == typeof(ulong) || type == typeof(Nullable<long>))
            {
                return "NUMBER(19)";
            }

            if (type == typeof(float) || type == typeof(Nullable<float>))
            {
                return "NUMBER(7,3)";
            }

            if (type == typeof(double) || type == typeof(Nullable<double>))
            {
                return "NUMBER(15,5)";
            }

            if (type == typeof(decimal) || type == typeof(Nullable<decimal>))
            {
                var len = length.ToString();
                if (len.Contains("."))
                {
                    len = len.Replace(".", ",");
                    return $"NUMBER({len})";
                }
                if (length <= 0)
                    return "NUMBER(18,2)";
                return $"NUMBER({length},0)";
            }

            if (type == typeof(DateTime) || type == typeof(Nullable<DateTime>))
            {
                return "TIMESTAMP";
            }

#if CORE6
            if (type == typeof(DateOnly))
            {
                if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Date)
                {
                    return "DATE";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.DateTime)
                {
                    return "TIMESTAMP";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.Number)
                {
                    return "NUMBER(9)";
                }
                else if (ShardingFactory.DateOnlyFormat == DbTypeDateOnly.String)
                {
                    return "VARCHAR2(10)";
                }
                return "NUMBER(9)";
            }

            if (type == typeof(TimeOnly))
            {
                if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.TimeSpan)
                {
                    return "TIME";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Time)
                {
                    return "TIME";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.DateTime)
                {
                    return "TIMESTAMP";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.Number)
                {
                    return "NUMBER(19)";
                }
                else if (ShardingFactory.TimeOnlyFormat == DbTypeTimeOnly.String)
                {
                    return "VARCHAR2(8)";
                }
                return "NUMBER(19)";
            }
#endif

            throw new Exception(UnknownTypeMessage(type.Name));

            //return "BLOB";
        }

    }
}
