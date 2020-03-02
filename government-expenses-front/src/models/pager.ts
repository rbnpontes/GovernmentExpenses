export default interface IPager<T>{
    page        : number;
    pageCount   : number;
    totalItems  : number;
    items       : T[];
}