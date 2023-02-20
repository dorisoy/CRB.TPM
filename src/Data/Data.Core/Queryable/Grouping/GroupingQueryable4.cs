using System;
using System.Linq.Expressions;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Data.Abstractions.Logger;
using CRB.TPM.Data.Abstractions.Pagination;
using CRB.TPM.Data.Abstractions.Queryable.Grouping;
using CRB.TPM.Data.Core.SqlBuilder;

namespace CRB.TPM.Data.Core.Queryable.Grouping;

internal class GroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4> : GroupingQueryable, IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4>
    where TEntity : IEntity, new()
    where TEntity2 : IEntity, new()
    where TEntity3 : IEntity, new()
    where TEntity4 : IEntity, new()
{
    public GroupingQueryable(QueryableSqlBuilder sqlBuilder, DTPger logger, Expression expression) : base(sqlBuilder, logger, expression)
    {
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4> Having(Expression<Func<IGrouping<TKey, TEntity, TEntity2, TEntity3, TEntity4>, bool>> expression)
    {
        _queryBody.SetHaving(expression);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4> Having(string havingSql)
    {
        _queryBody.SetHaving(havingSql);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderBy(string field)
    {
        _queryBody.SetSort(field, SortType.Asc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderByDescending(string field)
    {
        _queryBody.SetSort(field, SortType.Desc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderBy<TResult>(Expression<Func<IGrouping<TKey, TEntity, TEntity2, TEntity3, TEntity4>, TResult>> expression)
    {
        _queryBody.SetSort(expression, SortType.Asc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4> OrderByDescending<TResult>(Expression<Func<IGrouping<TKey, TEntity, TEntity2, TEntity3, TEntity4>, TResult>> expression)
    {
        _queryBody.SetSort(expression, SortType.Desc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity, TEntity2, TEntity3, TEntity4> Select<TResult>(Expression<Func<IGrouping<TKey, TEntity, TEntity2, TEntity3, TEntity4>, TResult>> expression)
    {
        _queryBody.SetSelect(expression);
        return this;
    }
}