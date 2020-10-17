using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.ErrorHandling;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _context;
        private readonly IMapper _mapper;
        private readonly ILogsService _log;
        private IHttpContextAccessor _httpContextAccessor;

        public StudentsController(IStudentsRepository context, IMapper mapper, ILogsService log, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _log = log;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = "GetStudents")]
        public async Task<IActionResult> GetStudentsAsync()
        {
            //return await _context.GetAll();
            var studentItems = await _context.GetAll();
            _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), "Eshte nxjerrur lista e studenteve");
            return Ok(_mapper.Map<IEnumerable<ReadStudentDTO>>(studentItems));
        }

        [HttpGet(Name = "GetLastFiveStudents")]
        public IActionResult GetLastFiveStudents()
        {
            var students = _context.GetLastFive();
            _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), "Eshte nxjerrur lista e 5 studenteve te fundit");
            return Ok(_mapper.Map<IEnumerable<ReadStudentDTO>>(students));
        }

        [HttpGet("{id:int}", Name = "GetSingleStudent")]
        public async Task<IActionResult> GetSingleStudentAsync([FromRoute] int id)
        {
            var item = await _context.Get(id);
            if (item == null)
            {
                _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), $"Nuk eshte gjetur studenti me id: {id}");
                return NotFound(new DataError("Student not found"));
            } 
            _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), $"Eshte nxjerrur studenti me id: {id}");
            return Ok(_mapper.Map<ReadStudentDTO>(item));
        }

        [HttpPost(Name= "AddStudent")]
        public async Task<IActionResult> AddStudentAsync(AddStudentDTO addStudentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var studentModel = _mapper.Map<Student>(addStudentDto);
                    studentModel.RegisteredDate = DateTime.Now;
                    await _context.Add(studentModel);

                    var readStudentDto = _mapper.Map<ReadStudentDTO>(studentModel);
                    _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), "Eshte shtuar nje student i ri");
                    return Ok(new DataError("Studenti eshte regjistruar me sukses!"));
                }
                catch (Exception ex)
                {
                    _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), "Deshtim ne regjistrimin e nje studenti");
                    return BadRequest(new DataError("Regjistrimi deshtoi " + ex.Message));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateStudent")]
        public async Task<IActionResult> UpdateStudentAsync(int id, UpdateStudentDTO updateStudentDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var student = _mapper.Map<Student>(updateStudentDTO);
                    await _context.Update(id, student);
                    _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), $"Eshte edituar studenti me id: {id}");
                    return Ok("Studenti eshte edituar me sukses!");
                }
                catch (Exception ex)
                {
                    _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), $"Deshtim ne editimin e studentit me id: {id}");
                    return BadRequest(new DataError("Editimi deshtoi " + ex.Message));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteStudent")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] int id)
        {
            try
            {
                var student = await _context.Get(id);

                student.Exams.Clear();

                await _context.Remove(id);
                _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), $"Eshte fshire studenti me id: {id}");
                return Ok("Studenti eshte fshire me sukses!");
            }
            catch (Exception ex)
            {
                _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), $"Deshtim ne editimin e studentit me id: {id}");
                return BadRequest(new DataError("Fshirja deshtoi " + ex.Message));
            }

        }
    }
}