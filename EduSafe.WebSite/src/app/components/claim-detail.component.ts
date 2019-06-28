import { Component, Input, OnInit } from '@angular/core';

import { ClaimStatusEntry } from '../classes/claimStatusEntry';

import { FileTransferService } from '../services/fileTransfer.service';

@Component({
  selector: 'claim-detail',
  templateUrl: '../views/claim-detail.component.html',
  styleUrls: ['../styles/claim-detail.component.css']
})

export class ClaimDetailComponent implements OnInit {
  @Input('claim') claimStatusEntry: ClaimStatusEntry;
  @Input('customer') customerIdentifier: string;
  claimType: string;

  public isFileUploading = false;
  public isFileUploaded = false;

  constructor(private fileTransferService: FileTransferService) { }

  uploadFilesToServer(files: FileList): void {
    this.isFileUploading = true;
    this.fileTransferService.uploadFiles(files, this.customerIdentifier, this.claimType)
      .then(uploadSuccess => {
        this.isFileUploaded = uploadSuccess;
        this.isFileUploading = false;
      });
  }

  ngOnInit(): void {
    this.claimType = this.claimStatusEntry.ClaimType;
  }
}
