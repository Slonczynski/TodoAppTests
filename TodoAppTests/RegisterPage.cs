using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TodoAppTests
{

    [TestClass]
    [TestCategory("Creating new account")]
    public class RegisterPage
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
            _driver.Navigate().GoToUrl("https://slonczynski.github.io/register.html");
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
            Assert.AreEqual("Sign up:", signText);

            // Check number of inputs
            var inputs = _driver.FindElements(By.ClassName("form-control")).Count;
            Assert.AreEqual(3, inputs);

            // Check redirection text
            var redirectionText = _driver.FindElement(By.Id("register-button")).Text;
            Assert.AreEqual("Login here", redirectionText);


            // **Check if inputs exist**

            // Email input
            _driver.FindElement(By.Id("input-email"));
            // Password input
            _driver.FindElement(By.Id("input-password"));
            // Repeat password input
            _driver.FindElement(By.Id("input-repeat-password"));

            // **Check if buttons exist**

            // Login button
            _driver.FindElement(By.Id("sign-up-button"));
            //Create account button
            _driver.FindElement(By.Id("register-button"));
        
        }
        [TestMethod]
        public void CheckButtons()
        {
            // Check log in button
            _driver.FindElement(By.Id("register-button")).Click();
            var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
            Assert.AreEqual("The email address is badly formatted.", errorMessage);
        }
    }
}
