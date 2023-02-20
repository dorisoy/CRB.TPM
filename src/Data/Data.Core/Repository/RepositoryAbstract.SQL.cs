using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Adapter;
using CRB.TPM.Data.Abstractions.ReadWriteSeparation;
using CRB.TPM.Utils.Models;
using Dapper;

namespace CRB.TPM.Data.Core.Repository;

public abstract partial class RepositoryAbstract<TEntity>
{
    /// <summary>
    /// 审计查询最近一周访问量
    /// </summary>
    /// <param name="param"></param>
    /// <param name="uow"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public Task<IEnumerable<ChartDataResultModel>> QueryLatestWeekPvAsync(object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
    {
        string QueryLatestWeekPv = "";
        switch (_adapter.Provider)
        {
            case DbProvider.SqlServer:
                QueryLatestWeekPv = @"SELECT CONVERT(VARCHAR(100), ExecutionTime, 23) AS [Key],
                               COUNT(0) AS [Value]
                        FROM dbo.{0}
                        WHERE ExecutionTime > DATEADD(DAY, -6, GETDATE())
                        GROUP BY CONVERT(VARCHAR(100), ExecutionTime, 23)
                        ORDER BY [Key];";
                break;
            case DbProvider.MySql:
                QueryLatestWeekPv = @"SELECT
	                        DATE_FORMAT(ExecutionTime, '%Y-%m-%d' ) `Key`,
	                        COUNT(0) `Value`
                        FROM
	                        {0} 
                        WHERE
	                        ExecutionTime > DATE_FORMAT( DATE_ADD(NOW(), interval -6 day), '%Y-%m-%d' )
                        GROUP BY
	                        `Key` ORDER BY `Key`";
                break;
            case DbProvider.Sqlite:
            case DbProvider.PostgreSQL:
            case DbProvider.Oracle:
                break;
        }

        var sql = string.Format(QueryLatestWeekPv, EntityDescriptor.TableName);
        var conn = OpenConn(uow, out IDbTransaction tran, NodeType.Slave);
        return conn.QueryAsync<ChartDataResultModel>(sql, param, tran, commandType: commandType);
    }
}