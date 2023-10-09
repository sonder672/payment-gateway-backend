using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Senior.PaymentMicroservice
{
    public static class Program
    {
        private readonly static IConfigurationRoot _configuration;

        static Program()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.paymentGateway.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static List<PaymentCardAppSettings>? GetValue(string key)
        {
            int[] numbers = Enumerable.Range(0, 15).ToArray();

            var creditCards = numbers.Select(number => new PaymentCardAppSettings(
                number: _configuration[$"{key}:{number}:Number"]!,
                bin: _configuration[$"{key}:{number}:BIN"]!,
                new Expiration(year: Int16.Parse(_configuration[$"{key}:{number}:Expiration:Year"]!),
                                month: Int16.Parse(_configuration[$"{key}:{number}:Expiration:Month"]!)
            ))).ToList();

            if (creditCards is null || creditCards.Count < 1)
            {
                return null;
            }

            return creditCards;
        }
    }
}