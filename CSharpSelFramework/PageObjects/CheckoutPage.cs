using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelFramework.PageObjects
{
    public class CheckoutPage
    {
        IWebDriver _driver;
        public CheckoutPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> checkoutCards;

        [FindsBy(How = How.CssSelector, Using = ".btn-success")]
        private readonly IWebElement checkoutButton;

        public IList<IWebElement> GetCards()
        {
            return checkoutCards;
        }
        public ConfirmationPage CheckoutButton()
        {
           checkoutButton.Click();
           return new ConfirmationPage(_driver);
        }
    }
}
