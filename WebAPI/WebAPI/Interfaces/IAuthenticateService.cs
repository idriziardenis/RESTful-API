﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IAuthenticateService
    {
        Task<User> Authenticate(Authentication model);
        bool IsValidToken(string token);
    }
}
