import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarpartComponent } from './navbarpart.component';

describe('NavbarpartComponent', () => {
  let component: NavbarpartComponent;
  let fixture: ComponentFixture<NavbarpartComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NavbarpartComponent]
    });
    fixture = TestBed.createComponent(NavbarpartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
