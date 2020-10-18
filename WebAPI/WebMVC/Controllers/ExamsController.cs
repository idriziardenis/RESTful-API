using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.DTOs;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class ExamsController : Controller
    {
        private ISubjectsRepository _subjectsRepository;
        private IStudentsRepository _studentsRepository;
        private IProfessorsRepository _professorsRepository;
        private IExamsRepository _examsRepository;

        public ExamsController(ISubjectsRepository subjectsRepository, IStudentsRepository studentsRepository, IProfessorsRepository professorsRepository, IExamsRepository examsRepository)
        {
            _subjectsRepository = subjectsRepository;
            _studentsRepository = studentsRepository;
            _professorsRepository = professorsRepository;
            _examsRepository = examsRepository;
        }

        public async Task<ActionResult> IndexAsync()
        {
            try
            {
                var result = await _examsRepository.GetAll();
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

        public async Task<IActionResult> AddAsync()
        {
            try
            {
                var professors = _professorsRepository.GetAll();
                var students = _studentsRepository.GetAll();
                var subjects = _subjectsRepository.GetAll();

                await Task.WhenAll(professors, students, subjects);

                if (professors.Result.Item1)
                {
                    ViewBag.ProfessorId = new SelectList(professors.Result.Item2, "Id", "ProfessorName");
                    ViewBag.StudentId = new SelectList(students.Result.Item2, "Id", "Name");
                    ViewBag.SubjectId = new SelectList(subjects.Result.Item2, "Id", "SubjectName");
                    return View();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAsync(AddExamDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Notification = new SuccessResult(false, "All fields are required!");
                    return View(model);
                }
                var result = await _examsRepository.Add(model);
                if (result.Item1)
                {
                    TempData["SuccessResultF"] = result.Item1;
                    TempData["SuccessResultM"] = result.Item2;

                    return RedirectToAction("Index", "Students");
                }
                else
                {
                    ViewBag.Notification = new SuccessResult((bool)result.Item1, (string)result.Item2);


                    var professors = _professorsRepository.GetAll();
                    var students = _studentsRepository.GetAll();
                    var subjects = _subjectsRepository.GetAll();

                    await Task.WhenAll(professors, students, subjects);

                    ViewBag.ProfessorId = new SelectList(professors.Result.Item2, "Id", "ProfessorName");
                    ViewBag.StudentId = new SelectList(students.Result.Item2, "Id", "Name");
                    ViewBag.SubjectId = new SelectList(subjects.Result.Item2, "Id", "SubjectName");

                    return View(model);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
