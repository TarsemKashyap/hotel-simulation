using Common.Dto;
using Database;
using Database.Domain;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Service
{
    public interface IStudentSignupTempService
    {
        Task<StudentSignupTempDto> Create(StudentSignupTempDto studentSignupTemp);
        Task<StudentSignupTempDto> Update(StudentSignupTempDto studentSignupTemp);
        Task<StudentSignupTempDto> GetById(Guid studentId);

        Task<StudentSignupTempDto> GetByRefrence(string refrenceId);
        Task<StudentSignupTempDto> GetByReferenceId(string referenceId);
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
            studentSignupTemp.Reference = Guid.NewGuid().ToString("N");
            StudentSignupTemp signup = _mapper.Map<StudentSignupTempDto, StudentSignupTemp>(studentSignupTemp);
            _context.StudentSignupTemp.Add(signup);
            await _context.SaveChangesAsync();
            return _mapper.Map<StudentSignupTempDto>(signup);


        }

        public async Task<StudentSignupTempDto> Update(StudentSignupTempDto studentSignupTemp)
        {
            try
            {
                studentSignupTemp.PaymentDate = DateTime.Now;

                StudentSignupTemp signup = _mapper.Map<StudentSignupTempDto, StudentSignupTemp>(studentSignupTemp);
                
                //Entry<signup>(studentSignupTemp).State = EntityState.Detached;
                _context.Entry(signup).State = EntityState.Detached;
                //var result = _context.StudentSignupTemp.Update(signup);

                await _context.SaveChangesAsync();
                return _mapper.Map<StudentSignupTempDto>(signup);
            }
            catch (Exception ex)
            {
                return new StudentSignupTempDto();
            }


        }

        public async Task<StudentSignupTempDto> GetByRefrence(string refrenceId)
        {
            var studentSignup = _context.StudentSignupTemp.FirstOrDefault(x => x.Reference == refrenceId);
            ;
            if (studentSignup == null)
                throw new ValidationException("student not found for given student Id");
            return studentSignup.Adapt<StudentSignupTempDto>();
        }

        public async Task<StudentSignupTempDto> GetById(Guid studentId)
        {
            var studentSignup = _context.StudentSignupTemp.FirstOrDefault(x => x.Id == studentId);
            ;
            if (studentSignup == null)
                throw new ValidationException("student not found for given student Id");
            return studentSignup.Adapt<StudentSignupTempDto>();
        }

        public async Task<StudentSignupTempDto> GetByReferenceId(string referenceId)
        {
            var studentSignup = _context.StudentSignupTemp.FirstOrDefault(x => x.Reference == referenceId);
            ;
            if (studentSignup == null)
                throw new ValidationException("student not found for given student referenceId");
            return studentSignup.Adapt<StudentSignupTempDto>();
        }
    }
}
