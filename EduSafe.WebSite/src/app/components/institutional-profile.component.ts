import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NotificationHistoryComponent } from '../components/notification-history.component'
import { PaymentHistoryComponent } from '../components/payment-history.component'
import { IndividualProfileComponent } from '../components/individual-profile.component'

import { InstitutionProfileEntry } from '../classes/institutionProfileEntry';

import { ServicingDataService } from '../services/servicingData.service';

@Component({
  selector: 'institutional-profile',
  templateUrl: '../views/institutional-profile.component.html',
  styleUrls: ['../styles/institutional-profile.component.css']
})

export class InsitutionalProfileComponent implements OnInit {
  institutionProfileEntry: InstitutionProfileEntry;
  customerNumber: string;

  constructor(
    private servicingDataService: ServicingDataService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
      this.activatedRoute.queryParams
        .subscribe((params) => {
          this.customerNumber = params.customerNumber;
        });
  }

  ngOnInit(): void {
    this.servicingDataService.getInstituionalServicingData(this.customerNumber)
      .then(result => this.institutionProfileEntry = result);
  }
}
