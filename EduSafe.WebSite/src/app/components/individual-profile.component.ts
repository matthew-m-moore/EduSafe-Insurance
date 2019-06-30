import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NotificationHistoryComponent } from '../components/notification-history.component'
import { PaymentHistoryComponent } from '../components/payment-history.component'

import { CustomerProfileEntry } from '../classes/customerProfileEntry';
import { CustomerEmailEntry } from '../classes/customerEmailEntry';

import { ServicingDataService } from '../services/servicingData.service';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'individual-profile',
  templateUrl: '../views/individual-profile.component.html',
  styleUrls: ['../styles/individual-profile.component.css']
})

export class IndividualProfileComponent implements OnInit {
  customerProfileEntry: CustomerProfileEntry;
  customerNumber: string;

  public isCustomerInformationRetrievedSuccessfully = true;
  public canNewEmailBeAdded = false;
  public customerHasPaymentHistory = false;
  public customerHasNotificationHistory = false;
  public customerHasClaims = false;
  public showClaimsHistory = false;

  isCustomerInformationCollapsed = true;
  isPaymentHistoryCollapsed = true;
  isNotificationHistoryCollapsed = true;

  @Input() newEmailAddress: string;
  @Input() isNewEmailAddressPrimary: boolean;

  @ViewChild('paymentHistory') private paymentHistoryComponent: PaymentHistoryComponent;
  @ViewChild('notificationHistory') private notificationHistoryComponent: NotificationHistoryComponent;

  constructor(
    private authenticationService: AuthenticationService,
    private servicingDataService: ServicingDataService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
      this.customerProfileEntry = new CustomerProfileEntry();
      this.activatedRoute.queryParams
        .subscribe((params) => {
          this.customerNumber = params.customerNumber;
        });
  }

  goBackToAuthentication(): void {
    this.router.navigate(['/portal-authentication']);
  }

  makeEmailPrimary(emailEntry: CustomerEmailEntry): void {
    this.servicingDataService.makeEmailAddressPrimary(emailEntry)
      .then(result => {
        if (result === true)
          this.customerProfileEntry.CustomerEmails.forEach(email => {
            if (email.EmailAddress === emailEntry.EmailAddress)
              email.IsPrimary = false;
            else
              email.IsPrimary = true;
          });
      });
  }

  removeEmail(emailEntry: CustomerEmailEntry): void {
    this.servicingDataService.removeAddressPrimary(emailEntry)
      .then(result => {
        if (result === true)
          this.customerProfileEntry.CustomerEmails =
            this.customerProfileEntry.CustomerEmails.filter(email => email.EmailAddress !== emailEntry.EmailAddress);
          });
  }

  addEmailAddress(): void {
    var customerEmailEntry = new CustomerEmailEntry();
    customerEmailEntry.EmailAddress = this.newEmailAddress;
    customerEmailEntry.IsPrimary = this.isNewEmailAddressPrimary;

    this.servicingDataService
      .addNewEmailAddress(customerEmailEntry)
      .then(resultEmailId => {
        if (resultEmailId > 0) {
          customerEmailEntry.EmailId = resultEmailId;
          this.customerProfileEntry.CustomerEmails.push(customerEmailEntry);
        }
      });
  }

  checkIfEmailCanBeAdded(): void {
    if (this.newEmailAddress.includes("@")) {
      this.canNewEmailBeAdded = true;
      this.customerProfileEntry.CustomerEmails.forEach(email => {
        if (email.EmailAddress === this.newEmailAddress)
          this.canNewEmailBeAdded = false;
        });
      }
    else
      this.canNewEmailBeAdded = false;
  };

  checkClaimsHistory(): boolean {
    if (!this.customerProfileEntry.ClaimStatusEntries)
      return false;
    if (this.customerProfileEntry.ClaimStatusEntries.length > 0)
      return true;
  }

  toggleClaimsDetail(): void {
    this.showClaimsHistory = !this.showClaimsHistory;
  }

  ngOnInit(): void {
    if (!this.authenticationService.isAuthenticated)
      this.goBackToAuthentication();
    else {
      this.authenticationService.isAuthenticated = false;
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
}
