using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WYJData
{
    public abstract class Database:IDisposable
    {

        public readonly SqlCommand sqlCommand;
        static public List<int> TransientErrorNumbers = new List<int> { 4060, 40197, 40501, 40613, 49918, 49919, 49920, 11001 };

        public Database(string connectionString = null)
        {
            connectionString ??= "Data Source=47.92.217.144;database=WYJ;uid=sa;pwd=****";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            sqlCommand = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.Text,
            };
        }




        public T GetModelById<T>(object id) where T :new()
        {
            T model = new T();
            sqlCommand.CommandText = SqlStatement.BuiltSql(string.Format(SqlStatement.select, "*", typeof(T).Name), string.Format(SqlStatement.where, "Id","=",id));
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                var ps = typeof(T).GetProperties();
                foreach (var property in ps)
                {
                    try
                    {
                        var data = reader[property.Name].ToString();
                        if(property.PropertyType==typeof(int))
                        property.SetValue(model,int.Parse(data));
                        else
                            property.SetValue(model,data);
                    }
                    catch
                    {

                    }

                }
            }
            reader.Close();
            return model;
        }

        public void Dispose()
        {
            sqlCommand.Dispose();
        }
    }
}
