using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CirculaireICTKeten.Controllers;
using CirculaireICTKeten.UnitTests.Helpers;
using CirculaireICTKeten.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CirculaireICTKeten.ViewModel.RestorePassword;
using CirculaireICTKeten.Services;

namespace CirculaireICTKeten.Tests.Services
{
    [TestClass]
    public class TokenProviderServiceTest
    {
       [TestMethod]
       public void GenerateToken()
        {
            var token = TokenProviderService.GenerateToken();
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void DecodeToken()
        {
            var token = TokenProviderService.GenerateToken();
            DateTime dateTime = TokenProviderService.GetDateTime(token);
            Assert.IsNotNull(token);
        }
    }
}
