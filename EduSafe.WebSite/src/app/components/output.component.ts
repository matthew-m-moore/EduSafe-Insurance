import { Component, Input, OnInit, AfterViewInit, ElementRef, TemplateRef } from '@angular/core';

import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal'

import { ModelComponent } from '../components/model.component';

import { ModelOutputSummary } from '../classes/modelOutputSummary';
import { ResultsEmailEntry } from '../classes/resultsEmailEntry';

import { SendEmailService } from '../services/sendEmail.Service';

@Component({
  selector: 'edusafe-output',
  templateUrl: '../views/output.component.html',
  styleUrls: ['../styles/output.component.css']
})

export class ModelOuputComponent implements OnInit, AfterViewInit {
  @Input() resultsEmailEntry: ResultsEmailEntry;
  modelOutputSummary: ModelOutputSummary;
  modalReference: BsModalRef;
  modalMessage: string;

  public isEmailSent = false;

  constructor(
    private modelComponent: ModelComponent,
    private elementRef: ElementRef,
    private modalService: BsModalService,
    private sendEmailService: SendEmailService
  ) { }

  revealModelInputsAgain(): void {
    this.modelComponent.isCalculated = false;
    this.isEmailSent = false;
    window.scroll(0, 0);
  }

  openConfirmationModal(provideEmailTemplate: TemplateRef<any>) {
    this.modalReference = this.modalService.show(provideEmailTemplate, { class: 'modal-sm' });
  }

  confirmSendEmail(): void {
    this.sendResultsEmail();
    this.modalMessage = "Results Email Sent";
    this.modalReference.hide();
  }

  declineSendEmail(): void {
    this.modalMessage = "Results Email Declined";
    this.modalReference.hide();
  }

  sendResultsEmail(): void {
    this.sendEmailService.sendResultsEmail(this.resultsEmailEntry)
      .then(emailSuccess => this.isEmailSent = emailSuccess);
  }

  ngAfterViewInit() {
    this.resultsEmailEntry.ResultsPageHtml = this.elementRef.nativeElement.innerHTML;
  }

  ngOnInit(): void {
    this.modelOutputSummary = this.modelComponent.modelOutputSummary;

    this.resultsEmailEntry = new ResultsEmailEntry();
    this.resultsEmailEntry.ModelInputEntry = this.modelComponent.modelInputEntry;
    this.resultsEmailEntry.ModelOutputSummary = this.modelOutputSummary;
  }
}
