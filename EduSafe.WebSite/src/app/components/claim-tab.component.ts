import { Component, Input } from '@angular/core';
import { ClaimStatusEntry } from '../classes/claimStatusEntry';

@Component({
  selector: 'claim-tab',
  templateUrl: '../views/claim-tab.component.html',
  styleUrls: ['../styles/claim-tab.component.css']
})

export class ClaimTabComponent {
  @Input('claimType') claimType: string;
  @Input() active = false;
  @Input() template;
  @Input() claimStatusEntry : ClaimStatusEntry;
}
