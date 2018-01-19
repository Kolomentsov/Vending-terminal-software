using System;

namespace CHW1.Models
{
    internal class Cash
    {
        public Guid Id { get; set; }

        public int Value { get; set; }

        public bool IsCoin { get; set; }

        internal Cash()
        {
            Id = Guid.Empty;
            Value = 0;
            IsCoin = false;
        }

        internal Cash(Guid id, int value, bool isCoin)
        {
            Id = id;
            Value = value;
            IsCoin = isCoin;
        }

        internal Cash(int value, bool isCoin)
        {
            Id = Guid.NewGuid();
            Value = value;
            IsCoin = isCoin;
        }
    }
}
