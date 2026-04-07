using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    /// <summary>
    /// Handles the logic for a human player's turn, including input, validation, and card actions.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the PlayerTurnHandler class.
    /// </remarks>
    /// <param name="player">The human player.</param>
    /// <param name="deck">The deck used for drawing cards.</param>
    /// <param name="input">Input provider for user input.</param>
    /// <param name="output">Output provider for user output.</param>
    public class PlayerTurnHandler(Player player, Deck deck, IInputProvider input, IOutputProvider output)
    {
        /// <summary>
        /// Handles the logic for a single human player turn, including input, validation, and card actions.
        /// </summary>
        /// <param name="currentCard">Reference to the current card in play (may be updated).</param>
        /// <param name="penaltyCard">The penalty card for special rules.</param>
        public void HandleTurn(ref Card currentCard, Card penaltyCard)
        {
            bool playerChoiceValid = false; // Tracks if the player made a valid move

            while (!playerChoiceValid)
            {
                output.WriteLine("---------------------------------------------------------------------"); // Print turn separator
                output.WriteLine($"\n            The Current Card is... {currentCard}"); // Show the current card


                if (deck.Length() > 0)
                {
                    output.WriteLine($"\n                There are {deck.Length()} cards in the deck!"); // Show deck size
                }
                else
                {
                    output.WriteLine("\nThere are no more cards in the deck!"); // Deck is empty
                }

                output.Write("\nPlay a Card (1 - # of Cards in Hand), Pick-Up (p.u), or Pass(p) >> "); // Prompt for action
                string playerDecision = input.GetInput().ToLower(); // Get and normalize input

                if (playerDecision != null)
                {
                    if (int.TryParse(playerDecision, out int decisionAsNumber)) // If input is a number
                    {
                        if (decisionAsNumber > 0 && decisionAsNumber <= player.Hand.Count) // Valid card index
                        {
                            Card potentialCard = player.Hand[decisionAsNumber - 1]; // Get selected card

                            if (GameMethods.ValidCard(potentialCard, currentCard, MainGame.GameModeChoice)) // Validate move
                            {
                                player.PlayCard(potentialCard); // Play the card
                                currentCard = potentialCard; // Update current card

                                output.WriteLine("\n---------------------------------------------------------------------");
                                output.WriteLine($"You played {potentialCard}, so...");
                                playerChoiceValid = true; // End turn
                            }
                            else
                            {
                                output.WriteLine("\n---------------------------------------------------------------------");
                                output.WriteLine("That is not a valid play, please choose again"); // Invalid move
                            }
                        }
                        else
                        {
                            output.WriteLine("\n---------------------------------------------------------------------");
                            output.WriteLine("That is an invalid input based on your current cards, please choose again."); // Invalid index
                        }
                    }
                    else if (playerDecision == "p.u" || playerDecision == "pu" || playerDecision == "pick up" || playerDecision == "pickup") // Pick up
                    {
                        if (deck.Length() > 0)
                        {
                            Card card = deck.DealCard()!; // Draw a card
                            player.PickupCard(card); // Add to hand

                            int playerPotentialPenaltyCount = GameMethods.GetPenaltyCount(card, penaltyCard); // Check penalty
                            
                            if (playerPotentialPenaltyCount > 0)
                            {
                                output.WriteLine("\n---------------------------------------------------------------------");
                                output.WriteLine("You decided to pick up and recieved the Queen of Spades! HAHAHAHA");
                                output.WriteLine("You recieved 5 additional cards because... why not...");
                            }
                            else
                            {
                                output.WriteLine("\n---------------------------------------------------------------------");
                                output.WriteLine("You decided to pick up!");
                            }

                            for (int i = 0; i < playerPotentialPenaltyCount; i++) // Add penalty cards
                            {
                                player.PickupCard(deck.DealCard()!);
                            }

                            playerChoiceValid = true; // End turn
                        }
                        else
                        {
                            output.WriteLine("\n---------------------------------------------------------------------");
                            output.WriteLine("There are no more cards in the deck! Either play or pass!"); // Can't pick up
                        }
                    }
                    else if (playerDecision == "pass" || playerDecision == "p") // Pass
                    {
                        if (deck.Length() > 0)
                        {
                            output.WriteLine("\n---------------------------------------------------------------------");
                            output.WriteLine("There are still cards in the deck, either play or pick up!"); // Can't pass yet
                        }
                        else
                        {
                            output.WriteLine("\n---------------------------------------------------------------------");
                            output.WriteLine("You decided to pass!");
                            playerChoiceValid = true; // End turn
                        }
                    }
                }
                else
                {
                    output.WriteLine("\n---------------------------------------------------------------------");
                    output.WriteLine("You did not make a decision, please try again"); // No input
                }
            }
        }
    }
}
