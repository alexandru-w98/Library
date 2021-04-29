using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Validation
{
    public interface IValidationService
    {
        bool IsValidName(string name);
        bool IsValidISBN(string isbn);
        bool IsValidPrice(double price);
    }
}
