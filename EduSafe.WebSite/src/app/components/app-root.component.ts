import { Component } from '@angular/core';

import { IpAddressCaptureService } from '../services/ipAddressCapture.service';

import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: '../views/app-root.component.html',
  styleUrls: ['../styles/app-root.component.css']
})

export class AppRootComponent {
  ipAddress: Observable<string>;

  titleText = 'Edu$afe';
  subtitleText = '[ Securing Your Future ]';

  public calculateIsClicked = false;

  constructor(
    private ipAddressCaptureService: IpAddressCaptureService
  ) { }

  revealHomePage(): void {
    this.calculateIsClicked = false;
  }

  revealModelInputs(): void {
    this.calculateIsClicked = true;
    this.ipAddress = this.ipAddressCaptureService.getIpAddress();
  }
}
