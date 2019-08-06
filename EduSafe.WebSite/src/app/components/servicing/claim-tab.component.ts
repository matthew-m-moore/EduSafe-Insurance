import { Component, Input } from '@angular/core';
import { ClaimStatusEntry } from '../../classes/servicing/claimStatusEntry';

@Component({
  selector: 'claim-tab',
  templateUrl: '../views/servicing/claim-tab.component.html',
  styleUrls: ['../styles/servicing/claim-tab.component.css']
})

export class ClaimTabComponent {
  @Input('claimType') claimType: string;
  @Input() active = false;
  @Input() template;
  @Input() claimStatusEntry : ClaimStatusEntry;
}
