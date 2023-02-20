using CRB.TPM.Data.Abstractions.Query;
using CRB.TPM.Mod.PS.Core.Application.Post.Dto;
using CRB.TPM.Mod.PS.Core.Domain.Post;
using System;
using System.Threading.Tasks;

namespace CRB.TPM.Mod.PS.Core.Application.Post
{
    public interface IPostService
    {
        Task<IResultModel> Add(PostAddDto dto);
        Task<IResultModel> Delete(Guid id);
        Task<IResultModel> Edit(Guid id);
        Task<PagingQueryResultModel<PostEntity>> Query(PostQueryDto dto);
        Task<IResultModel> Select();
        Task<IResultModel> Update(PostUpdateDto model);
    }
}