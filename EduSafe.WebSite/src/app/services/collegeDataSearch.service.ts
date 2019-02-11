import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { CollegeMajorData } from '../classes/collegeMajorData'

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()

export class CollegeDataSearchService {
  collegesListInMemory: string[];

  private collegesUrl = '//localhost:57097/api/search/colleges';
  private collegeMajorsUrl = '//localhost:57097/api/search/collegeMajor';

  constructor(private http: Http) { }

  searchColleges(collegeName: string): Observable<string[]> {
    return this.http
      .get(`${this.collegesUrl}/?collegeName=${collegeName}`).pipe(map(res => res.json()));
  }

  searchCollegeMajors(description: string): Observable<CollegeMajorData[]> {
    return this.http
      .get(`${this.collegeMajorsUrl}/?description=${description}`).pipe(map(res => res.json()));
  }
}
