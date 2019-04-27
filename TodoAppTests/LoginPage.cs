using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TodoAppTests
{

    [TestClass]
    [TestCategory("Logging in to website")]
    public class LoginPage
    {

        private IWebDriver _driver;

        [TestInitialize]
        public void GetChromeDriver()

        {
            // Get Chrome driver location
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // Runs headless Chrome
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--headless");
            _driver = new ChromeDriver(outPutDirectory, option);
            // Navigate to a website
            _driver.Navigate().GoToUrl("https://slonczynski.github.io");
        }

        [TestCleanup]
        public void Teardown()
        {
            _driver.Close();
            _driver.Quit();
        }

        [TestMethod]
        public void CheckElementsExistence()
        {

            // Check welcome message
            var signText = _driver.FindElement(By.ClassName("sign-text")).Text;
            Assert.AreEqual("Sign in:", signText);

            // Check number of inputs
            var inputs = _driver.FindElements(By.ClassName("form-control")).Count;
            Assert.AreEqual(2, inputs);


            // **Check if inputs exist**

            // Email input
            _driver.FindElement(By.Id("input-email"));
            // Password input
            _driver.FindElement(By.Id("input-password"));


            // **Check if buttons exist**

            // Login button
            _driver.FindElement(By.Id("login-button"));
            //Create account button
            _driver.FindElement(By.Id("create-an-account-button"));
        }

        [TestMethod]
        public void CheckButtons()
        {
            // Check log in button
            _driver.FindElement(By.Id("login-button")).Click();
            var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
            Assert.AreEqual("The email address is badly formatted.", errorMessage);
        }

        [TestMethod]
        public void CheckRedirection()
        {
            _driver.FindElement(By.Id("create-an-account-button")).Click();
            var currentUrl = _driver.Url;
            Assert.AreEqual("https://slonczynski.github.io/register.html", currentUrl);
        }

        [TestMethod]
        public void LoginTest()
        {
            // Fill inputs with credentials
            FillOutCredentials();

            // Click login button
            _driver.FindElement(By.Id("login-button")).Click();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //Check if spinner exists

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("spinner-border"))).Displayed);

        }

        private void FillOutCredentials()
        {
            _driver.FindElement(By.Id("input-email")).SendKeys("automated@testing.selenium");
            _driver.FindElement(By.Id("input-password")).SendKeys("123456");
        }

    }
}