using CirculaireICTKeten.Controllers;
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
    public class TransactionControllerTest
    {
        [Fact]
        public void IndexNoActiveTransaction()
        {
            // act
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.Index();

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexActiveTransaction()
        {
            // act
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            transactionManagerMock.SetupGet(q => q.ActiveTransaction).Returns(new TransactieModel()
            {
                TransactieID = 1
            });
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.Index();

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void IndexPostActiveTransaction()
        {
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            transactionManagerMock.SetupGet(q => q.ActiveTransaction).Returns(new TransactieModel()
            {
                TransactieID = 1
            });
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.IndexPost(null);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void IndexPostNoActiveTransaction()
        {
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            dbMock.Setup(q => q.ProfileData).Returns(new []
            {
                new ProfileDataModel()
                {
                   Id = 1,
                   Voornaam = "Noud",
                   Achternaam = "Wijngaards",
                   Balans = 8,
                }
            }.AsQueryable().BuildMockDbSet().Object);
            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.IndexPost(new TransactionIndexViewModel() {
                CustomerId = 1,
            });

            // assert
            Assert.IsType<RedirectToActionResult>(result);
            transactionManagerMock.Verify(q => q.StartTransaction(It.IsAny<ProfileDataModel>()), Times.Once());
        }

        [Fact]
        public void AddProductActiveTransaction()
        {
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            dbMock.Setup(q => q.Artikelen).Returns(new[]
            {
                new ArtikelenModel()
                {
                    ArtikelID = 1,
                    ArtikelNaam = "Poster van Marcel van de Beek",
                    ArtikelPunten = 7,
                },
                new ArtikelenModel()
                {
                    ArtikelID = 2,
                    ArtikelNaam = "Noud zijn t-shirt",
                    ArtikelPunten = 10,
                },
                new ArtikelenModel()
                {
                    ArtikelID = 3,
                    ArtikelNaam = "CD van Nick en Simon",
                    ArtikelPunten = 1,
                }
            }.AsQueryable().BuildMockDbSet().Object);
            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            transactionManagerMock.SetupGet(q => q.ActiveTransaction).Returns(new TransactieModel()
            {
                TransactieID = 1
            });
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.AddProduct();

            // assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<TransactionAddProductViewModel>(viewResult.ViewData.Model);
            Assert.Equal(3, viewModel.Products.Count());
        }

        [Fact]
        public void AddProductNoActiveTransaction()
        {
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.AddProduct();

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void AddProductPostNoActiveTransaction()
        {
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.AddProductPost(null);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void AddProductPostNoProductMatch()
        {
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            dbMock.SetupGet(q => q.Artikelen).Returns(new[]
            {
                new ArtikelenModel()
                {
                    ArtikelID = 1,
                    ArtikelNaam = "Poster van Vincent van der Meer",
                    ArtikelPunten = 4,
                },
                new ArtikelenModel()
                {
                    ArtikelID = 2,
                    ArtikelNaam = "Arne zijn sneakers",
                    ArtikelPunten = 7,
                },
                new ArtikelenModel()
                {
                    ArtikelID = 3,
                    ArtikelNaam = "CD van Rammstein",
                    ArtikelPunten = 3,
                }
            }.AsQueryable().BuildMockDbSet().Object);

            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            transactionManagerMock.SetupGet(q => q.ActiveTransaction).Returns(new TransactieModel()
            {
                TransactieID = 1
            });
            transactionManagerMock.Setup(q => q.AddProductForSellToTransaction(It.IsAny<ArtikelenModel>(), It.IsAny<byte>(), It.IsAny<short?>()));
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.AddProductPost(new TransactionAddProductViewModel()
            {
                IsForSell = true,
                Points = 3,
                NumberOfProducts = 1,
                SelectedProductId = 4,
            });

            // assert
            Assert.IsType<ViewResult>(result);
            transactionManagerMock.Verify(q => q.AddProductForSellToTransaction(It.IsAny<ArtikelenModel>(), It.IsAny<byte>(), It.IsAny<short?>()), Times.Never());
            transactionManagerMock.Verify(q => q.AddProductToBuyToTransaction(It.IsAny<ArtikelenModel>(), It.IsAny<byte>(), It.IsAny<short?>()), Times.Never());

        }

        [Fact]
        public void AddProductPostForSell()
        {
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            dbMock.SetupGet(q => q.Artikelen).Returns(new[]
            {
                new ArtikelenModel()
                {
                    ArtikelID = 1,
                    ArtikelNaam = "Poster van Marcel van den Beek",
                    ArtikelPunten = 4,
                },
                new ArtikelenModel()
                {
                    ArtikelID = 2,
                    ArtikelNaam = "Wesley zijn horloge",
                    ArtikelPunten = 7,
                },
                new ArtikelenModel()
                {
                    ArtikelID = 3,
                    ArtikelNaam = "CD van Imagine dDagons",
                    ArtikelPunten = 3,
                }
            }.AsQueryable().BuildMockDbSet().Object);

            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            transactionManagerMock.SetupGet(q => q.ActiveTransaction).Returns(new TransactieModel()
            {
                TransactieID = 1
            });
            transactionManagerMock.Setup(q => q.AddProductForSellToTransaction(It.IsAny<ArtikelenModel>(), It.IsAny<byte>(), It.IsAny<short?>()));
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.AddProductPost(new TransactionAddProductViewModel() {
                IsForSell = true,
                Points = 3,
                NumberOfProducts = 1,
                SelectedProductId = 1,
            });

            // assert
            Assert.IsType<RedirectToActionResult>(result);
            transactionManagerMock.Verify(q => q.AddProductForSellToTransaction(It.IsAny<ArtikelenModel>(), It.IsAny<byte>(), It.IsAny<short?>()), Times.Once());
            transactionManagerMock.Verify(q => q.AddProductToBuyToTransaction(It.IsAny<ArtikelenModel>(), It.IsAny<byte>(), It.IsAny<short?>()), Times.Never());
        }

        [Fact]
        public void AddProductPostToBuy()
        {
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            dbMock.SetupGet(q => q.Artikelen).Returns(new[]
            {
                new ArtikelenModel()
                {
                    ArtikelID = 1,
                    ArtikelNaam = "Poster van Chris Kockelkorn",
                    ArtikelPunten = 5,
                },
                new ArtikelenModel()
                {
                    ArtikelID = 2,
                    ArtikelNaam = "Damian zijn ketting",
                    ArtikelPunten = 3,
                },
                new ArtikelenModel()
                {
                    ArtikelID = 3,
                    ArtikelNaam = "CD van Katy Pery",
                    ArtikelPunten = 1,
                }
            }.AsQueryable().BuildMockDbSet().Object);

            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            transactionManagerMock.SetupGet(q => q.ActiveTransaction).Returns(new TransactieModel()
            {
                TransactieID = 1
            });
            transactionManagerMock.Setup(q => q.AddProductForSellToTransaction(It.IsAny<ArtikelenModel>(), It.IsAny<byte>(), It.IsAny<short?>()));
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.AddProductPost(new TransactionAddProductViewModel()
            {
                IsForSell = false,
                Points = 3,
                NumberOfProducts = 1,
                SelectedProductId = 1,
            });

            // assert
            Assert.IsType<RedirectToActionResult>(result);
            transactionManagerMock.Verify(q => q.AddProductForSellToTransaction(It.IsAny<ArtikelenModel>(), It.IsAny<byte>(), It.IsAny<short?>()), Times.Never());
            transactionManagerMock.Verify(q => q.AddProductToBuyToTransaction(It.IsAny<ArtikelenModel>(), It.IsAny<byte>(), It.IsAny<short?>()), Times.Once());
        }

        [Fact]
        public void ListNoActiveTransaction()
        {
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.List();

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void ListActiveTransaction()
        {
            TransactieModel transaction = new TransactieModel()
            {
                TransactieID = 1,
                Datum = DateTime.Now,
                ProfielId = 3,
            };

            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            dbMock.SetupGet(q => q.TransactieArtikelen).Returns(new[]
            {
                
                new TransactieArtikelenModel()
                {
                    Punten = 3,
                    ArtikelID = 2231,
                    IsVerkoop = true,
                    Aantal = 1,
                    TransactieID = transaction.TransactieID,
                    Transactie = transaction
                },
                new TransactieArtikelenModel()
                {
                    Punten = 4,
                    ArtikelID = 5431,
                    IsVerkoop = false,
                    Aantal = 1,
                    TransactieID = transaction.TransactieID,
                    Transactie = transaction
                }
            }.AsQueryable().BuildMockDbSet().Object);

            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            transactionManagerMock.SetupGet(q => q.ActiveTransaction).Returns(new TransactieModel()
            {
                TransactieID = 1,
                Profiel = new ProfileDataModel()
                {
                    Id = 1,
                    Voornaam = "Roel",
                    Achternaam = "Bindels",
                    Balans = 105
                },
                ProfielId = 1
            });
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.List();

            // assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<TransactionListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, viewModel.TransactionProducts.Count());
        }

        [Fact]
        public void FinishNoActiveTransaction()
        {
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.Finish();

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void FinishActiveTransaction()
        {
            Mock<CirculaireICTKeten_dbContext> dbMock = new Mock<CirculaireICTKeten_dbContext>();
            Mock<ITransactionManager> transactionManagerMock = new Mock<ITransactionManager>();
            transactionManagerMock.SetupGet(q => q.ActiveTransaction).Returns(new TransactieModel()
            {
                TransactieID = 1
            });
            TransactionController controller = new TransactionController(dbMock.Object, transactionManagerMock.Object);

            // arrange
            IActionResult result = controller.Finish();

            // assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result); 
            transactionManagerMock.Verify(q => q.EndTransaction(), Times.Once());
        }
    }
}
