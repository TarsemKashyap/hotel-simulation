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
        IEnumerable<StudentClassMappingDto> StudentList(string studentId);
        IEnumerable<StudentClassMappingDto> GetMissingClassList();
        Task<StudentClassMappingDto> IsDefaultUpdate(string studentId, bool isDefault);
        Task<StudentClassMappingDto> StudentAssignClass(StudentClassMappingDto classSession);
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
                    Code = x.Class.Code,
                    Institute = x.Student.Institue,
                    StudentId = x.StudentId
                })
                .ToList();

            return studentList;
        }

        public IEnumerable<StudentClassMappingDto> StudentList(string studentId)
        {
            var studentList = _context.StudentClassMapping
                .Include(x => x.Class)
                .Include(x => x.Student)
                .Where(x => x.StudentId == studentId)
                .Select(x => new StudentClassMappingDto
                {
                    Code = x.Class.Code,
                    Title = x.Class.Title,
                    CreatedBy = $"{x.Student.FirstName} {x.Student.LastName}",
                    StartDate = x.Class.StartDate,
                    EndDate = x.Class.EndDate
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
                LastName = studentSignup.Student.LastName
            };

            return studentSignupDto;
        }
        public async Task<StudentClassMappingDto> IsDefaultUpdate(string studentId, bool isDefault)
        {
            var studentData = await _context.StudentClassMapping.FirstOrDefaultAsync(x => x.StudentId == studentId);

            if (studentData != null)
            {
                studentData.isDefault = isDefault;
                await _context.SaveChangesAsync();
            }

            // Mapping the entity to the DTO class
            var studentDto = new StudentClassMappingDto
            {
                StudentId = studentData.StudentId,
                IsDefault = studentData.isDefault
            };

            return studentDto;
        }

        public IEnumerable<StudentClassMappingDto> GetMissingClassList()
        {
            var allClassTitles = _context.ClassSessions.Select(c => c.Title).ToList();

            var existingClassTitles = _context.StudentClassMapping
                .Include(x => x.Class)
                .Select(x => x.Class.Title)
                .ToList();

            var missingClassTitles = allClassTitles.Except(existingClassTitles).ToList();

            var missingClassList = missingClassTitles.Select(title => new StudentClassMappingDto
            {
                Title = title
            }).ToList();

            return missingClassList;
        }

        public async Task<StudentClassMappingDto> StudentAssignClass(StudentClassMappingDto studentClassMappingDto)
        {
            var classEntity = _context.ClassSessions.FirstOrDefault(x => x.Title == studentClassMappingDto.Title);
            studentClassMappingDto.ClassId = classEntity.ClassId;
            StudentClassMapping session = _mapper.Map<StudentClassMappingDto, StudentClassMapping>(studentClassMappingDto);
            _context.StudentClassMapping.Add(session);
            await _context.SaveChangesAsync();
            return _mapper.Map<StudentClassMappingDto>(session);

        }


    }

}
