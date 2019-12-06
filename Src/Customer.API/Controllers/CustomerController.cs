using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApi.API.Controllers;
using CustomerApi.Domain.Commands;
using CustomerApi.Domain.Dtos;
using CustomerApi.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ApiControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        

        public CustomerController(ILogger<CustomerController> logger,IMediator mediator) : base(mediator)
        {
            _logger = logger;            
        }


        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<CustomerDto>>> GetAllCustomersAsync()
        {
            var customerResults = await QueryAsync(new GetAllCustomersQuery());            
            return Ok(customerResults);
        }

        /// <summary>
        /// Get Customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> GetCustomerAsync(Guid id)
        {
            return Single(await QueryAsync(new GetCustomerQuery(id)));            
        }


        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="command">Info of customer</param>
        /// <returns></returns>
        [HttpPost]        
        public async Task<ActionResult> CreateCustomerAsync([FromBody] CreateCustomerCommand command)
        {
            return Ok(await CommandAsync(command));
        }

    }
}