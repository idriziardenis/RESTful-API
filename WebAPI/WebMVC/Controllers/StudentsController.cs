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
        private IStatusRepository _statusRepository;
        public StudentsController(IStudentsRepository studentsRepository, IProfessorsRepository professorsRepository, ISubjectsRepository subjectsRepository, IDepartamentsRepository departamentsRepository, IStatusRepository statusRepository)
        {
            _studentsRepository = studentsRepository;
            _professorsRepository = professorsRepository;
            _subjectsRepository = subjectsRepository;
            _departamentsRepository = departamentsRepository;
            _statusRepository = statusRepository;
        }

        // GET: StudentsController
        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var result = await _studentsRepository.GetAll();
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

        public async Task<ActionResult> LastFiveAsync()
        {
            try
            {
                var result = await _studentsRepository.GetLastFive();
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
        public async Task<ActionResult> DetailsAsync(int id)
        {
            try
            {
                var result = await _studentsRepository.GetAll();
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
        public async Task<ActionResult> AddAsync()
        {
            try
            {
                var deps = _departamentsRepository.GetAll();
                var statuses = _statusRepository.GetAll();

                await Task.WhenAll(deps, statuses);

                if (deps.Result.Item1)
                { 
                    ViewBag.DepartmentId = new SelectList(deps.Result.Item2, "Id", "DepartmentName");
                    ViewBag.StatusId = new SelectList(statuses.Result.Item2, "Id", "Name");
                    return View();
                }
                else
                {
                    TempData["SuccessResultF"] = deps.Result.Item1;
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
        public async Task<ActionResult> AddAsync(AddStudentDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Notification = new SuccessResult(false, "All fields are required!");
                    return View(model);
                }
                var result = await _studentsRepository.Add(model);
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

                    var deps = _departamentsRepository.GetAll();
                    var statuses = _statusRepository.GetAll();

                    await Task.WhenAll(deps, statuses);

                    ViewBag.DepartmentId = new SelectList(deps.Result.Item2, "Id", "DepartmentName");
                    ViewBag.StatusId = new SelectList(statuses.Result.Item2, "Id", "Name");

                    return View(model);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        // GET: StudentsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            try
            {
                var result = await _studentsRepository.GetById(id);
                if (result.Item1)
                {
                    var deps = _departamentsRepository.GetAll();
                    var statuses = _statusRepository.GetAll();

                    await Task.WhenAll(deps, statuses);
                    ViewBag.DepartmentId = new SelectList(deps.Result.Item2, "Id", "DepartmentName", result.Item2.DepartmentId);
                    ViewBag.StatusId = new SelectList(statuses.Result.Item2, "Id", "Name", result.Item2.StatusId);
                    return View(result.Item2);
                }
                else
                {
                    TempData["SuccessResultF"] = result.Item1;
                    TempData["SuccessResultM"] = "Student not found";

                    return RedirectToAction("Index", "Students");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(ReadStudentDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Notification = new SuccessResult(false, "All fields are required!");
                    return View(model);
                }
                var result = await _studentsRepository.Update(model.Id, model);
                if (result.Item1)
                {
                    TempData["SuccessResultF"] = result.Item1;
                    TempData["SuccessResultM"] = result.Item2;

                    return RedirectToAction("Index", "Students");
                }
                else
                {
                    ViewBag.Notification = new SuccessResult((bool)result.Item1, (string)result.Item2);
                    return View(model);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: StudentsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _studentsRepository.Delete(id);
                TempData["SuccessResultF"] = result.Item1;
                TempData["SuccessResultM"] = result.Item2;

                return RedirectToAction("Index", "Students");

            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: StudentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
