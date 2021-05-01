using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.BookServices
{
    public class BookApiService : IBookApiService
    {
        public IEnumerable<BookInfo> GetAllBooks()
        {
            return new List<BookInfo>
            {
                new BookInfo
                {
                    Book = new Book
                    {
                        Name = "Book1",
                        ISBN = "1234567890123"
                    },
                    Quantity = 3,
                    LoanPrice = 10
                },
                new BookInfo
                {
                    Book = new Book
                    {
                        Name = "Book2",
                        ISBN = "1234567390123"
                    },
                    Quantity = 2,
                    LoanPrice = 20
                },
                new BookInfo
                {
                    Book = new Book
                    {
                        Name = "Book3",
                        ISBN = "1234565890123"
                    },
                    Quantity = 1,
                    LoanPrice = 15
                }
            };
        }
    }
}
