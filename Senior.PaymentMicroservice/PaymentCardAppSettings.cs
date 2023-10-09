using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.PaymentMicroservice
{
    public class PaymentCardAppSettings
    {
        public PaymentCardAppSettings(string number, string bin, Expiration expiration)
        {
            Number = number;
            BIN = bin;
            Expiration = expiration;
        }

        public string Number { get; }
        public string BIN { get; }
        public Expiration Expiration { get; }
    }

    public class Expiration
    {
        public Expiration(short year, short month)
        {
            Year = year;
            Month = month;
        }

        public short Year { get; }
        public short Month { get; }
    }
}