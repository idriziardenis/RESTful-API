using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class DepartamentsController : Controller
    {
        private IDepartamentsRepository _departamentsRepository;

        public DepartamentsController(IDepartamentsRepository departamentsRepository)
        {
            _departamentsRepository = departamentsRepository;
        }

        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var result = await _departamentsRepository.GetAll();
                if (result.Item1)
                {
                    if (TempData["SuccessResultF"] != null)
                    {
                        ViewBag.Notification = new SuccessResult((bool)TempData["SuccessResultF"], (string)TempData["SuccessResultM"]);
                    }
                    return View(result.Item2);
                }
                else
                {
                    TempData["SuccessResultF"] = false;
                    TempData["SuccessResultM"] = "You are not logged in!";
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
