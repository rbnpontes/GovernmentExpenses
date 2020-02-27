using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Entities
{
    internal sealed class InternalExpenseType
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
    internal sealed class InternalExpenseData
    {
        [JsonProperty("fields")]
        public string Fields { get; set; }
        [JsonProperty("records")]
        public IList<object[]> Records { get; set; } 
    }
    internal sealed class InternalExpense : Expense
    {
        // (idx, values[])
        public Tuple<int, object[]> Data { get; set; }
    }
}
