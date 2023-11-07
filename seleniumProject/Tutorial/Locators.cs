using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace seleniumProject.Tutorial
{
    public class Locators
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            //implicit wait  declared globally
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }
        [Test]
        public void LocatorsIdentifiers()
        {
            driver.FindElement(By.Id("username")).SendKeys("ragnarok");
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("Odinson");
            driver.FindElement(By.Id("password")).SendKeys("Password");
            //css selector & Xpath

            //tagname[attribute='value']
            // #id #terms - class name -> css .classname
            //driver.FindElement(By.CssSelector("input[value='Sign In'")).Click();

            //tagname[@attribute='value']

            //css - .text-info span:nth-child(1) input
            //xpath selectorhub constructed link
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            //xpath copied link
            //driver.FindElement(By.XPath("//*[@id=\"terms\"]")).Click();

            //full xpath copied link
            //driver.FindElement(By.XPath("/html/body/div[1]/div/div/div/div/form/div[6]/label/span[1]/input")).Click();

            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(driver.FindElement(By.Id("signInBtn")), "Sign In"));

            string errMsg = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errMsg);

            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            string hrefAttr = link.GetAttribute("href");
            string expectedURL = "https://rahulshettyacademy.com/documents-request";
            /*Assert.AreEqual(expectedURL, hrefAttr);*/
            Assert.That(hrefAttr, Is.EqualTo(expectedURL));

            //validate url of the link text
        }
        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
