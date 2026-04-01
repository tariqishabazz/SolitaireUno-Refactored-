using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    /// <summary>
    /// Provides the introduction and setup sequence for the game, including user prompts and mode selection.
    /// </summary>
    public class GameIntroduction
    {
        /// <summary>
        /// Runs the introduction, prompts the user for game mode, and starts the game if the user agrees.
        /// </summary>
        /// <returns>The user's selected game mode (e.g., ascending/descending), or exits if declined.</returns>
        public static void ShowGameIntroduction()
        {
            IInputProvider realInput = new ConsoleInput(); // Input provider for user input
            IOutputProvider realOutput = new ConsoleOutput(); // Output provider for user output

            string PlayerGameModeChoice; // Stores the user's game mode choice
            GameMode gameMode;
           // int playerCountAsNumber = 0 ;


            realOutput.Write("Are you ready to play >> "); // Prompt user to start
            string playerChoice = realInput.GetInput().ToLower(); // Get user response

            if (playerChoice == "yes" || playerChoice == "y") // If user wants to play
            {
                bool validModeChoice = false; // Tracks if a valid mode was chosen
                while (!validModeChoice)
                {
                    realOutput.Write("\nNow... would you like to play the cards in ascending (a) or descending (d) order? >> "); // Prompt for mode
                    PlayerGameModeChoice = realInput.GetInput().ToLower(); // Get mode choice

                    // Validate mode choice
                    if (PlayerGameModeChoice.Equals("ascending") || PlayerGameModeChoice.Equals("descending") || PlayerGameModeChoice.Equals("a") || PlayerGameModeChoice.Equals("d"))
                    {
                        if (PlayerGameModeChoice.Equals("ascending") || PlayerGameModeChoice.Equals("a"))
                        {
                            realOutput.WriteLine("\nAscending it is!"); // Confirm ascending
                            validModeChoice = true;

                            gameMode = GameMode.Ascending;
                        }
                        else
                        {
                            realOutput.WriteLine("\nDescending it is!"); // Confirm descending
                            validModeChoice = true;

                            gameMode = GameMode.Descending;
                        }
                        
                        /*
                        bool validPlayerAmount = false;
                        while (!validPlayerAmount)
                        {
                            realOutput.WriteLine("And how many players are there? Up to five max");
                            string playerCount = realInput.GetInput();

                            if (int.TryParse(playerCount, out playerCountAsNumber))
                            {
                                if (playerCountAsNumber > 1 && playerCountAsNumber <= 5)
                                {
                                    validPlayerAmount = true;
                                    realOutput.WriteLine("Have fun!\n");
                                    realOutput.WriteLine("Let's Gooooooooooooooo!"); // Excitement!                             
                                }
                                else
                                {
                                    realOutput.WriteLine("Sorry, that is not a valid input, please try again");
                                }
                            }
                            else
                            {
                                realOutput.WriteLine("Sorry, that is not a valid input, please try again");
                            }
                        } 
                        */

                        // Start the game with the chosen mode
                        Deck normalDeck = new(); // Create a new deck

                        MainGame newGame = new(realInput, realOutput, normalDeck, gameMode); // Create game instance
                        newGame.StartGame(); // Start the game
                    }
                    else
                    {
                        realOutput.WriteLine("Please answer again, there may have been a mistake in your response"); // Invalid input
                    }
                }
            }
            else if (playerChoice == "no" || playerChoice == "n") // User declines
            {
                realOutput.WriteLine("I understand, come back when you are ready"); // Farewell message
            }
            else // Any other input
            {
                realOutput.WriteLine("What? GoodBye"); // Unrecognized input
            }
        }
    }
}
