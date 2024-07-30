namespace BookStore.Domain.Validator
{
    public class BookValidator
    {
        public bool isBookPriceValid(decimal price)
        {
            return price > 0;
        }

        public bool isBookQuantityValid(int pieces)
        {
            return pieces > 0;
        }
    }
}
