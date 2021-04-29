using Library.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Validation
{
    public class ValidationService : IValidationService
    {
        public bool IsDigitsOnly(string input)
        {
            foreach(var c in input)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsValidISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn) ||
                isbn.Length != AppConstants.ISBN_LENGTH ||
                !IsDigitsOnly(isbn))
            {
                return false;
            } else
            {
                return true;
            }
        }

        public bool IsValidName(string name)
        {
            if (name.Length > AppConstants.BOOK_NAME_LENGTH ||
                string.IsNullOrWhiteSpace(name))
            {
                return false;
            } else
            {
                return true;
            }
        }

        public bool IsValidPrice(double price)
        {
            if (price < 0)
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}
