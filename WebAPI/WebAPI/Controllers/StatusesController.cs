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
    public class StatusesController : ControllerBase
    {
        private readonly IStatusRepository _context;
        private readonly IMapper _mapper;
        private readonly ILogsService _log;
        private IHttpContextAccessor _httpContextAccessor;

        public StatusesController(IStatusRepository context, IMapper mapper, ILogsService log, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _log = log;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = "GetStatuses")]
        public async Task<IActionResult> GetStatusesAsync()
        {
            var professorItems = await _context.GetAll();
            _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), "Eshte nxjerrur lista e statuseve");
            return Ok(_mapper.Map<IEnumerable<ReadStatusesDTO>>(professorItems));
        }
    }
}
