using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CirculaireICTKeten.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CirculaireICTKeten.ViewModel;
using CirculaireICTKeten.BusinessLogic.Authentication;
using System.Globalization;
using System.Threading;

namespace CirculaireICTKeten.Controllers
{
    public class RegisterController : Controller
    {
        CirculaireICTKeten_dbContext db = new CirculaireICTKeten_dbContext();

        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
            var cultureInfo = CultureInfo.GetCultureInfo("NL");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("Email, Password, ValidationPassword, Voornaam, Achternaam, Straat, Huisnummer, Woonplaats, Postcode, Geboortedatum, Zakelijk")] Register model)
        {
            if (ModelState.IsValid)
            {
                //Try to get an Profile where email is similar to the userinput
                var profileExists = db.ProfileData.Where(user => user.Email == model.Email).FirstOrDefault();
                if (profileExists == null) //If there is not an registered user with the given Email
                {
                    var ProfileAge = GetAge(Convert.ToDateTime(model.Geboortedatum).Date);
                    if (ProfileAge >= 16) //Customer is old enough to register
                    {
                        if (model.Password == model.ValidationPassword) //Given passwords are equal
                        {
                            ProfileDataModel newProfile = new ProfileDataModel() //Create new ProfileObject
                            {
                                Email = model.Email,
                                Voornaam = model.Voornaam,
                                Achternaam = model.Achternaam,
                                Straat = model.Straat,
                                Huisnummer = model.Huisnummer,
                                Woonplaats = model.Woonplaats,
                                Postcode = model.Postcode,
                                Geboortedatum = Convert.ToDateTime(model.Geboortedatum).Date,
                                DateCreated = DateTime.Today.Date,
                            };

                            if (model.Zakelijk == true)
                            {
                                newProfile.AccountType = 2;
                            }
                            else
                            {
                                newProfile.AccountType = 1;
                            }
                            //Save profile to DB
                            db.ProfileData.Add(newProfile);
                            db.SaveChanges();

                            //Get ProfileId for Foreign relation
                            var ProfileId = db.ProfileData.Where(profile => profile.Email == model.Email).FirstOrDefault();
                            //Create new Account with relation to ProfileData
                            HashSalt hashSalt = HashSalt.GenerateHashSalt(16, model.Password);
                            AccountDataModel newAccount = new AccountDataModel()
                            {
                                ProfileId = ProfileId.Id,
                                Hash = hashSalt.hash,
                                Salt = hashSalt.salt,
                            };
                            db.AccountData.Add(newAccount);
                            db.SaveChanges();
                            return RedirectToAction("Index", "Home");

                        }
                        else //Passwords are not equal
                        {
                            ModelState.AddModelError("RegisterError", "De gegeven wachtwoorden komen niet overeen met elkaar.");
                            return View();
                        }

                    }
                    else //Customer is not old enough to register
                    {
                        ModelState.AddModelError("RegisterError", "U dient minimaal 16jaar te zijn om te registreren.");
                        return View();
                    }
                }
                else //If there is an user with the given Email
                {
                    ModelState.AddModelError("RegisterError", "Er bestaat al een account met dit Email adres.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public int GetAge(DateTime bornDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - bornDate.Year;
            if (bornDate > today.AddYears(-age))
                age--;
            return age;
        }
    }
}