namespace SolitaireUno
{
    /// <summary>
    /// Represents a player in the card game, managing the player's hand and actions.
    /// </summary>
    /// <remarks>The Player class provides methods to pick up, play, and display cards in the player's hand.
    /// It serves as the primary interface for interacting with a player's cards during gameplay.</remarks>
    public class Player 
    {
        public List<Card> playerHand = new List<Card>(); // creates a new, empty list, representing the players hand of cards
        public Player() //empty constructor... for now
        {

        }
        
        /// <summary>
        /// Adds the specified card to the player's hand.
        /// </summary>
        /// <param name="card">The card to add to the player's hand. Cannot be null.</param>
        public void PickupCard(Card card) //it takes a Card object as a parameter
        {
            playerHand.Add(card); //... and adds that card to the hand
        }

        /// <summary>
        /// Removes the specified card from the player's hand, representing the action of playing that card.
        /// </summary>
        /// <param name="card">The card to be played and removed from the player's hand. Cannot be null.</param>
        public void PlayCard(Card card) // this also takes a Card object
        {
            playerHand.Remove(card); // to play the card, it must remove it from the deck, indicating a move.
        }

        /// <summary>
        /// Displays the player's current hand to the console, listing each card with its position in the hand.
        /// </summary>
        /// <remarks>This method writes the hand to standard output. It is intended for console-based
        /// applications and may not be suitable for other user interfaces.</remarks>
        public void ShowHand()
        {
            Console.WriteLine("Your Hand: ");    // Title showing the player's hand
            int index = 0;                       // index to keep track of iteration
            foreach(Card card in playerHand)     // a foreach loop that goes through every Card object in the players hand (which is in memory before being shown)
            {
                Console.WriteLine($"   {index + 1}) {card}"); // "For each" card, it properly formats to be more pleasing, starting at 1, 1) Value of Suit
                index++;                                      // increment to properly number every card
            }
        }
    }
}
