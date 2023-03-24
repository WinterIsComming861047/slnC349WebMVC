using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjC349WebMVC.Models;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace prjC349WebMVC.Controllers
{
    public class ToolBoxController : Controller
    {
        c349dbEntities db = new c349dbEntities();
        // GET: ToolBox
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakingCargoPlan(string userId, string userPassword)
        {
            var MakingCargoPlanList = db.tMakingCargoPlan.OrderByDescending(m => m.id).ToList();
            if (userId == null || userPassword == null)
            {
                return View(MakingCargoPlanList);
            }

            EIP tmpEIPinstance = new EIP(userId, userPassword);
            if (tmpEIPinstance.Login())
            {
                ViewBag.isLogin = tmpEIPinstance.isLogin;
            };
            ViewBag.userId = userId;
            ViewBag.userPassword = userPassword;

            getExcel(tmpEIPinstance.cookieContainer);
            //string path = $@"C:\Users\214585\tmp_IGS4.xlsx";
            //string url = "http://eas.csc.com.tw/public/dx/ig/IFLAR_20230324-223927.xlsx";
            //var jsonObj = new
            //{
            //    stock = "89"
            //};
            //string PostData = JsonConvert.SerializeObject(jsonObj);
            //DownloadExcelFile(url, path, PostData, tmpEIPinstance.cookieContainer);
            //downExcel(url, path, PostData, tmpEIPinstance.cookieContainer);
            //IGS4(tmpEIPinstance.cookieContainer);

            return View(MakingCargoPlanList);
        }
        public void IGS4(CookieContainer cookieContainer)
        {
            string url = "http://eas.csc.com.tw/public/dx/ig/IFLAR_20230324-223927.xlsx";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = cookieContainer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "application/json,text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Method = "POST";
            request.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
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
                using (Stream sr = response.GetResponseStream())
                {
                    // sr 就是伺服器回覆的資料
                    FileStream fileStream = new FileStream($@"C:\Users\214585\tmp_IGS4_2.xlsx", FileMode.Create);
                    sr.CopyTo(fileStream);
                    ExcelManager.readExcel(fileStream);
                }
            }
        }

        public void getExcel(CookieContainer cookieContainer)
        {
            string url = "http://eas.csc.com.tw/public/dx/ig/IFLAR_20230324-223927.xlsx";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = cookieContainer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            request.Method = "GET";
            request.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8";


            FileStream fileStream = new FileStream($@"C:\Users\214585\tmp_IGS4_4.xlsx", FileMode.Create);
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
            fileStream = new FileStream($@"C:\Users\214585\tmp_IGS4_4.xlsx", FileMode.Create);
            ExcelManager.readExcel(fileStream);
        }

        public void downExcel(string url, string filePath, string postData, CookieContainer cookieContainer)
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            //set the cookie container object
            request.CookieContainer = cookieContainer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //set method POST and content type application/x-www-form-urlencoded
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";


            var jsonObj = new
            {
                stock = "89"
            };
            string PostData = JsonConvert.SerializeObject(jsonObj);

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(postData);
            request.ContentLength = bytes.Length;

            //意義不明
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(bytes, 0, bytes.Length);
                dataStream.Close();
            }

            var jsonXX = new
            {
                stock = "89"
            };
            //看到.GetResponse()才代表真正把 request 送到 伺服器
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    // sr 就是伺服器回覆的資料
                    string yy = sr.ReadToEnd(); //將 sr 寫入到 html中，呈現給客戶端看
                    JsonReader reader = new JsonTextReader(new StringReader(yy));
                }
            }


        }

        public async Task<bool> DownloadExcelFile(string url, string filePath, string postData, CookieContainer cookieContainer)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.CookieContainer = cookieContainer;

                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        using (var content = new StringContent(postData, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded"))
                        {
                            using (var response = await httpClient.PostAsync(url, content))
                            {
                                using (var stream = await response.Content.ReadAsStreamAsync())
                                {
                                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                                    {
                                        stream.CopyTo(fileStream);
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        public static void SaveAs(Uri url, string filename)
        {
            using (var client = new WebClient())
            {

                var data = client.DownloadData(url);
                SaveAs(data, filename);
            }

        }
        public static void SaveAs(byte[] data, string filename)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var fs = new FileStream(filename, FileMode.Create))
                {
                    ms.WriteTo(fs);
                }

            }
        }
    }
}