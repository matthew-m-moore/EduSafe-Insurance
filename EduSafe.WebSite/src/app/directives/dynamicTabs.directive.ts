import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[dynamicTabs]'
})
export class DynamicTabsDirective {
  constructor(public viewContainer: ViewContainerRef) { }
}
