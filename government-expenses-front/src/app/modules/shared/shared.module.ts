import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from './material.module';
import { StringCurrencyPipe } from 'src/app/generals/string-currency.pipe';
@NgModule({
  imports: [
    CommonModule,
    MaterialModule
  ], exports: [
    MaterialModule,
    StringCurrencyPipe
  ],
  declarations: [
    StringCurrencyPipe
  ],
})
export class SharedModule { }
