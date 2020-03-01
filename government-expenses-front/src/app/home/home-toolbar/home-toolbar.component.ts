import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'home-toolbar',
  templateUrl: './home-toolbar.component.html',
  styleUrls: ['./home-toolbar.component.scss']
})
export class HomeToolbarComponent implements OnInit {
  public appName : string;
  @Output()
  public toggleNav : EventEmitter<any> = new EventEmitter();
  constructor(app : AppComponent) { 
    this.appName = app.AppName;
  }

  ngOnInit() {
  }

}
