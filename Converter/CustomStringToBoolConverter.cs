using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modulum.Client.Infrastructure.Converter
{
    public class CustomStringToBoolConverter : BoolConverter<string>
    {
        public CustomStringToBoolConverter()
        {
            SetFunc = OnSet;
            GetFunc = OnGet;
        }

        public const string TrueString = "1";
        public const string FalseString = "0";
        public const string NullString = "0";

        private string OnGet(bool? value) => value == null ? NullString : (value == true ? TrueString : FalseString);

        private bool? OnSet(string arg)
        {
            try
            {
                if (arg == TrueString)
                    return true;
                if (arg == FalseString)
                    return false;
                return null;
            }
            catch (FormatException e)
            {
                UpdateSetError("Conversion error: " + e.Message);
                return null;
            }
        }
    }
}
