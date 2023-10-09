using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.PaymentMicroservice.DTOs.IN
{
    public class Pay
    {
        public Pay(short expirationYear, short expirationMonth, string number, string bin)
        {
            ExpirationMonth = expirationMonth;
            ExpirationYear = expirationYear;
            Number = number;
            Bin = bin;
        }

        [Required(ErrorMessage = "El campo ExpirationDay es obligatorio.")]
        public short ExpirationYear { get; }

        [Required(ErrorMessage = "El campo ExpirationMonth es obligatorio.")]
        [Range(1, 12, ErrorMessage = "ExpirationMonth debe estar entre 1 y 12.")]
        public short ExpirationMonth { get; }

        [Required(ErrorMessage = "El campo Number es obligatorio.")]
        [MaxLength(50, ErrorMessage = "Number no puede tener más de 50 caracteres.")]
        public string Number { get; }

        [Required(ErrorMessage = "El campo Bin es obligatorio.")]
        [MaxLength(10, ErrorMessage = "Bin no puede tener más de 10 caracteres.")]
        public string Bin { get; }
    }
}