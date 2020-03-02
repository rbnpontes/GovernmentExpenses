import { Component, OnInit } from '@angular/core';
import { ExpenseService } from 'src/services/Expense.service';
import { ITotalExpenses } from 'src/models/total.expenses';

interface IExpenseResultData{
  code? : number;
  name : string;
  result : ITotalExpenses;
}
@Component({
  selector: 'dashboard-monthly-expenses',
  templateUrl: './dashboard-monthly-expenses.component.html',
  styleUrls: ['./dashboard-monthly-expenses.component.scss']
})
export class DashboardMonthlyExpensesComponent implements OnInit {
  public data : IExpenseResultData[] = [];
  public count : number = 3;
  public loading : boolean = false;
  public get charts() : IExpenseResultData[]{
    return this.data.slice().splice(0, this.count);
  }
  constructor(private expense : ExpenseService) { }
  ngOnInit() {
    this.tryLoadData();
  }
  private async tryLoadData(){
    this.loading = true;
    let values = await this.expense.MonthlyTotalExpenses.toPromise();
    this.data = Object.keys(values).map(key => {
      return {code : values[key].groupCode, name : key, result : values[key]};
    }).sort(x => x.code);
    this.loading = false;
  }
  public tryLoadMore(){
    this.count += 3;
  }
}
