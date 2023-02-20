using System;

namespace CRB.TPM.Data.Sharding
{
    public class ColumnEntity
    {
        public string Name { get; set; }

        public Type CsType { get; set; }

        public double Length { get; set; }

        public string DbLength { get; set; }

        public string Comment { get; set; }

        public string DbType { get; set; }

        public string CsStringType { get; set; }
    }
}
