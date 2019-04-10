using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class ReinvestmentOptionsParametersConverter
    {
        public static ReinvestmentOptionsParameters Convert(ReinvestmentOptionsInputRecord reinvestmentOptionsInputRecord )
        {
            return new ReinvestmentOptionsParameters
            {
                OneMonthRate = reinvestmentOptionsInputRecord.OneMonthRate,

                PortionIn12M = reinvestmentOptionsInputRecord.PortionIn12M,
                PortionIn1M = reinvestmentOptionsInputRecord.PortionIn1M,
                PortionIn3M = reinvestmentOptionsInputRecord.PortionIn3M,
                PortionIn6M = reinvestmentOptionsInputRecord.PortionIn6M,

                PortionInCash = reinvestmentOptionsInputRecord.PortionInCash,
                SixMonthRate = reinvestmentOptionsInputRecord.SixMonthRate,
                ThreeMonthRate = reinvestmentOptionsInputRecord.ThreeMonthRate,
                TwelveMonthRate = reinvestmentOptionsInputRecord.TwelveMonthRate
            };
        }
    }
}
