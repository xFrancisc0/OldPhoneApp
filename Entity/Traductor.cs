using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldPhoneApp.Entity
{
    public class TupleClass
    {
        public required string key { get; set; }
        public required List<string> values { get; set; }
    }
    public class Traductor
    {
        public required TupleClass[] dictionary { get; set; }
    }
}
