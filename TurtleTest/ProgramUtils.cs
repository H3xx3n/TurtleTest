using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleTest
{
    public static class ProgramUtils
    {
        public static void WriteExampleFiles()
        {
            var gameSettings = new GameSettingsModel
            {
                Size = new Point(4, 3),
                StartPoint = new Point(0, 1),
                StartDirection = Direction.North,
                Exit = new Point(4, 2),
                Mines = new List<Point>
                {
                    new Point(1,1),
                    new Point(3,1),
                    new Point(3,3)
                }
            };

            var moves = new MovesModel
            {
                Sequences = new List<Sequence>{
                   new Sequence{Moves = new List<char> { 'm', 'r', 'm', 'm', 'm', 'm', 'r', 'm', 'm' }}, //0: Success
                   new Sequence{Moves = new List<char> { 'm', 'r', 'm', 'm'}}, //1: Still in danger
                   new Sequence{Moves = new List<char> { 'r','m', 'r', 'm', 'm', 'm', 'm', 'r', 'm', 'm' }}, //2: Mine hit
                   new Sequence{Moves = new List<char> { 'm', 'r', 'm', 'm', 'm', 'm', 'm', 'r', 'm', 'm' }}, //3: Boundary hit then Success
                   new Sequence{Moves = new List<char> { 'm', 'r', 'm', 'm', 'm', 'm', 'm', 'r', 'm', 'r','m' }}, //4: Boundary hit then Mine Hit
                   new Sequence{Moves = new List<char> { 'm', 'r', 'm', 'm', 'm', 'm', 'r', 'm', 'm','m','m','m' }} //5: Success but more moves inserted
                }
            };

            File.WriteAllText("game-settings", JsonConvert.SerializeObject(gameSettings));
            File.WriteAllText("moves", JsonConvert.SerializeObject(moves));
        }
    }
}
