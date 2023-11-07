using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports.Model;

namespace CSharpSelFramework.Utilities
{
    public class Base
    {
        public ExtentReports extent;
        ExtentTest test;
        //report file
        [OneTimeSetUp]
        public void Setup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory)!.Parent!.Parent!.FullName;
            string reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Rahul Shetty");

        }
        //public IWebDriver driver;
        ThreadLocal <IWebDriver> driver = new ThreadLocal<IWebDriver>();
        string browserName = null!;
        [SetUp]
        public void StartBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            //configuration
            browserName = TestContext.Parameters["browser"]!;
            if (browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"]!;
            }
            
            InitBrowser(browserName);
            //implicit wait  declared globally
            driver.Value!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value!.Manage().Window.Maximize();
            driver.Value!.Url = "https://rahulshettyacademy.com/loginpagePractise/";

        }

        public IWebDriver getDriver()
        {
            return driver.Value!;
        }

        public void InitBrowser(string browserName)
        {
            switch(browserName)
            {
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;
            }
        }

        public static JReader getDataParser()
        {
            return new JReader();
        }
        [TearDown]
        public void CloseDriver()
        {
            driver.Value!.Close();
            driver.Value!.Dispose();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            if(status == TestStatus.Failed)
            {
                test.Fail("Test failed", CaptureScreenshot(driver.Value!,fileName));
                test.Log(Status.Fail, $"test failed with logtrace {stackTrace}");
            }else if(status == TestStatus.Passed)
            {

            }
            extent.Flush();
            driver.Value!.Quit();
            driver.Dispose();
        }
        public MediaEntityModelProvider CaptureScreenshot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot,screenshotName).Build();
        }
    }
}
