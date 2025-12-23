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
    /// <remarks>This class contains utility methods for determining valid moves in card games. All methods
    /// are static and can be accessed without creating an instance of the class.</remarks>
    public class GameMethods
    {
        /// <summary>
        /// Determines whether the specified card can be legally played on top of the currently shown card according to
        /// game rules.
        /// </summary>
        /// <remarks>A card is considered a valid play if its value is exactly one less than the currently
        /// shown card, or if it is a King played on an Ace. This method does not consider suit or other game-specific
        /// rules that may apply.</remarks>
        /// <param name="potentialPlay">The card that is being considered for play.</param>
        /// <param name="currentlyShown">The card that is currently shown and on which a new card may be played.</param>
        /// <returns>true if the potential play is valid based on the current card; otherwise, false.</returns>
        public static bool ValidCard(Card potentialPlay, Card currentlyShown) 
        {
            if((int)potentialPlay.Value == (int)currentlyShown.Value - 1) // casted the Value enums to represent int numbers for comparison
            {
                return true;  
            }
            else if(potentialPlay.Value == Values.King && currentlyShown.Value == Values.Ace) 
            {
                return true; 
            }

            return false;
        }

    }
}