using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace prjC349WebMVC
{
    public  class FileManager
    {
        public static void tryDeleteFile(string filePath)
        {
            //刪除可能存在的舊檔案
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    Console.WriteLine("檔案已刪除");
                }
                else
                {
                    Console.WriteLine("找不到指定的檔案");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("發生錯誤：{0}", ex.Message);
            }
        }
    }
}