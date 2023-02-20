namespace CRB.TPM.Data.Sharding
{
    public class IndexEntity
    {
        public string Name { get; set; }

        public string Columns { get; set; }

        public IndexType Type { get; set; }

        public string StringType
        {
            get
            {
                switch (Type)
                {
                    case IndexType.PrimaryKey: return "IndexType.PrimaryKey";
                    case IndexType.Unique: return "IndexType.Unique";
                    case IndexType.Normal: return "IndexType.Normal";
                    case IndexType.FullText: return "IndexType.FullText";
                    case IndexType.Spatial: return "IndexType.Spatial";
                    case IndexType.Gist: return "IndexType.Gist";
                    case IndexType.JsonbGin: return "IndexType.JsonbGin";
                    case IndexType.JsonbGinPath: return "IndexType.JsonbGinPath";
                    case IndexType.JsonBtree: return "IndexType.JsonBtree";
                }
                return null;
            }
        }
    }
}
