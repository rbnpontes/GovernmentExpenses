import { Component, OnInit } from '@angular/core';
import { environment } from '../environments/environment';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  public readonly AppName = environment.applicationName;
  title = 'government-expenses-front';
  loading : boolean = true;
  public get lockScroll(): boolean{
    return document.body.classList.contains('lock-scroll');
  }
  public set lockScroll(state : boolean){
    document.body.classList[state ? 'add' : 'remove']('lock-scroll');
  }
  ngOnInit(): void {
    // After Open Application, Wait for 1000ms and hide loading
    setTimeout(() => this.loading = false, 1000);
  }
}
