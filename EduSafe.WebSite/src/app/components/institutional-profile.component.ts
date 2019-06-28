import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NotificationHistoryComponent } from '../components/notification-history.component'
import { PaymentHistoryComponent } from '../components/payment-history.component'
import { IndividualProfileComponent } from '../components/individual-profile.component'

import { InstitutionProfileEntry } from '../classes/institutionProfileEntry';

import { ServicingDataService } from '../services/servicingData.service';

@Component({
  selector: 'institutional-profile',
  templateUrl: '../views/institutional-profile.component.html',
  styleUrls: ['../styles/institutional-profile.component.css']
})

export class InstitutionalProfileComponent implements OnInit {
  institutionProfileEntry: InstitutionProfileEntry;
  customerNumber: string;

  public isCustomerInformationRetrievedSuccessfully = true;
  public customerHasPaymentHistory = false;
  public customerHasNotificationHistory = false;

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

  ngOnInit(): void {
    this.servicingDataService.getInstituionalServicingData(this.customerNumber)
      .then(result => {
        this.institutionProfileEntry = result;
        if (!this.institutionProfileEntry.CustomerIdNumber) {
          this.isCustomerInformationRetrievedSuccessfully = false;
        }   
        else {
          this.paymentHistoryComponent =
            new PaymentHistoryComponent(this.institutionProfileEntry.PaymentHistoryEntries);
          this.notificationHistoryComponent =
            new NotificationHistoryComponent(this.institutionProfileEntry.NotificationHistoryEntries);

          this.customerHasPaymentHistory = this.paymentHistoryComponent.checkPaymentHistory();
          this.customerHasNotificationHistory = this.notificationHistoryComponent.checkNotificationHistory();
        }
      });
  }
}
