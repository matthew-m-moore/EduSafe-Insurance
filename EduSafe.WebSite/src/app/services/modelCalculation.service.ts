import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import { ModelInputEntry } from '../classes/individuals/modelInputEntry';
import { ModelOutputSummary } from '../classes/individuals/modelOutputSummary';
import { InstitutionInputEntry } from '../classes/institutions/institutionInputEntry';
import { InstitutionOutputSummary } from '../classes/institutions/institutionOutputSummary';
import { EnvironmentSettings } from '../classes/environmentSettings';

@Injectable()

export class ModelCalculationService {
  private modelCalculationUrl = EnvironmentSettings.BaseApiUrl + '/api/calculate/premiums';
  private institutionCalculationUrl = EnvironmentSettings.BaseApiUrl + '/api/calculate/institutional-premiums';
  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: Http) { }

  calcModelOutput(modelInputEntry: ModelInputEntry): Promise<ModelOutputSummary> {
    return this.http.put(this.modelCalculationUrl, JSON.stringify(modelInputEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as ModelOutputSummary)
      .catch(this.reportError);
  } 

  calcInstitutionOutput(institutionInputEntry: InstitutionInputEntry): Promise<InstitutionOutputSummary> {
    return this.http.put(this.institutionCalculationUrl, JSON.stringify(institutionInputEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as InstitutionOutputSummary)
      .catch(this.reportError);
  }

  private reportError(error: any): Promise<any> {
    console.error('Model Calculation Service Error: ', error);
    return Promise.reject(error.message || error);
  }
}
