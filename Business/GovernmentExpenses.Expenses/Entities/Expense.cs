using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Entities
{
    public interface IExpensePair<T>
    {
        public T Code { get; set; }
        public string Name { get; set; }
    }
    public class ExpensePair<TCode> : IExpensePair<TCode>
    {
        public ExpensePair() { }
        internal ExpensePair(object code, string name)
        {
            Code = (TCode)Convert.ChangeType(code, typeof(TCode));
            Name = name;
        }

        public TCode Code { get; set; }
        public string Name { get; set; }
        public IExpensePair<object> Default
        {
            get
            {
                return new ExpensePair<object>(this.Code, this.Name);
            }
        }
    }
    public class Expense
    {
        public int Id { get; set; }
        public int AnoMovimentacao { get; set; }
        public ExpensePair<int> MesMovimentacao { get; set; }
        public ExpensePair<int> Orgao { get; set; }
        public ExpensePair<float> Unidade { get; set; }
        public ExpensePair<int> CategoriaEconomica { get; set; }
        public ExpensePair<int> GrupoDespesa { get; set; }
        public ExpensePair<int> ModalidadeAplicacao { get; set; }
        public ExpensePair<int> Elemento { get; set; }
        public ExpensePair<int> SubElemento { get; set; }
        public ExpensePair<int> Funcao { get; set; }
        public ExpensePair<int> SubFuncao { get; set; }
        public ExpensePair<int> Programa { get; set; }
        public ExpensePair<int> Acao { get; set; }
        public ExpensePair<int> FonteRecurso { get; set; }
        public int EmpenhoAno { get; set; }
        public ExpensePair<int> EmpenhoModalidade { get; set; }
        public int EmpenhoNumero { get; set; }
        public int? SubEmpenho { get; set; }
        public string IndicadorSubEmpenho { get; set; }
        public ExpensePair<int> Credor { get; set; }
        public ExpensePair<int> ModalidadeLicitacao { get; set; }
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
            Credor = expense.Credor;
            ModalidadeLicitacao = expense.ModalidadeLicitacao;
            ValorEmpenhado = expense.ValorEmpenhado;
            ValorLiquidado = expense.ValorLiquidado;
            ValorPago = expense.ValorPago;
        }
    }
}
