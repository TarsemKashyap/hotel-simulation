using Common.Dto;
using Database;
using Database.Migrations;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IMonthService
    {
        Task<IEnumerable<MonthDto>> AddGroupAsync(int monthId, MonthDto[] monthGroup);
        IEnumerable<MonthDto> monthList();
        Task<ResponseData> Create(MonthDto month);
        IEnumerable<MonthDto> List(string monthidId = null);
        Task<MonthDto> GetById(int sessionId);
        Task<MonthDto> Update(int id, MonthDto account);
        Task DeleteId(int sessionId);
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
            FunMonth objFunMonth = new FunMonth();
            ResponseData resObj = new ResponseData();
            int marketPercentage =Convert.ToInt16(month.TotalMarket);
            int classID = month.ClassId;
            ClassSessionDto classDetail = objFunMonth.GetClassDetailsById(classID, _context);
            int currentQuarter = classDetail.CurrentQuater;
            int numberOfHotels = classDetail.HotelsCount;
            int totMarket = marketPercentage * numberOfHotels * 500 * 30 / 100;
            int monthID = objFunMonth.CreateMonth(_context, classID, currentQuarter, totMarket);
            // Change If Condition As Requirment 
            if (monthID > 0)
            {
                if (currentQuarter == 0)
                {
                 //   objFunMonth.CreateMonth(_context, classID, currentQuarter, totMarket);
                    resObj.Message = "A new month has been created.";
                    int index = 1;


                    objFunMonth.CreateMarketingDecision(_context, monthID, currentQuarter, numberOfHotels);
                    objFunMonth.CreatePriceDecision(_context, monthID, currentQuarter, numberOfHotels);


                    if (currentQuarter == 0)
                    {

                        objFunMonth.CreateAttributeDecision(_context, monthID, currentQuarter, numberOfHotels);


                    }
                    if (currentQuarter > 0)
                    {
                        // Pending Work 
                    }

                   objFunMonth.CreateRoomAllocation(_context, monthID, currentQuarter, numberOfHotels);

                    //objFunMonth.CreateCustomerRawRating(_context, monthID, currentQuarter, numberOfHotels);

                    //////////////////////////////////////////////////
                    /////Create Weighted Attribute Table for New Quarter
                    /////////////////////////////////////////////////

                   // objFunMonth.CreateWeightedAttributeRating(_context, monthID, currentQuarter, numberOfHotels);


                    ////////////////////////////////////////////////
                    /////Insert income Statement template for new month
                    ///////////////////////////////////////////////
                    ///

                    if (currentQuarter == 0)
                    {
                       // objFunMonth.CreateIncomeState(_context, monthID, currentQuarter, numberOfHotels);
                    }
                    else
                    {
                        //Pending Work
                    }
                    /////////////////////////////////
                    //////Insert Goal template for new month
                    ////////////////////////////////
                    ///
                    //objFunMonth.CreateGoal(_context, monthID, currentQuarter, numberOfHotels);
                    ///////////////////////////////////////////////////////////////////
                    ///////////Insert sold room by channel table template for new month
                    ///////////////////////////////////////////////////////////////////
                    ///
                   // objFunMonth.CreateSoldRoomByChannel(_context, monthID, currentQuarter, numberOfHotels);
                    if (currentQuarter == 0)
                    {
                        //objFunMonth.CreateBalanceSheet(_context, monthID, currentQuarter, numberOfHotels);
                    }
                    else
                    {
                    }
                }
            }
            resObj.Message = "Month Create";
            resObj.StatusCode = 200;
            resObj.Data = "{monthID:" + monthID + "}";
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

        public IEnumerable<MonthDto> List(string monthId = null)
        {
            IQueryable<Month> query = _context.Months;
            if (!string.IsNullOrEmpty(monthId))
            {
                //query = query.Where(x => x.CreatedBy == monthidId);
            }
            var result = query.Select(x => new MonthDto
            {
                MonthId = x.MonthId,
                ClassId = x.ClassId
            });

            return result.AsEnumerable();
        }

        public IEnumerable<MonthDto> monthList()
        {
            throw new NotImplementedException();
        }

        public Task<MonthDto> Update(int id, MonthDto account)
        {
            throw new NotImplementedException();
        }
    }
}

