using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebMVC.Extensions;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticateService _authenticateService;
        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IAuthenticateService authenticateService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _authenticateService = authenticateService;
        }

        public IActionResult Index()
        {
            try
            {
                var token = Session.GetString("Token");
                if (string.IsNullOrEmpty(token))
                {
                    TempData["SuccessResultF"] = false;
                    TempData["SuccessResultM"] = "You are not logged in!";
                    RedirectToAction("Login");
                }
                else
                {
                    if(TempData["SuccessResultF"] != null)
                    {
                        ViewBag.Notification = new SuccessResult((bool)TempData["SuccessResultF"], (string)TempData["SuccessResultM"]);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Login");

        }

        public IActionResult Login()
        {
            if (TempData["SuccessResultF"] != null)
            {
                ViewBag.Notification = new SuccessResult((bool)TempData["SuccessResultF"], (string)TempData["SuccessResultM"]);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Authentication model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Notification = new SuccessResult(false, "Username and Password are required!");
                    return View(model);
                }

                var obj = await _authenticateService.Authenticate(model);
                SuccessResult sr = null;
                if (obj.Item1)
                {
                    var token = obj.Item2;
                    Session.SetString("Token", obj.Item2);

                    TempData["SuccessResultF"] = true;
                    TempData["SuccessResultM"] = "You are logged succssfully!";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    sr = new SuccessResult(obj.Item1, obj.Item2);
                    ViewBag.Notification = sr;
                    return View(model);
                }
            }
            catch (Exception)
            {
                return View(model);
            }
        }
    }
}
