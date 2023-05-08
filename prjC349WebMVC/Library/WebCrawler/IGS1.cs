using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using static prjC349WebMVC.IGS4;

namespace prjC349WebMVC
{
    public class IGS1
    {
        public EIP eip { get; set; }
        public List<Model> List { get { return _List; } }
        private List<Model> _List = new List<IGS1.Model>();
        public string warehouse;
        public IGS1(EIP eip)
        {
            this.eip = eip;
            List<List<object>> igs1_excel = GETToGetExcel();

            for (int i = 2; i < igs1_excel.Count; i++)
            {
                Model tmp_igs1 = new Model
                {
                    label = igs1_excel.ElementAt(i).ElementAt(0).ToString(),
                    thick = igs1_excel.ElementAt(i).ElementAt(1).ToString(),
                    width = igs1_excel.ElementAt(i).ElementAt(2).ToString(),
                    weight = igs1_excel.ElementAt(i).ElementAt(3).ToString(),
                    warehouse = igs1_excel.ElementAt(i).ElementAt(4).ToString(),
                    area = igs1_excel.ElementAt(i).ElementAt(5).ToString(),
                    prod_category = igs1_excel.ElementAt(i).ElementAt(6).ToString(),
                    spec = igs1_excel.ElementAt(i).ElementAt(7).ToString(),
                    state = igs1_excel.ElementAt(i).ElementAt(8).ToString(),
                    has_owner = igs1_excel.ElementAt(i).ElementAt(9).ToString(),
                    prod_Date = igs1_excel.ElementAt(i).ElementAt(10).ToString(),
                    release_Date = igs1_excel.ElementAt(i).ElementAt(11).ToString(),
                    bill_of_ladding = igs1_excel.ElementAt(i).ElementAt(12).ToString(),
                    change_order_num_count = igs1_excel.ElementAt(i).ElementAt(13).ToString(),
                    order_num = igs1_excel.ElementAt(i).ElementAt(14).ToString(),
                    order_category = igs1_excel.ElementAt(i).ElementAt(15).ToString(),
                    order_delivery = igs1_excel.ElementAt(i).ElementAt(16).ToString(),
                    is_order_finish = igs1_excel.ElementAt(i).ElementAt(17).ToString(),
                    customer = igs1_excel.ElementAt(i).ElementAt(18).ToString(),
                    customer_num = igs1_excel.ElementAt(i).ElementAt(19).ToString(),
                    region = igs1_excel.ElementAt(i).ElementAt(20).ToString(),
                    place = igs1_excel.ElementAt(i).ElementAt(21).ToString(),
                    undertaker = igs1_excel.ElementAt(i).ElementAt(22).ToString(),
                    dept = igs1_excel.ElementAt(i).ElementAt(23).ToString(),
                    order_month = igs1_excel.ElementAt(i).ElementAt(24).ToString(),
                    before_ask_days = igs1_excel.ElementAt(i).ElementAt(25).ToString(),
                    before_ask_interval = igs1_excel.ElementAt(i).ElementAt(26).ToString(),
                    overdue_interval = igs1_excel.ElementAt(i).ElementAt(27).ToString(),
                    inventory_days = igs1_excel.ElementAt(i).ElementAt(28).ToString(),
                    inventory_days_interval = igs1_excel.ElementAt(i).ElementAt(29).ToString()
                };
                _List.Add(tmp_igs1);
            }
        }
        public class Model
        {
            public string label { get; set; }
            public string thick { get; set; }
            public string width { get; set; }
            public string weight { get; set; }
            public string warehouse { get; set; }
            public string area { get; set; }
            public string prod_category { get; set; }
            public string spec { get; set; }
            public string state { get; set; }
            public string has_owner { get; set; }
            public string prod_Date { get; set; }
            public string release_Date { get; set; }
            public string bill_of_ladding { get; set; }
            public string change_order_num_count { get; set; }
            public string order_num { get; set; }
            public string order_category { get; set; }
            public string order_delivery { get; set; }
            public string is_order_finish { get; set; }
            public string customer { get; set; }
            public string customer_num { get; set; }
            public string region { get; set; }
            public string place { get; set; }
            public string undertaker { get; set; }
            public string dept { get; set; }
            public string order_month { get; set; }
            public string before_ask_days { get; set; }
            public string before_ask_interval { get; set; }
            public string overdue_interval { get; set; }
            public string inventory_days { get; set; }
            public string inventory_days_interval { get; set; }
        }


        public List<List<object>> GETToGetExcel()
        {
            string url = "http://eas.csc.com.tw/ig/stock/igs1?_action=generateReport&stock=89&type=All&isDomestic=false&isExport=false";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = eip.cookieContainer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            request.Method = "GET";

            string filePath = $@"D:\C349WebMVC\tmp_IGS1_{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}.xlsx";

            FileManager.tryDeleteFile(filePath);
            FileStream fileStream = new FileStream(filePath, FileMode.Create);

            try
            {
                //看到.GetResponse()才代表真正把 request 送到 伺服器
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream sr = response.GetResponseStream())
                    {
                        // sr 就是伺服器回覆的資料
                        sr.CopyTo(fileStream);
                        fileStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("發生錯誤：{0}", ex.Message);
            }


            //fileStream = new FileStream(filePath, FileMode.Create);
            List<List<object>> lists = ExcelManager.readExcel(filePath);
            FileManager.tryDeleteFile(filePath);
            return lists;

        }
    }
}