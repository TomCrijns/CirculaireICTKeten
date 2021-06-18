using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CirculaireICTKetenUITESTS
{
    [TestClass]
    public class ArtikelenPageTest
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
            webDriver.Url = "https://test-ruilwinkel-vaals.azurewebsites.net/Artikelen";
        }

        [TestMethod]

        //Check the Page Title of the Artikelen Page
        public void VerifyPageTitle()
        {
            Assert.IsTrue(webDriver.Title.Contains("- CirculaireICTKeten"), "Verified title of the page");
        }

        [TestMethod]

        //Check the name of the Artikelen Table
        public void checkTableName()
        {
            string tablename = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[1]/h2")).Text;
            Assert.IsTrue(tablename.Contains("Artikelen"), "Title of the artikelen table is rigt");
        }


        [TestMethod]

        //Check if there is an Niew Artikel button and if it contains the text "Nieuw Artikel"
        public void checkArtikelCreateButton()
        {
            string buttonName = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[1]/div[2]/a")).Text;
            Assert.IsTrue(buttonName.Contains("Nieuw artikel toevoegen"), "Title of the nieuw artikel knop is right");
        }

        [TestMethod]

        //Check if it is possible to create a new product.
        public void createArtikel()
        {
            var rand = new Random();
            webDriver.FindElement(By.XPath("/html/body/div/main/div/div/div[2]/a")).Click();
            string naam = webDriver.FindElement(By.XPath("/html/body/div/main/form/div/div[2]/div[1]/div[1]/div[1]/label")).Text;
            string id = webDriver.FindElement(By.XPath("/html/body/div/main/form/div/div[2]/div[1]/div[2]/div[1]/label")).Text;
            string punten = webDriver.FindElement(By.XPath("/html/body/div/main/form/div/div[2]/div[1]/div[3]/div[1]/label")).Text;
            string serienummer = webDriver.FindElement(By.XPath("/html/body/div/main/form/div/div[2]/div[1]/div[4]/div[1]/label")).Text;
            string create = webDriver.FindElement(By.XPath("/html/body/div/main/form/div/div[2]/div[1]/div[5]/div/div[1]/input")).Text;
            string back = webDriver.FindElement(By.XPath("/html/body/div/main/form/div/div[2]/div[1]/div[5]/div/div[2]/a")).Text;
            Assert.IsTrue(naam.Contains("ArtikelNaam"));
            Assert.IsTrue(id.Contains("ArtikelSoortId"));
            Assert.IsTrue(punten.Contains("ArtikelPunten"));
            Assert.IsTrue(serienummer.Contains("Serienummer"));
            Assert.IsNotNull(create);
            Assert.IsNotNull(back);
            bool input1 = webDriver.PageSource.Contains("ArtikelNaam");
            bool input2 = webDriver.PageSource.Contains("ArtikelSoortId");
            bool input3 = webDriver.PageSource.Contains("ArtikelPunten");
            bool input4 = webDriver.PageSource.Contains("Serienummer");
            Assert.IsTrue(input1);
            Assert.IsTrue(input2);
            Assert.IsTrue(input3);
            Assert.IsTrue(input4);
            int randomId = rand.Next(10);
            int randomPunten = rand.Next(20);
            int randomSerienummer = rand.Next();
            IWebElement inputNaam = webDriver.FindElement(By.XPath("//*[@id='ArtikelNaam']"));
            IWebElement inputId = webDriver.FindElement(By.XPath("//*[@id='ArtikelSoortId']"));
            IWebElement inputPunten = webDriver.FindElement(By.XPath("//*[@id='ArtikelPunten']"));
            IWebElement inputSerienummer = webDriver.FindElement(By.XPath("//*[@id='Serienummer']"));
            inputNaam.Clear();
            inputId.Clear();
            inputPunten.Clear();
            inputSerienummer.Clear();
            inputNaam.SendKeys("TestNaam");
            inputId.SendKeys(randomId.ToString());
            inputPunten.SendKeys(randomPunten.ToString());
            inputSerienummer.SendKeys(randomSerienummer.ToString());
            webDriver.FindElement(By.XPath("/html/body/div/main/form/div/div[2]/div[1]/div[5]/div/div[1]/input")).Click();
        }


        [TestMethod]

        //Check if the tableheads are correct.
        public void checkTableHead()
        {
            string kolomNaam1 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[1]")).Text;
            string kolomNaam2 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[2]")).Text;
            string kolomNaam3 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[3]")).Text;
            string kolomNaam4 = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/thead/tr/th[4]")).Text;
            Assert.IsTrue(kolomNaam1.Contains("Artikel Naam"), "Artikel naam is correct");
            Assert.IsTrue(kolomNaam2.Contains("Artikel SoortId"), "Artikel soort is correct");
            Assert.IsTrue(kolomNaam3.Contains("Artikel Punten"), "Artikel punten is correct");
            Assert.IsTrue(kolomNaam4.Contains("Serienummer"), "serienummer is correct");

        }

        [TestMethod]

        //Check if a row can be editted
        public void checkEditDeleteOptions()
        {
            string row = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/tbody/tr[1]")).Text;
            if (!string.IsNullOrEmpty(row))
            {
                string edit = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/tbody/tr[1]/td[5]/div/a[1]")).ToString();
                string delete = webDriver.FindElement(By.XPath("/html/body/div/main/div/div[2]/table/tbody/tr[1]/td[5]/div/a[2]")).ToString();
                Assert.IsNotNull(edit, "Edit button is correct");
                Assert.IsNotNull(delete, "Delete button is correct");
            }
            else
            {
                Assert.IsTrue(string.IsNullOrEmpty(row), "No artikel found");
            }
        }


        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            webDriver.Quit();
        }
    }
}
