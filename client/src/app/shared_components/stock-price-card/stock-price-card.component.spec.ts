import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockPriceCardComponent } from './stock-price-card.component';

describe('StockPriceCardComponent', () => {
  let component: StockPriceCardComponent;
  let fixture: ComponentFixture<StockPriceCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [StockPriceCardComponent]
    });
    fixture = TestBed.createComponent(StockPriceCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
