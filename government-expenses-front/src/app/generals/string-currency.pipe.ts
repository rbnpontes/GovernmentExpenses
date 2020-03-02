import { Pipe, PipeTransform } from '@angular/core';
import NumberUtils from 'src/common/number.utils';

@Pipe({
  name: 'stringCurrency'
})
export class StringCurrencyPipe implements PipeTransform {

  transform(value: string): any {
    let _val = NumberUtils.toCurrency(parseFloat(value.replace(',', '.')));
    return `R$ ${_val}`;
  }

}
