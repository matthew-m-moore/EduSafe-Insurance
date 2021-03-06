import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import { CustomerEmailEntry } from '../classes/servicing/customerEmailEntry';
import { CustomerProfileEntry } from '../classes/servicing/customerProfileEntry';
import { InstitutionProfileEntry } from '../classes/servicing/institutionProfileEntry';
import { EnvironmentSettings } from '../classes/environmentSettings';

@Injectable()

export class ServicingDataService {
  private institutionalUrl = EnvironmentSettings.BaseApiUrl + '/api/servicing/institution';
  private individualsUrl = EnvironmentSettings.BaseApiUrl + '/api/servicing/individual';
  private emailUrl = EnvironmentSettings.BaseApiUrl + '/api/servicing/email';
  private headers = new Headers({ 'Content-Type': 'application/json' });

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

  makeEmailAddressPrimary(customerEmailEntry: CustomerEmailEntry): Promise<boolean> {
    const apiUrl = `${this.emailUrl}/make-primary`;
    return this.http.put(apiUrl, JSON.stringify(customerEmailEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean);
  }

  removeEmailAddress(customerEmailEntry: CustomerEmailEntry): Promise<boolean> {
    const apiUrl = `${this.emailUrl}/remove`;
    return this.http.put(apiUrl, JSON.stringify(customerEmailEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean);
  }

  addNewEmailAddress(customerEmailEntry: CustomerEmailEntry): Promise<number> {
    const apiUrl = `${this.emailUrl}/add`;
    return this.http.post(apiUrl, JSON.stringify(customerEmailEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as number);
  }
}
