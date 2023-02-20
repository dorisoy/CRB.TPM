using CRB.TPM.Data.Abstractions.ReadWriteSeparation;
using CRB.TPM.Data.Abstractions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Core.Extensions;

public static class DbContextExtensions
{
    /// <summary>
    /// 数据库操作扩展类，批量插入
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="dataTable"></param>
    /// <param name="timeout"></param>
    public static void SqlBulkCopy(this IDbContext dbContext, DataTable dataTable, int timeout = 3600)
    {
        //这个获取连接字符串
        //var connStr = dbContext.Options.ConnectionString;
        //这个获取当前连接
        var conn = dbContext.SchemaProvider.CurrentConn();
        if (conn != null)
            dbContext.Adapter.SqlBulkCopy(conn, dataTable, timeout);
    }
}

