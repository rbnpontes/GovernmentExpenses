using GovernmentExpenses.Expenses.Entities;
using GovernmentExpenses.Expenses.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Retrieve a Basic Description from Routes to this Controller
        /// </summary>
        /// <remarks>
        /// Sample Request
        ///     
        ///     GET /api/expenses
        ///   
        /// </remarks>
        /// <returns>Dictionary</returns>
        [HttpGet]
        public object Get()
        {
        }
        [HttpGet("desc")]
        public object GetDesc()
        {
            return RoutersDesc;
        }
        [HttpGet("order-keys")]
        public IDictionary<string, IEnumerable<string>> GetExpensesOrderKeys([FromQuery(Name ="orderDesc")]bool? desc)
        {
            return GetOrderKeyData(service_.FetchExpensesOrderKeys(desc));
        }
        [HttpGet("total")]
        public IExpenseResult GetTotalExpenses()
        {
            return service_.FetchTotalExpenses();
        }
        [HttpGet("total/{prop}")]
        public IDictionary<string,IExpenseResult> GetTotalPerGroup(string prop)
        {
            return service_.FetchTotalExpensesByProp(prop);
        }
        [HttpGet("total/{prop}/keys")]
        public IDictionary<string, IEnumerable<string>> GetTotalPerGroupKeys([FromQuery(Name = "orderDesc")]bool? desc)
        {
            return GetOrderKeyData(service_.FetchExpensesKeys(desc));
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
        /// <returns></returns>
        [HttpGet("enums/{prop}")]
        public IReadOnlyList<IExpensePair> GetEnums(string prop, [FromQuery(Name = "orderBy")]string orderBy, [FromQuery(Name ="orderDesc")] bool? orderDesc)
        {
            return service_.FetchEnum(prop, orderBy, orderDesc);
        }
        [HttpGet("enums/order-keys")]
        public IDictionary<string, IEnumerable<string>> GetEnumsOrderKeys()
        {
            return GetOrderKeyData(new string[] { "code", "name" });
        }
        /// <summary>
        /// Retrieve all Enum Properties
        /// </summary>
        /// <remarks>
        /// Sample Request
        /// 
        ///     GET /api/expenses/enums/keys?desc=true
        ///     
        /// </remarks>
        /// <param name="orderDesc">Order by descending</param>
        /// <returns></returns>
        [HttpGet("enums/keys")]
        public IDictionary<string, IEnumerable<string>> GetEnumKeys([FromQuery(Name = "desc")]bool? orderDesc)
        {
            return GetOrderKeyData(service_.FetchEnumKeys(orderDesc));
        }
    }
}
