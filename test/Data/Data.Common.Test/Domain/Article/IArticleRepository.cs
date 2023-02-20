using CRB.TPM.Data.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Data.Common.Test.Domain.Article
{
    public interface IArticleRepository : IRepository<ArticleEntity>
    {
        Task<IList<ArticleEntity>> GetArticles(DateTime date);
    }
}
