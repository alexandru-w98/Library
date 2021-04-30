using Library.Constants;
using Library.Models;
using Library.Services;
using Library.Services.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Repositories
{
    public class LibraryRepository
    {
        private readonly IDependencyService _dependencyService;
        public List<BookInfo> Books { get; private set; }
        public List<Loan> LendBooks { get; private set; }

        public LibraryRepository() : this(new DependencyService())
        {

        }

        public LibraryRepository(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;

            Books = new List<BookInfo>();
            LendBooks = new List<Loan>();
        }

        public bool Add(Book book)
        {
            if (_dependencyService.Get<IValidationService>().isValidBook(book))
            {
                var searchedIndex = Books.FindIndex(item => item.Book.ISBN == book.ISBN);
                if (searchedIndex == -1)
                {
                    Books.Add(new BookInfo { Book = book });
                } else
                {
                    Books[searchedIndex].Quantity++;
                }
                return true;
            } else
            {
                return false;
            }
        }
        
        public bool Lend(string isbn)
        {
            var searchedBookIndex = Books.FindIndex(item => item.Book.ISBN == isbn);

            if (searchedBookIndex != -1)
            {
                if ((Books[searchedBookIndex].Quantity -= 1) == 0)
                {
                    Books.Remove(Books[searchedBookIndex]);
                }

                var currentDate = DateTimeOffset.UtcNow;
                LendBooks.Add(new Loan
                {
                    ISBN = isbn,
                    LoanStartDate = currentDate.ToUnixTimeMilliseconds().ToString(),
                    LoanEndDate = currentDate.AddDays(AppConstants.MAX_LOAN_DAYS).ToUnixTimeMilliseconds().ToString()
                });
                return true;
            } else
            {
                return false;
            }
        }
    }
}
