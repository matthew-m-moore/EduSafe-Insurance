using System;
using System.Collections.Generic;
using System.Linq;
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
        private StudentEnrollmentState _postGraduationState;

        public PostGraduationParameterizer(
            List<EnrollmentStateArray> enrollmentStateTimeSeries,
            StudentEnrollmentModelInput studentEnrollmentModelInput,
            MultiplicativeVector flatMultiplicativeVector,
            StudentEnrollmentState postGraduationState)
        {
            EnrollmentStateTimeSeries = enrollmentStateTimeSeries;

            _studentEnrollmentModelInput = studentEnrollmentModelInput;
            _flatMultiplicativeVector = flatMultiplicativeVector;
            _postGraduationState = postGraduationState;
        }

        public double Parameterize(double postGraduationRateGuess = -1.0)
        {
            if (EnrollmentStateTimeSeries == null || !EnrollmentStateTimeSeries.Any())
            {
                throw new Exception("ERROR: The initial enrollment state time series must exist before post-graduation rates can be determined.");
            }

            var numberOfMonthlyPeriodsToProject = _studentEnrollmentModelInput.NumberOfMonthlyPeriodsToProject;
            var enrollmentTargetsArray = _studentEnrollmentModelInput.EnrollmentTargetsArray;
            var transitionRatesArray = _studentEnrollmentModelInput.TransitionRatesArray;

            var priorPeriodStateArray = EnrollmentStateTimeSeries.First();
            for (var monthlyPeriod = 1; monthlyPeriod <= numberOfMonthlyPeriodsToProject; monthlyPeriod++)
            {
                // This conditional is only to leverage the default value of -1.0 to skip optimization when rates are provided without targets
                if (postGraduationRateGuess >= 0.0)
                    PrepareTransitionRatesArray(transitionRatesArray, postGraduationRateGuess, monthlyPeriod);

                var currentPeriodStateArray = EnrollmentStateTimeSeries[monthlyPeriod];
                var currentPeriodGraduates = currentPeriodStateArray[StudentEnrollmentState.Graduated];

                var enrollmentTargetsDictionary = enrollmentTargetsArray[monthlyPeriod];
                if (enrollmentTargetsDictionary != null && enrollmentTargetsDictionary.ContainsKey(_postGraduationState))
                {
                    var targetValue = enrollmentTargetsDictionary[_postGraduationState].TargetValue;

                    currentPeriodStateArray[_postGraduationState] = targetValue;
                    SetTransitionRateEntry(transitionRatesArray, currentPeriodGraduates, targetValue, monthlyPeriod);
                }

                CalculatePostGraduationAmount(transitionRatesArray, currentPeriodStateArray, priorPeriodStateArray, monthlyPeriod);
                priorPeriodStateArray = currentPeriodStateArray;
            }

            var totalPostGraduationAmount = EnrollmentStateTimeSeries[numberOfMonthlyPeriodsToProject].GetTotalState(_postGraduationState);
            return totalPostGraduationAmount;
        }

        private void PrepareTransitionRatesArray(
            EnrollmentTransitionsArray transitionRatesArray,
            double postGraduationRateGuess,
            int monthlyPeriod)
        {
            var transitionRate = transitionRatesArray
                [StudentEnrollmentState.Graduated, _postGraduationState, monthlyPeriod];

            if (transitionRate == null)
            {
                transitionRate = new EnrollmentTransition(
                    StudentEnrollmentState.Graduated,
                    _postGraduationState,
                    _flatMultiplicativeVector,
                    monthlyPeriod);

                transitionRatesArray[transitionRate] = transitionRate;
            }

            transitionRate.SetBaseTransitionRate(postGraduationRateGuess);
        }

        private void SetTransitionRateEntry(
            EnrollmentTransitionsArray transitionRatesArray,
            double currentPeriodGraduates,
            double targetValue,
            int monthlyPeriod)
        {
            var transitionRate = new EnrollmentTransition(
                StudentEnrollmentState.Graduated,
                _postGraduationState,
                _flatMultiplicativeVector,
                monthlyPeriod);

            transitionRatesArray[transitionRate] = transitionRate;

            var baseTransitionRate = targetValue / currentPeriodGraduates;
            transitionRate.SetBaseTransitionRate(baseTransitionRate);
        }

        private void CalculatePostGraduationAmount(
            EnrollmentTransitionsArray transitionRatesArray,
            EnrollmentStateArray currentPeriodStateArray,
            EnrollmentStateArray priorPeriodStateArray,
            int monthlyPeriod)
        {
            var postGraduationTransitionRate = transitionRatesArray
                [StudentEnrollmentState.Graduated, _postGraduationState, monthlyPeriod];

            if (postGraduationTransitionRate != null &&
                currentPeriodStateArray.Contains(StudentEnrollmentState.Graduated))
            {
                var postGraduationRate = postGraduationTransitionRate.GetTransitionRate();
                var postGraduationAmount = currentPeriodStateArray[StudentEnrollmentState.Graduated] * postGraduationRate;
                currentPeriodStateArray[_postGraduationState] = postGraduationAmount;                
            }

            var priorPeriodTotalAmount = priorPeriodStateArray.GetTotalState(_postGraduationState);
            currentPeriodStateArray.SetTotalState(_postGraduationState, priorPeriodTotalAmount);
        }
    }
}
