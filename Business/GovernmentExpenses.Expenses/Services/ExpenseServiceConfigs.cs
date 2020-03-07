using GovernmentExpenses.Core;
using GovernmentExpenses.Expenses.Entities;
using GovernmentExpenses.Expenses.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GovernmentExpenses.Expenses.Services
{
    internal partial class ExpenseService
    {
        public readonly ILogger Logger;
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
        #region Lambda Pairs
        /**
         * This pairs is used for handle a "generic" external property
         * I know I could use LINQ's "Expression" class, but for performances
         * issues i've decide to use a Pre-compiled Expression by Rosyln Compiler and
         * instead of creating at run time.
         * I think this is not best way but Dictionary can be offer a necessary speed
         * to system.
         */
        private readonly Dictionary<string, Func<Expense, object>> OrderKeyPairs = new Dictionary<string, Func<Expense, object>>
        {
            {"id", (x) => x.Id},
            {"ano_movimentacao", (x) => x.AnoMovimentacao},
            {"mes_movimentacao", (x)=> x.MesMovimentacao.Code},
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
            {"valor_pago", (x)=> x.ValorPago},
            {"valor_liquidado", (x)=> x.ValorLiquidado }
        };
        private readonly Dictionary<string, Func<Expense, IExpensePair<object>>> EnumKeyPairs = new Dictionary<string, Func<Expense, IExpensePair<object>>>
        {
            {"orgao",           (x)=> x.Orgao.Default },
            {"unidade",         (x)=> x.Unidade.Default },
            {"categoria",       (x)=> x.CategoriaEconomica.Default},
            {"grupo",           (x)=> x.GrupoDespesa.Default},
            {"mod_aplicacao",   (x)=> x.ModalidadeAplicacao.Default},
            {"elemento",        (x)=> x.Elemento.Default},
            {"sub_elemento",    (x)=> x.SubElemento.Default},
            {"funcao",          (x)=> x.Funcao.Default},
            {"sub_funcao",      (x)=> x.SubFuncao.Default },
            {"programa",        (x)=> x.Programa.Default},
            {"acao",            (x)=> x.Acao.Default },
            {"fonte_recurso",   (x)=> x.FonteRecurso.Default},
            {"empenho_mod",     (x)=> x.EmpenhoModalidade.Default },
            {"credor",          (x)=> x.Credor.Default},
            {"mod_licitacao",   (x)=> x.ModalidadeLicitacao.Default },
            {"mes_movimentacao",(x)=> x.MesMovimentacao.Default}
        };
        private readonly Dictionary<string, Func<IEnumerable<object>, Func<Expense, bool>>> ExpensesKeyPairs = new Dictionary<string, Func<IEnumerable<object>, Func<Expense, bool>>>
        {
            {"orgao",           (values) => (x) => values.Convert<object,int>().Contains((int)x.Orgao.Code)},
            {"unidade",         (values) => (x) => values.Contains(x.Unidade.Code)},
            {"categoria",       (values) => (x) => values.Convert<object,int>().Contains((int)x.CategoriaEconomica.Code)},
            {"grupo",           (values) => (x) => values.Convert<object,int>().Contains((int)x.GrupoDespesa.Code)},
            {"mod_aplicacao",   (values) => (x) => values.Convert<object,int>().Contains((int)x.ModalidadeAplicacao.Code)},
            {"elemento",        (values) => (x) => values.Convert<object,int>().Contains((int)x.Elemento.Code)},
            {"sub_elemento",    (values) => (x) => values.Convert<object,int>().Contains((int)x.SubElemento.Code)},
            {"funcao",          (values) => (x) => values.Convert<object,int>().Contains((int)x.Funcao.Code)},
            {"sub_funcao",      (values) => (x) => values.Convert<object,int>().Contains((int)x.SubFuncao.Code)},
            {"programa",        (values) => (x) => values.Convert<object,int>().Contains((int)x.Programa.Code)},
            {"acao",            (values) => (x) => values.Convert<object,int>().Contains((int)x.Acao.Code)},
            {"fonte_recurso",   (values) => (x) => values.Convert<object,int>().Contains((int)x.FonteRecurso.Code)},
            {"empenho_mod",     (values) => (x) => values.Convert<object,int>().Contains((int)x.EmpenhoModalidade.Code)},
            {"credor",          (values) => (x) => values.Convert<object,int>().Contains((int)x.Credor.Code)},
            {"mod_licitacao",   (values) => (x) => values.Convert<object,int>().Contains((int)x.ModalidadeLicitacao.Code)},
            {"mes_movimentacao",(values) => (x) => values.Convert<object,int>().Contains((int)x.MesMovimentacao.Code)}
        };
        #endregion
        public ExpenseService(ILogger logger)
        {
            Logger = logger;
        }
    }
}
