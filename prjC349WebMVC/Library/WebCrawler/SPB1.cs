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
    public class SPB1
    {
        public EIP eip { get; set; }
        public List<Model> List { get { return _List; } }
        private List<Model> _List = new List<SPB1.Model>();
        public string userId { get; set; }
        public SPB1(EIP eipinstance, List<string> bill_of_ladding_List)
        {
            this.eip = eipinstance;
            foreach (string bill_of_ladding in bill_of_ladding_List)
            {
                Model tmp_spb1 = new Model
                {
                    bill_of_ladding = bill_of_ladding,
                    making_cargo_plan = PostToGetData(bill_of_ladding)
                };
                _List.Add(tmp_spb1);
            }
        }
        public class Model
        {
            public string bill_of_ladding { get; set; }
            public string making_cargo_plan { get; set; }
        }
        public string PostToGetData(string bill_of_ladding)
        {
            string returnData = "";
            string url = $"http://mvatcp.csc.com.tw:6080/CICS/SPB1/SPOUB1/{eip.userId}";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = eip.cookieContainer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;";
            string NX_INV_NO_DOC = bill_of_ladding;
            string FUNC_DOC = "I";

            string PostData = $"{nameof(NX_INV_NO_DOC)}={HttpUtility.UrlEncode(NX_INV_NO_DOC, Encoding.UTF8)}&{nameof(FUNC_DOC)}={HttpUtility.UrlEncode(FUNC_DOC, Encoding.UTF8)}";
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
                        var myParse1 = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/form[1]/table[2]/tr[3]/td/table/tr[2]/td[9]/font");
                        //byte[] b = Encoding.Default.GetBytes(myParse1.InnerText);//將字串轉為byte[]
                        //var uu = Encoding.GetEncoding(950).GetString(b);//驗證轉碼後的字串,仍正確的顯示.
                        //byte[] c = Encoding.Convert(Encoding.Default, Encoding.UTF8, b);//進行轉碼,參數1,來源編碼,參數二,目標編碼,參數三,欲編碼變數
                        //var cc = Encoding.UTF8.GetString(c);//顯示轉為UTF8後,仍能正確的顯示字串
                        return myParse1.InnerHtml.Split('>')[1];
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