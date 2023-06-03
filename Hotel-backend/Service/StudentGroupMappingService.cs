﻿using Common.Dto;
using Database;
using Database.Domain;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IStudentGroupMappingService
    {
        Task<StudentRoleGroupAssign> UpsertStudentData(StudentRoleGroupAssign studentRoleMappingDto);
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
        public async Task<StudentRoleGroupAssign> UpsertStudentData(StudentRoleGroupAssign newRole)
        {


            var roleMapping = await _context.StudentRoleMapping.Where(x => x.StudentId == newRole.StudentId).ToListAsync();

            var classSession = await _context.StudentClassMapping.FirstOrDefaultAsync(x => x.StudentId == newRole.StudentId);
            classSession.GroupId = newRole.GroupId;
            _context.StudentClassMapping.Update(classSession);
            await _context.SaveChangesAsync();
            if (!roleMapping.Any())
            {
                var newRoles = newRole.Roles.Select(x => new StudentRoleMapping { RoleId = x, StudentId = newRole.StudentId });
                _context.StudentRoleMapping.AddRange(newRoles);
                await _context.SaveChangesAsync();
                return newRoles.Adapt<StudentRoleGroupAssign>();
            }



            // add new role

            var dbRoles = roleMapping.Select(x => x.RoleId);

            var newRolesToAdd = newRole.Roles.Except(dbRoles);

            var addnewRoleObject = newRole.Roles.Where(x => newRolesToAdd.Contains(x)).
                Select(x => new StudentRoleMapping { RoleId = x, StudentId = newRole.StudentId });

            _context.StudentRoleMapping.AddRange(addnewRoleObject);
            await _context.SaveChangesAsync();

            // remove roles

            var dbRolesAfterUpdate = (await _context.StudentRoleMapping.Where(x => x.StudentId == newRole.StudentId && !newRole.Roles.Contains(x.RoleId)).ToListAsync());


            _context.StudentRoleMapping.RemoveRange(dbRolesAfterUpdate);
            await _context.SaveChangesAsync();


            return newRole;
        }

    }
}

