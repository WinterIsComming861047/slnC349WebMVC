using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjC349WebMVC
{
    public class MySQLHelper
    {
        public static List<Dictionary<string, object>> ReadWithParam(MySqlConnection conn, MySqlCommand mySqlCommand)
        {
            List<Dictionary<string, object>> myList = new List<Dictionary<string, object>>();
            conn.Open();
            try
            {
                MySqlDataReader myData = mySqlCommand.ExecuteReader();
                if (!myData.HasRows)
                {
                    // 如果沒有資料,顯示沒有資料的訊息
                    Console.WriteLine("No data.");
                }

                // 讀取資料並且顯示出來
                while (myData.Read())
                {
                    Dictionary<string, object> myDic = new Dictionary<string, object>();
                    for (int i = 0; i < myData.FieldCount; i++)
                    {
                        myDic.Add(myData.GetName(i), myData.GetValue(i));
                    }
                    myList.Add(myDic);
                }
                myData.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " : " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return myList;
        }
    }
}