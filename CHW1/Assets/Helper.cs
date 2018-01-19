using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CHW1.Models;
using Newtonsoft.Json;

namespace CHW1.Assets
{
    internal class Helper
    {
        internal Terminal Data { get; private set; }

        internal void InitApp()
        {
            if (IsDataFileExist())
                LoadDatum();
            else
                CreateDefaultDatum();
        }

        internal void SaveDatum()
        {
            using (var file = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "/Datum.json"))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, Data);
            }
        }

        internal void InsertMoney(Cash cash)
        {
            Data.UserMoney += cash.Value;

            var money = Data.VendingMoney.FirstOrDefault(x => x.Cash.Id == cash.Id);

            if (money == null)
                Data.VendingMoney = Data.VendingMoney.Concat(new[] { new Money(cash, 0) });

            money.Amount++;
        }
        internal void InsertMoney(Guid cashId)
        {
            var cash = Data.Cashes.FirstOrDefault(x => x.Id == cashId);

            if (cash == null)
                throw new ApplicationException($"Не найдена валюта с ID {cashId}");

           InsertMoney(cash);
        }

        internal void InsertMoney(int value)
        {
            var cash = Data.Cashes.FirstOrDefault(x => x.Value == value);

            if (cash == null)
                throw new ApplicationException($"Не найдена валюта со значение {value}");

            InsertMoney(cash);
        }

        internal void ChooseItem(Item item)
        {
            if (item.Amount == 0)
                throw new ApplicationException($"Недостаточное количество товара с ID {item.Id}");

            if (item.Price > Data.UserMoney)
                throw new ApplicationException($"Недостаточно средств для покупки товара \"{item.Name}\"");

            Data.UserMoney -= item.Price;

            item.Amount--;
        }

        internal void ChooseItem(Guid itemId)
        {
            var item = Data.Items.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
                throw new ApplicationException($"Не найден товар с ID {itemId}");

            ChooseItem(item);
        }

        internal void ChooseItem(string name)
        {
            var item = Data.Items.FirstOrDefault(x => x.Name == name);

            if (item == null)
                throw new ApplicationException($"Не найден товар с именем {name}");

            ChooseItem(item);
        }

        internal void GiveChange()
        {
            var moneys = Data.VendingMoney.Where(x => x.Amount > 0 && x.Cash.IsCoin).OrderBy(x => x.Cash.Value);

            foreach (var money in moneys)
            {
                var count = Data.UserMoney / money.Cash.Value;

                Data.UserMoney -= money.Cash.Value * count;

                money.Amount -= count;
            }

            if (Data.UserMoney > 0)
                throw new ApplicationException("Пожалуйста, выберите еще товар - сдачи нет");
        }

        #region PRIVATE

        private static bool IsDataFileExist()
        {
            return File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Datum.json");
        }

        private void LoadDatum()
        {
            try
            {
                using (var file = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "/Datum.json"))
                {
                    var serializer = new JsonSerializer();
                    Data = (Terminal)serializer.Deserialize(file, typeof(Terminal));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("При попытки чтения данных произошла ошибка: ", ex);
            }
        }

        private void CreateDefaultDatum()
        {
            var cashes = new List<Cash>
            {
                new Cash(1, true),
                new Cash(2, true),
                new Cash(5,true),
                new Cash(10, true),
                new Cash(50, false),
                new Cash(100, false),
                new Cash(500, false),
                new Cash(1000, false)
            };

            var terminal = new Terminal
            {
                Items = new List<Item>
                {
                    new Item("Твикс", 15, 30),
                    new Item("Сникерс", 17, 20),
                    new Item("Кока-кола", 20, 13),
                    new Item("Печенье", 3, 27),
                    new Item("Вода", 5, 23),
                    new Item("Орешки", 15, 15),
                    new Item("Шоколад", 12, 14),
                    new Item("Спрайт", 19, 18),
                    new Item("Сендвич", 30, 3),
                    new Item("Поп-корн", 21, 16)
                },
                VendingMoney = GenerateRandomMoney(cashes),
                Cashes = cashes,
                UserMoney = 0
            };

            using (var file = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "/Datum.json"))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, terminal);
            }

            Data = terminal;

        }

        private static IEnumerable<Money> GenerateRandomMoney(IEnumerable<Cash> cashes)
        {
            var rnd = new Random();

            return cashes.Select(cash => new Money(cash, rnd.Next(1, 31)));
        }

        #endregion

    }
}
