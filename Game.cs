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
            Card penaltyCard = new Card(Suits.Spades, Values.Queen);

            while (player.playerHand.Count > 0 && computer.computerHand.Count > 0)
            {                    
                bool playerChoiceValid = false;

                while (!playerChoiceValid)
                {
                    player.ShowHand();
                    Console.WriteLine($"\n            The Current Card is... {currentCard}\n");
                    

                    if(gameDeck.Length() > 0)
                    {
                        Console.WriteLine($"\nThere are {gameDeck.Length()} cards in the deck!");
                    }
                    else
                    {
                        Console.WriteLine("\nThere are no more cards in the deck!");
                    }
                    
                    Console.WriteLine("\nPlay a Card (1 - # of Cards in Hand), Pick-Up (p.u), or Pass(p)");
                    string? playerDecision = Console.ReadLine().ToLower();

                    if (int.TryParse(playerDecision, out int decisionAsNumber))
                    {
                        if (decisionAsNumber > 0 && decisionAsNumber <= player.playerHand.Count)
                        {
                            Card potentialCard = player.playerHand[decisionAsNumber - 1];

                            if (GameMethods.ValidCard(potentialCard, currentCard))
                            {
                                player.PlayCard(potentialCard);
                                currentCard = potentialCard;

                                Console.WriteLine($"\nYou played {potentialCard}, so...");
                                playerChoiceValid = true;
                            }
                            else
                            {
                                Console.WriteLine("\nThat is not a valid play, please choose again");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nSorry, that is an invalid input based on your current cards, please choose again.\n");
                        }
                    }

                    else if (playerDecision == "p.u" || playerDecision == "pu" || playerDecision == "pick up" || playerDecision == "pickup")
                    {
                        if (gameDeck.Length() > 0)
                        {
                            Card card = gameDeck.DealCard();
                            player.PickupCard(card);

                            if (card.IsEqual(penaltyCard))
                            {
                                Console.WriteLine("\nYou decided to pick up and recieved the Queen of Spades! HAHAHAHA");
                                Console.WriteLine("You recieved 5 additional cards because... why not...\n");

                                for (int i = 0; i < 5; i++)
                                {
                                    player.PickupCard(gameDeck.DealCard()!);
                                }

                                playerChoiceValid = true;
                            }
                            else
                            {
                                Console.WriteLine("\nYou decided to pick up!");
                                playerChoiceValid = true;
                            }

                        }
                        else
                        {
                            Console.WriteLine("\nThere are no more cards in the deck! Either play or pass!\n");
                        }
                    }

                    else if (playerDecision == "pass" || playerDecision == "p")
                    {
                        if(gameDeck.Length() > 0)
                        {
                            Console.WriteLine("\nThere are still cards in the deck, either play or pick up!\n");
                        }
                        else
                        {
                            playerChoiceValid = true;
                        }
                    }
                }
                Card? potentialComputerPlay = computer.MakeMove(currentCard);
                
                if(potentialComputerPlay != null)
                {
                    currentCard = potentialComputerPlay;
                    Console.WriteLine($"\nComputer played: {potentialComputerPlay}\n");
                }
                else
                {
                    if (gameDeck.Length() > 0)
                    {
                        Card card = gameDeck.DealCard();

                        computer.PickupCard(card);

                        if (card.IsEqual(penaltyCard))
                        {
                            Console.WriteLine("\nThe computer decided to pick up and recieved the Queen of Spades!");
                            Console.WriteLine("It recieved 5 additional cards because... why not...\n");

                            for (int i = 0; i < 5; i++)
                            {
                                computer.PickupCard(gameDeck.DealCard()!);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nComputer couldn't make a move... and picked up");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nComputer couldn't play a move and couldn't pick up... so it passed");
                    }
                }
            }
            Console.WriteLine("\nGame Over!\n");

            if(computer.computerHand.Count == 0)
            {
                Console.WriteLine("\nYou Lose! You've been bested by the machine :(");
            }
            else if(player.playerHand.Count == 0)
            {
                Console.WriteLine("\nYou Win! You beat the computer! Congrats! :)");
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Are you ready to play...");
            string playerChoice = Console.ReadLine().ToLower();

            if(playerChoice == "yes" || playerChoice == "y")
            {
                Console.WriteLine("Let's gooooooooooooooo!\n");

                Game newGame = new Game();
                newGame.StartGame();
            }
            
            else if(playerChoice == "no" || playerChoice == "n")
            {
                Console.WriteLine("I understand, come back when you are ready");
            }

            else
            {
                Console.WriteLine("What?");
            }
        }
    }
}
