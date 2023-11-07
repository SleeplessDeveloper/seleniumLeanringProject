using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace seleniumProject.Tutorial
{
    public class WindowHandler
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            //implicit wait  declared globally
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }
        [Test] public void WindowHandle()
        {
            String email = "mentor@rahulshettyacademy.com";
            String parentWindowId = driver.CurrentWindowHandle;
            driver.FindElement(By.ClassName("blinkingText")).Click();

            Assert.That(driver.WindowHandles.Count, Is.EqualTo(2));

            String childWindowHandle = driver.WindowHandles[1];

            driver.SwitchTo().Window(childWindowHandle);

            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);
            String text = driver.FindElement(By.CssSelector(".red")).Text;

            //split text
            String[] splittedText = text.Split("at");

            String[] trimmedString = splittedText[1].Trim().Split(" ");
            Assert.That(trimmedString[0], Is.EqualTo(email));

            driver.SwitchTo().Window(parentWindowId);

            driver.FindElement(By.Id("username")).SendKeys(trimmedString[0]);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}
