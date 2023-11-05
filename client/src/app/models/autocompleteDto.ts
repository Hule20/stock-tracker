export interface AutocompleteDto {
  result: CompanyName[];
}

export interface CompanyName {
  description: string;
  displaySymbol: string;
  symbol: string;
  type: string;
}
