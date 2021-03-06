using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System;

namespace CirculaireICTKeten.UITests
{
    [TestClass]
    public class LoginUITest
    {
        [TestMethod]
        public void Login()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Login/Login";
            //ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");
            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.ClassName("Textbox")).SendKeys("1816802crijns@zuyd.nl");
                driver.FindElement(By.Id("PasswordTextBox")).SendKeys("T3stW@achtw00rD");
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.TagName("h1")));

                var text = driver.FindElement(By.TagName("h1"));

                //Assert
                Assert.IsTrue(text.Text.Contains("Dashboard"));
            }
        }

        [TestMethod]
        public void IncorrectEmail()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Login/Login";
            //ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");
            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.ClassName("Textbox")).SendKeys("testuser@CirculaireICTKeten.nl");
                driver.FindElement(By.Id("PasswordTextBox")).SendKeys("test2021");
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.Id("LoginValidationError")));

                var text = driver.FindElement(By.Id("LoginValidationError"));

                //Assert
                Assert.IsTrue(text.Text.Contains("DE GEBRUIKERSNAAM/WACHTWOORD IS INCORRECT"));
            }
        }

        [TestMethod]
        public void IncorrectPassword()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Login/Login";
            //ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");
            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.ClassName("Textbox")).SendKeys("testuser1@CirculaireICTKeten.nl");
                driver.FindElement(By.Id("PasswordTextBox")).SendKeys("test2021!");
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.Id("LoginValidationError")));

                var text = driver.FindElement(By.Id("LoginValidationError"));

                //Assert
                Assert.IsTrue(text.Text.Contains("DE GEBRUIKERSNAAM/WACHTWOORD IS INCORRECT"));
            }
        }

        [TestMethod]
        public void NoEmailFilledIn()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Login/Login";
            //ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.ClassName("Textbox")).SendKeys("");
                driver.FindElement(By.Id("PasswordTextBox")).SendKeys("test2021");
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.ClassName("Validation")));

                var text = driver.FindElement(By.ClassName("Validation"));

                //Assert
                Assert.IsTrue(text.Text.Contains("ER IS GEEN E-MAILADRES INGEVULD"));
            }
        }

        [TestMethod]
        public void NoPasswordFilledIn()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Login/Login";
            //ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");
            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                //Act
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();
                driver.FindElement(By.ClassName("Textbox")).SendKeys("testuser1@CirculaireICTKeten.nl");
                driver.FindElement(By.Id("PasswordTextBox")).SendKeys("");
                driver.FindElement(By.ClassName("Button")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));
                wait.Until(wt => wt.FindElement(By.Id("PasswordValidation")));

                var text = driver.FindElement(By.Id("PasswordValidation"));

                //Assert
                Assert.IsTrue(text.Text.Contains("ER IS GEEN WACHTWOORD INGEVULD"));
            }
        }
        [TestMethod]
        public void NoEmailAndPasswordFilledIn()
        {
            //Arange
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Login/Login";
            //ChromeDriver driver = new ChromeDriver(@"D:\School\Vakken\B2C6\TestProject1\TestProject1\bin\Debug\net5.0");
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

        /*[TestMethod]
        public void NoEmailFilledIn2()
        {
            //string randomEmail = RandomEmailGenerator();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Register/Register";
            //FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();

                //Input fields
                driver.FindElement(By.Id("emailTextbox")).SendKeys("");
                driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
                driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
                driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
                driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
                driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
                driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
                driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("04/06/2021");
                driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password");
                driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password");

                //Submit button
                driver.FindElement(By.Id("registerButton")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

                wait.Until(wt => wt.FindElement(By.Id("EmailValidation")));
                var message = driver.FindElement(By.Id("EmailValidation"));
                Assert.AreEqual("Er is geen e-mailadres ingevuld", message.Text);
                //Assert.IsTrue(message.Text.Contains("Er is geen e-mailadres ingevuld"));
            }
        }*/
    }
}
