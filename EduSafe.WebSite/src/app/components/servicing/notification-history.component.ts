import { Component, Input} from '@angular/core';

import { NotificationHistoryEntry } from '../../classes/servicing/notificationHistoryEntry';

@Component({
  selector: 'notification-history',
  templateUrl: '../views/servicing/notification-history.component.html',
  styleUrls: ['../styles/servicing/notification-history.component.css']
})

export class NotificationHistoryComponent {
  @Input() notificationHistoryEntries: NotificationHistoryEntry[]
}
