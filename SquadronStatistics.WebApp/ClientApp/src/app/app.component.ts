import { Component } from '@angular/core';
import { FilesService } from './_services/files.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ClientApp';
  isDbEmpty = true;

  constructor(
    private fileService: FilesService
  ){
    this.fileService.checkExistingFilesInDb().subscribe(result => {
      this.isDbEmpty = !result;
    });
  }
  
}
