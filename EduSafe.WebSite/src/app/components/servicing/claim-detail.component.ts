import { Component, Input, OnInit } from '@angular/core';

import { ClaimStatusEntry } from '../../classes/servicing/claimStatusEntry';
import { ClaimDocumentEntry } from '../../classes/servicing/claimDocumentEntry';

import { FileTransferService } from '../../services/fileTransfer.service';

@Component({
  selector: 'claim-detail',
  templateUrl: '../../views/servicing/claim-detail.component.html',
  styleUrls: ['../../styles/servicing/claim-detail.component.css']
})

export class ClaimDetailComponent implements OnInit {
  @Input('claim') claimStatusEntry: ClaimStatusEntry;
  claimType: string;

  public isFileUploading = false;
  public isFileUploaded = false;
  public fileNameAlreadyExists = false;
  public noFilesSelected = false;

  constructor(private fileTransferService: FileTransferService) { }

  dowloadFileFromServer(claimDocumentEntry: ClaimDocumentEntry): void {
    this.fileTransferService.downloadFile(claimDocumentEntry, this.claimStatusEntry);
  }

  uploadFilesToServer(files: FileList): void {
    if (!this.isFileListOkayForUpload(files)) return;

    this.isFileUploading = true;
    this.fileTransferService.uploadFiles(files, this.claimStatusEntry)
      .then(uploadSuccess => {
        this.isFileUploaded = uploadSuccess;
        this.isFileUploading = false;
      });
  }

  isFileListOkayForUpload(files: FileList): boolean {
    var numberOfFiles = files.length;

    if (numberOfFiles === 0) {
      this.noFilesSelected = true;
      return false;
    }

    for (var i = 0; i < numberOfFiles; i++) {
      var fileNameToUpload = files[i].name;
      if (this.claimStatusEntry.ClaimDocumentEntries.some(document => document.FileName === fileNameToUpload)) {
        this.fileNameAlreadyExists = true;
        return false;
      }
    }

    return true;
  }

  ngOnInit(): void {
    this.claimType = this.claimStatusEntry.ClaimType;
  }
}
