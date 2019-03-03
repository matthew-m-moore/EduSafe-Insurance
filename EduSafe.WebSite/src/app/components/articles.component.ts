import { Component, OnInit } from '@angular/core';
import { trigger, transition, animate, style, query, stagger } from '@angular/animations';

import { AppRootComponent } from '../components/app-root.component';

import { ArticleInformationEntry } from '../classes/articleInformationEntry';

import { ArticleInformationService } from '../services/articleInformation.service';

@Component({
  selector: 'edusafe-news',
  templateUrl: '../views/articles.component.html',
  styleUrls: ['../styles/articles.component.css'],
  animations: [
    trigger('listStagger', [
      transition('* <=> *', [
        query(':enter', [
          style({ opacity: 0, transform: 'translateY(-100%)' }),
          stagger(-30, [
            animate('500ms cubic-bezier(0.35, 0, 0.25, 1)',
              style({ opacity: 1, transform: 'none' }))
            ]),
          ],
        { optional: true })
      ])
    ])
  ]
})

export class ArticlesComponent implements OnInit {
  articlesList: ArticleInformationEntry[] = [];

  constructor(
    private appRootComponent: AppRootComponent,
    private articleInformationService: ArticleInformationService
  ) { }

  ngOnInit(): void {
    this.appRootComponent.isFirstLandingOnPage = false;

    if (this.appRootComponent.articlesList.length > 0)
      this.articlesList = this.appRootComponent.articlesList;
    else {
      this.articleInformationService.getArticleInformationEntries()
        .then(articleInformationEntries => {
          this.articlesList = articleInformationEntries;
        });
    }
  }
}
