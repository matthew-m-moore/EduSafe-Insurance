import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { ModelOutputSummary } from '../classes/modelOutputSummary';

@Injectable()

export class ModelCalculationService {
  private webApiUrl = '//localhost:57097/api/calculate';

  constructor(private http: Http) { }

  getModelOutput(): Promise<ModelOutputSummary> {
    return this.http.get(this.webApiUrl)
      .toPromise()
      .then(response => response.json() as ModelOutputSummary)
      .catch(this.reportError);
  }

  private reportError(error: any): Promise<any> {
    console.error('Model Calculation Service Error: ', error);
    return Promise.reject(error.message || error);
  }
}
