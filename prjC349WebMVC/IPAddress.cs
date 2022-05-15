using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjC349WebMVC
{
    public static class IPAddress
    {
        public static string Get()
        {
            HttpContext context = HttpContext.Current;
            string IPAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(IPAddress))
            {
                string[] addresses = IPAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];

        }
    }
}