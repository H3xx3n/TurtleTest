using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleTest
{
    public class GameSettingsModel
    {
        public Point Size { get; set; }
        public Direction StartDirection { get; set; }
        public Point StartPoint { get; set; }
        public Point Exit { get; set; }
        public List<Point> Mines { get; set; }
    }

    public enum Direction
    {
        North = 0, East = 1, South = 3, West = 4
    }
}
