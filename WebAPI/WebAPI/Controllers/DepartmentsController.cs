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
    [Authorize]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartamentsRepository _context;
        private readonly IMapper _mapper;
        private readonly ILogsService _log;
        private IHttpContextAccessor _httpContextAccessor;

        public DepartmentsController(IDepartamentsRepository context, IMapper mapper, ILogsService log, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _log = log;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = "GetDepartaments")]
        public async Task<IActionResult> GetDepartamentsAsync()
        {
            var professorItems = await _context.GetAll();
            _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), "Eshte nxjerrur lista e departamenteve");
            return Ok(_mapper.Map<IEnumerable<ReadDepartamentDTO>>(professorItems));
        }

    }
}
