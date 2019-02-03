import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: '../views/app-root.component.html',
  styleUrls: ['../styles/app-root.component.css']
})

export class AppRootComponent {
  headerText = 'EduSafe - Securing Your Future';

  public calculateIsClicked = false;

  revealModelInputs(): void {
    this.calculateIsClicked = true;
  }
}
