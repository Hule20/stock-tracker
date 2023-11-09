import { Component } from '@angular/core';
import { WatchlistStock } from 'src/app/models/db/watchlistStock';
import { DbService } from 'src/app/services/database/db.service';

@Component({
  selector: 'app-watchlist',
  templateUrl: './watchlist.component.html',
  styleUrls: ['./watchlist.component.css'],
})
export class WatchlistComponent {
  watchlist?: WatchlistStock[];/*  = [
    { id: 1, symbol: 'GME' }
  ]; */

  constructor(private dbService: DbService) {}

  ngOnInit() {
    this.dbService.getUserWatchlist().subscribe((result) => {
      console.log(result);
      this.watchlist = result;
    });

  }
}
