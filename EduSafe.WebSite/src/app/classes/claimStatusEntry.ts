import { ClaimDocumentEntry } from './claimDocumentEntry';
import { ClaimPaymentEntry } from './claimPaymentEntry'

export class ClaimStatusEntry {
  ClaimType: string;
  ClaimStatus: string;
  IsClaimApproved: boolean;
  ClaimDocumentEntries: ClaimDocumentEntry[];
  ClaimPaymentEntries: ClaimPaymentEntry[];
}
