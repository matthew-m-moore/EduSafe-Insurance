import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';

import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar'
import { CollapseModule } from 'ngx-bootstrap/collapse';

import { AppRoutingModule } from '../modules/routing.module';

import { AppRootComponent } from '../components/app-root.component';
import { HomeComponent } from '../components/home.component';
import { ModelComponent } from '../components/model.component';
import { ModelOuputComponent } from '../components/output.component';
import { ContactComponent } from '../components/contact.component';
import { ArticlesComponent } from '../components/articles.component';
import { AuthenticationComponent } from '../components/authentication.component';
import { IndividualProfileComponent } from '../components/individual-profile.component';
import { InstitutionalProfileComponent } from '../components/institutional-profile.component';
import { PaymentHistoryComponent } from '../components/payment-history.component';
import { NotificationHistoryComponent } from '../components/notification-history.component';
import { ClaimsComponent } from '../components/claims.component';
import { ClaimTabComponent } from '../components/claim-tab.component';
import { ClaimDetailComponent } from '../components/claim-detail.component';

import { AnimateOnScrollDirective } from '../directives/animateOnScroll.directive';
import { DynamicTabsDirective } from '../directives/dynamicTabs.directive';

import { ActivityCaptureService } from '../services/activityCapture.service';
import { ModelCalculationService } from '../services/modelCalculation.service';
import { CollegeDataSearchService } from '../services/collegeDataSearch.service';
import { ArticleInformationService } from '../services/articleInformation.service';
import { IpAddressCaptureService } from '../services/ipAddressCapture.service';
import { ScrollInformationService } from '../services/scrollInformation.service';
import { SendEmailService } from '../services/sendEmail.Service';
import { AuthenticationService } from '../services/authentication.service';
import { ExcelExportService } from '../services/excelExport.service';
import { FileTransferService } from '../services/fileTransfer.service';
import { ServicingDataService } from '../services/servicingData.service';
import { ClaimStatusEntry } from '../classes/claimStatusEntry';

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    CommonModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    TypeaheadModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ButtonsModule.forRoot(),
    ModalModule.forRoot(),
    ProgressbarModule.forRoot(),
    CollapseModule.forRoot(),
    AppRoutingModule  
  ],
  declarations: [
    AppRootComponent,
    HomeComponent,
    ModelComponent,
    ModelOuputComponent,
    ContactComponent,
    ArticlesComponent,
    AuthenticationComponent,
    IndividualProfileComponent,
    InstitutionalProfileComponent,
    PaymentHistoryComponent,
    NotificationHistoryComponent,
    ClaimsComponent,
    ClaimTabComponent,
    ClaimDetailComponent,
    AnimateOnScrollDirective,
    DynamicTabsDirective,
  ],
  entryComponents: [
    ClaimTabComponent,
  ],
  providers: [
    ActivityCaptureService,
    ModelCalculationService,
    CollegeDataSearchService,
    ArticleInformationService,
    IpAddressCaptureService,
    ScrollInformationService,
    SendEmailService,
    AuthenticationService,
    ExcelExportService,
    FileTransferService,
    ServicingDataService,
  ],
  bootstrap: [
    AppRootComponent
  ]
})
export class AppRootModule { }
