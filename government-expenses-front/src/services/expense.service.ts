import { Injectable } from '@angular/core';
import TotalExpenses from './mock/total_expenses.json';
import { ITotalExpenses } from 'src/models/total.expenses.js';
import { Observable, of } from 'rxjs';
import { delay, timeout, map } from 'rxjs/operators';
import Dictionary from 'src/models/dictionary.js';
import IExpense from 'src/models/expense.js';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment.js';
import IPager from 'src/models/pager.js';
@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  private API_PATH = environment.apiUrl + 'expenses';
  constructor(private http: HttpClient) { }
  public get TotalExpenses() : Observable<ITotalExpenses>{
    return this.http.get<ITotalExpenses>(this.API_PATH+'/total');
  }
  public get MonthlyTotalExpenses(): Observable<Dictionary<ITotalExpenses>> {
    return this.http.get<Dictionary<ITotalExpenses>>(this.API_PATH+'/total/mes_movimentacao');
  }
  public fetchTotalExpensesByProp(prop : string) : Observable<Dictionary<ITotalExpenses>>{
    return this.http.get<Dictionary<ITotalExpenses>>(this.API_PATH+'/total/'+prop);
  }
  public fetchExpensesByQuery(query: string, page: number = 0, pageSize : number = 10): Observable<IPager<IExpense>> {
    let url = this.API_PATH + (query ? query : '');
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
  public saveExpense(expense : IExpense) : Observable<IExpense>{
    return this.http.put<IExpense>(this.API_PATH+'?id='+expense.id, {
      valorPago : expense.valorPago,
      valorLiquidado : expense.valorLiquidado,
      valorEmpenhado : expense.valorEmpenhado
    });
  }
}
