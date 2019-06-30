import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import { AuthenticationPackage } from '../classes/authenticationPackage';
import { EnvironmentSettings } from '../classes/environmentSettings';

@Injectable()

export class AuthenticationService {
  private authUrl = EnvironmentSettings.BaseApiUrl + '/api/authentication/login';
  private idAuthUrl = EnvironmentSettings.BaseApiUrl + '/api/authentication/user';
  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: Http) { }

  authenticateUser(authenticationPackage: AuthenticationPackage): Promise<boolean> {
    return this.http.put(this.authUrl, JSON.stringify(authenticationPackage), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as boolean);
  }

  retrieveCustomerNumbersForIdentifier(authenticationPackage: AuthenticationPackage): Promise<string[]> {
    return this.http.post(this.idAuthUrl, JSON.stringify(authenticationPackage), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as string[]);
  }
}
