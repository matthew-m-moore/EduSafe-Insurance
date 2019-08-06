import { InstitutionInputEntry } from './institutionInputEntry';
import { InstitutionOutputSummary } from './institutionOutputSummary';

export class InstitutionResultEmailEntry {
  RecipientAddress: string;
  RecipientName: string
  ResultsPageHtml: string;
  InstitutionInputEntry: InstitutionInputEntry;
  InstitutionOutputSummary: InstitutionOutputSummary;
}
