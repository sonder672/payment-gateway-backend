using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Senior.PaymentMicroservice.Contracts;

namespace Senior.PaymentMicroservice.UseCases
{
    public class CheckOut : ICheckOut
    {
        private readonly IConfiguration _configuration;

        public CheckOut(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DTOs.OUT.Pay Pay(DTOs.IN.Pay paymentInformation)
        {
            var creditCards = Program.GetValue("CreditCard");

            if (creditCards is null)
            {
                throw new ArgumentNullException("Card number list from appsettings not found or there was an error");
            }

            var matchingCard = creditCards.FirstOrDefault(card => card.Number == paymentInformation.Number);

            if (matchingCard is null)
            {
                return new DTOs.OUT.Pay(
                    false,
                    "Wrong card number",
                    DTOs.OUT.StatusCodeEnum.NotFound
                );
            }

            var cardValidationResult = this.ValidateCard(paymentInformation, matchingCard);

            if (cardValidationResult != null)
            {
                return new DTOs.OUT.Pay(
                    false,
                    cardValidationResult,
                    DTOs.OUT.StatusCodeEnum.BadRequest
                );
            }

            return new DTOs.OUT.Pay(
                true,
                "Payment made",
                DTOs.OUT.StatusCodeEnum.OK
            );
        }

        private string? ValidateCard(DTOs.IN.Pay paymentInformation, PaymentCardAppSettings cardNumbers)
        {
            var errorMessages = new List<string>();

            if (paymentInformation.Bin != cardNumbers.BIN)
            {
                errorMessages.Add("Invalid BIN");
            }

            if (paymentInformation.ExpirationYear != cardNumbers.Expiration.Year
            || paymentInformation.ExpirationMonth != cardNumbers.Expiration.Month)
            {
                errorMessages.Add("invalid expiration date");
            }

            if (errorMessages.Count == 0)
            {
                return null;
            }

            return string.Join(", ", errorMessages);
        }
    }
}