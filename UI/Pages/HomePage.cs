using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UI.Pages
{
    internal class HomePage : IPage
    {
        private  IWebDriver _webDriver;
        private  IWebElement _btnSignUp;
        private  IWebElement _btnCart;
        private  WebDriverWait _wait;
        private  IWebElement _txtUserName;
        private  IWebElement _txtPassword;
        private  IWebElement _btnSubmitSignUp;

        public IWebDriver webDriver { get { return _webDriver; } }
        public IWebElement btnSignUp { get { return _btnSignUp; } }
        public IWebElement btnCart { get { return _btnCart; } }
        public WebDriverWait wait { get { return _wait; } }
        public IWebElement btnSubmitSignUp { get { return _btnSubmitSignUp;  } }
        public IWebElement txtUserName { get { return _txtUserName;  } }
        public IWebElement txtPassword { get { return _txtPassword;  } }

        private HomePage() {}

        public class HomePageBuilder {

            private readonly HomePage homePage;
            public HomePageBuilder(IWebDriver webDriver) {
                homePage = new HomePage();
                homePage._webDriver = webDriver;
                homePage._wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                homePage._btnCart = IPage.WaitToFindElement(homePage.webDriver, homePage.wait ,By.CssSelector("#cartur"));
                homePage._btnSignUp = IPage.WaitToFindElement(homePage.webDriver, homePage.wait, By.CssSelector("#signin2"));
            }
            public HomePageBuilder WithUserNameField()
            {
                homePage._txtUserName = IPage.WaitToFindElement(homePage.webDriver, homePage.wait, By.CssSelector("#sign-username"));
                return this;
            }
            public HomePageBuilder WithPasswordField()
            {
                homePage._txtPassword = IPage.WaitToFindElement(homePage.webDriver, homePage.wait, By.CssSelector("#sign-password"));
                return this;
            }
            public HomePageBuilder WithSignUpSubmitButton()
            {
                homePage._btnSubmitSignUp = IPage.WaitToFindElement(homePage.webDriver, homePage.wait, By.XPath("//*[@id=\"signInModal\"]/div/div/div[3]/button[2]"));
                return this;
            }
            public HomePage build() { return homePage; }
        }

        public void SignUp(string userName, string password)
        {
            EnterUserNameAndPassword(userName, password);
        }

        private void EnterUserNameAndPassword(string userName, string password)
        {
            txtUserName.SendKeys(userName);
            txtPassword.SendKeys(password);
        }

        public bool RedeemTheAlert()
        {
            wait.Until(ExpectedConditions.AlertIsPresent());
            string alert = webDriver.SwitchTo().Alert().Text;
            if (!alert.Equals("Sign up successful."))
            {
                return false;
            }
            return true;
        }

        public void GoToProduct(string item) {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText(item)));
            IPage.WaitToFindElement(webDriver, wait, By.PartialLinkText(item)).Click();
        }
    }
}
