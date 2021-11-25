using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations{
    public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            // Look for any book.
            if (context.Books.Any())
            {
                return;   // Data was already seeded
            }

            context.Books.AddRange(
               new Book()
               {
                   Title = "Lean Startup",
                   GenreId = 1,//(int)GenreEnum.PersonalGrowth, // Personal Growth
                   PageCount = 200,
                   PublishDate = new DateTime(2001, 06, 12)
               });

            context.SaveChanges();
        }
    }
}
}