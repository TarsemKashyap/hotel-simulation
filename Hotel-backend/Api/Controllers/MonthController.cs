﻿using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using Service;

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

        [HttpPost("edit/{classId}")]
        public async Task<IActionResult> CreateGroup(int classId, ClassGroupDto[] classGroup)
        {
            //foreach (var item in classGroup)
            //{
            //    _classGroupValidator.ValidateAndThrow(item);
            //}
            //var clasGroupResult = await _classSessionService.AddGroupAsync(classId, classGroup);
            //return Ok(clasGroupResult);
            return Ok("OK");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //var appUser = await _classSessionService.GetById(id);
            //return Ok(appUser);
            return Ok("OK");

        }

        [HttpPost("editMonth/{id}")]
        public async Task<IActionResult> ClassUpdate(int id, ClassSessionUpdateDto account)
        {
            //_validator.ValidateAndThrow(account);
            //var response = await _classSessionService.Update(id, account);
            ////return Ok(response);
            return Ok("OK");

        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> ClassDelete(int id)
        {
            //await _classSessionService.DeleteId(id);
            //return Ok();
            return Ok("OK");

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



        #region Comment
        // GET: MonthController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: MonthController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: MonthController/Create
        //public async Task<IActionResult> Create()
        //{
        //    return Ok();
        //}

        //// POST: MonthController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: MonthController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: MonthController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: MonthController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: MonthController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        #endregion
    }
}
