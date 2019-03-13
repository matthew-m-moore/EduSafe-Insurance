import { Directive, Input, Renderer, ElementRef, OnInit, OnDestroy, AfterViewInit } from '@angular/core';
import { ScrollInformationService } from '../services/scrollInformation.service';
import { Subscription } from 'rxjs';

@Directive({
  selector: '[animateOnScroll]'
})

export class AnimateOnScrollDirective implements OnInit, OnDestroy, AfterViewInit {

  private offsetTop: number;
  private isVisible: boolean;
  private windowHeight: number;
  private scrollSubscription: Subscription = new Subscription();
  private resizeSubscription: Subscription = new Subscription();

  @Input() animationName: string;
  @Input() offset: number = 1;

  constructor(
    private scrollInformationService: ScrollInformationService,
    private elementRef: ElementRef,
    private renderer: Renderer,   
  ) { }

  ngOnInit(): void {
    if (!this.animationName) {
      throw new Error('animationName required');
    }

    this.isVisible = false;

    this.scrollSubscription = this.scrollInformationService.scrollObservable
      .subscribe(() => this.manageVisibility());

    this.resizeSubscription = this.scrollInformationService.resizeObsservable
      .subscribe(() => this.manageVisibility());
  }

  ngAfterViewInit(): void {
    setTimeout(() => this.manageVisibility(), 1);
  }

  ngOnDestroy(): void {
    this.scrollSubscription.unsubscribe();
    this.resizeSubscription.unsubscribe();
  }

  private manageVisibility(): void {
    if (this.isVisible) {
      return;
    }

    this.getWinHeight();
    this.getOffsetTop();

    const scrollTrigger = this.offsetTop + this.offset - this.windowHeight;

    if (this.scrollInformationService.position >= scrollTrigger) {
      this.addAnimationClass();
    }
  }

  private addAnimationClass(): void {
    this.isVisible = true;
    this.setClass(this.animationName);
  }

  private setClass(classes: string): void {
    for (const c of classes.split(' ')) {
      this.renderer.setElementClass(this.elementRef.nativeElement, c, true);
    }
  }

  private getWinHeight(): void {
    this.windowHeight = window.innerHeight;
  }

  private getOffsetTop(): void {

    const viewportTop = this.elementRef.nativeElement.getBoundingClientRect().top;
    const clientTop = this.elementRef.nativeElement.clientTop;
    this.offsetTop = viewportTop + this.scrollInformationService.position - clientTop;
  }
}
