using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_Utils
{
    public class ReadResource
    {
        static System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
        static ResourceManager resourceManagerEmailConstant = new ResourceManager("ServiceLayer.Constants", Assembly.GetExecutingAssembly());
        public static string GetEmailConstant(string sMsgCode)
        {
            string resourceValue = string.Empty;
            try
            {
                resourceValue = resourceManagerEmailConstant.GetString(sMsgCode,ci);
            }
            catch (Exception ex)
            {
                resourceValue = "";
            }
            return resourceValue;
        }
    }
}
