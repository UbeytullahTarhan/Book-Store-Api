﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBooks
{
    public class CreateBookCommand

    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookModel Model { get; set; }

        public CreateBookCommand(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()

        {
            var book = _dbContext.Books.SingleOrDefault(X => X.Title == Model.Title);
            if (book is not null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut");

            }
            book = _mapper.Map<Book>(Model);

     
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            

        }

    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreID { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}