using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBooks;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers{
    [ApiController]
    [Route("[Controller]s")]
    public class BookController : ControllerBase{
        /*private static List<Book> BookList=new List<Book>(){
            
            new Book{
                Id=1,
                Title="LeonStartup",
                GenreId=1,//Personal Growth
                PageCount=200,
                PublishDate=new DateTime(2001,6,12)
                
            },
            
            new Book{
                Id=2,
                Title="Herland",
                GenreId=2,//Science Fiction
                PageCount=250,
                PublishDate=new DateTime(2010,5,23)
                
            },
            new Book{
                Id=3,
                Title="Dune",
                GenreId=2,//Personal Growth
                PageCount=540,
                PublishDate=new DateTime(2002,5,23)
                
            }
            
        };*/
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context,IMapper mapper){
            _context=context;
            _mapper = mapper;

        }
        [HttpGet]
        
        public IActionResult GetBooks(){
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.BookId = id;
                result=query.Handle();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                /*if (!result.IsValid)
                {
                    foreach (var item in result.Errors)
                    {
                        Console.WriteLine("Özellik " + item.PropertyName+" Error Message :"+item.ErrorMessage);
                    }
                }
                else
                {
                    
                }*/


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
            return Ok();



        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id ,[FromBody] UpdateBookModel updatedBook){
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            

            return Ok();

        }
        [HttpDelete]
        public IActionResult DeleteBook([FromBody] int id){
            DeleteBookCommand command = new DeleteBookCommand(_context);
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            try
            {
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
        
    
        /*[HttpGet]
        public Book Get([FromQuery] int id){
            var book=BookList.Where(book=> book.Id==id).SingleOrDefault();
            return book;
        }*/
        


    }
    
}