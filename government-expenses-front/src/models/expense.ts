import IExpensePair from './expense.pair';

export default interface IExpense{
    id                  : number;
    anoMovimentacao     : number;
    mesMovimentacao     : IExpensePair;
    orgao               : IExpensePair;
    unidade             : IExpensePair;
    categoriaEconomica  : IExpensePair;
    grupoDespesa        : IExpensePair;
    modalidadeAplicacao : IExpensePair;
    elemento            : IExpensePair;
    subElemento         : IExpensePair;
    funcao              : IExpensePair;
    subFuncao           : IExpensePair;
    programa            : IExpensePair;
    acao                : IExpensePair;
    fontRecurso         : IExpensePair;
    empenhoAno          : number;
    empenhoModalidade   : IExpensePair;
    empenhoNumero       : number;
    subEmpenho          : number;
    indicadorSubEmpenho : string;
    credor              : IExpensePair;
    modalidadeLicitacao : IExpensePair;
    valorEmpenhado      : string;
    valorLiquidado      : string;
    valorPago           : string;
}