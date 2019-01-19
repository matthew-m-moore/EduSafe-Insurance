using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Vectors;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment.Parameterization
{
    public class DropOutRateParameterizer
    {
        public List<EnrollmentStateArray> EnrollmentStateTimeSeries => _enrollmentStateTimeSeries;

        private StudentEnrollmentModelInput _studentEnrollmentModelInput;
        private List<EnrollmentStateArray> _enrollmentStateTimeSeries;
        private MultiplicativeVector _flatMultiplicativeVector;

        public DropOutRateParameterizer(
            StudentEnrollmentModelInput studentEnrollmentModelInput,
            MultiplicativeVector flatMultiplicativeVector)
        {
            _studentEnrollmentModelInput = studentEnrollmentModelInput;
            _flatMultiplicativeVector = flatMultiplicativeVector;
        }

        public double Parameterize(double dropOutRateGuess)
        {
            var enrollmentPercentage = 1.0;
            CreateInitialEnrollmentStateArrayEntry(enrollmentPercentage);

            var numberOfMonthlyPeriodsToProject = _studentEnrollmentModelInput.NumberOfMonthlyPeriodsToProject;
            var enrollmentTargetsArray = _studentEnrollmentModelInput.EnrollmentTargetsArray;
            var transitionRatesArray = _studentEnrollmentModelInput.TransitionRatesArray;

            var priorPeriodEnrollmentStateArray = _enrollmentStateTimeSeries.First();
            for (var monthlyPeriod = 1; monthlyPeriod <= numberOfMonthlyPeriodsToProject; monthlyPeriod++)
            {
                PrepareTransitionRatesArray(transitionRatesArray, dropOutRateGuess, monthlyPeriod);

                var enrollmentStateArray = new EnrollmentStateArray();
                var priorPeriodEnrollment = priorPeriodEnrollmentStateArray[StudentEnrollmentState.Enrolled];

                var enrollmentTargetsDictionary = enrollmentTargetsArray[monthlyPeriod];
                if (enrollmentTargetsDictionary != null)
                {
                    foreach (var enrollmentStateEntry in enrollmentTargetsDictionary)
                    {
                        var enrollmentState = enrollmentStateEntry.Key;
                        var targetValue = enrollmentStateEntry.Value.TargetValue;

                        enrollmentStateArray[enrollmentState] = targetValue;
                        SetTransitionRateEntry(transitionRatesArray, enrollmentState, priorPeriodEnrollment, targetValue, monthlyPeriod);
                    }
                }

                CalculateDropOutAmount(transitionRatesArray, enrollmentStateArray, priorPeriodEnrollment, monthlyPeriod);
                CalculateGraduationAmount(transitionRatesArray, enrollmentStateArray, priorPeriodEnrollment, monthlyPeriod);

                enrollmentStateArray.AdjustForTerminalStates(priorPeriodEnrollment, StudentEnrollmentState.Enrolled);
                CalculateEmploymentAmount(transitionRatesArray, enrollmentStateArray, monthlyPeriod);

                priorPeriodEnrollmentStateArray = enrollmentStateArray;
                _enrollmentStateTimeSeries.Add(enrollmentStateArray);
            }

            var totalDropOutAmount = _enrollmentStateTimeSeries.Sum(t => t[StudentEnrollmentState.DroppedOut]);
            return totalDropOutAmount;
        }

        private void CreateInitialEnrollmentStateArrayEntry(double initialEnrollmentPercentage)
        {
            var initialEnrollmentStateArray = new EnrollmentStateArray();
            initialEnrollmentStateArray[StudentEnrollmentState.Enrolled] = initialEnrollmentPercentage;

            _enrollmentStateTimeSeries = new List<EnrollmentStateArray>();
            _enrollmentStateTimeSeries.Add(initialEnrollmentStateArray);
        }

        private void PrepareTransitionRatesArray(
            EnrollmentTransitionsArray transitionRatesArray,
            double dropOutRateGuess,
            int monthlyPeriod)
        {
            var transitionRate = transitionRatesArray
                [StudentEnrollmentState.Enrolled, StudentEnrollmentState.DroppedOut, monthlyPeriod];

            if (transitionRate == null)
            {
                transitionRate = new EnrollmentTransition(
                    StudentEnrollmentState.Enrolled,
                    StudentEnrollmentState.DroppedOut,
                    _flatMultiplicativeVector,
                    monthlyPeriod);

                transitionRatesArray[transitionRate] = transitionRate;
            }

            transitionRate.SetBaseTransitionRate(dropOutRateGuess);
        }

        private void SetTransitionRateEntry(
            EnrollmentTransitionsArray transitionRatesArray,
            StudentEnrollmentState enrollmentState,
            double priorPeriodEnrollment,
            double targetValue,
            int monthlyPeriod)
        {
            var transitionRate = new EnrollmentTransition(
                StudentEnrollmentState.Enrolled,
                enrollmentState,
                _flatMultiplicativeVector,
                monthlyPeriod);

            transitionRatesArray[transitionRate] = transitionRate;

            var baseTransitionRate = targetValue / priorPeriodEnrollment;
            transitionRate.SetBaseTransitionRate(baseTransitionRate);
        }

        private void CalculateDropOutAmount(
            EnrollmentTransitionsArray transitionRatesArray,
            EnrollmentStateArray enrollmentStateArray,
            double priorPeriodEnrollment,
            int monthlyPeriod)
        {
            var dropOutTransitionRate = transitionRatesArray
                [StudentEnrollmentState.Enrolled, StudentEnrollmentState.DroppedOut, monthlyPeriod];

            if (!enrollmentStateArray.Contains(StudentEnrollmentState.DroppedOut))
            {
                var dropOutRate = dropOutTransitionRate.GetTransitionRate();
                var dropOutAmount = priorPeriodEnrollment * dropOutRate;
                enrollmentStateArray[StudentEnrollmentState.DroppedOut] = dropOutAmount;
            }
        }

        private void CalculateGraduationAmount(
            EnrollmentTransitionsArray transitionRatesArray,
            EnrollmentStateArray enrollmentStateArray,
            double priorPeriodEnrollment,
            int monthlyPeriod)
        {
            var graduationTransitionRate = transitionRatesArray
                [StudentEnrollmentState.Enrolled, StudentEnrollmentState.Graduated, monthlyPeriod];

            if (graduationTransitionRate != null &&
                !enrollmentStateArray.Contains(StudentEnrollmentState.Graduated))
            {
                var graduationRate = graduationTransitionRate.GetTransitionRate();
                var graduationAmount = priorPeriodEnrollment * graduationRate;
                enrollmentStateArray[StudentEnrollmentState.Graduated] = graduationAmount;
            }
        }

        private void CalculateEmploymentAmount(
            EnrollmentTransitionsArray transitionRatesArray,
            EnrollmentStateArray enrollmentStateArray,
            int monthlyPeriod)
        {
            var employmentTransitionRate = transitionRatesArray
                [StudentEnrollmentState.Graduated, StudentEnrollmentState.GraduatedEmployed, monthlyPeriod];

            if (employmentTransitionRate != null &&
                !enrollmentStateArray.Contains(StudentEnrollmentState.Graduated))
            {
                var employmentRate = employmentTransitionRate.GetTransitionRate();
                var employmentAmount = enrollmentStateArray[StudentEnrollmentState.Graduated] * employmentRate;
                enrollmentStateArray[StudentEnrollmentState.GraduatedEmployed] = employmentAmount;
            }
        }
    }
}
