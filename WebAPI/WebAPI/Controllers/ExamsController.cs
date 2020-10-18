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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExamsController : ControllerBase
    {
        private readonly IExamsRepository _context;
        private readonly IMapper _mapper;
        private readonly ILogsService _log;
        private IHttpContextAccessor _httpContextAccessor;

        public ExamsController(IExamsRepository context, IMapper mapper, ILogsService log, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _log = log;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = "GetExams")]
        public async Task<IActionResult> GetExamsAsync()
        {
            var examItems = await _context.GetAll();
            _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), "Eshte nxjerrur lista e provimeve");
            return Ok(_mapper.Map<IEnumerable<ReadExamDTO>>(examItems));
        }

        [HttpPost(Name = "AddExam")]
        public async Task<IActionResult> AddExamAsync(AddExamDTO addExamDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var examModel = _mapper.Map<Exam>(addExamDto);
                    await _context.Add(examModel);

                    var readExamDto = _mapper.Map<ReadExamDTO>(examModel);
                    _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), "Eshte regjistruar nje provim");
                    return Ok(new DataMessage("Provimi u regjistrua me sukses"));
                }
                catch (Exception ex)
                {
                    _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), "Deshtim ne regjistrimin e nje provimi.");
                    return BadRequest(new DataMessage("Insetimi deshtoi " + ex.Message));
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}