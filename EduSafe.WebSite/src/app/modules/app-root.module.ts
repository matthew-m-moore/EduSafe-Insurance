import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';

import { RoutingModule } from '../modules/routing.module';

import { AppRootComponent } from '../components/app-root.component';
import { HomeComponent } from '../components/home.component';
import { ModelComponent } from '../components/model.component';

import { ModelCalculationService } from '../services/modelCalculation.service';
import { CollegeDataSearchService } from '../services/collegeDataSearch.service';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    RoutingModule
  ],
  declarations: [
    AppRootComponent,
    HomeComponent,
    ModelComponent
  ],
  providers: [
    ModelCalculationService,
    CollegeDataSearchService
  ],
  bootstrap: [
    AppRootComponent
  ]
})
export class AppRootModule { }
