using GovernmentExpenses.Core;
using GovernmentExpenses.Expenses.Entities;
using GovernmentExpenses.Expenses.Interfaces;
using GovernmentExpenses.Expenses.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GovernmentExpenses.Expenses.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public partial class ExpenseController : ControllerBase
    {
        private readonly IExpenseService service_;
        public ExpenseController(IExpenseService service)
        {
            service_ = service;
        }
        private IDictionary<string, IEnumerable<string>> GetOrderKeyData(IEnumerable<string> values)
        {
            return new Dictionary<string, IEnumerable<string>>
            {
                {"items", values}
            };
        }
        private Pager<T> TryPaginate<T>(IEnumerable<T> source, int? page, int? pageSize)
        {
            if (page == null)
                page = 0;
            if (pageSize == null)
                pageSize = 10;

            return new Pager<T>
            {
                Page = page.Value,
                PageCount = source.Count() / pageSize.Value,
                TotalItems = source.Count(),
                Items = source.Page(page.Value, pageSize.Value)
            };
        }
        /// <summary>
        /// Retrieve all Expenses
        /// </summary>
        /// <param name="page">Set Page index</param>
        /// <param name="pageSize">Set Page Size</param>
        /// <param name="orderBy">Order items by prop</param>
        /// <param name="orderDesc">Order items by descending</param>
        /// <returns>Return as List of Expenses</returns>
        [HttpGet]
        public Pager<ExpenseDTO> Get(
            [FromQuery(Name = "page")] int page = 0,
            [FromQuery(Name = "pageSize")] int pageSize = 10,
            [FromQuery(Name = "orderBy")] string orderBy = null,
            [FromQuery(Name = "orderDesc")] bool? orderDesc = null)
        {
            return TryPaginate(service_.FetchAll(orderBy, orderDesc), page, pageSize);
        }
        /// <summary>
        /// Edit a Expense
        /// </summary>
        /// <param name="id">Expense ID</param>
        /// <param name="form">Expense Form Values</param>
        /// <returns>Return a Final Expense Value</returns>
        [HttpPut]
        public ExpenseDTO Edit(int id, ExpenseForm form)
        {
            return service_.TryEditExpense(id, form);
        }
        /// <summary>
        /// Try make a Search on all expenses
        /// </summary>
        /// <param name="criteria">Search Value</param>
        /// <param name="page">Set Page index</param>
        /// <param name="pageSize">Set Page Size</param>
        /// <param name="orderBy">Order Items by a Prop</param>
        /// <param name="orderDesc">Order Items by Descending</param>
        /// <returns>Return as List of ExpenseDTO</returns>
        [HttpGet("search")]
        public Pager<ExpenseDTO> Search(
            [FromQuery(Name = "q")] string criteria,
            [FromQuery(Name = "page")] int page = 0,
            [FromQuery(Name = "pageSize")] int pageSize = 10,
            [FromQuery(Name = "orderBy")] string orderBy = null,
            [FromQuery(Name = "orderDesc")] bool? orderDesc = null)
        {
            return TryPaginate(service_.FetchSearch(criteria, null, orderBy, orderDesc), page, pageSize);
        }
        /// <summary>
        /// Try make a search on a specific member
        /// </summary>
        /// <param name="criteria">Search Value</param>
        /// <param name="prop">Property Member</param>
        /// <param name="page">Set Page index</param>
        /// <param name="pageSize">Set Page Size</param>
        /// <param name="orderBy">Order Items by a Prop</param>
        /// <param name="orderDesc">Order Items by Descending</param>
        /// <returns>Rturns as List of ExpenseDTO</returns>
        [HttpGet("search/{prop}")]
        public Pager<ExpenseDTO> Search(
            [FromQuery(Name = "q")] string criteria,
            string prop,
            [FromQuery(Name = "page")] int page = 0,
            [FromQuery(Name = "pageSize")] int pageSize = 10,
            [FromQuery(Name = "orderBy")] string orderBy = null,
            [FromQuery(Name = "orderDesc")] bool? orderDesc = null)
        {
            return TryPaginate(service_.FetchSearch(criteria, prop, orderBy, orderDesc), page, pageSize);
        }
        /// <summary>
        /// Return a Sums of all expenses data
        /// </summary>
        /// <returns>Return a Result from all Calc</returns>
        [HttpGet("total")]
        public IExpenseResult GetTotalExpenses()
        {
            return service_.FetchTotalExpenses();
        }
        /// <summary>
        /// Return a Sum of all expenses data by a specific property member
        /// </summary>
        /// <param name="prop">Property Name</param>
        /// <returns>Return a result from all on specific property name</returns>
        [HttpGet("total/{prop}")]
        public IDictionary<string, IExpenseResult> GetTotalPerGroup(string prop)
        {
            return service_.FetchTotalExpensesByProp(prop);
        }
        /// <summary>
        /// Returns a Groups of Expenses from a Specific Property Member
        /// </summary>
        /// <param name="prop">Property Member</param>
        /// <returns>List of Groups with Total Items and Group Code</returns>
        [HttpGet("group/{prop}")]
        public IDictionary<string, IExpenseGroup> GetExpensesGroup(string prop)
        {
            return service_.FetchExpensesGroupByProp(prop);
        }
        /// <summary>
        /// Returns a List of Expenses from a Specific Group
        /// </summary>
        /// <param name="groupCode">Group Code</param>
        /// <param name="groupProp">Group Property</param>
        /// <param name="page">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="orderBy">Order by property</param>
        /// <param name="orderDesc">Order by descending</param>
        /// <returns>Returns a list of Expenses</returns>
        [HttpGet("group/{groupProp}/{groupCode}")]
        public Pager<ExpenseDTO> GetExpensesFromGroup(
            float groupCode,
            string groupProp,
            [FromQuery(Name = "page")]int? page,
            [FromQuery(Name = "pageSize")]int? pageSize,
            [FromQuery(Name = "orderBy")]string orderBy,
            [FromQuery(Name = "orderDesc")] bool? orderDesc)
        {
            return TryPaginate(service_.FetchExpenseGroupItems(groupProp, groupCode, orderBy, orderDesc), page, pageSize);
        }
        /// <summary>
        /// Returns a List of Expenses from a Specific Group but with multiple Group Codes
        /// </summary>
        /// <param name="groupProp">Group Property</param>
        /// <param name="groupCodes">Group Codes</param>
        /// <param name="page">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="orderBy">Order by property</param>
        /// <param name="orderDesc">Order by descending</param>
        /// <returns></returns>
        [HttpPost("group/{groupProp}")]
        public Pager<ExpenseDTO> GetExpensesFromGroup(
            string groupProp,
            GroupCodes groupCodes,
            [FromQuery(Name = "page")]int? page,
            [FromQuery(Name = "pageSize")]int? pageSize,
            [FromQuery(Name = "orderBy")]string orderBy,
            [FromQuery(Name = "orderDesc")] bool? orderDesc)
        {
            if (groupCodes == null)
                return TryPaginate(new ExpenseDTO[0], page, pageSize);
            if (groupCodes.Items == null)
                return TryPaginate(new ExpenseDTO[0], page, pageSize);
            return TryPaginate(service_.FetchExpenseGroupItems(groupProp, groupCodes.Items, orderBy, orderDesc), page, pageSize);
        }
        /// <summary>
        /// Retrieve Enums used by prop of <see cref="ExpenseDTO"/>.
        /// </summary>
        /// <remarks>
        /// Sample Request
        /// 
        ///     GET /api/expenses/enums/orgao
        ///     
        /// </remarks>
        /// <param name="prop">Property Name</param>
        /// <param name="orderBy">Property that will Ordering</param>
        /// <param name="orderDesc">Order by descending</param>
        /// <returns>List of Properties</returns>
        [HttpGet("enums/{prop}")]
        public IReadOnlyList<IExpensePair> GetEnums(string prop, [FromQuery(Name = "orderBy")]string orderBy, [FromQuery(Name = "orderDesc")] bool? orderDesc)
        {
            return service_.FetchEnum(prop, orderBy, orderDesc);
        }
        /// <summary>
        /// Retrieve all Enum Properties
        /// </summary>
        /// <param name="orderDesc">Order by descending</param>
        /// <returns>List of Properties</returns>
        [HttpGet("enums/keys")]
        [HttpGet("group/keys")]
        public IDictionary<string, IEnumerable<string>> GetEnumKeys([FromQuery(Name = "desc")]bool? orderDesc)
        {
            return GetOrderKeyData(service_.FetchEnumKeys(orderDesc));
        }
    }
}
