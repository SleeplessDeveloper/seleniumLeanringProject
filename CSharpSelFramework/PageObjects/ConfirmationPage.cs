using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelFramework.PageObjects
{
    public class ConfirmationPage
    {
        IWebDriver _driver;
        WebDriverWait _wait; 
        public ConfirmationPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(5));
            _wait = wait;
        }
        [FindsBy(How = How.Id, Using = "country")]
        private readonly IWebElement country;
        [FindsBy(How = How.LinkText, Using ="India")]
        private readonly IWebElement selectCountry;
        [FindsBy(How = How.CssSelector, Using = "label[for*='checkbox2']")]
        private readonly IWebElement checkBox;
        [FindsBy(How = How.CssSelector, Using = "[value='Purchase']")]
        private readonly IWebElement purchaseButton;
        [FindsBy(How = How.CssSelector, Using = ".alert-success")]
        private readonly IWebElement alert;

        public void GetCountry(string getCountry)
        {
            country.SendKeys(getCountry);
        }
        public void WaitForAutoSuggestiveDropDown()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
               .ElementIsVisible(By.LinkText("India")));
        }
        public void SelectCountry()
        {
            selectCountry.Click();
            checkBox.Click();
            purchaseButton.Click();
            
        }

        public string GetAlertText()
        {
            return alert.Text;
        }
        public void WaitForAlert()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementExists(By.CssSelector(".alert-success")));
        }
    }
}
