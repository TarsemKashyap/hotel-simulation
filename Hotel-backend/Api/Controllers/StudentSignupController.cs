using Common.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using Mysqlx;

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
        private readonly IEmailService _emailService;
        private readonly PaymentConfig _PaymentConfig;
        private readonly IPaymentService _paymentService;
        public StudentSignupController(IStudentSignupTempService studentSignupTempService,
            IValidator<StudentSignupTempDto> validator,
            IClassSessionService classSessionService,
            IEmailService emailService,
            IOptions<PaymentConfig>  paymentConfig,
            IPaymentService paymentService)
        {
            _validator = validator;
            _studentSignupTempService = studentSignupTempService;
            _classSessionService = classSessionService;
            _emailService = emailService;
            _PaymentConfig = paymentConfig.Value;
            _paymentService = paymentService;
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
           var result = await _paymentService.PaymentCheck(paymentTransactionDto);
           return Ok();

        }
    }
}
