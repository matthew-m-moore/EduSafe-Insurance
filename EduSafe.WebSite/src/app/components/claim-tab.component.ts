import { Component, Input } from '@angular/core';

@Component({
  selector: 'claim-tab',
  templateUrl: '../views/claim-tab.component.html',
  styleUrls: ['../styles/claim-tab.component.css']
})

export class ClaimTabComponent {
  @Input('claimType') claimType: string;
  @Input('customer') customerIdentifier: string;
  @Input() active = false;
  @Input() template;
  @Input() dataContext;
}
