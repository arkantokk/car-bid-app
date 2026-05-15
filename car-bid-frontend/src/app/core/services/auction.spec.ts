import { TestBed } from '@angular/core/testing';

import { AuctionService } from './auctionService';

describe('Auction', () => {
  let service: AuctionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuctionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
