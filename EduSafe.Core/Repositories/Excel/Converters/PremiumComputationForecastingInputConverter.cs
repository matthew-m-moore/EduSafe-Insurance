﻿using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.IO.Excel.Records;
using System.Collections.Generic;

namespace EduSafe.Core.Repositories.Excel.Converters
{
    public class PremiumComputationForecastingInputConverter
    {
        private PremiumComputationRepository _premiumComputationRepository;
        private ForecastedEnrollmentRepository _forecastedEnrollmentRepository;
        private ForecastedFirstYearPercentageRepository _forecastedFirstYearPercentageRepository;
        private ScenariosRepository _scenariosRepository;

        public PremiumComputationForecastingInputConverter(
            PremiumComputationRepository premiumComputationRepository,
            ForecastedEnrollmentRepository forecastedEnrollmentRepository,
            ForecastedFirstYearPercentageRepository forecastedFirstYearPercentageRepository,
            ScenariosRepository scenariosRepository)
        {
            _premiumComputationRepository = premiumComputationRepository;
            _forecastedEnrollmentRepository = forecastedEnrollmentRepository;
            _forecastedFirstYearPercentageRepository = forecastedFirstYearPercentageRepository;
            _scenariosRepository = scenariosRepository;
        }

        public PremiumComputationForecastingInput Convert(ForecastingParametersRecord forecastingParametersRecord)
        {
            var useNumericalComputation = true;
            var monthlyPeriodsToForecast = forecastingParametersRecord.MonthlyPeriodsToForecast;

            var forecastingScenariosDictionary = _premiumComputationRepository.GetPremiumComputationScenariosByName(useNumericalComputation);
            var forecastedEnrollmentProjection = _forecastedEnrollmentRepository.GetForecastedEnrollmentsProjection();
            var forecastedOverlayScenarios = _scenariosRepository
                .CreateForecastedOverlayScenarios(monthlyPeriodsToForecast, new List<string>(forecastingScenariosDictionary.Keys));

            if (forecastingParametersRecord.ApplyFirstYearPercentGlobally)
            {
                var percentageFirstTimeEnrolleeProjections = 
                    _forecastedFirstYearPercentageRepository.RetrieveGlobalPercentageFirstYearEnrolleeProjections();

                return new PremiumComputationForecastingInput(
                    monthlyPeriodsToForecast,
                    forecastingScenariosDictionary,
                    forecastedEnrollmentProjection,
                    forecastedOverlayScenarios,
                    percentageFirstTimeEnrolleeProjections)
                {
                    IgnoreRollForwardOnRateCurves = forecastingParametersRecord.IgnoreRollForwardOnRateCurves
                };
            }

            _forecastedFirstYearPercentageRepository.PopulatePercentageFirstYearEnrolleeProjections(forecastedEnrollmentProjection);

            return new PremiumComputationForecastingInput(
                monthlyPeriodsToForecast,
                forecastingScenariosDictionary,
                forecastedEnrollmentProjection,
                forecastedOverlayScenarios)
            {
                IgnoreRollForwardOnRateCurves = forecastingParametersRecord.IgnoreRollForwardOnRateCurves
            };
        }
    }
}
