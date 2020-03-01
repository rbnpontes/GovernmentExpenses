import { Injectable } from '@angular/core';
import TotalExpenses from './mock/total_expenses.json';
import { ITotalExpenses } from 'src/models/total.expenses.js';
import { Observable, of } from 'rxjs';
import Dictionary from 'src/models/dictionary.js';
@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  constructor() { }
  public get MonthlyTotalExpenses() : Observable<Dictionary<ITotalExpenses>>{
    return of(TotalExpenses);
  }
}
