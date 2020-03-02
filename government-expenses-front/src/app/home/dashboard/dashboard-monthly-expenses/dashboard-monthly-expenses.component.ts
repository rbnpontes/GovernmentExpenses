import { Component, OnInit, Input } from '@angular/core';
import { ExpenseService } from 'src/services/Expense.service';
import { ITotalExpenses } from 'src/models/total.expenses';
import { IExpenseResultData } from '../dashboard.component';

@Component({
  selector: 'dashboard-monthly-expenses',
  templateUrl: './dashboard-monthly-expenses.component.html',
  styleUrls: ['./dashboard-monthly-expenses.component.scss']
})
export class DashboardMonthlyExpensesComponent {
  @Input()
  public data : IExpenseResultData[];
  public count : number = 3;
  public get loading() : boolean{
    return this.data == null;
  }
  public get charts() : IExpenseResultData[]{
    return this.data.slice().splice(0, this.count);
  }
  constructor(private expense : ExpenseService) { }
  public tryLoadMore(){
    this.count += 3;
  }
}
