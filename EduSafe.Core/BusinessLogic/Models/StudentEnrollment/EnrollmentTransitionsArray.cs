using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment
{
    public class EnrollmentTransitionsArray
    {
        private Dictionary<StudentEnrollmentState, 
                Dictionary<StudentEnrollmentState, 
                Dictionary<int, EnrollmentTransition>>> _enrollmentTransitionArray;

        public EnrollmentTransitionsArray()
        {
            _enrollmentTransitionArray = 
                new Dictionary<StudentEnrollmentState, 
                    Dictionary<StudentEnrollmentState, 
                    Dictionary<int, EnrollmentTransition>>>();
        }

        /// <summary>
        /// Returns a deep, member-wise copy of the object.
        /// </summary>
        public EnrollmentTransitionsArray Copy()
        {
            var copyOfEnrollmentTransitionArray = _enrollmentTransitionArray
                .ToDictionary(kvp1 => kvp1.Key, kvp1 => kvp1.Value
                .ToDictionary(kvp2 => kvp2.Key, kvp2 => kvp2.Value
                .ToDictionary(kvp3 => kvp3.Key, kvp3 => kvp3.Value.Copy())));

            return new EnrollmentTransitionsArray()
            {
                _enrollmentTransitionArray = copyOfEnrollmentTransitionArray
            };
        }

        public EnrollmentTransition this
            [StudentEnrollmentState startEnrollmentState, StudentEnrollmentState endEnrollmentState, int monthlyPeriod]
        {
            get
            {
                if (_enrollmentTransitionArray.ContainsKey(startEnrollmentState) &&
                    _enrollmentTransitionArray[startEnrollmentState].ContainsKey(endEnrollmentState) &&
                    _enrollmentTransitionArray[startEnrollmentState][endEnrollmentState].ContainsKey(monthlyPeriod))
                {
                    return _enrollmentTransitionArray[startEnrollmentState][endEnrollmentState][monthlyPeriod];
                }

                return null;
            }
            private set
            {
                if (!_enrollmentTransitionArray.ContainsKey(startEnrollmentState))
                {
                    _enrollmentTransitionArray.Add(startEnrollmentState,
                        new Dictionary<StudentEnrollmentState, Dictionary<int, EnrollmentTransition>>());
                }

                if (!_enrollmentTransitionArray[startEnrollmentState].ContainsKey(endEnrollmentState))
                {
                    _enrollmentTransitionArray[startEnrollmentState].Add(endEnrollmentState,
                        new Dictionary<int, EnrollmentTransition>());
                }

                if (!_enrollmentTransitionArray[startEnrollmentState][endEnrollmentState].ContainsKey(monthlyPeriod))
                {
                    _enrollmentTransitionArray[startEnrollmentState][endEnrollmentState].Add(monthlyPeriod, value);
                }
                else
                {
                    _enrollmentTransitionArray[startEnrollmentState][endEnrollmentState][monthlyPeriod] = value;
                }
            }
        }

        public List<EnrollmentTransition> this[StudentEnrollmentState startEnrollmentState, StudentEnrollmentState endEnrollmentState]
        {
            get
            {
                if (_enrollmentTransitionArray.ContainsKey(startEnrollmentState) &&
                    _enrollmentTransitionArray[startEnrollmentState].ContainsKey(endEnrollmentState))
                {
                    return _enrollmentTransitionArray[startEnrollmentState][endEnrollmentState].Values.ToList();
                }

                return null;
            }
            set
            {
                foreach (var enrollmentTranstion in value)
                {
                    var monthlyPeriod = enrollmentTranstion.MonthlyPeriod;
                    this[startEnrollmentState, endEnrollmentState, monthlyPeriod] = enrollmentTranstion;
                }
            }
        }

        public EnrollmentTransition this[EnrollmentTransition studentEnrollmentTransition]
        {
            get
            {
                var startEnrollmentState = studentEnrollmentTransition.StartEnrollmentState;
                var endEnrollmentState = studentEnrollmentTransition.EndEnrollmentState;
                var monthlyPeriod = studentEnrollmentTransition.MonthlyPeriod;

                return this[startEnrollmentState, endEnrollmentState, monthlyPeriod];
            }
            set
            {
                var startEnrollmentState = studentEnrollmentTransition.StartEnrollmentState;
                var endEnrollmentState = studentEnrollmentTransition.EndEnrollmentState;
                var monthlyPeriod = studentEnrollmentTransition.MonthlyPeriod;

                this[startEnrollmentState, endEnrollmentState, monthlyPeriod] = value;
            }
        }
    }
}
