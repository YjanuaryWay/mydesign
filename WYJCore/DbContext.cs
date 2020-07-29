using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace WYJCore
{
    public class DbContext<TModel> where TModel : class, new()
    {
        public SqlSugarClient db;
        public DbContext(string config)
        {
            db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = $"Data Source=47.92.217.144;database={config};uid=sa;pwd=~Zhiqing12345678",
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
        }

        public virtual List<TModel> GetList() => db.Queryable<TModel>().ToList();
        public virtual ISugarQueryable<TModel> GetModel() => db.Queryable<TModel>();
        public virtual IDeleteable<TModel> Delete(TModel model) => db.Deleteable(model);
        public virtual IUpdateable<TModel> Update(TModel model) => db.Updateable(model);
        public virtual ISaveable<TModel> Save(TModel model) => db.Saveable(model);


    }
}
