import { Component } from '@angular/core';

import { IpAddressCaptureService } from '../services/ipAddressCapture.service';

@Component({
  selector: 'app-root',
  templateUrl: '../views/app-root.component.html',
  styleUrls: ['../styles/app-root.component.css']
})

export class AppRootComponent {

  titleText = 'Edu$afe';
  subtitleText = 'Securing Your Future';

  public calculateIsClicked = false;
  public ipAddress = "";

  constructor(
    private ipAddressCaptureService: IpAddressCaptureService
  ) { }

  revealHomePage(): void {
    this.calculateIsClicked = false;
    window.scroll(0, 0);
  }

  revealModelInputs(): void {
    this.calculateIsClicked = true;
    window.scroll(0, 0);

    this.ipAddressCaptureService.getIpAddressPromise()
      .then(ipAddressPromise => {
        this.ipAddress = ipAddressPromise;
      });
  }
}
