namespace SolitaireUno
{
    /// <summary>
    /// Represents a standard deck of playing cards, supporting shuffling, dealing, and special penalty card logic.
    /// </summary>
    /// <remarks>
    /// The Deck class builds a full deck, shuffles it, and ensures the penalty card (Queen of Spades) is inserted
    /// at a random position between indexes 20 and 45 to avoid early draws by either player.
    /// </remarks>
    public class Deck
    {
        Random random = new(); // Random number generator for shuffling and penalty card placement
        
        List<RegularCard> deckCards = []; // List to hold all cards in the deck
        List<SpecialCard> specialCards = [];

        /// <summary>
        /// Constructs a new deck, shuffles it, and inserts the penalty card (Queen of Spades) at a random safe position.
        /// </summary>
        public Deck()
        {
            // Build the deck: add every combination of value and suit
            foreach (Values value in Enum.GetValues<Values>())
            {
                foreach (Suits suit in Enum.GetValues<Suits>())
                {
                    deckCards.Add(new RegularCard(suit, value)); // Add each unique card
                }
            }
            
            foreach(SpecialCardType specialCard in Enum.GetValues<SpecialCardType>())
            {
                specialCards.Add(new SpecialCard(specialCard));
            }

            deckCards.AddRange(specialCards);

            InHouseShuffle(); // Shuffle the deck to randomize order

            RegularCard penaltyCard = new (Suits.Spades, Values.Queen); // Define the penalty card (Queen of Spades)
            
            // Find the index of the penalty card in the shuffled deck
            int index = 0;
            foreach (RegularCard card in deckCards)
            {
                if(card.IsEqual(penaltyCard)) // If this card is the penalty card
                { 
                    break; // Stop searching
                }
                index++; // Move to next card
            }

            deckCards.RemoveAt(index); // Remove the penalty card from its current position
            
            // Insert the penalty card at a random position between 20 and 45
            // This ensures neither player can draw it during the initial deal
            int randomPosition = random.Next(20, 45); // Pick a safe random index
            deckCards.Insert(randomPosition, penaltyCard); // Insert penalty card at the chosen position
        }
        
        /// <summary>
        /// Randomizes the order of the cards in the deck using an in-place shuffle algorithm.
        /// </summary>
        public void InHouseShuffle()
        {
            for(int i = deckCards.Count - 1; i > 0; i--)
            {
                int randomIndex = random.Next(0, i + 1); // Pick a random index
                RegularCard temp = deckCards[i]; // Store the card at i
                deckCards[i] = deckCards[randomIndex]; // Swap with card at randomIndex
                deckCards[randomIndex] = temp; // Complete the swap
            }
        }
        
        /// <summary>
        /// Returns the number of cards remaining in the deck.
        /// </summary>
        public int Length()
        {
            return deckCards.Count; // Return the count of cards left
        }

        /// <summary>
        /// Removes and returns the top card from the deck. If the deck is empty, returns null.
        /// </summary>
        /// <returns>The dealt card, or null if the deck is empty.</returns>
        public RegularCard? DealCard()
        {
            if(deckCards.Count == 0) // If the deck is empty
            {
                return null; // No card to deal
            }
            else
            {
                RegularCard topCard = deckCards[0]; // Get the top card
                deckCards.Remove(topCard); // Remove it from the deck
                return topCard; // Return the dealt card
            }
        }

        /// <summary>
        /// Constructs a deck from a pre-made list of cards (used for testing or custom setups).
        /// </summary>
        /// <param name="preMadeDeck">A list of cards to use as the deck.</param>
        public Deck(List<RegularCard> preMadeDeck)
        {
            deckCards = preMadeDeck; // Use the provided list as the deck
        }
    }
}

