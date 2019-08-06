import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import { InquiryEmailEntry } from '../classes/inquiryEmailEntry';
import { ResultsEmailEntry } from '../classes/individuals/resultsEmailEntry';
import { InstitutionResultEmailEntry } from '../classes/institutions/institutionResultEmailEntry';
import { EnvironmentSettings } from '../classes/environmentSettings';

@Injectable()

export class SendEmailService {
  private contactEmailUrl = EnvironmentSettings.BaseApiUrl + '/api/email/contact';
  private resultsEmailUrl = EnvironmentSettings.BaseApiUrl + '/api/email/results';
  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: Http) { }

  sendInquiryEmail(inquiryEmailEntry: InquiryEmailEntry): Promise<boolean> {
    return this.http.post(this.contactEmailUrl, JSON.stringify(inquiryEmailEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean)
      .catch(this.reportError);
  }

  sendResultsEmail(resultsEmailEntry: ResultsEmailEntry): Promise<boolean> {
    return this.http.put(this.resultsEmailUrl, JSON.stringify(resultsEmailEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean)
      .catch(this.reportError);
  }

  private reportError(error: any): Promise<any> {
    console.error('Send Email Service Error: ', error);
    return Promise.reject(error.message || error);
  }
}
