using CirculaireICTKeten.Controllers;
using CirculaireICTKeten.Models.Entity;
using CirculaireICTKeten.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MockQueryable.Moq;
using CirculaireICTKeten.Models;

namespace CirculaireICTKeten.UnitTests.Controllers
{
    public class MyTransactionControllerTest
    {
        [Fact]
        public async Task Index()
        {
            // act
            Mock<CirculaireICTKeten_dbContext> mock = new Mock<CirculaireICTKeten_dbContext>();
            mock.SetupGet(q => q.Transacties).Returns(new[]
            {
                new TransactieModel()
                {
                    TransactieID = 1,
                    Datum = DateTime.Now,
                    ProfielId = 3,
                },
                new TransactieModel()
                {
                    TransactieID = 2,
                    Datum = DateTime.Now.AddMinutes(-180),
                    ProfielId = 4,
                }
            }.AsQueryable().BuildMockDbSet().Object);
            MyTransactionsController controller = new MyTransactionsController(mock.Object);

            // arrange
            IActionResult result = await controller.Index();

            // assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TransactieModel>>(viewResult.ViewData.Model);
            Assert.True(model.Any());
        }


        [Fact]
        public async Task Details()
        {
            int transactionToCheckId = 2;

            // act
            Mock<CirculaireICTKeten_dbContext> mock = new Mock<CirculaireICTKeten_dbContext>();
            mock.SetupGet(q => q.Transacties).Returns(new[]
            {
                new TransactieModel()
                {
                    TransactieID = 1,
                    Datum = DateTime.Now,
                    ProfielId = 3,
                },
                new TransactieModel()
                {
                    TransactieID = 2,
                    Datum = DateTime.Now.AddMinutes(-201),
                    ProfielId = 4,
                }
            }.AsQueryable().BuildMockDbSet().Object);
            MyTransactionsController controller = new MyTransactionsController(mock.Object);

            // arrange
            IActionResult result = await controller.Details(transactionToCheckId);

            // assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TransactieModel>(viewResult.ViewData.Model);
            Assert.Equal(transactionToCheckId, model.TransactieID);
        }
    }
}
