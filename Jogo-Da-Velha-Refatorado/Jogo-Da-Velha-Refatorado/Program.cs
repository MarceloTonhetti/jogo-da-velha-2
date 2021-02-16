using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_Da_Velha_Refatorado
{
	class Program
	{
		static void Main(string[] args)
		{
			int[,] board = new int[3, 3];
			int choiceLine = 0, choiceColumn = 0;
			int matchResult = 0;
			int countPlayer = 1;
			int currentPlayer;

			tutorial();

			do
			{
				Console.Clear();
				Console.WriteLine("Jogo da velha\n");
				Console.WriteLine("Partida em andamento ");
				printBoard(board);
				Console.WriteLine("");

				currentPlayer = playerControl(countPlayer);

				if (readPosition(ref choiceLine, ref choiceColumn, currentPlayer) && validatePosition(board, choiceLine, choiceColumn))
				{
					makeMove(board, choiceLine, choiceColumn, currentPlayer);
					if (countPlayer >= 5)
					{
						matchResult = verifyVictoryOrDefeat(board);
						if (matchResult > 0)
						{
							Console.Clear();
							Console.WriteLine("O jogador {0} venceu !!!!!!!\n\n", currentPlayer);
						}
						else
							if (countPlayer == 9)
							{
								Console.Clear();
								Console.WriteLine("Jogo terminou em empate !!!!\n\n");
							}
					}
					countPlayer++;
				}
			} while (matchResult == 0 && countPlayer <= 9);

			Console.WriteLine("Partida Finalizada ");
			Console.WriteLine("\nTabuleiro final");
			printBoard(board);
			Console.ReadKey();
		}

		static void tutorial()
		{
			Console.WriteLine("---x---x---x---x--- Tutorial Rápido ---x---x---x---x---");
			Console.WriteLine("\nRepresentação do Jogador 1: X");
			Console.WriteLine("Representação do Jogador 2: O");
			Console.WriteLine("\nPara realizar uma jogada voce deve informar o valor da linha e da coluna");
			Console.WriteLine("Exemplo de jogada do jogador 1\n");
			Console.WriteLine("Escolha uma linha: 1");
			Console.WriteLine("Escolha uma coluna: 1");
			Console.WriteLine("\nTabuleiro apos a jogada:\n");

			Console.WriteLine("     0   1   2  COLUNAS\n");
			Console.WriteLine("0    - | - | -");
			Console.WriteLine("    ------------");
			Console.WriteLine("1    - | X | -");
			Console.WriteLine("    ------------");
			Console.WriteLine("2    - | - | -");
			Console.WriteLine("\nL\nI\nN\nH\nA\nS\n\n");

			Console.Write("Pressione qualquer tecla para iniciar o jogo...");
			Console.ReadKey();
		}

		static void printBoard(int[,] board)
		{
			char[,] auxBoard = new char[board.GetLength(0), board.GetLength(1)];

			for (int i = 0; i < board.GetLength(0); i++)
			{
				for (int j = 0; j < board.GetLength(1); j++)
				{
					if (board[i, j] == 0)
						auxBoard[i, j] = '-';
					else
						if (board[i, j] == 1)
						auxBoard[i, j] = 'X';
					else
							if (board[i, j] == 4)
						auxBoard[i, j] = 'O';
				}
			}

			Console.WriteLine("\n");
			Console.WriteLine("    0   1   2 \n");
			Console.WriteLine("0   " + auxBoard[0, 0] + " | " + auxBoard[0, 1] + " | " + auxBoard[0, 2]);
			Console.WriteLine("   -----------");
			Console.WriteLine("1   " + auxBoard[1, 0] + " | " + auxBoard[1, 1] + " | " + auxBoard[1, 2]);
			Console.WriteLine("   -----------");
			Console.WriteLine("2   " + auxBoard[2, 0] + " | " + auxBoard[2, 1] + " | " + auxBoard[2, 2]);
		}

		static int playerControl(int countPlayer)
		{
			if (countPlayer % 2 != 0)
				return 1;
			else
				return 2;
		}

		static bool readPosition(ref int choiceLine, ref int choiceColumn, int currentPlayer)
		{
			string x, y;

			Console.WriteLine("Jogador {0}", currentPlayer);
			Console.Write("Escolha a linha: ");
			x = Console.ReadLine();
			Console.Write("Escolha a coluna: ");
			y = Console.ReadLine();

			if (int.TryParse(x, out choiceLine) && (int.TryParse(y, out choiceColumn)))
				return true;
			else
			{
				Console.WriteLine("\nVocê digitou algo diferente de um número inteiro !!!");
				Console.Write("\nPrecione qualquer tecla para fazer uma nova leitura da posição.");
				Console.ReadKey();
				return false;
			}
		}

		static bool validatePosition(int[,] board, int choiceLine, int choiceColumn)
		{
			if (choiceLine < 0 || choiceLine > 2 || choiceColumn < 0 || choiceColumn > 2)
			{
				Console.WriteLine("Você digitou uma posição inexistente !!!");
				Console.Write("\nPrecione qualquer tecla para fazer uma nova leitura da posição.");
				Console.ReadKey();
				return false;
			}
			else
			{
				if (board[choiceLine, choiceColumn] == 0)
					return true;
				else
				{
					Console.WriteLine("A posição já foi preenchida !!!");
					Console.Write("\nPrecione qualquer tecla para fazer uma nova leitura da posição.");
					Console.ReadKey();
					return false;
				}
			}
		}

		static void makeMove(int[,] board, int choiceLine, int choiceColumn, int currentPlayer)
		{
			if (currentPlayer == 2)
				currentPlayer = 4;

			board[choiceLine, choiceColumn] = currentPlayer;
		}

		static int verifyVictoryOrDefeat(int[,] board)
		{
			int sum = 0;

			//Verificar linha:
			for (int line = 0; line < board.GetLength(0); line++)
			{
				sum = 0;
				for (int colunm = 0; colunm < board.GetLength(1); colunm++)
				{
					sum += board[line, colunm];
				}

				if (sum == 3)
					return 1;
				else
					if (sum == 12)
					return 2;
			}

			//Verificar coluna:
			for (int colunm = 0; colunm < board.GetLength(0); colunm++)
			{
				sum = 0;
				for (int line = 0; line < board.GetLength(1); line++)
				{
					sum += board[line, colunm];
				}

				if (sum == 3)
					return 1;
				else
					if (sum == 12)
					return 2;
			}

			//Verificar diagonal principal:
			sum = 0;
			for (int line = 0; line < board.GetLength(0); line++)
			{
				sum += board[line, line];
			}
			if (sum == 3)
				return 1;
			else if (sum == 12)
				return 2;

			//Verificar diagonal secundária:
			sum = 0;
			for (int line = 0, colunm = 2; line < board.GetLength(0); line++, colunm--)
			{
				sum += board[line, colunm];
			}
			if (sum == 3)
				return 1;
			else if (sum == 12)
				return 2;

			//Retorno padrão (empate)
			return 0;
		}
	}
}
