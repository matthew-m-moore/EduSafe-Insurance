import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NotificationHistoryComponent } from '../components/notification-history.component'
import { PaymentHistoryComponent } from '../components/payment-history.component'

import { InstitutionProfileEntry } from '../classes/institutionProfileEntry';
import { CustomerProfileEntry } from '../classes/customerProfileEntry';
import { CustomerEmailEntry } from '../classes/customerEmailEntry';

import { AuthenticationService } from '../services/authentication.service';
import { ServicingDataService } from '../services/servicingData.service';
import { ExcelExportService } from '../services/excelExport.service';


@Component({
  selector: 'institutional-profile',
  templateUrl: '../views/institutional-profile.component.html',
  styleUrls: ['../styles/institutional-profile.component.css']
})

export class InstitutionalProfileComponent implements OnInit {
  institutionProfileEntry: InstitutionProfileEntry;
  customerNumber: string;

  public isCustomerInformationRetrievedSuccessfully = true;
  public canNewEmailBeAdded = false;
  public customerHasPaymentHistory = false;
  public customerHasNotificationHistory = false;

  isCustomerInformationCollapsed = true;
  isStudentInformationCollapsed = true;
  isPaymentHistoryCollapsed = true;
  isNotificationHistoryCollapsed = true;

  @Input() newEmailAddress: string;
  @Input() isNewEmailAddressPrimary: boolean;

  @ViewChild('paymentHistory') private paymentHistoryComponent: PaymentHistoryComponent;
  @ViewChild('notificationHistory') private notificationHistoryComponent: NotificationHistoryComponent;

  constructor(
    private authenticationService: AuthenticationService,
    private servicingDataService: ServicingDataService,
    private excelExportService: ExcelExportService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    this.institutionProfileEntry = new InstitutionProfileEntry();
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
          this.institutionProfileEntry.CustomerEmails.forEach(email => {
            if (email.EmailAddress === emailEntry.EmailAddress)
              email.IsPrimary = true;
            else
              email.IsPrimary = false;
          });
      });
  }

  removeEmail(emailEntry: CustomerEmailEntry): void {
    if (emailEntry.IsPrimary)
      return; // Display: "Cannot remove primary email address."
              // "Please select another primary email address for this account before removing this email address."

    this.servicingDataService.removeEmailAddress(emailEntry)
      .then(result => {
        if (result === true)
          this.institutionProfileEntry.CustomerEmails =
            this.institutionProfileEntry.CustomerEmails.filter(email => email.EmailAddress !== emailEntry.EmailAddress);
      });
  }

  addEmailAddress(): void {
    var customerEmailEntry = new CustomerEmailEntry();
    customerEmailEntry.EmailAddress = this.newEmailAddress;
    customerEmailEntry.IsPrimary = this.isNewEmailAddressPrimary;
    customerEmailEntry.EmailSetId = this.institutionProfileEntry.EmailSetId;

    this.servicingDataService
      .addNewEmailAddress(customerEmailEntry)
      .then(resultEmailId => {
        if (resultEmailId > 0) {
          customerEmailEntry.EmailId = resultEmailId;
          this.institutionProfileEntry.CustomerEmails.push(customerEmailEntry);
        }
      });
  }

  checkIfEmailCanBeAdded(): void {
    if (this.newEmailAddress.includes("@")) {
      this.canNewEmailBeAdded = true;
      this.institutionProfileEntry.CustomerEmails.forEach(email => {
        if (email.EmailAddress === this.newEmailAddress)
          this.canNewEmailBeAdded = false;
      });
    }
    else
      this.canNewEmailBeAdded = false;
  };

  exportToExcel(customerProfileEntries: CustomerProfileEntry[]): void {
    
  }

  openIndividualCustomerPage(customerProfileEntry: CustomerProfileEntry): void {
    let newTabUrl = this.router.createUrlTree(['/individual-profile'], {
      queryParams: { customerNumber: customerProfileEntry.CustomerIdNumber }
    });

    window.open(newTabUrl.toString(), '_blank');
  }

  checkClaimsHistory(customerProfileEntry: CustomerProfileEntry): boolean {
    if (!customerProfileEntry.ClaimStatusEntries)
      return false;
    if (customerProfileEntry.ClaimStatusEntries.length > 0)
      return true;
  }

  ngOnInit(): void {
    if (!this.authenticationService.isAuthenticated)
      this.goBackToAuthentication();
    else {
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
}
