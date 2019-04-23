using System;
using EduSafe.Core.BusinessLogic.Scenarios.ScenarioLogic;
using EduSafe.Core.BusinessLogic.Scenarios.Shocks;
using EduSafe.Core.Interfaces;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class ScenarioLogicConverter
    {
        private const string _interestRateScenario = "interest rate";
        private const string _optionalityScenario = "optionality";
        private const string _enrollmentTargetScenario = "enrollment target";
        private const string _enrollmentTransitionScenario = "enrollment transition";

        public const string ServicingCostsScenario = "servicing costs";

        public static IScenario ConvertToScenario(ShockParametersRecord shockParametersRecord)
        {
            var shockLogic = ShockLogicConvertor.Convert(
                shockParametersRecord.ShockLogicType,
                shockParametersRecord.ShockValue);

            return ConvertToScenario(shockLogic, shockParametersRecord);
        }

        public static IScenario ConvertToScenario(ShockLogic shockLogic, ShockParametersRecord shockParametersRecord)
        {
            var shockScenarioType = shockParametersRecord.ShockType;
            if (string.IsNullOrWhiteSpace(shockScenarioType))
                throw new Exception("ERROR: A shock scenario type such as 'Interest Rate' or 'Enrollment Target' must be provided.");

            switch (shockScenarioType.ToLower())
            {
                case _interestRateScenario:
                    return CreateInterestRateScenario(shockLogic, shockParametersRecord);

                case _optionalityScenario:
                    return CreateOptionalityScenario(shockLogic, shockParametersRecord);

                case _enrollmentTargetScenario:
                    return CreateEnrollmentTargetScenario(shockLogic, shockParametersRecord);

                case _enrollmentTransitionScenario:
                    return CreateEnrollmentTransitionScenario(shockLogic, shockParametersRecord);

                case ServicingCostsScenario:
                    return CreateServicingCostsScenario(shockLogic, shockParametersRecord);

                default:
                    throw new Exception(string.Format("ERROR: Shock scenario type of '{0}' is not supported, please check inputs.",
                        shockScenarioType));
            }
        }

        private static IScenario CreateInterestRateScenario(ShockLogic shockLogic, ShockParametersRecord shockParametersRecord)
        {
            var shockScenarioName = string.IsNullOrWhiteSpace(shockParametersRecord.ShockScenarioName)
                ? _interestRateScenario.ToUpper() + ": " 
                    + shockParametersRecord.ShockLogicType + ", " 
                    + shockParametersRecord.ShockValue
                : shockParametersRecord.ShockScenarioName;

            return new InterestRateShockScenario(shockLogic, 
                shockParametersRecord.AllowPremiumsToAdjust)
            {
                ScenarioName = shockScenarioName
            };
        }

        private static IScenario CreateOptionalityScenario(ShockLogic shockLogic, ShockParametersRecord shockParametersRecord)
        {
            var enrollmentState = StudentEnrollmentStateConverter
                .ConvertStringToEnrollmentState(shockParametersRecord.EnrollmentTargetState);

            var shockScenarioName = string.IsNullOrWhiteSpace(shockParametersRecord.ShockScenarioName)
                ? _optionalityScenario.ToUpper() + ": " 
                    + enrollmentState + ", " 
                    + shockParametersRecord.ShockLogicType + ", " 
                    + shockParametersRecord.ShockValue
                : shockParametersRecord.ShockScenarioName;

            return new OptionalityShockScenario(enrollmentState, shockLogic, 
                shockParametersRecord.AllowPremiumsToAdjust)
            {
                ScenarioName = shockScenarioName
            };
        }

        private static IScenario CreateEnrollmentTargetScenario(ShockLogic shockLogic, ShockParametersRecord shockParametersRecord)
        {
            var enrollmentTargetState = StudentEnrollmentStateConverter
                .ConvertStringToEnrollmentState(shockParametersRecord.EnrollmentTargetState);

            var monthlyTargetPeriod = shockParametersRecord.MonthlyTargetPeriod.GetValueOrDefault(-1);

            var shockScenarioName = string.IsNullOrWhiteSpace(shockParametersRecord.ShockScenarioName)
                ? _enrollmentTargetScenario.ToUpper() + ": " 
                    + enrollmentTargetState + ", " 
                    + (monthlyTargetPeriod >= 0 ? "Period " + monthlyTargetPeriod : "All Periods") + ", " 
                    + shockParametersRecord.ShockLogicType + ", " 
                    + shockParametersRecord.ShockValue
                : shockParametersRecord.ShockScenarioName;

            return new EnrollmentModelTargetShockScenario(enrollmentTargetState, shockLogic, monthlyTargetPeriod,
                shockParametersRecord.AllowPremiumsToAdjust)
            {
                ScenarioName = shockScenarioName
            };
        }

        private static IScenario CreateEnrollmentTransitionScenario(ShockLogic shockLogic, ShockParametersRecord shockParametersRecord)
        {
            var startEnrollmentState = StudentEnrollmentStateConverter
                .ConvertStringToEnrollmentState(shockParametersRecord.StartEnrollmentState);
            var endEnrollmentState = StudentEnrollmentStateConverter
                .ConvertStringToEnrollmentState(shockParametersRecord.EndEnrollmentState);

            var monthlyPeriod = shockParametersRecord.MonthlyTargetPeriod;

            var shockScenarioName = string.IsNullOrWhiteSpace(shockParametersRecord.ShockScenarioName)
                ? _enrollmentTargetScenario.ToUpper() + ": "
                    + startEnrollmentState + ", "
                    + endEnrollmentState + ", "
                    + (monthlyPeriod.HasValue ? "Period " + monthlyPeriod.Value : "All Periods") + ", "
                    + shockParametersRecord.ShockLogicType + ", "
                    + shockParametersRecord.ShockValue
                : shockParametersRecord.ShockScenarioName;

            return new EnrollmentModelTransitionShockScenario(startEnrollmentState, endEnrollmentState, shockLogic, monthlyPeriod,
                shockParametersRecord.AllowPremiumsToAdjust)
            {
                ScenarioName = shockScenarioName
            };
        }

        private static IScenario CreateServicingCostsScenario(ShockLogic shockLogic, ShockParametersRecord shockParametersRecord)
        {
            var isSpecificCostOrFeeShock = !string.IsNullOrWhiteSpace(shockParametersRecord.SpecificCostOrFeeName);

            var shockScenarioName = string.IsNullOrWhiteSpace(shockParametersRecord.ShockScenarioName)
                ? _interestRateScenario.ToUpper() + ": "
                    + (isSpecificCostOrFeeShock ? "'"+ shockParametersRecord.SpecificCostOrFeeName +"', " : string.Empty)
                    + shockParametersRecord.ShockLogicType + ", "
                    + shockParametersRecord.ShockValue
                : shockParametersRecord.ShockScenarioName;

            var servicingCostsShockScenario = new ServicingCostsModelShockScenario(shockLogic,
                shockParametersRecord.AllowPremiumsToAdjust)
            {
                ScenarioName = shockScenarioName
            };

            if (isSpecificCostOrFeeShock)
                servicingCostsShockScenario.AddCostOrFeeName(shockParametersRecord.SpecificCostOrFeeName);

            return servicingCostsShockScenario;
        }
    }
}
