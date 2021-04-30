using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.BookServices
{
    public interface IBookApiService
    {
        IEnumerable<BookInfo> GetAllBooks();
    }
}
