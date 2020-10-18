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

namespace WebMVC.Repositorys
{
    public class StatusRepository : IStatusRepository
    {
        private static string WebAPIUrl = "http://localhost:59249/";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        public StatusRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<(bool, IEnumerable<ReadStatusesDTO>)> GetAll()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ReadStatusesDTO> statuses = null;
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

                var responseMessage = await client.GetAsync(requestUri: "/api/Statuses");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    statuses = JsonConvert.DeserializeObject<IEnumerable<ReadStatusesDTO>>(resultMessage);
                }
                else
                {

                }

                return (responseMessage.IsSuccessStatusCode, statuses);
            }
        }
    }
}
