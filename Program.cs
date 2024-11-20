/*
 * Name: [Logan Brooks]
 * South Hills Username: [lbrooks81]
 */

using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace TicTacToe
{
    public class Program
    {
        public const int BOARD_SIZE = 3;
        public static char[][] board = new char[BOARD_SIZE][];
        public const char PLAYER_ONE_SYMBOL = 'X';
        public const char PLAYER_TWO_SYMBOL = 'O';
        public static void Main()
        {
            StartGameInfo();
            InitializeBoard(board);

            bool P1Turn = true;
            while (true)
            {
                PrintBoard(board);
                P1Turn = PlayerTurn(board, P1Turn);
                if (IsWinner(board, !P1Turn)) break;
                if (BoardIsFull(board)) break;
            }
            
            PrintBoard(board);
            
            String whosTurn = P1Turn ? "Two" : "One";
            Console.WriteLine($"Player {whosTurn} Won!");
            
            while(PlayAgain())
            {
                Console.Clear();
                
                StartGameInfo();
                InitializeBoard(board);
                
                P1Turn = true;
                while (true)
                {
                    PrintBoard(board);
                    P1Turn = PlayerTurn(board, P1Turn);
                    if (IsWinner(board, !P1Turn)) break;
                    if (BoardIsFull(board)) break;
                }
                
                PrintBoard(board);
                
                whosTurn = P1Turn ? "Two" : "One";
                Console.WriteLine($"Player {whosTurn} Won!");
            }
        }
        public static bool PlayAgain()
        {
            Console.WriteLine("Type \"y\" to play again or anything else to quit.");

            if(Console.ReadLine().Equals("y"))
            {
                return true;
            }
            return false;
        }
        public static bool BoardIsFull(char[][] board)
        {
            foreach (char[]array in board)
            {
                foreach(char symbol in array)
                {
                    if(symbol == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool IsWinner(char[][] board, bool playerTurn)
        {   
            //Check Horizontals
            char playerSymbol = playerTurn ? PLAYER_ONE_SYMBOL : PLAYER_TWO_SYMBOL;
            
            int counter = 0;
            for(int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j].Equals(playerSymbol))
                    {
                        counter++;
                    }
                }
                if (counter == board[i].Length) return true;
                counter = 0;
            }
            
            counter = 0;
            // Check verticals
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[j][i].Equals(playerSymbol))
                    {
                        counter++;
                    }
                }
                if (counter == board.Length) return true;
                counter = 0;

            }
            
            // Check diagonals
            bool diagonalWin = true;
            char mainDiagonalFirst = board[0][0];

            for (int i = 1; i < BOARD_SIZE; i++)
            {
                if (board[i][i] == ' ' || board[i][i] != mainDiagonalFirst)
                {
                    diagonalWin = false;
                    break;
                }
            }

            if (diagonalWin) return true;
            
            diagonalWin = true;
            char antiDiagonalFirst = board[0][BOARD_SIZE - 1];
            for (int i = BOARD_SIZE - 2; i >= 0; i--)
            {
                if (board[BOARD_SIZE - i - 1][i] == ' ' || board[BOARD_SIZE - i - 1][i] != antiDiagonalFirst)
                {
                    diagonalWin = false;
                    break;
                }
            }
            return diagonalWin;
        }
        public static int InputValidation(String[] input)
        {
            if (input.Length != 2) return 1;
            
            int x = 0;
            int y = 0;
            if (int.TryParse(input[0], out x) == false) return 1;
            if (int.TryParse(input[1], out y) == false) return 1;
            
            if (x > board.Length) return 1;
            if (x < 1) return 1;
            
            if (y > board.Length) return 1;
            if (y < 1) return 1;
            
            if (!board[x - 1][y - 1].Equals(' ')) return -1;
            
            return 0;
        }
        public static bool PlayerTurn(char[][] board, bool playerTurn)
        {
            String startMessage = playerTurn ? "Player One's Turn" : "Player Two's Turn";
            Console.WriteLine(startMessage);
            Console.Write("Enter a row number followed by a column number separated by commas: ");
            
            String[] input = Console.ReadLine().Trim().Split(',');
            
            int validation = InputValidation(input);
            
            while (validation != 0)
            {
                if (validation == 1)
                {
                    Console.WriteLine("Invalid input.");
                    Console.Write("Enter a row number followed by a column number separated by commas: ");
                    input = Console.ReadLine().Trim().Split(',');
                    validation = InputValidation(input);
                }
                if (validation == -1)
                {
                    Console.WriteLine("Space is not empty.");
                    Console.Write("Enter a row number followed by a column number separated by commas: ");
                    input = Console.ReadLine().Trim().Split(',');
                    validation = InputValidation(input);
                }
            }

            board[int.Parse(input[0]) - 1][int.Parse(input[1]) - 1] 
                = playerTurn ? PLAYER_ONE_SYMBOL : PLAYER_TWO_SYMBOL;
            return playerTurn = !playerTurn;
        }
        public static void PrintBoard(char[][] board)
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                Console.Write(" " + (i + 1));
            }

            Console.Write(Environment.NewLine);

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                Console.WriteLine((i + 1) + String.Join('|', board[i]));

                if (i < BOARD_SIZE - 1)
                {
                    Console.WriteLine(" " + new String('-', BOARD_SIZE * 2 - 1));
                }
            }
        }
        public static void StartGameInfo()
        {
            Console.WriteLine("Tic Tac Toe");
            Console.WriteLine($"Player One will be represented by {PLAYER_ONE_SYMBOL}.");
            Console.WriteLine($"Player Two will be represented by {PLAYER_TWO_SYMBOL}.");
            Console.WriteLine("To select a spot, first enter a number for the column, press enter, and then enter a number for the row.");
        }
        public static void InitializeBoard(char[][] board)
        {
            for(int i = 0; i < BOARD_SIZE; ++i)
            {
                board[i] = [' ', ' ', ' '];
            }
        }
    }
}
