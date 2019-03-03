import { Injectable, OnDestroy } from '@angular/core';
import { Observable, Subscription, fromEvent } from 'rxjs';

@Injectable()
export class ScrollInformationService implements OnDestroy {
  scrollObservable: Observable<any>;
  resizeObsservable: Observable<any>;
  position: number;

  private scrollSubscription: Subscription = new Subscription();
  private resizeSubscription: Subscription = new Subscription();

  constructor() {
    this.manageScrollPosition();
    this.scrollObservable = fromEvent(window, 'scroll');

    this.scrollSubscription = this.scrollObservable
      .subscribe(() => this.manageScrollPosition());

    this.resizeObsservable = fromEvent(window, 'resize');

    this.resizeSubscription = this.resizeObsservable
      .subscribe(() => this.manageScrollPosition());
  }

  private manageScrollPosition(): void {
    this.position = window.pageYOffset;
  }

  ngOnDestroy(): void {
    this.scrollSubscription.unsubscribe();
    this.resizeSubscription.unsubscribe();
  }
}
