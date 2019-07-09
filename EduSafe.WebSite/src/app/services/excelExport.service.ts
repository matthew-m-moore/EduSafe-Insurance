import { Injectable } from '@angular/core';
import { Http, Headers, ResponseContentType } from '@angular/http';

import { EnvironmentSettings } from '../classes/environmentSettings';
import { InstitutionProfileEntry } from '../classes/institutionProfileEntry';
import { CustomerProfileEntry } from '../classes/customerProfileEntry';

@Injectable()

export class ExcelExportService {
  private studentsExcelExportUrl = EnvironmentSettings.BaseApiUrl + '/api/export/students';
  private paymentsInstitutionExcelExportUrl = EnvironmentSettings.BaseApiUrl + '/api/export/payments-institution';
  private paymentsIndividualExcelExportUrl = EnvironmentSettings.BaseApiUrl + '/api/export/payments-individual';
  private headers = new Headers({ 'Content-Type': 'application/json' });

  private defaultStudentReportName = 'Student-Information-Report.xlsx';

  constructor(private http: Http) { }

  getStudentsExport(institutionProfileEntry: InstitutionProfileEntry) {
    this.http.post(this.studentsExcelExportUrl, JSON.stringify(institutionProfileEntry),
      { headers: this.headers, responseType: ResponseContentType.Blob })
      .toPromise()
      .then(response => {
        var blob = response.blob();
        var filename = response.headers.get('filename');
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = filename ? filename : this.defaultStudentReportName;
        link.click();
      });
  }

  getInstitutionPaymentsExport(institutionProfileEntry: InstitutionProfileEntry) {
    this.http.post(this.paymentsInstitutionExcelExportUrl, JSON.stringify(institutionProfileEntry),
      { headers: this.headers, responseType: ResponseContentType.Blob })
      .toPromise()
      .then(response => {
        var blob = response.blob();
        var filename = response.headers.get('filename');
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = filename ? filename : this.defaultStudentReportName;
        link.click();
      });
  }

  getIndividualPaymentsExport(customerProfileEntry: CustomerProfileEntry) {
    this.http.post(this.paymentsIndividualExcelExportUrl, JSON.stringify(customerProfileEntry),
      { headers: this.headers, responseType: ResponseContentType.Blob })
      .toPromise()
      .then(response => {
        var blob = response.blob();
        var filename = response.headers.get('filename');
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = filename ? filename : this.defaultStudentReportName;
        link.click();
      });
  }
}
