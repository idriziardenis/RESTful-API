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
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfessorsController : ControllerBase
    {
        private readonly IProfessorsRepository _context;
        private readonly IMapper _mapper;
        private readonly ILogsService _log;
        private IHttpContextAccessor _httpContextAccessor;

        public ProfessorsController(IProfessorsRepository context, IMapper mapper, ILogsService log, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _log = log;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = "GetProfessors")]
        public async Task<IActionResult> GetProfessorsAsync()
        {
            var professorItems = await _context.GetAll();
            _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), "Eshte nxjerrur lista e profesoreve");
            return Ok(_mapper.Map<IEnumerable<ReadProffesorDTO>>(professorItems));
        }
    }
}