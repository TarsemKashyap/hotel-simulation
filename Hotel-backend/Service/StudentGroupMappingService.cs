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
            return null;
        }
    }
}

