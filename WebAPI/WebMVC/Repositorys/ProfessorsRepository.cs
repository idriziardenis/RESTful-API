using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebMVC.DTOs;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Repositorys
{
    public class ProfessorsRepository : IProfessorsRepository
    {
        private static string WebAPIUrl = "http://localhost:59249/";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        public ProfessorsRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Professor> Add(Professor t)
        {
            throw new NotImplementedException();
        }

        public Task<Professor> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Professor> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Professor> Update(int id, Professor newT)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, IEnumerable<ReadProffesorDTO>)> GetAll()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ReadProffesorDTO> professors = null;
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                var token = Session.GetString("Token");

                if (string.IsNullOrEmpty(token))
                {
                    return (false, null);
                }
                //else
                //{
                //    var isValid = _authenticateService.IsValidTokenAsync(token).Result;
                //    if(!isValid)
                //    {
                //        return (false, null);
                //    }
                //}

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

                var responseMessage = await client.GetAsync(requestUri: "/api/Professors");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    professors = JsonConvert.DeserializeObject<IEnumerable<ReadProffesorDTO>>(resultMessage);
                }
                else
                {

                }

                return (responseMessage.IsSuccessStatusCode, professors);
            }
        }

    }
}
