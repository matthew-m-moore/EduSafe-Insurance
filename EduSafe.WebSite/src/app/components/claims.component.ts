import { Component, OnInit, AfterContentInit, ViewChild, ComponentFactoryResolver} from '@angular/core';

import { IndividualProfileComponent } from './individual-profile.component';
import { ClaimTabComponent } from './claim-tab.component';

import { ClaimStatusEntry } from '../classes/claimStatusEntry';
import { ClaimPaymentEntry } from '../classes/claimPaymentEntry';

import { DynamicTabsDirective } from '../directives/dynamicTabs.directive';

import { FileTransferService } from '../services/fileTransfer.service';

@Component({
  selector: 'claims-inventory',
  templateUrl: '../views/claims.component.html',
  styleUrls: ['../styles/claims.component.css']
})

export class ClaimsComponent implements OnInit, AfterContentInit {
  claimStatusEntries: ClaimStatusEntry[];
  claimPaymentEntries: ClaimPaymentEntry[];
  claimDynamicTabs: ClaimTabComponent[] = [];

  public isFileUploading = false;
  public isFileUploaded = false;

  @ViewChild(DynamicTabsDirective) dynamicTabPlaceholder: DynamicTabsDirective;
  @ViewChild('claimEntry') claimEntryTemplate;

  constructor(
    private individualProfileComponent: IndividualProfileComponent,
    private componentFactoryResolver: ComponentFactoryResolver,
    private fileTransferService: FileTransferService)
  { }

  onClaimExists(claim: ClaimStatusEntry) {
    this.createTab(
      claim.ClaimType,
      this.claimEntryTemplate,
      claim
    );
  }

  createTab(claimType: string, template, claim) {
    const componentFactory =
      this.componentFactoryResolver.resolveComponentFactory(ClaimTabComponent);

    const viewContainerReference = this.dynamicTabPlaceholder.viewContainer;
    const componentReference = viewContainerReference.createComponent(componentFactory);

    const tabInstance: ClaimTabComponent = componentReference.instance as ClaimTabComponent;
    tabInstance.claimType = claimType;
    tabInstance.template = template;
    tabInstance.claimStatusEntry = claim;

    this.claimDynamicTabs.push(componentReference.instance as ClaimTabComponent);
  }

  selectTab(claimTab: ClaimTabComponent) {
    this.claimDynamicTabs.forEach(tab => (tab.active = false));
    claimTab.active = true;
  }

  ngAfterContentInit(): void {
    if (this.claimDynamicTabs.length > 0)
      this.selectTab(this.claimDynamicTabs[0]);
  }

  ngOnInit(): void {
    this.claimStatusEntries =
      this.individualProfileComponent.customerProfileEntry.ClaimStatusEntries;
    this.claimPaymentEntries =
      this.individualProfileComponent.customerProfileEntry.ClaimPaymentEntries;
    this.fileTransferService.customerIdentifier =
      this.individualProfileComponent.customerProfileEntry.CustomerUniqueId;

    this.claimStatusEntries.forEach(claimEntry => this.onClaimExists(claimEntry));
  }
}
