using Microsoft.AspNetCore.Mvc;
using System;

namespace MonthlyBillRestAPI.Controllers
{
    /// <summary>
    ///  FileController class handles HTTP GET, POST, PUT, and DELETE requests.
    /// </summary>
    [ApiController]
	[Route("[controller]")]

	public class MonthlyBillController : ControllerBase
	{
        /// <summary>
        /// _logger is property of  Controller  that implements ILogger interface. 
		/// It is used for logging purpose,it is defined  as private and readonly
        /// </summary>
        private readonly ILogger<MonthlyBillController> _logger;

		public MonthlyBillController(ILogger<MonthlyBillController> logger)
		{
			_logger = logger;
		}

        /// <summary>
        /// Action for getting Item
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>
        [HttpGet]
		[ProducesResponseType(200, Type = typeof(List<MonthlyBill>))]
        // IActionResult is an interface and return type
		// of this interface allows you to return an object of any type
        public IActionResult GetBill()
		{
			return Ok(BillRepository.getInstance().getItems());
		}


        /// <summary>
        /// Action for getting Item based on id
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>

        [HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(List<MonthlyBill>))]
		[ProducesResponseType(404)]

		public IActionResult GetMonthlyBill(int id)
		{
            MonthlyBill get_data = BillRepository.getInstance().GetItem(id);
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
        /// Action for creating Item
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>

        [HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]

		public IActionResult CreateBill([FromBody] MonthlyBill bill)
		{
			if (bill == null)
			{
				return BadRequest("Bill is null");
			}

			bool result = BillRepository.getInstance().addItem(bill);

			if (result)
			{
				return Ok("Successfully added");
			}
			else
			{
				return BadRequest("Bill not added");
			}
		}

        /// <summary>
        /// Action for updating Item
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>

        [HttpPut("{id}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]

		public IActionResult UpdateBill(int id, [FromBody] MonthlyBill updated)
		{
			if (updated == null)
			{
				return BadRequest("Bill is null");
			}
			bool isUpdated = BillRepository.getInstance().editItem(id, updated);

			if (!isUpdated)
			{
				return NotFound("No matching Bill");
			}
			else
			{
				return Ok("Successfully updated");
			}
		}

        /// <summary>
        /// Action for deleting Item
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>

        [HttpDelete("{id}")]
		public IActionResult DeleteBill(int id)
		{
			bool deleted = BillRepository.getInstance().deleteItem(id);
			if (deleted)
			{
				return NotFound("No matching id");
			}
			else
			{
				return Ok("Bill deleted");
			}
		}

        /// <summary>
        /// Action for calculating max, min, mean of amount
        /// </summary>
        /// <returns>returns max, min, mean</returns>


        [HttpGet("DataAnalysis")]
        [ProducesResponseType(200)]
        public IActionResult GetMinMaxMean(string filePath)
        {
            var (min, max, mean) = BillRepository.GetMinMaxMean(filePath);
            return Ok(new { min, max, mean });
        }
    }



}
    