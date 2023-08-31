namespace Common.ReportDto
{

    public class MarketSharePositionReportDto
    {
        public List<MarketSharePositionDto> Data { get; set; } = new List<MarketSharePositionDto>();

        public void Add(string label, decimal mketShare, decimal mktPosition)
        {
            Data.Add(new MarketSharePositionDto(label) { ActualMarketShare = mketShare, MarketSharePosition = mktPosition });
        }


    }
    public class MarketSharePositionDto
    {
        public string Label { get; set; }
        public decimal ActualMarketShare { get; set; }
        public decimal MarketSharePosition { get; set; }
        public MarketSharePositionDto(string label)
        {
            Label = label;
        }
        public MarketSharePositionDto MarketShare(decimal value1)
        {
            ActualMarketShare = value1;
            return this;
        }
        public MarketSharePositionDto Position(decimal value1)
        {
            MarketSharePosition = value1;
            return this;
        }

    }
}
