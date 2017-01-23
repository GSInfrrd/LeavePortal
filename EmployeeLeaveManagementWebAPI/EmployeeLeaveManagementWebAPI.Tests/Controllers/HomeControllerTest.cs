using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeLeaveManagementWebAPI;
using EmployeeLeaveManagementWebAPI.Controllers;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_DAL.Repositories;
using System;

namespace EmployeeLeaveManagementWebAPI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private IUser user = new UserRepository();
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
        [TestMethod]
        public void GetEmployess()
        {
            try
            {
                var details = user.GetUserDetails(1);
            }
            catch(Exception ex)
            {
                throw;
            }

        }
    }
}

