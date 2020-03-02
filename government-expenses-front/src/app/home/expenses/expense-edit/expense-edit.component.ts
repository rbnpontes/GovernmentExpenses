import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import IExpense from 'src/models/expense';

@Component({
  selector: 'app-expense-edit',
  templateUrl: './expense-edit.component.html',
  styleUrls: ['./expense-edit.component.css']
})
export class ExpenseEditComponent implements OnInit {

  constructor(private dialogRef : MatDialogRef<ExpenseEditComponent>, @Inject(MAT_DIALOG_DATA) public data: IExpense) { }

  ngOnInit() {
  }
  public tryClose(){
    this.dialogRef.close();
  }
}
