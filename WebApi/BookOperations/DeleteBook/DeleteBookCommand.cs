using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private BookStoreDbContext _context;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            _context.Books.Remove(book);
            _context.SaveChanges();

        }

    }
}
