import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';

import { EnvironmentSettings } from '../classes/environmentSettings';
import { InstitutionProfileEntry } from '../classes/institutionProfileEntry';
import { PaymentHistoryEntry } from '../classes/paymentHistoryEntry';

@Injectable()

export class ExcelExportService {
  private studentsExcelExportUrl = EnvironmentSettings.BaseApiUrl + 'api/export/students';
  private paymentsExcelExportUrl = EnvironmentSettings.BaseApiUrl + 'api/export/payments';
  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(
    private http: Http,
    private httpClient: HttpClient
  ) { }

  getStudentsExport(institutionProfileEntry: InstitutionProfileEntry) {
    this.http.post(this.studentsExcelExportUrl, JSON.stringify(institutionProfileEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.blob())
      .then(blob => URL.createObjectURL(blob))
      .then(url => {
        var link = document.createElement("a");
        link.setAttribute("href", url);
        link.setAttribute("download", document.title);
        link.style.display = "none";
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
      });
  }

  getPaymentsExport(paymentHistoryEntries: PaymentHistoryEntry[]) {
    this.http.post(this.paymentsExcelExportUrl, JSON.stringify(paymentHistoryEntries), { headers: this.headers })
      .toPromise()
      .then(response => response.blob())
      .then(blob => URL.createObjectURL(blob))
      .then(url => {
        var link = document.createElement("a");
        link.setAttribute("href", url);
        link.setAttribute("download", document.title);
        link.style.display = "none";
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
      });
  }
}
