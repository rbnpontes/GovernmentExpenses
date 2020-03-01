import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../app.component';

@Component({
  selector: 'home-app',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  public showNav : boolean = true;
  ngOnInit() {
  }

}
