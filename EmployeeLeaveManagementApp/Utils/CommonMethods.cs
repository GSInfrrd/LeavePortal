using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using LMS_WebAPP_Utils;
using System.IO;
using System.Net.Mime;
using System.Web;

namespace LMS_WebAPP_Utils
{
    public static class CommonMethods
    {

        public static string EncryptDataForLogins(string username, string password)
        {
            try
            {
                string salt = Constants.SALT;
                // merge password and salt together
                string sHashWithSalt = password + username.Trim() + salt;
                // convert this merged value to a byte array
                byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
                // use hash algorithm to compute the hash
                HashAlgorithm algorithm = new SHA256Managed();
                // convert merged bytes to a hash as byte array
                byte[] hash = algorithm.ComputeHash(saltedHashBytes);
                // return the has as a base 64 encoded string
                return Convert.ToBase64String(hash);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EncryptString(string message)
        {
            string result = null;
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Constants.SALT));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(message);
            try
            {
                ICryptoTransform encryptor = TDESAlgorithm.CreateEncryptor();
                Results = encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
                result = Convert.ToBase64String(Results);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return result;
        }

        public static string DecryptString(string Message)
        {
            string result = null;
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Constants.SALT));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            try
            {
                byte[] DataToDecrypt = Convert.FromBase64String(Message);
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
                result = UTF8.GetString(Results);
            }
            catch (Exception ex)
            {
                //Logger.Error("CommonMethods : DecryptString(): Caught an Error " + ex);
                return result;
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return result;
        }

        public static string Description(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField(enumValue.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
        }

        public static void SendMailWithMultipleAttachments(string ToEmailId , bool IsAttachment ,string subject,string content = "",MemoryStream attachmentPath=null,string attachmentName="")
        {
            var message = new MailMessage();
            try
            {
                Logger.Info("Enter in to the SendMailWithMultipleAttachments");
                message.From = new MailAddress("alekya@infrrd.ai");
                //string ccEmailId = ReadResource.GetEmailConstant("CC_EMAIL_ID");
                string ccEmailId = ToEmailId;
                string toEmailId = ToEmailId;
              


                IList<string> toEmailIds = new List<string>();
                var mailPool = toEmailId.Split(new string[] { ":::" }, StringSplitOptions.None);
                if (mailPool.Count() > 0)
                {
                    foreach (var mail in mailPool)
                    {
                        if (!String.IsNullOrEmpty(mail))
                        {
                            message.To.Add(new MailAddress(mail));
                        }
                    }
                }
                else
                {
                    Logger.Error("To Mail Id should not be null");
                    throw new Exception(" To Mail Id should not be null");
                }
                if (ccEmailId != null && !ccEmailId.Equals(String.Empty))
                {
                    message.CC.Add(new MailAddress(ccEmailId));
                }

                message.Subject = subject;
                message.Body = content;
                message.IsBodyHtml = true;
                if (IsAttachment)
                {
                    attachmentPath.Position = 0;
                    var attachment = new Attachment(attachmentPath,attachmentName+".xls");
                    // message.Attachments.Add(new Attachment(attachment,attachmentName+".xls"));      
                    message.Attachments.Add(attachment);  
                }
                using (var client = new SmtpClient())
                {
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("alekya@infrrd.ai", "alkvyS9.");

                    client.Send(message);
                }
                // client.Host = ReadResource.GetEmailConstant("EMAIL_HOST");
                //client.Port = Convert.ToInt16(ReadResource.GetEmailConstant("EMAIL_PORT"));
                //client.UseDefaultCredentials = true;
                //client.Credentials = new System.Net.NetworkCredential(ReadResource.GetEmailConstant("EMAIL_USER_ID"), ReadResource.GetEmailConstant("EMAIL_PASSWORD"));
                //client.Send(message);

                Logger.Info("Executed successfully");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
            //finally
            //{
            //    Logger.Info("Enter in to the finally bloc of SendMailWithMultipleAttachments() ");
            //    if (message != null)
            //    {
            //        if (message.Attachments != null)
            //        {
            //            message.Attachments.Dispose();
            //        }
            //        message.Dispose();
            //    }
            //    if (client != null)
            //    {
            //        client.Dispose();
            //    }
            //}
        }
    }
}
