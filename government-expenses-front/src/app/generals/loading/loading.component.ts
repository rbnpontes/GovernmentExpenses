import { Component, OnInit, Input, HostBinding } from '@angular/core';
import {
  trigger,
  state,
  style,
  animate
} from '@angular/animations';
import LoadingAnimations from './loading.animations';
@Component({
  selector: 'loading',
  templateUrl: './loading.component.html',
  animations: LoadingAnimations,
  styleUrls: ['./loading.component.scss']
})
export class LoadingComponent implements OnInit {
  @Input('visible')
  public visible : boolean;
  @HostBinding('@.disabled')
  private disableAnim : boolean = true;
  constructor() { }

  ngOnInit() {
    setTimeout(()=> this.disableAnim = false);
  }
}
