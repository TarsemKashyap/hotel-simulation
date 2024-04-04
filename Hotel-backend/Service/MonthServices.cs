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
            using (var transaction = _context.Database.BeginTransaction())
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
                    if (currentQuarter == 0)
                    {
                        objFunMonth.CreateMonth(_context, classID, currentQuarter - 1, totMarket, true);
                        resObj.Message = "A new month has been created.";
                    }
                    int monthID = objFunMonth.CreateMonth(_context, classID, currentQuarter, totMarket, false);
                    // Change If Condition As Requirment 
                    if (monthID > 0)
                    {
                        // if (currentQuarter == 0)
                        {

                            await objFunMonth.CreateMarketingDecision(_context, monthID, currentQuarter, numberOfHotels);
                            await objFunMonth.CreatePriceDecision(_context, monthID, currentQuarter, numberOfHotels, false);
                            await objFunMonth.CreatePriceDecision(_context, monthID, currentQuarter, numberOfHotels, true);
                            await objFunMonth.CreateAttributeDecision(_context, monthID, currentQuarter, numberOfHotels);
                            await objFunMonth.CreateRoomAllocation(_context, monthID, currentQuarter, numberOfHotels, true);
                            await objFunMonth.CreateRoomAllocation(_context, monthID, currentQuarter, numberOfHotels, false);
                            await objFunMonth.CreateCustomerRawRating(_context, monthID, currentQuarter, numberOfHotels);
                            //////////////////////////////////////////////////
                            /////Create Weighted Attribute Table for New Quarter
                            /////////////////////////////////////////////////
                            await objFunMonth.CreateWeightedAttributeRating(_context, monthID, currentQuarter, numberOfHotels);
                            ////////////////////////////////////////////////
                            /////Insert income Statement template for new month
                            ///////////////////////////////////////////////
                            ///

                            await objFunMonth.CreateIncomeState(_context, monthID, currentQuarter, numberOfHotels);

                            /////////////////////////////////
                            //////Insert Goal template for new month
                            ////////////////////////////////
                            ///
                            await objFunMonth.CreateGoal(_context, monthID, currentQuarter, numberOfHotels);
                            ///////////////////////////////////////////////////////////////////
                            ///////////Insert sold room by channel table template for new month
                            ///////////////////////////////////////////////////////////////////
                            ///
                            await objFunMonth.CreateSoldRoomByChannel(_context, monthID, currentQuarter, numberOfHotels);

                            await objFunMonth.CreateBalanceSheet(_context, monthID, currentQuarter, numberOfHotels);

                            await objFunMonth.UpdateClassQuarter(_context, classID, currentQuarter);
                        }


                    }
                    await transaction.CommitAsync();
                    resObj.Message = "Month Create";
                    strjson = "{ monthID:" + monthID + "}";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    resObj.Message = ex.ToString();
                }


                var jobj = JsonConvert.DeserializeObject<MonthDto>(strjson)!;

                resObj.StatusCode = 200;
                resObj.Data = jobj;
                return resObj;
            }


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
            //IQueryable<ClassSession> query = _context.ClassSessions.Where(x => x.ClassId == classId);
            //var result = query.Select(x => new ClassSessionDto
            //{
            //    ClassId = x.ClassId,
            //    Title = x.Title,
            //    Memo = x.Memo,
            //    StartDate = x.StartDate,
            //    EndDate = x.EndDate,
            //    HotelsCount = x.HotelsCount,
            //    RoomInEachHotel = x.RoomInEachHotel,
            //    CurrentQuater = x.CurrentQuater,
            //    CreatedOn = x.CreatedOn,
            //    Code = x.Code,
            //    CreatedBy = x.CreatedBy,
            //    Status = x.Status.ToString(),


            //}).ToList();
            //ClassSessionDto obj = new ClassSessionDto();
            //if (result.Count > 0)
            //{
            //    obj.ClassId = classId;
            //    obj.Title = result[0].Title;
            //    obj.Memo = result[0].Memo;
            //    obj.StartDate = result[0].StartDate;
            //    obj.EndDate = result[0].EndDate;
            //    obj.HotelsCount = result[0].HotelsCount;
            //    obj.RoomInEachHotel = result[0].RoomInEachHotel;
            //    obj.CurrentQuater = result[0].CurrentQuater;
            //    obj.CreatedOn = result[0].CreatedOn;
            //    obj.Code = result[0].Code;
            //    obj.CreatedBy = result[0].CreatedBy;
            //    obj.Status = result[0].Status.ToString();
            //}
            //return obj;

        }

        public async Task<MonthDto> GetMonthInfoById(int classId, int quarterNo)
        {
            var data = _context.Months.SingleOrDefault(x => x.ClassId == classId && x.Sequence == quarterNo);
            if (data == null)
            {
                return new MonthDto();
            }
            //    throw new ValidationException("data not found ");
            return data.Adapt<MonthDto>();
            //IQueryable<Month> query = _context.Months.Where(x => x.ClassId == classId && x.Sequence == quarterNo);
            //var result = query.Select(x => new MonthDto
            //{
            //    MonthId = x.MonthId,
            //    ClassId = x.ClassId,
            //    Sequence = x.Sequence,
            //    TotalMarket = x.TotalMarket,
            //    ConfigId = x.ConfigId,
            //    IsComplete = x.IsComplete
            //}).ToList();
            //MonthDto obj = new MonthDto();
            //if (result.Count > 0)
            //{
            //    obj.MonthId = result[0].MonthId;
            //    obj.ClassId = result[0].ClassId;
            //    obj.Sequence = result[0].Sequence;
            //    obj.TotalMarket = result[0].TotalMarket;
            //    obj.ConfigId = result[0].ConfigId;
            //    obj.IsComplete = result[0].IsComplete;
            //}
            //return obj;

        }

        public IList<MonthDto> List(MonthDto month)
        {

            return (from c in _context.ClassSessions
                    join m in _context.Months on c.ClassId equals m.ClassId
                    where (c.ClassId == month.ClassId)
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

