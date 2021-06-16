using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CirculaireICTKeten.Controllers;

namespace CirculaireICTKeten.UnitTests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private readonly ILogger<HomeController> _logger;

        [TestMethod]
        public void Privacy()
        {
            HomeController controller = new HomeController(_logger);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
        configuration
    }
}
