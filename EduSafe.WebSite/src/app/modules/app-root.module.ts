import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { RoutingModule } from '../modules/routing.module';

import { AppRootComponent } from '../components/app-root.component';
import { HomeComponent } from '../components/home.component';
import { ModelComponent } from '../components/model.component';

import { ModelCalculationService} from '../services/modelCalculationService';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
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
