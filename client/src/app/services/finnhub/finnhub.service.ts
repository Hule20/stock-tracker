import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, first, take, tap } from 'rxjs';
import { AutocompleteDto } from 'src/app/models/autocompleteDto';
import { NewsArticle } from 'src/app/models/newsArticleDto';

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
    return this.httpClient
      .get<NewsArticle[]>(`${devEnv.be_base_url}stock/get-news`);
  }
}
