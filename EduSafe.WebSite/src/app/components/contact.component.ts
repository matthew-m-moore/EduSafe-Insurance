import { Component, Input, OnInit } from '@angular/core';

import { AppRootComponent } from '../components/app-root.component';

import { InquiryEmailEntry } from '../classes/inquiryEmailEntry';

import { ActivityCaptureService } from '../services/activityCapture.service';
import { SendEmailService } from '../services/sendEmail.Service';

@Component({
  selector: 'edusafe-contact',
  templateUrl: '../views/contact.component.html',
  styleUrls: ['../styles/contact.component.css']
})

export class ContactComponent implements OnInit {
  @Input() inquiryEmailEntry: InquiryEmailEntry;

  public isEmailSent = false;
  public isSending = false;

  constructor(
    private appRootComponent: AppRootComponent,
    private sendEmailService: SendEmailService,
    private activityCaptureService: ActivityCaptureService
  ) { }

  sendInquiryEmail(): void {
    this.isSending = true;
    this.sendEmailService.sendInquiryEmail(this.inquiryEmailEntry)
      .then(emailSuccess => {
        this.isEmailSent = emailSuccess;
        this.isSending = false;
      });

    this.activityCaptureService.captureInquiryEmailActivity(this.inquiryEmailEntry);
  }

  ngOnInit(): void {
    this.inquiryEmailEntry = new InquiryEmailEntry();
    this.inquiryEmailEntry.IpAddress = this.appRootComponent.ipAddress;
  }
}
