using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace prjC349WebMVC
{
    public class EIP
    {
        public string userId { get; set; }
        public string userPassword { get; set; }
        public CookieContainer cookieContainer { get { return _cookieContainer; } }
        private CookieContainer _cookieContainer;
        public bool isLogin { get { return _isLogin; } }
        private bool _isLogin;

        public string userName { get { return _userName; } }
        private string _userName;
        public string userDept { get { return _userDept; } }
        private string _userDept;

        public EIP(string userId, string userPassword)
        {
            this.userId = userId;
            this.userPassword = userPassword;
            _cookieContainer = new CookieContainer();
            _isLogin = false;
            _userName = null;
            _userDept = null;
        }

        public bool Login()
        {
            try
            {
                string url = "https://eip.csc.com.tw/SSO/DSS0/DSAOS0.aspx/";
                string myViewState = "";
                string myViewStateGenerator = "";
                string myEventValidation = "";

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                //set the cookie container object
                request.CookieContainer = cookieContainer;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                //set method POST and content type application/x-www-form-urlencoded
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";


                //看到.GetResponse()才代表真正把 request 送到 伺服器
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default))
                    {
                        var z = sr.ReadToEnd();
                        var document = new HtmlAgilityPack.HtmlDocument();
                        document.LoadHtml(z);
                        //Response.Write(z);


                        //注意:直接從chropath插件複製出來的xPath會有/tbody/、/div/這一層，有時候要刪掉、調整才能正常讀取(div亂序)
                        //

                        string[] tmpWebBrowserDLString = new string[2];
                        string[] tmpSplit = new string[2];
                        int k = 0;


                        var myParse1 = document.DocumentNode.SelectSingleNode("html[1]/body[1]/div[1]/form[1]/div[1]").InnerHtml;
                        ///html[1]/body[1]/div[1]/form[1]/div[1]/input[1]
                        var l = myParse1.IndexOf("value");
                        myParse1 = myParse1.Remove(0, myParse1.IndexOf("value"));
                        myParse1 = myParse1.Remove(0, myParse1.IndexOf("\"") + 1);
                        myParse1 = myParse1.Remove(myParse1.IndexOf("\""));
                        myParse1 = myParse1.Trim();
                        myViewState = myParse1;
                        myParse1 = "";

                        //myParse1 = document.DocumentNode.SelectSingleNode("html[1]/body[1]/div[1]/form[1]/div[2]/input[1]").OuterHtml;
                        myParse1 = document.DocumentNode.SelectSingleNode("//input[@id='__VIEWSTATEGENERATOR']").GetAttributeValue("value", "default"); ;
                        myViewStateGenerator = myParse1;
                        myParse1 = "";

                        myParse1 = document.DocumentNode.SelectSingleNode("//input[@id='__EVENTVALIDATION']").GetAttributeValue("value", "default");
                        //myParse1 = document.DocumentNode.SelectSingleNode("html[1]/body[1]/div[1]/form[1]/div[2]/input[2]").GetAttributeValue("value", "default");
                        ////input[@id='__EVENTVALIDATION']
                        ///html[1]/body[1]/div[1]/form[1]/div[2]/input[2]

                        myEventValidation = myParse1;
                        myParse1 = "";
                    }
                }

                request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.CookieContainer = cookieContainer;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                //set method POST and content type application/x-www-form-urlencoded
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                string EIPLoginPostData = "";
                string PostHead = "__VIEWSTATE=" + HttpUtility.UrlEncode(myViewState, Encoding.UTF8) + "&__VIEWSTATEGENERATOR=" + HttpUtility.UrlEncode(myViewStateGenerator) + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(myEventValidation);
                string userTail = string.Format($"&uxCompany=&uxUserId={userId}&uxPassword={userPassword}&uxSubmit=%E7%99%BB%E5%85%A5");
                //string userTail = string.Format("&uxCompany=&uxUserId={0}&uxPassword={1}&uxSubmit=%E7%99%BB%E5%85%A5", "214585", "791005"); //測試用userTail
                EIPLoginPostData = PostHead + userTail;

                string[] mySplit = EIPLoginPostData.Split('%');
                for (int i = 1; i < mySplit.Length; i++)
                {
                    var g = mySplit[i].Substring(0, 2).ToUpper();
                    var h = mySplit[i].Substring(2, mySplit[i].Length - g.Length);
                    mySplit[i] = g + h;
                }

                EIPLoginPostData = string.Join("%", mySplit);




                string postData = string.Format(EIPLoginPostData);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(postData);
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
                        var yy = sr.ReadToEnd(); //將 sr 寫入到 html中，呈現給客戶端看
                    }
                }

                //儲存登入者身分
                Uri uri = new Uri("http://eip.csc.com.tw");
                IEnumerator enumerator = cookieContainer.GetCookies(uri).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    string key2 = enumerator.Current.ToString();
                    string key = enumerator.Current.ToString().Split('=')[0];

                    if (key == "name") _userName = System.Web.HttpUtility.UrlDecode(enumerator.Current.ToString().Split('=')[1]);
                    if (key == "dept") _userDept = enumerator.Current.ToString().Split('=')[1];
                }

                if (userName != null && userDept != null)
                {
                    _isLogin = true;
                    Console.WriteLine("登入成功");
                    Console.WriteLine($"您好，{_userName} {_userDept}");
                }
                else
                {
                    _isLogin = false;
                    Console.WriteLine($"登入失敗，請重新登入");
                }
                //儲存登入者身分
            }
            catch
            {
            }
            return _isLogin;
        }
    }
}