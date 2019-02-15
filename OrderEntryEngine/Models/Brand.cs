using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    [Serializable]
    public class Brand : ILookupEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public virtual ICollection<Bike> Bikes { get; set; }
    }
}