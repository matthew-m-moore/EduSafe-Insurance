import { Component, Input, OnInit } from '@angular/core';

import { NotificationHistoryComponent } from '../components/notification-history.component'
import { PaymentHistoryComponent } from '../components/payment-history.component'
import { IndividualProfileComponent } from '../components/individual-profile.component'

import { ServicingDataService } from '../services/servicingData.service';

@Component({
  selector: 'institutional-profile',
  templateUrl: '../views/institutional-profile.component.html',
  styleUrls: ['../styles/institutional-profile.component.css']
})

export class InsitutionalProfileComponent {
}
