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

import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { ModelCalculationService } from '../services/modelCalculation.service';
import { CollegeDataService } from '../services/collegeData.service';
import { CollegeDataSearchService } from '../services/collegeDataSearch.service';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    RoutingModule,
    HttpClientInMemoryWebApiModule.forRoot(
      CollegeDataService,
      {
        dataEncapsulation: false,
        passThruUnknownUrl: true
      })
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
