using CRB.TPM.Config.Abstractions;
using CRB.TPM.Mod.Admin.Core.Domain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRB.TPM.Utils.Annotations;

namespace CRB.TPM.Mod.Admin.Core.Infrastructure;

public class AdminConfigStorageProvider : IConfigStorageProvider
{
    private readonly IConfigRepository _repository;

    public AdminConfigStorageProvider(IConfigRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> GetJson(ConfigType type, string code)
    {
        if (code.IsNull())
            return string.Empty;

        var entity = await _repository.Get(type, code);
        return entity == null ? string.Empty : entity.Value;
    }

    public async Task<bool> SaveJson(ConfigType type, string code, string json)
    {
        if (code.IsNull())
            throw new NullReferenceException("编码不能为空");

        var entity = await _repository.Get(type, code) ?? new ConfigEntity();

        entity.Type = type;
        entity.Code = code;
        entity.Value = json;

        if (entity.Id != Guid.Empty)
            return await _repository.Update(entity);

        return await _repository.Add(entity);
    }
}
