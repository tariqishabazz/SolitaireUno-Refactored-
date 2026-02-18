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

        public static string PlayerGameModeChoice;

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
            Card penaltyCard = new(Suits.Spades, Values.Queen); // setting the penalty card

            while (player.Hand.Count > 0 && computer.Hand.Count > 0)
            {                    
                bool playerChoiceValid = false; // bool representing whether player made a correct option

                /* --------------------------- PLAYERS TURN ---------------------------- */

                while (!playerChoiceValid)
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    player.ShowHand();
                    Console.WriteLine($"\n            The Current Card is... {currentCard}");


                    if (gameDeck.Length() > 0)
                    {
                        Console.WriteLine($"\n                There are {gameDeck.Length()} cards in the deck!");
                    }
                    else
                    {
                        Console.WriteLine("\nThere are no more cards in the deck!");
                    }

                    Console.Write("\nPlay a Card (1 - # of Cards in Hand), Pick-Up (p.u), or Pass(p) >> ");
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

                                    Console.WriteLine("\n---------------------------------------------------------------------");
                                    Console.WriteLine($"You played {potentialCard}, so...");
                                    playerChoiceValid = true;
                                }
                                else
                                {
                                    Console.WriteLine("\n---------------------------------------------------------------------");
                                    Console.WriteLine("That is not a valid play, please choose again");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n---------------------------------------------------------------------");
                                Console.WriteLine("That is an invalid input based on your current cards, please choose again.");
                            }
                        }

                        else if (playerDecision == "p.u" || playerDecision == "pu" || playerDecision == "pick up" || playerDecision == "pickup")
                        {
                            if (gameDeck.Length() > 0)
                            {
                                Card card = gameDeck.DealCard()!;
                                player.PickupCard(card);

                                int playerPotentialPenaltyCount = GameMethods.GetPenaltyCount(card, penaltyCard);
                                if(playerPotentialPenaltyCount > 0)
                                {
                                    Console.WriteLine("\n---------------------------------------------------------------------");
                                    Console.WriteLine("You decided to pick up and recieved the Queen of Spades! HAHAHAHA");
                                    Console.WriteLine("You recieved 5 additional cards because... why not...");
                                }
                                else
                                {
                                    Console.WriteLine("\n---------------------------------------------------------------------");
                                    Console.WriteLine("You decided to pick up!");
                                }

                                for (int i = 0; i < playerPotentialPenaltyCount; i++)
                                {
                                    player.PickupCard(gameDeck.DealCard()!);
                                }
                                
                                playerChoiceValid = true;
                            }
                            else
                            {
                                Console.WriteLine("\n---------------------------------------------------------------------");
                                Console.WriteLine("There are no more cards in the deck! Either play or pass!");
                            }
                        }

                        else if (playerDecision == "pass" || playerDecision == "p")
                        {
                            if (gameDeck.Length() > 0)
                            {
                                Console.WriteLine("\n---------------------------------------------------------------------");
                                Console.WriteLine("There are still cards in the deck, either play or pick up!");
                            }
                            else
                            {
                                playerChoiceValid = true;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n---------------------------------------------------------------------");
                        Console.WriteLine("You did not make a decision, please try again");
                    }
                }
                
       /* ------------------------- COMPUTERS TURN ------------------------- */

                Card? potentialComputerPlay = computer.MakeMove(currentCard);
                
                if(potentialComputerPlay != null)
                {
                    currentCard = potentialComputerPlay;
                    Console.WriteLine($"\nComputer played: {potentialComputerPlay}");
                }
                else
                {
                    if (gameDeck.Length() > 0)
                    {
                        Card card = gameDeck.DealCard()!;
                        computer.PickupCard(card);

                        int computerPotentialPenaltyCount = GameMethods.GetPenaltyCount(card, penaltyCard);
                        if (computerPotentialPenaltyCount > 0)
                        {
                            Console.WriteLine("\nThe computer decided to pick up and recieved the Queen of Spades!");
                            Console.WriteLine("It recieved 5 additional cards because... why not...");

                            for (int i = 0; i < computerPotentialPenaltyCount; i++)
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
            Console.Write("Are you ready to play >> ");
            string playerChoice = Console.ReadLine().ToLower();

            if(playerChoice == "yes" || playerChoice == "y")
            {
                bool validModeChoice = false;
                while (!validModeChoice)
                {
                    Console.Write("\nNow... would you like to play the cards in ascending (a) or descending (d) order? >> ");
                    PlayerGameModeChoice = Console.ReadLine().ToLower();


                    if (PlayerGameModeChoice.Equals("ascending") || PlayerGameModeChoice.Equals("descending")
                            || PlayerGameModeChoice.Equals("a") || PlayerGameModeChoice.Equals("d"))
                    {
                        if (PlayerGameModeChoice.Equals("ascending") || PlayerGameModeChoice.Equals("a"))
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
                        Console.WriteLine("Please answer again, there may have been a mistake in your response");
                    }
                }
                Console.WriteLine("Let's Gooooooooooooooo!");

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

// Add new game modes/card types?
// Fix Queen of Spades penalty bug?
// Add new GameMethods test for new logic?