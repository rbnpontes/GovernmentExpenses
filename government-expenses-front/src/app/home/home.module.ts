import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartsModule } from 'ng2-charts';
import { HomeComponent } from './home.component';
import { HomeSidebarComponent } from './home-sidebar/home-sidebar.component';
import { SharedModule } from '../modules/shared/shared.module';
import { HomeToolbarComponent } from './home-toolbar/home-toolbar.component';
import { UserProfileComponent } from './home-sidebar/user-profile/user-profile.component';
import { SidebarItemComponent } from './home-sidebar/sidebar-item/sidebar-item.component';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ExpensesComponent } from './expenses/expenses.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardPlaceholderComponent } from './dashboard/dashboard-placeholder/dashboard-placeholder.component';
import { DashboardMonthlyExpensesComponent } from './dashboard/dashboard-monthly-expenses/dashboard-monthly-expenses.component';
import { DashboardTotalExpenseGraphComponent } from './dashboard/dashboard-total-expense-graph/dashboard-total-expense-graph.component';
import { StringCurrencyPipe } from '../generals/string-currency.pipe';
import { DashboardGenericGraphComponent } from './dashboard/dashboard-generic-graph/dashboard-generic-graph.component';

const routes : Routes = [
  {path: '', redirectTo: '/home', pathMatch: 'full'},
  {path: 'home', component : DashboardComponent},
  {path: 'dashboard', component : DashboardComponent},
  {path: 'expenses', component : ExpensesComponent},
  {path: 'about', component : DashboardComponent},
  {path: 'licenses', component : DashboardComponent}
];

@NgModule({
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    SharedModule,
    ChartsModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    HomeComponent,
    HomeSidebarComponent
  ],
  declarations: [
    HomeComponent, 
    HomeSidebarComponent,
    HomeToolbarComponent,
    UserProfileComponent,
    SidebarItemComponent,
    DashboardComponent,
    ExpensesComponent,
    DashboardPlaceholderComponent,
    DashboardMonthlyExpensesComponent,
    DashboardTotalExpenseGraphComponent,
    DashboardGenericGraphComponent
  ]
})
export class HomeModule { }
