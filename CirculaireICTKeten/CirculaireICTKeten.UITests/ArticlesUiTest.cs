using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirculaireICTKeten.UITests
{
    [TestClass]
    public class AutomatedUITests : IDisposable
    {
        private readonly IWebDriver chromeDriver;
        public AutomatedUITests()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            chromeDriver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions);
        }
        public void Dispose()
        {
            chromeDriver.Quit();
            chromeDriver.Dispose();
        }

        [TestMethod()]
        public void ArticleSearch()
        {
            chromeDriver.Navigate()
                .GoToUrl("https://test-ruilwinkel-vaals.azurewebsites.net/Articles");
            chromeDriver.FindElement(By.Id("searchField")).SendKeys("Stoel");
            chromeDriver.FindElement(By.Id("searchButton")).Click();
            //WebDriverWait wait = new WebDriverWait(chromeDriver, new System.TimeSpan(0, 1, 0));
            //var text = chromeDriver.FindElement(By.ClassName("grid-container"));
            //Assert.IsTrue(text.Text.Contains("Stoel"));
        }

        [TestMethod()]
        public void ArticleSort()
        {
            chromeDriver.Navigate()
                .GoToUrl("https://test-ruilwinkel-vaals.azurewebsites.net/Articles");
            chromeDriver.FindElement(By.Id("sort-point")).Click();
            chromeDriver.FindElement(By.Id("sort-category")).Click();
            chromeDriver.FindElement(By.Id("sort-name")).Click();
        }
    }
}