namespace SolitaireUno
{
    public class RegularCard(Suits suit, Values value) : Card
    {
        public Suits Suit { get; } = suit;
        public Values Value { get; } = value;
        
        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
        
        public bool IsEqual(Card otherCard)
        {
            if (otherCard != null && otherCard is RegularCard regularCard)
            {
                return this.Value == regularCard.Value && this.Suit == regularCard.Suit;
            }
            return false;
        }
    }
}
