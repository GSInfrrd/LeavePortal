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

        public static string encryption(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }
    }
}
