namespace SolitaireUno
{
    /// <summary>
    /// Represents the main game logic and flow for a card game between a human player and the computer.
    /// </summary>
    /// <remarks>The Game class manages the initialization of players and the deck, handles turn-based
    /// gameplay, and determines the win condition. It provides the entry point for starting and running a complete game
    /// session via the Main method. User interaction is performed through the console.</remarks>
    public class Game
    {
        Player player = new();
        Computer computer = new();
        Deck gameDeck = new();
        Card currentCard;

        private readonly IInputProvider _input;

        public Game(IInputProvider input, Deck deck)
        {
            _input = input;
            gameDeck = deck;

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
        
        public Player? StartGame()
        {
            Card penaltyCard = new Card(Suits.Spades, Values.Queen); // setting the penalty card

            while (player.Hand.Count > 0 && computer.Hand.Count > 0)
            {                    
                bool playerChoiceValid = false; // bool representing whether player made a correct option

                /* --------------------------- PLAYERS TURN ---------------------------- */

                while (!playerChoiceValid)
                {
                    player.ShowHand();
                    Console.WriteLine($"\n            The Current Card is... {currentCard}\n");


                    if (gameDeck.Length() > 0)
                    {
                        Console.WriteLine($"\nThere are {gameDeck.Length()} cards in the deck!");
                    }
                    else
                    {
                        Console.WriteLine("\nThere are no more cards in the deck!");
                    }

                    Console.WriteLine("\nPlay a Card (1 - # of Cards in Hand), Pick-Up (p.u), or Pass(p)");
                    string playerDecision = _input.GetInput().ToLower();
                    
                    if (playerDecision != null)
                    {
                        if (int.TryParse(playerDecision, out int decisionAsNumber))
                        {
                            if (decisionAsNumber > 0 && decisionAsNumber <= player.Hand.Count)
                            {
                                Card potentialCard = player.Hand[decisionAsNumber - 1];

                                if (GameMethods.ValidCard(potentialCard, currentCard))
                                {
                                    player.PlayCard(potentialCard);
                                    currentCard = potentialCard;

                                    Console.WriteLine($"\nYou played {potentialCard}, so...");
                                    playerChoiceValid = true;
                                }
                                else
                                {
                                    Console.WriteLine("\nThat is not a valid play, please choose again\n");
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
                                Card card = gameDeck.DealCard()!;
                                player.PickupCard(card);

                                int potentialPenaltyCount = GameMethods.GetPenaltyCount(card, penaltyCard);
                                
                                if(potentialPenaltyCount > 0)
                                {
                                    Console.WriteLine("\nYou decided to pick up and recieved the Queen of Spades! HAHAHAHA");
                                    Console.WriteLine("You recieved 5 additional cards because... why not...\n");
                                }
                                else
                                {
                                    Console.WriteLine("\nYou decided to pick up!");
                                }

                                for (int i = 0; i < potentialPenaltyCount; i++)
                                {
                                    player.PickupCard(gameDeck.DealCard()!);
                                }
                                
                                playerChoiceValid = true;
                            }
                            else
                            {
                                Console.WriteLine("\nThere are no more cards in the deck! Either play or pass!\n");
                            }
                        }

                        else if (playerDecision == "pass" || playerDecision == "p")
                        {
                            if (gameDeck.Length() > 0)
                            {
                                Console.WriteLine("\nThere are still cards in the deck, either play or pick up!\n");
                            }
                            else
                            {
                                playerChoiceValid = true;
                            }
                        }
                    }

                    else
                    {
                        Console.WriteLine("\nYou did not make a decision, please try again\n");
                    }
                }
                
       /* ------------------------- COMPUTERS TURN ------------------------- */

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
                            Console.WriteLine("\nComputer couldn't make a move... and picked up\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nComputer couldn't play a move and couldn't pick up... so it passed\n");
                    }
                }
            }

     /* --------------------- WIN CONDITION ---------------------- */

            Console.WriteLine("\nGame Over!");

            if(computer.Hand.Count == 0)
            {
                Console.WriteLine("\nYou Lose! You've been bested by the machine :(");
                return computer;
            }
            else if(player.Hand.Count == 0)
            {
                Console.WriteLine("\nYou Win! You beat the computer! Congrats! :)");
                return player;
            }

            return null;
        }

        /* --------------------- INTRO CONVERSATION ---------------------- */
        static void Main(string[] args)
        {
            Console.WriteLine("Are you ready to play...");
            string playerChoice = Console.ReadLine().ToLower();

            if(playerChoice == "yes" || playerChoice == "y")
            {
                bool validModeChoice = false;
                while (!validModeChoice)
                {
                    Console.WriteLine("\nNow... would you like to play the cards in ascending (a) or descending (d) order...?");
                    string playerGameModeChoice = Console.ReadLine().ToLower();


                    if (playerGameModeChoice.Equals("ascending") || playerGameModeChoice.Equals("descending")
                            || playerGameModeChoice.Equals("a") || playerGameModeChoice.Equals("d"))
                    {
                        if (playerGameModeChoice.Equals("ascending") || playerGameModeChoice.Equals("a"))
                        {
                            Console.WriteLine("\nAscending it is!");
                            validModeChoice = true;
                        }
                        else
                        {
                            Console.WriteLine("\nDescending it is!");
                            validModeChoice = true;
                        }
                    }
                    else 
                    {
                        Console.WriteLine("Please answer again, there may have been a mistake");
                    }
                }
                Console.WriteLine("Let's Gooooooooooooooo!\n");

                IInputProvider realInput = new ConsoleInput();
                Deck normalDeck = new();
                
                Game newGame = new(realInput, normalDeck);
                newGame.StartGame();
            }
            
            else if(playerChoice == "no" || playerChoice == "n")
            {
                Console.WriteLine("I understand, come back when you are ready");
            }

            else
            {
                Console.WriteLine("What? GoodBye");
            }
        }
    }
}
