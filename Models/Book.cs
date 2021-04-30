using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Book
    {
        public Book() { }
        public Book(string isbn, string name, double loanPrice)
        {
            this.ISBN = isbn;
            this.Name = name;
            this.LoanPrice = loanPrice;
        }

        public string ISBN { get; set; }
        public string Name { get; set; }
        public double LoanPrice { get; set; }
    }
}
