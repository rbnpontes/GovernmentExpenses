using GovernmentExpenses.Core;
using GovernmentExpenses.Expenses.Entities;
using GovernmentExpenses.Expenses.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Services
{
    internal partial class ExpenseService
    {
        public ILogger Logger { get; private set; }
        private IRepository<Expense> repository_;
        public IRepository<Expense> Repository
        {
            get
            {
                if (repository_ == null)
                    repository_ = new ExpenseRepository(Logger);
                return repository_;
            }
        }
        private Dictionary<string, Func<Expense, object>> OrderKeyPairs = new Dictionary<string, Func<Expense, object>>
        {
            {"id", (x) => x.Id},
            {"ano_movimentacao", (x) => x.AnoMovimentacao},
            {"mes_movimentacao", (x)=> x.MesMovimentacao},
            {"orgao", (x) => x.Orgao.Code},
            {"unidade", (x)=> x.Unidade.Code},
            {"categoria",(x)=> x.CategoriaEconomica.Code},
            {"grupo", (x)=> x.GrupoDespesa.Code},
            {"mod_aplicacao",(x)=> x.ModalidadeAplicacao.Code},
            {"elemento", (x)=> x.Elemento.Code},
            {"sub_elemento", (x)=> x.SubElemento.Code},
            {"funcao", (x)=> x.Funcao.Code },
            {"sub_funcao", (x)=> x.SubFuncao.Code },
            {"programa", (x)=> x.Programa.Code},
            {"acao", (x)=>x.Acao.Code },
            {"fonte_recurso", (x)=> x.FonteRecurso.Code},
            {"empenho_ano", (x) => x.EmpenhoAno},
            {"empenho_mod", (x)=> x.EmpenhoModalidade.Code },
            {"empenho_num", (x)=> x.EmpenhoNumero},
            {"sub_empenho", (x)=> x.SubEmpenho},
            {"ind_sub_empenho", (x)=> x.IndicadorSubEmpenho },
            {"credor", (x) => x.Credor.Code},
            {"mod_licitacao", (x)=> x.ModalidadeLicitacao.Code },
            {"valor_empenhado", (x) => x.ValorEmpenhado},
            {"valor_pago", (x)=> x.ValorPago}
        };
    }
}
