﻿using Microsoft.AspNetCore.Mvc;
using System;

namespace TodoRestAPI.Controllers
{
    /// <summary>
    ///  FileController class handles HTTP GET, POST, PUT, and DELETE requests.
    /// </summary>
    [ApiController]
	[Route("[controller]")]

	public class TodoController : ControllerBase
	{
        /// <summary>
        /// _logger is property of  Controller  that implements ILogger interface. 
		/// It is used for logging purpose,it is defined  as private and readonly
        /// </summary>
        private readonly ILogger<TodoController> _logger;

		public TodoController(ILogger<TodoController> logger)
		{
			_logger = logger;
		}

        /// <summary>
        /// Action for getting Item
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>
        [HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Todo>))]
        // IActionResult is an interface and return type
		// of this interface allows you to return an object of any type
        public IActionResult GetTodos()
		{
			return Ok(TodoRepository.getInstance().getItems());
		}


        /// <summary>
        /// Action for getting Item based on id
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>

        [HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(List<Todo>))]
		[ProducesResponseType(404)]

		public IActionResult GetTodo(int id)
		{
			Todo todo = TodoRepository.getInstance().GetItem(id);
			if (todo == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(todo);
			}

		}

        /// <summary>
        /// Action for creating Item
        /// </summary>
        /// <returns>action will return a 200 Ok status code when it runs successfully</returns>

        [HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]

		public IActionResult CreateTodo([FromBody] Todo todo)
		{
			if (todo == null)
			{
				return BadRequest("Todo is null");
			}

			bool result = TodoRepository.getInstance().addItem(todo);

			if (result)
			{
				return Ok("Successfully added");
			}
			else
			{
				return BadRequest("Todo not added");
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

		public IActionResult UpdateTodo(int id, [FromBody] Todo todo)
		{
			if (todo == null)
			{
				return BadRequest("Todo is null");
			}
			bool isUpdated = TodoRepository.getInstance().editItem(id, todo);

			if (!isUpdated)
			{
				return NotFound("No matching Todo");
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
		public IActionResult DeleteTodo(int id)
		{
			bool deleted = TodoRepository.getInstance().deleteItem(id);
			if (deleted)
			{
				return NotFound("No matching id");
			}
			else
			{
				return Ok("Todo deleted");
			}
		}

        /// <summary>
        /// Action for calculating max of amount
        /// </summary>
        /// <returns>returns max</returns>

        [HttpGet("DataAnalysis_Max")]
		[ProducesResponseType(200)]

		public IActionResult DoAnalysis_Max()
		{

			List<double> billAmounts = new List<double>();

			using (var reader = new StreamReader("C:\\Users\\Vruddhi\\Desktop\\ACS_567_HWK\\HWK2\\HWK2\\HWK2\\bills.txt"))
			{
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					var parts = line.Split(',');
					billAmounts.Add(double.Parse(parts[2]));
				}
			}
			var billAmount = billAmounts.Max();

			return Ok(billAmount);
		}

        /// <summary>
        /// Action for calculating min of amount
        /// </summary>
        /// <returns>returns min</returns>


        [HttpGet("DataAnalysis_Min")]
        [ProducesResponseType(200)]

        public IActionResult DoAnalysis_Min()
        {

            List<double> billAmounts = new List<double>();

            using (var reader = new StreamReader("C:\\Users\\Vruddhi\\Desktop\\ACS_567_HWK\\HWK2\\HWK2\\HWK2\\bills.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    billAmounts.Add(double.Parse(parts[2]));
                }
            }
            var billAmount = billAmounts.Min();

            return Ok(billAmount);
        }

    }



}
    