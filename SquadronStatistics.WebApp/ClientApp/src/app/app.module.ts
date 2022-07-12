import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';
import { FilesGridDisplayComponent } from './filesGridDisplay/filesGridDisplay.component';
import { HttpClientModule } from '@angular/common/http';
import { FileUploaderComponent } from './fileUploader/fileUploader.component';
import { AppHeaderComponent } from './appHeader/appHeader.component';
import { NgChartsModule } from 'ng2-charts';
import { ChartComponent } from './chart/chart.component';
import { NothingToShowComponent } from './nothingToShow/nothingToShow.component';

@NgModule({
  declarations: [					
    AppComponent,
      FilesGridDisplayComponent,
      FileUploaderComponent,
      AppHeaderComponent,
      ChartComponent,
      NothingToShowComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    NgChartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
