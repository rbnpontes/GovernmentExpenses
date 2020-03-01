import { Component, OnInit, Input } from '@angular/core';
import IExpensePair from 'src/models/expense.pair';
import { ITotalExpenses } from 'src/models/total.expenses';
import NumberUtils from 'src/common/number.utils';
import { Router } from '@angular/router';

@Component({
  selector: 'dashboard-total-expense-graph',
  templateUrl: './dashboard-total-expense-graph.component.html',
  styleUrls: ['./dashboard-total-expense-graph.component.scss']
})
export class DashboardTotalExpenseGraphComponent implements OnInit {
  @Input()
  public title: string;
  @Input()
  public value: ITotalExpenses;
  public get chartData() {
    return [
      this.value.totalSettled,
      this.value.totalCommited,
      this.value.totalPayed
    ]
  }
  public get tooltipInfo() {
    return `Total Settled: R$ ${NumberUtils.toCurrency(this.value.totalSettled)}\n` +
      `Total Commited: R$ ${NumberUtils.toCurrency(this.value.totalCommited)}\n` +
      `Total Payed: R$ ${NumberUtils.toCurrency(this.value.totalPayed)}\n`;
  }
  constructor(private router: Router) { }

  ngOnInit() {
  }

  private tryRedirect() {
    if (this.value.groupCode)
      this.router.navigate(['/expenses'], {queryParams: {q : '/group/mes_movimentacao/' + this.value.groupCode}});
    else
      this.router.navigate(['/expenses']);
  }
}
