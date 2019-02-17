import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { Environment } from '../classes/environment';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()

export class IpAddressCaptureService {

  private ipAddressCaptureUrl = Environment.IpAddressCaptureUrl + '?format=jsonp&callback=JSONP_CALLBACK';

  constructor(private http: Http) { }

  getIpAddress(): Observable<string> {
    return this.http.get(this.ipAddressCaptureUrl).pipe(
      map(res => {
        let ipVar = res.text();
        let num = ipVar.indexOf(":");
        let num2 = ipVar.indexOf("\"});");
        ipVar = ipVar.slice(num + 2, num2);
        console.log('ipVar -- ', ipVar);
        return ipVar;
      }));
  }
}
