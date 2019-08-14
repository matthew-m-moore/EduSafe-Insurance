import { Component, Input, OnInit, AfterViewInit, ElementRef, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal'

import { ModelComponent } from '../../components/individuals/model.component';

import { ModelOutputSummary } from '../../classes/individuals/modelOutputSummary';
import { ModelOutputEntry } from '../../classes/individuals/modelOutputEntry';
import { ResultsEmailEntry } from '../../classes/individuals/resultsEmailEntry';

import { ActivityCaptureService } from '../../services/activityCapture.service';
import { SendEmailService } from '../../services/sendEmail.Service';

@Component({
  selector: 'edusafe-output',
  templateUrl: '../../views/individuals/output.component.html',
  styleUrls: ['../../styles/individuals/output.component.css']
})

export class ModelOuputComponent implements OnInit, AfterViewInit {
  @Input() resultsEmailEntry: ResultsEmailEntry;
  modelOutputSummary: ModelOutputSummary;
  modelOutputEntry: ModelOutputEntry;
  modelOuputCoverage: number;
  modalReference: BsModalRef;
  modalMessage: string;

  public isResultsEmailSent = false;
  public isSendingResults = false;

  constructor(
    private router: Router,
    private modelComponent: ModelComponent,
    private elementRef: ElementRef,
    private modalService: BsModalService,
    private sendEmailService: SendEmailService,
    private activityCaptureService: ActivityCaptureService
  ) { }

  goToHome(): void {
    let routingUrl = ['/edusafe-home'];
    this.router.navigate(routingUrl);
    window.scroll(0, 0);
  }

  revealModelInputsAgain(): void {
    this.isResultsEmailSent = false;
    this.modelComponent.isCalculated = false;
    window.scroll(0, 0);
  }

  openConfirmationModal(provideEmailTemplate: TemplateRef<any>) : void {
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
    this.sendEmailService.sendResultsEmail(this.resultsEmailEntry)
      .then(emailSuccess => {
        this.isResultsEmailSent = emailSuccess;
        this.isSendingResults = false;
      });
    
    this.modalMessage = "Results Email Sent";
    this.activityCaptureService.captureResultsEmailActivity(this.resultsEmailEntry);
  }

  ngAfterViewInit() {
    this.resultsEmailEntry.ResultsPageHtml = this.elementRef.nativeElement.innerHTML;
  }

  ngOnInit(): void {
    this.modelOutputSummary = this.modelComponent.modelOutputSummary;
    this.modelOutputEntry = this.modelComponent.modelOutputSummary.ModelOutputEntries[2];

    this.resultsEmailEntry = new ResultsEmailEntry();
    this.resultsEmailEntry.ModelInputEntry = this.modelComponent.modelInputEntry;
    this.resultsEmailEntry.ModelOutputSummary = this.modelOutputSummary;
  }
}
