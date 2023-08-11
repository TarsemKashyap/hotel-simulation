namespace Common.Dto
{
    public class RoomAllocationDetailsDto
    {
        public List<RoomAllocationDto> RoomAllocation { get; set; }

        public int WeekdayTotal { get; set; }

        public int WeekendTotal { get; set; }

    }
}
