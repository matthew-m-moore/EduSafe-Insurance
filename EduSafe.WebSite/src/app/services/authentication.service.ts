import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { EnvironmentSettings } from '../classes/environmentSettings';

@Injectable()

export class AuthenticationService {
  private idAuthUrl = EnvironmentSettings.BaseApiUrl + '/api/authentication/id';
  private emailAuthUrl = EnvironmentSettings.BaseApiUrl + '/api/authentication/email';

  constructor(private http: Http) { }

  // This will need to return the customer's unique identifer and cache it during the log in session
  // Eventually, there would need to be an inactivity timer to log out
}
