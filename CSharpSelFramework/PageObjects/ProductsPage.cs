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
    public class ProductsPage
    {
        IWebDriver _driver;
        WebDriverWait _wait;
        By cardTitle = By.CssSelector(".card-title a");
        By addToCart = By.CssSelector(".card-footer button");


        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(8));
            _wait = wait;
        }
        [FindsBy(How = How.TagName, Using = "app-card")]
        private readonly IList<IWebElement> cards;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private readonly IWebElement checkoutButton;

        public void waitForCheckOutDisplay()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        }


        public IList<IWebElement> getCards()
        {
            return cards;
        }

        public By GetCardTitle()
        {
            return cardTitle;
        }

        public By AddToCart()
        {
            return addToCart;
        }

        public CheckoutPage checkout()
        {
            checkoutButton.Click();
            return new CheckoutPage(_driver);
        }
    }
}
