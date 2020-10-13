using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Interfaces;
using WebMVC.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebMVC.DTOs;

namespace WebMVC.Repositorys
{
    public class StudentsRepository : IStudentsRepository
    {
        private static string WebAPIUrl = "http://localhost:59249/";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        public StudentsRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Student> Add(Student t)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool,IEnumerable<ReadStudentDTO>)> GetAll()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ReadStudentDTO> students = null;
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                var token = Session.GetString("Token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

                var responseMessage = await client.GetAsync(requestUri: "/api/Students/GetStudents");

                if(responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    students = JsonConvert.DeserializeObject<IEnumerable<ReadStudentDTO>>(resultMessage);
                }
                else
                {

                }

                return (responseMessage.IsSuccessStatusCode, students);
            }
        }

        public IEnumerable<Student> GetLastFive()
        {
            throw new NotImplementedException();
        }

        public Task<Student> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Update(int id, Student newT)
        {
            throw new NotImplementedException();
        }
    }
}
