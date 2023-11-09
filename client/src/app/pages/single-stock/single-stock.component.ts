import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, map } from 'rxjs';
import { CompanyName } from 'src/app/models/autocompleteDto';
import { CompanyProfile } from 'src/app/models/companyProfileDto';
import { LatestPriceDto } from 'src/app/models/latestPriceDataDto';
import { FinnhubService } from 'src/app/services/finnhub/finnhub.service';

@Component({
  selector: 'app-single-stock',
  templateUrl: './single-stock.component.html',
  styleUrls: ['./single-stock.component.css'],
})
export class SingleStockComponent {
  requestSymbol = '';
  companyProfile?: CompanyProfile;

  constructor(
    private activatedRoute: ActivatedRoute,
    private finnhubService: FinnhubService
  ) {}

  ngOnInit() {
    this.requestSymbol =
      this.activatedRoute.snapshot.paramMap.get('symbol') || '';

    this.getCompanyProfile(this.requestSymbol);
  }

  private getCompanyProfile(symbol: string) {
    this.finnhubService
      .getCompanyProfile(symbol)
      .subscribe((result) => (this.companyProfile = result));
  }
}
