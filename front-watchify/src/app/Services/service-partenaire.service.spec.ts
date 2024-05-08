import { TestBed } from '@angular/core/testing';

import { ServicePartenaireService } from './service-partenaire.service';

describe('ServicePartenaireService', () => {
  let service: ServicePartenaireService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServicePartenaireService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
