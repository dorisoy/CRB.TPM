using System;
using System.Linq.Expressions;
using CRB.TPM.Data.Abstractions.Entities;
using CRB.TPM.Data.Abstractions.Logger;
using CRB.TPM.Data.Abstractions.Pagination;
using CRB.TPM.Data.Abstractions.Queryable.Grouping;
using CRB.TPM.Data.Core.SqlBuilder;

namespace CRB.TPM.Data.Core.Queryable.Grouping;

internal class GroupingQueryable<TKey, TEntity> : GroupingQueryable, IGroupingQueryable<TKey, TEntity> where TEntity : IEntity, new()
{
    public GroupingQueryable(QueryableSqlBuilder sqlBuilder, DTPger logger, Expression expression) : base(sqlBuilder, logger, expression)
    {
    }

    public IGroupingQueryable<TKey, TEntity> Having(Expression<Func<IGrouping<TKey, TEntity>, bool>> expression)
    {
        _queryBody.SetHaving(expression);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity> Having(string havingSql)
    {
        _queryBody.SetHaving(havingSql);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity> OrderBy(string field)
    {
        _queryBody.SetSort(field, SortType.Asc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity> OrderByDescending(string field)
    {
        _queryBody.SetSort(field, SortType.Desc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity> OrderBy<TResult>(Expression<Func<IGrouping<TKey, TEntity>, TResult>> expression)
    {
        _queryBody.SetSort(expression, SortType.Asc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity> OrderByDescending<TResult>(Expression<Func<IGrouping<TKey, TEntity>, TResult>> expression)
    {
        _queryBody.SetSort(expression, SortType.Desc);
        return this;
    }

    public IGroupingQueryable<TKey, TEntity> Select<TResult>(Expression<Func<IGrouping<TKey, TEntity>, TResult>> expression)
    {
        _queryBody.SetSelect(expression);
        return this;
    }
}