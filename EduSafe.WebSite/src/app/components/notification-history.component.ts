import { Component, Input} from '@angular/core';

import { NotificationHistoryEntry } from '../classes/notificationHistoryEntry';

@Component({
  selector: 'notification-history',
  templateUrl: '../views/notification-history.component.html',
  styleUrls: ['../styles/notification-history.component.css']
})

export class NotificationHistoryComponent {
  @Input() notificationHistoryEntries: NotificationHistoryEntry[]
}
