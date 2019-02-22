import { Component, OnInit } from '@angular/core';

import { ActivityInputEntry } from '../classes/activityInputEntry';

import { ActivityCaptureService } from '../services/activityCapture.service';
import { IpAddressCaptureService } from '../services/ipAddressCapture.service';

@Component({
  selector: 'app-root',
  templateUrl: '../views/app-root.component.html',
  styleUrls: ['../styles/app-root.component.css']
})

export class AppRootComponent implements OnInit {
  activityInputEntry: ActivityInputEntry;

  titleText = 'Edu$afe';
  subtitleText = 'Securing Your Future';

  public calculateIsClicked = false;
  public contactIsClicked = false;
  public ipAddress = "";

  constructor(
    private activityCaptureService: ActivityCaptureService,
    private ipAddressCaptureService: IpAddressCaptureService
  ) { }

  revealHomePage(): void {
    this.calculateIsClicked = false;
    this.contactIsClicked = false;
    window.scroll(0, 0);
  }

  revealModelInputs(): void {
    this.calculateIsClicked = true;
    this.contactIsClicked = false;
    window.scroll(0, 0);
  }

  revealContactPage(): void {
    this.contactIsClicked = true;
    this.calculateIsClicked = false;
    window.scroll(0, 0);
  }

  ngOnInit(): void {
    this.activityInputEntry = new ActivityInputEntry();
    this.ipAddressCaptureService.getIpAddressPromise()
      .then(ipAddressPromise => {
        this.ipAddress = ipAddressPromise;
        this.activityInputEntry.IpAddress = this.ipAddress;
        this.activityCaptureService.captureIpAddress(this.activityInputEntry);
      });  
  }
}
