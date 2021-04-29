using Library.Models;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Repositories
{
    public class LibraryRepository
    {
        private readonly IDependencyService _dependencyService;
        public List<Book> Books { get; private set; }

        public LibraryRepository() : this(new DependencyService())
        {

        }

        public LibraryRepository(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
        }

        public bool Add(Book book)
        {
           
            return true;
        }
    }
}
