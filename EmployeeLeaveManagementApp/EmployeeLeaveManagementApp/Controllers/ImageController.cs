using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class ImageController : Controller
    {
        [HttpPost]
        public virtual ActionResult CropImage(string imagePath, int? cropPointX, int? cropPointY, int? imageCropWidth, int? imageCropHeight)
        {
            Logger.Info("Entering in ImageController APP CropImage method");
            try
            {
                if (string.IsNullOrEmpty(imagePath) || !cropPointX.HasValue || !cropPointY.HasValue || !imageCropWidth.HasValue || !imageCropHeight.HasValue)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
                }

                // string strimg = imagePath.Substring(imagePath.IndexOf(',')+1);
                // byte[] b = Convert.FromBase64String(strimg);
                // string strOriginal = System.Text.Encoding.UTF8.GetString(b);
                byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath(imagePath));
                byte[] croppedImage = ImageHelper.CropImage(imageBytes, cropPointX.Value, cropPointY.Value, imageCropWidth.Value, imageCropHeight.Value);

                string tempFolderName = Server.MapPath(ConfigurationManager.AppSettings["EmployeeImagePath"]);
                string fileName = Path.GetFileName(imagePath);
                string tempcropImagepath = Server.MapPath(ConfigurationManager.AppSettings["EmployeeImagePath"]) + "\\" + fileName;

                try
                {
                    FileHelper.SaveFile(croppedImage, Path.Combine(tempFolderName, fileName));
                }
                catch (Exception ex)
                {
                    //Log an error     
                    return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                }

                string photoPath = string.Concat("/", ConfigurationManager.AppSettings["Image.TempFolderName"], "/", fileName);
                Logger.Info("Successfully exiting from ImageController APP CropImage method");
                return Json(new { photoPath = photoPath, tempcropImagepath = tempcropImagepath }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ImageController APP CropImage method.", ex);
                return View("Error");
            }
        }
    }
}