using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace prjC349WebMVC.Controllers
{
    public class EIPLoginWebAPIController : ApiController
    {
        //public HttpResponseMessage Login(string userId, string userPassword, HttpRequestMessage request)
        //{
        //    //有內鬼，終止交易

        //    //這邊只能用cookkie儲存用戶的密碼
        //    //但是用cookie會變成明文
        //    //這邊應該搭配一個加密的程式，在替客戶端儲存密碼的同時保障隱私
        //    var resp = new HttpResponseMessage();



        //    EIP tmpEIPinstance = new EIP(userId, userPassword);
        //    if (CheckLoginInfo(userId, userPassword, request) == false)
        //    {
        //        var LoginState_cookie = new CookieHeaderValue("LoginState_cookie", "userId or userPassword empty");
        //        LoginState_cookie.Expires = DateTimeOffset.Now.AddDays(1);
        //        LoginState_cookie.Domain = Request.RequestUri.Host;
        //        LoginState_cookie.Path = "/";
        //        resp.Headers.AddCookies(new CookieHeaderValue[] { LoginState_cookie });
        //        return resp;
        //    }
        //    if (tmpEIPinstance.Login() == false)
        //    {
        //        var LoginState_cookie = new CookieHeaderValue("LoginState_cookie", "userId or userPassword error");
        //        LoginState_cookie.Expires = DateTimeOffset.Now.AddDays(1);
        //        LoginState_cookie.Domain = Request.RequestUri.Host;
        //        LoginState_cookie.Path = "/";
        //        resp.Headers.AddCookies(new CookieHeaderValue[] { LoginState_cookie });
        //        return resp;
        //    }

        //    var userId_cookie = new CookieHeaderValue("userId", userId);
        //    userId_cookie.Expires = DateTimeOffset.Now.AddDays(1);
        //    userId_cookie.Domain = Request.RequestUri.Host;
        //    userId_cookie.Path = "/";
        //    resp.Headers.AddCookies(new CookieHeaderValue[] { userId_cookie });

        //    var userPassword_cookie = new CookieHeaderValue("userPassword", userPassword);
        //    userPassword_cookie.Expires = DateTimeOffset.Now.AddDays(1);
        //    userPassword_cookie.Domain = Request.RequestUri.Host;
        //    userPassword_cookie.Path = "/";
        //    resp.Headers.AddCookies(new CookieHeaderValue[] { userPassword_cookie });


        //    return resp;
        //}

        //public bool CheckLoginInfo(string userId, string userPassword, HttpRequestMessage request)
        //{
        //    bool isNewUser = (userId == null || userPassword == null) ? false : true;
        //    var userId_cookie = request.Headers.GetCookies("userId").FirstOrDefault();
        //    var userPassword_cookie = request.Headers.GetCookies("userPassword").FirstOrDefault();
        //    bool isOldUser = (userId_cookie["userId"].Value == null || userPassword_cookie["userPassword"].Value == null) ? false : true;

        //    if (isNewUser == false || isOldUser == false) return false;

        //    string tmp_userId;
        //    string tmp_userPassword;

        //    if (isNewUser == true)
        //    {
        //        tmp_userId = userId;
        //        tmp_userPassword = userPassword;
        //        Session["userId"] = tmp_userId;
        //        Session["userPassword"] = tmp_userPassword;
        //        return true;
        //    }
        //    else if (isOldUser == true)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
    }
}
