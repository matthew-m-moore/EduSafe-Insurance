using System;
using EduSafe.Common.Enums;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class DayCountConventionConverter
    {
        public static DayCountConvention ConvertStringToDayCountConvention(string dayCountConventionText)
        {
            if (Enum.TryParse(dayCountConventionText, out DayCountConvention dayCountConvention))
                return dayCountConvention;

            var exceptionText = string.Format("The day count convention '{0}' is not yet supported.",
                dayCountConventionText);
            throw new NotSupportedException(exceptionText);
        }
    }
}
