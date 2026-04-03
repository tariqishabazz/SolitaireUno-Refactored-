using SolitaireUno;
using System.Numerics;
using System.Collections.Generic;
using System;

namespace SolitaireUno
{
    /// <summary>
    /// Represents a player in the card game, managing the player's hand and actions.
    /// </summary>
    /// <remarks>
    /// The Player class provides methods to pick up and play cards in the player's hand.
    /// It serves as the primary interface for interacting with a player's cards during gameplay.
    /// </remarks>
    public class Player
    {
        public List<Card> Hand = []; // The player's hand of cards

        /// <summary>
        /// Initializes a new instance of the Player class.
        /// </summary>
        public Player() // Empty constructor for Player
        {
        }

        /// <summary>
        /// Adds the specified card to the player's hand.
        /// </summary>
        /// <param name="card">The card to add to the player's hand. Cannot be null.</param>
        public void PickupCard(Card card)
        {
            Hand.Add(card); // Add the card to the player's hand
        }

        /// <summary>
        /// Removes the specified card from the player's hand, representing the action of playing that card.
        /// </summary>
        /// <param name="card">The card to be played and removed from the player's hand. Cannot be null.</param>
        public void PlayCard(Card card)
        {
            Hand.Remove(card); // Remove the card from the player's hand
        }
    }
}