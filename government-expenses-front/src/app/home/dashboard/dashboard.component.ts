import { Component, OnInit } from '@angular/core';
import { HomeComponent } from '../home.component';
import { ITotalExpenses } from 'src/models/total.expenses';
import { ExpenseService } from 'src/services/Expense.service';
import Dictionary from 'src/models/dictionary';

export interface IExpenseResultData{
  code? : number;
  name : string;
  result : ITotalExpenses;
}
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public totalExpense : ITotalExpenses = {totalCommited : 0, totalSettled : 0, totalPayed: 0};
  public monthlyExpense : IExpenseResultData[];
  public categoriesExpense : IExpenseResultData[];
  public sourceExpense : IExpenseResultData[];
  constructor(home : HomeComponent, private expense : ExpenseService) { 
    home.pageName = 'Dashboard';
  }
  public mapExpenses(data : Dictionary<ITotalExpenses>) : IExpenseResultData[]{
    return Object.keys(data).map(x => ({code : data[x].groupCode, name : x, result : data[x]})).sort((a,b) => a.code - b.code);
  }
  ngOnInit() {
    let _this = this;
    let assignResult = (prop : keyof (DashboardComponent)) => x => _this[prop as any]=this.mapExpenses(x);
    this.expense.TotalExpenses.subscribe(x => this.totalExpense = x);
    this.expense.MonthlyTotalExpenses.subscribe(assignResult('monthlyExpense'));
    this.expense.fetchTotalExpensesByProp('categoria').subscribe(assignResult('categoriesExpense'));
    this.expense.fetchTotalExpensesByProp('fonte_recurso').subscribe(assignResult('sourceExpense'));
  }
}
