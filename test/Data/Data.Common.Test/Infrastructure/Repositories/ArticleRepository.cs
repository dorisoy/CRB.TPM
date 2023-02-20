using Data.Common.Test.Domain.Article;
using CRB.TPM.Data.Core.Repository;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Data.Common.Test.Infrastructure.Repositories
{
    public class ArticleRepository : RepositoryAbstract<ArticleEntity>, IArticleRepository
    {
        public async Task<IList<ArticleEntity>> GetArticles(DateTime date)
        {
            var tableName = GetShardingTableName(date);
            var articles = Find(tableName);
            return await articles.ToList();
        }
    }
}
