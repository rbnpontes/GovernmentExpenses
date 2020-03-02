import { Injectable } from '@angular/core';
import TotalExpenses from './mock/total_expenses.json';
import { ITotalExpenses } from 'src/models/total.expenses.js';
import { Observable, of } from 'rxjs';
import { delay, timeout, map } from 'rxjs/operators';
import Dictionary from 'src/models/dictionary.js';
import IExpense from 'src/models/expense.js';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment.js';
import IPager from 'src/models/pager.js';
@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  constructor(private http: HttpClient) { }
  public get MonthlyTotalExpenses(): Observable<Dictionary<ITotalExpenses>> {
    return of(TotalExpenses).pipe(timeout(1000));
  }
  public fetchExpensesByQuery(query: string, page: number = 0, pageSize : number = 10): Observable<IPager<IExpense>> {
    let url = environment.apiUrl + 'expenses' + (query ? query : '');
    let params = {
      page : page.toString(),
      pageSize : pageSize.toString()
    };
    return this.http.get(url,
      {
        observe: 'response',
        params : params
      }).pipe(map(x => x.body)) as any;
  }
}
