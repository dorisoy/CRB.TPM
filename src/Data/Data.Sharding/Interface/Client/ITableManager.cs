using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRB.TPM.Data.Sharding
{
    public abstract class ITableManager
    {
        public ITableManager(string name, IDatabase database)
        {
            Name = name;
            DataBase = database;
        }


        #region prototype

        public string Name { get; }

        public IDatabase DataBase { get; }

        #endregion

        public abstract void CreateIndex(string name, string columns, IndexType indexType);

        public abstract void DropIndex(string name);

        public abstract void AddColumn(string name, Type t, double length = 0, string comment = null, string columnType = null);

        public abstract void DropColumn(string name);

        public abstract void ModifyColumn(string name, Type t, double length = 0, string comment = null, string columnType = null);

        public abstract List<IndexEntity> GetIndexEntityList();

        public abstract List<ColumnEntity> GetColumnEntityList(TableEntity tb = null, bool firstCharToUpper = false);

    }
}
