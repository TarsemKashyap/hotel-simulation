using Common.Dto;
using Database;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Microsoft.EntityFrameworkCore;
using Common;
using System.Data;

namespace Service
{
    public interface IStudentRolesMappingService
    {
        Task<StudentRoleGroupRequest> StudentRolesList(string studentId, int classId);
        Task<List<StudentRoleDto>> GetStudentRolesById(string studentId);
    }
    public class StudentRolesMappingService : IStudentRolesMappingService
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _context;
        private readonly IStudentClassMappingService _studentClassMappingService;

        public StudentRolesMappingService(IMapper mapper, HotelDbContext context, IStudentClassMappingService studentClassMappingService)
        {
            _mapper = mapper;
            _context = context;
            _studentClassMappingService = studentClassMappingService;
        }

        public async Task<List<StudentRoleDto>> GetStudentRolesById(string studentId)
        {
            var defaultClass = await _studentClassMappingService.GetDefaultByStudentID(studentId);
            var selectedRoles = _context.StudentRoleMapping.Where(x => x.StudentId == studentId && x.ClassId == defaultClass.ClassId).Join(_context.StudentRoles, mapping => mapping.RoleId, roles => roles.Id, (mapping, roles) => new StudentRoleDto
            {
                Id = mapping.RoleId,
                RoleName = roles.RoleName
            }).ToList();


            return selectedRoles;
        }

        public async Task<StudentRoleGroupRequest> StudentRolesList(string studentId, int classId)
        {
            var roles = _context.StudentRoles.ProjectToType<StudentRoleDto>().ToList();
            var groups = _context.ClassGroups.Where(x => x.ClassId == classId).ProjectToType<ClassGroupDto>().ToList();
            var selectedRoles = _context.StudentRoleMapping.Where(x => x.StudentId == studentId).Select(x => x.RoleId).ToList();
            var selectedGrupid = _context.StudentClassMapping.FirstOrDefault(x => x.StudentId == studentId && x.ClassId == classId);

            var request = new StudentRoleGroupRequest
            {
                StudentRole = roles,
                ClassGroups = groups,
                SelectedRoles = roles.Where(x => selectedRoles.Contains(x.Id)).ToList(),
                SelectedGroup = groups.FirstOrDefault(x => x.GroupId == selectedGrupid.GroupId)
            };

            return request;

        }
    }
}
