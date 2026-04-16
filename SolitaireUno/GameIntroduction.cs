using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SolitaireUno
{
    public class GameIntroduction
    {
        public static void ShowGameIntroduction()
        {
            IInputProvider realInput = new ConsoleInput();
            IOutputProvider realOutput = new ConsoleOutput();
            
            string PlayerGameModeChoice;
            string SuitEnforcementChoice;
            
            bool EnforceSuits = false;
            
            GameMode gameMode;
            
            
            realOutput.WriteLine(@"   
                  _____       _ _ _        _           _    _             
                 / ____|     | (_) |      (_)         | |  | |            
                | (___   ___ | |_| |_ __ _ _ _ __ ___ | |  | |_ __   ___  
                 \___ \ / _ \| | | __/ _` | | '__/ _ \| |  | | '_ \ / _ \ 
                 ____) | (_) | | | || (_| | | | |  __/| |__| | | | | (_) |
                |_____/ \___/|_|_|\__\__,_|_|_|  \___| \____/|_| |_|\___/ 
                ");
            
            realOutput.Write("Are You Ready to Play? ");
            string playerChoice = realInput.GetInput().ToLower();
            
            if (playerChoice == "yes" || playerChoice == "y")
            {
                bool validSuitEnforcementChoice = false;
                while (!validSuitEnforcementChoice)
                {
                    realOutput.Write("\nOkay, Would You Like to Enable Suit Enforcement? ");
                    SuitEnforcementChoice = realInput.GetInput().ToLower();

                    if (SuitEnforcementChoice.Equals("yes") || SuitEnforcementChoice.Equals("y"))
                    {
                        realOutput.WriteLine("Okay! Remember, Reds on Blacks and Blacks on Reds. (Black: Spades/Clubs | Red: Hearts/Diamonds)");

                        EnforceSuits = true;
                        validSuitEnforcementChoice = true;
                    }

                    else if (SuitEnforcementChoice.Equals("no") || SuitEnforcementChoice.Equals("n"))
                    {
                        validSuitEnforcementChoice = true;
                    }

                    else
                    {
                        realOutput.WriteLine("Please answer again, there may have been a mistake in your response");
                    }
                }

                bool validModeChoice = false;
                while (!validModeChoice)
                {
                    realOutput.Write("\nNow... Would You Like to Play the Cards in Ascending (a) or Descending (d) Order? >> ");
                    PlayerGameModeChoice = realInput.GetInput().ToLower();
                    
                    if (PlayerGameModeChoice.Equals("ascending") || PlayerGameModeChoice.Equals("descending") || PlayerGameModeChoice.Equals("a") || PlayerGameModeChoice.Equals("d"))
                    {
                        if (PlayerGameModeChoice.Equals("ascending") || PlayerGameModeChoice.Equals("a"))
                        {
                            realOutput.WriteLine("\nAscending it is!");
                            validModeChoice = true;
                            gameMode = GameMode.Ascending;
                        }
                        
                        else
                        {
                            realOutput.WriteLine("\nDescending it is!");
                            validModeChoice = true;
                            gameMode = GameMode.Descending;
                        }

                        Deck normalDeck = new();
                        MainGame newGame = new(realInput, realOutput, normalDeck, gameMode, EnforceSuits);
                        newGame.StartGame();
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
            }
            
            else
            {
                realOutput.WriteLine("What?");
            }
        }
    }
}
