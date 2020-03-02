import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import IExpense from 'src/models/expense';
import { ExpenseService } from 'src/services/Expense.service';

@Component({
  selector: 'app-expense-edit',
  templateUrl: './expense-edit.component.html',
  styleUrls: ['./expense-edit.component.css']
})
export class ExpenseEditComponent implements OnInit {

  constructor(
    private dialogRef : MatDialogRef<ExpenseEditComponent>, 
    @Inject(MAT_DIALOG_DATA) public data: IExpense,
    private service : ExpenseService) { }

  ngOnInit() {
  }
  private filterValue(str : string) : string{
    let value = parseFloat(str.replace(/[A-Za-z]/g,''));
    return isNaN(value) ? '0' : value.toString().replace('.',',');
  }
  public trySave(){
    this.data.valorEmpenhado = this.filterValue(this.data.valorEmpenhado);
    this.data.valorLiquidado = this.filterValue(this.data.valorLiquidado);
    this.data.valorPago = this.filterValue(this.data.valorPago);
    this.service.saveExpense(this.data).subscribe(()=> this.dialogRef.close())
  }
  public tryClose(){
    this.dialogRef.close();
  }
}
