namespace SolitaireUno
{
    public class Game
    {
        Player player = new Player();
        Computer computer = new Computer();
        Deck gameDeck = new Deck();
        Card currentCard;

        public Game()
        {
            for(int i = 0; i < 10; i++)
            {
                Card card = gameDeck.DealCard()!;
                player.PickupCard(card);
            }

            for (int i = 0; i < 10; i++)
            {
                Card card = gameDeck.DealCard()!;
                computer.PickupCard(card);
            }

            currentCard = gameDeck.DealCard()!;
        }
        public void StartGame()
        {
            while(player.playerHand.Count > 0 && computer.computerHand.Count > 0)
            {
                player.ShowHand();
                Console.WriteLine($"The Current Card is... {currentCard}");
                Console.WriteLine("Play a Card (1 - # of Cards in Hand), Pick-Up (p.u), or Pass(p)");
                string? playerDecision = Console.ReadLine().ToLower();

                if (int.TryParse(playerDecision, out int decisionAsNumber))
                {
                    if (decisionAsNumber > 0 && decisionAsNumber <= player.playerHand.Count)
                    {
                        if(GameMethods.ValidCard())
                        {
                            player.PlayCard(card);
                        }
                    }
                }

                else if (playerDecision == "p.u" || playerDecision == "pu" || playerDecision == "pick up" || playerDecision == "pickup")
                {
                    Card card = gameDeck.DealCard();
                    player.PickupCard(card);
                }

                else if (playerDecision == "pass" || playerDecision == "p")
                {
                    continue;
                }
            }
        }


        static void Main(string[] args)
        {

        }
    }
}

/*
 Next Steps for Your Return:
    Card Validation Logic: Inside your if (int.TryParse...) block, you need to check if the chosen card is actually in the hand and if it's a legal move using GameMethods.ValidCard.

    The Queen's Trap: Add the penalty logic for when a player draws the Queen of Spades (drawing 5 extra cards).

    The Computer's Turn: After the player's turn finishes, call computer.MakeMove(currentCard) to let the AI play.

    Win Condition: Add a final check outside the loop to announce who won once someone's hand reaches zero cards.
 
    When you return, remember that you’ll need to update the currentCard whenever a player or computer successfully plays a card from their hand!
 
 */