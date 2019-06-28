import { Component, Input, OnInit } from '@angular/core';

import { NotificationHistoryEntry } from '../classes/notificationHistoryEntry';

@Component({
  selector: 'notification-history',
  templateUrl: '../views/notification-history.component.html',
  styleUrls: ['../styles/notification-history.component.css']
})

export class NotificationHistoryComponent {
  public notificationHistoryEntries: NotificationHistoryEntry[]

  constructor(notificationHistoryEntries: NotificationHistoryEntry[]) {
      this.notificationHistoryEntries = notificationHistoryEntries;
  }

  checkNotificationHistory(): boolean {
    if (this.notificationHistoryEntries)
      return this.notificationHistoryEntries.length > 0

    return false;
  }
}
