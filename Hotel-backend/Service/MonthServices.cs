using Common.Dto;
using Database;
using Database.Migrations;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Service
{
    public interface IMonthService
    {
        Task<IEnumerable<MonthDto>> AddGroupAsync(int monthId, MonthDto[] monthGroup);
        IEnumerable<MonthDto> monthList();
        Task<ResponseData> Create(MonthDto month);
        IList<MonthDto> List(MonthDto month);
        Task<MonthDto> GetById(int sessionId);
        Task<MonthDto> Update(int id, MonthDto account);
        Task DeleteId(int sessionId);
        Task<ClassSessionDto> GetClassInfoById(int classId);
        Task<MonthDto> GetMonthInfoById(int classId, int quarterNo);
        Task<Boolean> updateMonthCompletedStatus(MonthDto mdt);
        Task<bool> UpdateClassStatus(ClassSessionDto csdt);
        Task<MonthDto> GetMonthDtlsByClassId(int classId);
    }
    public class MonthServices : IMonthService
    {
        private readonly IMapper _mapper;
        private readonly HotelDbContext _context;

        public MonthServices(IMapper mapper, HotelDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public Task<IEnumerable<MonthDto>> AddGroupAsync(int monthId, MonthDto[] monthGroup)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData> Create(MonthDto month)
        {

            ResponseData resObj = new ResponseData();
            string strjson = "";
            try
            {

                FunMonth objFunMonth = new FunMonth();

                int marketPercentage = Convert.ToInt16(month.TotalMarket);
                int classID = month.ClassId;
                ClassSessionDto classDetail = objFunMonth.GetClassDetailsById(classID, _context);
                int currentQuarter = classDetail.CurrentQuater;
                int numberOfHotels = classDetail.HotelsCount;
                int totMarket = marketPercentage * numberOfHotels * 500 * 30 / 100;
                int monthID = -1;
                using (var createMonthTrax = _context.Database.BeginTransaction())
                {
                    if (currentQuarter == 0)
                    {
                        objFunMonth.CreateMonth(_context, classID, -1, totMarket, true);
                        resObj.Message = "A new month has been created.";
                    }
                    monthID = objFunMonth.CreateMonth(_context, classID, currentQuarter, totMarket, false);
                    await createMonthTrax.CommitAsync();
                }
                // Change If Condition As Requirment 
                if (currentQuarter == 0)
                {
                    using (var firstMonthTrans = _context.Database.BeginTransaction())
                    {
                        await objFunMonth.CreateFirstMarketingDecision(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.CreateFirstPriceDecision(_context, monthID, currentQuarter, numberOfHotels, true);
                        await objFunMonth.CreateFirstPriceDecision(_context, monthID, currentQuarter, numberOfHotels, false);
                        await objFunMonth.CreateFirstAttributeDecision(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.CreateFirstRoomAllocation(_context, monthID, currentQuarter, numberOfHotels, true);
                        await objFunMonth.CreateFirstRoomAllocation(_context, monthID, currentQuarter, numberOfHotels, false);
                        await objFunMonth.CreateCustomerRawRating(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.CreateWeightedAttributeRating(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.CreateGoal(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.CreateIncomeState(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.CreateSoldRoomByChannel(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.CreateBalanceSheet(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.UpdateClassQuarter(_context, classID, currentQuarter);
                        await firstMonthTrans.CommitAsync();
                    }

                }
                else
                {

                    using (var newMonthTrax = _context.Database.BeginTransaction())
                    {

                        int lastMonthId = await _context.Months.LastMonthId(monthID);
                        await objFunMonth.CreateMarketingDecision(_context, monthID, currentQuarter, lastMonthId);
                        await objFunMonth.CreatePriceDecision(_context, monthID, currentQuarter, lastMonthId);
                        await objFunMonth.CreateAttributeDecision(_context, monthID, currentQuarter, lastMonthId);
                        await objFunMonth.CreateRoomAllocation(_context, monthID, currentQuarter, lastMonthId);
                        await objFunMonth.CreateCustomerRawRating(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.CreateWeightedAttributeRating(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.CreateGoal(_context, monthID, currentQuarter, numberOfHotels);

                        await objFunMonth.CreateIncomeState(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.CreateSoldRoomByChannel(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.CreateBalanceSheet(_context, monthID, currentQuarter, numberOfHotels);
                        await objFunMonth.UpdateClassQuarter(_context, classID, currentQuarter);
                        await newMonthTrax.CommitAsync();

                    }
                }
                resObj.Message = "Month Create";
                strjson = "{ monthID:" + monthID + "}";
            }
            catch (Exception ex)
            {
                resObj.Message = ex.ToString();
            }


            var jobj = JsonConvert.DeserializeObject<MonthDto>(strjson)!;

            resObj.StatusCode = 200;
            resObj.Data = jobj;
            return resObj;


        }

        public Task DeleteId(int sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<MonthDto> GetById(int sessionId)
        {
            throw new NotImplementedException();
        }

        public async Task<ClassSessionDto> GetClassInfoById(int classId)
        {

            var data = _context.ClassSessions.SingleOrDefault(x => x.ClassId == classId);
            if (data == null)
            {
                return new ClassSessionDto();
            }
            // throw new ValidationException("data not found ");
            return data.Adapt<ClassSessionDto>();
           

        }

        public async Task<MonthDto> GetMonthInfoById(int classId, int quarterNo)
        {

            return (from c in _context.ClassSessions
                    join m in _context.Months on c.ClassId equals m.ClassId
                    where (c.ClassId == classId && m.Sequence == quarterNo)
                    select new MonthDto
                    {
                        MonthId = m.MonthId,
                        ClassId = m.ClassId,
                        Sequence = m.Sequence,
                        TotalMarket = m.TotalMarket,
                        ConfigId = m.ConfigId,
                        IsComplete = m.IsComplete,
                        Status = c.Status.ToString(),
                        StatusText = CalculationServices.GetCompletionStatus(c.Status, m.IsComplete)
                    }
                    ).FirstOrDefault();


        }

        public IList<MonthDto> List(MonthDto month)
        {

            return (from c in _context.ClassSessions
                    join m in _context.Months on c.ClassId equals m.ClassId
                    where (c.ClassId == month.ClassId && m.Sequence > 0)
                    select new MonthDto
                    {
                        MonthId = m.MonthId,
                        ClassId = m.ClassId,
                        Sequence = m.Sequence,
                        TotalMarket = m.TotalMarket,
                        ConfigId = m.ConfigId,
                        IsComplete = m.IsComplete,
                        Status = c.Status.ToString(),
                        StatusText = CalculationServices.GetCompletionStatus(c.Status, m.IsComplete)
                    }
                     ).ToList();


        }

        public IEnumerable<MonthDto> monthList()
        {
            throw new NotImplementedException();
        }

        public Task<MonthDto> Update(int id, MonthDto account)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> updateMonthCompletedStatus(MonthDto mdt)
        {

            FunMonth obj = new FunMonth();
            bool result = obj.UpdateMonthCompletedStatus(_context, mdt.ClassId, mdt.Sequence);
            return result;
        }

        public async Task<MonthDto> GetMonthDtlsByClassId(int classId)
        {
            var month = _context.Months.Where(x => x.ClassId == classId).OrderByDescending(o => o.MonthId).FirstOrDefault();
            if (month == null)
                throw new ValidationException("No months found");
            return month.Adapt<MonthDto>();
        }
        public async Task<bool> UpdateClassStatus(ClassSessionDto csdt)
        {

            FunMonth obj = new FunMonth();
            bool result = obj.UpdateClassStatus(_context, csdt.ClassId, csdt.Status);
            return result;
        }
        //UpdateClassStatus
    }
}

