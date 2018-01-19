using System.Collections.Generic;

namespace CHW1.Models
{
    internal class Terminal
    {
        public IEnumerable<Item> Items { get; set; }

        public IEnumerable<Money> VendingMoney { get; set; }

        public  int UserMoney { get; set; }

        public IEnumerable<Cash> Cashes { get; set; }

        internal Terminal()
        {
            Items = new List<Item>();
            VendingMoney = new List<Money>();
            UserMoney= 0;
            Cashes = new List<Cash>();
        }

        internal Terminal(IEnumerable<Item> items, IEnumerable<Money> vendingMoney, int usermoney, IEnumerable<Cash> cashes )
        {
            Items = items;
            VendingMoney = vendingMoney;
            UserMoney = usermoney;
            Cashes = cashes;
        }

       
    } 
}
