import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsGroupComponent } from './news-group.component';

describe('NewsGroupComponent', () => {
  let component: NewsGroupComponent;
  let fixture: ComponentFixture<NewsGroupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewsGroupComponent]
    });
    fixture = TestBed.createComponent(NewsGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
