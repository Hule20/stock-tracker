import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { Observable, map } from 'rxjs';
import { CompanyName } from 'src/app/models/autocompleteDto';
import { CompanyProfile } from 'src/app/models/companyProfileDto';
import { LatestPriceDto } from 'src/app/models/latestPriceDataDto';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';
import { DbService } from 'src/app/services/database/db.service';
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
    private finnhubService: FinnhubService,
    public authService: AuthenticationService,
    private dbService: DbService,
    private snackBar: MatSnackBar
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

  public addToWatchlist() {
    this.dbService.addToUserWatchlist(this.requestSymbol).subscribe(
      (next) => {
        this.snackBar.open(`${this.requestSymbol} added to watchlist!`, 'OK', {
          duration: 3000,
        });
      },
      (err) => console.log('err', err)
    );
  }
}
