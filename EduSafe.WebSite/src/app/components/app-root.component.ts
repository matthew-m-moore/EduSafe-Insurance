import { Component, OnInit } from '@angular/core';

import { ActivityInputEntry } from '../classes/activityInputEntry';
import { ArticleInformationEntry } from '../classes/articleInformationEntry';

import { ActivityCaptureService } from '../services/activityCapture.service';
import { ArticleInformationService } from '../services/articleInformation.service';
import { IpAddressCaptureService } from '../services/ipAddressCapture.service';

@Component({
  selector: 'app-root',
  templateUrl: '../views/app-root.component.html',
  styleUrls: ['../styles/app-root.component.css']
})

export class AppRootComponent implements OnInit {
  activityInputEntry: ActivityInputEntry;
  articlesList: ArticleInformationEntry[] = [];

  titleText = 'Edu$afe';
  subtitleText = 'Securing Your Future';

  public ipAddress = "";

  constructor(
    private activityCaptureService: ActivityCaptureService,
    private articleInformationService: ArticleInformationService,
    private ipAddressCaptureService: IpAddressCaptureService
  ) { }

  ngOnInit(): void {
    this.activityInputEntry = new ActivityInputEntry();
    this.ipAddressCaptureService.getIpAddressPromise()
      .then(ipAddressPromise => {
        this.ipAddress = ipAddressPromise;
        this.activityInputEntry.IpAddress = this.ipAddress;
        this.activityCaptureService.captureIpAddress(this.activityInputEntry);
      });

    this.articleInformationService.getArticleInformationEntries()
      .then(articleInformationEntries => {
        this.articlesList = articleInformationEntries;
      });
  }
}
