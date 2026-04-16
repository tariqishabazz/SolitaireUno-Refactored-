using System.Collections;
using System.Linq;

namespace SolitaireUno
{
    public class Computer : Player
    {
        public Card? MakeMove(Card currentCard, int opponentHandSize)
        {
            List<Card> validMoves = [];
            foreach (Card card in Hand)
            {
                if (GameMethods.ValidCard(card, currentCard, MainGame.GameModeChoice))
                {
                    validMoves.Add(card);
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