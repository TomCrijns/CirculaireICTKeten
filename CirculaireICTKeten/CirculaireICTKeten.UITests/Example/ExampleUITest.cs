using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System;

namespace CirculaireICTKeten.UITests.Example
{
    [TestClass]
    public class ExampleUITest
    {
        [TestMethod]
        public void NoEmailAndPasswordFilledIn()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkelvaalscore.azurewebsites.net/Login/Login";
            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.ClassName("Textbox")).SendKeys("");
                driver.FindElement(By.Id("PasswordTextBox")).SendKeys("");
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.Id("PasswordValidation")));

                var emailValidation = driver.FindElement(By.ClassName("Validation"));
                var passwordValidation = driver.FindElement(By.Id("PasswordValidation"));

                //Assert
                Assert.IsTrue(passwordValidation.Text.Contains("ER IS GEEN WACHTWOORD INGEVULD"));
                Assert.IsTrue(emailValidation.Text.Contains("ER IS GEEN E-MAILADRES INGEVULD"));
            }
        }
    }
}
