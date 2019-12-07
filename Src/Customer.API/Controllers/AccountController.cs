using CustomerApi.API.Controllers;
using CustomerApi.Domain.Commands;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        

        public AccountController(ILogger<AccountController> logger,IMediator mediator) : base(mediator)
        {
            _logger = logger;            
        }


        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]        
        public async Task<ActionResult<List<CustomerDto>>> GetAllAccountsAsync()
        {
            var accountsResults = await QueryAsync(new GetAllAccountsQuery());            
            return Ok(accountsResults);
        }


        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="command">Info of customer</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateAccountAsync([FromBody] CreateAccountCommand command)
        {
            return Ok(await CommandAsync(command));
        }

    }
}