using System;
using System.Collections.Generic;
using System.Text;

namespace WYJData
{
    static class SqlStatement
    {
        public static string insert = "INSERT INTO {0} ({1}) VALUES ({2}) ";
        public static string insert1 = "INSERT INTO {0} VALUES ({1}) ";
        public static string delete = "DELETE FROM {0} ";
        public static string update = "UPDATE {0} SET {1} ";
        public static string select = "SELECT {0} FROM [{1}] ";
        public static string create = "CREATE {0} {1} ";
        public static string alert  =  "ALTER {0} {1} ";
        public static string drop   =   "DROP {0} {1} ";
        public static string clearRows = "TRUNCATE TABLE {0} ";

        public static string where = "WHERE {0} {1} {2} ";
        public static string orderby = "ORDER BY {0} ";

        public static string BuiltSql(string a,string b)
        {
            return a + b;
        }
    }

}
