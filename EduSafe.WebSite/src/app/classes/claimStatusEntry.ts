import { ClaimDocumentEntry } from './claimDocumentEntry';

export class ClaimStatusEntry {
  ClaimType: string;
  ClaimStatus: string;
  IsClaimApproved: boolean;
  ClaimDocumentEntries: ClaimDocumentEntry[];
}
