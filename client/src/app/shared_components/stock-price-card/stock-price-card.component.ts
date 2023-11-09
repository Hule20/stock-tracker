import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { CompanyProfile } from 'src/app/models/companyProfileDto';
import { LatestPriceDto } from 'src/app/models/latestPriceDataDto';
import { FinnhubService } from 'src/app/services/finnhub/finnhub.service';

@Component({
  selector: 'app-stock-price-card',
  templateUrl: './stock-price-card.component.html',
  styleUrls: ['./stock-price-card.component.css'],
})
export class StockPriceCardComponent {
  @Input() symbol = '';
  latestStockPrice?: LatestPriceDto;
  companyProfile?: CompanyProfile;

  constructor(private finnhubService: FinnhubService) {}

  ngOnInit() {
    this.getStockData(this.symbol);
    this.getCompanyProfile(this.symbol);
  }

  private getStockData(symbol: string) {
    this.finnhubService
      .getLatestPriceData(symbol)
      .pipe(
        map((value) => {
          let fixedPercentage = Math.round(value.changePercentage * 100) / 100;
          return { ...value, changePercentage: fixedPercentage };
        })
      )
      .subscribe((result) => (this.latestStockPrice = result));
  }

  private getCompanyProfile(symbol: string) {
    this.finnhubService
      .getCompanyProfile(symbol)
      .subscribe((result) => (this.companyProfile = result));
  }
}
