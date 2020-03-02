import { Component, OnInit, Input } from '@angular/core';
import { ITotalExpenses } from 'src/models/total.expenses';

@Component({
  selector: 'dashboard-generic-graph',
  templateUrl: './dashboard-generic-graph.component.html',
  styleUrls: ['./dashboard-generic-graph.component.scss']
})
export class DashboardGenericGraphComponent implements OnInit {
  public get loading(){
    return this.data == null;
  }
  @Input()
  public data : ITotalExpenses[];
  @Input()
  public groupProp : string;
  public page : number = 0;
  public get chartData() : ITotalExpenses[] {
    return this.data.slice().splice(this.page * 4, 4);
  }
  constructor() { }

  ngOnInit() {
  }
  tryNavLeft(){
    if(this.page < 0 || this.loading)
      return;
    this.page--;
  }
  tryNavRight(){
    if(this.loading)
      return;
    this.page++;
    if(this.page * 4 > this.data.length)
      this.page--;
  }
}
