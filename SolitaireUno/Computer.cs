using System.Collections;
using System.Linq;

namespace SolitaireUno
{
    /// <summary>
    /// Represents a computer-controlled player in a card game, managing its hand and automated moves.
    /// </summary>
    /// <remarks>
    /// The Computer class provides methods for managing the computer player's hand and making moves
    /// based on the current state of play. It is intended to be used as part of a card game where automated opponents
    /// are required.
    /// </remarks>
    public class Computer : Player
    {
        /// <summary>
        /// Attempts to play a valid card from the computer's hand based on the specified current card.
        /// </summary>
        /// <remarks>
        /// The method evaluates each card in the computer's hand in order and plays the first
        /// valid card it finds. If no valid move is available, the method returns null to indicate that the computer
        /// cannot play a card on this turn.
        /// </remarks>
        /// <param name="currentCard">The card currently in play. Used to determine which cards in the computer's hand are valid to play.</param>
        /// <returns>The card played from the computer's hand if a valid move is found; otherwise, null if no valid card can be played.</returns>
        public Card? MakeMove(Card currentCard, int opponentHandSize)
        {
            List<Card> validMoves = []; // List to store valid moves

            foreach (Card card in Hand) // Check each card in the computer's hand
            {
                if (GameMethods.ValidCard(card, currentCard, MainGame.GameModeChoice)) // If the card is a valid move
                {
                    validMoves.Add(card); // Add to valid moves
                }
            }

            if(validMoves.Count == 0)
            {
                return null;
            }

            List<Card> regularMoves = validMoves.Where(card => card is RegularCard).ToList();
            List<Card> specialMoves = validMoves.Where(card => card is SpecialCard).ToList();

            if(opponentHandSize <= 3)
            {
                Random random = new();
                
                if (specialMoves.Count > 0)
                {
                    Card randomSpecialMove = specialMoves[random.Next(specialMoves.Count)];
                    return randomSpecialMove;
                }
                else
                {
                    Card randomRegularMove = regularMoves[random.Next(regularMoves.Count)];
                    return randomRegularMove;
                }
            }
            else
            {
                Random random = new();

                if(regularMoves.Count > 0)
                {
                    Card randomRegularMove = regularMoves[random.Next(regularMoves.Count)];
                    return randomRegularMove;
                }
                else
                {
                    Card randomSpecialMove = specialMoves[random.Next(specialMoves.Count)];
                    return randomSpecialMove;
                }
            }
        }
    }
}