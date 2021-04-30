using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Loan
    {
        public string ISBN { get; set; }
        public string LoanStartDate { get; set; }
        public string LoanEndDate { get; set; }
        public bool HasBeenReturned { get; set; } = false;
    }
}
