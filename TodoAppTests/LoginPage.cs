using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TodoAppTests
{

    [TestClass]
    [TestCategory("Logging in to website")]
    public class Login
    {
 
        private IWebDriver driver;

        [TestInitialize]
        public void GetChromeDriver()
        
        {
            // Get Chrome driver location
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // Create an instance of Chrome driver
            driver = new ChromeDriver(outPutDirectory);
            // Navigate to a website
            driver.Navigate().GoToUrl("https://slonczynski.github.io");
        }

        [TestMethod]
        public void CheckElementsExistence()
        {
            // *Check texts*

            // Check welcome message
            var signText = driver.FindElement(By.ClassName("sign-text")).Text;
            Assert.AreEqual("Sign in:", signText);

            // Check number of inputs
            var inputs = driver.FindElements(By.ClassName("form-control")).Count;
            Assert.AreEqual(2, inputs);

            // Check if inputs exist
            var inputEmail = driver.FindElement(By.Id("input-email"));
            var inputPassword = driver.FindElement(By.Id("input-password"));

            // Check if buttons exist
            var loginButton = driver.FindElement(By.Id("login-button"));
            var createButton = driver.FindElement(By.Id("create-an-account-button"));
        }

        [TestMethod]
        public void CheckButtons()
        {
            // Check log in button
            driver.FindElement(By.Id("login-button")).Click();
            var errorMessage = driver.FindElement(By.Id("error-message")).Text;
            Assert.AreEqual("The email address is badly formatted.", errorMessage);
            
        }

        [TestMethod]
        public void CheckRedirection()
        {
            driver.FindElement(By.Id("create-an-account-button")).Click();
            string currentUrl = driver.Url;
            Assert.AreEqual("https://slonczynski.github.io/register.html", currentUrl);
            ;
        }

        [TestCleanup]
        public void TestEnd()
        {
            driver.Close();
        }

    }

}

