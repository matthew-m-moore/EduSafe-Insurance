import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'edusafe-home',
  templateUrl: '../views/home.component.html',
  styleUrls: ['../styles/home.component.css']
})

export class HomeComponent {

  constructor(private router: Router) { }

  revealModelInputs(): void {
    let routingUrl = ['/edusafe-model'];
    this.router.navigate(routingUrl);
    window.scroll(0, 0);
  }

  goToContactUs(): void {
    let routingUrl = ['/edusafe-contact'];
    this.router.navigate(routingUrl);
    window.scroll(0, 0);
  }
}
