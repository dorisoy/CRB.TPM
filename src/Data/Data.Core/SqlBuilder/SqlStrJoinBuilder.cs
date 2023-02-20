using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Pagination;
using CRB.TPM.Data.Abstractions.Query;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace CRB.TPM.Data.Core.SqlBuilder
{
    public static class SqlStrJoinBuilderExt
    {
        public async static Task<PagingQueryResultModel<T>> ToPaginationResult<T>(this SqlStrJoinBuilder build, Task<IEnumerable<T>> query, QueryDto dto)
        {
            var rows = await query;
            var resultBody = new PagingQueryResultBody<T>(rows?.ToList(), dto.Paging);
            return new PagingQueryResultModel<T>().Success(resultBody);
        }
    }

    public class SqlStrJoinBuilder
    {
        public string SelectStr { get; set; } = string.Empty;
        public string SelectGroupByStr { get; set; } = string.Empty;
        public string FromStr { get; set; } = string.Empty;
        public string WhereStr { get; set; } = string.Empty;
        public string OrderStr { get; set; } = string.Empty;
        public string PageStr { get; set; } = string.Empty;
        public string GroupByStr { get; set; } = string.Empty;

        private StringBuilder CompleteSqlBuilder = new StringBuilder();

        private string JoinSelectFiled(string filedStr, int? top = null)
        {
            return "SELECT " + (top != null ? $" TOP {top} " : "") + filedStr + " ";
        }

        public SqlStrJoinBuilder JoinSelect(int? top = null)
        {
            CompleteSqlBuilder.Append(JoinSelectFiled(SelectStr, top));
            return this;
        }

        public SqlStrJoinBuilder JoinGroupBySelect(int? top = null)
        {
            CompleteSqlBuilder.Append(JoinSelectFiled(SelectGroupByStr, top));
            return this;
        }

        public SqlStrJoinBuilder JoinSelectTotalCount()
        {
            CompleteSqlBuilder.Append("SELECT COUNT(1) ");
            return this;
        }

        public SqlStrJoinBuilder JoinFrom()
        {
            CompleteSqlBuilder.Append(FromStr + " ");
            return this;
        }

        public SqlStrJoinBuilder JoinWhere()
        {
            CompleteSqlBuilder.Append(WhereStr);
            return this;
        }

        public SqlStrJoinBuilder JoinOrder()
        {
            CompleteSqlBuilder.Append(" " + OrderStr + " ");
            return this;
        }

        public SqlStrJoinBuilder GroupByOrder()
        {
            CompleteSqlBuilder.Append(" " + OrderStr + " ");
            return this;
        }

        public SqlStrJoinBuilder JoinPage(int skip, int take)
        {
            if (string.IsNullOrWhiteSpace(PageStr))
            {
                PageStr = $"offset {skip} rows fetch next {take} rows only";
            }
            CompleteSqlBuilder.Append(" " + PageStr + " ");
            return this;
        }

        public string Build()
        {
            var res = CompleteSqlBuilder.ToString();
            CompleteSqlBuilder = new StringBuilder();
            return res;
        }

        public string BuildQuery()
        {
            return JoinSelect().JoinFrom().JoinWhere().JoinOrder().Build();
        }

        public string BuildQueryPage(QueryDto dto)
        {
            return BuildQueryPage(dto.Paging);
        }

        public string BuildQueryPage(Paging paging)
        {
            return BuildQueryPage(paging.Skip, paging.Size);
        }

        public string BuildQueryPage(int take, int skip)
        {
            return JoinSelect().JoinFrom().JoinWhere().JoinOrder().JoinPage(take, skip).Build();
        }
      
        public string BuildTotalCount()
        {
            return JoinSelectTotalCount().JoinFrom().JoinWhere().Build();
        }

        public static SqlStrJoinBuilder CreateInstance()
        {
            var res = new SqlStrJoinBuilder();
            return res;
        }

   
    }
}
