import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import { ActivityInputEntry } from '../classes/activityInputEntry';
import { ModelInputEntry } from '../classes/individuals/modelInputEntry';
import { InstitutionInputEntry } from '../classes/institutions/institutionInputEntry';
import { InquiryEmailEntry } from '../classes/inquiryEmailEntry';
import { ResultsEmailEntry } from '../classes/individuals/resultsEmailEntry';
import { InstitutionResultEmailEntry } from '../classes/institutions/institutionResultEmailEntry';
import { EnvironmentSettings } from '../classes/environmentSettings';

@Injectable()

export class ActivityCaptureService {
  private ipAddressCaptureUrl = EnvironmentSettings.BaseApiUrl + '/api/activity/record';
  private calculationCaptureUrl = EnvironmentSettings.BaseApiUrl + '/api/activity/calc';
  private institutionCalcCaptureUrl = EnvironmentSettings.BaseApiUrl + '/api/activity/calc-institution';
  private emailInquiryCaptureUrl = EnvironmentSettings.BaseApiUrl + '/api/activity/email-inquiry';
  private emailResultsCaptureUrl = EnvironmentSettings.BaseApiUrl + '/api/activity/email-results';
  private emailInstitutionResultCaptureUrl = EnvironmentSettings.BaseApiUrl + '/api/activity/email-institution';
  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: Http) { }

  captureIpAddress(activityInputEntry: ActivityInputEntry): Promise<boolean> {
    return this.http.post(this.ipAddressCaptureUrl, JSON.stringify(activityInputEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean)
      .catch(this.reportError);
  }

  captureCalculationActivity(modelInputEntry: ModelInputEntry): Promise<boolean> {
    return this.http.post(this.calculationCaptureUrl, JSON.stringify(modelInputEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean)
      .catch(this.reportError);
  }

  captureInstitutionCalculationActivity(institutionInputEntry: InstitutionInputEntry): Promise<boolean> {
    return this.http.post(this.institutionCalcCaptureUrl, JSON.stringify(institutionInputEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean)
      .catch(this.reportError);
  }

  captureInquiryEmailActivity(inquiryEmailEntry: InquiryEmailEntry): Promise<boolean> {
    return this.http.post(this.emailInquiryCaptureUrl, JSON.stringify(inquiryEmailEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean)
      .catch(this.reportError);
  }

  captureResultsEmailActivity(resultsEmailEntry: ResultsEmailEntry): Promise<boolean> {
    return this.http.post(this.emailResultsCaptureUrl, JSON.stringify(resultsEmailEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean)
      .catch(this.reportError);
  }

  captureInstitutionResultEmailActivity(institutionResultEmailEntry: InstitutionResultEmailEntry): Promise<boolean> {
    return this.http.post(this.emailInstitutionResultCaptureUrl, JSON.stringify(institutionResultEmailEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean)
      .catch(this.reportError);
  }

  private reportError(error: any): Promise<any> {
    console.error('Activity Capture Service Error: ', error);
    return Promise.reject(error.message || error);
  }
}
