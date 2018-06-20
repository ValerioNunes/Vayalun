﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace HelperDinamico.Extension
{
    public class DebugLog
    {
        public static Boolean Logar(String strInfo)
        {
            bool result = false;
            string fileName = "";
            StreamWriter tw = null;

            fileName = ConfigurationManager.AppSettings["LOG_FILE_NAME"] + "_" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "_" + DateTime.Now.Hour;
            tw = new StreamWriter(HttpContext.Current.Server.MapPath("~/") + "/Logs/" + fileName + ".txt", true, System.Text.Encoding.Default);
            // tw = new StreamWriter("C:/inetpub/logs/" + fileName + ".txt", true, System.Text.Encoding.Default);

            strInfo = DateTime.Now + " => " + strInfo;
            tw.WriteLine(strInfo);
            result = true;
            tw.Close();

            return result;
        }
    }
}