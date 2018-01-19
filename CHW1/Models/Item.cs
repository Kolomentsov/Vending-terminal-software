using System;

namespace CHW1.Models
{
    /// <summary>
    /// Продаваемый объект
    /// </summary>
    internal class Item
    {
        /// <summary>
        /// Уникальный Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public int Amount { get; set; }

        internal Item()
        {
            Id = Guid.Empty; // 0000
            Name = string.Empty; //null
            Price = 0;
            Amount = 0;
        }

        internal Item(Guid id, string name, int price, int amount)
        {
            Id = id;
            Name = name;
            Price = price;
            Amount = amount;
        }

        internal Item(string name, int price, int amount)    // создать новый предмет 
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Amount = amount;
        }
    }
}