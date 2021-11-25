using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbcontext,IMapper mapper)
        {
            _mapper = mapper;
            _dbcontext = dbcontext;

        }
        public BookDetailViewModel Handle()
        {
            var book = _dbcontext.Books.Where(book => book.Id == BookId).SingleOrDefault();
            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
                
            }
            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
            vm.Title = book.Title;
            
           return vm;
        }

    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublicDate { get; set; }
    }
}
