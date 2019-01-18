using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;
using EduSafe.Common.Utilities;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Vectors;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment
{
    public class EnrollmentModel
    {
        public bool IsParameterized = false;

        private StudentEnrollmentModelInput _studentEnrollmentModelInput;
        private List<EnrollmentStateArray> _enrollmentStateTimeSeries;
        private MutiliplicativeVector _flatMultiplicativeVector;

        private const double _targetPrecision = 1e-14;

        public EnrollmentModel(StudentEnrollmentModelInput studentEnrollmentModelInput)
        {
            _studentEnrollmentModelInput = studentEnrollmentModelInput;
            _enrollmentStateTimeSeries = new List<EnrollmentStateArray>();
            _flatMultiplicativeVector = new MutiliplicativeVector(new DataCurve<double>(1.0));
        }

        public void ParameterizeModel()
        {
            // This would use the numerical search utility to solve for curves that appropriately
            // hit the targeted values in the inputs
            var dropOutTarget = _studentEnrollmentModelInput.EnrollmentTargetsArray[null, StudentEnrollmentState.DroppedOut];
            if (dropOutTarget != null)
            {
                var targetValue = dropOutTarget.TargetValue;
                NumericalSearchUtility.NewtonRaphsonWithBisection(ParameterizeDropOutRate, targetValue, _targetPrecision);
            }

            IsParameterized = true;          
        }

        private double ParameterizeDropOutRate(double dropOutRateGuess)
        {
            var enrollmentPercentage = 1.0;
            CreateInitialEnrollmentStateArrayEntry(enrollmentPercentage);

            var numberOfMonthlyPeriodsToProject = _studentEnrollmentModelInput.NumberOfMonthlyPeriodsToProject;
            var enrollmentTargetsArray = _studentEnrollmentModelInput.EnrollmentTargetsArray;
            var transitionRatesArray = _studentEnrollmentModelInput.TransitionRatesArray;

            var priorPeriodEnrollmentStateArray = _enrollmentStateTimeSeries.First();
            for (var monthlyPeriod = 1; monthlyPeriod < numberOfMonthlyPeriodsToProject; monthlyPeriod++)
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

                var dropOutTransitionRate = transitionRatesArray
                    [StudentEnrollmentState.Enrolled, StudentEnrollmentState.DroppedOut, monthlyPeriod];

                if (!enrollmentStateArray.Contains(StudentEnrollmentState.DroppedOut))
                {
                    var dropOutRate = dropOutTransitionRate.GetTransitionRate();
                    var dropOutAmount = priorPeriodEnrollment * dropOutRate;
                    enrollmentStateArray[StudentEnrollmentState.DroppedOut] = dropOutAmount;
                }

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

            return _enrollmentStateTimeSeries.Sum(t => t[StudentEnrollmentState.DroppedOut]);
        }        

        private void ParameterizeGraduateSchoolRate(double graduateSchoolRateGuess)
        {

        }

        private void ParameterizeEarlyHireRate(double earlyHireRateGuess)
        {

        }

        private void CreateInitialEnrollmentStateArrayEntry(double initialEnrollmentPercentage)
        {
            var initialEnrollmentStateArray = new EnrollmentStateArray();
            initialEnrollmentStateArray[StudentEnrollmentState.Enrolled] = initialEnrollmentPercentage;

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
                transitionRate = new StudentEnrollmentTransition(
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
            var transitionRate = new StudentEnrollmentTransition(
                StudentEnrollmentState.Enrolled, 
                enrollmentState,
                _flatMultiplicativeVector,
                monthlyPeriod);

            transitionRatesArray[transitionRate] = transitionRate;

            var baseTransitionRate = targetValue / priorPeriodEnrollment;
            transitionRate.SetBaseTransitionRate(baseTransitionRate);
        }        
    }
}
