using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ExamsRepository : IExamsRepository
    {
        private static string WebAPIUrl = "http://localhost:59249/";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        public ExamsRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        
        public async Task<(bool,string)> Add(AddExamDTO exam)
        {
            using (var client = new HttpClient())
            {
                DataMessage message = null;
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                var token = Session.GetString("Token");

                if (string.IsNullOrEmpty(token))
                {
                    return (false, "You are not logged in!");
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);
                var responseMessage = await client.PostAsJsonAsync<AddExamDTO>(requestUri: "/api/Exams", exam);
                var resultMessage = await responseMessage.Content.ReadAsStringAsync();
                message = JsonConvert.DeserializeObject<DataMessage>(resultMessage);

                return (responseMessage.IsSuccessStatusCode, message.Message);
            }
        }

        public Task<Exam> Get(int id)
        {
            throw new NotImplementedException();
        }



        public Task<Exam> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Exam> Update(int id, Exam newT)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, IEnumerable<ReadExamDTO>)> GetAll()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ReadExamDTO> exams = null;
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
                //    var isValid = await _authenticateService.IsValidTokenAsync(token);
                //    if(!isValid)
                //    {
                //        return (false, null);
                //    }
                //}

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

                var responseMessage = await client.GetAsync(requestUri: "/api/Exams");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = await responseMessage.Content.ReadAsStringAsync();
                    exams = JsonConvert.DeserializeObject<IEnumerable<ReadExamDTO>>(resultMessage);
                }
                else
                {

                }

                return (responseMessage.IsSuccessStatusCode, exams);
            }
        }
    }
}
