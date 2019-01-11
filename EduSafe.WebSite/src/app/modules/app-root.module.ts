import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RoutingModule } from '../modules/routing.module';

import { AppRootComponent } from '../components/app-root.component';
import { HomeComponent } from '../components/home.component';
import { ModelComponent } from '../components/model.component';

import { ModelCalculationService} from '../services/modelCalculationService';

@NgModule({
  imports: [
    BrowserModule,
    HttpModule,
    RoutingModule
  ],
  declarations: [
    AppRootComponent,
    HomeComponent,
    ModelComponent
  ],
  providers: [
    ModelCalculationService
  ],
  bootstrap: [
    AppRootComponent
  ]
})
export class AppRootModule { }
