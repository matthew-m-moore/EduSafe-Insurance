import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';

import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

import { RoutingModule } from '../modules/routing.module';

import { AppRootComponent } from '../components/app-root.component';
import { HomeComponent } from '../components/home.component';
import { ModelComponent } from '../components/model.component';
import { ModelOuputComponent } from '../components/output.component';
import { ContactComponent } from '../components/contact.component';

import { ModelCalculationService } from '../services/modelCalculation.service';
import { CollegeDataSearchService } from '../services/collegeDataSearch.service';
import { IpAddressCaptureService } from '../services/ipAddressCapture.service';
import { SendEmailService } from '../services/sendEmail.Service';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    TypeaheadModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ButtonsModule.forRoot(),
    RoutingModule  
  ],
  declarations: [
    AppRootComponent,
    HomeComponent,
    ModelComponent,
    ModelOuputComponent,
    ContactComponent
  ],
  providers: [
    ModelCalculationService,
    CollegeDataSearchService,
    IpAddressCaptureService,
    SendEmailService
  ],
  bootstrap: [
    AppRootComponent
  ]
})
export class AppRootModule { }
