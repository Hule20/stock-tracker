import { Component } from '@angular/core';
import { switchMap, EMPTY, map, Observable } from 'rxjs';
import { AutocompleteDto } from 'src/app/models/autocompleteDto';
import { NewsArticle } from 'src/app/models/newsArticleDto';
import { FinnhubService } from 'src/app/services/finnhub/finnhub.service';

@Component({
  selector: 'app-news-group',
  templateUrl: './news-group.component.html',
  styleUrls: ['./news-group.component.css'],
})
export class NewsGroupComponent {
  marketNewsResult$!: Observable<NewsArticle[]>;

  constructor(private finnhubService: FinnhubService) {}

  ngOnInit() {
    this.marketNewsResult$ = this.finnhubService.getMarketNews().pipe(
      map((val) => {
        return val.slice(0, 3);
      })
    );
  }
}
