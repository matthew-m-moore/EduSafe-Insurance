import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { CustomerProfileEntry } from '../classes/customerProfileEntry';
import { InstitutionProfileEntry } from '../classes/institutionProfileEntry';
import { EnvironmentSettings } from '../classes/environmentSettings';

@Injectable()

export class ServicingDataService {
  private institutionalUrl = EnvironmentSettings.BaseApiUrl + '/api/servicing/institution';
  private individualsUrl = EnvironmentSettings.BaseApiUrl + '/api/servicing/individual';

  constructor(private http: Http) { }

  getInstituionalServicingData(customerIdentifier: string): Promise<InstitutionProfileEntry> {
    const apiUrl = `${this.institutionalUrl}/${customerIdentifier}`;
    return this.http.get(apiUrl)
      .toPromise()
      .then(response => response.json() as InstitutionProfileEntry);
  }

  getIndividualsServicingData(customerIdentifier: string): Promise<CustomerProfileEntry> {
    const apiUrl = `${this.individualsUrl}/${customerIdentifier}`;
    return this.http.get(apiUrl)
      .toPromise()
      .then(response => response.json() as CustomerProfileEntry);
  }
}
