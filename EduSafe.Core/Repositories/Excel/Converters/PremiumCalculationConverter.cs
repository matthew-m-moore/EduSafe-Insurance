using System.Collections.Generic;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Models.Premiums;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class PremiumCalculationConverter
    {
        private Dictionary<string, Dictionary<InterestRateCurveType, InterestRateCurve>> _interestRateCurveSets;

        public PremiumCalculationConverter(
            Dictionary<string, Dictionary<InterestRateCurveType, InterestRateCurve>> interestRateCurveSets)
        {
            _interestRateCurveSets = interestRateCurveSets;
        }

        public PremiumCalculation GetAnalyticalPremiumCalculation(
            EnrollmentModelScenarioRecord enrollmentModelScenarioRecord)
        {
            var premiumCalculationInput = GetPremiumCalculationInput(enrollmentModelScenarioRecord);
            return new AnalyticalPremiumCalculation(premiumCalculationInput);
        }

        public PremiumCalculation GetNumericalPremiumCalculation(
            EnrollmentModelScenarioRecord enrollmentModelScenarioRecord)
        {
            var premiumCalculationInput = GetPremiumCalculationInput(enrollmentModelScenarioRecord);
            return new NumericalPremiumCalculation(premiumCalculationInput);
        }

        private PremiumCalculationModelInput GetPremiumCalculationInput(
            EnrollmentModelScenarioRecord enrollmentModelScenarioRecord)
        {
            var rateCurveSet = enrollmentModelScenarioRecord.RateCurveSet;
            var interestRateCurveType = InterestRateCurveTypeConverter
                .ConvertStringToInterestRateCurveType(enrollmentModelScenarioRecord.DiscountCurve);

            if (!_interestRateCurveSets.ContainsKey(rateCurveSet))
                throw new KeyNotFoundException(
                    string.Format("ERROR: Please check inputs, cannot find rate curves set: '{0}'.", rateCurveSet));

            if (!_interestRateCurveSets[rateCurveSet].ContainsKey(interestRateCurveType))
                throw new KeyNotFoundException(
                    string.Format("ERROR: Please check inputs, cannot find rate type '{0}' in rate curves set: '{1}'.", 
                        interestRateCurveType, rateCurveSet));

            var discountFactorCurve = _interestRateCurveSets[rateCurveSet][interestRateCurveType];

            var annualIncomeCoverage = enrollmentModelScenarioRecord.AnnualIncome;
            var monthsOfIncomeCoverage = enrollmentModelScenarioRecord.CoverageMonths;
            var dropOutCoverageOption = enrollmentModelScenarioRecord.DropOutOptionRatio;
            var gradSchoolCoverageOption = enrollmentModelScenarioRecord.GradSchoolOptionRatio;
            var earlyHireCoverageOption = enrollmentModelScenarioRecord.EarlyHireOptionRatio;

            var premiumCalculationModelInput =
                new PremiumCalculationModelInput(
                    annualIncomeCoverage,
                    monthsOfIncomeCoverage,
                    discountFactorCurve,
                    dropOutCoverageOption,
                    gradSchoolCoverageOption,
                    earlyHireCoverageOption);

            return premiumCalculationModelInput;
        }
    }
}
