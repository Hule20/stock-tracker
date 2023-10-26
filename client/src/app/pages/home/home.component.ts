import { FocusMonitor } from '@angular/cdk/a11y';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { EMPTY, Observable, map, startWith, switchMap } from 'rxjs';
import { AutocompleteDto, Result } from 'src/app/models/autocompleteDto';
import { FinnhubService } from 'src/app/services/finnhub/finnhub.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  formControl = new FormControl('');
  lookupResult?: Observable<string[]>;

  constructor(private finnhubService: FinnhubService) {}

  ngOnInit() {
    this.lookupResult = this.formControl.valueChanges.pipe(
      switchMap((inputValue) => {
        if (inputValue !== null && inputValue !== undefined) {
          return this.finnhubService.fillAutocomplete(inputValue.toString());
        } else {
          return EMPTY;
        }
      }),
      map((searchResult: AutocompleteDto) => {
        return searchResult.result.map((val) => val.symbol);
      })
    );
  }
}