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
         private readonly IAccountService _accountService;
        private readonly IValidator<StudentSignupDto> _accountValidator;
        public StudentSignupController(IStudentSignupTempService studentSignupTempService,
            IValidator<StudentSignupTempDto> validator,
            IClassSessionService classSessionService,
            IEmailService emailService,
            IOptions<PaymentConfig>  paymentConfig,
            IPaymentService paymentService,
       IAccountService accountService, IValidator<StudentSignupDto> accountValidator)
      
        {
            _validator = validator;
            _studentSignupTempService = studentSignupTempService;
            _classSessionService = classSessionService;
            _emailService = emailService;
            _PaymentConfig = paymentConfig.Value;
            _paymentService = paymentService;
            _accountService = accountService;
            _accountValidator = accountValidator;
        }

        [HttpPost("studentsignup"), AllowAnonymous]
        public async Task<IActionResult> Create(StudentSignupTempDto dto)
        {

            _validator.ValidateAndThrow(dto);
            var record = _classSessionService.ClassList().FirstOrDefault(r => r.Code == dto.ClassCode);

            if (record != null)
            {
                var response = await _studentSignupTempService.Create(dto);
                return Ok(response);
            }
            else
            {
                throw new Service.ValidationException("Invalid Class Code");
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
        [HttpGet("{referenceId}"), AllowAnonymous]
        public async Task<IActionResult> GetByReferenceId(string referenceId)
        {
            var signupUser = await _studentSignupTempService.GetByReferenceId(referenceId);
            if (signupUser.IsSignupComplete == true)
            {
                throw new Service.ValidationException("Account already has been created");
            }
            return Ok(signupUser);

        }

        [HttpPost("student"), AllowAnonymous]
        public async Task<ActionResult> StudentSignup(StudentSignupDto account)
        {
            _accountValidator.ValidateAndThrow(account);
            var student = new StudentSignupDto()
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                Password = account.Password,
                ClassCode = account.ClassCode,
                Reference = account.Reference,

            };
            await _accountService.StudentAccount(student);
            return Ok(student);


        }
    }
}
