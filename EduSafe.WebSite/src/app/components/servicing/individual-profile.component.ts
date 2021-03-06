import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { CustomerProfileEntry } from '../../classes/servicing/customerProfileEntry';
import { CustomerEmailEntry } from '../../classes/servicing/customerEmailEntry';

import { AuthenticationService } from '../../services/authentication.service';
import { ServicingDataService } from '../../services/servicingData.service';
import { ExcelExportService } from '../../services/excelExport.service';

@Component({
  selector: 'individual-profile',
  templateUrl: '../../views/servicing/individual-profile.component.html',
  styleUrls: ['../../styles/servicing/individual-profile.component.css']
})

export class IndividualProfileComponent implements OnInit {
  customerProfileEntry: CustomerProfileEntry;
  customerNumber: string;
  insitutionIdentifer: string;

  public isCustomerInformationRetrievedSuccessfully = true;
  public canNewEmailBeAdded = false;
  public customerHasPaymentHistory = false;
  public customerHasNotificationHistory = false;
  public customerHasClaims = false;
  public showClaimsHistory = false;

  isAcademicInformationCollapsed = true;
  isCustomerInformationCollapsed = true;
  isPaymentHistoryCollapsed = true;
  isNotificationHistoryCollapsed = true;

  @Input() newEmailAddress: string;
  @Input() isNewEmailAddressPrimary: boolean;

  constructor(
    private authenticationService: AuthenticationService,
    private servicingDataService: ServicingDataService,
    private excelExportService: ExcelExportService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
      this.customerProfileEntry = new CustomerProfileEntry();
      this.activatedRoute.queryParams
        .subscribe((params) => {
          this.customerNumber = params.customerNumber;
          this.insitutionIdentifer = params.institutionIdentifer;
        });
  }

  checkPassedViaInstitution(): boolean {
    if (this.insitutionIdentifer)
      return this.customerProfileEntry
        .InstitutionIdentifers.some(identifier => identifier === this.insitutionIdentifer);
    else
      return true;
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
          this.customerProfileEntry.CustomerEmails =
            this.customerProfileEntry.CustomerEmails.filter(email => email.EmailAddress !== emailEntry.EmailAddress);
          });
  }

  addEmailAddress(): void {
    var customerEmailEntry = new CustomerEmailEntry();
    customerEmailEntry.EmailAddress = this.newEmailAddress;
    customerEmailEntry.IsPrimary = this.isNewEmailAddressPrimary;
    customerEmailEntry.EmailSetId = this.customerProfileEntry.EmailSetId;

    this.servicingDataService
      .addNewEmailAddress(customerEmailEntry)
      .then(resultEmailId => {
        if (resultEmailId > 0) {
          customerEmailEntry.EmailId = resultEmailId;
          this.customerProfileEntry.CustomerEmails.push(customerEmailEntry);

          if (this.isNewEmailAddressPrimary) {
            this.customerProfileEntry.CustomerEmails.forEach(email => {
              if (email.EmailAddress === this.newEmailAddress)
                email.IsPrimary = true;
              else
                email.IsPrimary = false;
            });
          }
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

  exportPaymentsToExcel(): void {
    this.excelExportService.getIndividualPaymentsExport(this.customerProfileEntry);
  }

  checkClaimsHistory(): boolean {
    if (!this.customerProfileEntry.ClaimStatusEntries)
      return false;
    if (this.customerProfileEntry.ClaimStatusEntries.length > 0)
      return true;
  }

  toggleClaimsDetail(): void {
    this.showClaimsHistory = !this.showClaimsHistory;
  }

  checkPaymentHistory(): boolean {
    if (this.customerProfileEntry.PaymentHistoryEntries)
      return this.customerProfileEntry.PaymentHistoryEntries.length > 0
    else
      return false;
  }

  checkNotificationHistory(): boolean {
    if (this.customerProfileEntry.NotificationHistoryEntries)
      return this.customerProfileEntry.NotificationHistoryEntries.length > 0
    else
      return false;
  }

  ngOnInit(): void {
    if (!this.authenticationService.isAuthenticated && !this.insitutionIdentifer)
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
            if (!this.checkPassedViaInstitution())
              this.goBackToAuthentication();

            this.customerHasPaymentHistory = this.checkPaymentHistory();
            this.customerHasNotificationHistory = this.checkNotificationHistory();
            this.customerHasClaims = this.checkClaimsHistory();
          }
        });
      }
  }
}
