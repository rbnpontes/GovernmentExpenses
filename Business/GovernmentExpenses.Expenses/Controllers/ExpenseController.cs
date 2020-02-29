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
            return RoutersDesc;
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
        public Dictionary<string, IEnumerable<string>> GetEnumKeys([FromQuery(Name = "desc")]bool? orderDesc)
        {
            return new Dictionary<string, IEnumerable<string>>
            {
                {"items", service_.FetchEnumKeys(orderDesc)}
            };
        }
    }
}
