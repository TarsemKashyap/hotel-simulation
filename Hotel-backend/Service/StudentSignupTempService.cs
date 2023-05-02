using Common.Dto;
using Database;
using Database.Domain;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IStudentSignupTempService
    {
        Task<StudentSignupTempDto> Create(StudentSignupTempDto studentSignupTemp);
        Task<StudentSignupTempDto> GetById(Guid studentId);
    }
    public class StudentSignupTempService: IStudentSignupTempService
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _context;
        public StudentSignupTempService(IMapper mapper, HotelDbContext context) 
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<StudentSignupTempDto> Create(StudentSignupTempDto studentSignupTemp)
        {
            studentSignupTemp.CreatedDate = DateTime.Now;
            studentSignupTemp.PaymentDate = DateTime.Now;
            studentSignupTemp.Reference = Guid.NewGuid().ToString("N");
            StudentSignupTemp signup = _mapper.Map<StudentSignupTempDto, StudentSignupTemp>(studentSignupTemp);
            _context.StudentSignupTemp.Add(signup);
            await _context.SaveChangesAsync();
            return _mapper.Map<StudentSignupTempDto>(signup);


        }

        public async Task<StudentSignupTempDto> GetById(Guid studentId)
        {
            var studentSignup = _context.StudentSignupTemp.FirstOrDefault(x => x.Id == studentId);
            ;
            if (studentSignup == null)
                throw new ValidationException("student not found for given student Id");
            return studentSignup.Adapt<StudentSignupTempDto>();
        }
    }
}
