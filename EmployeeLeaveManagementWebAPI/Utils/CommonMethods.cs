using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Reflection;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using LMS_WebAPI_Domain;

namespace LMS_WebAPI_Utils
{
    public static class CommonMethods
    {

        public static string Description(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField(enumValue.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
        }    
    }
}
