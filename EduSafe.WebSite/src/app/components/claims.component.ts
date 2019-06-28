import {
  Component,
  ContentChildren,
  QueryList,
  AfterContentInit,
  ViewChild,
  ComponentFactoryResolver,
  ViewContainerRef,
  OnInit} from '@angular/core';

import { IndividualProfileComponent } from './individual-profile.component';
import { ClaimTabComponent } from './claim-tab.component';

import { ClaimStatusEntry } from '../classes/claimStatusEntry';
import { ClaimPaymentEntry } from '../classes/claimPaymentEntry';

import { DynamicTabsDirective } from '../directives/dynamicTabs.directive';

@Component({
  selector: 'claims-inventory',
  templateUrl: '../views/claims.component.html',
  styleUrls: ['../styles/claims.component.css']
})

export class ClaimsComponent implements OnInit, AfterContentInit {
  claimStatusEntries: ClaimStatusEntry[];
  claimPaymentEntries: ClaimPaymentEntry[];
  claimDynamicTabs: ClaimTabComponent[] = [];
  customerIdentifier: string;

  public isFileUploading = false;
  public isFileUploaded = false;

  // @ContentChildren(ClaimTabComponent) claimTabs: QueryList<ClaimTabComponent>;
  @ViewChild(DynamicTabsDirective) dynamicTabPlaceholder: DynamicTabsDirective;
  @ViewChild('claimEntry') claimEntryTemplate;

  constructor(
    private individualProfileComponent: IndividualProfileComponent,
    private componentFactoryResolver: ComponentFactoryResolver) { }

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
    tabInstance.customerIdentifier = this.customerIdentifier;
    tabInstance.template = template;
    tabInstance.dataContext = claim;

    this.claimDynamicTabs.push(componentReference.instance as ClaimTabComponent);
    // this.selectTab(this.claimDynamicTabs[this.claimDynamicTabs.length - 1]);
  }

  selectTab(claimTab: ClaimTabComponent) {
    // this.claimTabs.toArray().forEach(tab => (tab.active = false));
    this.claimDynamicTabs.forEach(tab => (tab.active = false));

    claimTab.active = true;
  }

  ngAfterContentInit() {
    if (this.claimDynamicTabs.length > 0)
      this.selectTab(this.claimDynamicTabs[0]);
  }

  ngOnInit(): void {
    this.claimStatusEntries =
      this.individualProfileComponent.customerProfileEntry.ClaimStatusEntries;
    this.claimPaymentEntries =
      this.individualProfileComponent.customerProfileEntry.ClaimPaymentEntries;
    this.customerIdentifier =
      this.individualProfileComponent.customerProfileEntry.CustomerUniqueId;

    this.claimStatusEntries.forEach(claimEntry => this.onClaimExists(claimEntry));
  }
}
