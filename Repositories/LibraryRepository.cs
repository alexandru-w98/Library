using Library.Constants;
using Library.Models;
using Library.Services;
using Library.Services.BookServices;
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
        public List<BookInfo> AvailableBooks { get; private set; }
        public List<Loan> LendBooks { get; private set; }

        public LibraryRepository(List<BookInfo> availableBooks, IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
            AvailableBooks = availableBooks;
            LendBooks = new List<Loan>();
        }
        public LibraryRepository(List<Loan> lendBooks, IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
            LendBooks = lendBooks;

            Initialize();
        }
        public LibraryRepository(List<BookInfo> books, List<Loan> lendBooks, IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
            AvailableBooks = books;
            LendBooks = lendBooks;
        }
        public LibraryRepository(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
            LendBooks = new List<Loan>();

            Initialize();
        }

        private void Initialize()
        {
            try
            {
                AvailableBooks = new List<BookInfo>(_dependencyService.Get<IBookApiService>().GetAllBooks());
            } catch(Exception e)
            {
                AvailableBooks = new List<BookInfo>();
            }
        }

        public bool AddNewBook(Book book, double price)
        {
            var validationService = _dependencyService.Get<IValidationService>();

            if (validationService.isValidBook(book) &&
                validationService.IsValidPrice(price))
            {
                var searchedIndex = AvailableBooks.FindIndex(item => item.Book.ISBN == book.ISBN);
                if (searchedIndex == -1)
                {
                    AvailableBooks.Add(new BookInfo { Book = book, LoanPrice = price });

                    return true;
                }
            }

            return false;
        }

        public bool AddExistingBook(string isbn, int quantity = 1)
        {
            var bookIndex = AvailableBooks.FindIndex(item => item.Book.ISBN == isbn);

            if ((bookIndex != -1) &&
                _dependencyService.Get<IValidationService>().IsValidQuantity(quantity))
            {
                AvailableBooks[bookIndex].Quantity += quantity;
                return true;
            }

            return false;
        }

        public bool Lend(string isbn, string cnp)
        {
            var searchedBookIndex = AvailableBooks.FindIndex(item => item.Book.ISBN == isbn);

            if (searchedBookIndex != -1 &&
                AvailableBooks[searchedBookIndex].Quantity > 0 &&
                _dependencyService.Get<IValidationService>().IsValidCNP(cnp))
            {
                AvailableBooks[searchedBookIndex].Quantity--;

                var currentDate = DateTimeOffset.UtcNow;
                LendBooks.Add(new Loan
                {
                    CNP = cnp,
                    ISBN = isbn,
                    LoanPrice = AvailableBooks[searchedBookIndex].LoanPrice,
                    LoanStartDate = currentDate.ToUnixTimeMilliseconds().ToString(),
                    LoanEndDate = currentDate.AddDays(AppConstants.MAX_LOAN_DAYS).ToUnixTimeMilliseconds().ToString()
                });
                return true;
            } else
            {
                return false;
            }
        }

        public bool ReturnBook(string isbn, string cnp)
        {
            var searchedBookIndex = LendBooks.FindIndex(item => item.CNP == cnp && item.ISBN == isbn);

            if (searchedBookIndex != -1)
            {
                if (AddExistingBook(isbn))
                {
                    LendBooks[searchedBookIndex].HasBeenReturned = true;

                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }

        public void DisplayAvailableBooks()
        {
            foreach(var bookInfo in AvailableBooks)
            {
                Console.WriteLine("ISBN: " + bookInfo.Book.ISBN +
                    " Nume: " + bookInfo.Book.Name + 
                    " Quantity: " + bookInfo.Quantity +
                    " Loan Price: " + bookInfo.LoanPrice);
            }
        }
        
        public void DisplayLendBooks()
        {
            string result = "";
            foreach (var loan in LendBooks)
            {
                result += "CNP: " + loan.CNP +
                    " Loan Start: " + loan.LoanStartDate +
                    " Loan Duration: " + loan.LoanEndDate +
                    " Loan Price: " + loan.LoanPrice;

                result += loan.HasBeenReturned ? " Status: Returned" : " Status: Not Returned";
            }

            Console.WriteLine(result);
        }

        public int GetAvailableQuantity(string isbn)
        {
            var searchedBook = AvailableBooks.FirstOrDefault(bookInfo => bookInfo.Book.ISBN == isbn);

            if (searchedBook != null)
            {
                return searchedBook.Quantity;
            } else
            {
                return -1;
            }
        }

        public void CalculateBorrowPenalty()
        {
            var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            foreach (var loan in LendBooks)
            {
                try
                {
                    if (long.Parse(loan.LoanEndDate) <= long.Parse(currentTimestamp))
                    {
                        loan.LoanPrice += AppConstants.LOAN_PENALTY * loan.LoanPrice;
                    }
                } catch (Exception e)
                {
                    //
                }
            }
        }
    }
}
