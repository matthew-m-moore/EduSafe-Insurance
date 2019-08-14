import { Component, Input} from '@angular/core';

import { PaymentHistoryEntry } from '../../classes/servicing/paymentHistoryEntry';

@Component({
  selector: 'payment-history',
  templateUrl: '../../views/servicing/payment-history.component.html',
  styleUrls: ['../../styles/servicing/payment-history.component.css']
})

export class PaymentHistoryComponent {
  @Input() paymentHistoryEntries: PaymentHistoryEntry[]
}
