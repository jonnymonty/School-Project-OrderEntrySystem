using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntrySystem
{
    public class EntityControlAttribute : Attribute
    {
        public EntityControlAttribute(ControlType controlType, int sequence)
        {
            this.ControlType = controlType;
            this.Sequence = sequence;
        }

        public EntityControlAttribute(ControlType controlType, string description, int sequence)
            : this(controlType, sequence)
        {
            this.Description = description;
        }

        public ControlType ControlType { get; set; }

        public string Description { get; set; }

        public int Sequence { get; set; }
    }
}
