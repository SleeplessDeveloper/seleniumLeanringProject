using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelFramework.PageObjects
{
    public class LogInPage
    {
        private IWebDriver _driver;
        public LogInPage(IWebDriver driver)
        {    
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "username")]
        private readonly IWebElement username;

        [FindsBy(How = How.Id, Using ="password")]
        private readonly IWebElement password;
        
        [FindsBy(How = How.XPath, Using = "//div[@class='form-group'][5]/label/span/input")]
        private readonly IWebElement checkBox;

        [FindsBy(How = How.XPath, Using = "//input[@value='Sign In']")]
        private readonly IWebElement signIn;

        public ProductsPage ValidLogIn(string lUsername, string lPassword)
        {
            username.SendKeys(lUsername);
            password.SendKeys(lPassword);
            checkBox.Click();
            signIn.Click();

            return new ProductsPage(_driver);
        }
    }
}
