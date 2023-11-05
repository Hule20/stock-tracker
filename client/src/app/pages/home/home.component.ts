import { FocusMonitor } from '@angular/cdk/a11y';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { EMPTY, Observable, map, switchMap, take } from 'rxjs';
import {
  AutocompleteDto,
  CompanyName,
} from 'src/app/models/autocompleteDto';
import { NewsArticle } from 'src/app/models/newsArticleDto';
import { FinnhubService } from 'src/app/services/finnhub/finnhub.service';
import { SharedDataService } from 'src/app/services/shared-data/shared-data.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  formControl = new FormControl('');
  symbolSearchResult$?: Observable<CompanyName[]>;

  constructor(
    private finnhubService: FinnhubService,
    private sharedDataService: SharedDataService
  ) {
    this.symbolSearchResult$ = this.formControl.valueChanges.pipe(
      switchMap((inputValue) => {
        if (inputValue !== null && inputValue !== undefined) {
          return this.finnhubService.fillAutocomplete(inputValue.toString());
        } else {
          return EMPTY;
        }
      }),
      map((value) => {
        return value.result;
      })
    );
  }

  setCompanyInfo(option: CompanyName) {
    this.sharedDataService.setCompanyInfo(option);
  }
}
