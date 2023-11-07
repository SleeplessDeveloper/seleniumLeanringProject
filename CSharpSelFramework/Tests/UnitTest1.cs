using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using CSharpSelFramework.Utilities;
using CSharpSelFramework.PageObjects;
using NUnit.Framework;

namespace CSharpSelFramework.Tests
{
    public class Tests : Base
    {

        [Test, TestCaseSource(nameof(AddTestDataConfig)), Category("smoke")]
        //[TestCase("rahulshettyacademy", "learning")]
        [Parallelizable(ParallelScope.All)]
        public void EndToEndTest(string username, string password, string[] expectedProducts)
        {
            string[] actualProducts = new string[2];
            LogInPage login = new LogInPage(getDriver());
            ProductsPage productsPage = login.ValidLogIn(username,password);
            productsPage.waitForCheckOutDisplay();
            
            IList<IWebElement> products = productsPage.getCards();

            foreach (IWebElement product in products)
            {
                if (expectedProducts.Contains(product.FindElement(productsPage.GetCardTitle()).Text))
                {
                    product.FindElement(productsPage.AddToCart()).Click();
                    //click on cart
                }
                TestContext.Progress.WriteLine(product.FindElement(productsPage.GetCardTitle()).Text);
            }
            CheckoutPage checkoutPage = productsPage.checkout();

            IList<IWebElement> checkoutCards = checkoutPage.GetCards();
            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }
            Assert.That(actualProducts, Is.EqualTo(expectedProducts));

            ConfirmationPage confirmationPage = checkoutPage.CheckoutButton();

            confirmationPage.GetCountry("ind");

            confirmationPage.WaitForAutoSuggestiveDropDown();

            confirmationPage.SelectCountry();

            confirmationPage.WaitForAlert();

            string confirmText = confirmationPage.GetAlertText();

            StringAssert.Contains("Success", confirmText);
        }

        private static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username_wrong"), getDataParser().extractData("password_wrong"),getDataParser().extractDataArray("products"));
        }

    }
}