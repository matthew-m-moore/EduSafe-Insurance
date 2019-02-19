import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { EnvironmentSettings } from '../classes/environmentSettings';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()

export class IpAddressCaptureService {

  private ipAddressCaptureUrl = EnvironmentSettings.IpAddressCaptureUrl + '?format=jsonp&callback=JSONP_CALLBACK';

  constructor(private http: Http) { }

  getIpAddress(): Observable<string> {
    return this.http.get(this.ipAddressCaptureUrl).pipe(
      map(res => {
        let responseText = res.text();
        let leftBoundary = responseText.indexOf(":");
        let rightBoundary = responseText.indexOf("\"});");
        var ipAddress = responseText.slice(leftBoundary + 2, rightBoundary);
        return ipAddress;
      }));
  }

  getIpAddressPromise(): Promise<string> {
    return this.http.get(this.ipAddressCaptureUrl)
      .toPromise()
      .then(res => {
        let responseText = res.text();
        let leftBoundary = responseText.indexOf(":");
        let rightBoundary = responseText.indexOf("\"});");
        var ipAddress = responseText.slice(leftBoundary + 2, rightBoundary);
        return ipAddress;
      });
  }
}
