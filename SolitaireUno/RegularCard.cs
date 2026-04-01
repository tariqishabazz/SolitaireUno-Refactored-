namespace SolitaireUno
{
    /// <summary>
    /// Represents a standard playing card with a suit and a value.
    /// </summary>
    /// <remarks>A Card is immutable and uniquely identified by its suit and value. Instances of this class
    /// can be compared for equality using the IsEqual method.</remarks>
    /// <remarks>
    /// Initializes a new instance of the Card class with the specified suit and value.
    /// </remarks>
    /// <param name="suit">The suit of the card to assign.</param>
    /// <param name="value">The value of the card to assign.</param>
    public class RegularCard(Suits suit, Values value)
    {
        public Suits Suit { get; } = suit;
        public Values Value { get; } = value;

        /// <summary>
        /// Returns a string that represents the value and suit of the card.
        /// </summary>
        /// <returns>A string in the format "{Value} of {Suit}", where Value is the card's value and Suit is the card's suit.</returns>
        public override string ToString() // this overrides the default ToString() method to create our own
        {
            return $"{Value} of {Suit}";
        }

        /// <summary>
        /// Determines whether the current card is equal to the specified card based on value and suit.
        /// </summary>
        /// <param name="otherCard">The card to compare with the current card. Can be null.</param>
        /// <returns>true if both cards have the same value and suit; otherwise, false.</returns>
        public bool IsEqual(RegularCard otherCard) // compares two Card objects, determining if they are equal 
        { 
            if (otherCard != null) // if the other card isn't null in memory... 
            {
                return this.Value == otherCard.Value && this.Suit == otherCard.Suit; // return the result IF the card in THIS instance has the same Value and Suit as another card
            }
            
            return false; // return false IF cards are not the same.
        }
    }
}
