import { Component, Input, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { InstitutionProfileEntry } from '../../classes/servicing/institutionProfileEntry';
import { CustomerProfileEntry } from '../../classes/servicing/customerProfileEntry';
import { CustomerEmailEntry } from '../../classes/servicing/customerEmailEntry';

import { AuthenticationService } from '../../services/authentication.service';
import { ServicingDataService } from '../../services/servicingData.service';
import { ExcelExportService } from '../../services/excelExport.service';

@Component({
  selector: 'institutional-profile',
  templateUrl: '../views/servicing/institutional-profile.component.html',
  styleUrls: ['../styles/servicing/institutional-profile.component.css']
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

          if (this.isNewEmailAddressPrimary) {
            this.institutionProfileEntry.CustomerEmails.forEach(email => {
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
      this.institutionProfileEntry.CustomerEmails.forEach(email => {
        if (email.EmailAddress === this.newEmailAddress)
          this.canNewEmailBeAdded = false;
      });
    }
    else
      this.canNewEmailBeAdded = false;
  };

  openIndividualCustomerPage(customerProfileEntry: CustomerProfileEntry): void {
    let newTabUrl = this.router.createUrlTree(['/individual-profile'], {
      queryParams: {
        customerNumber: customerProfileEntry.CustomerIdNumber,
        institutionIdentifer: this.institutionProfileEntry.CustomerUniqueId
      }
    });

    window.open(newTabUrl.toString(), '_blank');
  }

  exportStudentsToExcel(): void {
    this.excelExportService.getStudentsExport(this.institutionProfileEntry);
  }

  exportPaymentsToExcel(): void {
    this.excelExportService.getInstitutionPaymentsExport(this.institutionProfileEntry);
  }

  checkPaymentHistory(): boolean {
    if (this.institutionProfileEntry.PaymentHistoryEntries)
      return this.institutionProfileEntry.PaymentHistoryEntries.length > 0
    else
      return false;
  }

  checkNotificationHistory(): boolean {
    if (this.institutionProfileEntry.NotificationHistoryEntries)
      return this.institutionProfileEntry.NotificationHistoryEntries.length > 0
    else
      return false;
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
            this.customerHasPaymentHistory = this.checkPaymentHistory();
            this.customerHasNotificationHistory = this.checkNotificationHistory();
          }
        });
      }
  }
}
