import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'stringCurrency'
})
export class StringCurrencyPipe implements PipeTransform {

  transform(value: string): any {
    let _val = parseFloat(value.replace(',', '.'));
    return `R$ ${_val}`;
  }

}
