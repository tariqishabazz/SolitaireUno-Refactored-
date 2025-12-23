
namespace SolitaireUno
{
    public class Deck
    {
        Random random = new Random(); // creating random object for random number generation
        
        List<Card> deckCards = new List<Card>(); // creating a new, list object to hold all the deck cards

        public Deck() // the deck constructor essentially builds the card deck 
        {
            foreach (Values value in Enum.GetValues<Values>()) // it loops through all the values... 
            {
                foreach (Suits suit in Enum.GetValues<Suits>()) // and all the suits... to create unique cards for every value/suit
                {
                    deckCards.Add(new Card(suit, value)); // it then takes every card and adds it to our deck list 
                }
            }
            
            InHouseShuffle(); // to prevent unfairness, cards are randomized each round

            Card penaltyCard = new Card(Suits.Spades, Values.Queen); // one new card object is set as the penalty card
            
            int index = 0; // setting an index to track loop iteration 
            foreach (Card card in deckCards) // For each card in the deck...
            {
                if(card.IsEqual(penaltyCard)) // this checks to see if THAT specific card is equal to our previously set penalty card
                { 
                    break; //... if it is, we break the loop
                }
                index++; // increments the index
            }

            deckCards.RemoveAt(index); // once it is found, we then use the index the card was located, and remove the element at that position, that being the Queen of Spades
            
            int randomPosition = random.Next(5, 27); // we then create a random position the removed card will be placed back into, it can be towards the start of the deck or the end, ensuring additional randomness.
            deckCards.Insert(randomPosition, penaltyCard); // this places the card at the random position
        }
        
        /// <summary>
        /// Randomizes the order of the cards in the deck using an in-place shuffle algorithm.
        /// </summary>
        /// <remarks>This method modifies the current deck by rearranging its cards in a random order. The
        /// shuffle is performed in-place and affects the underlying collection directly. Call this method to ensure the
        /// deck is randomized before dealing or drawing cards.</remarks>
        public void InHouseShuffle()
        {
            for(int i = deckCards.Count - 1; i > 0; i--) // to shuffle each card, we use a for loop, this one starts at the end of the deck and works towards the beginning
            {
                int randomIndex = random.Next(0, i + 1); // takes a random number between 0 and i + 1

                Card temp = deckCards[i]; // this sets a temp Card variable to whatever Card is at index i
                deckCards[i] = deckCards[randomIndex]; // whatever Card is at i, will be switched to whatever Card is at the randomIndex previously stored
                deckCards[randomIndex] = temp; // then the Card at the randomIndex will be switched to the temp Card

            }
        }
        
        public int Length() // a custom method to display the length of the deck 
        {
            return deckCards.Count;
        }

        /// <summary>
        /// Removes and returns the top card from the deck. If the deck is empty, returns null.
        /// </summary>
        /// <remarks>Each call to this method reduces the number of cards in the deck by one. The method
        /// returns cards in last-in, first-out order, with the most recently added card dealt first.</remarks>
        /// <returns>The dealt <see cref="Card"/> object representing the top card of the deck, or null if the deck is empty.</returns>
        public Card? DealCard() // this method deals a single card ...
        {
            if(deckCards.Count == 0) // but IF the deck is empty, it can't
            {
                return null; // ... so it returns null
            }
            
            else // if the deck isn't empty...
            {
                int lastIndex = deckCards.Count - 1; // ... it takes the last index, which is the last card in the deck, hence deckCard.Count - 1
                Card lastCard = deckCards[lastIndex]; // it then takes the actual card from the deck and stores it into lastCard
                deckCards.RemoveAt(lastIndex); // we then remove the card at that last index
                
                return lastCard; // lastly, we return the newly removed card
            }
        }

    }
}

