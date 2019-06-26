import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NotificationHistoryComponent } from '../components/notification-history.component'
import { PaymentHistoryComponent } from '../components/payment-history.component'

import { CustomerProfileEntry } from '../classes/customerProfileEntry';

import { ServicingDataService } from '../services/servicingData.service';

@Component({
  selector: 'individual-profile',
  templateUrl: '../views/individual-profile.component.html',
  styleUrls: ['../styles/individual-profile.component.css']
})

export class IndividualProfileComponent {
  customerProfileEntry: CustomerProfileEntry;
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
    this.servicingDataService.getIndividualsServicingData(this.customerNumber)
      .then(result => this.customerProfileEntry = result);
  }
}
