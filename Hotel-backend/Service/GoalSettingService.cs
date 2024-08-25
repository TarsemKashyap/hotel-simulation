using Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Service;
using Common.Dto;
using MapsterMapper;
using Mysqlx.Prepare;
using System;

public interface IGoalSettingService
{

    Task<GoalDto> GoalSettingDetails(int MonthID, int? groupId, int quarterNo);

    Task UpdateGoalSettings(GoalDto goalSettingDtos);

}

public class GoalSettingService : IGoalSettingService
{
    private readonly IMapper _mapper;
    private readonly HotelDbContext _context;

    public GoalSettingService(IMapper mapper, HotelDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task UpdateGoalSettings(GoalDto goalDto)
    {

        var goalDetails = _context.Goal.SingleOrDefault(x => x.ID == goalDto.ID);
        goalDetails.OccupancyM = Convert.ToDecimal(goalDto.OccupancyM) / 100;
        goalDetails.RoomRevenM = Convert.ToDecimal(goalDto.RoomRevenM);
        goalDetails.TotalRevenM = Convert.ToDecimal(goalDto.TotalRevenM);
        goalDetails.ShareRoomM = Convert.ToDecimal(goalDto.ShareRoomM) / 100;
        goalDetails.ShareRevenM = Convert.ToDecimal(goalDto.ShareRevenM) / 100;
        goalDetails.RevparM = Convert.ToDecimal(goalDto.RevparM);
        goalDetails.ADRM = Convert.ToDecimal(goalDto.ADRM);
        goalDetails.YieldMgtM = Convert.ToDecimal(goalDto.YieldMgtM) / 100;
        goalDetails.MgtEfficiencyM = Convert.ToDecimal(goalDto.MgtEfficiencyM) / 100;
        goalDetails.ProfitMarginM = Convert.ToDecimal(goalDto.ProfitMarginM) / 100;


        _context.Goal.Update(goalDetails);
        await _context.SaveChangesAsync();
    }

    public async Task<GoalDto> GoalSettingDetails(int MonthID, int? groupId, int quarterNo)
    {
        var goalDetails = _context.Goal.Where(c => c.MonthID == MonthID && c.GroupID == groupId && c.QuarterNo == quarterNo).Select(s => new GoalDto
        {
            OccupancyM = s.OccupancyM * 100,
            RoomRevenM = s.RoomRevenM,
            TotalRevenM = s.TotalRevenM,
            ShareRoomM = s.ShareRoomM * 100,
            ShareRevenM = s.ShareRevenM * 100,
            RevparM = s.RevparM,
            ADRM = s.ADRM,
            YieldMgtM = s.YieldMgtM * 100,
            MgtEfficiencyM = s.MgtEfficiencyM * 100,
            ProfitMarginM = s.ProfitMarginM * 100,
            GroupID = s.GroupID,
            QuarterNo = s.QuarterNo,
            ID = s.ID
        }).FirstOrDefault();

        return goalDetails;

    }


}