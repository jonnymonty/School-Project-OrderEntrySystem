using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class EntityColumnAttribute : Attribute
    {
        public EntityColumnAttribute(int sequence, int width)
            : this(sequence, string.Empty, width)
        {
        }

        public EntityColumnAttribute(int width, string description, int sequence)
        {
            this.Description = description;
            this.Width = width;
            this.Sequence = sequence;
        }

        public string Description { get; set; }

        public int Width { get; set; }

        public int Sequence { get; set; }
    }
}
