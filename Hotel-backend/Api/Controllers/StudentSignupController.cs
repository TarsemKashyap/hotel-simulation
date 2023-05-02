using Common.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("signup")]
    public class StudentSignupController : AbstractBaseController
    {
        private readonly IValidator<StudentSignupTempDto> _validator;
        private readonly IStudentSignupTempService _studentSignupTempService;
        private readonly IClassSessionService _classSessionService;
        public StudentSignupController(IStudentSignupTempService studentSignupTempService,
            IValidator<StudentSignupTempDto> validator,
            IClassSessionService classSessionService)
        {
            _validator = validator;
            _studentSignupTempService = studentSignupTempService;
            _classSessionService = classSessionService;
        }

        [HttpPost("studentsignup"), AllowAnonymous]
        public async Task<IActionResult> Create(StudentSignupTempDto dto)
        {
            try
            {
                _validator.ValidateAndThrow(dto);
                var record = _classSessionService.ClassList().FirstOrDefault(r => r.Code == dto.ClassCode);

                if (record == null)
                {
                    var response = await _studentSignupTempService.Create(dto);
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Invalid class code");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("signup/{id}"), AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var signupUser = await _studentSignupTempService.GetById(id);
            return Ok(signupUser);

        }


        [HttpPost("paymentCheck"), AllowAnonymous]
        public async Task<IActionResult> PaymentStatus(PaymentTransactionDto paymentTransactionDto)
        {
            return Ok("Payment transaction processed successfully.");
        }
    }
}
