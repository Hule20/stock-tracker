import { Component, Input, ViewChild } from '@angular/core';

import { ChartConfiguration, ChartOptions, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { OhlcTimeseriesDto } from 'src/app/models/ohlcTimeseriesDto';
import { FinnhubService } from 'src/app/services/finnhub/finnhub.service';

@Component({
  selector: 'app-stock-chart',
  templateUrl: './stock-chart.component.html',
  styleUrls: ['./stock-chart.component.css'],
})
export class StockChartComponent {
  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;
  @Input() symbol = '';

  closePriceList!: number[];
  dateList!: string[];

  constructor(private finnhubService: FinnhubService) {}

  ngOnInit() {
    this.finnhubService.getHistoricData(this.symbol).subscribe((entries) => {
      this.closePriceList = entries.map((entry) => entry.close);
      this.dateList = entries.map((entry) => entry.date);

      this.lineChartData.labels = this.dateList;
      this.lineChartData.datasets[0].data = this.closePriceList;

      this.chart?.update();
    });
  }

  public lineChartData: ChartConfiguration<'line'>['data'] = {
    labels: [],
    datasets: [
      {
        data: [],
        label: this.symbol,
        fill: true,
        tension: 0.5,
        borderColor: 'black',
        backgroundColor: 'rgba(255,0,0,0.3)',
      },
    ],
  };
  public lineChartOptions: ChartOptions<'line'> = {
    responsive: false,
    plugins: {
      legend: {
        display: false,
      },
    },
    scales: {
      x: {
        grid: {
          display: false,
        },
      },
      y: {
        grid: {
          display: false,
        },
      },
    },
  };
}
