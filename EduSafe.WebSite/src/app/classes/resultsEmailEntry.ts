import { ModelInputEntry } from './modelInputEntry';
import { ModelOutputSummary } from './modelOutputSummary';

export class ResultsEmailEntry {
  RecipientAddress: string;
  RecipientName: string
  ResultsPageHtml: string;
  ModelInputEntry: ModelInputEntry;
  ModelOutputSummary: ModelOutputSummary;
}
