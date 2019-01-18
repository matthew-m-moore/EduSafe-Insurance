using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Vectors;

namespace EduSafe.Core.BusinessLogic.Models.StudentEnrollment
{
    public class StudentEnrollmentTransition
    {
        public StudentEnrollmentState StartEnrollmentState { get; }
        public StudentEnrollmentState EndEnrollmentState { get; }

        public int MonthlyPeriod { get; private set; }
        public double BaseTransitionRate { get; private set; }
        public Vector ShapingVector { get; private set; }

        public StudentEnrollmentTransition(
            StudentEnrollmentState startEnrollmentState,
            StudentEnrollmentState endEnrollmentState,
            int monthlyPeriod)
        {
            StartEnrollmentState = startEnrollmentState;
            EndEnrollmentState = endEnrollmentState;  
            MonthlyPeriod = monthlyPeriod;
        }

        public StudentEnrollmentTransition(
            StudentEnrollmentState startEnrollmentState,
            StudentEnrollmentState endEnrollmentState,
            Vector shapingVector,
            int monthlyPeriod) : 
                this(startEnrollmentState, endEnrollmentState, monthlyPeriod)
        {
            ShapingVector = shapingVector;
        }

        public double GetTransitionRate()
        {
            var transitionRate = ShapingVector.ApplyVector(MonthlyPeriod, BaseTransitionRate);
            return transitionRate;
        }

        public void SetTransitionRate(double transitionRate)
        {
            BaseTransitionRate = ShapingVector.UnapplyVector(MonthlyPeriod, BaseTransitionRate);
        }

        public void SetBaseTransitionRate(double baseTransitionRate)
        {
            BaseTransitionRate = baseTransitionRate;
        }

        public void SetShapingVecotr(Vector shapingVector)
        {
            ShapingVector = shapingVector;
        }
    }
}