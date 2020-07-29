using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WYJCore;
using WYJCore.Web;
using WYJWeb.Models;

namespace WYJWeb.Apis
{
    public class NotificationController : ControllerBase<Notification>
    {


        public Response GetList(string key,int pageIndex, int pageSize)
        {
            return Respond(() =>
            {
                var list = new List<Notification>();
                var db = new DB();
                db.sqlCommand.CommandText = $"SELECT TOP({pageSize}) * FROM Notification";
                var reader = db.sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var model = new Notification();
                    var propertys = typeof(Notification).GetProperties();
                    foreach (var property in propertys)
                    {
                        try
                        {
                            var data = reader[property.Name];
                            if (property.PropertyType == typeof(int))
                                property.SetValue(model, int.Parse(data.ToString()));
                            else
                                property.SetValue(model, data);
                        }
                        catch
                        {
                        }
                        finally
                        {
                        }
                    }
                    list.Add(model);
                }
                reader.Close();
                db.sqlCommand.Dispose();
                return new
                {
                    pageSize,
                    Data = list,
                    list.Count
                };
            });
        }

    }
}
