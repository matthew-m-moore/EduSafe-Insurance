using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSafe.IO.Excel.Records
{
    public class CostOrFeeInputRecord
    {
        public string CostOrFeeName { get; set; }
        public double Amount { get; set; }
        public int? FrequencyInMonths { get; set; }
    }
}
