import { NotificationHistoryEntry } from './notificationHistoryEntry';
import { PaymentHistoryEntry } from './paymentHistoryEntry';
import { ClaimStatusEntry } from './claimStatusEntry';
import { ClaimOptionEntry } from './claimOptionEntry';
import { ClaimPaymentEntry } from './claimPaymentEntry';

export class CustomerProfileEntry {
  CustomerIdNumber: number;
  CustomerUniqueId: string;
  CustomerName: string;
  CustomerAddress1: string;
  CustomerAddress2: string;
  CustomerAddress3: string;
  CustomerCity: string;
  CustomerState: string;
  CustomerZip: string;
  CustomerEmails: string[];
  CollegeName: string;
  CollegeMajor: string;
  CollegeMinor: string;
  CollegeStartDate: Date;
  ExpectedGraduationDate: Date;
  NotificationHistoryEntries: NotificationHistoryEntry[];
  CustomerBalance: number;
  MonthlyPaymentAmount: number;
  TotalPaidInPremiums: number;
  NextPaymentDueDate: Date;
  PaymentHistoryEntries: PaymentHistoryEntry[];
  TotalCoverageAmount: number;
  RemainingCoverageAmount: number;
  CoverageMonths: number;
  ClaimStatusEntries: ClaimStatusEntry[];
  ClaimOptionEntries: ClaimOptionEntry[];
  ClaimPaymentEntries: ClaimPaymentEntry[];
  EnrollmentVerified: boolean;
  GraduationVerified: boolean;
}
