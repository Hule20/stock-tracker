import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WatchlistStock } from 'src/app/models/db/watchlistStock';
import { devEnv } from 'src/environments/environment.dev';

@Injectable({
  providedIn: 'root',
})
export class DbService {
  constructor(private http: HttpClient) {}

  getUserWatchlist(): Observable<WatchlistStock[]> {
    return this.http.get<WatchlistStock[]>(
      `${devEnv.be_base_url}user/watchlist`
    );
  }
}
