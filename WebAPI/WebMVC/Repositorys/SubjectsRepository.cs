﻿using Microsoft.AspNetCore.Http;
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
    public class SubjectsRepository : ISubjectsRepository
    {
        private static string WebAPIUrl = "http://localhost:59249/";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public SubjectsRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Subject> Add(Subject t)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Update(int id, Subject newT)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, IEnumerable<ReadSubjectDTO>)> GetAll()
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ReadSubjectDTO> subjects = null;
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

                var responseMessage = await client.GetAsync(requestUri: "/api/Subjects");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    subjects = JsonConvert.DeserializeObject<IEnumerable<ReadSubjectDTO>>(resultMessage);
                }
                else
                {

                }

                return (responseMessage.IsSuccessStatusCode, subjects);
            }
        }
    }
}
