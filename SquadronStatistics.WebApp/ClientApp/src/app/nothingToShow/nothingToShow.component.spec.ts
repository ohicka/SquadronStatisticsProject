/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { NothingToShowComponent } from './nothingToShow.component';

describe('NothingToShowComponent', () => {
  let component: NothingToShowComponent;
  let fixture: ComponentFixture<NothingToShowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NothingToShowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NothingToShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
