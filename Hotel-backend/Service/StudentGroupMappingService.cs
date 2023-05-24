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
        Task<StudentGroupMapping> UpsertStudentData(StudentGroupMappingDto studentGroupMappingDto);
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
        public async Task<StudentGroupMapping> UpsertStudentData(StudentGroupMappingDto studentGroupMappingDto)
        {/*
            var existingMapping = await _context.StudentGroupMapping
                .SingleOrDefaultAsync(x => x.StudentId == studentGroupMappingDto.StudentId;

            if (existingMapping != null)
            {
                // Update existing roles
                existingMapping.RoleIds.Clear();
                existingMapping.RoleIds.AddRange(studentGroupMappingDto.RoleIds.Select(role => new StudentRoleMapping
                {
                    Id = role
                }));

                await _context.SaveChangesAsync();

                return existingMapping;
            }
            else
            {
                // Insert new mapping with roles
                var newMapping = new StudentGroupMapping
                {
                    StudentId = studentGroupMappingDto.studentId,
                    GroupId = studentGroupMappingDto.GroupId,
                    RoleIds = studentGroupMappingDto.RoleIds.Select(role => new StudentRoleMapping
                    {
                        Id = role
                    }).ToList()
                };

                _context.StudentGroupMapping.Add(newMapping);
                await _context.SaveChangesAsync();*/

                return null;
            }
        }
    }

