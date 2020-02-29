using GovernmentExpenses.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Controllers
{
    public partial class ExpenseController
    {
        /// <summary>
        /// Retrieve all Orderable Properties used in a List of Expenses
        /// </summary>
        /// <param name="desc">Order by descending</param>
        /// <returns>Returns a List of Orderable Properties</returns>
        [HttpGet("order-keys")]
        [HttpGet("search/order-keys")]
        [HttpGet("group/order-keys")]
        public IDictionary<string, IEnumerable<string>> GetExpensesOrderKeys([FromQuery(Name = "orderDesc")]bool? desc)
        {
            return GetOrderKeyData(service_.FetchExpensesOrderKeys(desc));
        }
        /// <summary>
        /// Retrieve all Properties used in a Expenses
        /// </summary>
        /// <param name="desc"></param>
        /// <returns>Returns a list of Properties</returns>
        [HttpGet("keys")]
        [HttpGet("search/keys")]
        [HttpGet("total/{prop}/keys")]
        [HttpGet("group/keys")]
        public IDictionary<string, IEnumerable<string>> GetExpensesProperties([FromQuery(Name = "orderDesc")]bool? desc)
        {
            return GetOrderKeyData(service_.FetchExpensesKeys(desc));
        }
        /// <summary>
        /// Return all Orderable Properties used in a Enums
        /// </summary>
        /// <returns>Returns a List of Orderable Properties</returns>
        [HttpGet("enums/order-keys")]
        public IDictionary<string, IEnumerable<string>> GetEnumsOrderKeys()
        {
            return GetOrderKeyData(new string[] { "code", "name" });
        }
    }
}
