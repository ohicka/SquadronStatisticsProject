import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge } from 'rxjs';
import { Pagination } from '../_models/pagination';
import { FilesService } from '../_services/files.service';
import { RefreshDataService } from '../_services/refreshData.service';

@Component({
  selector: 'app-filesGridDisplay',
  templateUrl: './filesGridDisplay.component.html',
  styleUrls: ['./filesGridDisplay.component.css']
})
export class FilesGridDisplayComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['id', 'uploadDate',  'fileName'];
  //exampleDatabase: ExampleHttpDatabase | null;
  files: File[] = [];
  pagination: Pagination;
  resultsLength = 0;

  isLoadingResults = true;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private filesService: FilesService,
    refreshDataService: RefreshDataService
  ) {
      refreshDataService.lastFileId$.subscribe(lastFileId => {
        this.paginator.pageIndex = 0;
        this.loadFileList(this.paginator.pageIndex, this.paginator.pageSize);
      });
   }

  ngOnInit(): void {
   // this.loadFileList();
  }

  ngAfterViewInit() {

    this.loadFileList(this.paginator.pageIndex, this.paginator.pageSize);

  }

  loadFileList(pageIndex: number, pageSize: number) {
    this.isLoadingResults = true;
    this.filesService.getUploadedFiles(pageIndex + 1, pageSize).subscribe(response => {
      this.files = response.result;
      this.pagination = response.pagination;
    }, error => {
      this.isLoadingResults = false;
      console.log(error);
    }, () => {
      this.isLoadingResults = false;
      this.resultsLength = this.pagination.totalItems;
    });

  }

  onPageChanged(event: any) {
    this.loadFileList(event.pageIndex, event.pageSize);
  }
}

