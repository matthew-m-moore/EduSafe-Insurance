using System.Collections.Generic;
using System.Linq;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Vectors;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment.Parameterization
{
    public class DropOutRateParameterizer : IParameterizer
    {
        public List<EnrollmentStateArray> EnrollmentStateTimeSeries { get; private set; }

        private StudentEnrollmentModelInput _studentEnrollmentModelInput;
        private MultiplicativeVector _flatMultiplicativeVector;

        private readonly List<StudentEnrollmentState> _allowedParameterizationTargets = new List<StudentEnrollmentState>
            {
                StudentEnrollmentState.Enrolled,
                StudentEnrollmentState.DroppedOut,
                StudentEnrollmentState.Graduated
            };

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

            var priorPeriodStateArray = EnrollmentStateTimeSeries.First();
            for (var monthlyPeriod = 1; monthlyPeriod <= numberOfMonthlyPeriodsToProject; monthlyPeriod++)
            {
                PrepareTransitionRatesArray(transitionRatesArray, dropOutRateGuess, monthlyPeriod);

                var currentPeriodStateArray = new EnrollmentStateArray(monthlyPeriod);
                var priorPeriodEnrollment = priorPeriodStateArray.GetTotalState(StudentEnrollmentState.Enrolled);

                var enrollmentTargetsDictionary = enrollmentTargetsArray[monthlyPeriod];
                if (enrollmentTargetsDictionary != null)
                {
                    foreach (var enrollmentStateEntry in enrollmentTargetsDictionary)
                    {
                        var enrollmentState = enrollmentStateEntry.Key;
                        var targetValue = enrollmentStateEntry.Value.TargetValue;

                        if (!_allowedParameterizationTargets.Contains(enrollmentState)) continue;

                        currentPeriodStateArray[enrollmentState] = targetValue;
                        SetTransitionRateEntry(transitionRatesArray, enrollmentState, priorPeriodEnrollment, targetValue, monthlyPeriod);
                    }
                }

                CalculateTerminalStateAmount
                    (transitionRatesArray, currentPeriodStateArray, priorPeriodStateArray, StudentEnrollmentState.DroppedOut, monthlyPeriod);
                CalculateTerminalStateAmount
                    (transitionRatesArray, currentPeriodStateArray, priorPeriodStateArray, StudentEnrollmentState.Graduated, monthlyPeriod);

                // Note: There is an order-dependency to this adjustment. It assumes the drop-out paramaterizer runs first.
                currentPeriodStateArray.AdjustForTerminalStates(StudentEnrollmentState.Enrolled);
                currentPeriodStateArray.SetTotalState(StudentEnrollmentState.Enrolled, priorPeriodEnrollment);

                priorPeriodStateArray = currentPeriodStateArray;
                EnrollmentStateTimeSeries.Add(currentPeriodStateArray);
            }

            var totalDropOutAmount = EnrollmentStateTimeSeries[numberOfMonthlyPeriodsToProject].GetTotalState(StudentEnrollmentState.DroppedOut);
            return totalDropOutAmount;
        }

        private void CreateInitialEnrollmentStateArrayEntry(double initialEnrollmentPercentage)
        {
            var initialEnrollmentStateArray = new EnrollmentStateArray(0);
            initialEnrollmentStateArray.SetTotalState(StudentEnrollmentState.Enrolled, initialEnrollmentPercentage);

            EnrollmentStateTimeSeries = new List<EnrollmentStateArray>();
            EnrollmentStateTimeSeries.Add(initialEnrollmentStateArray);
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

        private void CalculateTerminalStateAmount(
            EnrollmentTransitionsArray transitionRatesArray,
            EnrollmentStateArray currentPeriodStateArray,
            EnrollmentStateArray priorPeriodStateArray,
            StudentEnrollmentState enrollmentState,
            int monthlyPeriod)
        {
            var terminalStateTransitionRate = transitionRatesArray
                [StudentEnrollmentState.Enrolled, enrollmentState, monthlyPeriod];

            if (terminalStateTransitionRate != null && !currentPeriodStateArray.Contains(enrollmentState))
            {
                var terminalStateRate = terminalStateTransitionRate.GetTransitionRate();
                var terminalStateAmount = priorPeriodStateArray.GetTotalState(StudentEnrollmentState.Enrolled) * terminalStateRate;
                currentPeriodStateArray[enrollmentState] = terminalStateAmount;               
            }
   
            var priorPeriodTotalAmount = priorPeriodStateArray.GetTotalState(enrollmentState);
            currentPeriodStateArray.SetTotalState(enrollmentState, priorPeriodTotalAmount);
        }
    }
}
