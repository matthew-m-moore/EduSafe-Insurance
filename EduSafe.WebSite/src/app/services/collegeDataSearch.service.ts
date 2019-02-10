import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

@Injectable()

export class CollegeDataSearchService {
  collegesListInMemory: string[];

  private collegesUrl = 'api/collegesList';
  private collegeMajorsUrl = 'api/collegeMajors';

  constructor(private httpClient: HttpClient) { }

  searchColleges(searchText: string): Observable<string[]> {
    return this.httpClient
      .get<string[]>(`${this.collegesUrl}/?collegeName=${searchText}`).pipe();
  }

  searchCollegeMajors(searchText: string): Observable<string[]> {
    return this.httpClient
      .get<string[]>(`${this.collegeMajorsUrl}/?description=${searchText}`).pipe();
  }
}
