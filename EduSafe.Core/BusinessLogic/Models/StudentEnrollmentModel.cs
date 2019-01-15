using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Core.BusinessLogic.Containers;

namespace EduSafe.Core.BusinessLogic.Models
{
    public class StudentEnrollmentModel
    {
        public bool IsParameterized = false;

        private StudentEnrollmentModelInput _studentEnrollmentModelInput { get; }
        private List<StudentEnrollmentStateArray> _enrollmentStateTimeSeries { get; }

        public void ParameterizeModel()
        {
            IsParameterized = true;
            throw new NotImplementedException();

            // This would use the numerical search utility to solve for curves that appropriately
            // hit the targeted values in the inputs
        }
    }
}
