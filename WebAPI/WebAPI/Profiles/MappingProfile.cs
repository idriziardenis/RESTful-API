using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Profiles
{
    public class MappingProfile : Profile
    {
        // Source -> Destination
        public MappingProfile()
        {
            // Student
            CreateMap<AddStudentDTO, Student>();
            CreateMap<Student, ReadStudentDTO>();
            CreateMap<UpdateStudentDTO, Student>();
            CreateMap<Student, UpdateStudentDTO>();

            // User
            CreateMap<User, ReadUserDTO>();

            // Professor
            CreateMap<Professor, ReadProffesorDTO>();

            // Subjects
            CreateMap<Subject, ReadSubjectDTO>();

            // Exam
            CreateMap<AddExamDTO, Exam>();
            CreateMap<Exam, ReadExamDTO>();

            // Departament
            CreateMap<ReadDepartamentDTO, Department>();
            CreateMap<Department, ReadDepartamentDTO>();

            // Status
            CreateMap<ReadStatusesDTO, Status>();
            CreateMap<Status, ReadStatusesDTO>();
        }
    }
}
