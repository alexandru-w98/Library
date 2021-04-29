using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Loan
    {
        public string ISBN { get; set; }
        public int LoanStartDate { get; set; }
        public int LoanEndDate { get; set; }
    }
}
