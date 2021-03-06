﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using EduSafe.Core.BusinessLogic.Scenarios.ScenarioLogic;
using EduSafe.Core.Interfaces;
using EduSafe.Core.Repositories.Excel.Converters;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel
{
    public class ShockScenariosRepository : ExcelDataRepository
    {
        private const string _shockParametersTab = "ShockParameters";

        private List<ShockParametersRecord> _shockParametersRecords;
        private bool _hasSelectedShockScenario;

        public string SelectedShockScenario { get; }

        public ShockScenariosRepository(string pathToExcelFile, string selectedShockScenario = null) : base(pathToExcelFile)
        {
            _shockParametersRecords = _ExcelFileReader
                .GetTransposedDataFromSpecificTab<ShockParametersRecord>(_shockParametersTab);

            SelectedShockScenario = selectedShockScenario;
            _hasSelectedShockScenario = !string.IsNullOrWhiteSpace(SelectedShockScenario);
        }

        public ShockScenariosRepository(Stream fileStream, string selectedShockScenario = null) : base(fileStream)
        {
            _shockParametersRecords = _ExcelFileReader
                .GetTransposedDataFromSpecificTab<ShockParametersRecord>(_shockParametersTab);

            SelectedShockScenario = selectedShockScenario;
            _hasSelectedShockScenario = !string.IsNullOrWhiteSpace(SelectedShockScenario);
        }

        /// <summary>
        /// Retrieves a dictionary of scenarios based on the shock parameters records provided.
        /// </summary>
        public Dictionary<string, List<IScenario>> RetrieveAllScenarios()
        {
            var dictionaryOfScenarios = new Dictionary<string, List<IScenario>>();
            foreach (var shockParametersRecord in _shockParametersRecords)
            {
                var shockScenarioName = shockParametersRecord.ShockScenarioName;

                IScenario scenario = null;
                if (string.IsNullOrWhiteSpace(shockScenarioName))
                {
                    scenario = ScenarioLogicConverter.ConvertToScenario(shockParametersRecord);
                    shockScenarioName = scenario.ScenarioName;
                }

                if (!dictionaryOfScenarios.ContainsKey(shockScenarioName))
                    dictionaryOfScenarios.Add(shockScenarioName, new List<IScenario>());

                AddScenarioToList(dictionaryOfScenarios[shockScenarioName], shockParametersRecord, scenario);
            }

            return dictionaryOfScenarios;
        }

        /// <summary>
        /// Creates a dictionary of overlay scenarios by forecasting period and forecasting scenario name. If no forecasting
        /// scenario name is provided, the default is to apply the overlay scenario to the entire forecasting period.
        /// </summary>
        public Dictionary<int, Dictionary<string, List<IScenario>>> 
            CreateForecastedOverlayScenarios(int monthlyPeriodsToForecast, List<string> scenarioNames)
        {
            var forecastedOverlayScenarioDictionary = new Dictionary<int, Dictionary<string, List<IScenario>>>();
            if (!_hasSelectedShockScenario) return forecastedOverlayScenarioDictionary;

            foreach (var shockParametersRecord in _shockParametersRecords)
            {
                if (shockParametersRecord.ShockScenarioName != SelectedShockScenario) continue;

                // The rationale behind creating the scenario here is that it saves memory, since duplicate scenario objects
                // for multiple forecasting periods are all pointing back to the same reference.
                var scenario = ScenarioLogicConverter.ConvertToScenario(shockParametersRecord);
                var overlayScenarioStartPeriod = shockParametersRecord.StartingForecastPeriod.GetValueOrDefault(0);
                var overlayScenarioEndPeriod = shockParametersRecord.EndingForecastPeriod.GetValueOrDefault(monthlyPeriodsToForecast);

                for (var forecastingPeriod = overlayScenarioStartPeriod; forecastingPeriod <= overlayScenarioEndPeriod; forecastingPeriod++)
                {
                    if (!forecastedOverlayScenarioDictionary.ContainsKey(forecastingPeriod))
                        forecastedOverlayScenarioDictionary.Add(forecastingPeriod, new Dictionary<string, List<IScenario>>());

                    var forecastingScenarioName = shockParametersRecord.ForecastingScenario;
                    if (string.IsNullOrEmpty(forecastingScenarioName))
                    {
                        foreach (var scenarioName in scenarioNames)
                        {
                            if (!forecastedOverlayScenarioDictionary[forecastingPeriod].ContainsKey(scenarioName))
                                forecastedOverlayScenarioDictionary[forecastingPeriod].Add(scenarioName, new List<IScenario>());

                            AddScenarioToList(
                                forecastedOverlayScenarioDictionary[forecastingPeriod][scenarioName],
                                shockParametersRecord,
                                scenario);
                        }
                    }
                    else
                    {
                        if (!forecastedOverlayScenarioDictionary[forecastingPeriod].ContainsKey(forecastingScenarioName))
                            forecastedOverlayScenarioDictionary[forecastingPeriod].Add(forecastingScenarioName, new List<IScenario>());

                        AddScenarioToList(
                            forecastedOverlayScenarioDictionary[forecastingPeriod][forecastingScenarioName],
                            shockParametersRecord,
                            scenario);
                    }
                }
            }

            return forecastedOverlayScenarioDictionary;
        }

        private void AddScenarioToList(List<IScenario> listOfScenarios, ShockParametersRecord shockParametersRecord,
            IScenario scenarioProvided = null)
        {
            if (shockParametersRecord.ShockType.ToLower() == ScenarioLogicConverter.ServicingCostsScenario)
            {
                // It may be possible to skip creation of a new object for servicing cost scenarios
                AddToExistingServicingCostsScenario(listOfScenarios, shockParametersRecord);
            }
            else
            {
                var scenario = scenarioProvided ?? ScenarioLogicConverter.ConvertToScenario(shockParametersRecord);
                listOfScenarios.Add(scenario);
            }
        }

        private void AddToExistingServicingCostsScenario(List<IScenario> listOfScenarios, ShockParametersRecord shockParametersRecord,
            IScenario scenarioProvided = null)
        {
            var shockLogic = ShockLogicConvertor.Convert(
                shockParametersRecord.ShockLogicType,
                shockParametersRecord.ShockValue);

            foreach (var servicingCostScenario in listOfScenarios.OfType<ServicingCostsModelShockScenario>())
            {
                // I am assuming here that the first scenario encountered is the right now to add a name to
                if (servicingCostScenario.ShockLogic.Equals(shockLogic) && 
                    shockParametersRecord.AllowPremiumsToAdjust == servicingCostScenario.AllowPremiumsToAdjust)
                {
                    servicingCostScenario.AddCostOrFeeName(shockParametersRecord.SpecificCostOrFeeName);
                    return;
                }
            }

            var scenario = scenarioProvided ?? ScenarioLogicConverter.ConvertToScenario(shockLogic, shockParametersRecord);
            listOfScenarios.Add(scenario);
        }
    }
}
