import { TestBed } from '@angular/core/testing';

import { ResenaServiceService } from './resena.service.service';

describe('ResenaServiceService', () => {
  let service: ResenaServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ResenaServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
