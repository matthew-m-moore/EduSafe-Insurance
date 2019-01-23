using System;
using System.Collections.Generic;
using System.Linq;
using EduSafe.Common;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Vectors;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment.Parameterization
{
    public class PostGraduationParameterizer : IParameterizer
    {
        public List<EnrollmentStateArray> EnrollmentStateTimeSeries { get; protected set; }

        private StudentEnrollmentModelInput _studentEnrollmentModelInput;
        private MultiplicativeVector _flatMultiplicativeVector;

        protected StudentEnrollmentState _StartingEnrollmentState;
        protected StudentEnrollmentState _EndingEnrollmentState;
        protected int _StartingMonthlyPeriod;

        public PostGraduationParameterizer(
            List<EnrollmentStateArray> enrollmentStateTimeSeries,
            StudentEnrollmentModelInput studentEnrollmentModelInput,
            MultiplicativeVector flatMultiplicativeVector,
            StudentEnrollmentState endingEnrollmentState)
        {
            EnrollmentStateTimeSeries = enrollmentStateTimeSeries;

            _studentEnrollmentModelInput = studentEnrollmentModelInput;
            _flatMultiplicativeVector = flatMultiplicativeVector;

            _StartingEnrollmentState = StudentEnrollmentState.Graduated;
            _EndingEnrollmentState = endingEnrollmentState;
            _StartingMonthlyPeriod = 1;
        }

        public double Parameterize(double transitionRateGuess = -1.0)
        {
            if (EnrollmentStateTimeSeries == null || !EnrollmentStateTimeSeries.Any())
            {
                throw new Exception("ERROR: The initial enrollment state time series must exist before post-graduation rates can be determined.");
            }

            var numberOfMonthlyPeriodsToProject = _studentEnrollmentModelInput.NumberOfMonthlyPeriodsToProject;
            var enrollmentTargetsArray = _studentEnrollmentModelInput.EnrollmentTargetsArray;
            var transitionRatesArray = _studentEnrollmentModelInput.TransitionRatesArray;

            var priorPeriodStateArray = EnrollmentStateTimeSeries.First();
            for (var monthlyPeriod = _StartingMonthlyPeriod; monthlyPeriod <= numberOfMonthlyPeriodsToProject; monthlyPeriod++)
            {
                // This conditional is only to leverage the default value of -1.0 to skip optimization when rates are provided without targets
                if (transitionRateGuess >= 0.0)
                    PrepareTransitionRatesArray(transitionRatesArray, transitionRateGuess, monthlyPeriod);

                var currentPeriodStateArray = EnrollmentStateTimeSeries[monthlyPeriod];
                var enrollmentTargetsDictionary = enrollmentTargetsArray[monthlyPeriod];

                if (enrollmentTargetsDictionary != null && enrollmentTargetsDictionary.ContainsKey(_EndingEnrollmentState))
                {
                    var currentPeriodDeltaAmount = currentPeriodStateArray[_StartingEnrollmentState];
                    var targetValue = enrollmentTargetsDictionary[_EndingEnrollmentState].TargetValue;

                    currentPeriodStateArray[_EndingEnrollmentState] = targetValue;
                    SetTransitionRateEntry(transitionRatesArray, currentPeriodDeltaAmount, targetValue, monthlyPeriod);
                }

                PopulateStateArray(transitionRatesArray, currentPeriodStateArray, priorPeriodStateArray, monthlyPeriod);              
                priorPeriodStateArray = currentPeriodStateArray;
            }

            var totalEndingStateAmount = EnrollmentStateTimeSeries[numberOfMonthlyPeriodsToProject].GetTotalState(_EndingEnrollmentState);
            return totalEndingStateAmount;
        }

        protected virtual void PopulateStateArray(
            EnrollmentTransitionsArray transitionRatesArray, 
            EnrollmentStateArray currentPeriodStateArray, 
            EnrollmentStateArray priorPeriodStateArray, 
            int monthlyPeriod)
        {
            CalculateEndingStateAmount(transitionRatesArray, currentPeriodStateArray, priorPeriodStateArray, monthlyPeriod);
            CalculateRemainingStateAmount(currentPeriodStateArray, priorPeriodStateArray);
        }

        protected void CalculateEndingStateAmount(
            EnrollmentTransitionsArray transitionRatesArray,
            EnrollmentStateArray currentPeriodStateArray,
            EnrollmentStateArray priorPeriodStateArray,
            int monthlyPeriod)
        {
            var transitionRateEntry = transitionRatesArray
                [_StartingEnrollmentState, _EndingEnrollmentState, monthlyPeriod];

            if (transitionRateEntry != null &&
                currentPeriodStateArray.Contains(_StartingEnrollmentState))
            {
                var transitionRate = transitionRateEntry.GetTransitionRate();
                var endingStateAmount = GetEndingStateAmount(currentPeriodStateArray) * transitionRate;
                currentPeriodStateArray[_EndingEnrollmentState] = endingStateAmount;
            }

            var priorPeriodTotalAmount = priorPeriodStateArray.GetTotalState(_EndingEnrollmentState);
            currentPeriodStateArray.SetTotalState(_EndingEnrollmentState, priorPeriodTotalAmount);
        }

        protected virtual double GetEndingStateAmount(EnrollmentStateArray currentPeriodStateArray)
        {
            return currentPeriodStateArray[_StartingEnrollmentState];
        }

        private void PrepareTransitionRatesArray(
            EnrollmentTransitionsArray transitionRatesArray,
            double transitionRateGuess,
            int monthlyPeriod)
        {
            var transitionRate = transitionRatesArray
                [_StartingEnrollmentState, _EndingEnrollmentState, monthlyPeriod];

            if (transitionRate == null)
            {
                transitionRate = new EnrollmentTransition(
                    _StartingEnrollmentState,
                    _EndingEnrollmentState,
                    _flatMultiplicativeVector,
                    monthlyPeriod);

                transitionRatesArray[transitionRate] = transitionRate;
            }

            transitionRate.SetBaseTransitionRate(transitionRateGuess);
        }

        private void SetTransitionRateEntry(
            EnrollmentTransitionsArray transitionRatesArray,
            double currentPeriodGraduates,
            double targetValue,
            int monthlyPeriod)
        {
            var transitionRate = new EnrollmentTransition(
                _StartingEnrollmentState,
                _EndingEnrollmentState,
                _flatMultiplicativeVector,
                monthlyPeriod);

            transitionRatesArray[transitionRate] = transitionRate;

            var baseTransitionRate = targetValue / currentPeriodGraduates;
            transitionRate.SetBaseTransitionRate(baseTransitionRate);
        }

        private void CalculateRemainingStateAmount(EnrollmentStateArray currentPeriodStateArray, EnrollmentStateArray priorPeriodStateArray)
        {
            var remainingEnrollmentState = Constants.PostGraduationEnrollmentStates
                .SingleOrDefault(s => !_studentEnrollmentModelInput.PostGraduationTargetStates.Contains(s));

            if (remainingEnrollmentState == default(StudentEnrollmentState))
                ThrowTooFewTargetsException();

            currentPeriodStateArray.AdjustForRemainingState(
                remainingEnrollmentState, 
                _StartingEnrollmentState, 
                Constants.PostGraduationEnrollmentStates);

            var priorPeriodTotalAmount = priorPeriodStateArray.GetTotalState(remainingEnrollmentState);
            currentPeriodStateArray.SetTotalState(remainingEnrollmentState, priorPeriodTotalAmount);
        }

        private void ThrowTooFewTargetsException()
        {
            var targetedStates = string.Empty;

            // Basically, this means the sytem is under-determined, so it returns an error
            var tooFewTargetsText = string.Format("ERROR: There are not enough post-graduation states targeted. "
                + "The following states are already being targeted: {0}", targetedStates);

            if (_studentEnrollmentModelInput.PostGraduationTargetStates.Any())
            {
                _studentEnrollmentModelInput.PostGraduationTargetStates.ToList()
                    .ForEach(s => targetedStates += (" " + s.ToString() + " "));
            }
            else
            {
                targetedStates = "(none)";
            }

            throw new Exception(tooFewTargetsText);
        }
    }
}
