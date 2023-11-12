import { Component, Input } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { map, take } from 'rxjs';
import { CompanyProfile } from 'src/app/models/companyProfileDto';
import { LatestPriceDto } from 'src/app/models/latestPriceDataDto';
import { DbService } from 'src/app/services/database/db.service';
import { FinnhubService } from 'src/app/services/finnhub/finnhub.service';

@Component({
  selector: 'app-stock-price-card',
  templateUrl: './stock-price-card.component.html',
  styleUrls: ['./stock-price-card.component.css'],
})
export class StockPriceCardComponent {
  @Input() symbol = '';
  @Input() showRemoveButton = false;
  latestStockPrice?: LatestPriceDto;
  companyProfile?: CompanyProfile;
  constructor(
    private finnhubService: FinnhubService,
    private dbService: DbService,
    private snackBar: MatSnackBar
  ) {}

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

  removeFromWatchlist(symbol: string) {
    this.dbService.removeFromUserWatchlist(symbol).subscribe(
      (next) => {
        this.snackBar.open(`${symbol} removed from watchlist`, 'OK', {
          duration: 3000,
        });
      },
      (err) => console.log('error', err)
    );
  }
}
