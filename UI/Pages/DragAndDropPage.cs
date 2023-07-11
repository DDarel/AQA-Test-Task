using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Pages
{
    internal class DragAndDropPage : IPage
    {
        private IWebDriver _webDriver;
        private WebDriverWait _wait;

        public IWebDriver webDriver { get { return _webDriver; } }
        public WebDriverWait wait { get { return _wait; } }

        private DragAndDropPage() { }

        public class DragAndDropPageBuilder
        {

            private readonly DragAndDropPage cartPage;
            public DragAndDropPageBuilder(IWebDriver webDriver)
            {
                cartPage = new DragAndDropPage();
                cartPage._webDriver = webDriver;
                cartPage._wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            }

            public DragAndDropPage build() { return cartPage; }
        }

        public void MovePhotos(int quant) {
            Actions action = new Actions(webDriver);
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//*[@id=\"post-2669\"]//*[@class=\"resp-tabs-container\"]/div[1]/p/iframe")));
            IWebElement drag = IPage.WaitToFindElement(webDriver, wait, By.XPath("//*[@id=\"gallery\"]/li[1]"));
            IWebElement drop = IPage.WaitToFindElement(webDriver, wait, By.XPath("//*[@id=\"trash\"]"));
            for (int count = 0; count < quant; count++) {
                action.DragAndDrop(drag, drop).Build().Perform();
                drag = IPage.WaitToFindElement(webDriver, wait, By.XPath("//*[@id=\"gallery\"]/li[1]"));
                action.Pause(new TimeSpan(10));
            } 
        }

        public bool CheckPhotoIsMoved(int quant) {
             IWebElement element = IPage.WaitToFindElement(webDriver, wait, By.XPath("//*[@id=\"trash\"]/ul/li[" + quant + "]"));
            if (element == null)
            {
                return false;
            }
            return true;
        }
    }
}
