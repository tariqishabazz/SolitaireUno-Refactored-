namespace SolitaireUno
{
    public class Player
    {
        public List<Card> playerHand = new List<Card>();
        public Player()
        {

        }
        
        public void PickupCard(Card card)
        {
            playerHand.Add(card);
        }
        public void PlayCard(Card card)
        {
            playerHand.Remove(card);
        }
        public void ShowHand()
        {                
            int index = 0;
            foreach(Card card in playerHand)
            {
                Console.WriteLine($"{index + 1}) {card}");
                index++;
            }
        }
    }
}
