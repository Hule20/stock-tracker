export interface AutocompleteDto {
  result: Result[];
}

export interface Result {
  description: string;
  displaySymbol: string;
  symbol: string;
  type: string;
}
