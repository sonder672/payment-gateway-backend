using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.PaymentMicroservice.Contracts
{
    public interface ICheckOut
    {
        public DTOs.OUT.Pay Pay(DTOs.IN.Pay paymentInformation);
    }
}