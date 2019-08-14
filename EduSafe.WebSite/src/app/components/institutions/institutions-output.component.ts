import { Component, Input, OnInit, AfterViewInit, ElementRef, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal'

import { InstitutionsModelComponent } from '../../components/institutions/institutions-model.component';

import { InstitutionOutputSummary } from '../../classes/institutions/institutionOutputSummary';
import { InstitutionOutputEntry } from '../../classes/institutions/institutionOutputEntry';
import { InstitutionResultEmailEntry } from '../../classes/institutions/institutionResultEmailEntry';

import { ActivityCaptureService } from '../../services/activityCapture.service';
import { SendEmailService } from '../../services/sendEmail.Service';


@Component({
  selector: 'institutions-output',
  templateUrl: '../../views/institutions/institutions-output.component.html',
  styleUrls: ['../../styles/institutions/institutions-output.component.css']
})

export class InstitutionsOutputComponent implements OnInit, AfterViewInit {
  @Input() resultsEmailEntry: InstitutionResultEmailEntry;
  institutionOutputSummary: InstitutionOutputSummary;
  institutionOutputEntry: InstitutionOutputEntry;

  modalReference: BsModalRef;
  modalMessage: string;

  public isResultsEmailSent = false;
  public isSendingResults = false;

  constructor(
    private router: Router,
    private institutionsModelComponent: InstitutionsModelComponent,
    private elementRef: ElementRef,
    private modalService: BsModalService,
    private sendEmailService: SendEmailService,
    private activityCaptureService: ActivityCaptureService
  ) { }

  goToHome(): void {
    let routingUrl = ['/institutions'];
    this.router.navigate(routingUrl);
    window.scroll(0, 0);
  }

  revealModelInputsAgain(): void {
    this.isResultsEmailSent = false;
    this.institutionsModelComponent.isCalculated = false;
    window.scroll(0, 0);
  }

  openConfirmationModal(provideEmailTemplate: TemplateRef<any>): void {
    this.isSendingResults = true;
    this.modalReference = this.modalService.show(provideEmailTemplate, { class: 'modal-sm', animated: true });
  }

  confirmSendEmail(): void {
    this.modalReference.hide();
    this.sendResultsEmail();
  }

  declineSendEmail(): void {
    this.modalMessage = "Results Email Declined";
    this.isSendingResults = false;
    this.modalReference.hide();
  }

  sendResultsEmail(): void {
    this.sendEmailService.sendInstitutionResultEmail(this.resultsEmailEntry)
      .then(emailSuccess => {
        this.isResultsEmailSent = emailSuccess;
        this.isSendingResults = false;
      });

    this.modalMessage = "Results Email Sent";
    this.activityCaptureService.captureInstitutionResultEmailActivity(this.resultsEmailEntry);
  }

  ngAfterViewInit() {
    this.resultsEmailEntry.ResultsPageHtml = this.elementRef.nativeElement.innerHTML;
    if (!this.institutionOutputSummary) this.goToHome();
  }

  ngOnInit(): void {
    this.institutionOutputSummary = this.institutionsModelComponent.institutionOutputSummary;
    this.institutionOutputEntry = this.institutionsModelComponent.institutionOutputSummary.InstitutionOutputEntries[0];

    this.resultsEmailEntry = new InstitutionResultEmailEntry();
    this.resultsEmailEntry.InstitutionInputEntry = this.institutionsModelComponent.institutionInputEntry;
    this.resultsEmailEntry.InstitutionOutputSummary = this.institutionOutputSummary;
  }
}
