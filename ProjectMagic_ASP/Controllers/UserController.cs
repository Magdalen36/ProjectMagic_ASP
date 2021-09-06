using Microsoft.AspNetCore.Mvc;
using ProjectMagic_ASP.Models;
using ProjectMagic_ASP.Models.Forms;
using ProjectMagic_ASP.Services;
using ProjectMagic_ASP.Services.Bases;
using ProjectMagic_ASP.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMagic_ASP.Controllers
{
    public class UserController : Controller
    {

        private readonly IService<UserModel, UserForm> _userService;

        public UserController(IService<UserModel, UserForm> us)
        {
            _userService = us;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserForm form)
        {
            if (ModelState.IsValid)
            {
                _userService.Insert(form);
                HttpContext.Session.Set<int>("UserId", form.Id);
                TempData["succes"] = "Insertion effectuée";
                HttpContext.Session.Set<bool>("IsLogged", true);
                TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");

                return RedirectToAction("Index", "Collection", new { id = HttpContext.Session.Get<int>("UserId") } ); 
            }
            else
            {
                TempData["error"] = "Formulaire invalide";
                return View(form);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginForm form)
        {

            if (ModelState.IsValid)
            {
                int userId = (_userService as UserService).Login(form.Email, form.Password);

                if (userId != 0)
                {
                    HttpContext.Session.Set<bool>("IsLogged", true);
                    TempData["isLogged"] = HttpContext.Session.Get<bool>("IsLogged");
                    HttpContext.Session.Set<int>("UserId", userId);

                    return RedirectToAction("Index", "Collection", new { id = HttpContext.Session.Get<int>("UserId") });
                }
                else
                {
                    TempData["error"] = "Identifiant ou mot de passe invalide";
                    return View(form);
                }
            }
            else
            {
                TempData["error"] = "formulaire invalide";
                return View(form);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Set<bool>("IsLogged", false);
            return RedirectToAction("Index", "Home");
        }
    }
}
