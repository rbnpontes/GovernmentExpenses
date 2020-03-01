import { Component, OnInit, Input } from '@angular/core';
import IExpensePair from 'src/models/expense.pair';

@Component({
  selector: 'dashboard-placeholder',
  templateUrl: './dashboard-placeholder.component.html',
  styleUrls: ['./dashboard-placeholder.component.scss']
})
export class DashboardPlaceholderComponent implements OnInit {
  @Input()
  public value : string;
  @Input()
  public label : string;
  @Input()
  public icon : string;
  constructor() { }

  ngOnInit() {
  }

}
