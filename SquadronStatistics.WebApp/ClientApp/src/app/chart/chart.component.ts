import { Component, OnInit } from '@angular/core';
import { ChartConfiguration, ChartData, ChartDataset, ChartOptions, LegendElement } from 'chart.js';

import { reduce } from 'rxjs/operators';
import { RowData } from '../_models/rowData';
import { RefreshDataService } from '../_services/refreshData.service';
import { FilesService } from '../_services/files.service';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {
  fileId = 0;
  uploadedDate: string;
  rowsData: RowData[] = [];

  barChartLegend = false;
  barChartPlugins = [];

  barChartOptions: ChartOptions = {
    responsive: true,
    maintainAspectRatio: false,
    scales : {
      yAxes: {
        ticks: {
          stepSize: 10,
        },
        max: 100
      }
    }
  };

  dataSets = [];

  labels = [];
  values = [];
  colors = [];

  barChartData: ChartData;

  constructor(
    refreshDataService: RefreshDataService,
    private filesService: FilesService
    ) { 
    refreshDataService.lastFileId$.subscribe(lastFileId => {
      this.fileId = lastFileId;
      this.refreshRowsData();
    });
  }

  ngOnInit() {
    this.filesService.getLastUploadedFileFromDb().subscribe(file => {
      this.fileId = file.id;
      this.uploadedDate = file.uploadDate;
    }, error => {
      console.log(error);
    }, () => {
      this.refreshRowsData();
    });
  }

  refreshRowsData() {
    this.filesService.getFileRows(this.fileId).subscribe(rows => {
      this.rowsData = rows;
      this.labels = this.rowsData.map(r => {
        return r.label;
      });
      this.values = this.rowsData.map(r => {
        return r.value;
      });
      
      this.colors = this.rowsData.map(r => {
        return r.color;
      });

    },error => {
      console.log(error);
    }, () => {
      this.dataSets = [{ data: this.values, label: '# ' + this.fileId + " Date: " +  this.uploadedDate, 
                        backgroundColor: this.colors, hoverBackgroundColor: this.colors }];
      this.barChartData = { datasets: this.dataSets, labels: this.labels };
    });
  }

}
