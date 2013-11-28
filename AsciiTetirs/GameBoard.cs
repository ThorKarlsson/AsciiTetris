using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiTetris
{
    public class GameBoard
    {
        private bool[,] board = new bool[20, 20];
        private bool[,] currentPiece;

        private List<Coordinates> pieceCoordinates;

        public GameBoard()
        {
            pieceCoordinates = new List<Coordinates>();
            InitializeBorder();
            GetNextPiece();
            UpdateBoard();

            runGame();
        }


        private void runGame()
        {
            while (!GameOver())
            {
                while (ValidMoveAvailable())
                {
                    UpdateBoard();
                    AwaitCommand();
                }
                UpdateBoard();
                GetNextPiece();
            }
            UpdateBoard();
            System.Console.WriteLine("Game Over");
        }

        private bool GameOver()
        {
            return !ValidMoveAvailable();
        }

        private bool ValidMoveAvailable()
        {
            foreach (Coordinates c in pieceCoordinates)
            {
                //run along the bottom of the piece and inspect whether there is a block/floor beneath it
                if (board[c.x, c.y+1] == true && !pieceCoordinates.Contains(new Coordinates(c.x, c.y+1)))
                {
                    return false;
                }
            }
            return true;
        }

        private void GetNextPiece()
        {
            currentPiece = Tetrimino.NextPiece();
            int pieceWidth = currentPiece.GetLength(0);
            int pieceHeight = currentPiece.GetLength(1);
            int pieceX = 10;
            int pieceY = 0;
            pieceCoordinates.Clear();
            for (int y = 0; y < pieceHeight; y++)
            {
                for (int x = 0; x < pieceWidth; x++)
                {
                    if (currentPiece[x, y] == true)
                    {
                        pieceCoordinates.Add(new Coordinates(pieceX + x, pieceY + y));
                    }
                }
            }
        }

        private void AwaitCommand()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (IsInvalidKeyPress(key.Key))
            {
                key = System.Console.ReadKey();  
            }
            
            ClearPiece();
            
            ProcessKeyPress(key.Key);

            
        }

        private void ProcessKeyPress(ConsoleKey key)
        {
            ShiftDown();
            switch (key)
            {
                case ConsoleKey.W:
                    CounterClockwiseRotation();
                    ShiftDown();
                    break;
                case ConsoleKey.S:
                    ClockwiseRotation();
                    RightShift();
                    break;
                case ConsoleKey.A:
                    LeftShift();
                    break;
                case ConsoleKey.D:
                    RightShift();
                    break;
                default:
                    throw new InvalidOperationException("Invalid key input");
            }
        }

        private void ShiftDown()
        {
            foreach(Coordinates c in pieceCoordinates)
            {
                c.y++;
            }
        }

        private void ClockwiseRotation()
        {
            pieceCoordinates = TetriminoRotater.ClockwiseRotation(pieceCoordinates);
        }

        private void CounterClockwiseRotation()
        {
            pieceCoordinates = TetriminoRotater.CounterClockwiseRotation(pieceCoordinates);
        }

        private void LeftShift()
        {
            foreach (Coordinates c in pieceCoordinates)
            {
                c.x--;
            }
        }

        private void RightShift()
        {
            foreach (Coordinates c in pieceCoordinates)
            {
                c.x++;
            }
        }

        private void UpdateBoard()
        {
            PlacePiece();
            DrawBoard();
        }

        private void PlacePiece()
        {
            foreach (Coordinates c in pieceCoordinates)
            {
                board[c.x, c.y] = true;
            }
        }

        private void ClearPiece()
        {
            foreach (Coordinates c in pieceCoordinates)
            {
                board[c.x, c.y] = false;
            }
        }
        
        private void DrawBoard()
        {
            Console.Clear();
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    if (board[x, y] == true)
                    {
                        System.Console.Write("*");
                    }
                    else
                    {
                        System.Console.Write(" ");
                    }
                }
                System.Console.WriteLine();
            }
        }

        private void InitializeBorder()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (IsBorder(x,y))
                    {
                        board[x, y] = true;
                    }
                }
            }
        }

        private bool IsBorder(int x, int y)
        {

            if (x == 0 || x == 19)
            {
                return true;
            }
            if (y == 19)
            {
                return true;
            }
            return false;
        }

        private bool IsInvalidKeyPress(ConsoleKey key)
        {
            if (key == ConsoleKey.W || key == ConsoleKey.A || key == ConsoleKey.S || key == ConsoleKey.D)
            {
                if (key == ConsoleKey.A)
                {
                    return InvalidLeftShift();
                }
                if (key == ConsoleKey.D)
                {
                    return InvalidRightShift();
                }
                if (key == ConsoleKey.W)
                {
                    return InvalidCounterClockwiseRotation();
                }
                if (key == ConsoleKey.S)
                {
                    return InvalidClockwiseRotation();
                }
            }
            return true;
        }

        private bool InvalidLeftShift()
        {
            foreach (Coordinates c in pieceCoordinates)
            {
                //check if coordinate to the left is free AND not part of the piece
                if (board[c.x - 1, c.y] == true && !pieceCoordinates.Contains(new Coordinates(c.x-1, c.y)))
                {
                    return true;
                }
            }
            return false;

        }

        private bool InvalidRightShift()
        {
            foreach (Coordinates c in pieceCoordinates)
            {
                //check if coordinate to the right is free AND not part of the piece
                if (board[c.x + 1, c.y] == true && !pieceCoordinates.Contains(new Coordinates(c.x + 1, c.y)))
                {
                    return true;
                }
            }
            return false;
        }

        private bool InvalidClockwiseRotation()
        {
            var newLocation = TetriminoRotater.ClockwiseRotation(pieceCoordinates);
            return InvalidLocation(newLocation);
        }

        private bool InvalidCounterClockwiseRotation()
        {
            var newLocation = TetriminoRotater.CounterClockwiseRotation(pieceCoordinates);
            return InvalidLocation(newLocation);
        }

        private bool InvalidLocation(List<Coordinates> newLocation)
        {
            foreach (Coordinates c in newLocation)
            {
                if (!IsOnBoard(c.x, c.y)) { return true; }
                
                if (board[c.x, c.y] == true && !pieceCoordinates.Contains(new Coordinates(c.x,c.y)))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsOnBoard(int x, int y)
        {
            if (x >= 0 && x < 20)
            {
                if (y >= 0 && y < 20)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
