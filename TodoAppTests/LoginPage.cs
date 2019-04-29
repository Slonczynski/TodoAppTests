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
                //ChromeOptions option = new ChromeOptions();
                //option.AddArguments("--headless", "--enable-features=NetworkService");
                _driver = new ChromeDriver(outPutDirectory/*, option*/);
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
            public void TestCase1And2()
            {

                // Check welcome message
                var signText = _driver.FindElement(By.ClassName("sign-text")).Text;
                Assert.AreEqual("Sign in:", signText);

                // Check number of inputs
                var inputs = _driver.FindElements(By.ClassName("form-control")).Count;
                Assert.AreEqual(2, inputs);

                // Check redirection text
                var redirectionText = _driver.FindElement(By.Id("submit-button")).Text;
                Assert.AreEqual("New user?", redirectionText);


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
            public void TestCase3()
            {
                // Click log in button
                _driver.FindElement(By.Id("login-button")).Click();
                

                // Check error message
                var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
                Assert.AreEqual("The email address is badly formatted.", errorMessage);
            }

            [TestMethod]
            public void TestCase4()
            {
                // Fill inputs
                _driver.FindElement(By.Id("input-email")).SendKeys("automated@testing.selenium");
                _driver.FindElement(By.Id("input-password")).SendKeys("123456");

                // Click login button
                _driver.FindElement(By.Id("login-button")).Click();

                //Check if spinner works

                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("spinning-button"))).Displayed);

                // Check redirection
                Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("logout-button"))).Displayed);
                var currentUrl = _driver.Url;
                Assert.AreEqual("https://slonczynski.github.io/todo.html", currentUrl);
            }

            [TestMethod]
            public void TestCase5()
            {
                // Fill inputs
                _driver.FindElement(By.Id("input-email")).SendKeys("automated@testing.selenium");
                _driver.FindElement(By.Id("input-password")).SendKeys("");

                // Submit
                _driver.FindElement(By.Id("login-button")).Click();

                //Check error message
                var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
                Assert.AreEqual("The password is invalid or the user does not have a password.", errorMessage);
            }

            [TestMethod]
            public void TestCase6()
            {
                // Fill inputs
                _driver.FindElement(By.Id("input-email")).SendKeys("");
                _driver.FindElement(By.Id("input-password")).SendKeys("123456");

                // Submit
                _driver.FindElement(By.Id("login-button")).Click();

                //Check error message
                var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
                Assert.AreEqual("The email address is badly formatted.", errorMessage);
            }

            [TestMethod]
            public void TestCase7()
            {
                _driver.FindElement(By.Id("input-email")).SendKeys("abvasdas");
                _driver.FindElement(By.Id("input-password")).SendKeys("");

                // Submit
                _driver.FindElement(By.Id("login-button")).Click();

                //Check error message
                var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
                Assert.AreEqual("The email address is badly formatted.", errorMessage);
            }

            [TestMethod]
            public void TestCase8()
            {
                _driver.FindElement(By.Id("input-email")).SendKeys("automated@testing.selenium");
                _driver.FindElement(By.Id("input-password")).SendKeys("1");

                // Submit
                _driver.FindElement(By.Id("login-button")).Click();

                //Check error message
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message"))).Displayed);
                var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
                Assert.AreEqual("The password is invalid or the user does not have a password.", errorMessage);
            }

            [TestMethod]
            public void TestCase9()
            {
                _driver.FindElement(By.Id("input-email")).SendKeys("notworkingemail@testing.selenium");
                _driver.FindElement(By.Id("input-password")).SendKeys("12");

                // Submit
                _driver.FindElement(By.Id("login-button")).Click();

                //Check error message
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                Assert.IsTrue(wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error-message"))).Displayed);
                var errorMessage = _driver.FindElement(By.Id("error-message")).Text;
                Assert.AreEqual("There is no user record corresponding to this identifier. The user may have been deleted.", errorMessage);
            }

            [TestMethod]
            public void TestCase10()
            {
            _driver.FindElement(By.Id("create-an-account-button")).Click();
            var currentUrl = _driver.Url;
            Assert.AreEqual("https://slonczynski.github.io/register.html", currentUrl);
            }




    }
}