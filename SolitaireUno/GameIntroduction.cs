using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    public class GameIntroduction
    {
        public static string ShowGameIntroduction()
        {
            IInputProvider realInput = new ConsoleInput();
            IOutputProvider realOutput = new ConsoleOutput();

            string PlayerGameModeChoice;

            realOutput.Write("Are you ready to play >> ");
            string playerChoice = realInput.GetInput().ToLower();

            if (playerChoice == "yes" || playerChoice == "y")
            {
                bool validModeChoice = false;
                while (!validModeChoice)
                {
                    realOutput.Write("\nNow... would you like to play the cards in ascending (a) or descending (d) order? >> ");
                    PlayerGameModeChoice = realInput.GetInput().ToLower();


                    if (PlayerGameModeChoice.Equals("ascending") || PlayerGameModeChoice.Equals("descending") || PlayerGameModeChoice.Equals("a") || PlayerGameModeChoice.Equals("d"))
                    {
                        if (PlayerGameModeChoice.Equals("ascending") || PlayerGameModeChoice.Equals("a"))
                        {
                            realOutput.WriteLine("\nAscending it is!");
                            validModeChoice = true;

                            realOutput.WriteLine("Let's Gooooooooooooooo!");


                            Deck normalDeck = new();
                            MainGame newGame = new(realInput, realOutput, normalDeck, PlayerGameModeChoice);
                            newGame.StartGame();

                            return PlayerGameModeChoice;
                        }
                        else
                        {
                            realOutput.WriteLine("\nDescending it is!");
                            validModeChoice = true;

                            realOutput.WriteLine("Let's Gooooooooooooooo!");


                            Deck normalDeck = new();
                            MainGame newGame = new(realInput, realOutput, normalDeck, PlayerGameModeChoice);
                            newGame.StartGame();

                            return PlayerGameModeChoice;
                        }
                    }
                    
                    else
                    {
                        realOutput.WriteLine("Please answer again, there may have been a mistake in your response");
                    }
                }
            }

            else if (playerChoice == "no" || playerChoice == "n")
            {
                realOutput.WriteLine("I understand, come back when you are ready");
                Environment.Exit(0);
            }

            else
            {
                realOutput.WriteLine("What? GoodBye");
                Environment.Exit(0);
            }
            
            return string.Empty;
        }
    }
}
