using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Validation
{
    public class ValidationService : IValidationService
    {
        public bool IsValidISBN(string isbn)
        {
            return true;
        }

        public bool IsValidName(string name)
        {
            return true;
        }

        public bool IsValidPrice(double price)
        {
            return true;
        }
    }
}
