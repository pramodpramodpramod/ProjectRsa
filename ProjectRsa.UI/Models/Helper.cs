using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectRsa.UI.Models
{
    public static class Helper
    {

        public static string ScreenShotPath
        {
            get
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "content/screenshots");
                return path;
            }
        }

        public static string ScreenShotUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ScreenShotUrl"];
            }
        }

    }
}