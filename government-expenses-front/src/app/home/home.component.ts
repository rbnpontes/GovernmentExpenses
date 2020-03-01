import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../app.component';
import { RouterOutlet } from '@angular/router';
import HomeAnimations from './home.animations';

@Component({
  selector: 'home-app',
  templateUrl: './home.component.html',
  animations: HomeAnimations,
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public showNav : boolean = true;
  public pageName : string = 'Dashboard';
  ngOnInit() {
  }
  public prepareRoute(outlet: RouterOutlet){
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData['animation'];
  }
}
