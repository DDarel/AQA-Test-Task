using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace UI.Pages
{
    internal interface IPage
    {
        IWebDriver webDriver { get; }
        WebDriverWait wait { get; }
        static IWebElement WaitToFindElement(IWebDriver driver, WebDriverWait _wait, By element)
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(element));
                IWebElement _element = driver.FindElement(element);
                return _element;
            }
            catch
            {
                throw new Exception($"The page wasn't loaded");
            }
        }
    }
}
