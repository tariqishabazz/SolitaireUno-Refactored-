using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    /// <summary>
    /// Handles the logic for the computer's turn, including AI move selection and penalty handling.
    /// </summary>
    public class ComputerTurnHandler
    {
        private readonly Computer _computer; // Reference to the computer player
        private readonly Deck _deck; // Reference to the deck used in the game
        private readonly IOutputProvider _output; // Handles output to the user

        /// <summary>
        /// Initializes a new instance of the ComputerTurnHandler class.
        /// </summary>
        /// <param name="computer">The computer player.</param>
        /// <param name="deck">The deck used for drawing cards.</param>
        /// <param name="output">Output provider for user output.</param>
        public ComputerTurnHandler(Computer computer, Deck deck, IOutputProvider output)
        {
            _computer = computer; // Assign computer reference
            _deck = deck; // Assign deck reference
            _output = output; // Assign output provider
        }

        /// <summary>
        /// Handles the logic for a single computer turn, including AI move selection and penalty handling.
        /// </summary>
        /// <param name="currentCard">Reference to the current card in play (may be updated).</param>
        /// <param name="penaltyCard">The penalty card for special rules.</param>
        public void HandleTurn(ref Card currentCard, RegularCard penaltyCard)
        {
            Card? potentialComputerPlay = _computer.MakeMove(currentCard); // Let the computer try to play a card

            if (potentialComputerPlay != null) // If a valid move was made
            {
                currentCard = potentialComputerPlay; // Update the current card
                _output.WriteLine($"\nComputer played: {potentialComputerPlay}"); // Announce the move
            }
            else // No valid move, must pick up or pass
            {
                if (_deck.Length() > 0)
                {
                    Card card = _deck.DealCard()!; // Draw a card
                    _computer.PickupCard(card); // Add to computer's hand

                    int computerPotentialPenaltyCount = GameMethods.GetPenaltyCount(card, penaltyCard); // Check penalty

                    if (computerPotentialPenaltyCount > 0)
                    {
                        _output.WriteLine("\nThe computer decided to pick up and recieved the Queen of Spades!");
                        _output.WriteLine("It recieved 5 additional cards because... why not...");

                        for (int i = 0; i < computerPotentialPenaltyCount; i++) // Add penalty cards
                        {
                            _computer.PickupCard(_deck.DealCard()!);
                        }
                    }
                    else
                    {
                        _output.WriteLine("\nComputer couldn't make a move... and picked up"); // No penalty, just picked up
                    }
                }
                else
                {
                    _output.WriteLine("\nComputer couldn't play a move and couldn't pick up... so it passed"); // No cards left to pick up
                }
            }
        }
    }
}
