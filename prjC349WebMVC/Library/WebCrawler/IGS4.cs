using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace prjC349WebMVC
{
    public class IGS4
    {
        public EIP eip { get; set; }
        public List<Model> List { get { return _List; }}
        private List<Model> _List = new List<IGS4.Model>();
        public IGS4(EIP eip)
        {
            this.eip = eip;
            var igs4_response = PostToURL();
            List<List<object>> igs4_excel = getExcel(eip.cookieContainer, igs4_response.Filename);

            for (int i = 2; i < igs4_excel.Count - 4; i++)
            {
                Model tmp_igs4 = new Model
                {
                    bill_of_ladding = igs4_excel.ElementAt(i).ElementAt(0).ToString(),
                    area = igs4_excel.ElementAt(i).ElementAt(1).ToString(),
                    label = igs4_excel.ElementAt(i).ElementAt(2).ToString(),
                    order_num = igs4_excel.ElementAt(i).ElementAt(3).ToString(),
                    weight = igs4_excel.ElementAt(i).ElementAt(4).ToString(),
                    width = igs4_excel.ElementAt(i).ElementAt(5).ToString(),
                    state = igs4_excel.ElementAt(i).ElementAt(6).ToString(),
                    ship_num_or_customer = igs4_excel.ElementAt(i).ElementAt(7).ToString(),
                };
                _List.Add(tmp_igs4);
            }
        }

        #region Response
        public class Response
        {
            public string Severity { get; set; }
            public List<Alert> Alerts { get; set; }
            public string Filename { get; set; }
            public string Message { get; set; }
        }

        public class Alert
        {
            public string Type { get; set; }
            public string Message { get; set; }
        }
        #endregion

        public class Model
        {
            public string bill_of_ladding { get; set; }
            public string area { get; set; }
            public string label { get; set; }
            public string order_num { get; set; }
            public string weight { get; set; }
            public string width { get; set; }
            public string state { get; set; }
            public string ship_num_or_customer { get; set; }
        }


        public Response PostToURL()
        {
            string url = "http://eas.csc.com.tw/ig/stock/igs4?_action=exportExcel&_format=json";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = eip.cookieContainer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "application/json;charset=UTF-8";
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            var jsonObj = new
            {
                stock = "89"
            };
            string PostData = JsonConvert.SerializeObject(jsonObj);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(PostData);
            request.ContentLength = bytes.Length;

            //意義不明
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(bytes, 0, bytes.Length);
                dataStream.Close();
            }


            //看到.GetResponse()才代表真正把 request 送到 伺服器
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    // sr 就是伺服器回覆的資料
                    Response igs4_response = JsonConvert.DeserializeObject<Response>(sr.ReadToEnd());
                    return igs4_response;
                }
            }
        }

        public List<List<object>> getExcel(CookieContainer cookieContainer, string filename)
        {
            //string url = "http://eas.csc.com.tw/public/dx/ig/IFLAR_20230324-223927.xlsx";
            string url = Path.Combine("http://eas.csc.com.tw/public/dx/ig", filename);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = cookieContainer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            request.Method = "GET";
            request.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8";


            string filePath = $@"D:\C349WebMVC\tmp_IGS4_{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}.xlsx";

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