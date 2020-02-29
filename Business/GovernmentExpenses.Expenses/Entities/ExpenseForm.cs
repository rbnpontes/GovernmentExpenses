using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Entities
{
    public class ExpenseForm
    {
        public int? AnoMovimentacao { get; set; }
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
        public int? EmpenhoAno { get; set; }
        public ExpensePair<int?> EmpenhoModalidade { get; set; }
        public int? EmpenhoNumero { get; set; }
        public int? SubEmpenho { get; set; }
        public string IndicadorSubEmpenho { get; set; }
        public ExpensePair<int> Credor { get; set; }
        public ExpensePair<int> ModalidadeLicitacao { get; set; }
        public string ValorEmpenhado { get; set; }
        public string ValorLiquidado { get; set; }
        public string ValorPago { get; set; }
    }
}
