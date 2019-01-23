using System;
using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment
{
    public class EnrollmentStateArray
    {
        public int MonthlyPeriod { get; }

        private Dictionary<StudentEnrollmentState, double> _incrementalStateArray { get; }
        private Dictionary<StudentEnrollmentState, double> _totalStateArray { get; }

        public EnrollmentStateArray(int monthlyPeriod)
        {
            MonthlyPeriod = monthlyPeriod;

            _incrementalStateArray = new Dictionary<StudentEnrollmentState, double>();
            _totalStateArray = new Dictionary<StudentEnrollmentState, double>();
        }

        public bool Contains(StudentEnrollmentState studentEnrollmentState)
        {
            return _incrementalStateArray.ContainsKey(studentEnrollmentState);
        }

        public bool ContainsTotal(StudentEnrollmentState studentEnrollmentState)
        {
            return _totalStateArray.ContainsKey(studentEnrollmentState);
        }

        public void AdjustForTerminalStates(StudentEnrollmentState enrollmentState)
        {
            var remainingFractionOfArray = _incrementalStateArray.Where(a => a.Key != enrollmentState).Sum(s => s.Value);
            _incrementalStateArray[enrollmentState] = -1.0 * remainingFractionOfArray;
        }

        public void AdjustForRemainingState(
            StudentEnrollmentState remainingEnrollmentState,
            StudentEnrollmentState startingEnrollmentState,
            List<StudentEnrollmentState> mutuallyExclusiveEnrollmentStates)
        {
            var remainingFractionOfArray = _incrementalStateArray
                .Where(a => mutuallyExclusiveEnrollmentStates.Contains(a.Key) && a.Key != remainingEnrollmentState)
                .Sum(s => s.Value);

            var startingEnrollmentAmount = this[startingEnrollmentState];
            _incrementalStateArray[remainingEnrollmentState] = startingEnrollmentAmount - remainingFractionOfArray;
        }

        public void RenormalizeArray(EnrollmentStateArray baseEnrollmentStateArray, StudentEnrollmentState renormalizedState)
        {
            foreach(var enrollmentState in _totalStateArray.Keys)
            {
                var originalValue = _totalStateArray[enrollmentState];
                var baseValue = baseEnrollmentStateArray[renormalizedState];

                var renormalizedValue = originalValue / baseValue;

                if (enrollmentState != renormalizedState)
                {
                    var priorValue = baseEnrollmentStateArray[enrollmentState];
                    renormalizedValue -= (priorValue / baseValue);
                }
            }
        }

        public double GetTotalState(StudentEnrollmentState enrollmentState)
        {
            if (_totalStateArray.ContainsKey(enrollmentState))
            {
                return _totalStateArray[enrollmentState];
            }

            return 0.0;
        }

        public void SetTotalState(StudentEnrollmentState enrollmentState, double priorStateValue = 0.0)
        {
            var incrementalStateValue = this[enrollmentState];
            var value = priorStateValue + incrementalStateValue;

            if (!_totalStateArray.ContainsKey(enrollmentState))
            {
                _totalStateArray.Add(enrollmentState, value);
            }
            else
            {
                _totalStateArray[enrollmentState] = value;
            }
        }

        public double this[StudentEnrollmentState enrollmentState]
        {
            get
            {
                if (_incrementalStateArray.ContainsKey(enrollmentState))
                {
                    return _incrementalStateArray[enrollmentState];
                }

                return 0.0;
            }
            set
            {
                if (!_incrementalStateArray.ContainsKey(enrollmentState))
                {
                    _incrementalStateArray.Add(enrollmentState, value);
                }
                else
                {
                    _incrementalStateArray[enrollmentState] = value;
                }
            }
        }
    }
}
