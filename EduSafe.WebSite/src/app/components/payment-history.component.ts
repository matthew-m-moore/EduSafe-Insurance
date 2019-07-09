import { Component, Input} from '@angular/core';

import { PaymentHistoryEntry } from '../classes/paymentHistoryEntry';

@Component({
  selector: 'payment-history',
  templateUrl: '../views/payment-history.component.html',
  styleUrls: ['../styles/payment-history.component.css']
})

export class PaymentHistoryComponent {
  @Input() paymentHistoryEntries: PaymentHistoryEntry[]
}
