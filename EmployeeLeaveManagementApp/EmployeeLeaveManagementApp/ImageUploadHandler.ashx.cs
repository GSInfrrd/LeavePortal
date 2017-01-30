using EmployeeLeaveManagementApp.Controllers;
using LMS_WebAPP_Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeLeaveManagementApp
{

    /// <summary>
    /// Summary description for ImageUploadHandler
    /// </summary>
    public class ImageUploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];
                    string fname;
                    if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }
                    if (context.Request.UrlReferrer.OriginalString.Contains("CompanyAnnouncement"))
                        {
                        fname = Path.Combine(context.Server.MapPath(ConfigurationManager.AppSettings["ImagePath"]), fname);
                    }
                    else
                    {
                        fname = Path.Combine(context.Server.MapPath(ConfigurationManager.AppSettings["EmployeeImagePath"]), fname);
                    }
                    file.SaveAs(fname);
                  //  var image = new ProfileImageModel();
                  //  using (var binaryReader = new BinaryReader(file.InputStream))
                  //  {
                  //      binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
                  //      using (var fs = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                  //      {
                  //          int numBytesToRead = file.ContentLength;
                  //          do
                  //          {
                  //              int length = numBytesToRead > 2048 ? 2048 : numBytesToRead;
                  //              var fileData = new byte[length];
                  //              int n = binaryReader.Read(fileData, 0, length);
                  //              if (n == 0)
                  //                  break;
                  //              fs.Write(fileData, 0, length);
                  //              numBytesToRead -= n;
                  //          } while (numBytesToRead > 0);
                  //          fs.Close();
                  //      }
                  //      image.FileName = file.FileName;
                  //      image.FilePath = fname;
                  //      image.ContentLength = file.ContentLength;
                  //      image.ContentType = file.ContentType;
                  //  }
                  //  var hrController = new HRController();
                  //  var imageId = 0;
                  //Task.Run(async () => { imageId = await hrController.AddImage(image); }).Wait();
                    context.Response.Write(fname);
            }
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
}