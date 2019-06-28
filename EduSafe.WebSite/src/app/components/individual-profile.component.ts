import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NotificationHistoryComponent } from '../components/notification-history.component'
import { PaymentHistoryComponent } from '../components/payment-history.component'
import { ClaimsComponent } from '../components/claims.component'

import { CustomerProfileEntry } from '../classes/customerProfileEntry';

import { ServicingDataService } from '../services/servicingData.service';

@Component({
  selector: 'individual-profile',
  templateUrl: '../views/individual-profile.component.html',
  styleUrls: ['../styles/individual-profile.component.css']
})

export class IndividualProfileComponent implements OnInit {
  customerProfileEntry: CustomerProfileEntry;
  customerNumber: string;

  public isCustomerInformationRetrievedSuccessfully = true;
  public customerHasPaymentHistory = false;
  public customerHasNotificationHistory = false;
  public customerHasClaims = false;
  public showClaimsHistory = false;

  @ViewChild('paymentHistory') private paymentHistoryComponent: PaymentHistoryComponent;
  @ViewChild('notificationHistory') private notificationHistoryComponent: NotificationHistoryComponent;

  constructor(
    private servicingDataService: ServicingDataService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
      this.activatedRoute.queryParams
        .subscribe((params) => {
          this.customerNumber = params.customerNumber;
        });
  }

  goBackToAuthentication(): void {
    this.router.navigate(['/portal-authentication']);
  }

  checkClaimsHistory(): boolean {
    if (!this.customerProfileEntry.ClaimStatusEntries)
      return false;
    if (this.customerProfileEntry.ClaimStatusEntries.length > 0)
      return true;
  }

  toggleClaimsDetail(): void {
    this.customerHasClaims = !this.customerHasClaims;
  }

  ngOnInit(): void {
    this.servicingDataService.getIndividualsServicingData(this.customerNumber)
      .then(result => {
        this.customerProfileEntry = result;
        if (!this.customerProfileEntry.CustomerIdNumber) {
          this.isCustomerInformationRetrievedSuccessfully = false;
        }
        else {
          this.paymentHistoryComponent =
            new PaymentHistoryComponent(this.customerProfileEntry.PaymentHistoryEntries);
          this.notificationHistoryComponent =
            new NotificationHistoryComponent(this.customerProfileEntry.NotificationHistoryEntries);

          this.customerHasPaymentHistory = this.paymentHistoryComponent.checkPaymentHistory();
          this.customerHasNotificationHistory = this.notificationHistoryComponent.checkNotificationHistory();

          this.customerHasClaims = this.checkClaimsHistory();
        }
      });
  }
}
