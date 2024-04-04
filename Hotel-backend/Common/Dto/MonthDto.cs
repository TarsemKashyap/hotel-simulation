using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class MonthDto
    {
        public int MonthId { get; set; }
        public int ClassId { get; set; }
        public int Sequence { get; set; }
        public decimal TotalMarket { get; set; }
        public bool IsComplete { get; set; }
        public int ConfigId { get; set; }
        public string Status { get; set; }
        public string StatusText { get; set; }
    public virtual ClassSessionDto Class { get; set; }

    
}
