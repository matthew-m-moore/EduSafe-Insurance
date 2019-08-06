import { ModelOutputHeaders } from './modelOutputHeaders';
import { ModelOutputEntry } from './modelOutputEntry';

export class ModelOutputSummary {
  OutputTitle: string;
  DropOutCoveragePercentage: number;
  GradSchoolCoveragePercentage: number;
  EarlyHireCoveragePercentage: number;
  ModelOutputHeaders: ModelOutputHeaders;
  ModelOutputEntries: ModelOutputEntry[];
}
