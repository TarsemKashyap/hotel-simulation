using Database;

public class RoomAllocationRepository : Repository<RoomAllocation>
{
    public RoomAllocationRepository(HotelDbContext context) : base(context)
    {
    }
}