using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CirculaireICTKeten.Controllers;
using CirculaireICTKeten.Tests.Helpers;
using CirculaireICTKeten.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CirculaireICTKeten.ViewModel.RestorePassword;
using CirculaireICTKeten.Services;
using CirculaireICTKeten.Services.EmailService.Configuration;
namespace CirculaireICTKeten.Tests.Services.EmailService.EmailConfiguration
{
    [TestClass]
   public class EmailConfigurationTest
    {
        [TestMethod]
        public void GetEmailConfigurationTest()
        {
            IConfigurationRoot root = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            IConfiguration configuration = root as IConfiguration;

            var emailConfiguration = SendEmailConfiguration.GetEmailConfiguration(configuration);
            Assert.IsNotNull(emailConfiguration);
            Assert.AreEqual("smtp.gmail.com", emailConfiguration.SmtpServer);
            Assert.AreEqual(587, Int32.Parse(emailConfiguration.SmtpPort.ToString()));
            Assert.AreEqual(true, bool.Parse(emailConfiguration.SmtpUseSsl.ToString()));
            Assert.AreEqual(true, bool.Parse(emailConfiguration.SmtpRequireAuthentication.ToString()));
            Assert.AreEqual("ruilwinkels@gmail.com", emailConfiguration.SmtpUsername);
            Assert.AreEqual("MegaTron.1996", emailConfiguration.SmtpPassword);
        }
    }
}
