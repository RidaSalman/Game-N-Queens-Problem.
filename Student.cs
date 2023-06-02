using System;

class NQueens
{
    private int n;
    private int[,] board;

    public NQueens(int n)
    {
        this.n = n;
        board = new int[n, n];
    }

    public int this[int row, int col]
    {
        get { return board[row, col]; }
        set { board[row, col] = value; }
    }

    public bool SolveQueensProblem()
    {
        if (PlaceQueens(0))
        {
            PrintBoard();
            Console.WriteLine("MESSAGE: Congratulations! You won!");
            return true;
        }
        Console.WriteLine("No solution found.");
        return false;
    }

    private bool PlaceQueens(int row)
    {
        if (row == n)
        {
            return true;
        }

        for (int col = 0; col < n; col++)
        {
            if (IsSafe(row, col))
            {
                board[row, col] = 1;

                if (PlaceQueens(row + 1))
                {
                    return true;
                }

                board[row, col] = 0;
            }
        }
        return false;
    }

    private bool IsSafe(int row, int col)
    {
        for (int i = 0; i < row; i++)
        {
            if (board[i, col] == 1)
            {
                Console.WriteLine("Violation of rule: Queens cannot be placed in the same column.");
                return false;
            }
            int rowDiff = row - i;
            int colDiff = Math.Abs(col - GetQueenColumnIndex(i));
            if (rowDiff == colDiff)
            {
                Console.WriteLine("Violation of rule: Queens cannot be placed on the same diagonal.");
                return false;
            }
        }
        return true;
    }

    private int GetQueenColumnIndex(int row)
    {
        for (int col = 0; col < n; col++)
        {
            if (board[row, col] == 1)
            {
                return col;
            }
        }
        return -1;
    }

    public void PrintBoard()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(board[i, j] == 1 ? "Q " : "- ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter the value of n: ");
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer value.");
            return;
        }

        NQueens game = new NQueens(n);
        if (game.SolveQueensProblem())
        {
            Console.WriteLine("You can remove a queen by specifying its position (row, column) separated by a space.");
            while (true)
            {
                Console.Write("Enter the position of the queen to remove (or 'q' to quit): ");
                string input = Console.ReadLine()?.Trim().ToLower();
                if (input == "q")
                {
                    break;
                }
                string[] position = input?.Split(' ');
                if (position?.Length != 2 || !int.TryParse(position[0], out int row) || !int.TryParse(position[1], out int col))
                {
                    Console.WriteLine("Invalid input. Please enter valid row and column indices.");
                    continue;
                }
                if (row < 0 || row >= n || col < 0 || col >= n)
                {
                    Console.WriteLine("Invalid position. Please enter valid row and column indices.");
                    continue;
                }
                if (game[row, col] == 1)
                {
                    game[row, col] = 0;
                    Console.WriteLine("Queen removed successfully!");
                    game.PrintBoard();
                }
                else
                {
                    Console.WriteLine("There is no queen at the specified position.");
                }
            }
        }
    }
}
