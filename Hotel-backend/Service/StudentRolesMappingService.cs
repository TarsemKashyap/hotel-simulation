using Common.Dto;
using Database;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IStudentRolesMappingService
    {
        Task<IList<StudentRoleMappingDto>> StudentRolesList();
    }
    public class StudentRolesMappingService : IStudentRolesMappingService
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _context;

        public StudentRolesMappingService(IMapper mapper, HotelDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IList<StudentRoleMappingDto>> StudentRolesList()
        {
            var users = _context.StudentRoleMapping.ToList();
            return users.Adapt<IList<StudentRoleMappingDto>>();

        }
    }
}
