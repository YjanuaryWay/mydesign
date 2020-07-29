using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Threading;

namespace WYJData
{
    public class DbContext
    {
        private readonly SqlCommand SqlCommand;

        public DbContext(string connectionString)
        {
            //bool succeeded = false;
            //int totalNumberOfTimesToTry = 4;
            //int retryIntervalSeconds = 10;

            //for (int tries = 1;
            //  tries <= totalNumberOfTimesToTry;
            //  tries++)
            //{
            //    try
            //    {
            //        if (tries > 1)
            //        {
            //            Console.WriteLine
            //              ("Transient error encountered. Will begin attempt number {0} of {1} max...",
            //              tries, totalNumberOfTimesToTry
            //              );
            //            Thread.Sleep(1000 * retryIntervalSeconds);
            //            retryIntervalSeconds = Convert.ToInt32
            //              (retryIntervalSeconds * 1.5);
            //        }
            //        succeeded = true;
            //        break;
            //    }
            //    catch (SqlException sqlExc)
            //    {
            //        if (TransientErrorNumbers.Contains(sqlExc.Number))
            //        {
            //            Console.WriteLine("{0}: transient occurred.", sqlExc.Number);
            //            continue;
            //        }
            //        else
            //        {
            //            Console.WriteLine(sqlExc);
            //            succeeded = false;
            //            break;
            //        }
            //    }
            //    catch (TestSqlException sqlExc)
            //    {
            //        if (TransientErrorNumbers.Contains(sqlExc.Number))
            //        {
            //            Console.WriteLine("{0}: transient occurred. (TESTING.)", sqlExc.Number);
            //            continue;
            //        }
            //        else
            //        {
            //            Console.WriteLine(sqlExc);
            //            succeeded = false;
            //            break;
            //        }
            //    }
            //    catch (Exception Exc)
            //    {
            //        Console.WriteLine(Exc);
            //        succeeded = false;
            //        break;
            //    }
            //}

        }


        public void Update<T>(T MODEL)
        {
            SqlCommand.CommandText = $@"
                  UPDATE {typeof(T).Name} SET 

            ";
            var reader = SqlCommand.ExecuteReader();
        }


        public void Save<T>(T MODEL)
        {


        }


    }

    [Serializable]
    internal class TestSqlException : Exception
    {
        internal TestSqlException(int testErrorNumber)
        { Number = testErrorNumber; }

        internal int Number
        { get; set; }
    }
}
