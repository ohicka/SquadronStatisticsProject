import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { map} from 'rxjs/operators';
import { RowData } from '../_models/rowData';
import { FileInfo } from '../_models/fileInfo';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilesService {

paginatedFilesResult: PaginatedResult<File[]> = new PaginatedResult<File[]>();

baseUrl = environment.apiUrl + 'files/';
constructor(private http: HttpClient) { }


getUploadedFiles(pageNumber?: number, itemsPerPage?: number) {
  let queryParams = new HttpParams();
  let url = this.baseUrl + 'getUploadedFilesInfo';
  if (pageNumber !== null && itemsPerPage !== null)
  {
    // queryParams.append('pageNumber', pageNumber.toString());
    // queryParams.append('pageSize', itemsPerPage.toString());
    url += '?pageNumber=' + pageNumber.toString() + '&pageSize=' + itemsPerPage.toString();
  }

  return this.http.get<File[]>(url, {observe: 'response'}).pipe(
    map(response => {
      this.paginatedFilesResult.result = response.body;
      let paginationHeader = response.headers.get('Pagination');
      if (paginationHeader !== null) {
        this.paginatedFilesResult.pagination = JSON.parse(paginationHeader);
      }
      return this.paginatedFilesResult;
    })
  )
}

uploadFile(formData: FormData) {

  return this.http.post(this.baseUrl + 'uploadFile', formData);

}

// used to get the last uploaded file from db when loading data on page init
getLastUploadedFileFromDb() {
  return this.http.get<FileInfo>(this.baseUrl + 'getLastUploadedFileFromDb');
}

getFileRows(fielId: number) {

  return this.http.get<RowData[]>(this.baseUrl + 'getFileRows?fileId=' + fielId.toString());

}


checkExistingFilesInDb() {

  return this.http.get<boolean>(this.baseUrl + 'checkExistingFilesInDb');

}

}
