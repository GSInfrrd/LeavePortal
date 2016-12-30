﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_Utils
{
    public static class Logger
    {
        private static log4net.ILog Log { get; set; }

        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();

            Log = log4net.LogManager.GetLogger("ApplicationLog");//log4net.LogManager.GetLogger(typeof(Logger));
        }

        public static void Error(object msg)
        {
            Log.Error(msg);
        }

        public static void Error(object msg, Exception ex)
        {
            Log.Error(msg, ex);
        }

        public static void Error(Exception ex)
        {
            Log.Error(ex.Message, ex);
        }

        public static void Info(string msg)
        {
            Log.Info(msg);
        }
    }
}
