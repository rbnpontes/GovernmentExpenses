using GovernmentExpenses.Core;
using GovernmentExpenses.Expenses.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GovernmentExpenses.Expenses.Repository
{
    internal class LocalRepository : IRepository<Expense>
    {
        private ILogger logger_;
        private IList<InternalExpense> expenses_;
        public LocalRepository(ILogger logger)
        {
            logger_ = logger;
            Initialize();
        }
        // Read Database values and save at memory
        private InternalExpenseData ReadInternalData()
        {
            //var data = Utils.DeserializeFile<dynamic>($"{AppDomain.CurrentDomain.BaseDirectory}Artifacts\\db.json");
            //var result = new InternalExpenseData();
            //result.Fields = data.fields;
            //result.Records = data.records;
            //return result;
            return Utils.DeserializeFile<InternalExpenseData>($"{AppDomain.CurrentDomain.BaseDirectory}Artifacts\\db.json");
        }
        private IList<InternalExpense> CreatingExpenses(InternalExpenseData data)
        {
            InternalExpense[] expenses = new InternalExpense[data.Records.Count];
            Parallel.ForEach(data.Records, (item, state, idx) =>
            {
                /* ### TABLE ####
                 * [KEY]:[IDX]
                 * id:0, ano_movimentacao:1, mes_movimentacao:2, orgao_codigo: 3, orgao_nome: 4, unidade_codigo: 5,
                 * unidade_nome: 6, categoria_economica_codigo: 7, categoria_economica_nome: 8, grupo_despesa_codigo: 9,
                 * grupo_despesa_nome: 10, modalidade_aplicacao_codigo: 11, modalidade_aplicacao_nome: 12, elemento_codigo:13,
                 * elemento_nome: 14, subelemento_cod: 15, subelemento_nome: 16, funcao_codigo: 17, funcao_nome: 18,
                 * subfuncao_codigo: 19, subfuncao_nome : 20, programa_codigo: 21, programa_nome: 22, acao_codigo: 23,
                 * acao_nome:24, fonte_recurso_codigo:25, fonte_recurso_nome:26, empenho_ano: 27, empenho_modalidade_nome:28,
                 * empenho_modalidade_codigo: 29, empenho_numero: 30, subempenho: 31, indicador_subempenho: 32, credor_codigo: 33,
                 * credor_nome: 34, modalidade_licitacao_codigo: 35, modalidade_licitacao_nome: 36, valor_empenhado: 37,
                 * valor_liquidado: 38, valor_pago: 39
                 */

                // Fill all Expense Data into a Internal Entity
                InternalExpense expense = new InternalExpense();
                expense.Data = Tuple.Create((int)(long)idx, item);
                expense.Id = (int)(long)item[0];
                expense.AnoMovimentacao = (int)(long)item[1];
                expense.MesMovimentacao = (int)(long)item[2];
                expense.Orgao               = new ExpensePair<int>((int)(long)item[3], (string)item[4]);
                expense.Unidade             = new ExpensePair<float>((float)(double)item[5], (string)item[6]);
                expense.CategoriaEconomica  = new ExpensePair<int>((int)(long)item[7], (string)item[8]);
                expense.GrupoDespesa        = new ExpensePair<int>((int)(long)item[9], (string)item[10]);
                expense.ModalidadeAplicacao = new ExpensePair<int>((int)(long)item[11], (string)item[12]);
                expense.Elemento            = new ExpensePair<int>((int)(long)item[13], (string)item[14]);
                expense.SubElemento         = new ExpensePair<int>((int)(long)item[15], (string)item[16]);
                expense.Funcao              = new ExpensePair<int>((int)(long)item[17], (string)item[18]);
                expense.SubFuncao           = new ExpensePair<int>((int)(long)item[19], (string)item[20]);
                expense.Programa            = new ExpensePair<int>((int)(long)item[21], (string)item[22]);
                expense.Acao                = new ExpensePair<int>((int)(long)item[23], (string)item[24]);
                expense.FonteRecurso        = new ExpensePair<int>((int)(long)item[25], (string)item[26]);
                expense.EmpenhoAno = (int)(long)item[27];
                expense.EmpenhoModalidade   = new ExpensePair<int>((int)(long)item[29], (string)item[28]);
                expense.EmpenhoNumero = (int)(long)item[30];
                expense.SubEmpenho = (int)(long)item[31];
                expense.Credor              = new ExpensePair<int>((int)(long)item[33], (string)item[34]);
                expense.ModalidadeLicitacao = new ExpensePair<int>((int)(long)item[35], (string)item[36]);
                expense.ValorEmpenhado = (string)item[37];
                expense.ValorLiquidado = (string)item[38];
                expense.ValorPago = (string)item[39];
                // Set this new object to list of results
                expenses[idx] = expense;
            });
            return expenses.ToList();
        }
        private void Initialize()
        {
            try
            {
                logger_.LogInformation($"Reading Database at: \"{AppDomain.CurrentDomain.BaseDirectory}Artifacts\\db.json\"");
                InternalExpenseData data = ReadInternalData();
                logger_.LogInformation("Creating Expenses Objects");
                expenses_ = CreatingExpenses(data);
                logger_.LogInformation("Repository Created with Success!");
            }catch(Exception e){
                logger_.LogError(e, "Error has ocurred at initialization of Repository");
                throw e; 
            }
        }
        public IList<Expense> All()
        {
            return expenses_.Cast<Expense>().ToList();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Insert(Expense item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Expense item)
        {
            throw new NotImplementedException();
        }

        public void Update(Expense item)
        {
            throw new NotImplementedException();
        }

        public IList<Expense> Where(Func<Expense, bool> predicate)
        {
            return expenses_.Where(predicate).ToList();
        }
    }
}
