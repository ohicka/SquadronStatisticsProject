import { HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { RefreshDataService } from '../_services/refreshData.service';
import { FilesService } from '../_services/files.service';
import { FileUploadResponse } from '../_models/fileUploadResponse';

@Component({
  selector: 'app-fileUploader',
  templateUrl: './fileUploader.component.html',
  styleUrls: ['./fileUploader.component.css']
})
export class FileUploaderComponent implements OnInit {
  fileName = '';
  fileId = 0;
  resultMessage: string;

  uploadResponse: FileUploadResponse;

  isUploading = false;

  @Input()
    requiredFileType:string;

    //@Output() onUploadCompleted = new EventEmitter<boolean>();

  constructor(
    private filesServive: FilesService,
    private refreshDataService: RefreshDataService
  ) { }

  ngOnInit() {
  }

  onFileSelected(event) {

    const file:File = event.target.files[0];
    this.fileName = file.name;

    if (file) {

      const formData = new FormData();
      formData.append("file", file, this.fileName);

      this.isUploading = true;
      this.filesServive.uploadFile(formData).subscribe(response => {
          this.uploadResponse = <FileUploadResponse>response;
          this.refreshDataService.announceNewFileId(this.uploadResponse.fileId);
      }, error => {
        console.log(error);
        this.reset();
        this.resultMessage = "Error appeared during file uploading."
      }, () => {
        this.reset();
        this.resultMessage = "File was uploaded successfully. File Id: " + this.uploadResponse.fileId.toString() 
                              + " Number of valid rows: " + this.uploadResponse.validRowsNumber.toString();
      });
    }
  }
  
  reset() {
    this.isUploading = false;
  }

}
