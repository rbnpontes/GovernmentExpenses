import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GeneralModule } from './generals/general.module';
import { HomeModule } from './home/home.module';
import { HttpClientModule } from '@angular/common/http';
import { StringCurrencyPipe } from './generals/string-currency.pipe';

@NgModule({
   declarations: [
      AppComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      BrowserAnimationsModule,
      HttpClientModule,
      GeneralModule,
      HomeModule
   ],
   exports: [],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
