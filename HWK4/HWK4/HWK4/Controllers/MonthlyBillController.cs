
using HWK4.Interfaces;
using HWK4.Models;
using HWK4.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MonthlyBillRestAPI.Controllers
{
    /// <summary>
    ///  MonthlyBillController class handles HTTP GET, POST, PUT, and DELETE requests.
    /// </summary>
    /// 
    [ApiController]
    [Route("[controller]")]

    public class MonthlyBillController : ControllerBase
    {
        /// <summary>
        /// _logger is property of  Controller  that implements ILogger interface. 
		/// It is used for logging purpose,it is defined  as private and readonly
        /// </summary>

        private readonly ILogger<MonthlyBillController> _logger;
        private readonly IBillRepository _monthlyBill;

        public MonthlyBillController(ILogger<MonthlyBillController> logger, IBillRepository monthlyBill)
        {
            _logger = logger;
            _monthlyBill = monthlyBill;
        }



        /// <summary>
        /// Action for getting Item
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<MonthlyBill>))]

        public IActionResult getItems()
        {
            _logger.Log(LogLevel.Information, "Get Items");
            return Ok(_monthlyBill.getItems());
        }


        /// <summary>
        /// Action for getting Item based on id
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(MonthlyBill))]
        [ProducesResponseType(404)]

        public IActionResult GetItem(int id)
        {
            MonthlyBill get_data = _monthlyBill.GetItem(id);
            if (get_data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(get_data);
            }

        }

        /// <summary>
        /// Action for checking if the Bill exists
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>

        [HttpGet("MonthlyBillExists")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult MonthlyBillExists(int id)
        {
            bool result = _monthlyBill.MonthlyBillExists(id);
            
            if (!result)
            {
                return NotFound("No matching id");
            }
            else
            {
                return Ok("Bill exists");
            }
        }


        /// <summary>
        /// Action for creating Item
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult addItem([FromBody] MonthlyBill bill)
        {
            if (bill == null)
            {
                return BadRequest("Bill is null");
            }

            bool result = _monthlyBill.addItem(bill);
            return result ? Ok(result) : BadRequest();
        }


        /// <summary>
        /// Action for updating Item
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>

        [HttpPut()]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public IActionResult editItem( [FromBody] MonthlyBill updated)
        {
            if (updated == null)
            {
                return BadRequest("Bill is null");
            }
            MonthlyBill result = _monthlyBill.GetItem(updated.Id);

            if (updated == null)
            {
                return BadRequest("Bill not found");
            }
            result.Provider = updated.Provider;
            result.Bill = updated.Bill;
            result.Amount = updated.Amount;
            result.IsCompleted = updated.IsCompleted;

            bool isUpdated = _monthlyBill.editItem(result);
            return isUpdated ? Ok(result) : BadRequest();
        }


        /// <summary>
        /// Action for deleting Item
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>

        [HttpDelete("{id}")]
        public IActionResult DeleteBill(int id)
        {
            bool result = _monthlyBill.deleteItem(id);

            return result ? Ok(result) : BadRequest();
        }




        /// <summary>
        /// Action for calculating max, min, average of amount
        /// </summary>
        /// <returns>returns max, min, average</returns>

        [HttpGet("DataAnalysis")]
        [ProducesResponseType(200)]
        public IActionResult GetDataAnalysis()
        {
            _logger.Log(LogLevel.Information, "Get analysis");
            return Ok(_monthlyBill.DataAnalysis());
        }

    }



}
