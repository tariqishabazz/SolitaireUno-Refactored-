using System.Collections.Generic;

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
        
        private readonly List<Card>? gameDeck = [];
        private readonly Stack<Card> transformedDeck = [];

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
                    gameDeck.Add(new RegularCard(suit, value)); // Add each unique card
                }
            }
            
            foreach(SpecialCardType specialCard in Enum.GetValues<SpecialCardType>())
            {
                gameDeck.Add(new SpecialCard(specialCard));
            }

            RegularCard penaltyCard = new (Suits.Spades, Values.Queen); // Define the penalty card (Queen of Spades)
            
            // Find the index of the penalty card in the shuffled deck
            int index = 0;
            foreach (Card card in gameDeck)
            {
                if (card is RegularCard regularCard)
                    if (regularCard.IsEqual(penaltyCard))
                        break;            
                index++; // Move to next card
            }
            
            InHouseShuffle(); // Shuffle the deck to randomize order

            gameDeck.RemoveAt(index); // Remove the penalty card from its current position
            
            // Insert the penalty card at a random position between 20 and 45
            // This ensures neither player can draw it during the initial deal
            int randomPosition = random.Next(20, 45); // Pick a safe random index
            gameDeck.Insert(randomPosition, penaltyCard); // Insert penalty card at the chosen position

            foreach(Card card in gameDeck)
                transformedDeck.Push(card);

            gameDeck = null; // the list verson of deck no longer needed
        }

        /// <summary>
        /// Randomizes the order of the cards in the deck using an in-place shuffle algorithm.
        /// </summary>
        public void InHouseShuffle()
        {
            if (gameDeck is not null)
            {
                for (int i = gameDeck.Count - 1; i > 0; i--)
                {
                    int randomIndex = random.Next(0, i + 1); // Pick a random index
                    (gameDeck[randomIndex], gameDeck[i]) = (gameDeck[i], gameDeck[randomIndex]); // Store the card at i
                }
            }
        }


        /// <summary>
        /// Returns the number of cards remaining in the deck.
        /// </summary>
        public int Length() => transformedDeck.Count;

        /// <summary>
        /// Removes and returns the top card from the deck. If the deck is empty, returns null.
        /// </summary>
        /// <returns>The dealt card, or null if the deck is empty.</returns>
        public Card? DealCard()
        {
            if (transformedDeck.Count != 0) // If the deck isn't empty
                return transformedDeck.Pop(); // return a card
            else
                return null; // else return null
        }

        /// <summary>
        /// Constructs a deck from a pre-made list of cards (used for testing or custom setups).
        /// </summary>
        /// <param name="preMadeDeck">A list of cards to use as the deck.</param>
        public Deck(Stack<Card> preMadeDeck)
        {
            transformedDeck = preMadeDeck; // Use the provided list as the deck
        }
    }
}

