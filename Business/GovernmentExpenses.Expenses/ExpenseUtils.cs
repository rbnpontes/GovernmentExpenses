using GovernmentExpenses.Expenses.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses
{
    public static class ExpenseUtils
    {
        private static readonly string[] MonthNames = {
            "January","February", "March",
            "April", "May", "June",
            "July", "August", "September",
            "October", "November", "December"
        };
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
        public static Expense GetExpenseFromArray(IList<object> item)
        {
            // Fill all Expense Data into a Internal Entity
            InternalExpense expense = new InternalExpense();
            expense.Data = item;
            expense.Id = Convert.ToInt32(item[0]);
            expense.AnoMovimentacao = Convert.ToInt32(item[1]);
            var month = Convert.ToInt32(item[2]);
            expense.MesMovimentacao = new ExpensePair<int>(month, MonthNames[--month]);
            expense.Orgao = new ExpensePair<int>(Convert.ToInt32(item[3]), (string)item[4]);
            expense.Unidade = new ExpensePair<float>(Convert.ToSingle(item[5]), (string)item[6]);
            expense.CategoriaEconomica = new ExpensePair<int>(Convert.ToInt32(item[7]), (string)item[8]);
            expense.GrupoDespesa = new ExpensePair<int>(Convert.ToInt32(item[9]), (string)item[10]);
            expense.ModalidadeAplicacao = new ExpensePair<int>(Convert.ToInt32(item[11]), (string)item[12]);
            expense.Elemento = new ExpensePair<int>(Convert.ToInt32(item[13]), (string)item[14]);
            expense.SubElemento = new ExpensePair<int>(Convert.ToInt32(item[15]), (string)item[16]);
            expense.Funcao = new ExpensePair<int>(Convert.ToInt32(item[17]), (string)item[18]);
            expense.SubFuncao = new ExpensePair<int>(Convert.ToInt32(item[19]), (string)item[20]);
            expense.Programa = new ExpensePair<int>(Convert.ToInt32(item[21]), (string)item[22]);
            expense.Acao = new ExpensePair<int>(Convert.ToInt32(item[23]), (string)item[24]);
            expense.FonteRecurso = new ExpensePair<int>(Convert.ToInt32(item[25]), (string)item[26]);
            expense.EmpenhoAno = Convert.ToInt32(item[27]);
            expense.EmpenhoModalidade = new ExpensePair<int>(Convert.ToInt32(item[29]), (string)item[28]);
            expense.EmpenhoNumero = Convert.ToInt32(item[30]);
            expense.SubEmpenho = Convert.ToInt32(item[31]);
            expense.IndicadorSubEmpenho = (string)item[32];
            expense.Credor = new ExpensePair<int>(Convert.ToInt32(item[33]), (string)item[34]);
            expense.ModalidadeLicitacao = new ExpensePair<int>(Convert.ToInt32(item[35]), (string)item[36]);
            expense.ValorEmpenhado = (string)item[37];
            expense.ValorLiquidado = (string)item[38];
            expense.ValorPago = (string)item[39];
            return expense;
        }
        public static IList<object> GetArrayFromExpense(Expense item)
        {
            IList<object> values = new object[40];
            values[0] = item.Id;
            values[1] = item.AnoMovimentacao;
            values[2] = item.MesMovimentacao.Code;
            // Orgao
            values[3] = item.Orgao.Code;
            values[4] = item.Orgao.Name;
            // Unidade
            values[5] = item.Unidade.Code;
            values[6] = item.Unidade.Name;
            // Categoria
            values[7] = item.CategoriaEconomica.Code;
            values[8] = item.CategoriaEconomica.Name;
            // Grupo
            values[9] = item.GrupoDespesa.Code;
            values[10] = item.GrupoDespesa.Name;
            // Modalidade Aplicacao
            values[11] = item.ModalidadeAplicacao.Code;
            values[12] = item.ModalidadeAplicacao.Name;
            // Elemento
            values[13] = item.Elemento.Code;
            values[14] = item.Elemento.Name;
            // Sub Elemento
            values[15] = item.SubElemento.Code;
            values[16] = item.SubElemento.Name;
            // Funcao
            values[17] = item.Funcao.Code;
            values[18] = item.Funcao.Name;
            // Sub Funcao
            values[19] = item.SubFuncao.Code;
            values[20] = item.SubFuncao.Name;
            // Programa
            values[21] = item.Programa.Code;
            values[22] = item.Programa.Name;
            // Acao
            values[23] = item.Acao.Code;
            values[24] = item.Acao.Name;
            // Fonte Recurso
            values[25] = item.FonteRecurso.Code;
            values[26] = item.FonteRecurso.Name;
            // Empenho Ano
            values[27] = item.EmpenhoAno;
            // Empenho Modalidade
            values[29] = item.EmpenhoModalidade.Code;
            values[28] = item.EmpenhoModalidade.Name;
            // Empenho Numero
            values[30] = item.EmpenhoNumero;
            // Sub Empenho
            values[31] = item.SubEmpenho;
            values[32] = item.IndicadorSubEmpenho;
            // Credor
            values[33] = item.Credor.Code;
            values[34] = item.Credor.Name;
            // Modalidade Licitacao
            values[35] = item.ModalidadeLicitacao.Code;
            values[36] = item.ModalidadeLicitacao.Name;
            // Valor Empenhado
            values[37] = item.ValorEmpenhado;
            // Valor Liquidado
            values[38] = item.ValorLiquidado;
            // Valor Pago
            values[39] = item.ValorPago;
            return values;
        }
    }
}
