using System;
using System.Collections.Generic;
using EduSafe.Common.Enums;

namespace EduSafe.Common
{
    public static class Constants
    {
        public const string DatabaseOwnerSchemaName = "dbo";
        public const string IndividualCustomerSchemaName = "cust";

        public const string PeriodIdentifier = "Period";
        public const string TotalIdentifier = "Total";

        public static DateTime SqlMinDate = new DateTime(1900, 1, 1);

        public static int ProcessorCount = Environment.ProcessorCount;

        public const int MonthsInOneYear = 12;

        public const int PercentagePoints = 100;

        public const int ThreeHundredSixtyDaysInOneYear = 360;
        public const int ThreeHundredSixtyFiveDaysInOneYear = 365;
        public const int ThreeHundredSixtySixDaysInOneYear = 366;

        public const int ThirtyDaysInOneMonth = 30;
        public const int ThirtyOneDaysInOneMonth = 31;

        public const int IndividalAccountNumberLength = 11;

        public static readonly List<InterestRateCurveType> DiscountFactorCurves = new List<InterestRateCurveType>
        {
            InterestRateCurveType.LiborDiscount,
            InterestRateCurveType.TreasuryDiscount
        };

        public static readonly List<InterestRateCurveType> ZeroVolatilitySpotRateCurves = new List<InterestRateCurveType>
        {
            InterestRateCurveType.LiborSpot,
            InterestRateCurveType.TreasurySpot
        };

        public static readonly List<StudentEnrollmentState> PostGraduationEnrollmentStates = new List<StudentEnrollmentState>
        {
            StudentEnrollmentState.GraduatedEmployed,
            StudentEnrollmentState.GraduatedUnemployed,
            StudentEnrollmentState.GraduateSchool
        };
    }
}
