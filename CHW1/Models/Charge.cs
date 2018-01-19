using System.Collections.Generic;

namespace CHW1.Models
{
    internal class Charge
    {
        public IEnumerable<Cash> Cashes;

        public Charge()
        {
            Cashes = new List<Cash>();

        }

        public Charge(IEnumerable<Cash> cashes)
        {
            Cashes = cashes;
        }
    }
}
