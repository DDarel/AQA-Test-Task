using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace UI.Pages
{
    internal class CartPage : IPage
    {
        private IWebDriver _webDriver;
        private IWebElement _btnSignUp;
        private IWebElement _btnCart;
        private WebDriverWait _wait;
        private IWebElement _btnDelete;
        private IWebElement _btnPlaceOrder;
        private IWebElement _textName;
        private IWebElement _textCountry;
        private IWebElement _textCity;
        private IWebElement _textCreditCard;
        private IWebElement _textMonth;
        private IWebElement _textYear;
        private IWebElement _btnPurchase;

        public IWebDriver webDriver { get { return _webDriver; } }
        public IWebElement btnSignUp { get { return _btnSignUp; } }
        public IWebElement btnCart { get { return _btnCart; } }
        public WebDriverWait wait { get { return _wait; } }

        public IWebElement btnDelete { get { return _btnDelete; } }
        public IWebElement btnPlaceOrder { get { return _btnPlaceOrder; } }
        public IWebElement textName { get { return _textName; } }
        public IWebElement textCountry { get { return _textCountry; } }
        public IWebElement textCity { get { return _textCity; } }
        public IWebElement textCreditCard { get { return _textCreditCard; } }
        public IWebElement textMonth { get { return _textMonth; } }
        public IWebElement textYear { get { return _textYear; } }
        public IWebElement btnPurchase { get { return _btnPurchase; } }

        private CartPage() { }

        public class CartPageBuilder
        {

            private readonly CartPage cartPage;
            public CartPageBuilder(IWebDriver webDriver)
            {
                cartPage = new CartPage();
                cartPage._webDriver = webDriver;
                cartPage._wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                cartPage._btnCart = IPage.WaitToFindElement(cartPage.webDriver, cartPage.wait, By.XPath("//*[@id=\"navbarExample\"]/ul/li[4]/a"));
                cartPage._btnSignUp = IPage.WaitToFindElement(cartPage.webDriver, cartPage.wait, By.CssSelector("#signin2"));
            }
 
            public CartPageBuilder WithDeleteButton(string item)
            {
                cartPage._btnDelete = IPage.WaitToFindElement(cartPage.webDriver, cartPage.wait, By.XPath("//*[@class=\"success\"]//td[text()[contains(.,\'" + item + "\')]]/..//td[4]/a"));
                return this;
            }
            public CartPageBuilder WithPlaceOrderButton()
            {
                cartPage._btnPlaceOrder = IPage.WaitToFindElement(cartPage.webDriver, cartPage.wait, By.XPath("//*[@id=\"page-wrapper\"]//button[text()[contains(.,'Place Order')]]"));
                return this;
            }
            public CartPageBuilder WithName()
            {
                cartPage._textName = IPage.WaitToFindElement(cartPage.webDriver, cartPage.wait, By.XPath("//*[@id=\"name\"]"));
                return this;
            }
            public CartPageBuilder WithCountry()
            {
                cartPage._textCountry = IPage.WaitToFindElement(cartPage.webDriver, cartPage.wait, By.XPath("//*[@id=\"country\"]"));
                return this;
            }
            public CartPageBuilder WithCity()
            {
                cartPage._textCity = IPage.WaitToFindElement(cartPage.webDriver, cartPage.wait, By.XPath("//*[@id=\"city\"]"));
                return this;
            }
            public CartPageBuilder WithCreditCard()
            {
                cartPage._textCreditCard = IPage.WaitToFindElement(cartPage.webDriver, cartPage.wait, By.XPath("//*[@id=\"card\"]"));
                return this;
            }
            public CartPageBuilder WithMonth()
            {
                cartPage._textMonth = IPage.WaitToFindElement(cartPage.webDriver, cartPage.wait, By.XPath("//*[@id=\"month\"]"));
                return this;
            }
            public CartPageBuilder WithYear()
            {
                cartPage._textYear = IPage.WaitToFindElement(cartPage.webDriver, cartPage.wait, By.XPath("//*[@id=\"year\"]"));
                return this;
            }
            public CartPageBuilder WithPurchaseButton()
            {
                cartPage._btnPurchase = IPage.WaitToFindElement(cartPage.webDriver, cartPage.wait, By.XPath("//*[@id=\"orderModal\"]//button[2]"));
                return this;
            }

            public CartPage build() { return cartPage; }
        }
        public bool CheckItemIsDeleted(string item) {
            IWebElement _element = webDriver.FindElement(By.XPath("//*[@class=\"success\"]//td[text()[contains(.,\'" + item + "\')]]/..//td[4]/a"));
            if (_element != null)
            {
                return false;
            }
            return true;
        }

        public void EnterUserInfo(string name, string country, string city, int creditCard, int month, int year) {
            textName.SendKeys(name);
            textCountry.SendKeys(country);
            textCity.SendKeys(city);
            textCreditCard.SendKeys(creditCard.ToString());    
            textMonth.SendKeys(month.ToString());
            textYear.SendKeys(year.ToString());
        }

        public bool CheckOrderPlaced() { 
            IWebElement element  = IPage.WaitToFindElement(webDriver, wait, By.XPath("//h2[text()[contains(.,'Thank you for your purchase!')]]"));
            if (element != null)
            {
                return true;
            }
            return false;
        }
    }
}
