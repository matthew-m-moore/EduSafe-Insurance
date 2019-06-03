import { Component, Input, OnInit } from '@angular/core';

import { NotificationHistoryComponent } from '../components/notification-history.component'
import { PaymentHistoryComponent } from '../components/payment-history.component'

import { ServicingDataService } from '../services/servicingData.service';

@Component({
  selector: 'individual-profile',
  templateUrl: '../views/individual-profile.component.html',
  styleUrls: ['../styles/individual-profile.component.css']
})

export class IndividualProfileComponent {
}
