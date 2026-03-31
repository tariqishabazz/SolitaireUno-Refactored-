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
    public class PlayerTurnHandler
    {
        private readonly Player _player; // Reference to the human player
        private readonly Deck _deck; // Reference to the deck used in the game
        private readonly IInputProvider _input; // Handles user input
        private readonly IOutputProvider _output; // Handles user output

        /// <summary>
        /// Initializes a new instance of the PlayerTurnHandler class.
        /// </summary>
        /// <param name="player">The human player.</param>
        /// <param name="deck">The deck used for drawing cards.</param>
        /// <param name="input">Input provider for user input.</param>
        /// <param name="output">Output provider for user output.</param>
        public PlayerTurnHandler(Player player, Deck deck, IInputProvider input, IOutputProvider output)
        {
            _player = player; // Assign player reference
            _deck = deck; // Assign deck reference
            _input = input; // Assign input provider
            _output = output; // Assign output provider
        }

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
                _output.WriteLine("---------------------------------------------------------------------"); // Print turn separator
                _output.WriteLine($"\n            The Current Card is... {currentCard}"); // Show the current card


                if (_deck.Length() > 0)
                {
                    _output.WriteLine($"\n                There are {_deck.Length()} cards in the deck!"); // Show deck size
                }
                else
                {
                    _output.WriteLine("\nThere are no more cards in the deck!"); // Deck is empty
                }

                _output.Write("\nPlay a Card (1 - # of Cards in Hand), Pick-Up (p.u), or Pass(p) >> "); // Prompt for action
                string playerDecision = _input.GetInput().ToLower(); // Get and normalize input

                if (playerDecision != null)
                {
                    if (int.TryParse(playerDecision, out int decisionAsNumber)) // If input is a number
                    {
                        if (decisionAsNumber > 0 && decisionAsNumber <= _player.Hand.Count) // Valid card index
                        {
                            Card potentialCard = _player.Hand[decisionAsNumber - 1]; // Get selected card

                            if (GameMethods.ValidCard(potentialCard, currentCard, )) // Validate move
                            {
                                _player.PlayCard(potentialCard); // Play the card
                                currentCard = potentialCard; // Update current card

                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine($"You played {potentialCard}, so...");
                                playerChoiceValid = true; // End turn
                            }
                            else
                            {
                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine("That is not a valid play, please choose again"); // Invalid move
                            }
                        }
                        else
                        {
                            _output.WriteLine("\n---------------------------------------------------------------------");
                            _output.WriteLine("That is an invalid input based on your current cards, please choose again."); // Invalid index
                        }
                    }
                    else if (playerDecision == "p.u" || playerDecision == "pu" || playerDecision == "pick up" || playerDecision == "pickup") // Pick up
                    {
                        if (_deck.Length() > 0)
                        {
                            Card card = _deck.DealCard()!; // Draw a card
                            _player.PickupCard(card); // Add to hand

                            int playerPotentialPenaltyCount = GameMethods.GetPenaltyCount(card, penaltyCard); // Check penalty
                            
                            if (playerPotentialPenaltyCount > 0)
                            {
                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine("You decided to pick up and recieved the Queen of Spades! HAHAHAHA");
                                _output.WriteLine("You recieved 5 additional cards because... why not...");
                            }
                            else
                            {
                                _output.WriteLine("\n---------------------------------------------------------------------");
                                _output.WriteLine("You decided to pick up!");
                            }

                            for (int i = 0; i < playerPotentialPenaltyCount; i++) // Add penalty cards
                            {
                                _player.PickupCard(_deck.DealCard()!);
                            }

                            playerChoiceValid = true; // End turn
                        }
                        else
                        {
                            _output.WriteLine("\n---------------------------------------------------------------------");
                            _output.WriteLine("There are no more cards in the deck! Either play or pass!"); // Can't pick up
                        }
                    }
                    else if (playerDecision == "pass" || playerDecision == "p") // Pass
                    {
                        if (_deck.Length() > 0)
                        {
                            _output.WriteLine("\n---------------------------------------------------------------------");
                            _output.WriteLine("There are still cards in the deck, either play or pick up!"); // Can't pass yet
                        }
                        else
                        {
                            playerChoiceValid = true; // End turn
                        }
                    }
                }
                else
                {
                    _output.WriteLine("\n---------------------------------------------------------------------");
                    _output.WriteLine("You did not make a decision, please try again"); // No input
                }
            }
        }
    }
}
