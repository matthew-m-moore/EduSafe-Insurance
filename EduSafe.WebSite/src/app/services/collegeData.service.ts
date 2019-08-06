import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';

import { CollegeMajorData } from '../classes/collegeMajorData';
import { InstitutionalGradData } from '../classes/institutions/institutionalGradData';
import { InstitutionInputEntry } from '../classes/institutions/institutionInputEntry';
import { EnvironmentSettings } from '../classes/environmentSettings';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()

export class CollegeDataService {
  collegesListInMemory: string[];

  private collegesSearchUrl = EnvironmentSettings.BaseApiUrl + '/api/data/search-colleges';
  private collegeMajorsSearchUrl = EnvironmentSettings.BaseApiUrl + '/api/data/search-collegeMajor';
  private collegeMajorsDataUrl = EnvironmentSettings.BaseApiUrl + '/api/data/collegeMajors';
  private institutionalDataUrl = EnvironmentSettings.BaseApiUrl + '/api/data/institutional';

  private headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: Http) { }

  searchColleges(collegeName: string): Observable<string[]> {
    return this.http
      .get(`${this.collegesSearchUrl}/?collegeName=${collegeName}`).pipe(map(res => res.json()));
  }

  searchCollegeMajors(description: string): Observable<string[]> {
    return this.http
      .get(`${this.collegeMajorsSearchUrl}/?description=${description}`).pipe(map(res => res.json()));
  }

  getCollegeMajorsData(): Promise<CollegeMajorData[]> {
    return this.http.get(this.collegeMajorsDataUrl)
      .toPromise()
      .then(response => response.json() as CollegeMajorData[]);
  }

  getInstitutionalGradData(institutionInputEntry: InstitutionInputEntry): Promise<InstitutionalGradData> {
    return this.http.put(this.institutionalDataUrl, JSON.stringify(institutionInputEntry), { headers: this.headers })
      .toPromise()
      .then(response => response.json() as InstitutionalGradData);
  }
}
