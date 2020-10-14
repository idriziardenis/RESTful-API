using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface IAuthenticateService
    {
        Task<(bool,string)> Authenticate(Authentication model);
        Task<bool> IsValidTokenAsync(string token);
    }
}
