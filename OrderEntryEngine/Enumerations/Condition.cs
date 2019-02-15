using OrderEntrySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderEntryEngine
{
    public enum Condition
    {
        [EntityDescription("Double Plus UnGood")]
        Poor,
        [EntityDescription("Just Average")]
        Average,
        [EntityDescription("Double Plus Good")]
        Excellent
    }
}