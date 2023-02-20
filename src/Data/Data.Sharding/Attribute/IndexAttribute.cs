using System;

namespace CRB.TPM.Data.Sharding
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class IndexAttribute : Attribute
    {
        public string Name;

        public string Columns;

        public IndexType Indextype;

        public IndexAttribute(string name, string columns, IndexType indexType)
        {
            Name = name;
            Columns = columns;
            Indextype = indexType;
        }
    }
}
