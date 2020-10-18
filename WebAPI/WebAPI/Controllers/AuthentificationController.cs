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
    public class AuthentificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authenticateService;
        private readonly ILogsService _log;
        private IHttpContextAccessor _httpContextAccessor;
        public AuthentificationController(IAuthenticateService authenticateService, IMapper mapper, ILogsService log, IHttpContextAccessor httpContextAccessor)
        {
            _authenticateService = authenticateService;
            _mapper = mapper;
            _log = log;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost()]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(Authentication model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new DataMessage("Model is not valid"));
            }

            var user = await _authenticateService.Authenticate(model);

            if (user != null)
            {
                _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), $"Eshte autentifikua useri me username: {user.Username}");
                return Ok(user.Token);
            }
            else
            {
                _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), $"Tentim i deshtuar per tu autentifku!");
                return BadRequest("Username or Password is incorrect");
            }
        }

        [HttpGet()]
        [AllowAnonymous]
        public IActionResult IsValidToken(string token)
        {
            if(_authenticateService.IsValidToken(token))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        #region Comment
        //public async Task<IActionResult> Authenticate(Authentication model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(new DataError("Model is not valid"));
        //    }

        //    var user = await _authenticateService.Authenticate(model);

        //    if (user != null)
        //    {
        //        _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), $"Eshte autentifikua useri me username: {user.Username}");
        //        return Ok(_mapper.Map<User, ReadUserDTO>(user));
        //    }
        //    else
        //    {
        //        _log.AddLog(Request, _httpContextAccessor, this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), $"Tentim i deshtuar per tu autentifku!");
        //        return BadRequest(new { message = "Username or Password is incorrect" });
        //    }
        //}
        #endregion
    }
}