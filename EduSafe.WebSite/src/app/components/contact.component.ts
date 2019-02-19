import { Component, Input, OnInit } from '@angular/core';

import { AppRootComponent } from '../components/app-root.component';

import { InquiryEmailEntry } from '../classes/inquiryEmailEntry';

import { SendEmailService } from '../services/sendEmail.Service';

@Component({
  selector: 'edusafe-contact',
  templateUrl: '../views/contact.component.html',
  styleUrls: ['../styles/contact.component.css']
})

export class ContactComponent implements OnInit {
  @Input() inquiryEmailEntry: InquiryEmailEntry;

  public isEmailSent = false;

  constructor(
    private appRootComponent: AppRootComponent,
    private sendEmailService: SendEmailService
  ) { }

  sendInquiryEmail(): void {
    this.sendEmailService.sendInquiryEmail(this.inquiryEmailEntry)
      .then(emailSuccess => this.isEmailSent = emailSuccess);
  }

  ngOnInit(): void {
    this.inquiryEmailEntry = new InquiryEmailEntry();
    this.inquiryEmailEntry.IpAddress = this.appRootComponent.ipAddress;
  }
}
