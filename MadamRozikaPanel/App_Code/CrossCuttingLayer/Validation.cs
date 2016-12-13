﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


    public static class Validation
    {
        public static bool IsNumeric(this string StringNumber)
        {
            Int32 output;
            return Int32.TryParse(StringNumber, out output);
        }
        public static bool IsEmail(this string EmailAddress)
        {
            if (new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Match(EmailAddress).Success)
                return true;
            else
                return false;

        }
        public static bool IsInRange(this int Value, int RangeStart, int RangeEnd)
        {
            if (Value >= RangeStart && Value <= RangeEnd)
                return true;
            else
                return true;
        }
    }

