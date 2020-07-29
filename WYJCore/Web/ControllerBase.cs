using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace WYJCore.Web
{
    public abstract class ControllerBase<TModel> where TModel : class, new()
    {
        public ControllerBase()
        {
            Db = new DbContext<TModel>("WYJ");
            Db.db.CodeFirst.InitTables<TModel>();
        }
        protected DbContext<TModel> Db;
        public SimpleClient<TModel> CurrentDb => new SimpleClient<TModel>(Db.db);

        public HttpContext CurrentHttpContext { get; set; }
        public virtual List<TModel> GetList() => CurrentDb.GetList();
        public virtual TModel GetModel(Expression<Func<TModel, bool>> expression) => CurrentDb.GetSingle(expression);

        public virtual Response Save(string form)
        {
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<TModel>(form);
            BeforeSave(model);
            Db.Save(model).ExecuteReturnEntity();
            AfterSave(model);
            return new Response { IsSuccess = true, Data = model };
        }

        public virtual Response Delete(Expression<Func<TModel, bool>> expression)
        {
            return Respond(() =>
            {
                CurrentDb.Delete(expression);
            });
        }
        public virtual Response DeleteById(int id)
        {
            return Respond(() =>
            {
                CurrentDb.DeleteById(id);
            });
        }

        internal virtual void AfterSave(TModel model)
        {
        }

        internal virtual void BeforeSave(TModel model) { }

        protected Response Respond(Func<object> func, string msg = null)
        {
            return new Response
            {
                Data = func(),
                IsSuccess = true,
                Msg = msg
            };
        }
        protected Response Respond(Action action, string msg = null)
        {
            action();
            return new Response
            {
                IsSuccess = true,
                Msg = msg
            };
        }
    }
}
