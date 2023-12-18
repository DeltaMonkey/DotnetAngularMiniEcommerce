import { TestBed } from '@angular/core/testing';

import { AleritfyService } from './aleritfy.service';

describe('AleritfyService', () => {
  let service: AleritfyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AleritfyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
