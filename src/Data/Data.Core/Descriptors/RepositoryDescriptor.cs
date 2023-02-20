using System;
using CRB.TPM.Data.Abstractions.Descriptors;

namespace CRB.TPM.Data.Core.Descriptors;

public class RepositoryDescriptor : IRepositoryDescriptor
{
    public Type EntityType { get; }

    public Type InterfaceType { get; }

    public Type ImplementType { get; }

    public RepositoryDescriptor(Type entityType, Type interfaceType, Type implementType)
    {
        EntityType = entityType;
        InterfaceType = interfaceType;
        ImplementType = implementType;
    }

}