
namespace SolitaireUno
{
    public class Deck
    {
        Random random = new Random();

        List<Card> deckCards = new List<Card>();

        public Deck()
        {
            foreach (Values value in Enum.GetValues<Values>())
            {
                foreach (Suits suit in Enum.GetValues<Suits>())
                {
                    deckCards.Add(new Card(suit, value));
                }
            }
            
            InHouseShuffle();

            Card penaltyCard = new Card(Suits.Spades, Values.Queen);
            
            int index = 0;
            foreach (Card card in deckCards)
            {
                if(card.IsEqual(penaltyCard))
                { 
                    break;
                }
                index++;
            }

            deckCards.RemoveAt(index);
            
            int randomPosition = random.Next(5, 27);
            deckCards.Insert(randomPosition, penaltyCard);
        }
        
        public void InHouseShuffle()
        {
            for(int i = deckCards.Count - 1; i > 0; i--)
            {
                int randomIndex = random.Next(0, i + 1);

                Card temp = deckCards[i];
                deckCards[i] = deckCards[randomIndex];
                deckCards[randomIndex] = temp;

            }
        }
        
        public int Length()
        {
            return deckCards.Count;
        }

        public Card? DealCard()
        {
            if(deckCards.Count == 0)
            {
                return null;
            }
            
            else
            {
                int lastIndex = deckCards.Count - 1;
                Card lastCard = deckCards[lastIndex];
                deckCards.RemoveAt(lastIndex);
                
                return lastCard;
            }
        }

    }
}

