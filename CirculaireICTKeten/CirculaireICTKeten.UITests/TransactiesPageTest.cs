using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CirculaireICTKetenUITESTS
{
    [TestClass]
    public class TransactiesPageTest
    {
        private readonly Random _random = new Random();
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        IWebDriver webDriver;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize edge driver
            ChromeOptions option = new ChromeOptions();
            option.AddArguments("--headless");
            webDriver = new ChromeDriver(option);
            webDriver.Url = "https://test-ruilwinkel-vaals.azurewebsites.net/Transactie";
        }

        [TestMethod]

        //Check the Page Title of the Transactie Page
        public void VerifyPageTitle()
        {
            Assert.IsTrue(webDriver.Title.Contains("- CirculaireICTKeten"), "Verified title of the page");
        }

        [TestMethod]

        //Check the name of the Transactie Table
        public void checkTableName()
        {
            string tablename = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div/h2")).Text;
            Assert.IsTrue(tablename.Contains("Transacties"), "Title of the transacties table is rigt");
        }

        [TestMethod]

        //Check if the tableheads are correct.
        public void checkTableHead()
        {
            string kolomNaam1 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[1]")).Text;
            string kolomNaam2 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[2]")).Text;
            string kolomNaam3 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[3]")).Text;
            string kolomNaam4 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[4]")).Text;
            string kolomNaam5 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[5]")).Text;
            string kolomNaam6 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[6]")).Text;
            string kolomNaam7 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[7]")).Text;
            string kolomNaam8 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[8]")).Text;
            Assert.IsTrue(kolomNaam1.Contains("ProfielId"), "ProfielId is correct");
            Assert.IsTrue(kolomNaam2.Contains("Datum"), "Datum is correct");
            Assert.IsTrue(kolomNaam3.Contains("ArtikelID"), "ArtikelID is correct");
            Assert.IsTrue(kolomNaam4.Contains("ArtikelAantal"), "ArtikelAantal is correct");
            Assert.IsTrue(kolomNaam5.Contains("Serienummer"), "Serienummer is correct");
            Assert.IsTrue(kolomNaam6.Contains("Donatie"), "Donatie is correct");
            Assert.IsTrue(kolomNaam7.Contains("Lening"), "Lening is correct");
            Assert.IsTrue(kolomNaam8.Contains("TransactieID"), "TransactieID is correct");

        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            webDriver.Quit();
        }
    }
}
