using System;
using System.Linq.Expressions;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Data.Abstractions.Logger;
using CRB.TPM.Data.Abstractions.Pagination;
using CRB.TPM.Data.Abstractions.Queryable.Grouping;
using CRB.TPM.Data.Core.SqlBuilder;

namespace CRB.TPM.Data.Core.Queryable.Grouping;

internal class GroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> : GroupingQueryable, IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10>
    where TEntity : IEntity, new()
    where TEntity2 : IEntity, new()
    where TEntity3 : IEntity, new()
    where TEntity4 : IEntity, new()
    where TEntity5 : IEntity, new()
    where TEntity6 : IEntity, new()
    where TEntity7 : IEntity, new()
    where TEntity8 : IEntity, new()
    where TEntity9 : IEntity, new()
    where TEntity10 : IEntity, new()
{
    public GroupingQueryable(QueryableSqlBuilder sqlBuilder, DTPger logger, Expression expression) : base(sqlBuilder, logger, expression)
    {
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> Having(Expression<Func<IGrouping<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10>, bool>> expression)
    {
        _queryBody.SetHaving(expression);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> Having(string havingSql)
    {
        _queryBody.SetHaving(havingSql);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> OrderBy(string field)
    {
        _queryBody.SetSort(field, SortType.Asc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> OrderByDescending(string field)
    {
        _queryBody.SetSort(field, SortType.Desc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> OrderBy<TResult>(Expression<Func<IGrouping<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10>, TResult>> expression)
    {
        _queryBody.SetSort(expression, SortType.Asc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> OrderByDescending<TResult>(Expression<Func<IGrouping<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10>, TResult>> expression)
    {
        _queryBody.SetSort(expression, SortType.Desc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10> Select<TResult>(Expression<Func<IGrouping<TKey, TEntity, TEntity2, TEntity3, TEntity4, TEntity5, TEntity6, TEntity7, TEntity8, TEntity9, TEntity10>, TResult>> expression)
    {
        _queryBody.SetSelect(expression);
        return this;
    }
}