using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
            //RuleFor(command => command.Model.PageCount).GreaterThan(0);
            //RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            //RuleFor(command => command.Model.Title).NotEmpty().MaximumLength(4);
        }
    }
}
