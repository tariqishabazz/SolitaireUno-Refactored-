namespace SolitaireUno
{
    /// <summary>
    /// Represents a computer-controlled player in a card game, managing its hand and automated moves.
    /// </summary>
    /// <remarks>The Computer class provides methods for managing the computer player's hand and making moves
    /// based on the current state of play. It is intended to be used as part of a card game where automated opponents
    /// are required.</remarks>
    public class Computer
    {
        public List<Card> computerHand = new List<Card>(); // creates a new, empty list to hold the computer's cards
        
        /// <summary>
        /// Adds the specified card to the computer player's hand.
        /// </summary>
        /// <param name="card">The card to add to the computer player's hand. Cannot be null.</param>
        public void PickupCard(Card card)
        {
            computerHand.Add(card);
        }
        
        /// <summary>
        /// Removes the specified card from the computer's hand, representing that the card has been played.
        /// </summary>
        /// <param name="card">The card to play. Must be present in the computer's hand.</param>
        public void PlayCard(Card card)
        {
            computerHand.Remove(card);
        }

        /// <summary>
        /// Attempts to play a valid card from the computer's hand based on the specified current card.
        /// </summary>
        /// <remarks>The method evaluates each card in the computer's hand in order and plays the first
        /// valid card it finds. If no valid move is available, the method returns null to indicate that the computer
        /// cannot play a card on this turn.</remarks>
        /// <param name="currentCard">The card currently in play. Used to determine which cards in the computer's hand are valid to play.</param>
        /// <returns>The card played from the computer's hand if a valid move is found; otherwise, null if no valid card can be
        /// played.</returns>
        public Card? MakeMove(Card currentCard) // this method takes a Card object... 
        {
            foreach(Card card in computerHand) // it also starts a foreach loop to run through all the computer's cards
            {
                if(GameMethods.ValidCard(card, currentCard)) // using our ValidCard method, if it returns true... 
                {
                    PlayCard(card); // the computer is allowed to play that card, PLayCard removes the logical play from the computer... 
                    return card; //... and returns that card
                }
            }
            return null; // if the computer couldn't find a good play, this returns null
        }
    }

}
