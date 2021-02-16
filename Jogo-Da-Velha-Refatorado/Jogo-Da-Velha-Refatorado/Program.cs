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
			//declarar as variáveis
			const int line = 3, column = 3;
			int[,] board = new int[line, column], auxBoard = new int[line, column];

			int choiceLine = 0, choiceColumn = 0;
			int matchResult;
			int countPlayer = 1;
			int currentPlayer;

			//exibir tutorial
			tutorial();

			do
			{
				//imprimir tabuleiro
				imprimirJogo(board);
				//controlar de qual jogador é a vez
				currentPlayer = playerControl(countPlayer);
				//ler dados da posição da jogada

				
				//validar dados da jogada
				//Se dados ok
				if (readPosition(ref choiceLine, ref choiceColumn, currentPlayer) && validatePosition(board, choiceLine, choiceColumn))
				{
					//realizar jogada
					makeMove(board, choiceLine, choiceColumn, currentPlayer);
					//Se estiver na jogada >= 5
					if (countPlayer >= 5)
					{
						//verificarStatusDeVitoriaOuDerrota
						matchResult = verifyVictoryOrDefeat();
						if (matchResult > 0) ;
						//printar vitória do jogador atual (que vem do matchResult);
						//Se não VitoriaOuDerrota E jogada == 9
						else
							if (countPlayer == 9) ;
						//Jogo terminou em empate
						//incrementar contador da jogada
						countPlayer++;
					}
				}
				//Se dados não ok
				else
				{
					//Limpar console
					//reiniciar jogada
				}
				
			} while (true);

			Console.ReadKey();
		}

		static void tutorial()
		{
		}

		static void imprimirJogo(int[,] board)
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
				return false;
		}

		static bool validatePosition(int[,] board, int choiceLine, int choiceColumn)
		{
			if (choiceLine < 0 || choiceLine > 2 || choiceColumn < 0 || choiceColumn > 2)
			{
				Console.WriteLine("Você digitou uma posição inexistente !!!");
				return false;
			}
			else
			{
				if (board[choiceLine, choiceColumn] == 0)
					return true;
				else
				{
					Console.WriteLine("A posição já foi preenchida !!!");
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

		static int verifyVictoryOrDefeat()
		{
			return 0;
		}
	}
}
