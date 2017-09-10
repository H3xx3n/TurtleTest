using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleTest
{
    public class Game
    {
        public Direction CurrentDirection { get; set; }
        public Point CurrentPosition { get; set; }
        public List<char> Moves { get; set; }
        public string Result { get; set; }
        public bool HasFinished { get; set; }

        private GameSettingsModel _gameSettings { get; set; }

        public Game(GameSettingsModel settings)
        {
            _gameSettings = settings;
            CurrentPosition = _gameSettings.StartPoint;
            CurrentDirection = _gameSettings.StartDirection;
            Moves = new List<char>();
            Result = "Still in danger!";
        }

        public void GoToNextPosition(char move)
        {
            if (HasFinished)
            {
                throw new Exception("Moves found after game finished. Aborting!");
            }

            Moves.Add(move);

            if (Char.ToLower(move) == 'm')
            {
                UpdatePositionFromCurrentDirection();
                CheckResult();
            }
            else if (Char.ToLower(move) == 'r')
            {
                UpdateCurrentDirection();
            }
            else
            {
                throw new Exception(string.Format("Unexpected move type found: {0}. Skipping!", move));
            }
        }

        private void CheckResult()
        {
            if (CurrentPosition.Equals(_gameSettings.Exit))
            {
                Result = "Success!";
                HasFinished = true;
            }

            foreach (var minePosition in _gameSettings.Mines)
            {
                if (CurrentPosition.Equals(minePosition))
                {
                    Result = "Mine hit!";
                    HasFinished = true;
                }
            }
        }
        private void UpdateCurrentDirection()
        {
            switch (CurrentDirection)
            {
                case Direction.North:
                    {
                        CurrentDirection = Direction.East;
                        break;
                    }

                case Direction.East:
                    {
                        CurrentDirection = Direction.South;
                        break;
                    }

                case Direction.South:
                    {
                        CurrentDirection = Direction.West;
                        break;
                    }

                case Direction.West:
                    {
                        CurrentDirection = Direction.North;
                        break;
                    }
            }
        }
        private void UpdatePositionFromCurrentDirection()
        {
            var nextPosition = new Point();
            switch (CurrentDirection)
            {
                case Direction.North:
                    {
                        nextPosition = new Point(CurrentPosition.X, CurrentPosition.Y - 1);
                        break;
                    }
                case Direction.East:
                    {
                        nextPosition = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
                        break;
                    }
                case Direction.South:
                    {
                        nextPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
                        break;
                    }
                case Direction.West:
                    {
                        nextPosition = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
                        break;
                    }
            }

            try
            {
                if (CheckBoundaries(nextPosition))
                {
                    CurrentPosition = nextPosition;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private bool CheckBoundaries(Point position)
        {
            var gameBoard = new Rectangle(0, 0, _gameSettings.Size.X + 1, _gameSettings.Size.Y + 1);
            if (!gameBoard.Contains(position))
            {
                throw new Exception(string.Format("Cannot move to: {0},{1} during the move {2}. Skipping!", position.X, position.Y, Moves.Count));
            }

            return true;
        }
    }
}
