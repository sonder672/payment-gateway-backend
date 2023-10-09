using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Services.Helper
{
    public static class UUID
    {
        public static string Generate()
        {
            Guid uniqueId = Guid.NewGuid();

            return uniqueId.ToString();
        }
    }
}