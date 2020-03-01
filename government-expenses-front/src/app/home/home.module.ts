import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { HomeSidebarComponent } from './home-sidebar/home-sidebar.component';
import { SharedModule } from '../modules/shared/shared.module';
import { HomeToolbarComponent } from './home-toolbar/home-toolbar.component';
import { UserProfileComponent } from './home-sidebar/user-profile/user-profile.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    HomeComponent,
    HomeSidebarComponent
  ],
  declarations: [
    HomeComponent, 
    HomeSidebarComponent,
    HomeToolbarComponent,
    UserProfileComponent
  ]
})
export class HomeModule { }
