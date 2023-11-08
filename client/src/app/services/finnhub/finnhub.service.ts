import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, first, take, tap } from 'rxjs';
import { AutocompleteDto, CompanyName } from 'src/app/models/autocompleteDto';
import { CompanyProfile } from 'src/app/models/companyProfileDto';
import { LatestPriceDto } from 'src/app/models/latestPriceDataDto';
import { NewsArticle } from 'src/app/models/newsArticleDto';
import { OhlcTimeseriesDto } from 'src/app/models/ohlcTimeseriesDto';

import { devEnv } from 'src/environments/environment.dev';

@Injectable({
  providedIn: 'root',
})
export class FinnhubService {
  constructor(private httpClient: HttpClient) {}

  fillAutocomplete(symbol: string): Observable<AutocompleteDto> {
    return this.httpClient.get<AutocompleteDto>(
      `${devEnv.be_base_url}stock/lookup?ticker=${symbol}`
    );
  }

  getMarketNews(): Observable<NewsArticle[]> {
    return this.httpClient.get<NewsArticle[]>(
      `${devEnv.be_base_url}stock/news`
    );
  }

  // currently set to fixed date
  getHistoricData(symbol: string): Observable<OhlcTimeseriesDto[]> {
    return this.httpClient.get<OhlcTimeseriesDto[]>(
      `${devEnv.be_base_url}stock/historic?ticker=${symbol}&dateFrom=2023-10-20&dateTo=2023-10-30`
    );
  }

  getLatestPriceData(symbol: string): Observable<LatestPriceDto> {
    return this.httpClient.get<LatestPriceDto>(
      `${devEnv.be_base_url}stock/latest?ticker=${symbol}`
    );
  }

  getCompanyProfile(symbol: string): Observable<CompanyProfile> {
    return this.httpClient.get<CompanyProfile>(
      `${devEnv.be_base_url}stock/company-profile?ticker=${symbol}`
    );
  }
}
