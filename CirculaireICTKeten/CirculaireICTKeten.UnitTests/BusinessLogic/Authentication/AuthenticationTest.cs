using Microsoft.VisualStudio.TestTools.UnitTesting;
using CirculaireICTKeten.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirculaireICTKeten.Tests.BusinessLogic.Authentication
{
    [TestClass]
    public class AuthenticationTest
    {
        [TestMethod]
        public void CorrectLogin()
        {
            //Arange
            Login loginAccount = new Login();
            loginAccount.emailAddress = "testuser1@CirculaireICTKeten.nl";
            loginAccount.password = "test2021";

            //Act
            bool authenticated = CirculaireICTKeten.BusinessLogic.Authentication.Authentication.AuthenticateUser(
                loginAccount.emailAddress,
                "testuser1@CirculaireICTKeten.nl",
                loginAccount.password,
                "zPcuh9FQGBfNatOO118OM8UtIP0sTFriDJd/MegmC3hPn8s5rs880mArdmFfYToHRAb3raQLNwoFhdkN4AoIh1BuFyeKvoscQqFpFa4y23KmjaSVk4uDTC0hk5dr8SpU4jVlySfD3iP0b/cifnyNlA3Xe/EsYVuY+V7v2xiB0BQTnBub+ly+bA4jY9FSVmUgg/3qqdTAK8wpfjs9z5GpkbgbnSTKzPwEdeJLHnPxd5X9D92X2Auaqyjd3ABn55oR5r8EsBURhtexy1zBi8RBCqDopFCez7O6DpbMJfCwEy4pmcSy/Yil4NGcdDHeXrmM5KM9G3eN0QhOqxjGUqId5w==",
                "X9hY/UwgcVcOpBN+pgmL3Q==");

            //Assert
            Assert.IsTrue(authenticated);
        }

        [TestMethod]
        public void WrongEmailAddress()
        {
            //Arange
            Login loginAccount = new Login();
            loginAccount.emailAddress = "testuser@CirculaireICTKeten.nl";
            loginAccount.password = "test2021";

            //Act
            bool authenticated = CirculaireICTKeten.BusinessLogic.Authentication.Authentication.AuthenticateUser(
                loginAccount.emailAddress,
                "testuser1@CirculaireICTKeten.nl",
                loginAccount.password,
                "zPcuh9FQGBfNatOO118OM8UtIP0sTFriDJd/MegmC3hPn8s5rs880mArdmFfYToHRAb3raQLNwoFhdkN4AoIh1BuFyeKvoscQqFpFa4y23KmjaSVk4uDTC0hk5dr8SpU4jVlySfD3iP0b/cifnyNlA3Xe/EsYVuY+V7v2xiB0BQTnBub+ly+bA4jY9FSVmUgg/3qqdTAK8wpfjs9z5GpkbgbnSTKzPwEdeJLHnPxd5X9D92X2Auaqyjd3ABn55oR5r8EsBURhtexy1zBi8RBCqDopFCez7O6DpbMJfCwEy4pmcSy/Yil4NGcdDHeXrmM5KM9G3eN0QhOqxjGUqId5w==",
                "X9hY/UwgcVcOpBN+pgmL3Q==");

            //Assert
            Assert.IsFalse(authenticated);
        }

        [TestMethod]
        public void WrongPassword()
        {
            //Arange
            Login loginAccount = new Login();
            loginAccount.emailAddress = "testuser1@CirculaireICTKeten.nl";
            loginAccount.password = "test2022";

            //Act
            bool authenticated = CirculaireICTKeten.BusinessLogic.Authentication.Authentication.AuthenticateUser(
                loginAccount.emailAddress,
                "testuser1@CirculaireICTKeten.nl",
                loginAccount.password,
                "zPcuh9FQGBfNatOO118OM8UtIP0sTFriDJd/MegmC3hPn8s5rs880mArdmFfYToHRAb3raQLNwoFhdkN4AoIh1BuFyeKvoscQqFpFa4y23KmjaSVk4uDTC0hk5dr8SpU4jVlySfD3iP0b/cifnyNlA3Xe/EsYVuY+V7v2xiB0BQTnBub+ly+bA4jY9FSVmUgg/3qqdTAK8wpfjs9z5GpkbgbnSTKzPwEdeJLHnPxd5X9D92X2Auaqyjd3ABn55oR5r8EsBURhtexy1zBi8RBCqDopFCez7O6DpbMJfCwEy4pmcSy/Yil4NGcdDHeXrmM5KM9G3eN0QhOqxjGUqId5w==",
                "X9hY/UwgcVcOpBN+pgmL3Q==");

            //Assert
            Assert.IsFalse(authenticated);
        }
    }
}