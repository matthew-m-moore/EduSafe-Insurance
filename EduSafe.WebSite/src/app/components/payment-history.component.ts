import { Component, Input, OnInit } from '@angular/core';

import { PaymentHistoryEntry } from '../classes/paymentHistoryEntry';

@Component({
  selector: 'payment-history',
  templateUrl: '../views/payment-history.component.html',
  styleUrls: ['../styles/payment-history.component.css']
})

export class PaymentHistoryComponent {
  public paymentHistoryEntries: PaymentHistoryEntry[]

  constructor(paymentHistoryEntries: PaymentHistoryEntry[]) {
      this.paymentHistoryEntries = paymentHistoryEntries;
  }

  checkPaymentHistory(): boolean {
    if (this.paymentHistoryEntries)
      return this.paymentHistoryEntries.length > 0

    return false;
  }
}
