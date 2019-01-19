using System;
using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Enums;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment
{
    public class EnrollmentStateArray
    {
        private Dictionary<StudentEnrollmentState, double> _enrollmentStateArray { get; }

        public EnrollmentStateArray()
        {
            _enrollmentStateArray = new Dictionary<StudentEnrollmentState, double>();
        }

        public bool Contains(StudentEnrollmentState studentEnrollmentState)
        {
            return _enrollmentStateArray.ContainsKey(studentEnrollmentState);
        }

        public void AdjustForTerminalStates(double priorEnrollmentStateValue, StudentEnrollmentState enrollmentState)
        {
            var remainingFractionOfArray = _enrollmentStateArray.Where(a => a.Key != enrollmentState).Sum(s => s.Value);
            _enrollmentStateArray[enrollmentState] = Math.Max(priorEnrollmentStateValue - remainingFractionOfArray, 0.0);
        }

        public void RenormalizeArray(EnrollmentStateArray baseEnrollmentStateArray, StudentEnrollmentState renormalizedState)
        {
            foreach(var enrollmentState in _enrollmentStateArray.Keys)
            {
                var originalValue = _enrollmentStateArray[enrollmentState];
                var baseValue = baseEnrollmentStateArray[renormalizedState];

                var renormalizedValue = originalValue / baseValue;

                if (enrollmentState != renormalizedState)
                {
                    var priorValue = baseEnrollmentStateArray[enrollmentState];
                    renormalizedValue -= (priorValue / baseValue);
                }
            }
        }

        public double this[StudentEnrollmentState studentEnrollmentState]
        {
            get
            {
                if (_enrollmentStateArray.ContainsKey(studentEnrollmentState))
                {
                    return _enrollmentStateArray[studentEnrollmentState];
                }

                return 0.0;
            }
            set
            {
                if (!_enrollmentStateArray.ContainsKey(studentEnrollmentState))
                {
                    _enrollmentStateArray.Add(studentEnrollmentState, value);
                }
                else
                {
                    _enrollmentStateArray[studentEnrollmentState] = value;
                }
            }
        }
    }
}
