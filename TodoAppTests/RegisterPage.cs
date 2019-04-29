using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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
            // Runs headless Chrome; issues with redirection while using headless
            //ChromeOptions option = new ChromeOptions();
            //option.AddArgument("--headless");
            _driver = new ChromeDriver(outPutDirectory/*, option*/);
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
        public void TestCase11And12()
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

            // Sign up button
            _driver.FindElement(By.Id("sign-up-button"));
            //Create account button
            _driver.FindElement(By.Id("register-button"));

        }

        [TestMethod]
        public void TestCase13()
        {
            _driver.FindElement(By.Id("sign-up-button")).Click();
            var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
            Assert.AreEqual("The email address is badly formatted.", errorMessage);
        }

        [TestMethod]
        public void TestCase14()
        {
            // Enter email
            _driver.FindElement(By.Id("input-email")).SendKeys("automated@testing.selenium");

            // Click sign up
            _driver.FindElement(By.Id("sign-up-button")).Click();

            // Check error
            var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
            Assert.AreEqual("The password must be 6 characters long or more.", errorMessage);
        }

        [TestMethod]
        public void TestCase15()
        {
            // Enter email
            _driver.FindElement(By.Id("input-email")).SendKeys("automated@testing.selenium");

            // Enter passwords
            _driver.FindElement(By.Id("input-password")).SendKeys("123456");
            _driver.FindElement(By.Id("input-repeat-password")).SendKeys("abcdef");


            // Click sign up
            _driver.FindElement(By.Id("sign-up-button")).Click();

            // Check error
            var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
            Assert.AreEqual("Those passwords didn't match. Try again.", errorMessage);
        }

        [TestMethod]
        public void TestCase16()
        {
            // Enter email
            _driver.FindElement(By.Id("input-email")).SendKeys("bademailformat");

            // Enter passwords
            _driver.FindElement(By.Id("input-password")).SendKeys("123456");
            _driver.FindElement(By.Id("input-repeat-password")).SendKeys("123456");


            // Click sign up
            _driver.FindElement(By.Id("sign-up-button")).Click();

            // Check error
            var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
            Assert.AreEqual("The email address is badly formatted.", errorMessage);
        }

        [TestMethod]
        public void TestCase17()
        {
            // Enter email
            _driver.FindElement(By.Id("input-email")).SendKeys(RandomEmailGenerator());

            // Enter passwords
            _driver.FindElement(By.Id("input-password")).SendKeys("123");
            _driver.FindElement(By.Id("input-repeat-password")).SendKeys("123");


            // Click sign up
            _driver.FindElement(By.Id("sign-up-button")).Click();

            // Check error
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message"))).Displayed);
            var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
            Assert.AreEqual("Password should be at least 6 characters", errorMessage);
        }

        [TestMethod]
        public void TestCase18()
        {
            // Enter email
            _driver.FindElement(By.Id("input-email")).SendKeys(RandomEmailGenerator());


            // Click sign up
            _driver.FindElement(By.Id("sign-up-button")).Click();

            // Check error
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message"))).Displayed);
            var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
            Assert.AreEqual("The password must be 6 characters long or more.", errorMessage);
        }

        [TestMethod]
        public void TestCase19()
        {
            // Enter email
            _driver.FindElement(By.Id("input-email")).SendKeys(RandomEmailGenerator());


            // Enter passwords
            _driver.FindElement(By.Id("input-password")).SendKeys("123456");
            _driver.FindElement(By.Id("input-repeat-password")).SendKeys("123456");

            // Click sign up
            _driver.FindElement(By.Id("sign-up-button")).Click();

            // Check redirection
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("logout-button"))).Displayed);

            var currentUrl = _driver.Url;
            Assert.AreEqual("https://slonczynski.github.io/todo.html", currentUrl);
        }


        public string RandomEmailGenerator()
        {
            var rnd = new Random();
            var randomEmail = "random_email" + rnd.Next(1, 9000) + "@wqeqw" + rnd.Next(1, 9000) + ".owwwawda";
            return randomEmail;
        }
    }
}
