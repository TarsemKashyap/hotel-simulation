using Common.Dto;
using Common.Model;
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

        Task<AddRemoveClassDto> ClassesByStudent(string studentId);
        ClassOverviewDto List(int classId);
        Task<StudentClassMappingDto> GetById(Guid studentId);
        Task<StudentClassMappingDto> GetDefaultByStudentID(string studentId);
        IEnumerable<StudentClassMappingDto> StudentList(string studentId);
        IEnumerable<StudentClassMappingDto> GetMissingClassList();
        Task IsDefaultUpdate(string studentId, StudentClassMappingDto dto);
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
        public ClassOverviewDto List(int ClassId)
        {

            var studentList = _context.StudentClassMapping
                .Include(x => x.Class)
                .Include(x => x.Student).Include(x => x.ClassGroup)
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
                    StudentId = x.StudentId,
                    GroupName = x.ClassGroup.Name,
                    StartDate = x.Class.StartDate,
                    EndDate = x.Class.EndDate
                })
                .ToList();

            var classSessions = _context.ClassSessions.FirstOrDefault(x => x.ClassId == ClassId);

            var classDetails = new ClassSessionDto
            {
                Title = classSessions.Title,
                TotalStudentCount = studentList.Count,
                StartDate = classSessions.StartDate,
                EndDate = classSessions.EndDate,
            };



            var classOverview = new ClassOverviewDto
            {
                StudentClassMappingDto = studentList,
                ClassSessionDto = classDetails
            };
            return (classOverview);
        }

        public IEnumerable<StudentClassMappingDto> StudentList(string studentId)
        {
            var studentList = _context.StudentClassMapping
                .Include(x => x.Class)
                .Where(x => x.StudentId == studentId)
                .Select(x => new StudentClassMappingDto
                {
                    Code = x.Class.Code,
                    Title = x.Class.Title,
                    StartDate = x.Class.StartDate,
                    EndDate = x.Class.EndDate
                })
                .ToList();

            return studentList;
        }

        

        public async Task<StudentClassMappingDto> GetDefaultByStudentID(string studentID)
        {
            var studentSignup = _context.StudentClassMapping.Include(x => x.Class)
                .Include(x => x.Student).FirstOrDefault(x => x.StudentId == studentID && x.isDefault);
            if (studentSignup == null)
                throw new ValidationException("student not found for given student id");
            var studentSignupDto = new StudentClassMappingDto
            {
                Id = studentSignup.Id,
                ClassId = studentSignup.ClassId,
                GroupId = studentSignup.GroupId
            };

            return studentSignupDto;
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
        public async Task IsDefaultUpdate(string studentId, StudentClassMappingDto dto)
        {
            var studentData = await _context.StudentClassMapping.Where(x => x.StudentId == studentId).ToListAsync();

            var targetClassId = studentData.FindIndex(x => x.ClassId == dto.ClassId);

            foreach (var item in studentData)
            {
                item.isDefault = false;
            }
            studentData[targetClassId].isDefault = true;
            _context.StudentClassMapping.UpdateRange(studentData);
            _context.SaveChanges();
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

        public async Task<AddRemoveClassDto> ClassesByStudent(string studentId)
        {
            var selectedClass = await _context.StudentClassMapping.Include(x => x.Class).Where(x => x.StudentId == studentId).Select(x => x.Class).ToListAsync();

            var selectedClassIds = selectedClass.Select(x => x.ClassId);
            List<ClassSession> availableClasses = await _context.ClassSessions.Where(x => !selectedClassIds.Contains(x.ClassId)).ToListAsync();

            return new AddRemoveClassDto
            {
                SelectedClasses = selectedClass.Adapt<IEnumerable<ClassSessionDto>>(),
                AvailableClasses = availableClasses.Adapt<IEnumerable<ClassSessionDto>>(),
            };
        }
    }

}
