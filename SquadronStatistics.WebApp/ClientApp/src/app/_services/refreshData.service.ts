import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RefreshDataService {

// fileId source
private lastFileIdSource = new Subject<number>();

// observable fileId stream
lastFileId$ = this.lastFileIdSource.asObservable();

constructor() { }

announceNewFileId(fileId: number) {
  this.lastFileIdSource.next(fileId);
}

}
