using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CRB.TPM.Data.Abstractions.Pagination;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Data.Abstractions.Query;

/// <summary>
/// 查询对象
/// </summary>
public abstract class QueryDto
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public QueryPagingDto Page { get; set; } = new();

    private Paging _paging;

    /// <summary>
    /// 转换成Paging分页类
    /// </summary>
    [SwaggerIgnore]
    public Paging Paging
    {
        get
        {
            if (_paging == null)
            {
                _paging = new Paging(Page.Index, Page.Size);

                if (Page.Sort != null && Page.Sort.Any())
                {
                    foreach (var sort in Page.Sort)
                    {
                        _paging.OrderBy.Add(new Sort(sort.Field, sort.Type));
                    }
                }
            }

            return _paging;
        }
    }

}

