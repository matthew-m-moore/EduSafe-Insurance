using System.Collections.Generic;
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
