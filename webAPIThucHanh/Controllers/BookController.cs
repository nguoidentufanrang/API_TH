﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webAPIThucHanh.Data;
using webAPIThucHanh.Models.DTO;
using webAPIThucHanh.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace webAPIThucHanh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IBookRepository _bookRepository;
		private readonly ILogger<BooksController> _logger;
		public BooksController(AppDbContext dbContext, IBookRepository bookRepository, ILogger<BooksController> logger)
        {
            _dbContext = dbContext;
            _bookRepository = bookRepository;
			_logger = logger;
		}
		

		[HttpGet("get-all-books")]
		[Authorize(Roles = "Read")]
		public IActionResult GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
			[FromQuery] string? sortBy, [FromQuery] bool isAscending,
			[FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)

		{
			_logger.LogInformation("GetAll Book Action method was invoked");
			_logger.LogWarning("This is a warning log");
			_logger.LogError("This is a error log");
			// su dung reposity pattern  
			var allBooks = _bookRepository.GetAllBooks(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
			_logger.LogInformation($"Finished GetAllBook request with data { JsonSerializer.Serialize(allBooks)}"); 
			return Ok(allBooks);
		}

		[HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var bookWithIdDTO = _bookRepository.GetBookById(id);
            return Ok(bookWithIdDTO);
        }
		private bool ValidateAddBook(AddBookRequestDTO addBookRequestDTO)
		{
			if (addBookRequestDTO == null)
			{
				ModelState.AddModelError(nameof(addBookRequestDTO), "Please add book data");
				return false;
			}

			if (string.IsNullOrEmpty(addBookRequestDTO.Description))
			{
				ModelState.AddModelError(nameof(addBookRequestDTO.Description), "Description cannot be null");
			}

			if (addBookRequestDTO.Rate < 0 || addBookRequestDTO.Rate > 5)
			{
				ModelState.AddModelError(nameof(addBookRequestDTO.Rate), "Rate cannot be less than 0 and more than 5");
			}

			return ModelState.IsValid;
		}
		[HttpPost("add-book")]
		[Authorize(Roles = "Write")]
		public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
		{
			if (!ValidateAddBook(addBookRequestDTO))
			{
				return BadRequest(ModelState);
			}
			if (ModelState.IsValid)
			{
				// Thực hiện thêm book vào DB
				var bookAdd = _bookRepository.AddBook(addBookRequestDTO);
				return Ok(bookAdd);
			}
			else
			{
				return BadRequest(ModelState);
			}
            
		}

		[HttpPut("update-book-by-id/{id}")]
		[Authorize(Roles = "Write")]
		public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
        {
            var updateBook = _bookRepository.UpdateBookById(id, bookDTO);
            return Ok(updateBook);
        }
        [HttpDelete("delete-book-by-id/{id}")]
		[Authorize(Roles = "Write")]
		public IActionResult DeleteBookById(int id)
        {
            var deleteBook = _bookRepository.DeleteBookById(id);
            return Ok(deleteBook);
        }
    }
}
