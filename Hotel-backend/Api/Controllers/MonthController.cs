using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using Service;
using System;

namespace Api.Controllers
{
    [ApiController]
    // [Authorize]
    [Route("month")]
    public class MonthController : AbstractBaseController
    {
        private readonly IValidator<MonthDto> _validator;
        // private readonly IValidator<ClassGroupDto> _classGroupValidator;
        private readonly IMonthService _monthService;

        public MonthController(IMonthService monthService)
        {
            //_validator = validator;
            //_classGroupValidator = classGroupValidator;
            _monthService = monthService;
            //_validator = validator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(MonthDto dto)
        {
            //_validator.ValidateAndThrow(dto);
            // dto.CreatedBy = LoggedUserId;

            try
            {
                var response = await _monthService.Create(dto);
                return Ok(response);

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return Ok(message);
            }
            //return Ok("OK");
        }


        [HttpPost("list")]
        public IActionResult MonthList(MonthDto month)
        {
            // string instructorId = IsAdmin ? null : LoggedUserId;

            var MonthResult = _monthService.List(month);
            return Ok(MonthResult);

        }
        [HttpGet("classInfo/{classId}")]
        public async Task<ClassSessionDto> ClassInfo(int classId)
        {
            // string instructorId = IsAdmin ? null : LoggedUserId;

            ClassSessionDto MonthResult = await _monthService.GetClassInfoById(classId);
            return MonthResult;

        }
        [HttpGet("monthInfo/{classId}/{quarterNo}")]
        public Task<MonthDto> MonthInfo(int classId, int quarterNo)
        {
            // string instructorId = IsAdmin ? null : LoggedUserId;
            var MonthResult = _monthService.GetMonthInfoById(classId, quarterNo);
            return MonthResult;

        }

        [HttpPost("UpdateMonthCompletedStatus")]
        public async Task<bool> UpdateMonthCompletedStatus(MonthDto dto)
        {
            //_validator.ValidateAndThrow(dto);
            // dto.CreatedBy = LoggedUserId;


            var response = await _monthService.updateMonthCompletedStatus(dto);
            return response;
            //return Ok("OK");
        }
        //UpdateClassStatus

        [HttpPost("UpdateClassStatus")]
        public async Task<bool> UpdateClassStatus(ClassSessionDto dto)
        {
            //_validator.ValidateAndThrow(dto);
            // dto.CreatedBy = LoggedUserId;


            var response = await _monthService.UpdateClassStatus(dto);
            return response;

        }

    }
}
