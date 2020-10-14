using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.DTOs;
using WebMVC.Interfaces;

namespace WebMVC.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentsRepository _studentsRepository;
        private IProfessorsRepository _professorsRepository;
        private ISubjectsRepository _subjectsRepository;
        private IDepartamentsRepository _departamentsRepository;
        public StudentsController(IStudentsRepository studentsRepository, IProfessorsRepository professorsRepository, ISubjectsRepository subjectsRepository, IDepartamentsRepository departamentsRepository)
        {
            _studentsRepository = studentsRepository;
            _professorsRepository = professorsRepository;
            _subjectsRepository = subjectsRepository;
            _departamentsRepository = departamentsRepository;
        }

        // GET: StudentsController
        public ActionResult Index()
        
        {
            var result = _studentsRepository.GetAll().Result;
            if(result.Item1)
            {
                return View(result.Item2);
            }
            else
            {
                TempData["SuccessResultF"] = false;
                TempData["SuccessResultM"] = "You are not logged in!";
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult LastFive()

        {
            var result = _studentsRepository.GetLastFive().Result;
            if (result.Item1)
            {
                return View(result.Item2);
            }
            else
            {
                TempData["SuccessResultF"] = false;
                TempData["SuccessResultM"] = "You are not logged in!";
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(int id)
        {
            var result = _studentsRepository.GetAll().Result;
            if (result.Item1)
            {
                return View(result.Item2);
            }
            else
            {
                TempData["SuccessResultF"] = false;
                TempData["SuccessResultM"] = "You are not logged in!";
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: StudentsController/Create
        public ActionResult Add()
        {
            var result = _departamentsRepository.GetAll().Result;
            if(result.Item1)
            {
                ViewBag.DepartmentId = new SelectList(result.Item2, "Id", "DepartmentName");
                return View();
            }
            else
            {
                TempData["SuccessResultF"] = false;
                TempData["SuccessResultM"] = "You are not logged in!";
                return RedirectToAction("Login", "Home");
            } 
        }

        // POST: StudentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddStudentDTO model)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
