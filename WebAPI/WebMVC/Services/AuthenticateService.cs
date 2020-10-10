using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Interfaces;
using WebMVC.Models;
using WebMVC.Shared;

namespace WebMVC.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private static string WebAPIUrl = "http://localhost:59249/";
        //private readonly UserManager<User> userManager;
        //private readonly SignInManager<User> signInManager;

        public async Task<(bool,string)> Authenticate(Authentication model)
        {
            try
            {
                var tokenBased = string.Empty;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.BaseAddress = new Uri(WebAPIUrl);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: "application/json"));

                    var responseMessage = await client.PostAsJsonAsync<Authentication>(requestUri: "/api/Authentification", model);

                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    tokenBased = JsonConvert.DeserializeObject<string>(resultMessage);

                    return (responseMessage.IsSuccessStatusCode, tokenBased);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
