import { Component, Input, OnInit } from '@angular/core';

import { FileTransferService } from '../services/fileTransfer.service';

@Component({
  selector: 'claims-detail',
  templateUrl: '../views/claims.component.html',
  styleUrls: ['../styles/claims.component.css']
})

export class ClaimsComponent {
  public isFileUploading = false;
  public isFileUploaded = false;

  // This needs to be selected on the screen
  public claimType = '';

  // This needs to be set up on initialization of the class
  public customerIdentifier = '';

  constructor(
    private fileTransferService: FileTransferService,
  ) { }

  uploadFilesToServer(files: FileList) : void
  {
    this.isFileUploading = true;
    this.fileTransferService.uploadFiles(files, this.customerIdentifier, this.claimType)
      .then(uploadSuccess => {
        this.isFileUploaded = uploadSuccess;
        this.isFileUploading = false;
      });
  }
}
