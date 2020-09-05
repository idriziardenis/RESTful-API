using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectsRepository _context;
        private readonly IMapper _mapper;
        private readonly ILogsService _log;
        private IHttpContextAccessor _httpContextAccessor;

        public SubjectsController(ISubjectsRepository context, IMapper mapper, ILogsService log, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _log = log;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = "GetSubjects")]
        public async Task<IActionResult> GetSubjectsAsync()
        {
            var subjectItems = await _context.GetAll();
            _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), "Eshte nxjerrur lista e lendeve");
            return Ok(_mapper.Map<IEnumerable<ReadSubjectDTO>>(subjectItems));
        }
    }
}