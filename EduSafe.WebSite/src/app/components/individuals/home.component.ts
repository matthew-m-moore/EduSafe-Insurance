import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';

import { AppRootComponent } from '../../components/app-root.component';

@Component({
  selector: 'edusafe-home',
  templateUrl: '../../views/individuals/home.component.html',
  styleUrls: ['../../styles/individuals/home.component.css']
})

export class HomeComponent implements OnInit {

  constructor(
    private router: Router,
    private appRootComponent: AppRootComponent) { }

  loadInstitutionsPage(): void {
    let routingUrl = ['/institutions'];
    this.router.navigate(routingUrl);
    this.appRootComponent.isInstitutional = true;
    window.scroll(0, 0);
  }

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

  ngOnInit(): void {
    this.appRootComponent.isInstitutional = false;
  }
}
