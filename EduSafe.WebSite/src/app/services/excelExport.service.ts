import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';

import { EnvironmentSettings } from '../classes/environmentSettings';

@Injectable()

export class ExcelExportService {
  private excelExportUrl = EnvironmentSettings.BaseApiUrl + '/api/export';

  constructor(
    private http: Http,
    private httpClient: HttpClient
  ) { }

  // One route is the for the 'students' export one is for the 'payments' export
}
