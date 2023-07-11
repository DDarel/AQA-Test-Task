using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Infrastructure;
using UI.Pages;
using static UI.Pages.CartPage;
using static UI.Pages.HomePage;
using static UI.Pages.ProductPage;

namespace UI.StepDefinitions
{
    [Binding]
    public class DemoblazeTestingStepDefinitions
    {
        IWebDriver webDriver = new EdgeDriver();
        //IWebDriver webDriver = new ChromeDriver();
       
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        public DemoblazeTestingStepDefinitions(ISpecFlowOutputHelper specFlowOutputHelper) {
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        [Given(@"User open home page")]
        public void GivenUserOpenSite()
        {
            webDriver.Navigate().GoToUrl("https://www.demoblaze.com/index.html");
        }

        [When(@"User signing up")]
        public void WhenUserSigningUp(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            HomePage home = new HomePageBuilder(webDriver).build();
            home.btnSignUp.Click();
            home = new HomePageBuilder(webDriver).WithUserNameField().WithPasswordField().WithSignUpSubmitButton().build();
            home.SignUp((string)data.UserName, (string)data.Password);
            home.btnSubmitSignUp.Click();
        }

        [Then(@"User succesfully signed up")]
        public void ThenUserSignedUp()
        {
            HomePage home = new HomePageBuilder(webDriver).build();
            if (!home.RedeemTheAlert()) {
                throw new Exception($"This user already exist."); ;
            }
        }

        [Then(@"User didn't sign up")]
        public void ThenUserDidntSignUp()
        {
            HomePage home = new HomePageBuilder(webDriver).build();
            if (home.RedeemTheAlert())
            {
                throw new Exception($"User succesfully signed up.");
            }
        }

        [Given(@"User open (.*) item page")]
        public void GivenUserOpenProductPage(string item)
        {
            webDriver.Navigate().GoToUrl("https://www.demoblaze.com/index.html");
            HomePage home = new HomePageBuilder(webDriver).build();
            home.GoToProduct(item);
        }

        [When(@"User click on Add to cart button")]
        public void WhenUserClickOnAddToCartButton()
        {
            ProductPage productPage = new ProductPageBuilder(webDriver).build();
            productPage.btnAddToCart.Click();
        }

        [Then(@"Item successfully added")]
        public void ThenItemSuccesfullyAdded()
        {
            ProductPage productPage = new ProductPageBuilder(webDriver).build();
            productPage.RedeemTheAlert();
        }

        [Given(@"User open cart page")]
        public void GivenUserOpenCartPage()
        {
            webDriver.Navigate().GoToUrl("https://www.demoblaze.com/cart.html"); 
        }

        [Given(@"User added (.*) to cart")]
        public void GivenUserAddedSamsungGalaxySToCart(string item)
        {
            GivenUserOpenProductPage(item);
            WhenUserClickOnAddToCartButton();
        }

        [When(@"User click on delete (.*) button")]
        public void WhenUserClickOnDeleteButton(string item)
        {
            CartPage cartPage = new CartPageBuilder(webDriver).WithDeleteButton(item).build();
            cartPage.btnDelete.Click();
        }

        [Then(@"(.*) successfully deleted")]
        public void ThenItemSuccesfullyDeleted(string item)
        {
            CartPage cartPage = new CartPageBuilder(webDriver).build();
            if (cartPage.CheckItemIsDeleted(item))
            {
                throw new Exception($"Item was not deleted.");
            }
        }

        [When(@"User click on Place order button")]
        public void WhenUserClickOnPlaceOrderButton()
        {
            CartPage cartPage = new CartPageBuilder(webDriver).WithPlaceOrderButton().build();
            cartPage.btnPlaceOrder.Click();
        }

        [When(@"User fill order info")]
        public void WhenUserFillOrderInfo(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            CartPage cartPage = new CartPageBuilder(webDriver).WithName().WithCountry().WithCity().WithCreditCard().WithMonth().WithYear().WithPurchaseButton().build();
            cartPage.EnterUserInfo((string)data.Name, (string)data.Country, (string)data.City, (int)data.CreditCard, (int)data.Month, (int)data.Year);
            cartPage.btnPurchase.Click();
        }


        [Then(@"Order successfully placed")]
        public void ThenOrderSuccessfullyPlaced()
        {
            CartPage cartPage = new CartPageBuilder(webDriver).build();
            if (!cartPage.CheckOrderPlaced()) {
                throw new Exception($"Order was not placed.");
            }
        }

    }
}