import { CustomerEmailEntry } from './customerEmailEntry';
import { PaymentHistoryEntry } from './paymentHistoryEntry';
import { CustomerProfileEntry } from './customerProfileEntry';
import { NotificationHistoryEntry } from './notificationHistoryEntry';

export class InstitutionProfileEntry {
  CustomerIdNumber: number;
  CustomerUniqueId: string;
  CustomerName: string;
  CustomerAddress1: string;
  CustomerAddress2: string;
  CustomerAddress3: string;
  CustomerCity: string;
  CustomerState: string;
  CustomerZip: string;
  EmailSetId: number;
  CustomerEmails: CustomerEmailEntry[];
  CustomerBalance: number;
  MonthlyPaymentAmount: number;
  NextPaymentDueDate: Date;
  PaymentHistoryEntries: PaymentHistoryEntry[];
  CustomerProfileEntries: CustomerProfileEntry[];
  NotificationHistoryEntries: NotificationHistoryEntry[];
}
