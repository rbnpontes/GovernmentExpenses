export default class NumberUtils {
    /**
     * Convert Number to Currency String Value
     * @param value Currency Number
     */
    static toCurrency(value: number): string {
        let raw = value.toString().split('.');
        let decimals = parseInt(raw[0]);
        let cents = '00';
        if (raw.length > 1)
            cents = raw[1];
        return decimals.toLocaleString('pt-BR') + ',' + cents;
    }
    static lerp(value: number, to: number, t: number) {
        return (1 - t) * value + t * to;
    }
    static clamp(value: number, min: number, max: number): number {
        return (value >= max) ? max : (value <= min ? min : value);
    }
    static randomInt(min: number, max: number): number {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min)) + min;
    }
    /**
     * Similar to a method Number.toFixed, but this method don't round values
     * @param value 
     * @param decimals 
     * @param appendZero 
     * @author Ruben Gomes
     */
    static truncate(value: number, decimals: number, appendZero: boolean = false): string {
        let [_int, _decimals] = value.toString().split('.');
        let result = _int;
        if (_decimals == undefined && appendZero) {
            result += '.0';
        } else if (_decimals != undefined) {
            _decimals = _decimals.substr(0, decimals);
            if (_decimals.length < decimals && appendZero)
                _decimals += '0';
            result += '.' + _decimals;
        }
        return result;
    }
    static parse(value: string): number {
        value = value.replace('R$', '').replace(/\./g, '').replace(',', '.');
        return parseFloat(value);
    }
}