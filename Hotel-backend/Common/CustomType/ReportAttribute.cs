namespace Common
{
    public class ReportAttribute
    {
        public string Label { get; set; }
        public AbstractDecimal Data { get; set; }
        public List<ReportAttribute> Childern { get; private set; }
        private void InitDict()
        {
            if (Childern == null)
                Childern = new List<ReportAttribute>();
        }
        public void AddChild(string label, decimal? money)
        {
            InitDict();
            var currency = money.HasValue ? new Currency(money.Value) : null;
            var report = new ReportAttribute { Label = label, Data = currency };
            AddChild(report);
        }
        public static implicit operator ReportAttribute(string label)
        {
            return new ReportAttribute { Label = label };
        }

        public void AddChild(params ReportAttribute[] childs)
        {
            InitDict();
            Childern.AddRange(childs);
        }

    }


}
