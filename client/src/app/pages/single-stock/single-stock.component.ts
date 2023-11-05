import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { CompanyName } from 'src/app/models/autocompleteDto';
import { LatestPriceDto } from 'src/app/models/latestPriceDataDto';
import { FinnhubService } from 'src/app/services/finnhub/finnhub.service';
import { SharedDataService } from 'src/app/services/shared-data/shared-data.service';

@Component({
  selector: 'app-single-stock',
  templateUrl: './single-stock.component.html',
  styleUrls: ['./single-stock.component.css'],
})
export class SingleStockComponent {
  requestSymbol = '';
  latestStockPrice!: LatestPriceDto;
  companyName?: CompanyName;

  constructor(
    private activatedRoute: ActivatedRoute,
    private finnhubService: FinnhubService,
    private sharedDataService: SharedDataService
  ) {}

  ngOnInit() {
    this.requestSymbol =
      this.activatedRoute.snapshot.paramMap.get('symbol') || '';

    this.getStockData(this.requestSymbol);
    this.getCompanyInfoData();
  }

  private getStockData(symbol: string) {
    this.finnhubService
      .getLatestPriceData(symbol)
      .subscribe((result) => (this.latestStockPrice = result));
  }

  private getCompanyInfoData() {
    this.sharedDataService.getCompanyInfo().subscribe(
      (info) => (this.companyName = info)
    );
  }
}
