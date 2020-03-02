import { Component, OnInit } from '@angular/core';
import { HomeComponent } from '../home.component';
import { ITotalExpenses } from 'src/models/total.expenses';
import { ExpenseService } from 'src/services/Expense.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public totalExpense : ITotalExpenses = {totalCommited : 0, totalSettled : 0, totalPayed: 0};
  constructor(home : HomeComponent, private expense : ExpenseService) { 
    home.pageName = 'Dashboard';
  }
  ngOnInit() {
    this.expense.TotalExpenses.subscribe(x => this.totalExpense = x);
  }
}
