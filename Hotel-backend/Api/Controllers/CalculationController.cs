using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("Calculation")]
    public class CalculationController : AbstractBaseController
    {
        private readonly IValidator<MonthDto> _validator;

        private readonly ICalculationServices _calculationService;
        public CalculationController(ICalculationServices calculationService)
        {

            _calculationService = calculationService;

        }
        [HttpPost("Calculation")]
        public async Task<IActionResult> Calculation(MonthDto dto)
        {
            //_validator.ValidateAndThrow(dto);
            // dto.CreatedBy = LoggedUserId;


            var response = await _calculationService.Calculation(dto);
            return Ok(response);
            //return Ok("OK");
        }
        [HttpPost("calculationList")]
        public async Task<IActionResult> CalculationList(MonthDto dto)
        {
            //_validator.ValidateAndThrow(dto);
            // dto.CreatedBy = LoggedUserId;


            var response = await _calculationService.List(dto);
            return Ok(response);
            //return Ok("OK");
        }
    }
}
