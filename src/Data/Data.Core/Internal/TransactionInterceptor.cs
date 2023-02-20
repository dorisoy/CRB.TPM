using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using CRB.TPM.Data.Abstractions;
using CRB.TPM.Data.Abstractions.Annotations;

namespace CRB.TPM.Data.Core.Internal;

/// <summary>
/// 特性事务拦截器，用于提供允许成员侦听的DynamicProxy扩展
/// </summary>
internal class TransactionInterceptor : IInterceptor
{
    private readonly IDbContext _context;
    private readonly IRepositoryManager _manager;

    public TransactionInterceptor(IDbContext context, IRepositoryManager manager)
    {
        _context = context;
        _manager = manager;
    }

    /// <summary>
    /// 拦截
    /// </summary>
    /// <param name="invocation">封装代理方法的调用</param>
    public void Intercept(IInvocation invocation)
    {
        var transactionAttribute = invocation.MethodInvocationTarget.GetCustomAttribute<TransactionAttribute>();
        if (transactionAttribute == null)
        {
            //调用业务方法
            invocation.Proceed();
        }
        else
        {
            InterceptAsync(invocation, transactionAttribute);
        }
    }

    /// <summary>
    /// 异步拦截
    /// </summary>
    /// <param name="invocation">封装代理方法的调用</param>
    /// <param name="attribute"></param>
    private async void InterceptAsync(IInvocation invocation, TransactionAttribute attribute)
    {
        //创建工作单元
        using var uow = _context.NewUnitOfWork(attribute.IsolationLevel);
        try
        {
            //使仓储绑定工作单元
            foreach (var repository in _manager.Repositories)
            {
                repository.BindingUow(uow);
            }

            //调用业务方法
            invocation.Proceed();

            dynamic result = invocation.ReturnValue;
            if (result is Task<IResultModel>)
            {
                var rModel = await result;
                if (rModel.Successful)
                {
                    uow.SaveChanges();
                }
                else
                {
                    uow.Rollback();
                }
            }
            else
            {
                uow.SaveChanges();
            }
        }
        catch
        {
            uow.Rollback();

            throw;
        }
    }
}