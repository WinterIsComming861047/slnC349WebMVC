using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace prjC349WebMVC.Library.WebCrawler
{
    public class OYR1_OY01Report
    {
        public EIP eip { get; set; }
        public List<Model> List { get { return _List; } }
        private List<Model> _List = new List<OYR1_OY01Report.Model>();
        public string warehouse;
        public OYR1_OY01Report(EIP eip, string warehouse)
        {
            this.eip = eip;
            this.warehouse = warehouse;
            List<List<object>> oyr1_d1report_excel = PostToGetExcel();

            for (int i = 0; i < oyr1_d1report_excel.Count; i++)
            {
                Model tmp_oyr1_d1report = new Model
                {
                    warehouse = oyr1_d1report_excel.ElementAt(i).ElementAt(0).ToString(),
                    area = oyr1_d1report_excel.ElementAt(i).ElementAt(1).ToString(),
                    line = oyr1_d1report_excel.ElementAt(i).ElementAt(2).ToString(),
                    preserve = oyr1_d1report_excel.ElementAt(i).ElementAt(3).ToString(),
                    length = oyr1_d1report_excel.ElementAt(i).ElementAt(4).ToString(),
                    base_value = oyr1_d1report_excel.ElementAt(i).ElementAt(5).ToString(),
                    center = oyr1_d1report_excel.ElementAt(i).ElementAt(6).ToString(),
                    area_code = oyr1_d1report_excel.ElementAt(i).ElementAt(7).ToString(),
                    area_code_1 = oyr1_d1report_excel.ElementAt(i).ElementAt(8).ToString().Split('.')[0],
                    layer = oyr1_d1report_excel.ElementAt(i).ElementAt(9).ToString(),
                    category = oyr1_d1report_excel.ElementAt(i).ElementAt(10).ToString().Split('.')[0],
                    remain = oyr1_d1report_excel.ElementAt(i).ElementAt(11).ToString(),
                    total = oyr1_d1report_excel.ElementAt(i).ElementAt(12).ToString(),
                };
                _List.Add(tmp_oyr1_d1report);
            }
        }
        public class Model
        {
            public string warehouse { get; set; }
            public string area { get; set; }
            public string line { get; set; }
            public string preserve { get; set; }
            public string length { get; set; }
            public string base_value { get; set; }
            public string center { get; set; }
            public string area_code { get; set; }
            public string area_code_1 { get; set; }
            public string layer { get; set; }
            public string category { get; set; }
            public string remain { get; set; }
            public string total { get; set; }
        }

        public List<List<object>> PostToGetExcel()
        {
            string url = "http://eas.csc.com.tw/oy/report/oyr1";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = eip.cookieContainer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string stock = warehouse;
            string action = "getOY01Report";

            string PostData = $"stocks[]={HttpUtility.UrlEncode(stock, Encoding.UTF8)}&_action={HttpUtility.UrlEncode(action, Encoding.UTF8)}";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(PostData);
            request.ContentLength = bytes.Length;

            //意義不明
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(bytes, 0, bytes.Length);
                dataStream.Close();
            }

            string filePath = $@"D:\C349WebMVC\tmp_OYR1_OY01Report_{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}.xlsx";

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