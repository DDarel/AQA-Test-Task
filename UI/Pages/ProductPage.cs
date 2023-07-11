using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UI.Pages
{
    internal class ProductPage : IPage
    {
        private IWebDriver _webDriver;
        private IWebElement _btnSignUp;
        private IWebElement _btnCart;
        private WebDriverWait _wait;
        private IWebElement _btnAddToCart;

        public IWebDriver webDriver { get { return _webDriver; } }
        public IWebElement btnSignUp { get { return _btnSignUp; } }
        public IWebElement btnCart { get { return _btnCart; } }
        public WebDriverWait wait { get { return _wait; } }
        public IWebElement btnAddToCart { get { return _btnAddToCart; } }

        private ProductPage() { }

        public class ProductPageBuilder
        {
            private readonly ProductPage productPage;
            public ProductPageBuilder(IWebDriver webDriver)
            {
                productPage = new ProductPage();
                productPage._webDriver = webDriver;
                productPage._wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                productPage._btnCart = IPage.WaitToFindElement(productPage.webDriver, productPage.wait, By.CssSelector("#cartur"));
                productPage._btnSignUp = IPage.WaitToFindElement(productPage.webDriver, productPage.wait, By.CssSelector("#signin2"));
                productPage._btnAddToCart = IPage.WaitToFindElement(productPage.webDriver, productPage.wait, By.XPath("//*[@id=\"tbodyid\"]/div[2]/div/a"));
            }
            public ProductPage build() { return productPage; }
        }

        public bool RedeemTheAlert()
        {
            wait.Until(ExpectedConditions.AlertIsPresent());
            string alert = webDriver.SwitchTo().Alert().Text;
            if (!alert.Equals("Product added"))
            {
                return false;
            }
            return true;
        }
    }
}
