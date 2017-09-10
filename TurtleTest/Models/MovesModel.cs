using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleTest
{
    public class MovesModel
    {
        public List<Sequence> Sequences { get; set; }
    }

    public class Sequence
    {
        public List<char> Moves { get; set; }
    }
}
