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
        private readonly IAuthenticateService _authenticateService;
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        public StudentsRepository(IHttpContextAccessor httpContextAccessor, IAuthenticateService authenticateService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticateService = authenticateService;
        }

        public async Task<(bool, string)> Add(AddStudentDTO student)
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
                var responseMessage = await client.PostAsJsonAsync<AddStudentDTO>(requestUri: "/api/Students/AddStudent", student);
                var resultMessage = await responseMessage.Content.ReadAsStringAsync();
                message = JsonConvert.DeserializeObject<DataMessage>(resultMessage);

                return (responseMessage.IsSuccessStatusCode, message.Message);
            }
        }

        public async Task<(bool, ReadStudentDTO)> GetById(int id)
        {
            using (var client = new HttpClient())
            {
                ReadStudentDTO student = null;
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

                var responseMessage = await client.GetAsync(requestUri: "/api/Students/GetSingleStudent/" + id);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = await responseMessage.Content.ReadAsStringAsync();
                    student = JsonConvert.DeserializeObject<ReadStudentDTO>(resultMessage);
                }
                else
                {

                }

                return (responseMessage.IsSuccessStatusCode, student);
            }
        }

        public async Task<(bool, IEnumerable<ReadStudentDTO>)> GetAll()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ReadStudentDTO> students = null;
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

                var responseMessage = await client.GetAsync(requestUri: "/api/Students/GetStudents");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = await responseMessage.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<IEnumerable<ReadStudentDTO>>(resultMessage);
                }
                else
                {

                }

                return (responseMessage.IsSuccessStatusCode, students);
            }
        }


        public async Task<(bool,string)> Update(int id, ReadStudentDTO newT)
        {
            using (var client = new HttpClient())
            {
                DataMessage message = null;
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                var token = Session.GetString("Token");
                var UpdateStudentDTO = new UpdateStudentDTO() { Name = newT.Name, Surname = newT.Surname, DateOfBirth = newT.DateOfBirth, DepartmentId = newT.DepartmentId, Index = newT.Index, StatusId = newT.StatusId }; 
                if (string.IsNullOrEmpty(token))
                {
                    return (false, "You are not logged in!");
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);
                var responseMessage = await client.PutAsJsonAsync<UpdateStudentDTO>(requestUri: "/api/Students/UpdateStudent/"+id, UpdateStudentDTO);
                var resultMessage = await responseMessage.Content.ReadAsStringAsync();
                message = JsonConvert.DeserializeObject<DataMessage>(resultMessage);

                return (responseMessage.IsSuccessStatusCode, message.Message);
            }
        }

        public async Task<(bool, IEnumerable<ReadStudentDTO>)> GetLastFive()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ReadStudentDTO> students = null;
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

                var responseMessage = await client.GetAsync(requestUri: "/api/Students/GetLastFiveStudents");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = await responseMessage.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<IEnumerable<ReadStudentDTO>>(resultMessage);
                }
                else
                {

                }

                return (responseMessage.IsSuccessStatusCode, students);
            }
        }

        public async Task<(bool, string)> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                DataMessage dm = null;
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

                var responseMessage = await client.DeleteAsync(requestUri: "/api/Students/DeleteStudent/"+id);

                var resultMessage = await responseMessage.Content.ReadAsStringAsync();
                dm = JsonConvert.DeserializeObject<DataMessage>(resultMessage);

                return (responseMessage.IsSuccessStatusCode, dm.Message);
            }
        }
    }
}
