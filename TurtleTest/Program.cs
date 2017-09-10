using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Validation
            if (args.Count() != 2)
            {
                Console.WriteLine("Invalid parameters! Should be: game-settings moves");
                Console.Read();
                Environment.Exit(0);
            }

            var gameSettingsFileName = args[0];
            var movesFileName = args[1];

            if (!File.Exists(gameSettingsFileName) || !File.Exists(movesFileName))
            {
                Console.WriteLine("Files not found! Example files have been written to the application directory");
                ProgramUtils.WriteExampleFiles();
                Console.Read();
                Environment.Exit(0);
            }
            #endregion

            GameSettingsModel gameSettings = null;
            MovesModel moves = null;

            #region Reading Files
            try
            {
                gameSettings = JsonConvert.DeserializeObject<GameSettingsModel>(File.ReadAllText(gameSettingsFileName));
                moves = JsonConvert.DeserializeObject<MovesModel>(File.ReadAllText(movesFileName));


            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error parsing files: {0}. Example files have been written to the application directory", ex.Message));
                ProgramUtils.WriteExampleFiles();
                Console.Read();
                Environment.Exit(0);
            }
            #endregion

            for (var i = 0; i < moves.Sequences.Count; i++)
            {
                var game = new Game(gameSettings);

                foreach (var move in moves.Sequences[i].Moves)
                {
                    if (!game.HasFinished)
                    {
                        try
                        {
                            game.GoToNextPosition(move);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(string.Format("Sequence {0}: {1}", i, ex.Message));
                        }
                    }
                }
                Console.WriteLine(string.Format("Sequence {0}: {1}", i, game.Result));
            }
            Console.Read();
            Environment.Exit(0);
        }
    }
}
