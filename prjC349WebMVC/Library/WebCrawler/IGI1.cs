using MySqlX.XDevAPI;
using Org.BouncyCastle.Bcpg;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace prjC349WebMVC.Library.WebCrawler
{
    public class IGI1
    {
        public EIP eip { get; set; }
        public List<Model> List { get { return _List; } }
        private List<Model> _List = new List<IGI1.Model>();
        public string userId { get; set; }
        public IGI1(EIP eipinstance, List<string> label_List)
        {
            this.eip = eipinstance;
            foreach (string label in label_List)
            {
                Model tmp_igi1 = PostToGetData(label);
                _List.Add(tmp_igi1);
            }
        }
        public class Model
        {
            public string label { get; set; }
            public string order_num { get; set; }
            public string state { get; set; }
            public string preserve_code { get; set; }
        }
        public IGI1.Model PostToGetData(string label)
        {
            IGI1.Model returnData = new IGI1.Model();

            string url = $"http://eas.csc.com.tw/ig/basic/igi1";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = eip.cookieContainer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            string coilNoLabel = label;
            string _action = "read";

            string PostData = $"{nameof(coilNoLabel)}={HttpUtility.UrlEncode(coilNoLabel, Encoding.UTF8)}&{nameof(_action)}={HttpUtility.UrlEncode(_action, Encoding.UTF8)}";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(PostData);
            request.ContentLength = bytes.Length;

            //意義不明
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(bytes, 0, bytes.Length);
                dataStream.Close();
            }


            try
            {
                //看到.GetResponse()才代表真正把 request 送到 伺服器
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        // sr 就是伺服器回覆的資料
                        var z = sr.ReadToEnd(); //將 sr 寫入到 html中，呈現給客戶端看
                        var document = new HtmlAgilityPack.HtmlDocument();
                        document.LoadHtml(z);
                        //var myParse1 = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/form[1]/table[2]/tr[3]/td/table/tr[2]/td[9]/font");
                        var orderNum = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[3]/div[2]/form[1]/table/tbody/tr[2]/td[3]/a");
                        returnData.order_num = orderNum.InnerHtml.Split('\r')[0];
                        var state = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[3]/div[2]/form[1]/table/tbody/tr[7]/td[2]");
                        returnData.state = state.InnerHtml;
                        var preserve = document.DocumentNode.SelectSingleNode("/html/body/div[3]/div[2]/form/table/tbody/tr[18]/td[3]");
                        returnData.preserve_code = preserve.InnerHtml;
                        returnData.label = label;

                        return returnData;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("發生錯誤：{0}", ex.Message);
            }


            return returnData;

        }
    }
}