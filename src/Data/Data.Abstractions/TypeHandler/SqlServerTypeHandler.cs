using Dapper;
using System;
using System.Data;

namespace CRB.TPM.Data.Abstractions.TypeHandler;

public class SqlServerGuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid guid)
    {
        parameter.Value = guid.ToString();
    }

    public override Guid Parse(object value)
    {
        if (value is Guid)
            return (Guid)value;
        else if (value is Nullable<Guid>)
            return Guid.Empty;

        return new Guid((string)value);
    }
}
