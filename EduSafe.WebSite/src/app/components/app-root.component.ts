import { Component, OnInit } from '@angular/core';

import { IpAddressCaptureService } from '../services/ipAddressCapture.service';

@Component({
  selector: 'app-root',
  templateUrl: '../views/app-root.component.html',
  styleUrls: ['../styles/app-root.component.css']
})

export class AppRootComponent implements OnInit {
  titleText = 'Edu$afe';
  subtitleText = 'Securing Your Future';

  public calculateIsClicked = false;
  public contactIsClicked = false;
  public ipAddress = "";

  constructor(
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
    this.ipAddressCaptureService.getIpAddressPromise()
      .then(ipAddressPromise => {
        this.ipAddress = ipAddressPromise;
      });
  }
}
