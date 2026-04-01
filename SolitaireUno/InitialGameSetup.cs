using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    /// <summary>
    /// Handles the initial setup of the game, including dealing cards to players and preparing the deck.
    /// </summary>
    public static class InitialGameSetup
    {
        /// <summary>
        /// Sets up the game by dealing cards to the player and computer and shuffling the deck.
        /// </summary>
        /// <param name="player">The human player to receive cards.</param>
        /// <param name="computer">The computer player to receive cards.</param>
        /// <param name="deck">The deck to deal cards from.</param>
        public static void SetupGame(Player player, Computer computer, Deck deck)
        {
            // Deal 10 cards to the human and computer players
            for (int i = 0; i < 10; i++)
            {
                RegularCard playerCard = deck.DealCard()!; // Deal a card from the deck
                player.PickupCard(playerCard); // Add the card to the player's hand

                RegularCard computerCard = deck.DealCard()!; // Deal a card from the deck
                computer.PickupCard(computerCard); // Add the card to the computer's hand
            }
        }
    }
}
