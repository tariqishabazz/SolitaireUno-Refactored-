namespace SolitaireUno
{
    public class Card
    {
        public Suits Suit { get; }
        public Values Value { get; }

        public Card(Suits suit, Values value)
        {
            Suit = suit;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }

        public bool IsEqual(Card otherCard)
        { 
            if (otherCard != null)
            {
                return this.Value == otherCard.Value && this.Suit == otherCard.Suit;
            }
            
            return false;
        }
    }
}
