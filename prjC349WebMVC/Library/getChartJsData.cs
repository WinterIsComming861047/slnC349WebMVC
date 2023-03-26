using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace prjC349WebMVC
{

    public class getChartJsData
    {
        string tbMonth;
        public getChartJsData(string tbMonth)
        {
            this.tbMonth = tbMonth;
        }

        public string AddDateInWhereClause()
        {
            string SQL_Where_Clause = "WHERE ";
            if (tbMonth != "")
            {
                SQL_Where_Clause = SQL_Where_Clause + "MONTH(tdate) = @month AND YEAR(tdate) = @year ";
            }
            if (SQL_Where_Clause != "WHERE ") SQL_Where_Clause = SQL_Where_Clause + "AND ";

            return SQL_Where_Clause;
        }
        private List<List<Dictionary<string, object>>> AddDateParamAndReadSQL(Dictionary<string, object> dataParamDic)
        {
            List<string> SQLs = (List<string>)dataParamDic["SQLs"];
            MySqlConnection conn = (MySqlConnection)dataParamDic["conn"];
            List<List<Dictionary<string, object>>> myLists = new List<List<Dictionary<string, object>>>();
            foreach (var sql in SQLs)
            {
                MySqlCommand mySqlCommand = new MySqlCommand(sql, conn);
                if (tbMonth != "")
                {
                    System.Diagnostics.Debug.WriteLine($"Month = {DateTime.Parse(tbMonth).Month}");
                    System.Diagnostics.Debug.WriteLine($"Year = {DateTime.Parse(tbMonth).Year}");
                    mySqlCommand.Parameters.AddWithValue("@month", DateTime.Parse(tbMonth).Month);
                    mySqlCommand.Parameters.AddWithValue("@year", DateTime.Parse(tbMonth).Year);
                }
                List<Dictionary<string, object>> myList = MySQLHelper.ReadWithParam(conn, mySqlCommand);
                myLists.Add(myList);
            }
            return myLists;
        }

        public List<ChartJsDataSource> ByCategory()
        {
            MySqlConnection conn = new MySqlConnection(WebConfigurationManager.ConnectionStrings["c349ConnectionString"].ConnectionString);

            List<string> SQLs = new List<string>();

            string SQL_Prefix = $"select tdate,count(*) from c34.remote_visua_inspection ";
            string SQL_Where_Clause = AddDateInWhereClause();

            string SQL_Postfix = $"category='鋼板'  group by tdate";
            string SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;

            SQLs.Add(SQL);

            SQL_Postfix = $"category='線材'  group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='熱軋'  group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='冷軋'  group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);


            Dictionary<string, object> dataParamDic = new Dictionary<string, object>();
            //dataParamDic.Add("myLists", myLists);
            dataParamDic.Add("SQLs", SQLs);
            dataParamDic.Add("conn", conn);
            List<List<Dictionary<string, object>>> myLists = AddDateParamAndReadSQL(dataParamDic);

            dataParamDic = new Dictionary<string, object>();
            dataParamDic.Add("SQLs", SQLs);
            dataParamDic.Add("myLists", myLists);

            List<ChartJsDataSource> finalList = CombineValueByDate(dataParamDic);
            return finalList;
        }
        public List<ChartJsDataSource> ByPort()
        {
            MySqlConnection conn = new MySqlConnection(WebConfigurationManager.ConnectionStrings["c349ConnectionString"].ConnectionString);

            string SQL_Prefix = $"select tdate,count(*) from c34.remote_visua_inspection ";
            string SQL_Where_Clause = AddDateInWhereClause();


            List<string> SQLs = new List<string>();
            string SQL_Postfix = $"category='鋼板' and horbor like'C%' group by tdate";
            string SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='鋼板' and horbor not like'C%' group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='線材' and horbor like'C%' group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='線材' and horbor not like'C%' group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='熱軋' and horbor like'C%' group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='熱軋' and horbor not like'C%' group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='冷軋' and horbor like'C%' group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='冷軋' and horbor not like'C%' group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            List<List<Dictionary<string, object>>> myLists = new List<List<Dictionary<string, object>>>();

            Dictionary<string, object> dataParamDic = new Dictionary<string, object>();
            dataParamDic.Add("myLists", myLists);
            dataParamDic.Add("SQLs", SQLs);
            dataParamDic.Add("conn", conn);
            myLists = AddDateParamAndReadSQL(dataParamDic);

            dataParamDic = new Dictionary<string, object>();
            dataParamDic.Add("SQLs", SQLs);
            dataParamDic.Add("myLists", myLists);

            //List<ChartDataSource> finalList = CombineValueByDate(dataParamDic);
            List<ChartJsDataSource> finalList = CombineValueByDate(dataParamDic);

            return finalList;

        }
        public List<ChartJsDataSource> DomesticSales()
        {
            MySqlConnection conn = new MySqlConnection(WebConfigurationManager.ConnectionStrings["c349ConnectionString"].ConnectionString);

            string SQL_Prefix = $"select tdate,count(*) from c34.remote_visua_inspection ";
            string SQL_Where_Clause = AddDateInWhereClause();


            List<string> SQLs = new List<string>();
            string SQL_Postfix = $"category='鋼板' and client !='' group by tdate";
            string SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='線材' and client !='' group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='熱軋' and client !='' group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Postfix = $"category='冷軋' and client !='' group by tdate";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);


            List<List<Dictionary<string, object>>> myLists = new List<List<Dictionary<string, object>>>();

            Dictionary<string, object> dataParamDic = new Dictionary<string, object>();
            dataParamDic.Add("myLists", myLists);
            dataParamDic.Add("SQLs", SQLs);
            dataParamDic.Add("conn", conn);
            myLists = AddDateParamAndReadSQL(dataParamDic);

            dataParamDic = new Dictionary<string, object>();
            dataParamDic.Add("SQLs", SQLs);
            dataParamDic.Add("myLists", myLists);

            List<ChartJsDataSource> finalList = CombineValueByDate(dataParamDic);

            return finalList;

        }

        public List<ChartJsDataSource> Total()
        {
            MySqlConnection conn = new MySqlConnection(WebConfigurationManager.ConnectionStrings["c349ConnectionString"].ConnectionString);
            //select tdate,client,count(client) from c34.remote_visua_inspection where category='線材' and client!=''  group by tdate,client
            string SQL_Prefix = $"select tdate,count(client),client from c34.remote_visua_inspection ";
            string SQL_Where_Clause = AddDateInWhereClause();
            List<string> SQLs = new List<string>();
            string SQL_Postfix = $"category='鋼板' and client!=''  group by tdate,client";
            string SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Prefix = $"select tdate,count(shipNum),shipNum from c34.remote_visua_inspection ";
            SQL_Where_Clause = AddDateInWhereClause();
            SQL_Postfix = $"category='鋼板' and client=''  group by tdate,shipNum";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Prefix = $"select tdate,count(tfrom),tfrom from c34.remote_visua_inspection ";
            SQL_Where_Clause = AddDateInWhereClause();
            SQL_Postfix = $"category='鋼板' and client=''  group by tdate,tfrom";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Prefix = $"select tdate,count(client),client from c34.remote_visua_inspection ";
            SQL_Where_Clause = AddDateInWhereClause();
            SQL_Postfix = $"category='線材' and client!=''  group by tdate,client";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Prefix = $"select tdate,count(shipNum),shipNum from c34.remote_visua_inspection ";
            SQL_Where_Clause = AddDateInWhereClause();
            SQL_Postfix = $"category='線材' and client=''  group by tdate,shipNum";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Prefix = $"select tdate,count(tfrom),tfrom from c34.remote_visua_inspection ";
            SQL_Where_Clause = AddDateInWhereClause();
            SQL_Postfix = $"category='線材' and client=''  group by tdate,tfrom";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Prefix = $"select tdate,count(client),client from c34.remote_visua_inspection ";
            SQL_Where_Clause = AddDateInWhereClause();
            SQL_Postfix = $"category='熱軋' and client!=''  group by tdate,client";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Prefix = $"select tdate,count(shipNum),shipNum from c34.remote_visua_inspection ";
            SQL_Where_Clause = AddDateInWhereClause();
            SQL_Postfix = $"category='熱軋' and client=''  group by tdate,shipNum";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Prefix = $"select tdate,count(tfrom),tfrom from c34.remote_visua_inspection ";
            SQL_Where_Clause = AddDateInWhereClause();
            SQL_Postfix = $"category='熱軋' and client=''  group by tdate,tfrom";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Prefix = $"select tdate,count(client),client from c34.remote_visua_inspection ";
            SQL_Where_Clause = AddDateInWhereClause();
            SQL_Postfix = $"category='冷軋' and client!=''  group by tdate,client";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Prefix = $"select tdate,count(shipNum),shipNum from c34.remote_visua_inspection ";
            SQL_Where_Clause = AddDateInWhereClause();
            SQL_Postfix = $"category='冷軋' and client=''  group by tdate,shipNum";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            SQL_Prefix = $"select tdate,count(tfrom),tfrom from c34.remote_visua_inspection ";
            SQL_Where_Clause = AddDateInWhereClause();
            SQL_Postfix = $"category='冷軋' and client=''  group by tdate,tfrom";
            SQL = SQL_Prefix + SQL_Where_Clause + SQL_Postfix;
            SQLs.Add(SQL);

            List<List<Dictionary<string, object>>> myLists = new List<List<Dictionary<string, object>>>();

            Dictionary<string, object> dataParamDic = new Dictionary<string, object>();
            dataParamDic.Add("myLists", myLists);
            dataParamDic.Add("SQLs", SQLs);
            dataParamDic.Add("conn", conn);
            myLists = AddDateParamAndReadSQL(dataParamDic);

            dataParamDic = new Dictionary<string, object>();
            dataParamDic.Add("SQLs", SQLs);
            dataParamDic.Add("myLists", myLists);

            List<ChartJsDataSource> finalList = Total_CombineValueByDate(dataParamDic);

            return finalList;

        }

        public List<ChartJsDataSource> Total_CombineValueByDate(Dictionary<string, object> dataParamDic)
        {

            List<string> SQLs = (List<string>)dataParamDic["SQLs"];

            List<List<Dictionary<string, object>>> SQLDataLists = (List<List<Dictionary<string, object>>>)dataParamDic["myLists"];

            Dictionary<string, List<string>> Date_DataLis_Dic = new Dictionary<string, List<string>>();
            //將查詢月份的所有日期填滿數值0
            if (tbMonth != "")
            {
                for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Parse(tbMonth).Year, DateTime.Parse(tbMonth).Month); i++)
                {
                    string finalDate = $"{DateTime.Parse(tbMonth).Year}/{DateTime.Parse(tbMonth).Month}/{i}";
                    List<string> finalRow = new List<string>();
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");

                    Date_DataLis_Dic.Add(finalDate, finalRow);
                }
            }

            int product = 0;
            //將SQL傳回相同日期的值合併在一起
            for (int k = 0; k < SQLDataLists.Count; k++) //list集合
            {
                //int k = 0;
                for (int j = 0; j < SQLDataLists[k].Count; j++)//row集合
                {
                    string tDate = DateTime.Parse(SQLDataLists[k].ElementAt(j).ElementAt(0).Value.ToString()).ToShortDateString().ToString();
                    string clientCount = SQLDataLists[k].ElementAt(j).ElementAt(1).Value.ToString();
                    string client = SQLDataLists[k].ElementAt(j).ElementAt(2).Value.ToString();

                    List<string> finalRow = new List<string>();
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");

                    //未指定月份，判斷是否需合併相同日期後進行處理
                    if (tbMonth == "")
                    {
                        finalRow[k + product * 2] = clientCount;
                        finalRow[k + product * 2 + 1] = client + "*" + clientCount;
                        if (!Date_DataLis_Dic.ContainsKey(tDate))//沒有出現過的日期，用add方法加入
                            Date_DataLis_Dic.Add(tDate, finalRow);
                        else
                        {
                            if (Date_DataLis_Dic[tDate][k + product * 2 + 1] == "")//代表日期有出現過，但是沒有客戶資料
                            {
                                Date_DataLis_Dic[tDate][k + product * 2] = clientCount;
                                Date_DataLis_Dic[tDate][k + product * 2 + 1] = client + "*" + clientCount;
                            }
                            else
                            {
                                Date_DataLis_Dic[tDate][k + product * 2] = (int.Parse(Date_DataLis_Dic[tDate][k + product * 2]) + int.Parse(clientCount)).ToString();
                                Date_DataLis_Dic[tDate][k + product * 2 + 1] = Date_DataLis_Dic[tDate][k + product * 2 + 1] + "</br>" + client + "*" + clientCount;
                            }
                        }
                    }
                    else
                    {
                        if (Date_DataLis_Dic[tDate][k + product * 2 + 1] == "")//代表日期有出現過，但是沒有客戶資料
                        {
                            Date_DataLis_Dic[tDate][k + product * 2] = clientCount;
                            Date_DataLis_Dic[tDate][k + product * 2 + 1] = client + "*" + clientCount;
                        }
                        else
                        {
                            Date_DataLis_Dic[tDate][k + product * 2] = (int.Parse(Date_DataLis_Dic[tDate][k + product * 2]) + int.Parse(clientCount)).ToString();
                            Date_DataLis_Dic[tDate][k + product * 2 + 1] = Date_DataLis_Dic[tDate][k + product * 2 + 1] + "</br>" + client + "*" + clientCount;
                        }
                    }

                }

                k++;
                for (int j = 0; j < SQLDataLists[k].Count; j++)//row集合
                {
                    string tDate = DateTime.Parse(SQLDataLists[k].ElementAt(j).ElementAt(0).Value.ToString()).ToShortDateString().ToString();
                    string shipNumCount = SQLDataLists[k].ElementAt(j).ElementAt(1).Value.ToString();
                    string shipNum = SQLDataLists[k].ElementAt(j).ElementAt(2).Value.ToString();
                    List<string> finalRow = new List<string>();
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");

                    //未指定月份，判斷是否需合併相同日期後進行處理
                    if (tbMonth == "")
                    {
                        finalRow[k + product * 2 + 1] = shipNumCount;
                        finalRow[k + product * 2 + 2] = shipNum + "*" + shipNumCount;
                        if (!Date_DataLis_Dic.ContainsKey(tDate))//沒有出現過的日期，用add方法加入
                            Date_DataLis_Dic.Add(tDate, finalRow);
                        else
                        {
                            if (Date_DataLis_Dic[tDate][k + product * 2 + 2] == "")
                            {
                                Date_DataLis_Dic[tDate][k + product * 2 + 1] = shipNumCount;
                                Date_DataLis_Dic[tDate][k + product * 2 + 2] = shipNum + "*" + shipNumCount;
                            }
                            else
                            {
                                Date_DataLis_Dic[tDate][k + product * 2 + 1] = (int.Parse(Date_DataLis_Dic[tDate][k + product * 2 + 1]) + int.Parse(shipNumCount)).ToString();
                                Date_DataLis_Dic[tDate][k + product * 2 + 2] = Date_DataLis_Dic[tDate][k + product * 2 + 2] + "</br>" + shipNum + "*" + shipNumCount;
                            }
                        }
                    }
                    else
                    {
                        if (Date_DataLis_Dic[tDate][k + product * 2 + 2] == "")
                        {
                            Date_DataLis_Dic[tDate][k + product * 2 + 1] = shipNumCount;
                            Date_DataLis_Dic[tDate][k + product * 2 + 2] = shipNum + "*" + shipNumCount;
                        }
                        else
                        {
                            Date_DataLis_Dic[tDate][k + product * 2 + 1] = (int.Parse(Date_DataLis_Dic[tDate][k + product * 2 + 1]) + int.Parse(shipNumCount)).ToString();
                            Date_DataLis_Dic[tDate][k + product * 2 + 2] = Date_DataLis_Dic[tDate][k + product * 2 + 2] + "</br>" + shipNum + "*" + shipNumCount;
                        }
                    }
                }
                k++;
                for (int j = 0; j < SQLDataLists[k].Count; j++)//row集合
                {
                    string tDate = DateTime.Parse(SQLDataLists[k].ElementAt(j).ElementAt(0).Value.ToString()).ToShortDateString().ToString();

                    string tfromCount = SQLDataLists[k].ElementAt(j).ElementAt(1).Value.ToString();
                    string tfrom = SQLDataLists[k].ElementAt(j).ElementAt(2).Value.ToString();
                    List<string> finalRow = new List<string>();
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");
                    finalRow.Add("0"); finalRow.Add(""); finalRow.Add("0"); finalRow.Add(""); finalRow.Add("");

                    //未指定月份，判斷是否需合併相同日期後進行處理
                    if (tbMonth == "")
                    {
                        finalRow[k + product * 2 + 2] = tfrom + "*" + tfromCount;
                        //finalRow[5 * (k - 1) + 4] = tfrom;
                        if (!Date_DataLis_Dic.ContainsKey(tDate))//沒有出現過的日期，用add方法加入
                            Date_DataLis_Dic.Add(tDate, finalRow);
                        else
                        {
                            if (Date_DataLis_Dic[tDate][k + product * 2 + 2] == "")
                            {
                                Date_DataLis_Dic[tDate][k + product * 2 + 2] = tfrom + "*" + tfromCount;
                            }
                            else
                            {
                                Date_DataLis_Dic[tDate][k + product * 2 + 2] = Date_DataLis_Dic[tDate][k + product * 2 + 2] + "</br>" + tfrom + "*" + tfromCount;
                            }
                        }
                    }

                    else
                    {
                        if (Date_DataLis_Dic[tDate][k + product * 2 + 2] == "")
                        {
                            Date_DataLis_Dic[tDate][k + product * 2 + 2] = tfrom + "*" + tfromCount;
                        }
                        else
                        {
                            Date_DataLis_Dic[tDate][k + product * 2 + 2] = Date_DataLis_Dic[tDate][k + product * 2 + 2] + "</br>" + tfrom + "*" + tfromCount;
                        }
                    }
                }
                product++;
            }

            //將合併後的值整理成ChartJs可用的形式
            List<ChartJsDataSource> finalList = new List<ChartJsDataSource>();
            for (int i = 0; i < Date_DataLis_Dic.Count; i++)
            {
                List<string> vs = new List<string>();
                vs.Add(Date_DataLis_Dic.ElementAt(i).Key);
                for (int j = 1; j <= Date_DataLis_Dic.ElementAt(i).Value.Count; j++)
                    vs.Add(Date_DataLis_Dic.ElementAt(i).Value.ElementAt(j - 1).ToString());

                ChartJsDataSource chartDataSource = new ChartJsDataSource(vs);
                finalList.Add(chartDataSource);
            }
            finalList.Sort();

            return finalList;

        }


        public List<ChartJsDataSource> CombineValueByDate(Dictionary<string, object> dataParamDic)
        {

            List<string> SQLs = (List<string>)dataParamDic["SQLs"];

            List<List<Dictionary<string, object>>> myLists = (List<List<Dictionary<string, object>>>)dataParamDic["myLists"];

            Dictionary<string, List<string>> finalDic = new Dictionary<string, List<string>>();
            //將查詢月份的所有日期填滿數值0
            if (tbMonth != "")
            {
                for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Parse(tbMonth).Year, DateTime.Parse(tbMonth).Month); i++)
                {
                    string finalDate = $"{DateTime.Parse(tbMonth).Year}/{DateTime.Parse(tbMonth).Month}/{i}";
                    List<string> finalRow = new List<string>();
                    for (int j = 0; j < SQLs.Count; j++) finalRow.Add("0");
                    finalDic.Add(finalDate, finalRow);
                }

            }
            //將SQL傳回相同日期的值合併在一起
            for (int i = 0; i < myLists.Count; i++) //list集合
            {
                for (int j = 0; j < myLists[i].Count; j++)//row集合
                {
                    string finalDate = DateTime.Parse(myLists[i].ElementAt(j).ElementAt(0).Value.ToString()).ToShortDateString().ToString();
                    string finalData = myLists[i].ElementAt(j).ElementAt(1).Value.ToString();
                    List<string> finalRow = new List<string>();
                    for (int k = 0; k < SQLs.Count; k++) finalRow.Add("0");

                    //未指定月份，判斷是否需合併相同日期後進行處理
                    if (tbMonth == "")
                    {
                        finalRow[i] = finalData;
                        if (!finalDic.ContainsKey(finalDate))//沒有出現過的日期，用add方法加入
                            finalDic.Add(finalDate, finalRow);
                        else
                            finalDic[finalDate][i] = finalData;//出現過的日期，用改值的方式合併
                    }
                    else//有指定月份，因為先前已將所有日期填滿0，故直接進行合併
                        finalDic[finalDate][i] = finalData;

                }
            }

            //將合併後的值整理成ChartJS可用的形式
            List<ChartJsDataSource> finalList = new List<ChartJsDataSource>();
            for (int i = 0; i < finalDic.Count; i++)
            {
                List<string> vs = new List<string>();
                vs.Add(finalDic.ElementAt(i).Key);
                for (int j = 1; j <= finalDic.ElementAt(i).Value.Count; j++)
                    vs.Add(finalDic.ElementAt(i).Value.ElementAt(j - 1).ToString());

                ChartJsDataSource chartDataSource = new ChartJsDataSource(vs);
                finalList.Add(chartDataSource);
            }
            finalList.Sort();

            return finalList;

        }
    }
}


