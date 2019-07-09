import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient, HttpRequest, HttpEventType } from '@angular/common/http';

import { EnvironmentSettings } from '../classes/environmentSettings';

@Injectable()

export class FileTransferService {
  private uploadFilesUrl = EnvironmentSettings.BaseApiUrl + '/api/file/upload';
  private downloadFileUrl = EnvironmentSettings.BaseApiUrl + '/api/file/download';

  public uploadProgress: number;
  public uploadMessage: string;
  public customerIdentifier: string;

  constructor(
    private http: Http,
    private httpClient: HttpClient
  ) { }

  uploadFiles(files: FileList, claimType: string) : Promise<boolean> {
    if (files.length === 0) {
      this.uploadMessage = "No Files Selected";
      return Promise.resolve(false);
    }

    const formData = new FormData();
    const apiUrl = `${this.uploadFilesUrl}/${this.customerIdentifier}/${claimType}`;

    for (var i = 0; i < files.length; i++)
      formData.append(files.item(i).name, files.item(i));

    const uploadFileHttpRequest = new HttpRequest('POST', apiUrl, formData, {
      reportProgress: true,
    });

    this.httpClient.request(uploadFileHttpRequest).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress)
        this.uploadProgress = Math.round(100 * event.loaded / event.total);

      else if (event.type === HttpEventType.Response) {
        var responseMessage = event.body.toString();
        if (responseMessage === "OK") {
          this.uploadMessage = "Upload Successful";
          return Promise.resolve(true);
        }
        else {
          this.uploadMessage = "Problem with File Upload";
          return Promise.resolve(false);
        }
      }
    });
  }

  // Do need to return anything here or does the browser just handle this for me?
  downloadFile(fileName: string, claimType: string): void {
    const apiUrl = `${this.downloadFileUrl}/${this.customerIdentifier}/${claimType}/${fileName}`;
    this.http.get(apiUrl);
  }
}
