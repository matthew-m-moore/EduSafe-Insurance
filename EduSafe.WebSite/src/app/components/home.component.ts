import { Component } from '@angular/core';
import { AppRootComponent } from '../components/app-root.component';

@Component({
  selector: 'edusafe-home',
  templateUrl: '../views/home.component.html',
  styleUrls: ['../styles/home.component.css']
})

export class HomeComponent {

  constructor(private appRootComponent: AppRootComponent) { }

  revealModelInputs(): void {
    this.appRootComponent.calculateIsClicked = true;
    this.appRootComponent.contactIsClicked = false;
    window.scroll(0, 0);
  }
}
