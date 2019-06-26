import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import { AuthenticationPackage } from '../classes/authenticationPackage';
import { EnvironmentSettings } from '../classes/environmentSettings';

@Injectable()

export class AuthenticationService {
  private authUrl = EnvironmentSettings.BaseApiUrl + '/api/authentication';
  private idAuthUrl = EnvironmentSettings.BaseApiUrl + '/api/authentication/id';
  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: Http) { }

  authenticateUser(authenticationPackage: AuthenticationPackage): Promise<boolean> {
    return this.http.put(this.authUrl, JSON.stringify(authenticationPackage), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean);
  }

  retrieveCustomerNumbersForIdentifier(userIdentifier: string): Promise<string[]> {
    return this.http.put(this.idAuthUrl, JSON.stringify(userIdentifier), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as string[]);
  }
}
