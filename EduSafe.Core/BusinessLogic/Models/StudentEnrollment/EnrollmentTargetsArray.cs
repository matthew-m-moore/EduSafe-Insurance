using System.Collections.Generic;
using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment
{
    public class EnrollmentTargetsArray
    {
        private Dictionary<int?, Dictionary<StudentEnrollmentState, StudentEnrollmentTarget>> _enrollmentTargetsArray;

        public EnrollmentTargetsArray()
        {
            _enrollmentTargetsArray = new Dictionary<int?, Dictionary<StudentEnrollmentState, StudentEnrollmentTarget>>();
        }

        public StudentEnrollmentTarget this[int? monthlyPeriod, StudentEnrollmentState targetEnrollmentState]
        {
            get
            {
                if(_enrollmentTargetsArray.ContainsKey(monthlyPeriod) &&
                   _enrollmentTargetsArray[monthlyPeriod].ContainsKey(targetEnrollmentState))
                {
                    return _enrollmentTargetsArray[monthlyPeriod][targetEnrollmentState];
                }

                return null;
            }
            set
            {
                if (!_enrollmentTargetsArray.ContainsKey(monthlyPeriod))
                {
                    _enrollmentTargetsArray.Add(monthlyPeriod, new Dictionary<StudentEnrollmentState, StudentEnrollmentTarget>());
                }

                if (!_enrollmentTargetsArray[monthlyPeriod].ContainsKey(targetEnrollmentState))
                {
                    _enrollmentTargetsArray[monthlyPeriod].Add(targetEnrollmentState, value);
                }
                else
                {
                    _enrollmentTargetsArray[monthlyPeriod][targetEnrollmentState] = value;
                }
            }
        }

        public Dictionary<StudentEnrollmentState, StudentEnrollmentTarget> this[int? monthlyPeriod]
        {
            get
            {
                if (_enrollmentTargetsArray.ContainsKey(monthlyPeriod))
                {
                    return _enrollmentTargetsArray[monthlyPeriod];
                }

                return null;
            }
        }
    }
}
