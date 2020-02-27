using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Entities
{
    public class ExpensePair
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public ExpensePair() { }
        public ExpensePair(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
    public class Expense
    {
        public int Id { get; set; }
        public int AnoMovimentacao { get; set; }
        public int MesMovimentacao { get; set; }
        public ExpensePair Orgao { get; set; }
        public ExpensePair Unidade { get; set; }
        public ExpensePair CategoriaEconomica { get; set; }
        public ExpensePair GrupoDespesa { get; set; }
        public ExpensePair ModalidadeAplicacao { get; set; }
        public ExpensePair Elemento { get; set; }
        public ExpensePair SubElemento { get; set; }
        public ExpensePair Funcao { get; set; }
        public ExpensePair SubFuncao { get; set; }
        public ExpensePair Programa { get; set; }
        public ExpensePair Acao { get; set; }
        public ExpensePair FonteRecurso { get; set; }
        public int EmpenhoAno { get; set; }
        public ExpensePair EmpenhoModalidade { get; set; }
        public int EmpenhoNumero { get; set; }
        public int SubEmpenho { get; set; }
        public string IndicadorSubEmpenho { get; set; }
        public ExpensePair Credor { get; set; }
        public ExpensePair ModalidadeLicitacao { get; set; }
        public string ValorEmpenhado { get; set; }
        public string ValorLiquidado { get; set; }
        public string ValorPago { get; set; }
    }
    public class ExpenseDTO : Expense
    {
        public ExpenseDTO(Expense expense)
        {
            Id = expense.Id;
            AnoMovimentacao = expense.AnoMovimentacao;
            MesMovimentacao = expense.MesMovimentacao;
            Orgao = expense.Orgao;
            Unidade = expense.Unidade;
            CategoriaEconomica = expense.CategoriaEconomica;
            GrupoDespesa = expense.GrupoDespesa;
            ModalidadeAplicacao = expense.ModalidadeAplicacao;
            Elemento = expense.Elemento;
            SubElemento = expense.SubElemento;
            Funcao = expense.Funcao;
            SubFuncao = expense.SubFuncao;
            Programa = expense.Programa;
            Acao = expense.Acao;
            FonteRecurso = expense.FonteRecurso;
            EmpenhoAno = expense.EmpenhoAno;
            EmpenhoModalidade = expense.EmpenhoModalidade;
            EmpenhoNumero = expense.EmpenhoNumero;
            SubEmpenho = expense.SubEmpenho;
            IndicadorSubEmpenho = expense.IndicadorSubEmpenho;
            ModalidadeLicitacao = expense.ModalidadeLicitacao;
            ValorEmpenhado = expense.ValorEmpenhado;
            ValorLiquidado = expense.ValorLiquidado;
            ValorPago = expense.ValorPago;
        }
    }
}
