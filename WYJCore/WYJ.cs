using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WYJCore.Extension;
using WYJCore.Web;

namespace WYJCore
{
    public static class WYJ
    {
        static WYJ()
        {
            var file = File.ReadAllText(Environment.CurrentDirectory + "/menu.json");
            Menus = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Menu>>(file);
        }

        public static void Init()
        {
            var types = new Classes(x => x.Namespace.Contains("Models")).Types;
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = $"Data Source=47.92.217.144;database=WYJ;uid=sa;pwd=~Zhiqing12345678",
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });
            db.CodeFirst.InitTables(types);
        }

        public static List<Menu> Menus;

        public static Type[] Services = new Classes(x => x.Name.EndsWith("Controller")).Types;

        public static Type GetServiceType(string name)
        {
            return Services.First(x => x.Name.EqualTo(name));
        }

    }
}