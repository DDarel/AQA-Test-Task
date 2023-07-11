using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;
using UI.Pages;
using static UI.Pages.DragAndDropPage;

namespace UI.StepDefinitions
{
    [Binding]
    public class DragAndDropTestingStepDefinitions
    {

        IWebDriver webDriver = new ChromeDriver();

        [Given(@"User open DragAndDrop site")]
        public void GivenUserOpenDragAndDropSite()
        {
            webDriver.Navigate().GoToUrl("https://www.globalsqa.com/demo-site/draganddrop/#Photo%20Manager");
           // webDriver.Navigate().GoToUrl("https://jqueryui.com/droppable/");
        }

        [When(@"User Drag (.*) photo")]
        public void WhenUserDragPhoto(int quantity)
        {
            DragAndDropPage page = new DragAndDropPageBuilder(webDriver).build();
            page.MovePhotos(quantity);
        }

        [Then(@"User check drag (.*) photos success")]
        public void ThenUserCheckDragSuccess(int quantity)
        {
            DragAndDropPage page = new DragAndDropPageBuilder(webDriver).build();
            if (!page.CheckPhotoIsMoved(quantity)) {
                throw new Exception($"Less than" + quantity + " was moved");
            }
        }
    }
}
