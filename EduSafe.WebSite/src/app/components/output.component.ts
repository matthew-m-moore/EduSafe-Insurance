import { Component, OnInit, AfterViewInit, ElementRef } from '@angular/core';

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
  modelOutputSummary: ModelOutputSummary;
  resultsHtml: string;

  public isEmailSent = false;

  constructor(
    private modelComponent: ModelComponent,
    private elementRef: ElementRef,
    private sendEmailService: SendEmailService
  ) { }

  revealModelInputsAgain(): void {
    this.modelComponent.isCalculated = false;
    this.isEmailSent = false;
    window.scroll(0, 0);
  }

  sendResultsEmail(): void {
    var resultsEmailEntry = new ResultsEmailEntry();
    resultsEmailEntry.ModelInputEntry = this.modelComponent.modelInputEntry;
    resultsEmailEntry.ModelOutputSummary = this.modelOutputSummary;
    resultsEmailEntry.ResultsPageHtml = this.resultsHtml;
    // I need a modal to capture the user's email address for their results

    this.sendEmailService.sendResultsEmail(resultsEmailEntry)
      .then(emailSuccess => this.isEmailSent = emailSuccess);
  }

  ngAfterViewInit() {
    this.resultsHtml = this.elementRef.nativeElement.innerHTML;
  }

  ngOnInit(): void {
    this.modelOutputSummary = this.modelComponent.modelOutputSummary;
  }
}
