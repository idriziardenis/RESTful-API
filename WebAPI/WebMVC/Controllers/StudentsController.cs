using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.DTOs;
using WebMVC.Interfaces;
using WebMVC.Models;

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
            try
            {
                var result = _studentsRepository.GetAll().Result;
                if (result.Item1)
                {
                    if(TempData["SuccessResultF"] != null)
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

        public ActionResult LastFive()
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var result = _studentsRepository.GetAll().Result;
                if (result.Item1)
                {
                    return View(result.Item2);
                }
                else
                {
                    TempData["SuccessResultF"] = result.Item1;
                    TempData["SuccessResultM"] = "You are not logged in!";
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        // GET: StudentsController/Create
        public ActionResult Add()
        {
            try
            {
                var result = _departamentsRepository.GetAll().Result;
                if (result.Item1)
                {
                    ViewBag.DepartmentId = new SelectList(result.Item2, "Id", "DepartmentName");
                    return View();
                }
                else
                {
                    TempData["SuccessResultF"] = result.Item1;
                    TempData["SuccessResultM"] = "You are not logged in!";
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
             
        }

        // POST: StudentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddStudentDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Notification = new SuccessResult(false, "All fields are required!");
                    return View(model);
                }
                var result = _studentsRepository.Add(model).Result;
                if (result.Item1)
                {
                    TempData["SuccessResultF"] = result.Item1;
                    TempData["SuccessResultM"] = result.Item2;

                    return RedirectToAction("Index", "Students");
                }
                else
                {
                    ViewBag.Notification = new SuccessResult((bool)result.Item1, (string)result.Item2);
                    model.Index = "";

                    var dep = _departamentsRepository.GetAll().Result;
                    ViewBag.DepartmentId = new SelectList(dep.Item2, "Id", "DepartmentName");
                    return View(model);
                }
            }
            catch (Exception ex)
            {

                throw ex;
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
