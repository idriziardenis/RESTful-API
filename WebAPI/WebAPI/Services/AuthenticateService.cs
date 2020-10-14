using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Shared;

namespace WebAPI.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        private readonly DBContext _context;
        //private readonly UserManager<User> userManager;
        //private readonly SignInManager<User> signInManager;

        public AuthenticateService(DBContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
            //this.userManager = userManager;
            //this.signInManager = signInManager;
        }
        public async Task<User> Authenticate(Authentication model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);

            if (user != null)
            {
                user.Token = TokenProvider.GenerateToken(user.Username, user.Id.ToString());
                user.TokenExpireTime = DateTime.UtcNow.AddMinutes(20);
                user.LastLogin = DateTime.UtcNow;
                _context.Entry(await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id)).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
                return user;
            }
            else
            {
                return null;
            }
        }

        public bool IsValidToken(string token)
        {
            try
            {
                ClaimsPrincipal principal = TokenProvider.GetPrincipal(token);
                if(principal == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //public Task<User> Authenticate(string userName, string password)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
