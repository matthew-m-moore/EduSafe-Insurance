using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class ForecastedOverlayScenariosConverter
    {
        public static Dictionary<int, Dictionary<string, IScenario>> ConvertForecastedOverlayScenarios()
        {
            return new Dictionary<int, Dictionary<string, IScenario>>();
        }
    }
}
