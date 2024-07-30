using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Validator
{
    public class OrderValidator
    {
        public bool IsOrderQuantityPositiveNumber(int requestedQuantity)
        {
            return requestedQuantity > 0;
        }
        public bool IsOrderQuantityValid(int pieces, int requestedQuantity, int oldQuantity)
        {
            return pieces >= requestedQuantity || requestedQuantity<=oldQuantity;
        }

    }
}
