namespace Assignment_1_G_92_2139.Services
{
    public class OrderService
    {
        public decimal CalculateOrderTotal(int quantity, decimal pricePerUnit)
        {
            if (quantity < 0 || pricePerUnit < 0)
                throw new ArgumentException("Quantity and price must be non-negative.");

            return quantity * pricePerUnit;
        }
    }
}