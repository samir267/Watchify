import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetaillFilmComponent } from './detaill-film.component';

describe('DetaillFilmComponent', () => {
  let component: DetaillFilmComponent;
  let fixture: ComponentFixture<DetaillFilmComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DetaillFilmComponent]
    });
    fixture = TestBed.createComponent(DetaillFilmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
