using Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service;

using Common.Dto;
using Mapster;
using MapsterMapper;
using Newtonsoft.Json.Linq;

public interface IRoomAllocationService
    {
        
        Task<IList<RoomAllocationDto>> RoomAllocationDetails(int MonthID,int? groupId,int quarterNo);

        Task UpdateRoomAlocations(List<RoomAllocationDto> roomAllocationDto);
}

public class RoomAllocationService : IRoomAllocationService
{
    private readonly IMapper _mapper;
    private readonly HotelDbContext _context;

    public RoomAllocationService(IMapper mapper, HotelDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task UpdateRoomAlocations(List<RoomAllocationDto> roomAllocationDto)
    {
        var RoomAllocationList = new List<RoomAllocation>();
        foreach (var item in roomAllocationDto)
        {
            var roomAllocation = _context.RoomAllocation.SingleOrDefault(x => x.ID == item.ID);
            roomAllocation.RoomsAllocated = item.RoomsAllocated;
            RoomAllocationList.Add(roomAllocation);
           
        }
        _context.RoomAllocation.UpdateRange(RoomAllocationList);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<RoomAllocationDto>> RoomAllocationDetails(int MonthID, int? groupId, int quarterNo)
    {
        var roomAllocationDetails = _context.RoomAllocation.Where(c => c.MonthID == MonthID && c.GroupID == groupId && c.QuarterNo == quarterNo).Select(s=> new RoomAllocationDto
        {
            MonthID = s.MonthID,
            GroupID= s.GroupID,
            QuarterNo = s.QuarterNo,    
            Segment = s.Segment.Replace("\t", ""),
            ID = s.ID,
            Weekday =  s.Weekday,
            RoomsAllocated= s.RoomsAllocated,

        }).ToList();


        return roomAllocationDetails;

    }
  

}