import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { HomeComponent } from '../home.component';
import IExpense from 'src/models/expense';
import { ExpenseService } from 'src/services/Expense.service';
import IPager from 'src/models/pager';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.scss']
})
export class ExpensesComponent implements OnInit, AfterViewInit {
  public readonly expenseColumns = ['id', 'organ', 'category', 'source', 'value_settled', 'value_commited', 'value_payed'];
  public readonly pageSizes = [5, 10, 25, 100];
  public pageIdx = 0;
  public loading = false;
  public data: IPager<IExpense> = { items: [], page: 0, pageCount: 0, totalItems: 0 };
  public get pageSize() : number{
    if(localStorage.getItem('pageSize'))
      return parseInt(localStorage.getItem('pageSize'));
    else
      return 10;
  }
  public set pageSize(value : number){
    localStorage.setItem('pageSize', value.toString());
  }
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(home: HomeComponent, private expense: ExpenseService) {
    home.pageName = 'Expenses';
  }

  ngOnInit() {
    this.loadExpenses();
  }
  ngAfterViewInit(): void {
  }
  private loadExpenses() {
    this.loading = true;
    this.expense.fetchExpensesByQuery('', this.pageIdx, this.pageSize).subscribe(x => {
      this.loading = false;
      this.data = x;
    });
  }
  public handlePageChange(e : PageEvent){
    this.pageSize = e.pageSize;
    this.pageIdx = e.pageIndex;
    this.loadExpenses();
  }
}
