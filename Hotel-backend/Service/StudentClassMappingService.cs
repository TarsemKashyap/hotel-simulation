using Common.Dto;
using Database;
using Database.Domain;
using Database.Migrations;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IStudentClassMappingService
    {
        IEnumerable<StudentClassMappingDto> List(int ClassId);
        Task<StudentClassMappingDto> GetById(Guid studentId);
    }
    public class StudentClassMappingService : IStudentClassMappingService
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _context;
        public StudentClassMappingService(IMapper mapper, HotelDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public IEnumerable<StudentClassMappingDto> List(int ClassId)
        {
            var studentList = _context.StudentClassMapping
                .Include(x => x.Class)
                .Include(x => x.Student)
                .Where(x => x.ClassId == ClassId)
                .Select(x => new StudentClassMappingDto
                {
                    Id = x.Id,
                    Title = x.Class.Title,
                    FirstName = x.Student.FirstName,
                    LastName = x.Student.LastName,  
                    Email = x.Student.Email,
                    ClassCode = x.Class.Code,
                    Institute = x.Student.Institue,
                    StudentId = x.StudentId
                    // add any other properties you want to include in the DTO
                })
                .ToList();

           return studentList;
        }

        public async Task<StudentClassMappingDto> GetById(Guid id)
        {
            var studentSignup = _context.StudentClassMapping.Include(x => x.Class)
                .Include(x => x.Student).FirstOrDefault(x => x.Id == id);
            if (studentSignup == null)
                throw new ValidationException("student not found for given student id");
            var studentSignupDto = new StudentClassMappingDto
            {
                Id = studentSignup.Id,
                StudentId = studentSignup.StudentId,
                ClassId = studentSignup.ClassId,
                Title = studentSignup.Class.Title,
                FirstName = studentSignup.Student.FirstName,
                LastName= studentSignup.Student.LastName
            };

            return studentSignupDto;
        }


    }

}
