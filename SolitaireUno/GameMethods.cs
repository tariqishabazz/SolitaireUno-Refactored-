using SolitaireUno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireUno
{
    /// <summary>
    /// Provides static methods for evaluating card plays and game logic.
    /// </summary>
    /// <remarks>
    /// This class contains utility methods for determining valid moves and penalties in card games. All methods
    /// are static and can be accessed without creating an instance of the class.
    /// </remarks>
    public class GameMethods
    {
        /// <summary>
        /// Determines whether the specified card can be legally played on top of the currently shown card according to
        /// game rules and the selected game mode.
        /// </summary>
        /// <param name="potentialPlay">The card that is being considered for play.</param>
        /// <param name="currentlyShown">The card that is currently shown and on which a new card may be played.</param>
        /// <param name="playerChoice">The user's selected game mode (ascending/descending).</param>
        /// <returns>true if the potential play is valid based on the current card and mode; otherwise, false.</returns>
        public static bool ValidCard(Card potentialPlay, Card currentlyShown, string playerChoice)
        {
            // If descending mode, check if the card is one less than the current or King on Ace
            if (playerChoice.Equals("descending") || playerChoice.Equals("d"))
            {
                if ((int)potentialPlay.Value == (int)currentlyShown.Value - 1) // Card is one less
                {
                    return true;
                }
                else if (potentialPlay.Value == Values.King && currentlyShown.Value == Values.Ace) // King on Ace
                {
                    return true;
                }
                return false; // Not a valid move in descending mode
            }
            else // Ascending mode
            {
                if ((int)potentialPlay.Value == (int)currentlyShown.Value + 1) // Card is one more
                {
                    return true;
                }
                else if (potentialPlay.Value == Values.Ace && currentlyShown.Value == Values.King) // Ace on King
                {
                    return true;
                }
                return false; // Not a valid move in ascending mode
            }
        }

        /// <summary>
        /// Determines the penalty count for a dealt card based on a penalty card.
        /// </summary>
        /// <param name="dealtCard">The card that was just dealt.</param>
        /// <param name="penaltyCard">The penalty card to compare against.</param>
        /// <returns>The number of penalty cards to add (e.g., 5 for Queen of Spades), or 0 if no penalty.</returns>
        public static int GetPenaltyCount(Card dealtCard, Card penaltyCard)
        {
            if(dealtCard.IsEqual(penaltyCard)) // If the dealt card matches the penalty card
            {
                return 5; // Apply penalty (e.g., 5 extra cards)
            }
            return 0; // No penalty
        }
    }
}