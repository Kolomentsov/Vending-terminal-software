namespace CHW1.Models
{
    internal class Money
    {
        public Cash Cash { get; set; }

        public int Amount { get; set; }

        public Money()
        {
            Amount = 0;
            Cash = new Cash();
        }

        public Money(Cash cash, int amount)
        {
            Amount = amount;
            Cash = cash;
        }
    }

}
