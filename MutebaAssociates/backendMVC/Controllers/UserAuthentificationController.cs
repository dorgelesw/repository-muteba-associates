using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using backendMVC.Models.repository;
using System.Security.Claims;

namespace backendMVC.Controllers
{
    public class UserAuthentificationController : Controller
    {
        // Get an instance of repository of account.
        private IAccountRepository repository = new AccountRepository();

        // GET: UserAuthentification.
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        // Check user connexion.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(dal.Account model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Authentification fail
            if (!checkUserAuthentification(model.Login, model.Password))
            {
                ModelState.AddModelError(string.Empty, "user login or password incorrect.");
                return View(model);
            }

            // Authentification success, 
            // inject user parameters into cookie for local authentification.
            var loginClaim = new Claim(ClaimTypes.NameIdentifier, model.Login);
            var claimsIdentity = new ClaimsIdentity(new[] { loginClaim }, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignIn(claimsIdentity);

            // Forward origine URL :
            if (Url.IsLocalUrl(ViewBag.ReturnUrl))
                return Redirect(ViewBag.ReturnUrl);

            // Forward to the home page :
            return RedirectToAction("Index", "Home");
        }

        private bool checkUserAuthentification(string login, string password)
        {
            // TODO : insert here codes to validate user login and password...
            // For this version, We find the login and password from the list of accounts...
            List<dal.Account> accounts = repository.getAccountList();
            dal.Account account = accounts.Find((a => a.Login == login && a.Password == password));
            return account != null;
        }


        [HttpGet]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut();

            // Forward to the user login page :
            return RedirectToAction("Index", "Home");
        }
    }
}