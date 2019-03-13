import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { ArticleInformationEntry } from '../classes/articleInformationEntry';
import { EnvironmentSettings } from '../classes/environmentSettings';

@Injectable()

export class ArticleInformationService {
  private articlesUrl = EnvironmentSettings.BaseApiUrl + '/api/articles';

  constructor(private http: Http) { }

  getArticleInformationEntries(): Promise<ArticleInformationEntry[]> {
    return this.http.get(this.articlesUrl)
      .toPromise()
      .then(response => response.json() as ArticleInformationEntry[]);
  }
}
