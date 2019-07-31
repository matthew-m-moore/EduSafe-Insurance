import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AppRootComponent } from '../components/app-root.component';

@Component({
  selector: 'institutions',
  templateUrl: '../views/institutions.component.html',
  styleUrls: ['../styles/institutions.component.css']
})

export class InstitutionsComponent implements OnInit {

  constructor(
    private router: Router,
    private appRootComponent: AppRootComponent)
  { }

  loadIndividualsPage(): void {
    let routingUrl = ['/edusafe-home'];
    this.router.navigate(routingUrl);
    window.scroll(0, 0);
  }

  revealModelInputs(): void {
    let routingUrl = ['/institutions-model'];
    this.router.navigate(routingUrl);
    window.scroll(0, 0);
  }

  goToContactUs(): void {
    let routingUrl = ['/edusafe-contact'];
    this.router.navigate(routingUrl);
    window.scroll(0, 0);
  }

  ngOnInit(): void {
    this.appRootComponent.isInstitutional = true;
  }
}
