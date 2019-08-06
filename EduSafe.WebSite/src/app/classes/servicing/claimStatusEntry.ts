import { ClaimDocumentEntry } from './claimDocumentEntry';

export class ClaimStatusEntry {
  ClaimNumber: number;
  ClaimType: string;
  ClaimStatus: string;
  IsClaimApproved: boolean;
  ClaimDocumentEntries: ClaimDocumentEntry[];
}
