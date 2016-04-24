using System;

namespace Dymova.DotNetCourse.TicTacToe
{
    public class Controller
    {
        private static readonly int[][,] WinConditions =
        {
            //columns
            new[,] {{0, 0}, {0, 1}, {0, 2}},
            new[,] {{1, 0}, {1, 1}, {1, 2}},
            new[,] {{2, 0}, {2, 1}, {2, 2}},

            //rows
            new[,] {{0, 0}, {1, 0}, {2, 0}},
            new[,] {{0, 1}, {1, 1}, {2, 1}},
            new[,] {{0, 2}, {1, 2}, {2, 2}},

            //diagonals
            new[,] {{0, 0}, {1, 1}, {2, 2}},
            new[,] {{0, 2}, {1, 1}, {2, 0}}
        };

        private Player _currentPlayer;
        private Cell _fieldStatus = Cell.Free;
        private int _prevX;
        private int _prevY;
        private readonly Cell[,] _sectorsInfo;
        private int _stepCount;

        public Controller()
        {
            var _stepCount = 0;
            Field = new Cell[9, 9];

            _sectorsInfo = new Cell[3, 3];
        }

        public Cell[,] Field { get; private set; }

        public void Run()
        {
            ConsoleInterface.DisplayField(Field, _sectorsInfo);

            int x;
            int y;

            var random = new Random();

            _currentPlayer = random.Next(1) == 0 ? Player.O : Player.X;

            while (_fieldStatus == Cell.Free)
            {
                ConsoleInterface.GetNextStep(out x, out y, _currentPlayer);
                if (IsSuitable(x, y))
                {
                    Field[x, y] = _currentPlayer == Player.X ? Cell.X : Cell.O;
                    UpdateSectorInfo(x, y);
                    CheckField();
                    ConsoleInterface.DisplayField(Field, _sectorsInfo);
                    _prevX = x;
                    _prevY = y;
                    _stepCount++;
                    _currentPlayer = _currentPlayer == Player.X ? Player.O : Player.X;
                }
                else
                {
                    ConsoleInterface.DisplayError("you shouldn't choose this cell");
                }
            }

            ConsoleInterface.DisplayResult(_fieldStatus);
        }

        private void CheckField()
        {
            foreach (var condition in WinConditions)
            {
                if (ConditionIsTrue(_sectorsInfo, condition, 0, 0, ref _fieldStatus))
                {
                    return;
                }
            }

            if (IsFilled(0, 0, _sectorsInfo))
            {
                _fieldStatus = Cell.Draw;
            }
        }


        private void UpdateSectorInfo(int x, int y)
        {
            var startSectorX = x/3*3;
            var startSectorY = y/3*3;
            var sectorStatus = _sectorsInfo[x/3, y/3];

            foreach (var condition in WinConditions)
            {
                if (ConditionIsTrue(Field, condition, startSectorX, startSectorY, ref sectorStatus))
                {
                    _sectorsInfo[x / 3, y / 3] = sectorStatus;
                    return;
                }
            }

            if (IsFilled(startSectorX, startSectorY, Field))
            {
                sectorStatus = Cell.Draw;
            }
        }

        private static bool ConditionIsTrue(Cell[,] field, int[,] condition, int x, int y, ref Cell sector)
        {
            var firstCell = field[condition[0, 0] + x, condition[0, 1] + y];
            if (firstCell != Cell.O && firstCell != Cell.X)
            {

                return false;
            }

            for (var i = 1; i < 3; i++)
            {
                var nextCell = field[condition[i, 0] + x, condition[i, 1] + y];
                if (nextCell != firstCell)
                {
                    return false;
                }
            }

            sector = firstCell;
            return true;
        }

        private bool IsSuitable(int x, int y)
        {
            if (Field[x, y] != Cell.Free)
            {
                return false;
            }
            if (_stepCount != 0)
            {
                var sectorX = _prevX - _prevX/3*3;
                var sectorY = _prevY - _prevY/3*3;
                if (IsFilled(sectorX, sectorY, Field) != true)
                {
                    if (x/3 != sectorX || y/3 != sectorY)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsFilled(int sectorX, int sectorY, Cell[,] field)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Field[sectorX + i, sectorY + j] == Cell.Free)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}