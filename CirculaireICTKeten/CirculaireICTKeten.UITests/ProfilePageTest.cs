using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CirculaireICTKetenUITESTS
{
    [TestClass]
    public class ProfilePageTest
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
            webDriver.Url = "https://test-ruilwinkel-vaals.azurewebsites.net/Profiel";
        }

        [TestMethod]

        //Check the Page Title of the Profile Page
        public void VerifyPageTitle()
        {
            Assert.IsTrue(webDriver.Title.Contains("- CirculaireICTKeten"), "Verified title of the page");
        }

        [TestMethod]

        //Check the name of the Profile Table
        public void checkTableName()
        {
            string tablename = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[1]/div[1]/h2")).Text;
            Assert.IsTrue(tablename.Contains("Profielen"), "Title of the profielen table is rigt");
        }


        [TestMethod]

        //Check if there is an Niew Profile button and if it contains the text "Nieuw Artikel"
        public void checkProfileCreateButton()
        {
            string buttonName = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[1]/div[2]/a")).Text;
            Assert.IsTrue(buttonName.Contains("nieuw profiel toevoegen"), "Title of the nieuw profiel knop is right");
        }


        [TestMethod]

        //Check if the tableheads are correct.
        public void checkTableHead()
        {
            string kolomNaam1 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[1]")).Text;
            string kolomNaam2 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[2]")).Text;
            string kolomNaam3 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[3]")).Text;
            string kolomNaam4 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[4]")).Text;
            string kolomNaam5 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[5]")).Text;
            string kolomNaam6 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[6]")).Text;
            string kolomNaam7 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[7]")).Text;
            string kolomNaam8 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[8]")).Text;
            string kolomNaam9 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[9]")).Text;
            string kolomNaam10 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[10]")).Text;
            string kolomNaam11 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[11]")).Text;
            string kolomNaam12 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[12]")).Text;
            string kolomNaam13 = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/thead/tr/th[13]")).Text;

            Assert.IsTrue(kolomNaam1.Contains("Id"), "Id is correct");
            Assert.IsTrue(kolomNaam2.Contains("Email"), "Email is correct");
            Assert.IsTrue(kolomNaam3.Contains("Voornaam"), "Voornaam is correct");
            Assert.IsTrue(kolomNaam4.Contains("Achternaam"), "Achternaam is correct");
            Assert.IsTrue(kolomNaam5.Contains("Balans"), "Balans is correct");
            Assert.IsTrue(kolomNaam6.Contains("AccountType"), "AccountType is correct");
            Assert.IsTrue(kolomNaam7.Contains("Ledenpas"), "Ledenpas is correct");
            Assert.IsTrue(kolomNaam8.Contains("Straat"), "Straat is correct");
            Assert.IsTrue(kolomNaam9.Contains("Huisnummer"), "Huisnummer is correct");
            Assert.IsTrue(kolomNaam10.Contains("Woonplaats"), "Woonplaats is correct");
            Assert.IsTrue(kolomNaam11.Contains("Postcode"), "Postcode is correct");
            Assert.IsTrue(kolomNaam12.Contains("DateCreated"), "DateCreated is correct");
            Assert.IsTrue(kolomNaam13.Contains("Geboortedatum"), "Geboortedatum is correct");


        }

        [TestMethod]

        //Check if a row can be editted
        public void checkEditDeleteOptions()
        {
            string row = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/tbody/tr[1]")).Text;
            if (!string.IsNullOrEmpty(row))
            {
                string edit = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/tbody/tr[1]/td[14]/div/a[1]")).ToString();
                string delete = webDriver.FindElement(By.XPath("/html/body/div[3]/div[2]/main/div/div[2]/table/tbody/tr[1]/td[14]/div/a[2]")).ToString();
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
