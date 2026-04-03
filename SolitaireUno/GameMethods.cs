using SolitaireUno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        private const int PenaltyCardCount = 5;
        
        /// <summary>
        /// Determines whether the specified card can be legally played on top of the currently shown card according to
        /// game rules and the selected game mode.
        /// </summary>
        /// <param name="potentialPlay">The card that is being considered for play.</param>
        /// <param name="currentlyShown">The card that is currently shown and on which a new card may be played.</param>
        /// <param name="playerChoice">The user's selected game mode (ascending/descending).</param>
        /// <returns>true if the potential play is valid based on the current card and mode; otherwise, false.</returns>
        public static bool ValidCard(Card potentialPlay, Card currentlyShown, GameMode gameMode)
        {
            if (potentialPlay is RegularCard && currentlyShown is RegularCard)
                return gameMode == GameMode.Descending ? IsValidDescending(potentialPlay, currentlyShown)
                   : IsValidAscending(potentialPlay, currentlyShown);
            else
                return IsSpecialCard(potentialPlay, currentlyShown, gameMode);
        }

        private static bool IsValidDescending(Card potentialPlay, Card currentlyShown)
        {
            if (potentialPlay is RegularCard potentialCard && currentlyShown is RegularCard currentCard)
                if ((int)potentialCard.Value == (int)currentCard.Value - 1)
                    return true;

            return IsWrapAround(potentialPlay, currentlyShown, GameMode.Descending);
        }

        private static bool IsValidAscending(Card potentialPlay, Card currentlyShown)
        {
            if(potentialPlay is RegularCard potentialCard && currentlyShown is RegularCard currentCard)
                if ((int)potentialCard.Value == (int)currentCard.Value + 1)
                    return true;

            return IsWrapAround(potentialPlay, currentlyShown, GameMode.Ascending);
        }


        private static bool IsWrapAround(Card potentalPlay, Card currentlyShown, GameMode gameMode)
        {
            if (potentalPlay is RegularCard potentialCard && currentlyShown is RegularCard currentCard)
                if (gameMode == GameMode.Descending)
                    return potentialCard.Value == Values.King && currentCard.Value == Values.Ace;
                else
                    return potentialCard.Value == Values.Ace && currentCard.Value == Values.King;
            else
                return false;
        }

        private static bool IsSpecialCard(Card potentialPlay, Card currentCard, GameMode gameMode)
        {
            if (potentialPlay is SpecialCard specialCard)
                    SpecialCardAction();
                    
            return true;


            return true;
        }

        private static bool SpecialCardAction(SpecialCard specialCard)
        {
            switch(specialCard.CardType)
            {
                case SpecialCardType.Skip:
                    return true;
                case SpecialCardType.DrawTwo:
                    
                    return true;
                case SpecialCardType.DrawFour:

                    return true;
                case SpecialCardType.ChangeOrder:

                    return true;
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
            if (dealtCard is RegularCard regularCard)
                return regularCard.IsEqual(penaltyCard) ? PenaltyCardCount : 0;
            else
                return 0;
        }
    }
}