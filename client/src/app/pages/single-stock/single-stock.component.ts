import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-single-stock',
  templateUrl: './single-stock.component.html',
  styleUrls: ['./single-stock.component.css'],
})
export class SingleStockComponent {
  requestSymbol = '';
  constructor(private activatedRoute: ActivatedRoute) {}

  ngOnInit() {
    this.requestSymbol = this.activatedRoute.snapshot.paramMap.get('symbol') || '';
  }
}
