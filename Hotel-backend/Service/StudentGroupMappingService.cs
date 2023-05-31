using Common.Dto;
using Database;
using Database.Domain;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IStudentGroupMappingService
    {
        Task<StudentRoleMappingDto> UpsertStudentData(StudentRoleMappingDto studentRoleMappingDto);
    }
    public class StudentGroupMappingService : IStudentGroupMappingService
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _context;
        public StudentGroupMappingService(IMapper mapper, HotelDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<StudentRoleMappingDto> UpsertStudentData(StudentRoleMappingDto studentRoleMappingDto)
        {
            var classSessionEntity = _context.StudentRoleMapping.Where(x => x.StudentId == studentRoleMappingDto.StudentId).ToList();

            if (classSessionEntity.Count == 0)
            {
                var added = studentRoleMappingDto.Roles
                    .Where(x => !classSessionEntity.Any(y => y.RoleId == x.roleId))
                    .Select(x => new StudentRoleMapping
                    {
                        RoleId = x.roleId,
                        StudentId = studentRoleMappingDto.StudentId,
                    })
                    .ToList();

                 classSessionEntity.AddRange(added);

                var groupId = new StudentClassMapping
                {
                    StudentId = studentRoleMappingDto.StudentId,
                    GroupId = studentRoleMappingDto.GroupId,
                };

                var existClassMapping = _context.StudentClassMapping.FirstOrDefault(x => x.StudentId == studentRoleMappingDto.StudentId);
                if (existClassMapping != null)
                {
                    existClassMapping.GroupId = studentRoleMappingDto.GroupId;
                    _context.StudentClassMapping.Update(existClassMapping);
                }
                /*else
                {
                    _context.StudentClassMapping.Add(groupId);
                }*/

                await _context.SaveChangesAsync();

                var deleteItems = classSessionEntity
                    .Where(x => !studentRoleMappingDto.Roles.Any(r => r.roleId == x.RoleId))
                    .ToList();

                _context.StudentRoleMapping.RemoveRange(deleteItems);
                await _context.SaveChangesAsync();
            }

            return studentRoleMappingDto;
        }

    }
}

