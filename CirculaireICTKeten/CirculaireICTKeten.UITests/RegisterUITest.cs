using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text;
using System;
using System.IO;

namespace CirculaireICTKeten.UITests
{
    [TestClass]
    public class RegisterUITest
    {
        //Method for making RandomEmails
        public string RandomEmailGenerator()
        {
            var generator = new Random();
            bool lowerCase = false;
            int size = generator.Next(0, 15);
            
            var builder = new StringBuilder();
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)generator.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            string randomstring = builder.ToString().ToLower();
            string email = randomstring + "@testmail.com";
            return email;
        }
        
        [TestMethod]
        public void DoBToYoung()
        {
            string randomEmail = RandomEmailGenerator();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Register/Register";
            //FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();

                //Input fields
                driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
                driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
                driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
                driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
                driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
                driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
                driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
                driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("03-06-2021");
                driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password1");
                driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password2");

                //Submit button
                driver.FindElement(By.Id("registerButton")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

                wait.Until(wt => wt.FindElement(By.Id("registrationError")));
                var message = driver.FindElement(By.Id("registrationError"));
                Assert.IsTrue(message.Text.Contains("U dient minimaal 16jaar te zijn om te registreren."));
                //Assert.AreEqual("U dient minimaal 16jaar te zijn om te registreren.", message.Text);

                // driver.Close();
                // driver.Dispose();
            }
        }

        [TestMethod]
        public void EmailExists()
        {
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Register/Register";
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            //FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();

                //Input fields
                driver.FindElement(By.Id("emailTextbox")).SendKeys("admin@ruilwinkelvaals.nl");
                driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
                driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
                driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
                driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
                driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
                driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
                driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("03-06-2000");
                driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password1");
                driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password2");

                //Submit button
                driver.FindElement(By.Id("registerButton")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

                wait.Until(wt => wt.FindElement(By.Id("registrationError")));
                var message = driver.FindElement(By.Id("registrationError"));
                Assert.IsTrue(message.Text.Contains("Er bestaat al een account met dit Email adres."));

                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void PasswordNotEqual()
        {
            string randomEmail = RandomEmailGenerator();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Register/Register";
            //FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();

                //Input fields
                driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
                driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
                driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
                driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
                driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
                driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
                driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
                driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("03-06-2000");
                driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password1");
                driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password2");

                //Submit button
                driver.FindElement(By.Id("registerButton")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

                wait.Until(wt => wt.FindElement(By.Id("registrationError")));
                var message = driver.FindElement(By.Id("registrationError"));
                Assert.IsTrue(message.Text.Contains("De gegeven wachtwoorden komen niet overeen met elkaar."));

                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void NoEmailFilledIn()
        {
            string randomEmail = RandomEmailGenerator();
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
                driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("03-06-2000");
                driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password");
                driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password");

                //Submit button
                driver.FindElement(By.Id("registerButton")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

                wait.Until(wt => wt.FindElement(By.Id("EmailValidation")));
                var message = driver.FindElement(By.Id("EmailValidation"));
                Assert.IsTrue(message.Text.Contains("Er is geen e-mailadres ingevuld"));

                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void NoPasswordFilledIn()
        {
            string randomEmail = RandomEmailGenerator();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Register/Register";
            //FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();

                //Input fields
                driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
                driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
                driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
                driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
                driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
                driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
                driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
                driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("03-06-2000");
                driver.FindElement(By.Id("passwordTextbox")).SendKeys("");
                driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password");

                //Submit button
                driver.FindElement(By.Id("registerButton")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

                wait.Until(wt => wt.FindElement(By.Id("PasswordValidation")));
                var message = driver.FindElement(By.Id("PasswordValidation"));
                Assert.IsTrue(message.Text.Contains("Er is geen wachtwoord ingevuld"));

                driver.Close();
                driver.Dispose();
            }
        }

        [TestMethod]
        public void NoValidationPasswordFilledIn()
        {
            string randomEmail = RandomEmailGenerator();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Register/Register";
            //FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();

                //Input fields
                driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
                driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
                driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
                driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
                driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
                driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
                driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
                driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("03-06-2000");
                driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password");
                driver.FindElement(By.Id("validateTextbox")).SendKeys("");

                //Submit button
                driver.FindElement(By.Id("registerButton")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

                wait.Until(wt => wt.FindElement(By.Id("ValidationPasswordValidation")));
                var message = driver.FindElement(By.Id("ValidationPasswordValidation"));
                Assert.IsTrue(message.Text.Contains("Er is geen wachtwoord ingevuld"));
                Assert.AreEqual("Er is geen wachtwoord ingevuld", message.Text);
                //driver.Close();
                //driver.Dispose();
            }
        }

        [TestMethod]
        public void NoDoBFilledIn()
        {
            string randomEmail = RandomEmailGenerator();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Register/Register";
            //FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();

                //Input fields
                driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
                driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
                driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
                driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
                driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
                driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
                driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
                driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("");
                driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password");
                driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password");

                //Submit button
                driver.FindElement(By.Id("registerButton")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

                wait.Until(wt => wt.FindElement(By.Id("DoBValidation")));
                var message = driver.FindElement(By.Id("DoBValidation"));
                //Assert.IsTrue(message.Text.Contains("Er is geen geboortedatum ingevuld"));
                Assert.AreEqual("Er is geen geboortedatum ingevuld", message.Text);
                //driver.Close();
                //driver.Dispose();
            }
        }

        [TestMethod]
        public void SuccessNewRegistration()
        {
            string randomEmail = RandomEmailGenerator();
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            string url = "https://test-ruilwinkel-vaals.azurewebsites.net/Register/Register";
            //FirefoxDriver driver = new FirefoxDriver(@"D:\Program_Files\Downloads\FireFox_Driver");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), chromeOptions))
            {
                driver.Navigate().GoToUrl(url);
                driver.Manage().Window.Maximize();

                //Input fields
                driver.FindElement(By.Id("emailTextbox")).SendKeys(randomEmail);
                driver.FindElement(By.Id("voornaamTextbox")).SendKeys("UI-T-Voornaam");
                driver.FindElement(By.Id("achternaamTextbox")).SendKeys("UI-T-Achternaam");
                driver.FindElement(By.Id("straatTextbox")).SendKeys("UI-T-Straat");
                driver.FindElement(By.Id("huisnummerTextbox")).SendKeys("UI-T-1");
                driver.FindElement(By.Id("woonplaatsTextbox")).SendKeys("UI-T-Woonplaats");
                driver.FindElement(By.Id("postcodeTextbox")).SendKeys("UI-T-PST");
                driver.FindElement(By.Id("geboortedatumTextbox")).SendKeys("03-06-2000");
                driver.FindElement(By.Id("passwordTextbox")).SendKeys("UI-T-password");
                driver.FindElement(By.Id("validateTextbox")).SendKeys("UI-T-password");

                //Submit button
                driver.FindElement(By.Id("registerButton")).Click();
                WebDriverWait wait = new WebDriverWait(driver, new System.TimeSpan(0, 1, 0));

                wait.Until(wt => wt.FindElement(By.ClassName("Label")));
                var message = driver.FindElement(By.ClassName("Label"));
                Assert.IsTrue(message.Text.Contains("E-MAILADRES"));

                driver.Close();
                driver.Dispose();
            }
        }
    }
}
