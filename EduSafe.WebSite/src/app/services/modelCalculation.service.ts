import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import { ModelInputEntry } from '../classes/modelInputEntry';
import { ModelOutputSummary } from '../classes/modelOutputSummary';
import { Environment } from '../classes/environment';

@Injectable()

export class ModelCalculationService {
  private webApiUrlGet = Environment.BaseApiUrl + '/api/calculate';
  private webApiUrlPut = Environment.BaseApiUrl + '/api/calculate/premiums';
  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: Http) { }

  getModelOutput(): Promise<ModelOutputSummary> {
    return this.http.get(this.webApiUrlGet)
      .toPromise()
      .then(response => response.json() as ModelOutputSummary)
      .catch(this.reportError);
  }

  calcModelOutput(modelInputEntry: ModelInputEntry): Promise<ModelOutputSummary> {
    return this.http.put(this.webApiUrlPut, JSON.stringify(modelInputEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as ModelOutputSummary)
      .catch(this.reportError);
  } 

  private reportError(error: any): Promise<any> {
    console.error('Model Calculation Service Error: ', error);
    return Promise.reject(error.message || error);
  }
}
