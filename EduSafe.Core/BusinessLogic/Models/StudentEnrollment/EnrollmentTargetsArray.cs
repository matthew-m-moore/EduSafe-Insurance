using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment
{
    public class EnrollmentTargetsArray
    {
        private Dictionary<int, Dictionary<StudentEnrollmentState, EnrollmentTarget>> _enrollmentTargetsArray;
        private const int _totalTargetKey = -1;

        public EnrollmentTargetsArray()
        {
            _enrollmentTargetsArray = new Dictionary<int, Dictionary<StudentEnrollmentState, EnrollmentTarget>>();
        }

        /// <summary>
        /// Returns a deep, member-wise copy of the object.
        /// </summary>
        public EnrollmentTargetsArray Copy()
        {
            var copyOfEnrollmentTargetsArray = _enrollmentTargetsArray
                .ToDictionary(kvp1 => kvp1.Key, kvp1 => kvp1.Value
                .ToDictionary(kvp2 => kvp2.Key, kvp2 => kvp2.Value.Copy()));

            return new EnrollmentTargetsArray()
            {
                _enrollmentTargetsArray = copyOfEnrollmentTargetsArray
            };
        }

        public double TotalTarget(StudentEnrollmentState enrollmentState)
        {
            // Note that the null key is used to indicate the total target over all projected months
            if (_enrollmentTargetsArray.ContainsKey(_totalTargetKey) &&
                _enrollmentTargetsArray[_totalTargetKey].ContainsKey(enrollmentState))
            {
                return _enrollmentTargetsArray[_totalTargetKey][enrollmentState].TargetValue;
            }

            var totalTarget = _enrollmentTargetsArray
                .Where(e => e.Key != _totalTargetKey)
                .Sum(a => a.Value[enrollmentState].TargetValue);

            return totalTarget;
        }

        public EnrollmentTarget this[int monthlyPeriod, StudentEnrollmentState targetEnrollmentState]
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
                    _enrollmentTargetsArray.Add(monthlyPeriod, new Dictionary<StudentEnrollmentState, EnrollmentTarget>());
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

        public EnrollmentTarget this[StudentEnrollmentState targetEnrollmentState]
        {
            get
            {
                return this[_totalTargetKey, targetEnrollmentState];
            }
            set
            {
                this[_totalTargetKey, targetEnrollmentState] = value;
            }
        }

        public Dictionary<StudentEnrollmentState, EnrollmentTarget> this[int monthlyPeriod]
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
