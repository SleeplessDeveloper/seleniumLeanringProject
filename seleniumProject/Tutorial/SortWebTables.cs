using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace seleniumProject.Tutorial
{
    public class SortWebTables
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
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";

        }
        [Test]
        public void SortTable()
        {
            ArrayList a = new ArrayList();
            SelectElement dropDown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            dropDown.SelectByText("20");

            // step 1 - Get all veggie names into arraylist A
            IList<IWebElement> veggies = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach(IWebElement veggie in veggies)
            {
                a.Add(veggie.Text);
            }

            // step 2 - Sort this arraylist -A
            foreach (String element in a)
            {
                TestContext.Progress.WriteLine(element);
            }
            TestContext.Progress.WriteLine("\nAfter sorting\n");

            a.Sort();

            foreach( String element in a)
            {
                TestContext.Progress.WriteLine(element);
            }

            //step 3 - go and click column
            driver.FindElement(By.CssSelector("th[aria-label *= 'fruit name']")).Click();

            // step 4 - Get all veggie names into arraylist B
            ArrayList b = new ArrayList();
            IList<IWebElement> Sortedveggies = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach (IWebElement veggie in Sortedveggies)
            {
                b.Add(veggie.Text);
            }

            // arraylist A to B = equal
            //Assert.AreEqual(a, b);
            Assert.That(a, Is.EqualTo(b));
        }
        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
