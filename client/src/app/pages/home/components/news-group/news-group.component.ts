import { Component } from '@angular/core';
import { switchMap, EMPTY, map, Observable } from 'rxjs';
import { AutocompleteDto } from 'src/app/models/finnhub/autocompleteDto';
import { NewsArticle } from 'src/app/models/finnhub/newsArticleDto';
import { FinnhubService } from 'src/app/services/finnhub/finnhub.service';

@Component({
  selector: 'app-news-group',
  templateUrl: './news-group.component.html',
  styleUrls: ['./news-group.component.css'],
})
export class NewsGroupComponent {
  marketNewsResult$?: Observable<NewsArticle[]>;

  constructor(private finnhubService: FinnhubService) {}

  ngOnInit() {
    this.marketNewsResult$ = this.finnhubService.getMarketNews().pipe(
      map((news) => {
        
        return news.map((article) => {
          const dateOnlyStr = article.datetime.slice(0, 10);
          const date = new Date(dateOnlyStr);
          const formattedDate = date.toLocaleDateString('en-GB', {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric'
          })
          
          return { ...article, datetime: formattedDate}
        }).slice(0, 3);
      })
    );
  }
}
