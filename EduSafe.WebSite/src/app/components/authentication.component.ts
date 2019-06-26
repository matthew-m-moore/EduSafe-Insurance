import { Component, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal'

import { AuthenticationPackage } from '../classes/authenticationPackage';
import { EnvironmentSettings } from '../classes/environmentSettings';

import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'portal-authentication',
  templateUrl: '../views/authentication.component.html',
  styleUrls: ['../styles/authentication.component.css']
})

// Eventually, there would need to be an inactivity timer to log out
export class AuthenticationComponent {
  authenticationPackage: AuthenticationPackage;
  modalReference: BsModalRef;
  customerNumbers: string[];
  userIdentifier: string;
  passwordEntered: string;

  public authenticationFailedBadUserIdentifier = false;
  public authenticationFailedBadPassword = false;
  public hidePasswordEntered = true;
  public isAuthenticated = false;

  constructor(
    private authenticationService: AuthenticationService,
    private modalService: BsModalService,
    private router: Router)
  { }

  showHidePassword(): void {
    this.hidePasswordEntered = !this.hidePasswordEntered;
  }

  authenticateUser(userIdentifier: string, passwordEntered: string): void {
    this.authenticationFailedBadUserIdentifier = false;
    this.authenticationFailedBadPassword = false;

    // This password entered will eventually need to actually be encrypted somehow
    this.authenticationPackage.EncryptedPassword = passwordEntered;
    this.authenticationPackage.CustomerIdentifier = userIdentifier;

    this.authenticationService
      .retrieveCustomerNumbersForIdentifier(userIdentifier)
      .then(result => this.customerNumbers = result);

    if (this.customerNumbers.length > 1)
      document.getElementById("openChooseCustomerNumberModal").click();
    else if (this.customerNumbers.length === 0)
      this.authenticationFailedBadUserIdentifier = true;
    else
      this.authenticationService.authenticateUser(this.authenticationPackage)
        .then(authenticationResult => {
          if (authenticationResult === true)
            this.loadCustomerProfile(userIdentifier);
          else
            this.authenticationFailedBadPassword = true;
        });     
  }

  loadCustomerProfile(userIdentifier: string): void {
    if (userIdentifier.length < EnvironmentSettings.IndividalAccountNumberLength) {
      this.router.navigate(['/institutional-profile'], {
        queryParams: { customerNumber: userIdentifier }
        });
    }
    else {
      this.router.navigate(['/individual-profile'], {
        queryParams: { customerNumber: userIdentifier }
      });
    }
  }

  openChooseCustomerNumberModal(chooseCustomerNumberTemplate: TemplateRef<any>): void {
    this.modalReference = this.modalService
      .show(chooseCustomerNumberTemplate, { class: 'modal-sm', animated: true });
  }

  chooseCustomerNumber(customerNumber: string): void {
    this.authenticationPackage.CustomerIdentifier = customerNumber;
    this.modalReference.hide();
    this.authenticate();
  }

  authenticate(): void {
    this.authenticateUser(
      this.authenticationPackage.CustomerIdentifier,
      this.authenticationPackage.EncryptedPassword);
  }
}
